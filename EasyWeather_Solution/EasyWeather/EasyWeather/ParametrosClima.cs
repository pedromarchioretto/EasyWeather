using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EasyWeather
{
    public class ParametrosClima : INotifyPropertyChanged
    {
        public string erros { get; set; }
        public MainData Main { get; set; }
        public WeatherData[] Weather { get; set; }
        public WindData Wind { get; set; }
        public string Name { get; set; }
        public SysData Sys { get; set; }       
        public float Visibility {get; set; }
        public string localizacao {  get; set; }


        public class MainData
        {
            public float Temp { get; set; }
            public float Temp_min { get; set; }
            public float Temp_max { get; set; }
            public int Humidity { get; set; }
        }

        public class WeatherData
        {
            public string Description { get; set; }
            public string Icon { get; set; }
        }

        public class WindData
        {
            private float speed;

            public float Speed
            {
                get { return speed * 3.6f; }
                set { speed = value; }
            }
        }

        public class SysData
        {
            public string Country { get; set; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged()
        {
            foreach (var property in GetType().GetProperties())
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property.Name));
            }
        }

    }

}
