namespace SolarDynamicObservatoryWebService
{
    public interface ISolarDynamicWebObservatoryDataScraper
    {
        public List<Product> QueryProducts();
        public Task<List<Product>> QueryProducts(string date);
        public Task<List<Product>> QueryProducts(string date, string utcTime = "", string resolution = "", string wavelength = "");
    }
}
