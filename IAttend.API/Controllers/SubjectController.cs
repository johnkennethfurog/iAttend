using System;
using System.Threading.Tasks;
using IAttend.API.Data;
using IAttend.API.Dtos;
using IAttend.API.Helpers;
using IAttend.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace IAttend.API.Controllers
{
    [Route("api/[controller]")]
    public class SubjectController : Controller
    {
        private readonly ISubjectRepository subjectRepository;

        public SubjectController(ISubjectRepository subjectRepository)
        {
            this.subjectRepository = subjectRepository;
        }

        [HttpGet("subjects")]
        public async Task<IActionResult> GetSubjects(SubjectParams subjectParams)
        {
            var subjects = await subjectRepository.GetSubjects(subjectParams);

            // Response.AddPagination(subjects.CurrentPage,subjects.PageSize,subjects.TotalCount,subjects.TotalPages);

            return Ok(new PaginatedDto<Subject>
            {
                Items = subjects,
                CurrentPage = subjects.CurrentPage,
                PageSize = subjects.PageSize,
                TotalCount = subjects.TotalCount,
                TotalPages = subjects.TotalPages
            });
        }

            [HttpPost]
        public async Task<IActionResult> AddSubject([FromBody]Subject subj)
        {
            try
            {
                var subject = await subjectRepository.AddSubject(subj);
                if(subject != null)
                    return Ok(subject);
                else
                    return BadRequest("Something went wrong");
            }
            catch(InvalidOperationException invalidEx)
            {
                return BadRequest(invalidEx.Message);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditSubject([FromBody]Subject subj)
        {
            try
            {
                var sched = await subjectRepository.EditSubject(subj);
                if(sched != null)
                    return Ok(sched);
                else
                    return BadRequest("Something went wrong");
            }
            catch(InvalidOperationException invalidEx)
            {
                return BadRequest(invalidEx.Message);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{subjCode}")]
        public async Task<IActionResult> DeleteScehedule(string subjCode)
        {
            var deleted = await subjectRepository.DeleteSubject(subjCode);
            return Ok(deleted);
        } 
    }
}