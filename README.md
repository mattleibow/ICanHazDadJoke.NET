# ICanHazDadJoke.NET

[![Build Status](https://dev.azure.com/mattleibow/OpenSource/_apis/build/status/ICanHazDadJoke.NET?branchName=master)](https://dev.azure.com/mattleibow/OpenSource/_build/latest?definitionId=14&branchName=master)  [![NuGet](https://img.shields.io/nuget/v/ICanHazDadJoke.NET.svg)](https://www.nuget.org/packages/ICanHazDadJoke.NET)

A .NET Client for [icanhazdadjoke.com](https://icanhazdadjoke.com/), the internet's largest selection of dad jokes.

The API is simple and you only require two pieces of information:

1. The name of the library or website that is accessing the API
2. A URL/e-email where someone can be contacted regarding the library/website

These two pieces of information are used to create the “User Agent” to let the service know who the app is. *(More information can be found on the service website: https://icanhazdadjoke.com/api#custom-user-agent)*

```csharp
// the identifying information
var libraryName = "ICanHazDadJoke.NET Readme";
var contactUri = "https://github.com/mattleibow/ICanHazDadJoke.NET";

// creating a client
var client = new DadJokeClient(libraryName, contactUri);

// getting a dad joke
string dadJoke = await client.GetRandomJokeStringAsync();
```

There are more methods that can be used to search (with pagination) as well as methods to retrieve a specific dad joke.

For a fun read and more information, check out the workbook - which you can run on your own machine:

https://github.com/mattleibow/ICanHazDadJoke.NET/blob/master/workbooks/simple.workbook

