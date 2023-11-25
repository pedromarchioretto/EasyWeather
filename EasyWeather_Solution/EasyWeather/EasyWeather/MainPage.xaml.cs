using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EasyWeather
{
    public partial class MainPage : FlyoutPage
    {
        public MainPage()
        {
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



    }
}
