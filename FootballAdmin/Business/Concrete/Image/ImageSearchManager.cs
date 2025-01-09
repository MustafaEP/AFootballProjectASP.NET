using Business.Abstract.Image;
using System.Net.Http;
using System.Threading.Tasks;
using Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices.JavaScript;
using Newtonsoft.Json.Linq;

namespace Business.Concrete.Image
{
    public class ImageSearchManager : IImageSearchService
    {
        private readonly string _apiKey = "Your APIKEY"; //GOOGLE API KEY
        private readonly string _cx = "Your CSE_ID"; //CSE_ID

        public async Task<string> GetPlayerImageAsync(string playerName)
        {
            string query = playerName;
            string apiUrl = $"https://www.googleapis.com/customsearch/v1?q={query}&cx={_cx}&searchType=image&key={_apiKey}";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(apiUrl);
                var json = JObject.Parse(response);
                var imageUrl = json["items"]?[0]?["link"]?.ToString();
                return imageUrl ?? "Resim bulunamadı";
            }
        }
    }
}
