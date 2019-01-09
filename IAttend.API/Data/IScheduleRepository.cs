using System.Collections.Generic;
using System.Threading.Tasks;
using IAttend.API.Models;

namespace IAttend.API.Data
{
    public interface IScheduleRepository
    {
        Task<List<Pocos.Schedule>> GetSchedules(string roomNumber);
        Task<Schedule> AddSchedule(Schedule schedule);
        Task<Subject> GetSubject(string subjectCode);
    }
}