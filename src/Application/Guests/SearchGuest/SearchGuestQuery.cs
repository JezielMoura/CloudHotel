namespace CloudHotel.Application.Guests.SearchGuest;

public class SearchGuestQuery(int page = 1, int limit = 10) : ListQuery, IRequest<IEnumerable<SearchGuestResponse>>
{
    public override int Page => page;
    public override int Limit => limit;
}


