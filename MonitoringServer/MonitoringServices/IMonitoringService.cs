using System.Threading.Tasks;

namespace MonitoringServer.MonitoringServices
{
    public interface IMonitoringService
    {
        public string Name { get; }
        public string Schedule { get; }
        Task<string> GetUpdate();
    }
}