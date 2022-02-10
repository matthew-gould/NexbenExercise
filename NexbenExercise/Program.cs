using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NexbenExercise.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NexbenExercise
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // DI for this seems overkill, but I thought building this out in a normal
            // workflow and with better organization would be helpful.
            var serviceBuilder = new ServiceCollection()
            .AddSingleton<ILastFmApiService, LastFmApiService>()
            .BuildServiceProvider();

            var apiService = serviceBuilder.GetService<ILastFmApiService>();

            var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<Program>()
            .Build();

            var apiKey = config.GetSection("ApiKey").Value;
            
            var result = await apiService.GetAllTracks(apiKey);

            var resultCount = result.Count();
            var topHalf = Math.Ceiling(resultCount / 2.0);

            var resultList = result.ToList();
            List<KeyValuePair<string, int>> DivisibleByNineList = new List<KeyValuePair<string, int>>();

            for (int i = 0; i < resultCount; i++)
            {
                Console.Write($"{resultList[i].Key} - {resultList[i].Value.SongCount} \r\n");

                if (resultList[i].Value.DivisibleByNine && i <= topHalf-1)
                {
                    DivisibleByNineList.Add(new KeyValuePair<string, int>(resultList[i].Key, resultList[i].Value.TotalPlaycount));
                }
            }

            if (DivisibleByNineList.Any())
            {
                foreach (var artist in DivisibleByNineList)
                {
                    Console.Write($"\r\n Artist divisible by 9: {artist.Key} - {artist.Value}");
                }
            }
            else
            {
                var tayString = await apiService.TayTayIsMyBae(apiKey);
                Console.Write($"\r\n No artists divisible by 9. Top ranked TayTay song: {tayString}");
            }

            Console.ReadLine();
        }
    }
}