using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using IAttend.API.Data;
using IAttend.API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace IAttend.API.Controllers
{
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IInstructorRepository _instructorRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IMapper _mapper;

        public StudentController(
            IStudentRepository studentRepository,
            IInstructorRepository instructorRepository,
            ISubjectRepository subjectRepository,
            IAttendanceRepository attendanceRepository,
            IMapper mapper)
        {
            _studentRepository = studentRepository;
            _instructorRepository = instructorRepository;
            _subjectRepository = subjectRepository;
            _attendanceRepository = attendanceRepository;
            _mapper = mapper;
        }

        [HttpGet("subjects/{StudentNumber}")]
        public async Task<IActionResult> GetSubject(string StudentNumber)
        {
            var studentSubjects = await _studentRepository.GetStudentSubjects(StudentNumber);
            var studentSubjectDto = new List<StudentSubjectDto>();
            studentSubjects.ForEach(subj =>
            {
                var sched = subj.Schedule;
                var instructor = sched.Instructor;

                var subjectDto = _mapper.Map<SubjectDto>(sched);
                var teacherDto = _mapper.Map<TeacherDto>(instructor);

                studentSubjectDto.Add(new StudentSubjectDto{ Subject = subjectDto,Instructor = teacherDto });
            });

            return Ok(studentSubjectDto);
        }

        [HttpPost("attendance")]
        public async Task<IActionResult> MarkAttendance([FromBody]StudentToAttendanceDto studentAttendanceDto)
        {
            var isOpen = await _attendanceRepository.DoesAttendanceExistAndIsActive(studentAttendanceDto.AttendanceSessionId);

            if(isOpen)
            {
                var attendanceCreated = await _attendanceRepository.MarkAtendance(
                    studentAttendanceDto.AttendanceSessionId,
                    studentAttendanceDto.StudentNumber,
                    true);

                if(attendanceCreated)
                return Ok();
            }

            return NotFound();
        }

        [HttpGet("{studentNumber}/attendances/{scheduleId}")]
        public async Task<IActionResult> GetAttendanceForSubject(string studentNumber,int scheduleId)
        {
            var attendances = await _attendanceRepository.GetStudentAttendances(scheduleId,studentNumber);

            var studentAttendanceDto = _mapper.Map<List<StudentAttendanceDto>>(attendances);
            return Ok(studentAttendanceDto);
        }
    }
}