public class CheckUserRequest
{
	public bool supportsChunks { get; set; }

	public bool supportsCompression { get; set; }

	public string user { get; set; }

	public string token { get; set; }

	public bool reconnect { get; set; }

	public CheckUserRequest()
	{
		reconnect = false;
	}
}
