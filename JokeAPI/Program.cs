using System.Reflection;
using Microsoft.EntityFrameworkCore;
using JokeAPI.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction =>
{
    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile); 
    setupAction.IncludeXmlComments(xmlCommentsFullPath);
});
    
// Configure to seed data with DbContext
builder.Services.AddDbContext<JokeContext>(
    options =>
    {
        options.UseInMemoryDatabase("JokeCollection");
        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    });

var app = builder.Build();


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => " 😈 It's not a bug. It's an undocumented feature! 😈");

app.Run();

#pragma warning disable CS1591
public abstract partial class Program { }
#pragma warning restore CS1591
