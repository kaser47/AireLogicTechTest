using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Common;
using Newtonsoft.Json;

namespace ApiCaller
{
    public class ApiCaller
    {
        public event EventHandler<MessageEventArgs> ApiMessageLogged;

        public ApiCaller()
        {
        }

        public Guid FindArtist(string artistName)
        {
            if(string.IsNullOrWhiteSpace(artistName)) return Guid.Empty;
            
            Log($"Starting first api call. Finding Artist '{artistName}'");
            artistName = HttpUtility.UrlPathEncode(artistName);
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(ApiCallerConsts.artistApiCaller);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            Uri requestUri = new Uri($"{ApiCallerConsts.artistApiCaller}" + artistName);
            client.DefaultRequestHeaders.Add("User-Agent", ApiCallerConsts.userAgent);

            dynamic data = null;
            var task = client.GetAsync(requestUri).ContinueWith((taskwithresponse) =>
            {
                var response = taskwithresponse.Result;
                var jsonString = response.Content.ReadAsStringAsync();
                jsonString.Wait();
                data = JsonConvert.DeserializeObject(jsonString.Result);
            });
            task.Wait();

            Guid artistId = Guid.Empty;

            if (data.artists.Count != 0)
            {
                artistId = data.artists[0].id;
                Log($"Found artist '{data.artists[0].name}'");
            }
            
            client.Dispose();
            return artistId;
        }

        public List<string> FindSongTitlesByArtist(Guid artistId)
        {
            if(artistId == Guid.Empty) return null;
            Log($"Starting second api call. Finding Titles by using id = '{artistId}'");
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(ApiCallerConsts.songTitleApiCaller);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            
            dynamic data = null;
            List<string> songTitles = new List<string>();
            int offset = 0;
            int i = 0;

            do
            {
                Uri requestUri = new Uri($"{ApiCallerConsts.songTitleApiCaller}{artistId.ToString()}&offset={offset}");
                client.DefaultRequestHeaders.Add("User-Agent", ApiCallerConsts.userAgent);
                
                var task = client.GetAsync(requestUri).ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    data = JsonConvert.DeserializeObject(jsonString.Result);
                });
                task.Wait();

                if (data.works == null) return null;
                
                foreach (dynamic work in data.works)
                {
                    songTitles.Add(work.title.ToString());
                }

                i++;
                offset = 100 * i;
                Log($"Acquired '{data.works.Count}' new song titles");
            } while (data.works.Count == 100);

            songTitles = songTitles.Distinct().OrderBy(x => x).ToList();
            Log($"Found {songTitles.Count} songs in total (Duplicates have been removed)");
            client.Dispose();
            return songTitles;
        }

        public string FindLyricsByArtistAndTitle(string artist, string title)
        {
            if (string.IsNullOrWhiteSpace(artist) || string.IsNullOrWhiteSpace(title)) return null;
            
            Log($"Started to find lyrics for '{title}'");
            string encodedTitle = HttpUtility.UrlPathEncode(title);
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(ApiCallerConsts.lyricsApiCaller);
            
            dynamic data = null;

            Uri requestUri = new Uri($"{ApiCallerConsts.lyricsApiCaller}{artist}/{encodedTitle}");
            client.DefaultRequestHeaders.Add("User-Agent", ApiCallerConsts.userAgent);


            var task = client.GetAsync(requestUri).ContinueWith((taskwithresponse) =>
            {
                var response = taskwithresponse.Result;
                var jsonString = response.Content.ReadAsStringAsync();
                jsonString.Wait();
                data = JsonConvert.DeserializeObject(jsonString.Result);
            });
            task.Wait();

            string lyrics = data.lyrics;

            return lyrics;
        }

        private void Log(string message)
        {
            OnApiMessageLogged(new MessageEventArgs(message));
        }

        protected virtual void OnApiMessageLogged(MessageEventArgs e)
        {
            ApiMessageLogged?.Invoke(this, e);
        }
    }
}
