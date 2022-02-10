# NexbenExercise
Tech Screen

My approach to this was to make a console app, but still try to include some of my normal workflow, so I included (some) dependency injection and also
allowed the user to input both the number of songs and the % of top artists they want to get back (for challenge 2/3).

Regarding the DI, ideally IConfig would be injected into the service, not the program.cs. However, I found attempting to do this was cumbersome setup-wise and not worth
the time, so I made the ApiKey a parameter for the API calls instead. I also instantiated the HttpClient, but if I could go back I'd use IHttpClientFactory via DI
in my service to make it more in line with a normal workflow.

In regards to the user inputs, I discovered a bug in the LastFM API where most multiples of 50 above 100 will only return 50 songs. For example, 150 and 250 will both
return only 50 songs. 1000 will return 1000. I didnt feel like testing all the multiples up to 1000 (or above), so just know that some of these inputs will only return 
50 and that's a bug on LastFM's side. 
