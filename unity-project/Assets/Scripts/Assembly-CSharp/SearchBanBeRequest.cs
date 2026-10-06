public class SearchBanBeRequest
{
	public string Name { get; set; }

	public int Level { get; set; }

	public SearchBanBeRequest()
	{
		Name = string.Empty;
		Level = 0;
	}
}
