using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IAttend.API.Models;
using Microsoft.EntityFrameworkCore;

namespace IAttend.API.Data
{
    public class StudentRepositorySqlLite : IStudentRepository
    {
        private readonly DataContext _dataContext;

        public StudentRepositorySqlLite(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Student> GetStudent(string StudentNumber)
        {
            return await _dataContext.Students.FirstOrDefaultAsync(stud => stud.StudentName == StudentNumber);
        }

        public async Task<Student> GetStudent(int StudentId)
        {
            return await _dataContext.Students.FirstOrDefaultAsync(stud => stud.ID == StudentId);
        }

        public async Task<List<Student>> GetStudents()
        {
            return await _dataContext.Students.ToListAsync();
        }

        public async Task<List<StudentSubject>> GetStudentSubjects(int StudentID)
        {
            return await _dataContext.StudentSubjects.Where(stud => stud.StudentID == StudentID).ToListAsync();
        }

        public async Task<List<StudentSubject>> GetStudentSubjects(string StudentNumber)
        {
            var studId = await GetStudentId(StudentNumber);
            return await _dataContext.StudentSubjects.Where(subj => subj.ScheduleID == studId)
            .Include(subj => subj.Schedule).ToListAsync();
        }

        private async Task<int> GetStudentId(string StudentNumber)
        {
            var studend = await _dataContext.Students.FirstOrDefaultAsync(stud => stud.StudentNumber == StudentNumber);
            return studend.ID;
        }
    }
}