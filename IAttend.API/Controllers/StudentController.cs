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
        private readonly IMapper _mapper;

        public StudentController(
            IStudentRepository studentRepository,
            IInstructorRepository instructorRepository,
            ISubjectRepository subjectRepository,
            IMapper mapper)
        {
            _studentRepository = studentRepository;
            _instructorRepository = instructorRepository;
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        [HttpGet("getSubject/{StudentId}")]
        public async Task<IActionResult> GetSubject(int StudentId)
        {
            var studentSubjects = await _studentRepository.GetStudentSubjects(StudentId);
            var studentSubjectDto = new List<StudentSubjectDto>();
            studentSubjects.ForEach(subj =>
            {
                var sched = subj.Schedule;
                var instructor = sched.Instructor;

                var subjectDto = _mapper.Map<SubjectDto>(sched);
                var instructorDto = _mapper.Map<InstructorDto>(instructor);

                studentSubjectDto.Add(new StudentSubjectDto{ Subject = subjectDto,Instructor = instructorDto });
            });

            return Ok(studentSubjectDto);
        }
    }
}