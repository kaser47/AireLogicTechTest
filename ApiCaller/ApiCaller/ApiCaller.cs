using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace ApiCaller
{
    public static class ApiCallerConsts{
        public static string userAgent = "ApiCaller/1.0 (kaser47@hotmail.com)";
        public static string artistApiCaller = "https://musicbrainz.org/ws/2/artist?limit=1&query=";
        public static string songTitleApiCaller = "https://beta.musicbrainz.org/ws/2/work/?limit=100&artist=";
        public static string lyricsApiCaller = "https://api.lyrics.ovh/v1/";
    }

    public class ApiCaller
    {
        private string _artist;
        
        public ApiCaller()
        {
        }
        
        public Guid FindArtist(string artistName)
        {
            _artist = HttpUtility.UrlPathEncode(artistName);
            HttpClient client = new HttpClient();
            
            client.BaseAddress = new Uri(ApiCallerConsts.artistApiCaller);

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            Uri requestUri = new Uri($"{ApiCallerConsts.artistApiCaller}" + _artist);
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
            }
            
            client.Dispose();
            
            return artistId;
        }

        public List<string> FindSongTitlesByArtist(Guid artistId)
        {
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

            
                foreach (dynamic work in data.works)
                {
                    songTitles.Add(work.title.ToString());
                }

                i++;
                offset = 100 * i;
            } 
            while (data.works.Count == 100);

            songTitles = songTitles.Distinct().OrderBy(x => x).ToList();
            
            client.Dispose();
            
            return songTitles;
        }

        public string FindLyricsByArtistAndTitle(string title)
        {
            string encodedTitle = HttpUtility.UrlPathEncode(title);
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(ApiCallerConsts.lyricsApiCaller);

//            client.DefaultRequestHeaders.Accept.Add(
//                new MediaTypeWithQualityHeaderValue("application/json"));

            
            dynamic data = null;
            
                Uri requestUri = new Uri($"{ApiCallerConsts.lyricsApiCaller}{_artist}/{encodedTitle}");
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
    }
}
