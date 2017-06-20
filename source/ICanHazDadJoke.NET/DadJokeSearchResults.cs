using Newtonsoft.Json;

namespace ICanHazDadJoke.NET
{
	/// <summary>
	/// Represents dad joke search results.
	/// </summary>
	public class DadJokeSearchResults
	{
		/// <summary>
		/// Gets or sets the current page number.
		/// </summary>
		/// <value>The current page number.</value>
		[JsonProperty("current_page")]
		public int CurrentPage { get; set; }

		/// <summary>
		/// Gets or sets the search results limit.
		/// </summary>
		/// <value>The search results limit.</value>
		[JsonProperty("limit")]
		public int Limit { get; set; }

		/// <summary>
		/// Gets or sets the next page number.
		/// </summary>
		/// <value>The next page number.</value>
		[JsonProperty("next_page")]
		public int NextPage { get; set; }

		/// <summary>
		/// Gets or sets the previous page number.
		/// </summary>
		/// <value>The previous page number.</value>
		[JsonProperty("previous_page")]
		public int PreviousPage { get; set; }

		/// <summary>
		/// Gets or sets the results.
		/// </summary>
		/// <value>The results.</value>
		[JsonProperty("results")]
		public DadJoke[] Results { get; set; }

		/// <summary>
		/// Gets or sets the search term.
		/// </summary>
		/// <value>The search term.</value>
		[JsonProperty("search_term")]
		public string SearchTerm { get; set; }

		/// <summary>
		/// Gets or sets the status.
		/// </summary>
		/// <value>The status.</value>
		[JsonProperty("status")]
		public int Status { get; set; }

		/// <summary>
		/// Gets or sets the total number of jokes.
		/// </summary>
		/// <value>The total number of jokes.</value>
		[JsonProperty("total_jokes")]
		public int TotalJokes { get; set; }

		/// <summary>
		/// Gets or sets the total number of pages.
		/// </summary>
		/// <value>The total number of pages.</value>
		[JsonProperty("total_pages")]
		public int TotalPages { get; set; }
	}
}
