using System.Collections.Generic;
using System.Threading.Tasks;
using IAttend.API.Models;

namespace IAttend.API.Data
{
    public interface ISubjectRepository
    {
         Task<Subject> GetSubject(int SubjectID);

         Task<Subject> GetSubject(string SubjectCode);

         Task<List<Subject>> GetSubjects();
    }
}