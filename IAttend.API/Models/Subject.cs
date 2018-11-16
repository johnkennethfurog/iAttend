using System.ComponentModel.DataAnnotations;

namespace IAttend.API.Models
{
    public class Subject
    {    
        [Key]
        public string Code { get; set; }
    
        public string Name { get; set; }
    }
}