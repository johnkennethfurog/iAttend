using System.Collections.Generic;
using System.Threading.Tasks;
using IAttend.API.Models;

namespace IAttend.API.Data
{
    public interface IInstructorRepository
    {
         Task<Instructor> GetInstructor(int InstructorId);
         Task<Instructor> GetInstructor(string InstructorNumber);
         Task<List<Instructor>> GetInstructors();
         Task<List<Schedule>> GetSchedules(string InstructorNumber);
         Task<List<Student>> GetStudents(int ScheduleId);
    }
}