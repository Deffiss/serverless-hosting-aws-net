using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Models;

var builder = WebApplication.CreateBuilder(args);

// Add Swagger/OpenAPI support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add S3 service client to dependency injection container
// builder.Services.AddAWSService<IAmazonS3>();

// Add AWS Lambda support.
builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);
builder.Services.AddCors(); 

var app = builder.Build();

app.UseSwagger(setup => setup.RouteTemplate = "weather/swagger/{documentname}/swagger.json");
app.UseSwaggerUI(setup =>
{
    setup.SwaggerEndpoint("/weather/swagger/v1/swagger.json", "Weather API V1");
    setup.RoutePrefix = "weather/swagger";
});

app.UseCors(bp => bp.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

string[] Summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

// GET weather
app.MapGet("/weather", WeatherForecast[] () =>
    Enumerable.Range(1, 5).Select(index => new WeatherForecast
    {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    }).ToArray()
);

app.Run();