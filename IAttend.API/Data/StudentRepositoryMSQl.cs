using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IAttend.API.Helpers;
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

        public Task<Student> AddStudent(Student Student)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteStudent(string StudentNumber)
        {
            throw new System.NotImplementedException();
        }

        public Task<Student> EditStudent(Student Student)
        {
            throw new System.NotImplementedException();

        }

        public async Task<IAttend.API.Models.ContactPerson> GetContactPerson(string studentNumber)
        {
            var students = _dataContext.Students.Where(x => x.StudentNumber == studentNumber)
                .AsQueryable()
                .Include(x => x.ContactPerson);

            var student = await students.FirstOrDefaultAsync();

            if (student != null) 
                return student.ContactPerson;
            else
                return null;
        }

        public async Task<Student> GetStudent(string StudentNumber)
        {
            return await _dataContext.StudentsView.FirstOrDefaultAsync(student => student.StudentNumber == StudentNumber);
        }

        public async Task<PagedList<Student>> GetStudents(PaginatedParams StudentParams)
        {
            return await PagedList<Student>.CreatePagedList(_dataContext.StudentsView,StudentParams.PageNumber,StudentParams.PageSize);
        }

        public async Task<List<StudentSubject>> GetStudentSubjects(string StudentNumber)
        {
            return await _dataContext.StudentsSubjectsView.Where(x=> x.StudentNumber == StudentNumber).ToListAsync();
        }
    }
}