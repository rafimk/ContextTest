using ContextTest.Api.Contexts;
using ContextTest.Api.Contexts.Accessors;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddContexts()
        .AddHttpClient("MyClient")
        .AddContextHandler();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.UseContexts();
app.MapGet("/weatherforecast", (IContextProvider contextProvider, IContextAccessor contextAccessor) =>
{
    var test = contextProvider.Current("rafi");
    var corelationId = contextAccessor.Context?.CorrelationId;
    var user = contextAccessor.Context?.UserId;
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
