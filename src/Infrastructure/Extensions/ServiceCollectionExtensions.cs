using CloudHotel.Application.Rooms.CreateRoom;
using CloudHotel.Domain.GuestAggregate;
using CloudHotel.Domain.ReservationAggregate;
using CloudHotel.Domain.RoomAggregate;
using CloudHotel.Infrastructure.Persistence.Context;
using CloudHotel.Infrastructure.Persistence.Repositories;
using FluentValidation;

namespace CloudHotel.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureValidators(this IServiceCollection services) =>
        services.AddValidatorsFromAssemblyContaining<CreateRoomCommand>();

    public static void ConfigureHttpContext(this IServiceCollection services) =>
        services.AddHttpContextAccessor();
    
    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => {
            options.UseNpgsql(configuration["PostgresConnectionString"]);
        });
        
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AppDbContext>());
        services.AddScoped<IAppDbContext>(sp => sp.GetRequiredService<AppDbContext>());
    }
    
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options => options.SwaggerDoc("v1", new(){ Title = "CloudHotel" }));
    }
    
    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<IGuestRepository, GuestRepository>();
    }
    
    public static void ConfigureMediator(this IServiceCollection services)
    {
        services.AddMediatR(options => 
        {
            options.Lifetime = ServiceLifetime.Scoped;
            options.RegisterServicesFromAssemblyContaining<CreateRoomCommand>();
        });
    }
}