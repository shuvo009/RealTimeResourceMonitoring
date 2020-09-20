using System;
using System.Collections.Generic;
using MonitoringServer.MonitoringServices;

namespace MonitoringServer
{
    public interface IMonitoringServiceManager
    {
        void Add(string resource, string connectionId);
        void Remove(string connectionId);
        List<IMonitoringService> GetMonitoringServices(DateTime dateTime);
        void UpdateGetNextOccurrence(string name);
    }
}