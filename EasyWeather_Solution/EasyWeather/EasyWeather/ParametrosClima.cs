using System;
using System.Collections.Generic;
using System.Text;

namespace EasyWeather
{
    public class ParametrosClima
    {
        public string erros {  get; set; }
        public MainData Main { get; set; }
        public WeatherData[] Weather { get; set; }
        public WindData Wind { get; set; }
        public string Name { get; set; }
        public SysData Sys { get; set; }
        public float Visibility { get; set; }

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
            public float Speed { get; set; }
        }

        public class SysData
        {
            public string Country { get; set; }
        }
    }

}
