using System.Collections.Generic;
using System.Threading.Tasks;
using IAttend.API.Models;

namespace IAttend.API.Data
{
    public interface IInstructorRepository
    {
         Task<Instructor> GetSubject(int InstructorId);
         Task<Instructor> GetSubject(string InstructorNumber);
         Task<List<Instructor>> GetInstructors();
    }
}