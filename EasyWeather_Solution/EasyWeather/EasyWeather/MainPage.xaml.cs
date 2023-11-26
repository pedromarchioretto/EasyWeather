using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;

namespace EasyWeather
{
    public partial class MainPage : FlyoutPage
    {
        public ParametrosClima excecoes { get; set; }
        public MainPage()
        { 
            excecoes = new ParametrosClima();
            excecoes.erros = "";
            BindingContext = excecoes;
            
            InitializeComponent();
        }

        void ExibirFlyout()
        {
            ((FlyoutPage)Application.Current.MainPage).IsPresented = true;
        }
        private void OnImageTapped(object sender, EventArgs e)
        {
            ExibirFlyout();
        }


        private HttpClient _client = new HttpClient();

        public void Pesquisar_Cidade(object sender, EventArgs e)
        {
            var searchBar = (SearchBar)sender;
            string searchText = searchBar.Text;

            ChavesAPI chaves = new ChavesAPI();

            ObterDadosDaAPI(searchText, chaves.ChaveAPI);
        }

        async Task ObterDadosDaAPI(string cidade, string ChaveAPI)
        {
            try
            {
                string APIUrl = String.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&lang=pt_br", cidade, ChaveAPI);
                HttpResponseMessage response = await _client.GetAsync(APIUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    // Deserializa o JSON para um objeto
                    ParametrosClima objeto = JsonConvert.DeserializeObject<ParametrosClima>(json);
                    DisplayAlert("Título", objeto.Weather[0].Description, "OK");
                }
                else
                {
                    excecoes.erros = "Você digitou uma cidade inválida, tente novamente :)";
                    OnPropertyChanged(nameof(excecoes.erros));
                }
            }
            catch (Exception ex)
            {
                excecoes.erros = "Parece que aconteceu algum erro :(, tente novamente mais tarde!";
                OnPropertyChanged(nameof(excecoes.erros));
            }
        }
    }
}
