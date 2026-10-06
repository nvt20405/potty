using UnityEngine;

public class KyNgoThangCapItem : MonoBehaviour
{
	public UIButton btnNhan;

	public UILabel lbThangCapLevel;

	public UISprite spriteVIP;

	public OtherCfg.LenCapNhanThuongCfg m_Data;

	public PhanThuongItem phanThuong1;

	public PhanThuongItem phanThuong2;

	public PhanThuongItem phanThuong3;

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
		phanThuong1.gameObject.SetActive(false);
		phanThuong2.gameObject.SetActive(false);
		phanThuong3.gameObject.SetActive(false);
		lbThangCapLevel.text = Localization.instance.Get("LenCapLabel") + " [FF0000]" + m_Data.CapYeuCau;
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
			phanThuong1.gameObject.SetActive(true);
			if (phanThuongResponse.PhanThuongList[0] != null)
			{
				phanThuong1.Set(phanThuongResponse.PhanThuongList[0], false);
				phanThuong1.displayInfo(string.Empty, "x" + phanThuongResponse.PhanThuongList[0].Count);
			}
			if (phanThuongResponse.PhanThuongList.Count >= 2)
			{
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
		}
		if (m_Data.CapYeuCau == 4)
		{
			spriteVIP.spriteName = "icon_vip1";
			spriteVIP.MakePixelPerfect();
			spriteVIP.transform.parent.gameObject.SetActive(true);
			phanThuong3.gameObject.SetActive(false);
		}
		else if (m_Data.CapYeuCau == 10)
		{
			spriteVIP.spriteName = "icon_vip2";
			spriteVIP.MakePixelPerfect();
			spriteVIP.transform.parent.gameObject.SetActive(true);
			phanThuong3.gameObject.SetActive(false);
		}
		else
		{
			spriteVIP.transform.parent.gameObject.SetActive(false);
		}
	}
}
