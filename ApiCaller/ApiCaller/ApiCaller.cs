using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ApiCaller
{
    public static class ApiCallerConsts{
        public static string userAgent = "ApiCaller/1.0 (kaser47@hotmail.com)";
        public static string artistApiCaller = "https://musicbrainz.org/ws/2/artist?query=";
    }

    public class ApiCaller
    {
        public ApiCaller()
        {

        }
    
        public void FindArtist(string artistName)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ApiCallerConsts.artistApiCaller);

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            Uri requestUri = new Uri($"{ApiCallerConsts.artistApiCaller}" + artistName);
            client.DefaultRequestHeaders.Add("User-Agent", ApiCallerConsts.userAgent);

            ArtistResponse model= new ArtistResponse();
            // List data response.
            var task = client.GetAsync(requestUri).ContinueWith((taskwithresponse) =>
            {
                var response = taskwithresponse.Result;
                var jsonString = response.Content.ReadAsStringAsync();
                jsonString.Wait();
                model = JsonConvert.DeserializeObject<ArtistResponse>(jsonString.Result);

            });
            task.Wait();
            
            //if (response.IsSuccessStatusCode)
            //{
            //    // Parse the response body.
            //    var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
            //    foreach (var d in dataObjects)
            //    {
            //        Console.WriteLine("{0}", d.Name);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            //}

            //Make any other calls using HttpClient here.

            //Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();
        }
    }
}
