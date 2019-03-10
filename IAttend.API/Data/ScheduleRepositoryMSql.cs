using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IAttend.API.Models;
using IAttend.API.Pocos;
using Microsoft.EntityFrameworkCore;

namespace IAttend.API.Data
{
    public class ScheduleRepositoryMSql : IScheduleRepository
    {

        private readonly DataContext _dataContext;

        public ScheduleRepositoryMSql(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Models.Schedule> AddSchedule(Models.Schedule schedule)
        {
            _dataContext.Schedules.Add(schedule);
            await _dataContext.SaveChangesAsync();

            return schedule;
        }

        public async Task<bool> DeleteSchedule(int scheduleId)
        {
            var sched = await GetSchedule(scheduleId);
            _dataContext.Schedules.Remove(sched);

            return await _dataContext.SaveChangesAsync() > 0;

        }

        public async Task<Models.Schedule> EditSchedule(Dtos.AddScheduleDto schedule)
        {
            var sched = await GetSchedule(schedule.Id);
            
            sched.Room = schedule.Room;
            sched.TimeFrom = schedule.TimeFrom;
            sched.TimeTo = schedule.TimeTo;
            sched.DayOfWeek = schedule.DayOfWeek;

            if(sched.Subject.Code != schedule.SubjectCode)
            {
                var subj = await GetSubject(schedule.SubjectCode);
                sched.Subject = subj;
            }

            await _dataContext.SaveChangesAsync();

            return sched;
        }

        public async Task<Models.Schedule> GetSchedule(int scheduleId)
        {
            return await _dataContext.Schedules.FirstOrDefaultAsync(x => x.ID == scheduleId);
        }

        public async Task<List<Pocos.Schedule>> GetSchedules(string roomNumber)
        {
            var dayToday = (int)DateTime.Now.DayOfWeek;
            return await _dataContext.RoomSchedulesView.Where(x => x.Room == roomNumber && x.DayOfWeek == dayToday).ToListAsync();
        }

        public async Task<Subject> GetSubject(string subjectCode)
        {
            return await _dataContext.Subjects.FirstOrDefaultAsync(x => x.Code == subjectCode);
        }

        public async Task<List<Subject>> GetSubjects()
        {
            return await _dataContext.Subjects.ToListAsync();
        }
    }
}
