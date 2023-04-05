using System.Diagnostics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Net;
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
            .AddSource(DiagnosticsConfig.ActivitySource1.Name, DiagnosticsConfig.ActivitySource2.Name,
                DiagnosticsConfig.ActivitySource3.Name)
            .ConfigureResource(resource => resource
                .AddService(DiagnosticsConfig.ServiceName, DiagnosticsConfig.ServiceNamespace,
                    DiagnosticsConfig.DeploymentEnvironment))
            .AddAspNetCoreInstrumentation()
            .AddConsoleExporter()
            .AddJaegerExporter());





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
public static class DiagnosticsConfig
{
    /// <summary></summary>
    public const string ServiceName = "JokeAPI";
    /// <summary></summary>
    public const string ServiceNamespace = "JokeCenter";
    /// <summary></summary>
    public const string DeploymentEnvironment = "Development";
    
    public static ActivitySource ActivitySource1 = new ActivitySource(ServiceName);
    public static ActivitySource ActivitySource2 = new ActivitySource(ServiceName);
    public static ActivitySource ActivitySource3 = new ActivitySource(DeploymentEnvironment);
}