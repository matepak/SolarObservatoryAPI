//using SolarDynamicObservatoryWebService.Models;
//using System.Text;
//using System.Text.Json;

namespace SolarDynamicObservatoryWebService.Helpers
{
    public class LatestProducts
    {
        public LatestProducts() { }
        public List<Product> QueryProducts()
        {
            List<Product> products = new List<Product>();

            foreach (var _resolution in SdoWebQueryParams.GetResolutions)
            {
                foreach (var _waveLenght in SdoWebQueryParams.GetWaveLenghts)
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
            product.Url = SdoWebQueryParams.GetBaseUrl + _resolution + "_" + _waveLenght + ".jpg";
            products.Add(product);
        }
    }
}
