using UnityEngine;

public class TuongDuongTeTheItem : MonoBehaviour
{
	public UILabel lbInfo;

	public UIButton btnDoi;

	public UIButton btnHopQua;

	public UILabel lbDaNhan;

	public UserInfo.ServerData.MocTichLuyNap mThuongNapData;

	public int IdxMocThuongNap = -1;

	public void setData(UserInfo.ServerData.MocTichLuyNap mocThuongNap, int indexMocNap, int soKNBDaNap)
	{
		mThuongNapData = mocThuongNap;
		IdxMocThuongNap = indexMocNap;
		lbInfo.text = ((mocThuongNap == null) ? string.Empty : string.Format(Localization.instance.Get("SoDiemCanDoiLabel"), mocThuongNap.GiaTri));
		if (!CommonHelper.CheckFlag(GameManager.instance.m_GameClient.UserInfo.Gamer.TichLuyNapFlag, indexMocNap))
		{
			btnDoi.gameObject.SetActive(true);
			lbDaNhan.gameObject.SetActive(false);
		}
		else
		{
			btnDoi.gameObject.SetActive(false);
			lbDaNhan.gameObject.SetActive(true);
		}
	}

	public void onClick_HomQua()
	{
		if (mThuongNapData != null)
		{
			PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
			phanThuongResponse.PhanThuongList = mThuongNapData.ListPhanThuong;
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongSeNhanDuoc"), phanThuongResponse);
		}
	}
}
