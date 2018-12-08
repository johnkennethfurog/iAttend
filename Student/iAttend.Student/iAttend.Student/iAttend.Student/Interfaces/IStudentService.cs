using iAttend.Student.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iAttend.Student.Interfaces
{
    public interface IStudentService
    {
        Task<List<StudentSubject>> GetSubjects(string studentNumber);
        Task<List<StudentAttendance>> GetAttendances(string studentNumber, int scheduleId);
        Task<bool> ScanDocument(PayloadStudentAttendance attenadancQr);
        Task<StudentInfo> GetStudent(string studentNumber);
    }
}
