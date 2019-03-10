using iAttend.Student.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace iAttend.Student.EventAggs
{
    public class AttendanceStartedEvent : PubSubEvent<AttendanceStartedEventArg> { }
    public class AttendanceStartedEventArg
    {
        public int ScheduleId { get; set; }
        public bool IsActive { get; set; }
    }

    public class ScheduleAddedEvent : PubSubEvent<TeacherSubject> { }
}
