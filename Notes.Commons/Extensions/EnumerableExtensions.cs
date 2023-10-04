using Notes.Commons;
using Notes.ViewModels;
using System.Linq.Dynamic.Core;

namespace Notes.Commons.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> models, string attribute, OrderBy orderBy)
        => models.AsQueryable().OrderBy($"{attribute} {orderBy}");

    public static IndexViewModel<T> ToIndexViewModel<T>(this IEnumerable<T> models, int countOfAllModels, int? pageSize, int? pageNumber)
    {
        PageViewModel pageViewModel = new(countOfAllModels, pageNumber ?? 1, pageSize ?? countOfAllModels);
        return new IndexViewModel<T>(models, pageViewModel);
    }
}