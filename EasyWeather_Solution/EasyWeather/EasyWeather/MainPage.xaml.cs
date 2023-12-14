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
using System.ComponentModel.Design;
using Xamarin.Essentials;

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

            meuPicker.Items.Clear();
            preencher_picker();
        }

        //metodos para abrir e fechar o flyout
        void ExibirFlyout()
        {
            ((FlyoutPage)Application.Current.MainPage).IsPresented = true;
        }

        void EsconderFlyout()
        {
            ((FlyoutPage)Application.Current.MainPage).IsPresented = false;
        }

        //ao clicar no icone da lupa
        private void OnImageTapped(object sender, EventArgs e)
        {
            ExibirFlyout();
        }




        private HttpClient _client = new HttpClient();

        //metodo quando a barra de pesquisa for confirmada
        public void Pesquisar_Cidade(object sender, EventArgs e)
        {
            var searchBar = (SearchBar)sender;
            string searchText = searchBar.Text.Trim();

            ChavesAPI chaves = new ChavesAPI();

            ObterDadosDaAPI(searchText, chaves.OpenWeatherAPI, chaves.TimeZoneAPI);
        }

        //instanciando as classes
        ParametrosClima parametrosclima = new ParametrosClima();
        WorldtimeAPI worldtime = new WorldtimeAPI();
        Favoritos favoritar = new Favoritos();

        //chamar a api em questao
        async Task ObterDadosDaAPI(string cidade, string ChaveAPI, string ChaveTimeZoneKEY)
        {
            try
            {
                string APIUrl = String.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&lang=pt_br&units=metric", cidade, ChaveAPI);
                HttpResponseMessage response = await _client.GetAsync(APIUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();


                    //deserializando o json e colocando na classe ParametrosClima()
                    parametrosclima = JsonConvert.DeserializeObject<ParametrosClima>(json);
                    parametrosclima.localizacao = String.Format("{0} - {1}", parametrosclima.Name, parametrosclima.Sys.Country);

                    EsconderFlyout();

                    //formatando strings para fazer a chamada para outra API
                    string cidade_formatada = parametrosclima.Name;
                    cidade_formatada = cidade_formatada.Replace(" ", "_");

                    //deserializando o json da segunda API
                    string horario_local = await worldtime.HorarioLocal(ChaveTimeZoneKEY, formatarFloat(parametrosclima.coord.lat), formatarFloat(parametrosclima.coord.lon));
                    var jsonretornado = JsonConvert.DeserializeObject<ParametrosClima>(horario_local);
                    parametrosclima.formatted = jsonretornado.formatted;
                    
                        DateTimeOffset timestamp = DateTimeOffset.Parse(parametrosclima.formatted);
                        TimeSpan horario = timestamp.TimeOfDay;
                        int horarioFormatado = int.Parse(horario.ToString(@"hh"));


                    string descricao = parametrosclima.Weather[0].Description;

                    if ((descricao == "céu limpo" || descricao == "céu claro" || descricao == "algumas nuvens") && (5 <= horarioFormatado && horarioFormatado <= 18))
                    {
                        parametrosclima.ImagemBackground = "Limpo.png";
                    }
                        else if(descricao == "céu limpo" || descricao == "céu claro" && 5 > horarioFormatado || horarioFormatado > 18)
                        {
                            parametrosclima.ImagemBackground = "Noite.png";
                        }
                            else if (descricao == "nublado" || descricao == "chuva leve" || descricao == "tempestade" || descricao == "chuva pesada" || descricao == "chuva moderada" || descricao == "chuva fraca")
                            {
                                parametrosclima.ImagemBackground = "Chuva.png";
                            }
                                else if (descricao == "neve")
                                {
                                    parametrosclima.ImagemBackground = "Neve.png";
                                }
                                    else
                                    {
                                        parametrosclima.ImagemBackground = "Neutral.png";
                                    }

                    f_guia.IsVisible = false;
                    f_Temperatura.IsVisible = true;
                    f_especificos.IsVisible = true;
                    i_estrelavazia.IsVisible = true;

                    if(favoritar.CidadeExiste(parametrosclima.Name) == true)
                    {
                        i_estrelapreenchida.IsVisible = true;
                    }
                    else
                    {
                        i_estrelapreenchida.IsVisible = false;
                    }

                    BindingContext = parametrosclima;

                }
                else
                {
                    excecoes.erros = "Você digitou uma cidade inválida, tente novamente :)";
                    excecoes.ImagemBackground = "Neutral.png";
                    f_Temperatura.IsVisible = false;
                    f_especificos.IsVisible = false;
                    i_estrelavazia.IsVisible = false;
                    i_estrelapreenchida.IsVisible = false;
                    f_guia.IsVisible = true;

                    BindingContext = excecoes;
                }
            }
            catch (Exception ex)
            {
                excecoes.erros = "Parece que aconteceu algum erro :(, tente novamente mais tarde!";
                excecoes.ImagemBackground = "Neutral.png";
                f_Temperatura.IsVisible = false;
                f_especificos.IsVisible = false;
                i_estrelavazia.IsVisible = false;
                i_estrelapreenchida.IsVisible = false;
                f_guia.IsVisible = true;

                BindingContext = excecoes;
            }
        }

        public string formatarFloat(float n)
        {
            float numero = n;

            string numeroString = numero.ToString(System.Globalization.CultureInfo.InvariantCulture);

            return numeroString;

        }


        //metodo para adicionar e remover favoritos do arquivo local
        private void adicionarFavorito(object sender, EventArgs e)
        {
            i_estrelapreenchida.IsVisible = true;
            favoritar.SalvarDadosLocalmente(parametrosclima.Name);

            meuPicker.Items.Clear();
            preencher_picker();
        }

        //metodo para adicionar e remover favoritos do arquivo local
        private void removerFavorito(object sender, EventArgs e)
        {
            i_estrelapreenchida.IsVisible = false;
            favoritar.RemoverCidadeFavorita(parametrosclima.Name);

            meuPicker.Items.Clear();
            preencher_picker();
        }



        //metodo para preencher o picker de cidades favoritas
        public void preencher_picker()
        {
            List<string> cidades_favoritas = favoritar.ListarCidadesFavoritas();

            foreach (string elemento in cidades_favoritas)
            {
                meuPicker.Items.Add(elemento);
            }
        }

        private void ItemAlterado(object sender, EventArgs e)
        {
            ChavesAPI chaves = new ChavesAPI();

            pesquisar.Text = string.Empty;

            var picker = (Picker)sender;
            if (picker.SelectedIndex == -1)
            {
                // Nenhum item está selecionado, então retorne imediatamente
                return;
            }
            var selectedItem = picker.SelectedItem as string;

            ObterDadosDaAPI(selectedItem, chaves.OpenWeatherAPI, chaves.TimeZoneAPI);
        }
    }
}
