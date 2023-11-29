using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EasyWeather
{
    public class GoogleApi
    {
        public async Task<string> HorarioLocal(float latitude, float longitude)
        {
            string Latitude = latitude.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string Longitude = longitude.ToString(System.Globalization.CultureInfo.InvariantCulture);

            ChavesAPI chavesAPI = new ChavesAPI();
            string API_KEY = chavesAPI.GoogleAPI;

            string url = $"https://maps.googleapis.com/maps/api/timezone/json?location={Latitude},{Longitude}&timestamp={ObterTimestampAtual()}&key={API_KEY}";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var conteudo = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(conteudo);
                    return conteudo;
                }
                else
                {
                    Console.WriteLine("Falha ao obter o horário do local.");
                    return null; 
                }
            }
        }

        static int ObterTimestampAtual()
        {
            TimeSpan tempoUnix = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return (int)tempoUnix.TotalSeconds;
        }

    }
}
