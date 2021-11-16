using SpecificationDesignPattern.Logic.Helpers;

namespace Logic.Movies
{
    public class MovieService : IMovieService
    {
        private readonly IDbContextFactory<SpecPatternReadDbContext> _dbFactory;

        public MovieService(IDbContextFactory<SpecPatternReadDbContext> dbFactory)
        {
            ArgumentNullException.ThrowIfNull(nameof(dbFactory));

            _dbFactory = dbFactory;
        }

        public async Task<(IReadOnlyList<MovieEntity>, int)> GetList(
            Specification<MovieEntity> specification,
            decimal minimumRating,
            int page = 0,
            int pageSize = 4)
        {
            using var context1 = _dbFactory.CreateDbContext();
            var total = context1.Movies
                .Where(specification.ToExpression())
                .Where(x => x.Rating >= minimumRating)
                .CountAsync()
                .ConfigureAwait(false);

            using var context2 = _dbFactory.CreateDbContext();
            var data = context2.Movies
                .Where(specification.ToExpression())
                .Where(x => x.Rating >= minimumRating)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync()
                .ConfigureAwait(false);

            return (await data, await total);
        }
    }
}
