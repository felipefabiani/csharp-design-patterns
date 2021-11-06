namespace Logic.Movies
{
    public interface IMovieService
    {
        Task<IReadOnlyList<MovieEntity>> GetList(MovieSearchVM movieSearchVM);
    }
}