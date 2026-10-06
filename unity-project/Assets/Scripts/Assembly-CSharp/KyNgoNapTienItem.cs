using UnityEngine;

public class KyNgoNapTienItem : MonoBehaviour
{
	public UILabel lbVang;

	public UserInfo.ServerData.MocThuongNap mThuongNapData;

	public UISprite spHomQua;

	public void setData(UserInfo.ServerData.MocThuongNap mocThuong)
	{
		if (mocThuong != null)
		{
			mThuongNapData = mocThuong;
			lbVang.text = mocThuong.GiaTri.ToString();
			if (GameManager.instance.m_GameClient.UserInfo.Gamer.DiemThuongNap < mocThuong.GiaTri)
			{
				spHomQua.color = new Color(114f / 255f, 95f / 255f, 95f / 255f);
			}
		}
	}

	public void btnHopQua_OnClick()
	{
		if (mThuongNapData != null)
		{
			PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
			phanThuongResponse.PhanThuongList = mThuongNapData.ListPhanThuong;
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongSeNhanDuoc"), phanThuongResponse);
		}
	}
}
