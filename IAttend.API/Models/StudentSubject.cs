namespace IAttend.API.Models
{
    public class StudentSubject
    {
        public int ID { get; set; }
    
        public Student Student { get; set; }
    
        public Schedule Schedule { get; set; }
    }
}