using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace MonitoringServer
{
    public class MonitoringHub : Hub
    {
        private readonly IMonitoringServiceManager _monitoringServiceManager;

        public MonitoringHub(IMonitoringServiceManager monitoringServiceManager)
        {
            _monitoringServiceManager = monitoringServiceManager;
        }

        public async Task Subscribe(string resource)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, resource);
            _monitoringServiceManager.Add(resource, Context.ConnectionId);
        }

        public async Task Unsubscribe(string resource)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, resource);
            _monitoringServiceManager.Remove(Context.ConnectionId);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _monitoringServiceManager.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);

        }
    }
}