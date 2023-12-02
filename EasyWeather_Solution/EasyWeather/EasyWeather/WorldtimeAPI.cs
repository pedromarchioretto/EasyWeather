using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EasyWeather
{
    public class WorldtimeAPI
    {
        private HttpClient _client = new HttpClient();

        public async Task<string> HorarioLocal(string API_KEY, string latitude, string longitude)
        {
            try
            {
                string APIUrl = $"https://api.timezonedb.com/v2.1/get-time-zone?key={API_KEY}&format=json&by=position&lat={latitude}&lng={longitude}";
                HttpResponseMessage response = await _client.GetAsync(APIUrl);
                if (response.IsSuccessStatusCode)
                {
                    string js = await response.Content.ReadAsStringAsync();
                    return js;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

}

