using System.Collections.Generic;
using UnityEngine;

public class PopUpManhTrangBi : MonoBehaviour
{
	public OtherAvatar manhTrangBiAvatar;

	public UILabel manhTrangBiName;

	public UILabel soLuongHienCo;

	public UILabel lbTitle;

	public UILabel lbDiaDanh;

	private UserInfo.ManhTrangBiData m_ManhTrangBiData;

	public static PopUpManhTrangBi instance;

	private List<int> listGiangHoIdx = new List<int>();

	private List<int> listNhiemVuIdx = new List<int>();

	public void OnCloseClick()
	{
		DestroyPopup();
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public static void Create(string codeName, int soLuongCo)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("popup/PopupManhTrangBi"))).GetComponent<PopUpManhTrangBi>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.Set(codeName, soLuongCo);
	}

	public void Set(string codeName, int soLuongCo)
	{
		TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[codeName];
		manhTrangBiName.text = trangBiCfg.TenHienThi;
		soLuongHienCo.text = Localization.instance.Get("SoLuongLabel") + ": " + soLuongCo;
		getListInfoNhiemVu(codeName);
		if (!codeName.StartsWith("M") || !codeName.StartsWith("MMU"))
		{
			codeName = "M" + codeName;
		}
		manhTrangBiAvatar.Set(codeName);
	}

	private void getListInfoNhiemVu(string codeName)
	{
		TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[codeName];
		lbTitle.text = string.Format(Localization.instance.Get("ThongTinTimThayManhTrangBi"), trangBiCfg.TenHienThi);
		string text = string.Empty;
		if (!codeName.StartsWith("M") || !codeName.StartsWith("MMU"))
		{
			codeName = "M" + codeName;
		}
		if (ConfigManager.instance.m_listGiangHo != null && ConfigManager.instance.m_listGiangHo.Count > 0)
		{
			listGiangHoIdx.Clear();
			listNhiemVuIdx.Clear();
			for (int i = 0; i < ConfigManager.instance.m_listGiangHo.Count; i++)
			{
				GiangHoCfg giangHoCfg = ConfigManager.instance.m_listGiangHo[i];
				if (giangHoCfg == null || giangHoCfg.NhiemVuList == null || giangHoCfg.NhiemVuList.Count <= 0)
				{
					continue;
				}
				for (int j = 0; j < giangHoCfg.NhiemVuList.Count; j++)
				{
					GiangHoCfg.NhiemVu nhiemVu = giangHoCfg.NhiemVuList[j];
					if (nhiemVu == null || nhiemVu.PhanThuong == null || nhiemVu.PhanThuong.Count <= 0)
					{
						continue;
					}
					for (int k = 0; k < nhiemVu.PhanThuong.Count; k++)
					{
						GiangHoCfg.NhiemVu.PhanThuongNV phanThuongNV = nhiemVu.PhanThuong[k];
						if (phanThuongNV != null && phanThuongNV.PhanThuongCode == codeName)
						{
							string text2 = text;
							text = text2 + "[FF0000]" + (i + 1) + " - " + giangHoCfg.TenHienThi + "\n[-]";
							listGiangHoIdx.Add(i);
							listNhiemVuIdx.Add(j);
						}
					}
				}
			}
		}
		lbDiaDanh.text = text;
	}

	public void BtnGo_OnClick()
	{
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		int num = -1;
		if (listGiangHoIdx.Count > 0 && listNhiemVuIdx.Count == listGiangHoIdx.Count)
		{
			for (int i = 0; i < listGiangHoIdx.Count; i++)
			{
				int num2 = listGiangHoIdx[i];
				int num3 = listNhiemVuIdx[i];
				if (num2 >= userInfo.GiangHo.Count || num3 < 0)
				{
					break;
				}
				if (userInfo.GiangHo[num2] != null && userInfo.GiangHo[num2].NhiemVu != null && userInfo.GiangHo[num2].NhiemVu.Count > 0)
				{
					UserInfo.GiangHoData.NhiemVuRecord nhiemVuRecord = userInfo.GiangHo[num2].NhiemVu[num3];
					if (nhiemVuRecord.S > 0)
					{
						num = num2;
					}
				}
			}
		}
		if (num >= 0)
		{
			GUIManager.setScreen(GAME_SCREEN.ScreenWorldmap);
			ScreenWorldmap screenWorldmap = GUIManager.getScreen(GAME_SCREEN.ScreenWorldmap) as ScreenWorldmap;
			screenWorldmap.setByScreenTinhLuyen(num);
			DestroyPopup();
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("ThongBaoChuaVuotGiangHo"));
		}
	}
}
