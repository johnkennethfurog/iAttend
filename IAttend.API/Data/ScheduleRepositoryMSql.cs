using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<List<Pocos.Schedule>> GetSchedules(string roomNumber)
        {
            var dayToday = (int)DateTime.Now.DayOfWeek;
            return await _dataContext.RoomSchedulesView.Where(x => x.Room == roomNumber && x.DayOfWeek == dayToday).ToListAsync();
        }
    }
}
