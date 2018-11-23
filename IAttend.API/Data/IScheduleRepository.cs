using System.Collections.Generic;
using System.Threading.Tasks;
using IAttend.API.Models;

namespace IAttend.API.Data
{
    public interface IScheduleRepository
    {
        Task<List<Pocos.Schedule>> GetSchedules(string roomNumber);
    }
}