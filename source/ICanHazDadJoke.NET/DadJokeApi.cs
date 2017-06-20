using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ICanHazDadJoke.NET
{
	/// <summary>
	/// An API for fetching a random joke, a specific joke, or searching for jokes in a variety of formats.
	/// </summary>
	public class DadJokeApi
	{
		/// <summary>
		/// The URL of the service (https://icanhazdadjoke.com/).
		/// </summary>
		public const string BaseUrl = "https://icanhazdadjoke.com/";

		private const string NoUserAgentMessage =
			"A library name and contact URL/email must be provided, prefferably in the form 'LibraryName (ContactUri)'. " +
			"See https://icanhazdadjoke.com/api#custom-user-agent for more info.";

		private const string RandomJokeUrl = "/";
		private const string JokeUrl = "/j/{0}";
		private const string SearchUrl = "/search?term={0}&page={1}&limit={2}";
		private const string SubmitUrl = "/submit";

		private HttpClient textHttpClient;
		private HttpClient jsonHttpClient;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ICanHazDadJoke.NET.DadJokeApi"/> class.
		/// </summary>
		/// <param name="libraryName">The library name to use on the User-Agent.</param>
		/// <param name="contactUri">The contact URI to use on the User-Agent.</param>
		public DadJokeApi(string libraryName, string contactUri)
		{
			if (string.IsNullOrWhiteSpace(libraryName))
				throw new ArgumentException(NoUserAgentMessage, nameof(libraryName));
			if (string.IsNullOrWhiteSpace(contactUri))
				throw new ArgumentException(NoUserAgentMessage, nameof(contactUri));

			Init($"{libraryName} ({contactUri})");
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ICanHazDadJoke.NET.DadJokeApi"/> class.
		/// </summary>
		/// <param name="userAgent">The User-Agent string.</param>
		public DadJokeApi(string userAgent)
		{
			if (string.IsNullOrWhiteSpace(userAgent))
				throw new ArgumentException(NoUserAgentMessage, nameof(userAgent));

			Init(userAgent);
		}

		private void Init(string userAgent)
		{
			UserAgent = userAgent;

			textHttpClient = CreateHttpClient(userAgent, "text/plain");
			jsonHttpClient = CreateHttpClient(userAgent, "application/json");
		}

		private static HttpClient CreateHttpClient(string userAgent, string accept)
		{
			var client = new HttpClient();
			client.BaseAddress = new Uri(BaseUrl);
			client.DefaultRequestHeaders.UserAgent.TryParseAdd(userAgent);
			client.DefaultRequestHeaders.Accept.ParseAdd(accept);
			return client;
		}

		/// <summary>
		/// Gets the User-Agent string.
		/// </summary>
		/// <value>The User-Agent string.</value>
		public string UserAgent { get; private set; }

		/// <summary>
		/// Fetches a random joke.
		/// </summary>
		/// <returns>The random joke.</returns>
		public async Task<DadJoke> GetRandomJokeAsync()
		{
			var response = await jsonHttpClient.GetStringAsync(RandomJokeUrl).ConfigureAwait(false);
			return JsonConvert.DeserializeObject<DadJoke>(response);
		}

		/// <summary>
		/// Fetches a random joke as a string.
		/// </summary>
		/// <returns>The random joke.</returns>
		public async Task<string> GetRandomJokeStringAsync()
		{
			return await textHttpClient.GetStringAsync(RandomJokeUrl).ConfigureAwait(false);
		}

		/// <summary>
		/// Fetches a specific joke.
		/// </summary>
		/// <returns>The joke.</returns>
		/// <param name="id">The joke ID.</param>
		public async Task<DadJoke> GetJokeAsync(string id)
		{
			var uri = string.Format(JokeUrl, id);
			var response = await jsonHttpClient.GetStringAsync(uri).ConfigureAwait(false);
			return JsonConvert.DeserializeObject<DadJoke>(response);
		}

		/// <summary>
		/// Fetches a specific joke as a string.
		/// </summary>
		/// <returns>The joke.</returns>
		/// <param name="id">The joke ID.</param>
		public async Task<string> GetJokeStringAsync(string id)
		{
			var uri = string.Format(JokeUrl, id);
			var response = await textHttpClient.GetStringAsync(uri).ConfigureAwait(false);
			return response;
		}

		/// <summary>
		/// Fetches a specific joke as an image.
		/// </summary>
		/// <returns>The joke.</returns>
		/// <param name="id">The joke ID.</param>
		public async Task<Stream> GetJokeImageAsync(string id)
		{
			var uri = string.Format(JokeUrl, id + ".png");
			var response = await textHttpClient.GetStreamAsync(uri).ConfigureAwait(false);
			return response;
		}

		/// <summary>
		/// Searches for jokes.
		/// </summary>
		/// <returns>The jokes.</returns>
		/// <param name="term">The search term.</param>
		/// <param name="page">The search result page number.</param>
		/// <param name="limit">The search results limit.</param>
		public async Task<DadJokeSearchResults> SearchJokesAsync(string term = null, int page = 1, int limit = 20)
		{
			var uri = string.Format(SearchUrl, Uri.EscapeUriString(term), page, limit);
			var response = await jsonHttpClient.GetStringAsync(uri).ConfigureAwait(false);
			return JsonConvert.DeserializeObject<DadJokeSearchResults>(response);
		}

		/// <summary>
		/// Searches for jokes.
		/// </summary>
		/// <returns>The jokes as a list of strings.</returns>
		/// <param name="term">The search term.</param>
		/// <param name="page">The search result page number.</param>
		/// <param name="limit">The search results limit.</param>
		public async Task<string[]> SearchJokesStringsAsync(string term = null, int page = 1, int limit = 20)
		{
			var uri = string.Format(SearchUrl, term, page, limit);
			var response = await textHttpClient.GetStringAsync(uri).ConfigureAwait(false);
			return response?.Split('\n');
		}

		/// <summary>
		/// Submits an new joke.
		/// </summary>
		/// <returns>The joke.</returns>
		/// <param name="joke">The submission results.</param>
		public async Task<DadJokeSubmission> SubmitJokeAsync(string joke)
		{
			var content = new FormUrlEncodedContent(new Dictionary<string, string>
			{
				{ "joke", joke }
			});
			var response = await jsonHttpClient.PostAsync(SubmitUrl, content).ConfigureAwait(false);
			var text = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
			return JsonConvert.DeserializeObject<DadJokeSubmission>(text);
		}
	}
}
