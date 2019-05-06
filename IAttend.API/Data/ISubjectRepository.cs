using IAttend.API.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using IAttend.API.Helpers;

namespace IAttend.API.Data
{
    public interface ISubjectRepository
    {
        Task<Subject> AddSubject(Subject subject);
        Task<Subject> EditSubject(Subject subject);
        Task<bool> DeleteSubject(string subjectCode);
        Task<Subject> GetSubject(string subjectCode);   
        Task<PagedList<Subject>> GetSubjects(PaginatedParams subjectParams);
    }
}