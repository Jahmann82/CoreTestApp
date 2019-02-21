using System.Net.Http;
using System.Threading.Tasks;

namespace AsyncWebApplication.Services
{
    public class ContentService
    {
        public async Task<string> DoCurlAsync()
        {
            using (var httpClient = new HttpClient())
            {
                using (var httpResonse = await httpClient.GetAsync("http://en.wikipedia.org/").ConfigureAwait(false))
                {
                    return await httpResonse.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
            }
        }
    }
}