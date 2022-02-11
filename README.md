# NexbenExercise
Tech Screen

My approach to this was to make a console app, but still try to include some of my normal workflow, so I included (some) dependency injection and also
allowed the user to input both the number of songs and the % of top artists they want to get back (for challenge 2/3). I also wanted to iterate over the data as few times as possible to make it a more 'efficient' program.

Regarding the DI, ideally IConfig (used to conceal your API Key) would be injected into the service, not the program.cs. However, I found attempting to do this was cumbersome setup-wise and not worth the time, so I made the ApiKey a parameter for the API calls instead. I also instantiated the HttpClient, but if I could go back I'd use IHttpClientFactory via DI in my service to make it more in line with a normal workflow.

In regards to the user inputs, I discovered a bug in the LastFM API where most multiples of 50 above 100 will only return 50 songs. For example, 150 and 250 will both
return only 50 songs. 1000 will return 1000. I didnt feel like testing all the multiples up to 1000 (or above), so just know that some of these inputs will only return 
50 and that's a bug on LastFM's side. 

I did think of some edge cases:
1. No guidance around sort order when 2 artists tie (alphabetical? total playcount?).
2. It is possible to hit the max int (2,147,483,647) in total playcount, though unlikely (Looking at you The Weekend).

You may have to plug ApiKey into the Project secrets (NexbenExercise --> Manage User Secrets --> "ApKey": "LastFmApiKey") to run the app. Alternatively, you can
put the ApiKey into the appsettings.json, since secrets.json overrides that. Should work either way.
