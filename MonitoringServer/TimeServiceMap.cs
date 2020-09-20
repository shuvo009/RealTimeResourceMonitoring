using System;
using System.Collections.Generic;
using MonitoringServer.MonitoringServices;

namespace MonitoringServer
{
    public class TimeServiceMap
    {
        public DateTime DateTime { get; set; }
        public IMonitoringService MonitoringService { get; set; }
        public HashSet<string> ConnectionIds { get; set; } = new HashSet<string>();
    }
}