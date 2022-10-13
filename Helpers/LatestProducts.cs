//using SolarDynamicObservatoryWebService.Models;
//using System.Text;
//using System.Text.Json;

namespace SolarDynamicObservatoryWebService.Helpers
{
    public class LatestProducts
    {
        private static readonly string baseUrl =
            @"https://sdo.gsfc.nasa.gov/assets/img/latest/latest_";
        private static readonly string[] _resolutions =
            new string[] { "2048", "1024", "512" };
        private static readonly string[] _waveLenghts =
            new string[] { "0094","0131","0171","0193","0211","0304","0335","1600",
            "1700","HMIB","HMII","HMID","HMIBC","HMIIF","HMIIC"
        };

        public LatestProducts() { }
        public List<Product> QueryProducts()
        {
            List<Product> products = new List<Product>();

            foreach (var _resolution in _resolutions)
            {
                foreach (var _waveLenght in _waveLenghts)
                {
                    GetLatest(products, _resolution, _waveLenght);
                }
            }
            return products;
        }

        private static void GetLatest(List<Product> products, string _resolution, string _waveLenght)
        {
            Product product = new Product();
            product.Date = "";
            product.UtcTime = "";
            product.Resolution = _resolution;
            product.Wavelength = _waveLenght;
            product.Url = baseUrl + _resolution + "_" + _waveLenght + ".jpg";
            products.Add(product);
        }
    }
}
