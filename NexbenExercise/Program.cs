using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NexbenExercise.Services;
using System;

namespace NexbenExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            // DI for this seems overkill, but I thought building this out in a normal
            // workflow and with better organization would be helpful.
            var serviceBuilder = new ServiceCollection()
            .AddSingleton<ILastFmApiService, LastFmApiService>()
            .BuildServiceProvider();

            var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<Program>()
            .Build();

            var apiKey = config.GetSection("ApiKey").Value;

            var apiService = serviceBuilder.GetService<ILastFmApiService>();
            var result = apiService.GetAllTracks(apiKey);

            // to keep it open for now
            Console.ReadLine();
        }
    }
}