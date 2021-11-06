using SpecificationDesignPattern.Logic.Utils;

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

    public virtual bool IsSuitableForChildren() => MpaaRating <= MpaaRating.PG;

    public virtual bool HasCDVersion() => ReleaseDate <= DateTime.Now.AddMonths(-6);
}
