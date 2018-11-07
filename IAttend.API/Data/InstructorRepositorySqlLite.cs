using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IAttend.API.Models;
using Microsoft.EntityFrameworkCore;

namespace IAttend.API.Data
{
    public class InstructorRepositorySqlLite : IInstructorRepository
    {
        private readonly DataContext _dataContext;
        public InstructorRepositorySqlLite(DataContext dataContext)
        {
            _dataContext = dataContext;

        }
        public async Task<List<Instructor>> GetInstructors()
        {
            return await _dataContext.Instructors.ToListAsync();
        }

        public async Task<Instructor> GetInstructor(int InstructorId)
        {
            return await _dataContext.Instructors.FirstOrDefaultAsync(inst => inst.ID == InstructorId);
        }

        public async Task<Instructor> GetInstructor(string InstructorNumber)
        {
            return await _dataContext.Instructors.FirstOrDefaultAsync();
        }

        public async Task<List<Schedule>> GetSchedules(string InstructorNumber)
        {
            var schedules = _dataContext.Schedules
                            .Include(x => x.Instructor).AsQueryable();

            schedules = schedules.Where(x => x.Instructor.InstructorNumber == InstructorNumber).AsQueryable();

            schedules = schedules
                            .Include(x => x.Subject).AsQueryable();
            schedules = schedules.Include(x => x.StudentSubjects);

            
            return await schedules.ToListAsync();
        }

        public async Task<List<Student>> GetStudents(int ScheduleId)
        {
            var schedules = _dataContext.StudentSubjects.Include(x => x.Schedule);

            var Schedule = schedules.Where(x => x.Schedule.ID == ScheduleId).AsQueryable();

            var students = await Schedule.Include(x => x.Student).Select(x => x.Student).ToListAsync();

            return students;
        }
    }
}