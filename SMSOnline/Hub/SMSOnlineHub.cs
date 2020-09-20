using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SMSOnline.Models;

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
        public override Task OnConnected()
        {
            ConnectedUser.Ids.Add(Context.ConnectionId);
            return base.OnConnected();
        }
        public Task OnDisconnected()
        {
            ConnectedUser.Ids.Remove(Context.ConnectionId);
            return base.OnDisconnected(false);
        }
    }
}