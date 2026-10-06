using System.Collections.Generic;
using UnityEngine;

public class ScreenAnTheCaoThu : ScreenBase
{
	public UILabel lbTime;

	private GameObject NhanVat3D;

	public GameObject nhanVatAvatar3D;

	public UILabel lbLuotDanh;

	public GameObject grpVang;

	public UILabel lbPrice;

	public UILabel lbFree;

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(6);
		displayCaoThu3D();
		displayTime();
	}

	private void Update()
	{
		if (!(NhanVat3D != null))
		{
			return;
		}
		Avatar3D component = NhanVat3D.GetComponent<Avatar3D>();
		if (component.AvatarGO != null && component.AvatarGO.GetComponent<Animation>() != null && component.AvatarGO.GetComponent<Animation>().isPlaying)
		{
			if (component.AvatarGO.GetComponent<Animation>().cullingType != AnimationCullingType.AlwaysAnimate)
			{
				component.AvatarGO.GetComponent<Animation>().cullingType = AnimationCullingType.AlwaysAnimate;
			}
			if (component.AvatarGO.GetComponent<Animation>().IsPlaying("idle"))
			{
				component.AvatarGO.GetComponent<Animation>().wrapMode = WrapMode.Loop;
			}
			else
			{
				component.AvatarGO.GetComponent<Animation>().wrapMode = WrapMode.Default;
			}
		}
	}

	public void displayTime()
	{
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.AnTheCaoThuConfig != null)
		{
			UserInfo.ServerData.EventAnTheCaoThu anTheCaoThuConfig = GameManager.instance.m_GameClient.UserInfo.ServerInfo.AnTheCaoThuConfig;
			string arg = anTheCaoThuConfig.ThoiGianBatDau.Day + "/" + anTheCaoThuConfig.ThoiGianBatDau.Month + "/" + anTheCaoThuConfig.ThoiGianBatDau.Year;
			string arg2 = anTheCaoThuConfig.ThoiGianKetThuc.Day + "/" + anTheCaoThuConfig.ThoiGianKetThuc.Month + "/" + anTheCaoThuConfig.ThoiGianKetThuc.Year;
			lbTime.text = string.Format(Localization.instance.Get("ThoiGianDienRaSuKienLabel"), arg, arg2);
		}
	}

	public void displayCaoThu3D()
	{
		if (NhanVat3D != null)
		{
			Object.Destroy(NhanVat3D);
			NhanVat3D = null;
		}
		NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats["NV_MAI_HOA_DAO"];
		if (nhanVatCfg.VuKhiMacDinh.StartsWith("VK_") && !string.IsNullOrEmpty(nhanVatCfg.VuKhiMacDinh))
		{
			NhanVat3D = GUIManager.instance.InstantiateAvatar3DWithBattleAnim("NV_MAI_HOA_DAO", nhanVatCfg.VuKhiMacDinh, string.Empty, string.Empty, string.Empty).gameObject;
		}
		else
		{
			NhanVat3D = GUIManager.instance.InstantiateAvatar3DWithBattleAnim("NV_MAI_HOA_DAO", string.Empty, string.Empty, string.Empty, string.Empty).gameObject;
		}
		if (NhanVat3D != null)
		{
			NhanVat3D.transform.parent = nhanVatAvatar3D.transform;
			NhanVat3D.transform.localPosition = Vector3.zero;
			NhanVat3D.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			NhanVat3D.transform.localScale = Vector3.one;
			NhanVat3D.GetComponent<Avatar3D>().PlayAnimBattle("change", false);
			NhanVat3D.GetComponent<Avatar3D>().FadeQueuedAnimBattle("market", false, 0.3f);
			NhanVat3D.GetComponent<Avatar3D>().FadeQueuedAnimBattle("idle", false, 0.2f);
		}
		updateGia();
	}

	private void updateGia()
	{
		int soLuotQuayTrongNgay = getSoLuotQuayTrongNgay();
		if (soLuotQuayTrongNgay >= 3)
		{
			lbFree.gameObject.SetActive(false);
			grpVang.gameObject.SetActive(true);
			lbPrice.text = getGiaVang(soLuotQuayTrongNgay + 1).ToString();
		}
		else
		{
			lbFree.gameObject.SetActive(true);
			grpVang.gameObject.SetActive(false);
		}
	}

	private int getSoLuotQuayTrongNgay()
	{
		int result = 0;
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("ADCT"))
		{
			string[] array = GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Split(';');
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].StartsWith("ADCT"))
				{
					result = int.Parse(array[i].Substring(4, array[i].Length - 4));
				}
			}
		}
		return result;
	}

	private int getGiaVang(int luot)
	{
		if (luot <= 3)
		{
			return 0;
		}
		int giaKhoiDau = GameManager.instance.m_GameClient.UserInfo.ServerInfo.AnTheCaoThuConfig.GiaKhoiDau;
		int giaPlus = GameManager.instance.m_GameClient.UserInfo.ServerInfo.AnTheCaoThuConfig.GiaPlus;
		int num = luot - 3;
		int num2 = 0;
		num2 = ((num != 1) ? (giaKhoiDau + (num - 1) * giaPlus) : giaKhoiDau);
		if (num2 >= 100)
		{
			return 100;
		}
		return num2;
	}

	public void btnQua_OnClick()
	{
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.AnTheCaoThuConfig == null)
		{
			return;
		}
		UserInfo.ServerData.EventAnTheCaoThu anTheCaoThuConfig = GameManager.instance.m_GameClient.UserInfo.ServerInfo.AnTheCaoThuConfig;
		PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
		phanThuongResponse.PhanThuongList = new List<PhanThuongResponse.PhanThuong>();
		if (anTheCaoThuConfig.ListPhanThuongRandom != null && anTheCaoThuConfig.ListPhanThuongRandom.Count > 0)
		{
			for (int i = 0; i < anTheCaoThuConfig.ListPhanThuongRandom.Count; i++)
			{
				PhanThuongResponse.PhanThuong phanThuong = new PhanThuongResponse.PhanThuong();
				phanThuong = anTheCaoThuConfig.ListPhanThuongRandom[i].PhanThuong;
				phanThuongResponse.PhanThuongList.Add(phanThuong);
			}
		}
		if (phanThuongResponse.PhanThuongList.Count > 0)
		{
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTitle"), Localization.instance.Get("PopupPhanThuongADCTDesc"), phanThuongResponse);
		}
	}

	public void btnQuay_OnClick()
	{
		GameManager.instance.m_GameClient.RequestDanhAnDanhCaoThu();
	}

	public void updateMainMenuView()
	{
		GadgetPanelBottom gadgetPanelBottom = GUIManager.instance.gadgetPanelBottom;
		if (!(gadgetPanelBottom != null))
		{
			return;
		}
		gadgetPanelBottom.checkDisplayThongBaoSuKien();
		if (gadgetPanelBottom.listKyNgoMenu == null || gadgetPanelBottom.listKyNgoMenu.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < gadgetPanelBottom.listKyNgoMenu.Count; i++)
		{
			if (gadgetPanelBottom.listKyNgoMenu[i].kyNgoID == MenuButtonKyNgo.KyNgoType.BacMayMan)
			{
				if (gadgetPanelBottom.checkThongBaoBacMayMan())
				{
					gadgetPanelBottom.listKyNgoMenu[i].notifyIcon.gameObject.SetActive(true);
				}
				else
				{
					gadgetPanelBottom.listKyNgoMenu[i].notifyIcon.gameObject.SetActive(false);
				}
			}
		}
	}

	public void updateView()
	{
		updateGia();
	}
}
