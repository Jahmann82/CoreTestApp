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
                using (var httpResonse = await httpClient.GetAsync("http://en.wikipedia.org/"))
                {
                    return await httpResonse.Content.ReadAsStringAsync();
                }
            }
        }
    }
}