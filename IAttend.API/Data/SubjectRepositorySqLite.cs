using System.Collections.Generic;
using System.Threading.Tasks;
using IAttend.API.Models;
using Microsoft.EntityFrameworkCore;

namespace IAttend.API.Data
{
    public class SubjectRepositorySqLite : ISubjectRepository
    {
        private readonly DataContext _dataContext;

        public SubjectRepositorySqLite(DataContext dataContext )
        {
            _dataContext = dataContext;
        }
        public async Task<Subject> GetSubject(int SubjectID)
        {
            return await _dataContext.Subjects.FirstOrDefaultAsync(subj => subj.ID == SubjectID);
        }

        public async Task<Subject> GetSubject(string SubjectCode)
        {
            return await _dataContext.Subjects.FirstOrDefaultAsync(subj => subj.Code == SubjectCode);
        }

        public async Task<List<Subject>> GetSubjects()
        {
            return await _dataContext.Subjects.ToListAsync();
        }
    }
}