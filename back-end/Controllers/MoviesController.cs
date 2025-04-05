using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Models;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private const string API_TOKEN = "GOES-HERE";
        private const string BASE_URL = "https://webjetapitest.azurewebsites.net/api";

        public MoviesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("x-access-token", API_TOKEN);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IActionResult result = StatusCode(500, "Error fetching movies");
            try
            {
                var filmWorldTask = _httpClient.GetAsync($"{BASE_URL}/filmworld/movies");
                var cinemaWorldTask = _httpClient.GetAsync($"{BASE_URL}/cinemaworld/movies");
                Task[] getTasks = new Task[] { filmWorldTask, cinemaWorldTask };
                TimeSpan timeout = TimeSpan.FromSeconds(1.5d);

                int index = Task.WaitAny(getTasks, timeout);
                if (index == -1)
                {
                    result = StatusCode(500, "Timeout while fetching movies");
                }

                var filmWorldResponse = await filmWorldTask.Result.Content.ReadAsStringAsync();
                var cinemaWorldResponse = await cinemaWorldTask.Result.Content.ReadAsStringAsync();

                var filmWorldMovies = JsonSerializer.Deserialize<MovieListModel>(filmWorldResponse)?.Movies ?? new List<MovieSummaryModel>();
                var cinemaWorldMovies = JsonSerializer.Deserialize<MovieListModel>(cinemaWorldResponse)?.Movies ?? new List<MovieSummaryModel>();

                var allMovies = index == 0 ? filmWorldMovies : cinemaWorldMovies;
                result = Ok(allMovies);
            }
            catch (Exception ex)
            {
                result = StatusCode(500, $"Error fetching movies: {ex.Message}");
            }

            Response.Headers.Append("Access-Control-Allow-Origin", "http://localhost:3000");
            return result;
        }
    }

}
