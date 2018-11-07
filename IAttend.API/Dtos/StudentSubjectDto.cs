namespace IAttend.API.Dtos
{
    public class StudentSubjectDto
    {
        public TeacherDto Instructor { get; set; }
        public SubjectDto Subject { get; set; }   
    }
}