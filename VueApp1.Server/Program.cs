using System.Net;

namespace VueApp1.Server;

public class Program
{
    public static int ServerPortNumber = 5199;
    public static int ClientPortNumber = 5173;

    public static void Main(string[] args)
    {
        var app = BuildWebApplication();
        
        app.Run();
    }

    /// <summary>
    /// create a host for the application
    /// </summary>
    public static WebApplication BuildWebApplication()
    {
        var builder = WebApplication.CreateBuilder();

        if (builder.Environment.IsDevelopment())
        {
            // use server port number when developing since apps will be split out into multiple startup projects with different ports
            builder.WebHost.UseKestrel(x => x.Listen(IPAddress.Loopback, ServerPortNumber));
        }
        else
        {
            // ONLY use client port number for everything in prod since client / JS app will be running as a single app
            builder.WebHost.UseKestrel(x => x.Listen(IPAddress.Loopback, ClientPortNumber));
        }
        
        var app = builder.Build();

        app.UseDefaultFiles();
        app.UseStaticFiles();

        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        app.MapGet("/weatherforecast", () =>
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
                .ToArray();
            return forecast;
        });

        app.MapFallbackToFile("/index.html");

        return app;
    }

    internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}

