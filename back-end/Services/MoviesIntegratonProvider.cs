
using Models;
using System.Text.Json;

namespace Services;

public class MoviesIntegrationProvider : IIntegrationProvider
{
    private readonly HttpClient _httpClient;
    private const string BASE_URL = "https://webjetapitest.azurewebsites.net/api";
    private string API_TOKEN = "GOES-HERE";
    private readonly IConfiguration _configuration;

    public MoviesIntegrationProvider(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        API_TOKEN = _configuration.GetValue<string>("API_TOKEN") ?? "GOES-HERE";
        Console.WriteLine($"API_TOKEN: {API_TOKEN}");
        _httpClient.DefaultRequestHeaders.Add("x-access-token", API_TOKEN);
    }

    public async Task<IEnumerable<MovieSummaryModel>> GetMovies()
    {
        List<MovieSummaryModel> allMovies = [];

        try
        {
            var filmWorldTask = _httpClient.GetAsync($"{BASE_URL}/filmworld/movies");
            var cinemaWorldTask = _httpClient.GetAsync($"{BASE_URL}/cinemaworld/movies");
            Task[] getTasks = [filmWorldTask, cinemaWorldTask];
            TimeSpan timeout = TimeSpan.FromSeconds(1.5d);

            int index = Task.WaitAny(getTasks, timeout);
            if (index == -1)
            {
                throw new Exception("Timeout while fetching movies");
            }

            var filmWorldResponse = await filmWorldTask.Result.Content.ReadAsStringAsync();
            var cinemaWorldResponse = await cinemaWorldTask.Result.Content.ReadAsStringAsync();

            var filmWorldMovies = JsonSerializer.Deserialize<MovieListModel>(filmWorldResponse)?.Movies ?? new List<MovieSummaryModel>();
            var cinemaWorldMovies = JsonSerializer.Deserialize<MovieListModel>(cinemaWorldResponse)?.Movies ?? new List<MovieSummaryModel>();

            allMovies = index == 0 ? filmWorldMovies : cinemaWorldMovies;
        }
        catch (Exception ex)
        {
            throw new Exception("Error fetching movies", ex);
        }

        return allMovies;
    }
}
