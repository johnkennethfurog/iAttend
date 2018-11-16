using System.ComponentModel.DataAnnotations;

namespace IAttend.API.Models
{
    public class Instructor
    {
        public string Name { get; set; }
    
        public string Avatar { get; set; }
    
        public string EmailAddress { get; set; }

        [Key]
        public string InstructorNumber { get; set; }
    }
}