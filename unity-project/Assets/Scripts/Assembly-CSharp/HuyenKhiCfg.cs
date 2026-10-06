using System.Collections.Generic;

public class HuyenKhiCfg
{
	public string Name { get; set; }

	public string TenHienThi { get; set; }

	public int Loai { get; set; }

	public string LoaiBuff1 { get; set; }

	public string LoaiBuff2 { get; set; }

	public List<float> ChiSo1ByLevels { get; set; }

	public List<float> ChiSo2ByLevels { get; set; }

	public bool IsValid(out string message)
	{
		message = "OK";
		return true;
	}
}
