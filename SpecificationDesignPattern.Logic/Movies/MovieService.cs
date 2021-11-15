namespace Logic.Movies
{
    public class MovieService : IMovieService, IDisposable
    {
        private readonly SpecPatternReadDbContext _context;

        public MovieService(IDbContextFactory<SpecPatternReadDbContext> dbFactory)
        {
            ArgumentNullException.ThrowIfNull(nameof(dbFactory));
            _context = dbFactory.CreateDbContext();
            ArgumentNullException.ThrowIfNull(nameof(_context));
        }

        public async Task<IReadOnlyList<MovieEntity>> GetList(
            Expression<Func<MovieEntity, bool>> expression)
            => await _context.Movies.Where(expression).ToListAsync();

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
