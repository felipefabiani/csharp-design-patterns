namespace Logic.Movies
{
    public interface IMovieService
    {
        Task<IReadOnlyList<MovieEntity>> GetList(Expression<Func<MovieEntity, bool>> expression);
    }
}