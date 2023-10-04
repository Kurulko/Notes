using Notes.Commons;

namespace Notes.Commons.Extensions;

public static class StringExtensions
{
    public static OrderBy ParseToOrderBy(this string orderByStr)
       => orderByStr?.ToLower() switch
       {
           "ascending" or "asc" => OrderBy.Ascending,
           "descending" or "desc" => OrderBy.Descending,
           _ => throw new ArgumentException("Can't parse to OrderBy")
       };

    public static bool TryParseToOrderBy(this string? orderByStr, out OrderBy? orderBy)
    {
        try
        {
            orderBy = ParseToOrderBy(orderByStr!);
            return true;
        }
        catch
        {
            orderBy = default;
            return false;
        }
    }
}