
using Models;

namespace Services;

public interface IIntegrationProvider
{
    Task<IEnumerable<MovieSummaryModel>> GetMovies();
    Task<MovieDetailsModel> GetMovieDetails(string movieId);
}
