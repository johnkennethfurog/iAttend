using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IAttend.API.Models
{
    public class Student
    {
        public int ID { get; set; }

        public string StudentNumber { get; set; }
    
        public string StudentName { get; set; }
    
        public string Avatar { get; set; }
    
        public ContactPerson ContactPerson { get; set; }

        public ICollection<StudentSubject> StudentSubjects { get; set; }

        public Student()
        {
            StudentSubjects = new Collection<StudentSubject>();
        }
    }
}