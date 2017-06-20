using Newtonsoft.Json;

namespace ICanHazDadJoke.NET
{
	/// <summary>
	/// Represents a dad joke.
	/// </summary>
	public class DadJoke
	{
		/// <summary>
		/// Gets or sets the joke ID.
		/// </summary>
		/// <value>The joke ID.</value>
		[JsonProperty("id")]
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets the joke.
		/// </summary>
		/// <value>The joke.</value>
		[JsonProperty("joke")]
		public string Joke { get; set; }

		/// <summary>
		/// Gets or sets the status.
		/// </summary>
		/// <value>The status.</value>
		[JsonProperty("status")]
		public int Status { get; set; }
	}
}
