using System.Collections.Generic;
using System.Threading.Tasks;
using IAttend.API.Dtos;
using IAttend.API.Models;

namespace IAttend.API.Data
{
    public interface IScheduleRepository
    {
        Task<List<Pocos.Schedule>> GetSchedules(string roomNumber);
        Task<Subject> GetSubject(string subjectCode);
        Task<List<Subject>> GetSubjects();

        Task<Schedule> AddSchedule(Schedule schedule);
        Task<Schedule> EditSchedule(AddScheduleDto schedule);
        Task<bool> DeleteSchedule(int scheduleId);
        Task<Schedule> GetSchedule(int scheduleId);

    }
}