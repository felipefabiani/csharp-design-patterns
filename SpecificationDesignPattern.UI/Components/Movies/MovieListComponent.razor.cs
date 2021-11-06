﻿using Logic.Movies;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using SpecificationDesignPattern.Logic;
using SpecificationDesignPattern.Logic.Movies;
using SpecificationDesignPattern.Logic.ViewModels;

namespace SpecificationDesignPattern.UI.Components.Movies;

public partial class MovieListComponent
{
    [Parameter]
    public EventCallback<MovieEntity> OnMovieSelected { get; set; }

    [Parameter]
    public MovieSearchVM? MovieSearch { get; set; }

    [Inject] IMovieService MovieService { get; set; } = null!;

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

    protected override async Task OnParametersSetAsync()
    {
        if (MovieSearch is null)
        {
            return;
        }

        try
        {
            _loading = true;
            _movies = await MovieService.GetList(MovieSearch);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, MudBlazor.Severity.Error);
            Snackbar.Add("Unexpected error.", MudBlazor.Severity.Error);
        }
        finally
        {
            _loading = false;
        }
    }
}