namespace IAttend.API.Dtos
{
    public class StudentDto
    {
        public string StudentNumber { get; set; }
    
        public string StudentName { get; set; }
    
        public string Avatar { get; set; }

        public bool? IsScanned { get; set; }

        public bool IsPresent { get; set; }

        public bool IsDropped { get; set; }
    }
}