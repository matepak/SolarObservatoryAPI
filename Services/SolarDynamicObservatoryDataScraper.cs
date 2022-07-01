using HtmlAgilityPack;
using SolarDynamicObservatoryWebService;
using SolarDynamicObservatoryWebService.Helpers;

public class SolarDynamicObservatoryDataScraper : ISolarDynamicWebObservatoryDataScraper
{
    public string UrlBase { get; private set; }
    public List<Product> Products { get; private set; }

    public SolarDynamicObservatoryDataScraper(string urlBase)
    {
        UrlBase = urlBase;
        //Products = new List<Product>();
    }
    public List<Product> QueryProducts()
    {
        return new LatestProducts().QueryProducts();
    }

    public async Task<List<Product>> QueryProducts(
          string date
        , string utcTime = ""
        , string resolution = ""
        , string waveLength = ""
        ) 
    {
        await GetAllProductsAsync(UrlBase, UrlSubDirectory(date), "//a");

        return Products.Where(
            p => p.UtcTime.Contains(utcTime) &&
            p.Resolution.Contains(resolution) &&
            p.Wavelength == (waveLength)
        ).ToList();
    }

    public async Task<List<Product>> QueryProducts(string date)
    {
        await GetAllProductsAsync(UrlBase, UrlSubDirectory(date), "//a");
        return Products.ToList();
    }
    private async Task<string> GetPageAsync(string url)
    {
        using HttpClient? client = new HttpClient();
        return await client.GetStringAsync(url);
    }

    private async Task GetAllProductsAsync(string urlBase, string urlSubDirectory, string htmlNode)
    {
        Products = new List<Product>();
        HtmlDocument document = new HtmlDocument();
        document.LoadHtml(await GetPageAsync(urlBase + urlSubDirectory));
        var htmlNodes = document.DocumentNode.SelectNodes(htmlNode);
        foreach (var node in htmlNodes)
        {
            if (char.IsLetter(node.InnerText[0])) continue;
            Products.Add(Deserialize(node.InnerText));
        }
    }

    private Product Deserialize(string product, string delimiter = "_")
    {
        Product Product = new Product();
        string[] prod = product.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
        int fileExtension = prod[3].Length - 4;
        Product.Date = prod[0];
        Product.UtcTime = prod[1];
        Product.Resolution = prod[2];
        Product.Wavelength = prod[3].Remove(fileExtension);
        Product.Url = UrlBase + UrlSubDirectory(prod[0]) + product;
        return Product;
    }

    private string UrlSubDirectory(string date)
    {
        string year = date.Substring(0, 4);
        string month = date.Substring(4, 2);
        string day = date.Substring(6, 2);
        return year + "/" + month + "/" + day + "/";
    }

}