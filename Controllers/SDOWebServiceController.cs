using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;
using System.Text.Json;

namespace SolarDynamicObservatoryWebService.Controllers;

[ApiController]
[Route("/v1")]
public class SolarDynamicObservatoryWebServiceController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly ISolarDynamicWebObservatoryDataScraper _dataScraper;

    public SolarDynamicObservatoryWebServiceController(
        ILogger<SolarDynamicObservatoryWebServiceController> logger,
        ISolarDynamicWebObservatoryDataScraper dataScrapper
        )
    {
        _logger = logger;
        _dataScraper = dataScrapper;
    }
    ///<summary>
    /// Gets latest SDO data
    /// </summary>
    /// <returns>
    ///
    /// </returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /api/latest
    ///
    ///     {
    ///         "date": "",
    ///         "utcTime": "",
    ///         "resolution": "2048",
    ///         "wavelength": "0094",
    ///         "url": "https://sdo.gsfc.nasa.gov/assets/img/latest/latest_2048_0094.jpg"
    ///     }
    ///     ...
    ///       
    /// </remarks>
    [HttpGet("latest")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 900)]
    public ActionResult<List<Product>> GetLatest()
    {
        return _dataScraper.QueryProducts();
    }

    ///<summary>
    /// Query products by date
    /// </summary>
    /// <returns>
    ///
    /// </returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /api/queryProducts/20220819
    ///
    ///     {
    ///         "date": "20220819",
    ///         "utcTime": "000000",
    ///         "resolution": "1024",
    ///         "wavelength": "HMIB",
    ///         "url": "https://sdo.gsfc.nasa.gov/assets/img/browse/2022/08/19/20220819_000000_1024_HMIB.jpg"
    ///     }
    ///     ...
    ///       
    /// </remarks>


    [HttpGet("queryProducts/{date}")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 3600)]
    public async Task<ActionResult<List<Product>>> GetProducts(string date)
    {
        var response =  await _dataScraper.QueryProducts(date: date);
        if (response == null) return NotFound();
        return Ok(response);
    }

    [HttpGet("queryProducts/{wavelength}/{date}")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 3600)]
    public async Task<ActionResult<List<Product>>> GetProducts(string wavelength, string date)
    {
        var response = await _dataScraper.QueryProducts(date: date, wavelength: wavelength);
        if (response == null) return NotFound();
        return Ok(response);
    }

    [HttpGet("queryProducts/{wavelength}/{date}/{utc}/{resolution}")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 3600)]
    public async Task<ActionResult<List<Product>>> GetProducts(string wavelength, string date, string utc, string resolution)
    {
        return await _dataScraper.QueryProducts(date, utcTime: utc, resolution: resolution, wavelength: wavelength);
    }
}