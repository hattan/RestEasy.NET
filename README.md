# RestEasy.NET
A simple client for fetching data from an api and de-serializing it easily.

##Example

```

 public async Task<RottenTomatoApiResult> GetMovies()
        {
            var url = "http://api.rottentomatoes.com/api/public/v1.0/lists/movies/opening.json?apikey=<apikey>";
            var client = new RestClient();
            return await client.GetAsync<RottenTomatoApiResult>(url);
        }
        
```
