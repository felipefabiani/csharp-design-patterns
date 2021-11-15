namespace SpecificationDesignPattern.Logic.Helpers;

public sealed class MovieForKidsSpecification : Specification<MovieEntity>
{
    public override Expression<Func<MovieEntity, bool>> ToExpression()
        => movie => movie.MpaaRating <= MpaaRating.PG;
}
