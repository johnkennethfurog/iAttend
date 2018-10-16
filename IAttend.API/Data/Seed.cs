namespace IAttend.API.Data
{
    public class Seed
    {
        private readonly DataContext _context;
        public class Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedStudent()
        {
            var studentData = System.IO.File.ReadAllText("Data/StudentSeed.json");
            var students = JsonConvert.DeserializeObject<List<Student>>(studentData);

        
        }
    }
}