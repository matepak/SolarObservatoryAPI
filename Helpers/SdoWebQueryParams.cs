//using SolarDynamicObservatoryWebService.Models;
//using System.Text;
//using System.Text.Json;

namespace SolarDynamicObservatoryWebService.Helpers
{
    static class SdoWebQueryParams
    {
        private static string _baseUrl =
            @"https://sdo.gsfc.nasa.gov/assets/img/latest/latest_";
        private static string[] _resolutions =
            new string[] { "4096", "2048", "1024", "512" };
        private static string[] _waveLenghts =
            new string[] { "0094","0131","0171","0193","0211","0304","0335","1600",
            "1700","HMIB","HMII","HMID","HMIBC","HMIIF","HMIIC"
        };

        public static string GetBaseUrl => _baseUrl;
        public static string[] GetResolutions => _resolutions;
        public static string[] GetWaveLenghts => _waveLenghts;
    }
}