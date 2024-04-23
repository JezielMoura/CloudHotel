using CloudHotel.Infrastructure.Filters;

namespace CloudHotel.Infrastructure.Extensions;

internal static class ValidadeRouteHandlerBuilder
{
    public static RouteHandlerBuilder Validate<T>(this RouteHandlerBuilder builder)
    { 
        return builder.AddEndpointFilter<ValidationFilter<T>>();
    }
}