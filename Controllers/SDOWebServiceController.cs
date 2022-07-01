﻿using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;
using System.Text.Json;

namespace SolarDynamicObservatoryWebService.Controllers;

[ApiController]
[Route("api/")]
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
    /// 
    /// </remarks>

    [HttpGet("latest")]
    public ActionResult<List<Product>> GetLatest()
    {
        return _dataScraper.QueryProducts();
    }

    [HttpGet("queryProducts/{date}")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
    public async Task<ActionResult<List<Product>>> GetProducts(string date)
    {
        return await _dataScraper.QueryProducts(date: date);
    }

    [HttpGet("queryProducts/{wavelength}/{date}")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
    public async Task<ActionResult<List<Product>>> GetProducts(string wavelength, string date)
    {
        return await _dataScraper.QueryProducts(date: date, wavelength: wavelength);
    }

    [HttpGet("queryProducts/{wavelength}/{date}/{utc}/{resolution}")]
    public async Task<ActionResult<List<Product>>> GetProducts(string wavelength, string date, string utc, string resolution)
    {
        return await _dataScraper.QueryProducts(date, utcTime: utc, resolution: resolution, wavelength: wavelength);
    }
}