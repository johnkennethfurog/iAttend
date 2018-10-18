using System.Collections.Generic;
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

        public async Task<Instructor> GetSubject(int InstructorId)
        {
            return await _dataContext.Instructors.FirstOrDefaultAsync(inst => inst.ID == InstructorId);
        }

        public async Task<Instructor> GetSubject(string InstructorNumber)
        {
            return await _dataContext.Instructors.FirstOrDefaultAsync();
        }
    }
}