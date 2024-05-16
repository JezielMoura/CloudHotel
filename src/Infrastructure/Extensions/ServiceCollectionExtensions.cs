using CloudHotel.Application.Abstractions.Builders;
using CloudHotel.Application.Rooms.CreateRoom;
using CloudHotel.Domain.GuestAggregate;
using CloudHotel.Domain.ReservationAggregate;
using CloudHotel.Domain.RoomAggregate;
using CloudHotel.Domain.SettingsAggregate;
using CloudHotel.Infrastructure.Builders;
using CloudHotel.Infrastructure.Persistence.Context;
using CloudHotel.Infrastructure.Persistence.Repositories;
using FluentValidation;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace CloudHotel.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    private static readonly string _serviceName = "CloudHotel";
    private static readonly string[] _metricNames = ["Microsoft.AspNetCore.Hosting", "Microsoft.AspNetCore.Server.Kestrel"];
    
    public static void ConfigureOpenTelemetry(this IServiceCollection services, ILoggingBuilder logging)
    {
        services.AddSingleton(TracerProvider.Default.GetTracer(_serviceName));

        var builder = services.AddOpenTelemetry();

        builder.ConfigureResource(resource => resource.AddService(_serviceName));
        
        builder.WithMetrics(metrics => metrics
            .AddRuntimeInstrumentation()
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddMeter(_metricNames)
            .AddOtlpExporter());

        builder.WithTracing(tracing => tracing
            .AddSource(_serviceName)
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddEntityFrameworkCoreInstrumentation()
            .AddOtlpExporter());

        logging.AddOpenTelemetry(options => {
            options.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(_serviceName));
            options.IncludeFormattedMessage = true;
            options.IncludeScopes = true;
            options.AddOtlpExporter();
        });
    }
    
    public static void ConfigureValidators(this IServiceCollection services) =>
        services.AddValidatorsFromAssemblyContaining<CreateRoomCommand>();

    public static void ConfigureHttpContext(this IServiceCollection services) =>
        services.AddHttpContextAccessor();

    public static void ConfigureBuilders(this IServiceCollection services) =>
        services.AddScoped<IDocumentBuilder, DocumentBuilder>();
    
    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => {
            options.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=941690;Database=CloudHotel");
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
        services.AddScoped<ISettingsRepository, SettingsRepository>();
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