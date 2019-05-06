using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IAttend.API.Models
{
    public class Subject
    {    
        [Key]
        public string Code { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubjId { get; set; }
        public string Name { get; set; }
    }
}