using System.Diagnostics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using JokeAPI.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction =>
{
    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile); 
    setupAction.IncludeXmlComments(xmlCommentsFullPath);
});

builder.Services.AddDbContext<JokeContext>(
    options =>
    {
        options.UseInMemoryDatabase("JokeCollection");
    });


builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
        tracerProviderBuilder
            .AddSource(DiagnosticsConfig.ActivitySource.Name, "Version1")
            .ConfigureResource(resource => resource
                .AddService(DiagnosticsConfig.ServiceName, DiagnosticsConfig.ServiceNamespace)
                //Custom tags
                .AddAttributes(
                    new KeyValuePair<string, object>[]
                    {
                        new ("deployment.environment", "Development"),
                        new ("service.owner", "LETO"),
                    }))
            .AddAspNetCoreInstrumentation()
            .AddConsoleExporter()
            .AddJaegerExporter()
    );

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();

#pragma warning disable CS1591
public abstract partial class Program { }
#pragma warning restore CS1591

/// <summary>
/// Constructor for JokeController
/// </summary>

// Note: ActivitySource in .NET = Trace
// Note: Activity in .NET = Span
public class DiagnosticsConfig
{
    /// <summary></summary>
    public const string ServiceName = "JokeAPI";
    /// <summary></summary>
    public const string ServiceNamespace = "JokeCenter";
    /// <summary></summary>
    public static readonly ActivitySource ActivitySource = new(ServiceName);
}

