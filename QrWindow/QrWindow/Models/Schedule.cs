using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace QrWindow.Models
{
    public class Schedule : ViewModelBase
    {
        private Windows.UI.Xaml.Media.SolidColorBrush _backColor = new Windows.UI.Xaml.Media.SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        [JsonIgnore]
        public Windows.UI.Xaml.Media.SolidColorBrush BackColor
        {
            get { return _backColor; }
            set { Set(ref _backColor , value); }
        }



        [JsonProperty("instructor")]
        public string Instructor { get; set; }

        [JsonProperty("avatar")]
        public Uri Avatar { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("subjectCode")]
        public string SubjectCode { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("scheduleID")]
        public int ScheduleID { get; set; }
    }
}
