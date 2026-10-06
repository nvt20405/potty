using UnityEngine;

public class TichNapHangNgayItem : MonoBehaviour
{
	public UILabel lbInfo;

	public UISprite spHomQua;

	public UIButton btnDoi;

	public UserInfo.ServerData.MocTichLuyNapHangNgay mThuongNapData;

	public int IdxMocThuongNap = -1;

	public void setData(UserInfo.ServerData.MocTichLuyNapHangNgay mocThuongNap, int indexMocNap, int soKNBDaNap)
	{
		mThuongNapData = mocThuongNap;
		IdxMocThuongNap = indexMocNap;
		btnDoi.gameObject.SetActive(false);
		lbInfo.text = ((mocThuongNap == null) ? string.Empty : mocThuongNap.GiaTri.ToString());
		if (soKNBDaNap < mocThuongNap.GiaTri)
		{
			spHomQua.spriteName = "homqua_tichnap_deactive";
		}
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(string.Format("TichNapHangNgay{0}", indexMocNap)))
		{
			spHomQua.spriteName = "homqua_tichnap_normal";
		}
		if (!GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(string.Format("TichNapHangNgay{0}", indexMocNap)) && soKNBDaNap >= mocThuongNap.GiaTri)
		{
			btnDoi.gameObject.SetActive(true);
			spHomQua.spriteName = "homqua_tichnap_active";
		}
		spHomQua.MakePixelPerfect();
	}
}
