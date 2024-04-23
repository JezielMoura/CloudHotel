
var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureValidators();
builder.Services.ConfigureMediator();
builder.Services.ConfigureHttpContext();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureRepositories();

var app = builder.Build();

app.ConfigureSwagger();

app.MapGroup("/api/rooms").WithTags("Rooms").MapRoomsEndpoints();
app.MapGroup("/api/reservations").WithTags("Reservations").MapReservationsEndpoints();
app.MapGroup("/api/guests").WithTags("Guests").MapGuestsEndpoints();

app.Run();

public partial class Program;
