using IAttend.API.Models;
using IAttend.API.Pocos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAttend.API.Data
{
    public class InstructorRepositoryMSql : IInstructorRepository
    {
        private readonly DataContext _dataContext;
        public InstructorRepositoryMSql(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Instructor> GetInstructor(string InstructorNumber)
        {
            return await _dataContext.Instructors.FirstOrDefaultAsync(x => x.InstructorNumber == InstructorNumber);
        }

        public async Task<TeacherSubject> GetSchedule(string InstructorNumber, string subjectCode)
        {
            var subjects = _dataContext.TeacherSubjects.FromSql("select * from tvfTeachersLoad({0},0,{1})", InstructorNumber,subjectCode);
            if (subjects.Count() == 0)
                return null;

            return await subjects.FirstOrDefaultAsync();
        }

        public async Task<List<TeacherSubject>> GetSchedules(string InstructorNumber)
        {

            var subjects =  _dataContext.TeacherSubjects.FromSql("select * from tvfTeachersLoad({0},1,'')", InstructorNumber);
            if (subjects.Count() == 0)
                return null;

            return await subjects.ToListAsync();
        }

    }
}
