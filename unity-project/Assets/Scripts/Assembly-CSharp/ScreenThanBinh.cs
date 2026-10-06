using System.Collections.Generic;
using UnityEngine;

public class ScreenThanBinh : ScreenBase
{
	public enum ThanBinhTab
	{
		TabKhaiQuang = 0,
		TabTayLuyen = 1,
		TabDungLuyen = 2
	}

	public ThanBinhTab m_Tab;

	public GameObject groupNormal;

	public UpgradeTrangBiTab groupUpgrade;

	public GameObject btnKhaiQuang;

	public GameObject btnTayLuyen;

	public GameObject btnDungLuyen;

	public UILabel lbTitle;

	public UILabel lbDescription;

	public UILabel lbInfoNormal;

	public GameObject chiSoGrp;

	public UILabel lbChiSo1;

	public UILabel lbChiSo2;

	public UILabel lbChiSo3;

	public GameObject goChiSo1;

	public GameObject goChiSo2;

	public GameObject goChiSo3;

	public UISprite iconNormal1;

	public UISprite iconNormal2;

	public UISprite iconNormal3;

	public OtherAvatar trangBiAvatar;

	public UserInfo.TrangBiData trangBiData;

	private void Awake()
	{
		UIAnchor[] componentsInChildren = GetComponentsInChildren<UIAnchor>();
		UIAnchor[] array = componentsInChildren;
		UIAnchor[] array2 = array;
		foreach (UIAnchor uIAnchor in array2)
		{
			uIAnchor.widgetContainer = GUIManager.instance.GameFrame;
		}
	}

	public override void OnActive()
	{
		base.OnActive();
		trangBiData = null;
		displayInfo();
	}

	public void displayInfo()
	{
		groupNormal.gameObject.SetActive(true);
		groupUpgrade.gameObject.SetActive(false);
		switch (m_Tab)
		{
		case ThanBinhTab.TabKhaiQuang:
			TurnOnKhaiQuangTab();
			break;
		case ThanBinhTab.TabTayLuyen:
			TurnOnTayLuyenTab();
			break;
		case ThanBinhTab.TabDungLuyen:
			TurnOnDungLuyenTab();
			break;
		default:
			TurnOnKhaiQuangTab();
			break;
		}
		getInfoTrangBi();
	}

	public void getInfoTrangBi()
	{
		if (trangBiData != null)
		{
			OnSelectTrangBi(trangBiData.ID);
			return;
		}
		trangBiAvatar.Set("plus");
		chiSoGrp.gameObject.SetActive(false);
	}

	private void onClick_KhaiQuangTab(bool isActive)
	{
		if (isActive && m_Tab != ThanBinhTab.TabKhaiQuang && NGUITools.GetActive(groupNormal))
		{
			m_Tab = ThanBinhTab.TabKhaiQuang;
			TurnOnKhaiQuangTab();
		}
	}

	private void onClick_TayLuyenTab(bool isActive)
	{
		if (isActive && m_Tab != ThanBinhTab.TabTayLuyen && NGUITools.GetActive(groupNormal))
		{
			m_Tab = ThanBinhTab.TabTayLuyen;
			TurnOnTayLuyenTab();
		}
	}

	private void onClick_DungLuyenTab(bool isActive)
	{
		if (isActive && m_Tab != ThanBinhTab.TabDungLuyen && NGUITools.GetActive(groupNormal))
		{
			m_Tab = ThanBinhTab.TabDungLuyen;
			TurnOnDungLuyenTab();
		}
	}

	public void TurnOnKhaiQuangTab()
	{
		lbTitle.text = Localization.instance.Get("ChonTrangBiKhaiQuangTitle");
		lbDescription.text = Localization.instance.Get("DescriptionKhaiQuang");
		lbInfoNormal.text = Localization.instance.Get("InfoKhaiQuang");
		btnDungLuyen.gameObject.SetActive(false);
		btnTayLuyen.gameObject.SetActive(false);
		btnKhaiQuang.gameObject.SetActive(true);
	}

