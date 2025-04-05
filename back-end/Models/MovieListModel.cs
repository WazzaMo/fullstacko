using System.Text.Json.Serialization;

namespace Models;

public class MovieListModel
{
    [JsonPropertyName("Movies")]
    public List<MovieSummaryModel> Movies { get; set; }

    public MovieListModel()
    {
        Movies = new List<MovieSummaryModel>();
    }
}