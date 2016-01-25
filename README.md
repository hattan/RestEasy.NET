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
Note: In order to use RestEasy.NET you have to create the models based on the results of the api endpoint. In visual studio can do this easily by copying the json/xml result and choosing "paste special" follwed by the kind of data you want to generate classes for.
