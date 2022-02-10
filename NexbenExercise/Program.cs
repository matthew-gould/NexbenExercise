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
        static int songsInput;
        static float percentageInput;

        static async Task Main(string[] args)
        {
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

            GetSongsInput("Enter number of songs you'd like:");
            GetTopXInput("Enter percentage in decimal form of top artists you'd like returned:");
            Console.WriteLine("\r\n");
            
            var result = await apiService.GetAllTracks(apiKey, songsInput);

            var resultCount = result.Count();
            var topHalf = Math.Ceiling(resultCount * percentageInput);

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

        private static void GetSongsInput(string message)
        {
            int intResult;
            Console.WriteLine(message);
            var result = Console.ReadLine();
            if (int.TryParse(result, out intResult))
            {
                songsInput = intResult;
            }
            else
            {
                GetSongsInput("Trcky Tricky! Enter NUMBER of songs you'd like:");
            }
        }

        private static void GetTopXInput(string message)
        {
            float floatResult;
            Console.WriteLine(message);
            var result = Console.ReadLine();
            if (float.TryParse(result, out floatResult))
            {
                if (floatResult > 1)
                {
                    GetTopXInput("Please enter precentage in decimal form:");
                }
                else
                {
                    percentageInput = floatResult;
                }
            }
            else
            {
                GetTopXInput("Trcky Tricky! Enter precentage NUMBER you'd like:");
            }
        }
    }
}