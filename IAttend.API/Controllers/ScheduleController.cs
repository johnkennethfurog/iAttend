using AutoMapper;
using IAttend.API.Data;
using IAttend.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAttend.API.Controllers
{
    [Route("api/[controller]")]
    public class ScheduleController : Controller
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IMapper _mapper;

        public ScheduleController(IScheduleRepository scheduleRepository,
            IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
        }

        [HttpGet("schedules/{Room}")]
        public async Task<IActionResult> GetSchedules(string Room)
        {
            var schedules = await _scheduleRepository.GetSchedules(Room);
            var scheduleDto = _mapper.Map<List<ScheduleDto>>(schedules);


            return Ok(scheduleDto);
        }

        [HttpGet("subjects")]
        public async Task<IActionResult> GetSubjects()
        {
            var subjects = await _scheduleRepository.GetSubjects();
            return Ok(subjects);
        }
        
    }
}
