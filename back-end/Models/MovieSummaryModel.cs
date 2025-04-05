
namespace Models;

public class MovieSummaryModel
{
    public string Title { get; set; }
    public string Year { get; set; }
    public string ID { get; set; }
    public string Type { get; set; }
    public string Poster { get; set; }

    public MovieSummaryModel()
    {
        Title = string.Empty;
        Year = string.Empty;
        ID = string.Empty;
        Type = string.Empty;
        Poster = string.Empty;
    }
    
}

