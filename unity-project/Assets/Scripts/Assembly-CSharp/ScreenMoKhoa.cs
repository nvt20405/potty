using UnityEngine;

public class ScreenMoKhoa : ScreenBase
{
	public UserInfo.VoCongData m_Data;

	public GameObject groupNormal;

	public GameObject groupResult;

	public OtherAvatar voCongResultAva;

	public OtherAvatar vocongAvatar;

	public OtherAvatar vatphamAvatar;

	public UILabel voCongName;

	public UILabel vatPhamName;

	public UILabel vatPhamQuantity;

	public UIButton btnUnlock;

	public static int SoLuongVPMatTichYeuCau = 100;

	private bool isUnlockVCDefault;

	private UserInfo.VatPhamTieuThuData vpData;

	public override void OnActive()
	{
		if (m_Data != null)
		{
			displayInfo();
		}
	}

	public void Set(UserInfo.VoCongData vcData, bool vcDefault = false)
	{
		if (vcData != null)
		{
			m_Data = vcData;
			isUnlockVCDefault = vcDefault;
		}
		else
		{
			m_Data = null;
		}
	}

	public void displayInfo()
	{
		groupNormal.gameObject.SetActive(true);
		groupResult.gameObject.SetActive(false);
		CfgVoCong cfgVoCong = ConfigManager.instance.m_dicVCs[m_Data.Name];
		vocongAvatar.Set(m_Data.Name, 0, m_Data.Level);
		voCongName.text = cfgVoCong.TenHienThi;
		VatPhamTieuThuCfg vatPhamTieuThuCfg = ConfigManager.instance.m_dicVatPhamTieuThu["VP_MAT_TIN_GIANG_HO"];
		vatPhamName.text = vatPhamTieuThuCfg.TenHienThi;
		vpData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_MAT_TIN_GIANG_HO");
		if (vpData != null)
		{
			vatphamAvatar.Set(vpData);
			vatPhamQuantity.text = vpData.Quantity + "/ " + SoLuongVPMatTichYeuCau;
			return;
		}
		vpData = new UserInfo.VatPhamTieuThuData();
		vpData.Name = "VP_MAT_TIN_GIANG_HO";
		vpData.Quantity = 0;
		vatphamAvatar.Set(vpData);
		vatPhamQuantity.text = "0/ " + SoLuongVPMatTichYeuCau;
	}

	public void btnBack_OnClick(GameObject go)
	{
		if (GUIManager.instance.LastScreen != GAME_SCREEN.ScreenVoCong && GUIManager.instance.LastScreen != GAME_SCREEN.ScreenHelpInfo)
		{
			GUIManager.instance.SetScreen(GUIManager.instance.LastScreen);
		}
		else
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenVoCong);
		}
	}

	public void btnMoKhoa_OnClick(GameObject go)
	{
		if (m_Data == null)
		{
			return;
		}
		if ((GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_MAT_TIN_GIANG_HO") && GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_MAT_TIN_GIANG_HO").Quantity < SoLuongVPMatTichYeuCau) || !GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_MAT_TIN_GIANG_HO"))
		{
			MessagePopup.Create(Localization.instance.Get("ThongBaoKhongDuMatTinGiangHo"));
			return;
		}
		UnLockVoCongRequest unLockVoCongRequest = new UnLockVoCongRequest();
		if (isUnlockVCDefault)
		{
			unLockVoCongRequest.DeTuID = m_Data.HID;
			unLockVoCongRequest.VoCongID = 0;
		}
		else
		{
			unLockVoCongRequest.VoCongID = m_Data.ID;
		}
		GameManager.instance.m_GameClient.RequestUnLockVoCong(unLockVoCongRequest);
	}

	public void btnHelp_OnClick()
	{
		ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
		screenHelpInfo.setByLevel(1, 3);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
		EGDebug.Log("btnHelp_OnClick");
	}

	public void updateView(UnLockVoCongResponse response)
	{
		if (response == null)
		{
			return;
		}
		groupNormal.gameObject.SetActive(false);
		groupResult.gameObject.SetActive(true);
		if (isUnlockVCDefault)
		{
			UserInfo.HeroData heroData = GameManager.instance.m_GameClient.UserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == m_Data.HID);
			if (heroData != null && heroData.UnlockVCDefault == 1)
			{
				m_Data.Name = response.VoCongName;
				m_Data.Level = heroData.VoCong1Level;
				m_Data.Unlock = heroData.UnlockVCDefault;
				voCongResultAva.Set(m_Data.Name, 0, m_Data.Level);
			}
		}
		else
		{
			UserInfo.VoCongData voCongData = GameManager.instance.m_GameClient.UserInfo.VoCongList.Find((UserInfo.VoCongData e) => e.Name == response.VoCongName && e.ID == m_Data.ID);
			if (voCongData != null)
			{
				m_Data = voCongData;
			}
			if (m_Data.Unlock == 1)
			{
				voCongResultAva.Set(response.VoCongName, 0, m_Data.Level);
			}
		}
	}

	public void avatar_OnClick()
	{
		if (m_Data != null)
		{
			PopupVoCong.CreateByNormalScreen(GameManager.instance.m_GameClient.UserInfo, m_Data, m_Data.Level);
		}
	}

	public void vpNeed_OnClick()
	{
		if (vpData != null)
		{
			PopUpVatPham.Create(vpData);
		}
	}
}
