namespace CloudHotel.Application.Abstractions.Models;

public abstract class ListQuery
{
    public abstract int Page { get; }
    public abstract int Limit { get; }
    public int Offset => (Page - 1) * Limit;
}
