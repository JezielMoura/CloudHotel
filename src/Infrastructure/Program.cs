var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureValidators();
builder.Services.ConfigureMediator();
builder.Services.ConfigureHttpContext();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureOpenTelemetry(builder.Logging);
builder.Services.ConfigureRepositories();
builder.Services.ConfigureBuilders();

builder.WebHost.ConfigureKestrel(options => {

});

var app = builder.Build();

app.ConfigureSwagger();

app.MapGroup("/api/rooms").WithTags("Rooms").MapRoomsEndpoints();
app.MapGroup("/api/reservations").WithTags("Reservations").MapReservationsEndpoints();
app.MapGroup("/api/guests").WithTags("Guests").MapGuestsEndpoints();
app.MapGroup("/api/settings").WithTags("Settings").MapSettingsEndpoints();

if (app.Environment.IsDevelopment() is false) 
{
    app.UseDefaultFiles();
    app.UseStaticFiles();
    app.MapFallbackToFile("index.html");
}

app.Run();

public partial class Program;
