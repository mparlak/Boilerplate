namespace Boilerplate.Application.Common.Models;

public class PagedResponse<T>
{
    public int Index { get; set; }
    public long PageSize { get; set; }
    public long? Total { get; set; }
    public T Items { get; set; }
    public string First { get; set; }
    public string Next { get; set; }
    public string Prev { get; set; }
    public string Last { get; set; }
}