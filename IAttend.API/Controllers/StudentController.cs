using System.Threading.Tasks;
using IAttend.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace IAttend.API.Controllers
{
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IInstructorRepository _instructorRepository;
        private readonly ISubjectRepository _subjectRepository;

        public StudentController(
            IStudentRepository studentRepository,
            IInstructorRepository instructorRepository,
            ISubjectRepository subjectRepository)
        {
            _studentRepository = studentRepository;
            _instructorRepository = instructorRepository;
            _subjectRepository = subjectRepository;
        }

        [HttpGet("getSubject/{StudentId}")]
        public async Task<IActionResult> GetSubject(int StudentId)
        {
            var student = await _studentRepository.GetStudentSubjects(StudentId);

            return Ok();
        }
    }
}