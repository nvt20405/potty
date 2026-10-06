using System.Collections.Generic;

public class EffHuyenKhiCfg
{
	public string Name { get; set; }

	public string Mota { get; set; }

	public string TenHienThi { get; set; }

	public List<float> HeSoByLevels { get; set; }

	public bool IsValid(out string message)
	{
		message = "OK";
		return true;
	}
}
