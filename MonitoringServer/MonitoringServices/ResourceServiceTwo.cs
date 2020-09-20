using System.Net.Http;
using System.Threading.Tasks;

namespace MonitoringServer.MonitoringServices
{
    public class ResourceServiceTwo : IMonitoringService
    {
        public string Name => "ResourceServiceTwo";

        public string Schedule => "*/10 * * * * *";

        public async Task<string> GetUpdate()
        {
            var api = "https://localhost:44300/api/Monitor";
            var response = await new HttpClient().GetStringAsync(api);
            return response;
        }
    }
}