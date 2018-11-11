using System;
using System.Collections.Generic;
using System.Text;

namespace iAttend.Student.DependencyServices
{
    public interface IMessageService
    {
        void ShowMessage(string message,bool durationIsLong = false);
    }
}
