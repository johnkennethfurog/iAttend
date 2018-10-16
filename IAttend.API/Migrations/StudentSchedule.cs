using System.Collections.Generic;
using IAttend.API.Models;

namespace IAttend.API.Migrations
{
    public class StudentSchedule
    {
        public int ID { get; set; }
        public Schedule Schedule { get; set; }
        public ICollection<Student> Students { get; set; }

    }
}