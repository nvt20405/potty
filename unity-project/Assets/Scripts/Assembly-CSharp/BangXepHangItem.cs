using UnityEngine;

public class BangXepHangItem : MonoBehaviour
{
	public UILabel lbHang;

	public UILabel lbName;

	public UILabel lbDiem;

	public UISprite spHomQua;

	private UserInfo.ServerData.ULinhSonTrangCfg.TopItem m_Data;

	public void setData(UserInfo.ServerData.ULinhSonTrangCfg.TopItem data, GetTopULinhResponse.TopULinhData topData, int index)
	{
		if (data != null)
		{
			m_Data = data;
			lbHang.text = string.Format(Localization.instance.Get("DongNhanHangLabel"), (index + 1).ToString());
			if (topData != null && topData.Diem > 0)
			{
				lbName.text = topData.DisplayName;
				lbDiem.text = "[-] " + Localization.instance.Get("DiemLabel") + ": [ff0000]" + topData.Diem;
			}
			else
			{
				lbName.text = string.Empty;
				lbDiem.text = "[-] " + Localization.instance.Get("ItNhatDatMess") + " [ff0000]" + m_Data.MinDiem;
			}
			switch (index)
			{
			case 0:
				spHomQua.spriteName = "ruong_vang";
				break;
			case 1:
				spHomQua.spriteName = "ruong_bac";
				break;
			case 2:
				spHomQua.spriteName = "ruong_dong";
				break;
			default:
				spHomQua.spriteName = "ruong_thuong";
				break;
			}
			spHomQua.MakePixelPerfect();
			UIEventListener.Get(spHomQua.gameObject).onClick = btnHomQua_OnClick;
		}
	}

	public void btnHomQua_OnClick(GameObject go)
	{
		if (m_Data != null)
		{
			PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
			phanThuongResponse.PhanThuongList = m_Data.PhanThuongs;
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongSeNhanDuoc"), phanThuongResponse);
		}
	}
}
