using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Models;
using Services;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private const string API_TOKEN = "GOES-HERE";
    private const string BASE_URL = "https://webjetapitest.azurewebsites.net/api";
    private readonly IIntegrationProvider _integrationProvider;

    public MoviesController(HttpClient httpClient, IIntegrationProvider integrationProvider)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("x-access-token", API_TOKEN);
        _integrationProvider = integrationProvider;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        IActionResult result = StatusCode(500, "Error fetching movies");
        try
        {
            var movies = await _integrationProvider.GetMovies();

            result = Ok(movies);
        }
        catch (Exception ex)
        {
            result = StatusCode(500, $"Error fetching movies: {ex.Message}");
        }

        Response.Headers.Append("Access-Control-Allow-Origin", "http://localhost:3000");
        return result;
    }

    [HttpGet("{movieId}")]
    public async Task<IActionResult> GetMovieDetails(string movieId)
    {
        IActionResult result = StatusCode(500, "Error fetching movie details");
        try
        {
            var movieDetails = await _integrationProvider.GetMovieDetails(movieId);
            result = Ok(movieDetails);
        }
        catch (Exception ex)
        {
            result = StatusCode(500, $"Error fetching movie details: {ex.Message}");
        }

        Response.Headers.Append("Access-Control-Allow-Origin", "http://localhost:3000");
        return result;
    }
}

