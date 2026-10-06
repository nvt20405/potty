using UnityEngine;

public class CostumeGroup : MonoBehaviour
{
	public Transform Avatar3DObj;

	public UILabel labelName;

	public CostumeAvatar avatar;

	private Avatar3D avatar3D;

	private UserInfo _RefUserInfo;

	public GameObject btnCreate;

	public GameObject btnHelp;

	private void Start()
	{
		avatar.onAvatarClick = OnCostumeAvatarClick;
	}

	public void OnBackBtnClick()
	{
		ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
		screenDoiHinh.TurnOnNhanVatGroup();
	}

	public bool IsReadOnlyMode()
	{
		if (_RefUserInfo == null || _RefUserInfo.Gamer.ID == GameManager.instance.m_GameClient.UserInfo.Gamer.ID)
		{
			return false;
		}
		return true;
	}

	private void OnCreateBtnClick()
	{
		if (!IsReadOnlyMode())
		{
			GUIManager.setScreen(GAME_SCREEN.ScreenCreateCostume);
			ScreenCreateCostume screenCreateCostume = GUIManager.getScreen(GAME_SCREEN.ScreenCreateCostume) as ScreenCreateCostume;
			screenCreateCostume.SelectFirstCreationForNhanVat(avatar3D.CodeName);
		}
	}

	private void OnHelpBtnClick()
	{
		ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
		screenHelpInfo.setByLevel(0, 9);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
	}

	public void SetInfo(string costume, UserInfo.HeroData hero, UserInfo refUserInfo)
	{
		if (hero == null || refUserInfo == null)
		{
			return;
		}
		_RefUserInfo = refUserInfo;
		UserInfo.TrangBiData trangBiData = refUserInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.ID == hero.VuKhiID);
		UserInfo.VoCongData voCongData = refUserInfo.VoCongList.Find((UserInfo.VoCongData e) => e.ID == hero.VoCong3ID);
		UserInfo.VoCongData voCongData2 = refUserInfo.VoCongList.Find((UserInfo.VoCongData e) => e.ID == hero.VoCong4ID);
		if (avatar3D != null && (costume != avatar3D.CostumeName || avatar3D.CodeName != hero.Name))
		{
			avatar3D.gameObject.SetActive(false);
			Object.Destroy(avatar3D.gameObject);
			avatar3D = null;
		}
		if (avatar3D == null)
		{
			avatar3D = GUIManager.instance.InstantiateAvatar3DWithBattleAnim(hero.Name, (trangBiData != null) ? trangBiData.Name : string.Empty, (voCongData2 != null) ? voCongData2.Name : string.Empty, (voCongData != null) ? voCongData.Name : string.Empty, costume);
			avatar3D.transform.parent = Avatar3DObj;
			avatar3D.transform.localPosition = Vector3.zero;
			avatar3D.transform.localScale = Vector3.one;
			avatar3D.transform.localRotation = Quaternion.identity;
		}
		avatar3D.PlayAnimBattle("idle", true);
		CostumeCfg value = null;
		if (ConfigManager.instance.m_dicCostumeCfg.TryGetValue(costume, out value))
		{
			labelName.text = value.TenHienThi;
			UserInfo.CostumeData costumeData = null;
			if (refUserInfo.CostumeList != null)
			{
				costumeData = refUserInfo.CostumeList.Find((UserInfo.CostumeData e) => e.ID == hero.CostumeID);
			}
			if (costumeData != null)
			{
				avatar.SetInfo(costumeData.CodeName, costumeData.TinhLuyen, costumeData.HoangNgoc, costumeData.HongNgoc, costumeData.LamNgoc, costumeData.TuNgoc);
			}
			else
			{
				avatar.SetInfo(string.Empty);
			}
		}
		else if (string.IsNullOrEmpty(costume))
		{
			labelName.text = ConfigManager.instance.m_dicNhanVats[hero.Name].TenHienThi;
			avatar.SetInfo(string.Empty);
		}
		if (IsReadOnlyMode())
		{
			btnCreate.SetActive(false);
			btnHelp.SetActive(false);
		}
		else
		{
			btnCreate.SetActive(true);
			btnHelp.SetActive(true);
		}
	}

	private void OnCostumeAvatarClick(CostumeAvatar ava)
	{
		if (IsReadOnlyMode())
		{
			CostumeCfg value = null;
			if (!ConfigManager.instance.m_dicCostumeCfg.TryGetValue(ava.CodeName, out value))
			{
				return;
			}
			UserInfo.CostumeData costumeData = null;
			if (_RefUserInfo != null && _RefUserInfo.CostumeList != null)
			{
				costumeData = _RefUserInfo.CostumeList.Find((UserInfo.CostumeData e) => e.CodeName == ava.CodeName);
			}
			if (costumeData != null)
			{
				PopupCostume.Create(costumeData, false, true);
			}
			return;
		}
		CostumeCfg value2 = null;
		if (ConfigManager.instance.m_dicCostumeCfg.TryGetValue(ava.CodeName, out value2))
		{
			UserInfo.CostumeData costumeData2 = null;
			if (GameManager.instance.m_GameClient.UserInfo.CostumeList != null)
			{
				costumeData2 = GameManager.instance.m_GameClient.UserInfo.CostumeList.Find((UserInfo.CostumeData e) => e.CodeName == ava.CodeName);
			}
			if (costumeData2 != null)
			{
				PopupCostume.Create(costumeData2, false, false);
			}
		}
		else
		{
			ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
			UserInfo.HeroData currentHeroData = screenDoiHinh.GetCurrentHeroData();
			PopupSelectCostume.CreateForNhanVat(currentHeroData, OnFinishSelectCostume);
		}
	}

	private bool OnFinishSelectCostume(int costumeID, int hid)
	{
		if (hid > 0)
		{
			GameManager.instance.m_GameClient.RequestTakeOnCostume(costumeID, hid);
		}
		return true;
	}

	public void SetInfo(UserInfo.HeroData hero, UserInfo refUserInfo)
	{
		if (hero == null || refUserInfo == null)
		{
			return;
		}
		UserInfo.CostumeData costumeData = null;
		if (refUserInfo.CostumeList != null)
		{
			costumeData = refUserInfo.CostumeList.Find((UserInfo.CostumeData e) => e.ID == hero.CostumeID);
		}
		SetInfo((costumeData != null) ? costumeData.CodeName : string.Empty, hero, refUserInfo);
	}
}
