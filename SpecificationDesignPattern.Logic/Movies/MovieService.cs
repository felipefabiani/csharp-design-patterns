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

        public async Task<IReadOnlyList<MovieEntity>> GetList(MovieSearchVM movieSearchVM)
            => await _context.Movies
                .Where(x =>
                    (x.MpaaRating <= MpaaRating.PG || !movieSearchVM.IsForKidOnly) &&
                    x.Rating >= movieSearchVM.MinimumRating &&
                    (x.ReleaseDate <= DateTimeOffset.Now.AddYears(-6) || !movieSearchVM.IsAvailableOnCD)
                ).ToListAsync();

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
