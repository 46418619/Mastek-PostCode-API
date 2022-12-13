using Mastek_PostCode_API.Interface;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Mastek_PostCode_API.Model;

namespace Mastek_PostCode_API.Service
{
    public class PostCodeService : IPostCodeService
    {
        IConfiguration _configuration;
        public PostCodeService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<PostCodeDetailViewModel> GetPostCodeDetailAsync(string pC)
        {
            //Making API Call...
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://postcodes.io/");
                var response = await client.GetAsync("postcodes/" + pC);
                if (response.IsSuccessStatusCode)
                {
                    dynamic res = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                    var result = new PostCodeDetailViewModel();
                    result.PostCode = pC;
                    result.Country = res.result["country"];
                    result.Region = res.result["region"];
                    result.AdminDistrict = res.result["admin_district"];
                    result.ParliamentaryConstituency = res.result["parliamentary_constituency"];
                    string lat= res.result["latitude"];
                    result.Area = GetArea(double.Parse(lat));
                    return result;
                }
                else
                    return null;
            }
        }
        private string GetArea(double latitude)
        {
            if (latitude < 52.229466)
                return "South";
            else if (latitude >= 52.229466 && latitude < 53.27169)
                return "Midlands";
            else if (latitude >= 53.27169)
                return "North";
            else
                return "N/A";
        }
        public async Task<List<string>> GetAutoCompleteAsync(string pC)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://postcodes.io/");
                int maxCount = int.Parse(_configuration.GetSection("Data").GetSection("MaxCount").Value);
                var response = await client.GetAsync("postcodes/" + pC + "/autocomplete");
                if (response.IsSuccessStatusCode)
                {
                    dynamic res = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                    var postCodes = new List<string>();
                    foreach (var item in res.result)
                    {
                        postCodes.Add((string)item);
                    }
                    return postCodes.Take(maxCount).ToList();
                }
                else
                    return null;
            }
        }
    }
}
