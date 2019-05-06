using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IAttend.API.Helpers;
using IAttend.API.Models;
using Microsoft.EntityFrameworkCore;
using model = IAttend.API.Models;

namespace IAttend.API.Data
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly DataContext dataContext;

        public SubjectRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<Subject> AddSubject(Subject subject)
        {
            dataContext.Subjects.Add(subject);
            if(await dataContext.SaveChangesAsync() > 0)
                return subject;
            else
                return null;
        }

        public async Task<bool> DeleteSubject(string subjectCode)
        {
            var subj = await GetSubject(subjectCode);
            if(subj == null)
                return false;

            dataContext.Subjects.Remove(subj);
            return await dataContext.SaveChangesAsync() > 0;
        }

        public async Task<Subject> EditSubject(Subject subject)
        {
            var subj = await GetSubject(subject.Code);
            var subjToUpdate = await GetSubject(subject.SubjId);

            if(subj != null &&  subject.SubjId != subj.SubjId)
                return null;

            subjToUpdate.Code = subject.Code;
            subjToUpdate.Name = subject.Name;

            if(await dataContext.SaveChangesAsync() > 0)
                return subject;
            else
                return null;
        }

        async Task<Subject> GetSubject(int subjectId)
        {
            return await dataContext.Subjects.FirstOrDefaultAsync(subj => subj.SubjId == subjectId);
        }
        public async Task<Subject> GetSubject(string subjectCode)
        {
            return await dataContext.Subjects.FirstOrDefaultAsync(subj => subj.Code == subjectCode);
        }

        public async Task<PagedList<Subject>> GetSubjects(PaginatedParams subjectParams)
        {
            return await PagedList<Subject>.CreatePagedList(dataContext.Subjects,subjectParams.PageNumber,subjectParams.PageSize);
        }
    }
}