	public void TurnOnTayLuyenTab()
	{
		lbTitle.text = Localization.instance.Get("ChonTrangBiTayLuyenTitle");
		lbDescription.text = Localization.instance.Get("DescriptionTayLuyen");
		if (GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_TAY_LUYEN_THACH"))
		{
			lbInfoNormal.text = string.Format(Localization.instance.Get("InfoTayLuyen"), GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_TAY_LUYEN_THACH").Quantity);
		}
		else
		{
			lbInfoNormal.text = string.Format(Localization.instance.Get("InfoTayLuyen"), 0);
		}
		btnDungLuyen.gameObject.SetActive(false);
		btnTayLuyen.gameObject.SetActive(true);
		btnKhaiQuang.gameObject.SetActive(false);
	}

	public void TurnOnDungLuyenTab()
	{
		lbTitle.text = Localization.instance.Get("ChonTrangBiDungLuyenTitle");
		lbDescription.text = Localization.instance.Get("DescriptionDungLuyen");
		if (GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_DUNG_LUYEN_THACH"))
		{
			lbInfoNormal.text = string.Format(Localization.instance.Get("InfoDungLuyen"), GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_DUNG_LUYEN_THACH").Quantity);
		}
		else
		{
			lbInfoNormal.text = string.Format(Localization.instance.Get("InfoDungLuyen"), 0);
		}
		btnDungLuyen.gameObject.SetActive(true);
		btnTayLuyen.gameObject.SetActive(false);
		btnKhaiQuang.gameObject.SetActive(false);
	}

	public void OnClick_TrangBiAva()
	{
		List<int> list = new List<int>();
		if (GameManager.instance.m_GameClient.UserInfo.TrangBiList != null && GameManager.instance.m_GameClient.UserInfo.TrangBiList.Count > 0)
		{
			for (int i = 0; i < GameManager.instance.m_GameClient.UserInfo.TrangBiList.Count; i++)
			{
				if (GameManager.instance.m_GameClient.UserInfo.TrangBiList[i] != null && GameManager.instance.m_GameClient.UserInfo.TrangBiList[i].TinhLuyenLevel < 3)
				{
					list.Add(GameManager.instance.m_GameClient.UserInfo.TrangBiList[i].ID);
				}
			}
		}
		PopupSelectTrangBi.Create(OnSelectTrangBi, list, LoaiTrangBi.None, Localization.instance.Get("PopupSelectTrangBiTitle"));
	}

	public bool OnSelectTrangBi(int id)
	{
		if (id <= 0)
		{
			return false;
		}
		if (!NGUITools.GetActive(chiSoGrp.gameObject))
		{
			chiSoGrp.gameObject.SetActive(true);
		}
		trangBiData = GameManager.instance.m_GameClient.UserInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.ID == id);
		if (trangBiData != null)
		{
			trangBiAvatar.Set(trangBiData);
			goChiSo1.gameObject.SetActive(false);
			goChiSo2.gameObject.SetActive(false);
			goChiSo3.gameObject.SetActive(false);
			if (trangBiData.Effect != null && trangBiData.Effect.Count > 0)
			{
				if (trangBiData.Effect[0] != null)
				{
					lbChiSo1.text = trangBiData.Effect[0].GetEffectColor() + getStringChiSo(trangBiData.Effect[0].LoaiEff, trangBiData.Effect[0].EffVal);
					iconNormal1.spriteName = "icon_active_than_binh_list";
				}
				else
				{
					lbChiSo1.text = Localization.instance.Get("ChuaMoHieuUngTrangBiLabel");
					iconNormal1.spriteName = "icon_than_binh_list";
				}
				if (trangBiData.Effect.Count > 1 && trangBiData.Effect[1] != null)
				{
					lbChiSo2.text = trangBiData.Effect[1].GetEffectColor() + getStringChiSo(trangBiData.Effect[1].LoaiEff, trangBiData.Effect[1].EffVal);
					iconNormal2.spriteName = "icon_active_than_binh_list";
				}
				else
				{
					lbChiSo2.text = Localization.instance.Get("ChuaMoHieuUngTrangBiLabel");
					iconNormal2.spriteName = "icon_than_binh_list";
				}
				if (trangBiData.Effect.Count > 2 && trangBiData.Effect[2] != null)
				{
					lbChiSo3.text = trangBiData.Effect[2].GetEffectColor() + getStringChiSo(trangBiData.Effect[2].LoaiEff, trangBiData.Effect[2].EffVal);
					iconNormal3.spriteName = "icon_active_than_binh_list";
				}
				else
				{
					lbChiSo3.text = Localization.instance.Get("ChuaMoHieuUngTrangBiLabel");
					iconNormal3.spriteName = "icon_than_binh_list";
				}
			}
			else
			{
				lbChiSo1.text = Localization.instance.Get("ChuaMoHieuUngTrangBiLabel");
				lbChiSo2.text = Localization.instance.Get("ChuaMoHieuUngTrangBiLabel");
				lbChiSo3.text = Localization.instance.Get("ChuaMoHieuUngTrangBiLabel");
				iconNormal1.spriteName = "icon_than_binh_list";
				iconNormal2.spriteName = "icon_than_binh_list";
				iconNormal3.spriteName = "icon_than_binh_list";
			}
			iconNormal1.MakePixelPerfect();
			iconNormal2.MakePixelPerfect();
			iconNormal3.MakePixelPerfect();
			TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[trangBiData.Name];
			if (trangBiCfg.Hang == ItemClass.Binh)
			{
				goChiSo1.gameObject.SetActive(true);
			}
			else if (trangBiCfg.Hang == ItemClass.At)
			{
				goChiSo1.gameObject.SetActive(true);
				goChiSo2.gameObject.SetActive(true);
			}
			else if (trangBiCfg.Hang == ItemClass.Giap)
			{
				goChiSo1.gameObject.SetActive(true);
				goChiSo2.gameObject.SetActive(true);
				goChiSo3.gameObject.SetActive(true);
			}
		}
		return true;
	}

	private string getStringChiSo(UserInfo.TrangBiData.TRANG_BI_EFF tRANG_BI_EFF, float p)
	{
		string result = string.Empty;
		switch (tRANG_BI_EFF)
		{
		case UserInfo.TrangBiData.TRANG_BI_EFF.TANG_CONG:
			result = string.Format(Localization.instance.Get("TRANG_BI_EFF_TANG_CONG_DES"), p);
			break;
		case UserInfo.TrangBiData.TRANG_BI_EFF.TANG_MAU:
			result = string.Format(Localization.instance.Get("TRANG_BI_EFF_TANG_MAU_DES"), p);
			break;
		case UserInfo.TrangBiData.TRANG_BI_EFF.TANG_THU:
			result = string.Format(Localization.instance.Get("TRANG_BI_EFF_TANG_THU_DES"), p);
			break;
		case UserInfo.TrangBiData.TRANG_BI_EFF.TANG_NOI:
			result = string.Format(Localization.instance.Get("TRANG_BI_EFF_TANG_NOI_DES"), p);
			break;
		case UserInfo.TrangBiData.TRANG_BI_EFF.CHINH_XAC:
			result = string.Format(Localization.instance.Get("TRANG_BI_EFF_CHINH_XAC_DES"), p);
			break;
		case UserInfo.TrangBiData.TRANG_BI_EFF.KHANG_BAO:
			result = string.Format(Localization.instance.Get("TRANG_BI_EFF_KHANG_BAO_DES"), p);
			break;
		case UserInfo.TrangBiData.TRANG_BI_EFF.STUN:
			result = string.Format(Localization.instance.Get("TRANG_BI_EFF_STUN_DES"), p);
			break;
		case UserInfo.TrangBiData.TRANG_BI_EFF.PHONG_CHIEU:
			result = string.Format(Localization.instance.Get("TRANG_BI_EFF_PHONG_CHIEU_DES"), p);
			break;
		case UserInfo.TrangBiData.TRANG_BI_EFF.DINH_THAN:
			result = string.Format(Localization.instance.Get("TRANG_BI_EFF_DINH_THAN_DES"), p);
			break;
		case UserInfo.TrangBiData.TRANG_BI_EFF.BA_THE:
			result = string.Format(Localization.instance.Get("TRANG_BI_EFF_BA_THE_DES"), p);
			break;
		case UserInfo.TrangBiData.TRANG_BI_EFF.HO_THE:
			result = string.Format(Localization.instance.Get("TRANG_BI_EFF_HO_THE_DES"), p);
			break;
		case UserInfo.TrangBiData.TRANG_BI_EFF.HUT_MAU:
			result = string.Format(Localization.instance.Get("TRANG_BI_EFF_HUT_MAU_DES"), p);
			break;
		case UserInfo.TrangBiData.TRANG_BI_EFF.HOI_SINH:
			result = string.Format(Localization.instance.Get("TRANG_BI_EFF_HOI_SINH_DES"), p);
			break;
		case UserInfo.TrangBiData.TRANG_BI_EFF.BURN_MANA:
			result = string.Format(Localization.instance.Get("TRANG_BI_EFF_BURN_MANA_DES"), p);
			break;
		}
		return result;
	}

	public void btnKhaiQuang_OnClick()
	{
		if (trangBiData != null)
		{
			PopUpKhaiQuangTrangBi.Create(trangBiData);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("ThongBaoChuaChonTrangBiThanBinh"));
		}
	}

	public void btnDungLuyen_OnClick()
	{
		if (trangBiData != null && trangBiData.Effect != null && trangBiData.Effect.Count > 0)
		{
			GameManager.instance.m_GameClient.RequestDungLuyenTrangBi(trangBiData.ID);
		}
		else if (trangBiData.Effect == null || trangBiData.Effect.Count == 0)
		{
			MessagePopup.Create(Localization.instance.Get("CanKhaiQuangTruocKhiDungTayLuyen"));
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("ThongBaoChuaChonTrangBiThanBinh"));
		}
	}

	public void btnTayLuyen_OnClick()
	{
		if (trangBiData != null && trangBiData.Effect != null && trangBiData.Effect.Count > 0)
		{
			GameManager.instance.m_GameClient.RequestTayLuyenTrangBi(trangBiData.ID);
		}
		else if (trangBiData.Effect == null || trangBiData.Effect.Count == 0)
		{
			MessagePopup.Create(Localization.instance.Get("CanKhaiQuangTruocKhiDungTayLuyen"));
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("ThongBaoChuaChonTrangBiThanBinh"));
		}
	}

	public void btnConfirm_OnClick()
	{
		GameManager.instance.m_GameClient.RequestConfirmThanBinhTrangBi();
	}

	public void onClick_btnClose()
	{
		displayInfo();
	}

	public void OnUpdateInfo()
	{
		if (trangBiData != null)
		{
			trangBiData = GameManager.instance.m_GameClient.UserInfo.TrangBiList.Find((UserInfo.TrangBiData tb) => tb.ID == trangBiData.ID);
			displayInfo();
		}
	}

	public void OnShowConfirmGrp(UserInfo.TrangBiData newTrangbi, bool isTayLuyen)
	{
		groupNormal.gameObject.SetActive(false);
		groupUpgrade.SetData(trangBiData, newTrangbi, isTayLuyen);
		groupUpgrade.gameObject.SetActive(true);
	}
}
