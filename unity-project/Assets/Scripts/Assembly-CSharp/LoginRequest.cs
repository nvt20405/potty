public class LoginRequest
{
	public string PublisherAccessToken = string.Empty;

	public string Platform = string.Empty;

	public string user { get; set; }

	public string pass { get; set; }

	public int Version { get; set; }
}
