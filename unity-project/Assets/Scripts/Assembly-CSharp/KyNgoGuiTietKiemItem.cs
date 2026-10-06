using UnityEngine;

public class KyNgoGuiTietKiemItem : MonoBehaviour
{
	public UIButton btnNhan;

	public UILabel lbThangCapLevel;

	public OtherCfg.LenCapNhanThuongCfg m_Data;

	public PhanThuongItem phanThuong2;

	public void Set(OtherCfg.LenCapNhanThuongCfg cfg)
	{
		if (cfg != null)
		{
			m_Data = cfg;
			setDataThangCap();
		}
	}

	private void setDataThangCap()
	{
		phanThuong2.gameObject.SetActive(false);
		lbThangCapLevel.text = Localization.instance.Get("LenCapLabel") + " " + m_Data.CapYeuCau;
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.Level < m_Data.CapYeuCau)
		{
			btnNhan.isEnabled = false;
		}
		else
		{
			btnNhan.isEnabled = true;
		}
		PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
		phanThuongResponse.PhanThuongList = m_Data.PhanThuong;
		if (phanThuongResponse.PhanThuongList.Count >= 1)
		{
			phanThuong2.gameObject.SetActive(true);
			if (phanThuongResponse.PhanThuongList[0] != null)
			{
				phanThuong2.Set(phanThuongResponse.PhanThuongList[0], false);
				phanThuong2.displayInfo(string.Empty, "x" + phanThuongResponse.PhanThuongList[0].Count);
			}
		}
	}
}
