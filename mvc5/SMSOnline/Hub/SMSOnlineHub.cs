using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SMSOnline.Hub
{
    [HubName("SMSOnlineHub")]
    public class SMSOnlineHub : Microsoft.AspNet.SignalR.Hub
    {
        public static void BroadcastData()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SMSOnlineHub>();
            context.Clients.All.refreshSMSOnlineData();
        }
    }
}