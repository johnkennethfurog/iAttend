using System.Collections.Generic;
using System.Threading.Tasks;
using IAttend.API.Dtos;
using IAttend.API.Pocos;

namespace IAttend.API.Data
{
    public interface IStudentRepository
    {
        Task<Student> GetStudent(string StudentNumber);
        Task<List<StudentSubject>> GetStudentSubjects(string StudentNumber);
        Task<IAttend.API.Models.ContactPerson> GetContactPerson(string studentNumber);
    }
}