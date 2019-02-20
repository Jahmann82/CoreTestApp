using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
//        [HttpGet]
//        public async Task<string> Get()
//        {
//            var stringAsync = await GetStringAsync();
//
//            return stringAsync;
//        }
        
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            var stringAsync = GetStringAsync().Result;

            return stringAsync;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        
        private static async Task<string> GetStringAsync()
        {
            using (System.Net.Http.HttpClient c = new HttpClient())
            {
                //await Task.Delay(3000);
                var result = await c.GetStringAsync(new Uri("http://jahmannapi.azurewebsites.net/standings")).ConfigureAwait(false);
                
                return result;
            }
        }
        
        private static async Task<string> GetString2Async()
        {
            using (System.Net.Http.HttpClient c = new HttpClient())
            {
                await Task.Delay(3000);
                var result = await c.GetStringAsync(new Uri("http://jahmannapi.azurewebsites.net/contacts"));

                return result;
            }
        }
    }
}