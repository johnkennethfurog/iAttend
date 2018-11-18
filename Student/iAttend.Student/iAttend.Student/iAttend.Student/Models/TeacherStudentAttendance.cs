using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace iAttend.Student.Models
{
    public class TeacherStudentAttendance : BindableBase
    {
        [JsonProperty("studentNumber")]
        public string StudentNumber { get; set; }

        [JsonProperty("studentName")]
        public string StudentName { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        private bool _isPresent;

        [JsonProperty("isPresent")]
        public bool IsPresent
        {
            get { return _isPresent; }
            set { SetProperty(ref _isPresent ,value); }
        }

    }
}
