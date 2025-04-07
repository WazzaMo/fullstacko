
using Models;
using System.Text.Json;
using System.Net;
namespace Services;

public class MoviesIntegrationProvider : IIntegrationProvider
{
    private readonly HttpClient _httpClient;
    private const string BASE_URL = "https://webjetapitest.azurewebsites.net/api";
    private string API_TOKEN = "GOES-HERE";
    private readonly IConfiguration _Configuration;
    
    const double TIMEOUT = 1.5d;
    private double _TimeoutDuration = TIMEOUT;
    private readonly TimeSpan _Timeout = TimeSpan.FromSeconds(TIMEOUT);

    public MoviesIntegrationProvider(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _Configuration = configuration;
        var apiToken = _Configuration.GetValue<string>("API_TOKEN");
        if (apiToken == null)
        {
            Console.Error.WriteLine("API_TOKEN is not configured");
        }
        else
        {
            API_TOKEN = apiToken;
        }
        _httpClient.DefaultRequestHeaders.Add("x-access-token", API_TOKEN);
    }

    public async Task<IEnumerable<MovieSummaryModel>> GetMovies()
    {
        string endpoint1 = $"{BASE_URL}/filmworld/movies";
        string endpoint2 = $"{BASE_URL}/cinemaworld/movies";
        string[] endpoints = ["/filmworld/movies", "/cinemaworld/movies"];
        MovieListModel movieList = new MovieListModel();
        try
        {
            movieList = await GetData<MovieListModel>(endpoint1, endpoint2);
        }
        catch (Exception ex)
        {
            throw new Exception("Error fetching movies", ex);
        }

        return movieList.Movies;
    }

    public async Task<MovieDetailsModel> GetMovieDetails(string movieId)
    {
        string CinemaWorldEndpoint = $"{BASE_URL}/cinemaworld/movie/{movieId}";
        string FilmWorldEndpoint = $"{BASE_URL}/filmworld/movie/{movieId}";

        MovieDetailsModel movieDetails = new MovieDetailsModel();

        try
        {
            movieDetails = await GetData<MovieDetailsModel>(CinemaWorldEndpoint, FilmWorldEndpoint);
        }
        catch (Exception ex)
        {
            throw new Exception("Error fetching movie details", ex);
        }

        return movieDetails;
    }

    private async Task<T> GetData<T>(string endpoint1, string endpoint2)
    {
        string[] endpoints = [endpoint1, endpoint2];

        Task<HttpResponseMessage> response1 = _httpClient.GetAsync(endpoint1);
        Task<HttpResponseMessage> response2 = _httpClient.GetAsync(endpoint2);
        Task<HttpResponseMessage>[] getTasks = [response1, response2];
        int index = Task.WaitAny(getTasks, GetTimeout());
        if (index == -1)
        {
            BackoffTimeout();
            throw new Exception("Timeout while fetching movie details");
        }
        Console.WriteLine($"Response returned from : {endpoints[index]}");

        Task<HttpResponseMessage> getResponse = getTasks[index];
        var responseContent = await getResponse.Result.Content.ReadAsStringAsync();
        
        T result = JsonSerializer.Deserialize<T>(responseContent)
                ?? throw new Exception("Failed to deserialize response");
        return result;
    }

    private TimeSpan GetTimeout()
    {
        return TimeSpan.FromSeconds(_TimeoutDuration);
    }

    private void BackoffTimeout()
    {
        if (_TimeoutDuration < 60)
        {
            _TimeoutDuration *= 2;
            Console.WriteLine($"Timeout duration increased to {_TimeoutDuration} seconds");
        }
    }
}
