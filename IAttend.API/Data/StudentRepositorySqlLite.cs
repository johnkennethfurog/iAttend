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
            var student =  _dataContext.Students.Where(stud => stud.ID == StudentID).AsQueryable();

            student = student
            .Include(stud => stud.StudentSubjects)
            .ThenInclude(subj => subj.Schedule)
            .ThenInclude(sched => sched.Instructor)
            .AsQueryable();

            student = student.Include(stud => stud.StudentSubjects)
            .ThenInclude(subj => subj.Schedule)
            .ThenInclude(sched => sched.Subject);


            var stdnt = await student.FirstOrDefaultAsync();


            var subjects = stdnt.StudentSubjects.ToList();

            return subjects;
        } 

        public async Task<List<StudentSubject>> GetStudentSubjects(string StudentNumber)
        {
            var student = await _dataContext.Students.FirstOrDefaultAsync(stud => stud.StudentNumber == StudentNumber); 
            return student.StudentSubjects.ToList();
        }

        private async Task<int> GetStudentId(string StudentNumber)
        {
            var studend = await _dataContext.Students.FirstOrDefaultAsync(stud => stud.StudentNumber == StudentNumber);
            return studend.ID;
        }
    }
}