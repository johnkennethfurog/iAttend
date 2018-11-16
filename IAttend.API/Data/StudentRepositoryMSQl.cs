using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IAttend.API.Pocos;
using Microsoft.EntityFrameworkCore;

namespace IAttend.API.Data
{
    public class StudentRepositoryMSQl : IStudentRepository
    {
        private readonly DataContext _dataContext;

        public StudentRepositoryMSQl(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Student> GetStudent(string StudentNumber)
        {
            return await _dataContext.StudentsView.FirstOrDefaultAsync(student => student.StudentNumber == StudentNumber);
        }

        public async Task<List<StudentSubject>> GetStudentSubjects(string StudentNumber)
        {
            return await _dataContext.StudentsSubjectsView.Where(x=> x.StudentNumber == StudentNumber).ToListAsync();
        }
    }
}