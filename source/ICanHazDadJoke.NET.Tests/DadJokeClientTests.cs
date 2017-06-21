using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace ICanHazDadJoke.NET.Tests
{
	public class DadJokeClientTests
	{
		public const string TestingLibraryName = "ICanHazDadJoke.NET.Tests";
		public const string TestingContactUri = "https://github.com/mattleibow/ICanHazDadJoke.NET";
		public const string TestingUserAgent = TestingLibraryName + " (" + TestingContactUri + ")";

		public const string TestJokeId = "R7UfaahVfFd";
		public const string TestJokeJoke = "My dog used to chase people on a bike a lot. It got so bad I had to take his bike away.";

		[Fact]
		public async Task GetRandomJokeAsyncTest()
		{
			var api = new DadJokeClient(TestingUserAgent);
			var joke = await api.GetRandomJokeAsync();

			Assert.NotNull(joke);
			Assert.NotNull(joke.Id);
			Assert.NotNull(joke.Joke);
			Assert.Equal(200, joke.Status);
		}

		[Fact]
		public async Task GetRandomJokeStringAsyncTest()
		{
			var api = new DadJokeClient(TestingUserAgent);
			var joke = await api.GetRandomJokeStringAsync();

			Assert.NotNull(joke);
			Assert.True(joke.Length > 0);
		}

		[Fact]
		public async Task GetJokeAsyncTest()
		{
			var api = new DadJokeClient(TestingUserAgent);
			var joke = await api.GetJokeAsync(TestJokeId);

			Assert.NotNull(joke);
			Assert.Equal(TestJokeId, joke.Id);
			Assert.Equal(TestJokeJoke, joke.Joke);
			Assert.Equal(200, joke.Status);
		}

		[Fact]
		public async Task GetJokeStringAsyncTest()
		{
			var api = new DadJokeClient(TestingUserAgent);
			var joke = await api.GetJokeStringAsync(TestJokeId);

			Assert.NotNull(joke);
			Assert.Equal(TestJokeJoke, joke);
		}

		[Fact]
		public async Task GetJokeImageAsyncTest()
		{
			var api = new DadJokeClient(TestingUserAgent);
			var joke = await api.GetJokeImageAsync(TestJokeId);

			var stream = new MemoryStream();
			await joke.CopyToAsync(stream);

			Assert.NotNull(joke);
			Assert.True(stream.Length > 0);
		}

		[Fact]
		public async Task SearchJokesAsyncTest()
		{
			var api = new DadJokeClient(TestingUserAgent);
			var results = await api.SearchJokesAsync(TestJokeJoke);

			Assert.NotNull(results);
			Assert.Equal(1, results.CurrentPage);
			Assert.Equal(20, results.Limit);
			Assert.Equal(2, results.NextPage);
			Assert.Equal(1, results.PreviousPage);
			Assert.NotNull(results.Results);
			Assert.True(results.Results.Length > 0);
			Assert.Equal(TestJokeId, results.Results[0].Id);
			Assert.Equal(TestJokeJoke, results.Results[0].Joke);
			Assert.Equal(TestJokeJoke, results.SearchTerm);
			Assert.True(results.TotalJokes > 0);
			Assert.True(results.TotalPages > 0);
		}

		[Fact]
		public async Task SearchJokesStringsAsyncTest()
		{
			var api = new DadJokeClient(TestingUserAgent);
			var results = await api.SearchJokesStringsAsync(TestJokeJoke);

			Assert.NotNull(results);
			Assert.True(results.Length > 0);
			Assert.Equal(TestJokeJoke, results[0]);
		}

		[Fact]
		public async Task SubmitJokeAsyncTest()
		{
			var api = new DadJokeClient(TestingUserAgent);
			var response = await api.SubmitJokeAsync(TestJokeJoke);

			Assert.NotNull(response);
			Assert.NotNull(response.Id);
			Assert.Equal("accepted, pending approval", response.Message);
			Assert.Equal(202, response.Status);
		}
	}
}
