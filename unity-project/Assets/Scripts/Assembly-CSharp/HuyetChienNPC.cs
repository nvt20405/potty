using System.Collections.Generic;

public class HuyetChienNPC
{
	public int HID { get; set; }

	public string CodeName { get; set; }

	public List<GiangHoCfg.NhiemVu.NhanVatGH.VoCong> VoCongList { get; set; }

	public string VuKhi { get; set; }

	public int Level { get; set; }

	public float PosX { get; set; }

	public float PosY { get; set; }

	public int Slot { get; set; }
}
