using System.Collections.Generic;
using System.Threading.Tasks;
using IAttend.API.Models;

namespace IAttend.API.Data
{
    public interface IInstructorRepository
    {
         Task<Instructor> GetInstructor(string InstructorNumber);
         Task<List<Pocos.TeacherSubject>> GetSchedules(string InstructorNumber);
        Task<Pocos.TeacherSubject> GetSchedule(string InstructorNumber, int schedId);
    }
}