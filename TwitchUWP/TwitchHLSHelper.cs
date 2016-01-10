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
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace TwitchUWP
{
    public class TwitchHLSHelper
    {
        public static async Task LoadTwitchStream(string channelName, LiveStream livestream, string quality) {
            try
            {
                AccessToken.RootObject accessToken = await GetTokenAsync(channelName);
                Random random = new Random();

                var randomInt = random.Next(10000000, 99999999);

                String token = accessToken.token;
                String sig = accessToken.sig;

                String resultje = await GetStreamListAsync(channelName, token, sig, randomInt, quality);

                livestream.sourceStream = resultje;

                Debug.WriteLine(resultje);
            }
            catch (Exception) { }
        }

        private static async Task<AccessToken.RootObject> GetTokenAsync(string channelName)
        {
            string accessTokenURL = String.Format("https://api.twitch.tv/api/channels/{0}/access_token", channelName);

            string jsonMessage = await CallTwitchAsync(accessTokenURL);

            AccessToken.RootObject result = JsonConvert.DeserializeObject<AccessToken.RootObject>(jsonMessage);

            Debug.WriteLine(result);

            return result;
        }

        /// <summary>
        /// Gets a list of HLS streams based on chosen streamer
        /// </summary>
        /// <param name="channelName">Name of channel</param>
        /// <param name="token">Channel access token</param>
        /// <param name="sig">Channel signature</param>
        /// <param name="random">Random integer payload</param>
        /// <returns>M3u8 file containing the streams</returns>
        private static async Task<String> GetStreamListAsync(string channelName, string token, string sig, int random, string quality)
        {
            string streamListURL = String.Format("http://usher.twitch.tv/api/channel/hls/{0}.m3u8?player=twitchweb&token={1}&sig={2}&allow_audio_only=true&allow_source=true&type=any&p={3}", channelName, token, sig, random);

            String realResult = await ParseM3UAsync(streamListURL, quality);

            return realResult;
        }

        private static async Task<String> CallTwitchAsync(string url)
        {
            HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.twitchtv.v3+json"));

            var response = await http.GetAsync(url);

            return await response.Content.ReadAsStringAsync();
        }

        private async static Task<String> ParseM3UAsync(string url, string quality)
        {
            HttpClient http = new HttpClient();

            var data = await http.GetStringAsync(url);

            var lines = data.Split('\n');

            if (lines.Any()) {
                if(lines[0] != "#EXTM3U")
                {
                    return "null";
                }

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains("#EXT-X-STREAM") && lines[i].Contains("VIDEO=\"" + quality + "\""))
                    {
                        return lines[i + 1];
                    }
                }
            }

            return "null";
        }
    }
}
