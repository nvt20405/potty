using UnityEngine;

public class DangNhapNhanThuongTetItem : MonoBehaviour
{
	public PhanThuongItem phanThuongItem1;

	public PhanThuongItem phanThuongItem2;

	public PhanThuongItem phanThuongItem3;

	public PhanThuongItem phanThuongItem4;

	public UILabel lbTime;

	public UIButton btnNhan;

	private UserInfo.ServerData.PTDangNhapTet m_DataPhanThuong;

	public void setData(UserInfo.ServerData.PTDangNhapTet ptDangNhap, int index, int startIndex)
	{
		if (ptDangNhap == null)
		{
			return;
		}
		m_DataPhanThuong = ptDangNhap;
		lbTime.text = string.Empty;
		btnNhan.gameObject.SetActive(false);
		if (index == startIndex)
		{
			if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("DangNhapNhanThuongTet"))
			{
				lbTime.text = Localization.instance.Get("DaNhanMess");
			}
			else
			{
				btnNhan.gameObject.SetActive(true);
			}
		}
		if (index == startIndex + 1)
		{
			lbTime.text = Localization.instance.Get("SecondDayDangNhapLabel");
		}
		else if (index == startIndex + 2)
		{
			lbTime.text = Localization.instance.Get("ThreeDayDangNhapLabel");
		}
		else if (index >= startIndex + 3)
		{
			lbTime.text = string.Format(Localization.instance.Get("SoNgayDangNhapLabel"), index - startIndex);
		}
		if (ptDangNhap.ListPhanThuong == null)
		{
			return;
		}
		if (ptDangNhap.ListPhanThuong[0] != null)
		{
			phanThuongItem1.Set(ptDangNhap.ListPhanThuong[0], true);
			if (ptDangNhap.ListPhanThuong[0].Loai != PhanThuongResponse.LoaiPhanThuong.THU_CUOI)
			{
				phanThuongItem1.displayInfo(string.Empty, "x" + ptDangNhap.ListPhanThuong[0].Count);
			}
		}
		else
		{
			phanThuongItem1.setNullValue();
		}
		if (ptDangNhap.ListPhanThuong[1] != null)
		{
			phanThuongItem2.Set(ptDangNhap.ListPhanThuong[1], true);
			if (ptDangNhap.ListPhanThuong[1].Loai != PhanThuongResponse.LoaiPhanThuong.THU_CUOI)
			{
				phanThuongItem2.displayInfo(string.Empty, "x" + ptDangNhap.ListPhanThuong[1].Count);
			}
		}
		else
		{
			phanThuongItem2.setNullValue();
		}
		if (ptDangNhap.ListPhanThuong[2] != null)
		{
			phanThuongItem3.Set(ptDangNhap.ListPhanThuong[2], true);
			if (ptDangNhap.ListPhanThuong[2].Loai != PhanThuongResponse.LoaiPhanThuong.THU_CUOI)
			{
				phanThuongItem3.displayInfo(string.Empty, "x" + ptDangNhap.ListPhanThuong[2].Count);
			}
		}
		else
		{
			phanThuongItem3.setNullValue();
		}
		if (ptDangNhap.ListPhanThuong[3] != null)
		{
			phanThuongItem4.Set(ptDangNhap.ListPhanThuong[3], true);
			if (ptDangNhap.ListPhanThuong[3].Loai != PhanThuongResponse.LoaiPhanThuong.THU_CUOI)
			{
				phanThuongItem4.displayInfo(string.Empty, "x" + ptDangNhap.ListPhanThuong[3].Count);
			}
		}
		else
		{
			phanThuongItem4.setNullValue();
		}
	}

	public void onClick_BtnNhan()
	{
		if (m_DataPhanThuong != null)
		{
			DangNhapNhanThuongTetRequest dangNhapNhanThuongTetRequest = new DangNhapNhanThuongTetRequest();
			dangNhapNhanThuongTetRequest.Idx = m_DataPhanThuong.Index;
			GameManager.instance.m_GameClient.RequestDangNhapNhanThuongTet(dangNhapNhanThuongTetRequest);
		}
	}
}
