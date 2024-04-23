namespace CloudHotel.Application.Abstractions.Models;

public class ListResponse<T>(IEnumerable<T> items, int count, int pageNumber, int limit)
{
    public int Current => pageNumber;
    public int Pages => (int)Math.Ceiling(count / (double)limit);
    public int Limit => limit;
    public int Total => count;
    public bool HasPrev => Current > 1;
    public bool HasNext => Current < Pages;
    public IEnumerable<T> Items => items;
}
