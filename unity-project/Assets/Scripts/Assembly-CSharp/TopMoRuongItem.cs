using UnityEngine;

public class TopMoRuongItem : MonoBehaviour
{
	public UILabel lbName;

	public UILabel lbHang;

	public UILabel lbCount;

	private int mIndex = -1;

	public void setData(TopEventMoRuongResponse.MoRuongData data, int index)
	{
		if (data != null)
		{
			mIndex = index;
			lbName.text = data.DisplayName;
			lbHang.text = (index + 1).ToString();
			lbCount.text = data.SoLuong.ToString();
		}
		else
		{
			lbName.text = string.Empty;
			lbHang.text = string.Empty;
			lbCount.text = string.Empty;
		}
	}

	public void onClick_HomQua()
	{
		if (mIndex >= 0)
		{
			PhanThuongResponse phanThuongResponse = GameManager.instance.m_GameClient.UserInfo.ServerInfo.TopBaoRuongConfig.PhanThuongTopEvent[mIndex];
			if (phanThuongResponse != null)
			{
				PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongNhanDuoc"), phanThuongResponse);
			}
		}
	}
}
