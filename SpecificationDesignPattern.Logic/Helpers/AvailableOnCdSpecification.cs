namespace SpecificationDesignPattern.Logic.Helpers;

public sealed class AvailableOnCdSpecification : Specification<MovieEntity>
{
    private const int YEARS_BEFORE_DVD_IS_OUT = 5;
    public override Expression<Func<MovieEntity, bool>> ToExpression()
        => movie => movie.ReleaseDate <= DateTime.Now.AddYears(-YEARS_BEFORE_DVD_IS_OUT);
}