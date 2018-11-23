using QrWindow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrWindow.Services
{
    public interface IScheduleService
    {
        Task<List<Schedule>> GetSchedules(string Room);
    }
}
