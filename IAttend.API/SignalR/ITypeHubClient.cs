using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAttend.API.SignalR
{
    public interface ITypeHubClient
    {
        Task BroadcastMessage(string room, string students,int attendanceSessionId,int scheduleId,string guid);

        Task StopBroadcasting(string room);
    }
}
