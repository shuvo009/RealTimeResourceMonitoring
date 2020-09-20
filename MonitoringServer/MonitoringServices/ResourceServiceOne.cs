using System.Net.Http;
using System.Threading.Tasks;

namespace MonitoringServer.MonitoringServices
{
    public class ResourceServiceOne : IMonitoringService
    {
        public string Name => "ResourceServiceOne";

        public string Schedule => "*/5 * * * * *";

        public async Task<string> GetUpdate()
        {
            var api = "https://localhost:44323/api/Monitor";
            var response = await new HttpClient().GetStringAsync(api);
            return response;
        }
    }
}
