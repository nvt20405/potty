using System.Collections.Generic;

public class JoinCT2Response : ExDataBase
{
	public class DetuInfo
	{
		public string Name { get; set; }

		public float HpMax { get; set; }

		public float Hp { get; set; }

		public int HID { get; set; }
	}

	public List<DetuInfo> DoiHinhList { get; set; }

	public bool JoinNewRoom { get; set; }

	public ChienTruongChinhTa.NguoiChoi ThongTinNguoiChoi { get; set; }

	public ChienTruongChinhTa RoomInfo { get; set; }
}
