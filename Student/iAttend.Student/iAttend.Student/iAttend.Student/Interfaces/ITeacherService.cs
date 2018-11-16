using iAttend.Student.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iAttend.Student.Interfaces
{
    public interface ITeacherService
    {
        Task<List<TeacherSubject>> GetSubjects(string instructorNumber);
        Task<List<TeacherSubject>> GetSubjects();

        Task<List<TeacherStudentAttendance>> GetStudents(int scheduleId,DateTime date);
        Task<bool> MarkStudentAttendance(string studentNumber, int scheduleId);
        Task<bool> UnmarkStudentAttendance(string studentNumber, int scheduleId);
    }
}
