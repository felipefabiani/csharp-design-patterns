using MudBlazor;
using SpecificationDesignPattern.Logic.Helpers;

namespace SpecificationDesignPattern.UI.Components.Movies;

public partial class MovieListComponent
{
    [Parameter]
    public EventCallback<MovieEntity> OnMovieSelected { get; set; }

    [Parameter]
    public MovieSearchVM? MovieSearch { get; set; }

    [Inject] IMovieService MovieService { get; set; } = null!;

    private MudTable<MovieEntity> _table;
    private int _total = 0;

    private bool _dense = false;
    private bool _hover = true;
    private bool _striped = true;
    private bool _bordered = false;
    private bool _loading = false;
    private string _searchString1 = string.Empty;
    private MovieEntity? _selectedItem1 = null;

    private IReadOnlyList<MovieEntity> _movies = new List<MovieEntity>();
    private bool FilterFunc1(MovieEntity movie) => FilterFunc(movie, _searchString1);
    private static bool FilterFunc(MovieEntity movie, string searchString)
        => movie switch
        {
            { Name: var name } when name.Contains(searchString, StringComparison.OrdinalIgnoreCase) => true,
            { Genre: var genre } when genre.Contains(searchString, StringComparison.OrdinalIgnoreCase) => true,
            _ => false
        };

    private async Task<TableData<MovieEntity>> ServerReload(TableState state)
    {
        if (MovieSearch is null)
        {
            return new TableData<MovieEntity>() { TotalItems = _total, Items = _movies };
        }

        try
        {
            _loading = true;
            var spec = Specification<MovieEntity>.All;

            if (MovieSearch?.IsForKidOnly == true)
            {
                spec = spec.And(MovieEntity.IsSuitableForChildren);
            }

            if (MovieSearch?.IsAvailableOnCD == true)
            {
                spec = spec.And(MovieEntity.HasCDVersion);
            }

            (_movies, _total) = await MovieService.GetList(spec,
                MovieSearch?.MinimumRating ?? 0,
                _table.CurrentPage,
                _table.RowsPerPage);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
            Snackbar.Add("Unexpected error.", Severity.Error);
        }
        finally
        {
            _loading = false;
        }
        return new TableData<MovieEntity>() { TotalItems = _total, Items = _movies};
        
    }
    protected override void OnParametersSet()
    {
        _table?.ReloadServerData();
    }
}
