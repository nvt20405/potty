using UnityEngine;

public class UpgradeTrangBiTab : MonoBehaviour
{
	public UILabel lbTitle;

	public UILabel lbDescription;

	public UILabel lbHienCo;

	public UILabel lbChiso1_Now;

	public UILabel lbChiso2_Now;

	public UILabel lbChiso3_Now;

	public GameObject goChiSo1_Now;

	public GameObject goChiSo2_Now;

	public GameObject goChiSo3_Now;

	public UILabel lbChiso1_New;

	public UILabel lbChiso2_New;

	public UILabel lbChiso3_New;

	public GameObject goChiSo1_New;

	public GameObject goChiSo2_New;

	public GameObject goChiSo3_New;

	public UISprite iconUpgradeNow1;

	public UISprite iconUpgradeNow2;

	public UISprite iconUpgradeNow3;

	public UISprite iconUpgradeNew1;

	public UISprite iconUpgradeNew2;

	public UISprite iconUpgradeNew3;

	public UILabel lbActionBtn;

	private bool m_isTayLuyen;

	private UserInfo.TrangBiData m_TrangBiData;

	private UserInfo.TrangBiData m_TrangBiDataNew;

	public void SetData(UserInfo.TrangBiData trangBiData, UserInfo.TrangBiData newTrangBiData, bool isTayLuyen)
	{
		m_TrangBiData = trangBiData;
		m_TrangBiDataNew = newTrangBiData;
		m_isTayLuyen = isTayLuyen;
		if (m_isTayLuyen)
		{
			lbActionBtn.text = Localization.instance.Get("TayLuyenBtnLabel");
			lbTitle.text = Localization.instance.Get("ThayDoiHieuUngKhaiQuangTitle");
			lbDescription.text = Localization.instance.Get("PopupTayLuyenDes");
			if (GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_TAY_LUYEN_THACH"))
			{
				lbHienCo.text = string.Format(Localization.instance.Get("HienCoLabelPopUpKhaiQuang"), GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_TAY_LUYEN_THACH").Quantity);
			}
			else
			{
				lbHienCo.text = string.Format(Localization.instance.Get("HienCoLabelPopUpKhaiQuang"), 0);
			}
		}
		else
		{
			lbActionBtn.text = Localization.instance.Get("DungLuyenBtnLabel");
			lbTitle.text = Localization.instance.Get("NhangCapHieuUngKhaiQuangLabel");
			lbDescription.text = Localization.instance.Get("PopupDungLuyenDes");
			if (GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_DUNG_LUYEN_THACH"))
			{
				lbHienCo.text = string.Format(Localization.instance.Get("HienCoLabelPopUpKhaiQuang"), GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_DUNG_LUYEN_THACH").Quantity);
			}
			else
			{
				lbHienCo.text = string.Format(Localization.instance.Get("HienCoLabelPopUpKhaiQuang"), 0);
			}
		}
		loadCurrentValue();
		loadNewValue();
	}

	private void loadCurrentValue()
	{
		if (m_TrangBiData == null)
		{
			return;
		}
		if (m_TrangBiData.Effect != null && m_TrangBiData.Effect.Count > 0)
		{
			if (m_TrangBiData.Effect[0] != null)
			{
				lbChiso1_Now.text = m_TrangBiData.Effect[0].GetEffectColor() + getStringChiSo(m_TrangBiData.Effect[0].LoaiEff, m_TrangBiData.Effect[0].EffVal);
				iconUpgradeNow1.spriteName = "icon_active_than_binh_list";
			}
			else
			{
				lbChiso1_Now.text = Localization.instance.Get("ChuaMoHieuUngTrangBiLabel");
				iconUpgradeNow1.spriteName = "icon_than_binh_list";
			}
			if (m_TrangBiData.Effect.Count > 1 && m_TrangBiData.Effect[1] != null)
			{
				lbChiso2_Now.text = m_TrangBiData.Effect[1].GetEffectColor() + getStringChiSo(m_TrangBiData.Effect[1].LoaiEff, m_TrangBiData.Effect[1].EffVal);
				iconUpgradeNow2.spriteName = "icon_active_than_binh_list";
			}
			else
			{
				lbChiso2_Now.text = Localization.instance.Get("ChuaMoHieuUngTrangBiLabel");
				iconUpgradeNow2.spriteName = "icon_than_binh_list";
			}
			if (m_TrangBiData.Effect.Count > 2 && m_TrangBiData.Effect[2] != null)
			{
				lbChiso3_Now.text = m_TrangBiData.Effect[2].GetEffectColor() + getStringChiSo(m_TrangBiData.Effect[2].LoaiEff, m_TrangBiData.Effect[2].EffVal);
				iconUpgradeNow3.spriteName = "icon_active_than_binh_list";
			}
			else
			{
				lbChiso3_Now.text = Localization.instance.Get("ChuaMoHieuUngTrangBiLabel");
				iconUpgradeNow3.spriteName = "icon_than_binh_list";
			}
		}
		else
		{
			lbChiso1_Now.text = Localization.instance.Get("ChuaMoHieuUngTrangBiLabel");
			lbChiso2_Now.text = Localization.instance.Get("ChuaMoHieuUngTrangBiLabel");
			lbChiso3_Now.text = Localization.instance.Get("ChuaMoHieuUngTrangBiLabel");
			iconUpgradeNow1.spriteName = "icon_than_binh_list";
			iconUpgradeNow2.spriteName = "icon_than_binh_list";
			iconUpgradeNow3.spriteName = "icon_than_binh_list";
		}
		iconUpgradeNow1.MakePixelPerfect();
		iconUpgradeNow2.MakePixelPerfect();
		iconUpgradeNow3.MakePixelPerfect();
		goChiSo1_Now.gameObject.SetActive(false);
		goChiSo2_Now.gameObject.SetActive(false);
		goChiSo3_Now.gameObject.SetActive(false);
		TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[m_TrangBiData.Name];
		if (trangBiCfg.Hang == ItemClass.Binh)
		{
			goChiSo1_Now.gameObject.SetActive(true);
		}
		else if (trangBiCfg.Hang == ItemClass.At)
		{
			goChiSo1_Now.gameObject.SetActive(true);
			goChiSo2_Now.gameObject.SetActive(true);
		}
		else if (trangBiCfg.Hang == ItemClass.Giap)
		{
			goChiSo1_Now.gameObject.SetActive(true);
			goChiSo2_Now.gameObject.SetActive(true);
			goChiSo3_Now.gameObject.SetActive(true);
		}
	}

	private void loadNewValue()
	{
		if (m_TrangBiDataNew == null)
		{
			return;
		}
		if (m_TrangBiDataNew.Effect != null && m_TrangBiDataNew.Effect.Count > 0)
		{
			if (m_TrangBiDataNew.Effect[0] != null)
			{
				lbChiso1_New.text = m_TrangBiDataNew.Effect[0].GetEffectColor() + getStringChiSo(m_TrangBiDataNew.Effect[0].LoaiEff, m_TrangBiDataNew.Effect[0].EffVal);
				iconUpgradeNew1.spriteName = "icon_active_than_binh_list";
			}
			else
			{
				lbChiso1_New.text = Localization.instance.Get("ChuaMoHieuUngTrangBiLabel");
				iconUpgradeNew1.spriteName = "icon_than_binh_list";
			}
			if (m_TrangBiDataNew.Effect.Count > 1 && m_TrangBiDataNew.Effect[1] != null)
			{
				lbChiso2_New.text = m_TrangBiDataNew.Effect[1].GetEffectColor() + getStringChiSo(m_TrangBiDataNew.Effect[1].LoaiEff, m_TrangBiDataNew.Effect[1].EffVal);
				iconUpgradeNew2.spriteName = "icon_active_than_binh_list";
			}
			else
			{
				lbChiso2_New.text = Localization.instance.Get("ChuaMoHieuUngTrangBiLabel");
				iconUpgradeNew2.spriteName = "icon_than_binh_list";
			}
			if (m_TrangBiDataNew.Effect.Count > 2 && m_TrangBiDataNew.Effect[2] != null)
			{
				lbChiso3_New.text = m_TrangBiDataNew.Effect[2].GetEffectColor() + getStringChiSo(m_TrangBiDataNew.Effect[2].LoaiEff, m_TrangBiDataNew.Effect[2].EffVal);
				iconUpgradeNew3.spriteName = "icon_active_than_binh_list";
			}
			else
			{
				lbChiso3_New.text = Localization.instance.Get("ChuaMoHieuUngTrangBiLabel");
				iconUpgradeNew3.spriteName = "icon_than_binh_list";
			}
		}
		else
		{
			lbChiso1_New.text = Localization.instance.Get("ChuaMoHieuUngTrangBiLabel");
			lbChiso2_New.text = Localization.instance.Get("ChuaMoHieuUngTrangBiLabel");
			lbChiso3_New.text = Localization.instance.Get("ChuaMoHieuUngTrangBiLabel");
			iconUpgradeNew1.MakePixelPerfect();
			iconUpgradeNew2.MakePixelPerfect();
			iconUpgradeNew3.MakePixelPerfect();
		}
		goChiSo1_New.gameObject.SetActive(false);
		goChiSo2_New.gameObject.SetActive(false);
		goChiSo3_New.gameObject.SetActive(false);
		TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[m_TrangBiDataNew.Name];
		if (trangBiCfg.Hang == ItemClass.Binh)
		{
			goChiSo1_New.gameObject.SetActive(true);
		}
		else if (trangBiCfg.Hang == ItemClass.At)
		{
			goChiSo1_New.gameObject.SetActive(true);
			goChiSo2_New.gameObject.SetActive(true);
		}
		else if (trangBiCfg.Hang == ItemClass.Giap)
		{
			goChiSo1_New.gameObject.SetActive(true);
			goChiSo2_New.gameObject.SetActive(true);
			goChiSo3_New.gameObject.SetActive(true);
		}
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
}
