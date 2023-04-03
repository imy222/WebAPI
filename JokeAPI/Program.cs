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
