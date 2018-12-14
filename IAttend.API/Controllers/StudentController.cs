using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using IAttend.API.Data;
using IAttend.API.Dtos;
using IAttend.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace IAttend.API.Controllers
{
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IInstructorRepository _instructorRepository;
        private readonly IScheduleRepository _subjectRepository;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly ICommunication _communication;
        private readonly IMapper _mapper;

        public StudentController(
            IStudentRepository studentRepository,
            IInstructorRepository instructorRepository,
            IScheduleRepository subjectRepository,
            IAttendanceRepository attendanceRepository,
            ICommunication communication,
            IMapper mapper)
        {
            _studentRepository = studentRepository;
            _instructorRepository = instructorRepository;
            _subjectRepository = subjectRepository;
            _attendanceRepository = attendanceRepository;
            _communication = communication;
            _mapper = mapper;
        }

        [HttpGet("subjects/{StudentNumber}")]
        public async Task<IActionResult> GetSubject(string StudentNumber)
        {
            var studentSubjects = await _studentRepository.GetStudentSubjects(StudentNumber);
            var studentSubjectDto = _mapper.Map<List<StudentSubjectDto>>(studentSubjects);

            return Ok(studentSubjectDto);
        }

        [HttpPost("attendance")]
        public async Task<IActionResult> MarkAttendance([FromBody]StudentToAttendanceDto studentAttendanceDto)
        {
            if (!await _attendanceRepository.DoesAttendanceExistAndIsActive(studentAttendanceDto.AttendanceSessionId))
                return BadRequest(new ErrorDto("No Attendance Session found"));

            if (await _attendanceRepository.DoesStudentHasAttendance(studentAttendanceDto.StudentNumber, studentAttendanceDto.AttendanceSessionId))
                return BadRequest(new ErrorDto("Study already have attendance"));


            var attendanceCreated = await _attendanceRepository.MarkAtendance(
                studentAttendanceDto.AttendanceSessionId,
                studentAttendanceDto.StudentNumber,
                true,
                studentAttendanceDto.Guid);

            if (attendanceCreated)
            {
                var contactPerson = await _studentRepository.GetContactPerson(studentAttendanceDto.StudentNumber);

                if (contactPerson != null)
                    Task.Run(() =>
                    {
                        _communication.SendSms(_communication.GenerateSmsMessageForGuardian(studentAttendanceDto.StudentName, studentAttendanceDto.Subject, studentAttendanceDto.Time), contactPerson.MobileNumber);
                    });

                return Ok(true);
            }
            else
                return BadRequest(new ErrorDto("Unable to mark students attendance"));
        }

        [HttpGet("{studentNumber}/attendances/{scheduleId}")]
        public async Task<IActionResult> GetAttendanceForSubject(string studentNumber,int scheduleId)
        {
            var attendances = await _attendanceRepository.GetStudentAttendances(scheduleId,studentNumber);

            var studentAttendanceDto = _mapper.Map<List<StudentAttendanceDto>>(attendances);
            return Ok(studentAttendanceDto);
        }


        [HttpGet("confirm/{studentNumber}")]
        public async Task<IActionResult> GetStudentInfo(string studentNumber)
        {
            var student = await _studentRepository.GetStudent(studentNumber);

            if (student == null)
                return NotFound(new ErrorDto("Invalid student number"));

            var studentDto = _mapper.Map<StudentConfirmationDto>(student);

            return Ok(studentDto);
        }
    }
}