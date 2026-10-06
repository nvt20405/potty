public class LayDeTuRequest
{
	public enum LoaiLayDeTu
	{
		Loai1 = 0,
		Loai2 = 1,
		Loai3 = 2
	}

	public LoaiLayDeTu loai { get; set; }

	public bool isFree { get; set; }
}
