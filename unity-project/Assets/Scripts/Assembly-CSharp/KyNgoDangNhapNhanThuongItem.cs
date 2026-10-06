using UnityEngine;

public class KyNgoDangNhapNhanThuongItem : MonoBehaviour
{
	public PhanThuongItem phanThuong1;

	public PhanThuongItem phanThuong2;

	public PhanThuongItem phanThuong3;

	public UIButton btnNhan;

	public UILabel lbNgay;

	public OtherCfg.DangNhapNhanThuongCfg m_Data;

	public int currentIndex;

	public void Set(OtherCfg.DangNhapNhanThuongCfg cfg, int index, int daySpan, bool isDuDK)
	{
		phanThuong1.gameObject.SetActive(false);
		phanThuong2.gameObject.SetActive(false);
		phanThuong3.gameObject.SetActive(false);
		if (cfg != null)
		{
			m_Data = cfg;
			currentIndex = index;
			if (index == 0 || daySpan == index)
			{
				lbNgay.text = Localization.instance.Get("FirstDayDangNhapLabel");
			}
			else if (index == 1 || index - daySpan == 1)
			{
				lbNgay.text = Localization.instance.Get("SecondDayDangNhapLabel");
			}
			else if (index == 2 || index - daySpan == 2)
			{
				lbNgay.text = Localization.instance.Get("ThreeDayDangNhapLabel");
			}
			else
			{
				lbNgay.text = string.Format(Localization.instance.Get("SoNgayDangNhapLabel"), index - daySpan);
			}
			btnNhan.isEnabled = isDuDK;
		}
		PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
		phanThuongResponse.PhanThuongList = cfg.PhanThuong;
		if (phanThuongResponse.PhanThuongList.Count < 1)
		{
			return;
		}
		phanThuong1.gameObject.SetActive(true);
		if (phanThuongResponse.PhanThuongList[0] != null)
		{
			phanThuong1.Set(phanThuongResponse.PhanThuongList[0], false);
			phanThuong1.displayInfo(string.Empty, "x" + phanThuongResponse.PhanThuongList[0].Count);
		}
		if (phanThuongResponse.PhanThuongList.Count < 2)
		{
			return;
		}
		phanThuong2.gameObject.SetActive(true);
		if (phanThuongResponse.PhanThuongList[1] != null)
		{
			phanThuong2.Set(phanThuongResponse.PhanThuongList[1], false);
			phanThuong2.displayInfo(string.Empty, "x" + phanThuongResponse.PhanThuongList[1].Count);
		}
		if (phanThuongResponse.PhanThuongList.Count >= 3)
		{
			phanThuong3.gameObject.SetActive(true);
			if (phanThuongResponse.PhanThuongList[2] != null)
			{
				phanThuong3.Set(phanThuongResponse.PhanThuongList[2], false);
				phanThuong3.displayInfo(string.Empty, "x" + phanThuongResponse.PhanThuongList[2].Count);
			}
		}
	}

	public void onClick_btnHomQua()
	{
		EGDebug.Log("onCLick_HomQuaBtn");
		if (m_Data != null)
		{
			PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
			phanThuongResponse.PhanThuongList = m_Data.PhanThuong;
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongSeNhanDuoc"), phanThuongResponse);
		}
	}

	public void onClick_Item()
	{
		if (m_Data != null)
		{
			PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
			phanThuongResponse.PhanThuongList = m_Data.PhanThuong;
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongSeNhanDuoc"), phanThuongResponse);
		}
	}
}
