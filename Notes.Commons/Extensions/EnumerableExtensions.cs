using Notes.Commons;
using Notes.Models.Database.NotesModels;
using Notes.ViewModels;
using Notes.ViewModels.Database.NotesModels;
using System.Linq.Dynamic.Core;

namespace Notes.Commons.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> models, string attribute, OrderBy orderBy)
        => models.AsQueryable().OrderBy($"{attribute} {orderBy}");

    public static int CountOrDefault<T>(this IEnumerable<T>? models)
        => models?.Count() ?? default;

    public static IEnumerable<T> GetModelsOrEmpty<T>(this IEnumerable<T>? models)
        => models ?? Enumerable.Empty<T>();

    public static IndexViewModel<T> ToIndexViewModel<T>(this IEnumerable<T> models, int countOfAllModels, int? pageSize, int? pageNumber)
    {
        PageViewModel pageViewModel = new(countOfAllModels, pageNumber ?? 1, pageSize ?? countOfAllModels);
        return new IndexViewModel<T>(models, pageViewModel);
    }
}