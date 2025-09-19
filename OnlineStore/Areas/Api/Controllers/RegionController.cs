using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models;
using OnlineStore.Models.Dtos.Responses;
using OnlineStore.Helpers;
using OnlineStore.Services;

namespace OnlineStore.Controllers;

[ApiController]
[Area("Api")]
[Route("[Area]/countries")]
public class RegionController : ControllerBase
{
    private readonly ICountryService _country;
    private readonly ICityService _city;
    private readonly IStateService _state;

    public RegionController(ICountryService country, ICityService city, IStateService state)
    {
        _country = country;
        _city = city;
        _state = state;
    }

    // fetch all countries with cities with state
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<Country>>> CountriesAll()
    {
        var countries = await _country.ListAllWithDetailsBasedOnLaguage();
        return Ok(ApiResponseHelper<CountryDto>.CollectionSuccess(countries, ""));
    }

    // fetch all countries
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Country>>> Countries()
    {
        var response = await _country.ListBasedOnLaguage();
        return Ok(ApiResponseHelper<CountryDto>.CollectionSuccess(response, ""));
    }

    // fetch all city's states
    [HttpGet("{countryId}/states")]
    public async Task<ActionResult<IEnumerable<Country>>> States(int countryId)
    {
        var response = await _state.ListByCountry(countryId);
        return Ok(ApiResponseHelper<StateDto>.CollectionSuccess(response, ""));
    }
      // fetch all state's cities
    [HttpGet("state/{stateId}/cities")]
    public async Task<ActionResult<IEnumerable<Country>>> Cities(int stateId)
    {
        var response = await _city.ListByState(stateId);
        return Ok(ApiResponseHelper<CityDto>.CollectionSuccess(response, ""));
    }
} 