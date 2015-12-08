using TwitchUWP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace TwitchUWP
{
    public class TwitchHLSHelper
    {
        public async Task LoadTwitchStream(string channelName, ObservableCollection<LiveStream> livestream) {
            try
            {
                AccessToken.RootObject token = await GetTokenAsync(channelName);


            }
        }

        private async Task<AccessToken.RootObject> GetTokenAsync(string channelName)
        {
            string accessTokenURL = String.Format("https://api.twitch.tv/api/channels/{0}/access_token", channelName);

            string jsonMessage = await CallTwitchAsync(accessTokenURL);

            AccessToken.RootObject result = JsonConvert.DeserializeObject<AccessToken.RootObject>(jsonMessage);

            return result;
        }

        private async static Task<String> CallTwitchAsync(string url)
        {
            HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.twitchtv.v3+json"));

            var response = await http.GetAsync(url);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
