using CloudHotel.Application.Abstractions.Builders;
using CloudHotel.Application.Reservations.GetReservation;
using CloudHotel.Application.Settings.GetSettings;
using CloudHotel.Infrastructure.Templates;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CloudHotel.Infrastructure.Builders;

internal sealed class DocumentBuilder(IServiceProvider serviceProvider, ILoggerFactory loggerFactory) : IDocumentBuilder
{
    public async Task<string> Build(GetReservationResponse reservationResponse, GetSettingsResponse settingsResponse)
    {
        await using var htmlRenderer = new HtmlRenderer(serviceProvider, loggerFactory);

        var html = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
        {
            var dictionary = new Dictionary<string, object?>
            { 
                { "Reservation", reservationResponse }, 
                { "Settings", settingsResponse } 
            };
            var parameters = ParameterView.FromDictionary(dictionary);
            var output = await htmlRenderer.RenderComponentAsync<FnrhTemaplate>(parameters);

            return output.ToHtmlString();
        });

        return html;
    }
}