using TwitchUWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;

namespace TwitchUWP
{
    public class TwitchAPIHelper
    {
        private async Task<String> CallTwitchAsync(string url)
        {
            HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.Add("Accept", "application/vnd.twitchtv.v3+json");

            var response = await http.GetAsync(url);

            return await response.Content.ReadAsStringAsync();
        }

        private static async Task<GameWrapper> GetTopGamesAsync()
        {
            string topGamesURL = "https://api.twitch.tv/kraken/games/top?limit=100";

            string jsonMessage = await CallTwitchAsync(topGamesURL);

            var serializer = new DataContractJsonSerializer(typeof(GameWrapper));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonMessage));

            var result = (GameWrapper)serializer.ReadObject(ms);

            return result;
        }
    }
}
