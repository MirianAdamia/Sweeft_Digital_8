using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sweeft_Digital_8.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sweeft_Digital_8.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("DownloadCountries")]       
        public async Task<IActionResult> DownloadCountries()
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                var httpRequest = new HttpRequestMessage(HttpMethod.Get, "https://restcountries.com/v3.1/all");

                using (var response = await client.SendAsync(httpRequest))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    IEnumerable<Country> countries
                        = JsonConvert.DeserializeObject<IEnumerable<Country>>(apiResponse);

                    var lines = new List<string>();

                    foreach(var country in countries)
                    {
                        var line = $"Region - {country.Region}, Subregion - {country.Subregion}, " +
                            $"Population - {country.Population}, Area - {country.Area}, Latlng - ";

                        foreach (var lat in country.Latlng)
                            line += $" {lat}";

                        lines.Add(line);
                    }

                    await System.IO.File.WriteAllLinesAsync("Countries.txt", lines);

                    return Ok(countries);
                }
            }
        }
       

    }
}
