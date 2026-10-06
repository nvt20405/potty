public class OpenHopRequest
{
	private int count = 1;

	public int HopID { get; set; }

	public int KeyID { get; set; }

	public int Count
	{
		get
		{
			return count;
		}
		set
		{
			count = value;
		}
	}
}
