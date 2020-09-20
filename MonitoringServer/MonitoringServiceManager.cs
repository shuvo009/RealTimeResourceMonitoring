using System;
using System.Collections.Generic;
using System.Linq;
using MonitoringServer.MonitoringServices;
using NCrontab;

namespace MonitoringServer
{
    public class MonitoringServiceManager : IMonitoringServiceManager
    {
        private readonly IEnumerable<IMonitoringService> _monitoringServices;
        private readonly static List<TimeServiceMap> _services = new List<TimeServiceMap>();

        public MonitoringServiceManager(IEnumerable<IMonitoringService> monitoringServices)
        {
            _monitoringServices = monitoringServices;
        }

        public void Add(string resource, string connectionId)
        {
            var service = _services.FirstOrDefault(x => x.MonitoringService.Name.Equals(resource));
            if (service != null)
            {
                service.ConnectionIds.Add(connectionId);
                return;
            }

            var monitoringServices = _monitoringServices.FirstOrDefault(x => x.Name.Equals(resource));
            if (monitoringServices == null)
                return;

            _services.Add(new TimeServiceMap
            {
                MonitoringService = monitoringServices,
                DateTime = GetNextOccurrence(monitoringServices.Schedule),
                ConnectionIds = new HashSet<string> { connectionId }
            });
        }

        public void Remove(string connectionId)
        {
            foreach (var serviceMap in _services.Where(x => x.ConnectionIds.Any(s => s == connectionId)))
                serviceMap.ConnectionIds.Remove(connectionId);

            var services = _services.Where(x => x.ConnectionIds.Count < 1).ToList();

            foreach (var serviceMap in services)
                _services.Remove(serviceMap);
        }

        public List<IMonitoringService> GetMonitoringServices(DateTime dateTime)
        {
            var services =  _services.Where(x => x.DateTime <= dateTime).Select(x => x.MonitoringService).ToList();
            Console.WriteLine("Service Found : "+ services.Count);
            return services;
        }

        public void UpdateGetNextOccurrence(string name)
        {
            var index = _services.FindIndex(x => x.MonitoringService.Name.Equals(name));
            if (index < 0)
                return;

            _services[index].DateTime = GetNextOccurrence(_services[index].MonitoringService.Schedule);
        }

        #region Supported Methods
        private DateTime GetNextOccurrence(string schedule)
        {
            var time = CrontabSchedule.Parse(schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true }).GetNextOccurrence(DateTime.Now);
            Console.WriteLine(time);
            return time;
        }

        #endregion
    }
}