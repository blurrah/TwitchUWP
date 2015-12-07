using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TwitchUWP
{
    public class TwitchAPIHelper
    {
        private async Task<String> CallTwitchAsync(string url)
        {
            HttpClient http = new HttpClient();
            var response = await http.GetAsync(url);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
