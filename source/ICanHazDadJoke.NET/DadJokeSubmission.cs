using Newtonsoft.Json;

namespace ICanHazDadJoke.NET
{
	/// <summary>
	/// Represents a dad joke submission.
	/// </summary>
	public class DadJokeSubmission
	{
		/// <summary>
		/// Gets or sets the joke ID.
		/// </summary>
		/// <value>The joke ID.</value>
		[JsonProperty("id")]
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets the server message.
		/// </summary>
		/// <value>The server message.</value>
		[JsonProperty("message")]
		public string Message { get; set; }

		/// <summary>
		/// Gets or sets the status.
		/// </summary>
		/// <value>The status.</value>
		[JsonProperty("status")]
		public int Status { get; set; }
	}
}
