namespace Notes.ViewModels;

public class PageViewModel
{
    public PageViewModel(int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        TotalPages = pageSize <= 0 ? 1 : (int)Math.Ceiling((double)count / pageSize);
    }

    public int PageNumber { get; }
    public int TotalPages { get; }
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
}