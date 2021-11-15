using SpecificationDesignPattern.Logic.Utils;
using System.Linq.Expressions;

namespace SpecificationDesignPattern.Logic.Movies;
public class MovieEntity : Entity
{
    public string Name { get; set; } = string.Empty;
    public DateTimeOffset ReleaseDate { get; } = DateTimeOffset.MinValue;
    public MpaaRating MpaaRating { get; } = MpaaRating.G;
    public string Genre { get; } = string.Empty;
    public decimal Rating { get; } = 0M;

    protected MovieEntity()
    {
    }
    public static readonly Expression<Func<MovieEntity, bool>> IsSuitableForChildren = x => x.MpaaRating <= MpaaRating.PG;
    public static readonly Expression<Func<MovieEntity, bool>> HasCDVersion = x => x.ReleaseDate <= DateTime.Now.AddYears(-6);
}
