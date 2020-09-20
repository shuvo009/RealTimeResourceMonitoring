using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

namespace MonitoringServer
{
    public class BackgroundWorker : BackgroundService
    {
        private readonly IMonitoringServiceManager _monitoringServiceManager;
        private readonly IHubContext<MonitoringHub> _hubContext;

        public BackgroundWorker(IMonitoringServiceManager monitoringServiceManager, IHubContext<MonitoringHub> hubContext)
        {
            _monitoringServiceManager = monitoringServiceManager;
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    Console.WriteLine("Req:" + DateTime.Now);
                    var services = _monitoringServiceManager.GetMonitoringServices(DateTime.Now).Select(x => Task.Run(async () =>
                    {
                        var data = await x.GetUpdate();
                        await _hubContext.Clients.Group(x.Name).SendAsync(x.Name.ToLower(), new { payload = data });
                        _monitoringServiceManager.UpdateGetNextOccurrence(x.Name);
                    })).ToList();

                    await Task.WhenAll(services);

                    await Task.Delay(TimeSpan.FromSeconds(5));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
