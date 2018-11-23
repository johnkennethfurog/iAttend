using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QrWindow.Models;

namespace QrWindow.Services
{
    class ScheduleService : IScheduleService
    {
        private readonly IApiService _apiService;

        public ScheduleService()
        {
            _apiService = new ApiService
            {
                BaseUri = Util.BASE_URL
            };
        }

        public async Task<List<Schedule>> GetSchedules(string Room)
        {
            var result = await _apiService.GetResopnseAsync<List<Schedule>>($"api/schedule/schedules/{Room}");
            return result.Result;
            
        }
    }
}
