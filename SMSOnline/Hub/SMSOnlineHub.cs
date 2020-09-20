using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SMSOnline.Models;
using System.Threading.Tasks;
using SMSOnline.Helpers;

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
            ConnectedUser.Ids.Add(Context.ConnectionId,IdentityHelper.CurrentUserId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            ConnectedUser.Ids.Remove(Context.ConnectionId);
            return base.OnDisconnected(false);
        }
    }
}