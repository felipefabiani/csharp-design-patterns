using SpecificationDesignPattern.Logic.Helpers;

namespace Logic.Movies
{
    public interface IMovieService
    {
        Task<(IReadOnlyList<MovieEntity>, int total)> GetList(
            Specification<MovieEntity> specification,
            decimal minimumRating,
            int page = 0,
            int pageSize = 4);
    }
}