using TwitchUWP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Diagnostics;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace TwitchUWP
{
    public class TwitchAPIHelper
    {
        private async static Task<String> CallTwitchAsync(string url)
        {
            HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.twitchtv.v3+json"));

            var response = await http.GetAsync(url);

            return await response.Content.ReadAsStringAsync();
        }

        private static async Task<RootObject> GetTopGamesAsync()
        {
            string topGamesURL = "https://api.twitch.tv/kraken/games/top?limit=100";

            string jsonMessage = await CallTwitchAsync(topGamesURL);

            RootObject result = JsonConvert.DeserializeObject<RootObject>(jsonMessage);

            return result;
        }

        public static async Task PopulateTwitchTopGamesAsync(ObservableCollection<Game> topGames)
        {
            try
            {
                var GameWrapper = await GetTopGamesAsync();

                var games = GameWrapper.top;

                foreach (var game in games)
                {
                    topGames.Add(game.game);
                }

            } catch (Exception)
            {
                return;
            }
        }
    }
}
