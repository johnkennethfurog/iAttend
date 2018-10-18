using System.Collections.Generic;
using System.Threading.Tasks;
using IAttend.API.Dtos;
using IAttend.API.Models;

namespace IAttend.API.Data
{
    public interface IStudentRepository
    {
        Task<Student> GetStudent(string StudentNumber);
    
        Task<Student> GetStudent(int StudentId);

        Task<List<Student>> GetStudents();

        Task<List<StudentSubject>> GetStudentSubjects(string StudentNumber);
        Task<List<StudentSubject>> GetStudentSubjects(int StudentId);
    }
}