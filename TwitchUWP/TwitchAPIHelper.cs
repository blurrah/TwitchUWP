using TwitchUWP.Models;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net;

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

        private static async Task<Streamers.RootObject> GetStreamersAsync(string gameTitle)
        {
            string gameString = WebUtility.UrlEncode(gameTitle);
            string streamersURL = String.Format("https://api.twitch.tv/kraken/streams?game={0}&limit=20", gameString);

            string jsonMessage = await CallTwitchAsync(streamersURL);

            Streamers.RootObject result = JsonConvert.DeserializeObject<Streamers.RootObject>(jsonMessage);

            return result;
        }

        public static async Task PopulateTwitchStreamersAsync(string gameName, ObservableCollection<Streamers.Stream> streams)
        {
            try
            {
                var StreamWrapper = await GetStreamersAsync(gameName);
                var streamers = StreamWrapper.streams;

                foreach (var stream in streamers)
                {
                    streams.Add(stream);
                }
            } catch (Exception)
            {
                return;
            }
        }
    }
}
