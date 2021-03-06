using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using IAttend.API.Data;
using IAttend.API.Dtos;
using IAttend.API.Helpers;
using IAttend.API.Models;
using IAttend.API.Services;
using IAttend.API.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace IAttend.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class TeacherController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IInstructorRepository _instructorRepository;
        private readonly IHubContext<NotifyHub, ITypeHubClient> _hubContext;
        private readonly ICommunication _communication;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IMapper _mapper;

        public TeacherController(
            IStudentRepository studentRepository,
            IAttendanceRepository attendanceRepository,
            IInstructorRepository instructorRepository,
            IHubContext<NotifyHub, ITypeHubClient> hubContext,
            ICommunication communication,
            IScheduleRepository scheduleRepository,
            IMapper mapper)
        {
            _studentRepository = studentRepository;
            _attendanceRepository = attendanceRepository;
            _instructorRepository = instructorRepository;
            _hubContext = hubContext;
            _communication = communication;
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
        }

        [HttpPost("attendance")]
        public async Task<IActionResult> MarkAttendance([FromBody] TeacherToStudentAttendanceDto studentAttendance)
        {
            var attendance = await _attendanceRepository.GetAttendance(studentAttendance.ScheduleId, studentAttendance.Date);

            if (attendance == null)
                attendance = await _attendanceRepository.StartAttendanceSession(studentAttendance.ScheduleId, studentAttendance.Date, false);

            if (await _attendanceRepository.DoesStudentHasAttendance(studentAttendance.StudentNumber, attendance.ID))
                return Ok(true);

            var success = await _attendanceRepository.MarkAtendance(attendance.ID, studentAttendance.StudentNumber, false, string.Empty);

            if (success)
            {
                var contactPerson = await _studentRepository.GetContactPerson(studentAttendance.StudentNumber);

                if (contactPerson != null)
                    Task.Run(() =>
                   {
                       _communication.SendSms(_communication.GenerateSmsMessageForGuardian(studentAttendance.StudentName, studentAttendance.Subject, studentAttendance.Time), contactPerson.MobileNumber);
                   });

                return Ok(success);
            }
            else
                return NotFound("Unable to mark attendance");
        }

        [HttpPut("attendance")]
        public async Task<IActionResult> UnMarkAttendance([FromBody] TeacherToRemoveStudentAttendance studentAttendance)
        {
            var removed = await _attendanceRepository.UnMarkAtendance(
                studentAttendance.StudentNumber,
                studentAttendance.ScheduleId,
                studentAttendance.Date);

            if (removed)
                return Ok(removed);
            else
                return NotFound("Unable to unmarked student attendance");
        }

        [HttpGet("subjects")]
        public async Task<IActionResult> GetSubjects()
        {
            var schedules = await _instructorRepository.GetSchedules((User.GetInstructorNumber()));

            var teachersLoad = _mapper.Map<List<TeacherSubjectDto>>(schedules);

            return Ok(teachersLoad);
        }

        [HttpPost("{room}/{scheduleId}/start")]
        public async Task<IActionResult> StartAttendanceSession(int scheduleId, string room)
        {
            var attendance = await _attendanceRepository.StartAttendanceSession(scheduleId, null);

            if (attendance != null)
            {
                var attendanceDto = _mapper.Map<ActiveAttendanceSessionDto>(attendance);

                var students = await _attendanceRepository.GetSchedulesMasterList(scheduleId);

                await _hubContext.Clients.All.BroadcastMessage(room, students, attendance.ID, scheduleId, attendance.Guid);

                return Ok(attendanceDto);
            }
            else
                return NotFound(new ErrorDto("Unable to start new attendance session"));
        }

        [HttpPut("{room}/{attendanceSession}/stop")]
        public async Task<IActionResult> StopAttendanceSession(int attendanceSession, string room)
        {
            var started = await _attendanceRepository.StopAttendanceSession(attendanceSession);

            if (started)
            {
                await _hubContext.Clients.All.StopBroadcasting(room);
                return Ok(true);
            }
            else
                return NotFound(new ErrorDto("Unable to stop new attendance session"));
        }

        [HttpPut("{room}/{subjectId}/stopAll")]
        public async Task<IActionResult> StopAllAttendanceSession(int subjectId, string room)
        {
            var started = await _attendanceRepository.StopAllAttendanceSession(subjectId);

            if (started)
            {
                await _hubContext.Clients.All.StopBroadcasting(room);
                return Ok(true);
            }
            else
                return NotFound(new ErrorDto("Unable to stop new attendance session"));
        }

        [HttpGet("{scheduleId}/attendance/{date}")]
        public async Task<IActionResult> GetAttendance(int scheduleId, DateTime date)
        {
            var studentsAttendance = await _attendanceRepository.GetStudentAttendances(scheduleId, date);

            var studentDto = _mapper.Map<List<StudentDto>>(studentsAttendance);

            return Ok(studentDto);
        }

        [HttpGet("{studentNumber}/attendances/{scheduleId}")]
        public async Task<IActionResult> GetStudentsAttendance(string studentNumber, int scheduleId)
        {
            var attendances = await _attendanceRepository.GetStudentAttendances(scheduleId, studentNumber);

            var studentAttendanceDto = _mapper.Map<List<StudentAttendanceDto>>(attendances);
            return Ok(studentAttendanceDto);
        }

        [HttpPost("generateReport")]
        public async Task<IActionResult> GenerateReport([FromBody] ReportFilterDto reportFilter)
        {
            //var reportGenerationTasks = new List<Task<bool>>();
            //reportFilter.Subjects.ForEach(x =>
            //{
            //    reportGenerationTasks.Add(_attendanceRepository.GenerateAttendancesReport(x.Name, x.Time, x.SchedID, reportFilter.DateFrom, reportFilter.DateTo));
            //});

            //var res = await Task.WhenAll(reportGenerationTasks.ToArray());

            foreach (var x in reportFilter.Subjects)
                await _attendanceRepository.GenerateAttendancesReport(User.GetEmail(), x.Name, x.Time, x.SchedID, reportFilter.DateFrom, reportFilter.DateTo);

            return Ok(true);
        }

        [HttpGet("studentAbsent")]
        public async Task<IActionResult> GetAbents()
        {

            var studentAbsents = await _attendanceRepository.GetStudentsAbsent(User.GetInstructorNumber(), 6);

            var studentAbsentsDto = _mapper.Map<StudentAbsentDto>(studentAbsents);

            return Ok(studentAbsentsDto);

        }

        [HttpPost("schedule")]
        public async Task<IActionResult> AddScehedule([FromBody]AddScheduleDto sched)
        {

            var schedToAdd = new Schedule();

            var instuctor = await _instructorRepository.GetInstructor(User.GetInstructorNumber());
            var subj = await _scheduleRepository.GetSubject(sched.SubjectCode);

            schedToAdd.Subject = subj;
            schedToAdd.Instructor = instuctor;
            schedToAdd.Room = sched.Room;
            schedToAdd.TimeFrom = sched.TimeFrom;
            schedToAdd.TimeTo = sched.TimeTo;

            var addedSched = await _scheduleRepository.AddSchedule(schedToAdd);
            var pcoAddedSched = await _instructorRepository.GetSchedule(User.GetInstructorNumber(), addedSched.ID);
            var dtoSched = _mapper.Map<TeacherSubjectDto>(pcoAddedSched);


            return Ok(dtoSched);
        }

        [HttpPut("schedule")]
        public async Task<IActionResult> EditScehedule([FromBody]AddScheduleDto sched)
        {
            var addedSched = await _scheduleRepository.EditSchedule(sched);

            var pcoAddedSched = await _instructorRepository.GetSchedule(User.GetInstructorNumber(), addedSched.ID);
            var dtoSched = _mapper.Map<TeacherSubjectDto>(pcoAddedSched);


            return Ok(dtoSched);
        }

        [HttpDelete("schedule/{schedId}")]
        public async Task<IActionResult> DeleteScehedule(int schedId)
        {
            var deleted = await _scheduleRepository.DeleteSchedule(schedId);
            return Ok(deleted);
        }

        [HttpGet("schedule/{schedId}")]
        public async Task<IActionResult> GetScehedule(int schedId)
        {
            var sched = await _scheduleRepository.GetSchedule(schedId);
            var schedDto = _mapper.Map<AddScheduleDto>(sched);
            return Ok(schedDto);

        }



    }
}