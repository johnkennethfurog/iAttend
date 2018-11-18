using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using IAttend.API.Data;
using IAttend.API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace IAttend.API.Controllers
{
    [Route("api/[controller]")]
    public class TeacherController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IInstructorRepository _instructorRepository;
        private readonly IMapper _mapper;

        public TeacherController(
            IStudentRepository studentRepository,
            IAttendanceRepository attendanceRepository,
            IInstructorRepository instructorRepository,
            IMapper mapper)
        {
            _studentRepository = studentRepository;
            _attendanceRepository = attendanceRepository;
            _instructorRepository = instructorRepository;
            _mapper = mapper;
        }

        [HttpPost("attendance")]
        public async Task<IActionResult> MarkAttendance([FromBody] TeacherToStudentAttendanceDto studentAttendance )
        {
            var attendance = await _attendanceRepository.GetAttendance(studentAttendance.ScheduleId,studentAttendance.Date);
            
            if(attendance == null)
                attendance = await _attendanceRepository.StartAttendanceSession(studentAttendance.ScheduleId);

            var success =  await _attendanceRepository.MarkAtendance(attendance.ID,studentAttendance.StudentNumber,false);

            if(success)
                return Ok(success);
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

                if(removed)
                    return Ok(removed);
                else
                    return NotFound("Unable to unmarked student attendance");
        }

        [HttpGet("{instuctorNumber}/subjects")]
        public async Task<IActionResult> GetSubjects(string instuctorNumber)
        {
            var schedules =  await _instructorRepository.GetSchedules(instuctorNumber);

            var teachersLoad = _mapper.Map<List<TeacherSubjectDto>>(schedules);

            return Ok(teachersLoad);
        }

        [HttpPost("{scheduleId}/start")]
        public async Task<IActionResult> StartAttendanceSession(int scheduleId)
        {
            var attendance = await _attendanceRepository.StartAttendanceSession(scheduleId);

            if(attendance != null)
            {
                var attendanceDto = _mapper.Map<ActiveAttendanceSessionDto>(attendance);
                return Ok(attendanceDto);
            }
            else
                return NotFound( new ErrorDto("Unable to start new attendance session"));
        }

        [HttpPut("{attendanceSession}/stop")]
        public async Task<IActionResult> StopAttendanceSession(int attendanceSession)
        {
            var started = await _attendanceRepository.StopAttendanceSession(attendanceSession);

            if(started)
                return Ok(true);
            else
                return NotFound(new ErrorDto("Unable to stop new attendance session"));
        }

        [HttpGet("{scheduleId}/attendance/{date}")]
        public async Task<IActionResult> GetAttendance(int scheduleId,DateTime date)
        {
            var studentsAttendance = await _attendanceRepository.GetStudentAttendances(scheduleId,date);

            var studentDto = _mapper.Map<List<StudentDto>>(studentsAttendance);

            return Ok(studentDto);
        }
    }
}