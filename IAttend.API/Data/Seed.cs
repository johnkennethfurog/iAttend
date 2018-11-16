using System.Collections.Generic;
using IAttend.API.Models;
using Newtonsoft.Json;

namespace IAttend.API.Data
{
    public class Seed
    {
        private readonly DataContext _context;
        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedStudent()
        {
            var studentData = System.IO.File.ReadAllText("Data/SeedStudent.json");
            var students = JsonConvert.DeserializeObject<List<Student>>(studentData);

            _context.Students.AddRange(students);
            _context.SaveChanges();
        }


        public void SeedContactPerson()
        {
            var contactPersonsData = System.IO.File.ReadAllText("Data/SeedContactPerson.json");
            var contactPersons = JsonConvert.DeserializeObject<List<ContactPerson>>(contactPersonsData);

            _context.ContactPersons.AddRange(contactPersons);
            _context.SaveChanges();
        }

        public void SeedInstructor()
        {
            var instructorsData = System.IO.File.ReadAllText("Data/SeedInstructor.json");
            var instructor = JsonConvert.DeserializeObject<List<Instructor>>(instructorsData);

            _context.Instructors.AddRange(instructor);
            _context.SaveChanges();
        }

        public void SeedSchedule()
        {
            var schedulesData = System.IO.File.ReadAllText("Data/SeedSchedule.json");
            var schedules = JsonConvert.DeserializeObject<List<Schedule>>(schedulesData);

            _context.Schedules.AddRange(schedules);
            _context.SaveChanges();
        }

        public void SeedSubject()
        {
            var subjects  = new List<Subject>();
        }
    }
}