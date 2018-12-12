using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace IAttend.API.Models
{
    public class Student
    {
        [Key]
        public string StudentNumber { get; set; }
    
        public string StudentName { get; set; }
    
        public string Avatar { get; set; }
    
        public ContactPerson ContactPerson { get; set; }

        public ICollection<StudentSubject> StudentSubjects { get; set; }

        public bool IsDropped { get; set; }

        public Student()
        {
            StudentSubjects = new Collection<StudentSubject>();
        }
    }
}