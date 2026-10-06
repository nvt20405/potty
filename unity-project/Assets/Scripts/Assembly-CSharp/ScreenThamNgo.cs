using System.Collections.Generic;
using UnityEngine;

public class ScreenThamNgo : ScreenBase
{
	public UserInfo.VoCongData m_Data;

	public OtherAvatar voCongResultAvatar;

	public UILabel voCongName;

	public UILabel voCongNameResult;

	public OtherAvatar currentAvaCanSelected;

	public List<OtherAvatar> listAvatar;

	private Dictionary<OtherAvatar, UserInfo.VoCongData> mDicItemSelected;

	public UILabel lbTyLe;

	public UILabel lbResultThanhCong;

	public UILabel lbResultThatBai;

	public UILabel lbResultMaxLevel;

	public UIButton btnThamNgo;

	private float tyle;

	private bool isThamNgoVCDefault;

	public GameObject normalGroup;

	public GameObject resultGroup;

	public GameObject autoThamNgoGroup;

	public GameObject dieuKienTNGroup;

	public OtherAvatar vcBeforeAvatar;

	public OtherAvatar vcAfterAvatar;

	public GameObject m_AnimResult;

	public UILabel lbTieuHao1;

	public UILabel lbTieuHao5;

	public UILabel lbTyLeSuccess1;

	public UILabel lbTyLeSuccess5;

	private ThamNgoVoCongRequest requestAutoThamNgo1;

	private ThamNgoVoCongRequest requestAutoThamNgo5;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Set(UserInfo.VoCongData data, bool vcDefault = false)
	{
		if (data != null)
		{
			m_Data = data;
			isThamNgoVCDefault = vcDefault;
		}
	}

	public override void OnActive()
	{
		mDicItemSelected = new Dictionary<OtherAvatar, UserInfo.VoCongData>();
		m_AnimResult.SetActive(false);
		if (m_Data != null)
		{
			displayInfo();
		}
	}

	public void displayInfo()
	{
		CfgVoCong cfgVoCong = ConfigManager.instance.m_dicVCs[m_Data.Name];
		vcBeforeAvatar.Set(m_Data.Name, 0, m_Data.Level);
		vcAfterAvatar.Set(m_Data.Name, 0, m_Data.Level + 1);
		voCongResultAvatar.Set(m_Data.Name, 0, m_Data.Level);
		voCongName.text = cfgVoCong.TenHienThi;
		voCongNameResult.text = cfgVoCong.TenHienThi;
		for (int i = 0; i < listAvatar.Count; i++)
		{
			listAvatar[i].Set("plus");
			listAvatar[i].transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
		}
		if (m_Data.Level >= 10)
		{
			dieuKienTNGroup.gameObject.SetActive(false);
			normalGroup.gameObject.SetActive(false);
			resultGroup.gameObject.SetActive(true);
			autoThamNgoGroup.gameObject.SetActive(false);
			lbResultMaxLevel.gameObject.SetActive(true);
			lbResultThanhCong.gameObject.SetActive(false);
			lbResultThatBai.gameObject.SetActive(false);
		}
		else
		{
			lbTyLe.text = string.Empty;
			dieuKienTNGroup.gameObject.SetActive(true);
			resultGroup.gameObject.SetActive(false);
			normalGroup.gameObject.SetActive(true);
			autoThamNgoGroup.gameObject.SetActive(false);
		}
		mDicItemSelected.Clear();
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

	public void avatarCan_OnClick(GameObject go)
	{
		if (m_Data == null || m_Data.Level < 10)
		{
			currentAvaCanSelected = go.transform.GetComponent<OtherAvatar>();
			List<int> ignoreVoCongIDList = getIgnoreVoCongIDList();
			List<int> listVoCongSelectedID = getListVoCongSelectedID();
			PopUpSelectMultiVoCong.Create(OnSelectedAva, ignoreVoCongIDList, listVoCongSelectedID, VCClass.ALL, 5, string.Empty, true);
		}
	}

	public bool OnSelectedAva(List<int> ListVoCongID)
	{
		for (int i = 0; i < listAvatar.Count; i++)
		{
			if (i < ListVoCongID.Count)
			{
				if (ListVoCongID[i] > 0)
				{
					setAvatarSelected(ListVoCongID[i], listAvatar[i]);
				}
			}
			else
			{
				setAvatarSelected(0, listAvatar[i]);
			}
		}
		if (mDicItemSelected.Count > 0)
		{
			List<UserInfo.VoCongData> list = new List<UserInfo.VoCongData>();
			foreach (KeyValuePair<OtherAvatar, UserInfo.VoCongData> item in mDicItemSelected)
			{
				list.Add(item.Value);
			}
			tyle = ConfigManager.instance.GetTileThamNgo(m_Data, list);
			tyle *= 100f;
			if (tyle > 1f)
			{
				tyle = Mathf.Floor(tyle);
			}
			lbTyLe.text = Localization.instance.Get("TyLe") + ": " + tyle + "%";
		}
		else
		{
			lbTyLe.text = Localization.instance.Get("TyLe") + ": " + 0 + "%";
		}
		return true;
	}

	public void setAvatarSelected(int vo_cong_id, OtherAvatar avatar)
	{
		UserInfo.VoCongData voCongData = null;
		if (GameManager.instance.m_GameClient.UserInfo.VoCongList == null || GameManager.instance.m_GameClient.UserInfo.VoCongList.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < GameManager.instance.m_GameClient.UserInfo.VoCongList.Count; i++)
		{
			UserInfo.VoCongData voCongData2 = GameManager.instance.m_GameClient.UserInfo.VoCongList[i];
			if (voCongData2.ID == vo_cong_id)
			{
				voCongData = voCongData2;
			}
		}
		if (voCongData != null)
		{
			if (mDicItemSelected.ContainsKey(avatar))
			{
				mDicItemSelected.Remove(avatar);
			}
			mDicItemSelected.Add(avatar, voCongData);
			avatar.Set(voCongData.Name);
		}
		else
		{
			if (mDicItemSelected.ContainsKey(avatar))
			{
				mDicItemSelected.Remove(avatar);
			}
			avatar.Set("plus");
		}
		avatar.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
	}

	public List<int> getIgnoreVoCongIDList()
	{
		List<int> list = new List<int>();
		list.Add(m_Data.ID);
		for (int i = 0; i < GameManager.instance.m_GameClient.UserInfo.VoCongList.Count; i++)
		{
			UserInfo.VoCongData voCongData = GameManager.instance.m_GameClient.UserInfo.VoCongList[i];
			if (voCongData.HID > 0)
			{
				list.Add(voCongData.ID);
			}
		}
		return list;
	}

	public List<int> getListVoCongSelectedID()
	{
		List<int> list = new List<int>();
		if (mDicItemSelected.Count > 0)
		{
			foreach (KeyValuePair<OtherAvatar, UserInfo.VoCongData> item in mDicItemSelected)
			{
				list.Add(item.Value.ID);
			}
		}
		return list;
	}

	public void btnThamNgo_OnClick(GameObject go)
	{
		if (m_Data == null)
		{
			return;
		}
		if (mDicItemSelected != null && mDicItemSelected.Count > 0)
		{
			List<int> list = new List<int>();
			foreach (KeyValuePair<OtherAvatar, UserInfo.VoCongData> item in mDicItemSelected)
			{
				list.Add(item.Value.ID);
			}
			ThamNgoVoCongRequest thamNgoVoCongRequest = new ThamNgoVoCongRequest();
			if (isThamNgoVCDefault)
			{
				thamNgoVoCongRequest.DeTuID = m_Data.HID;
				thamNgoVoCongRequest.VoCongID = 0;
			}
			else
			{
				thamNgoVoCongRequest.VoCongID = m_Data.ID;
			}
			thamNgoVoCongRequest.listVoCongSuDung = list;
			GameManager.instance.m_GameClient.RequestThamNgoVoCong(thamNgoVoCongRequest);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("ChuaChonVoCongThamNgoMess"));
		}
	}

	public void btnAutoThamNgo_OnClick(GameObject go)
	{
		autoThamNgoGroup.gameObject.SetActive(true);
		dieuKienTNGroup.gameObject.SetActive(false);
		requestAutoThamNgo1 = null;
		requestAutoThamNgo5 = null;
		displayInfoAutoThamNgo();
	}

	public void autoThamNgo1_OnClick(GameObject go)
	{
		if (requestAutoThamNgo1 != null)
		{
			GameManager.instance.m_GameClient.RequestThamNgoVoCong(requestAutoThamNgo1);
		}
	}

	public void autoThamNgo5_OnClick(GameObject go)
	{
		if (requestAutoThamNgo5 != null)
		{
			GameManager.instance.m_GameClient.RequestThamNgoVoCong(requestAutoThamNgo5);
		}
	}

	public void displayInfoAutoThamNgo()
	{
		List<UserInfo.VoCongData> listVoCongAutoThamNgo = getListVoCongAutoThamNgo();
		lbTieuHao1.text = string.Format(Localization.instance.Get("TieuHaoBiKipThamNgo"), 1);
		lbTieuHao5.text = string.Format(Localization.instance.Get("TieuHaoBiKipThamNgo"), 5);
		if (listVoCongAutoThamNgo != null)
		{
			if (listVoCongAutoThamNgo.Count > 0)
			{
				List<UserInfo.VoCongData> list = new List<UserInfo.VoCongData>();
				requestAutoThamNgo1 = new ThamNgoVoCongRequest();
				list.Add(listVoCongAutoThamNgo[0]);
				if (isThamNgoVCDefault)
				{
					requestAutoThamNgo1.DeTuID = m_Data.HID;
					requestAutoThamNgo1.VoCongID = 0;
				}
				else
				{
					requestAutoThamNgo1.VoCongID = m_Data.ID;
				}
				if (requestAutoThamNgo1.listVoCongSuDung != null)
				{
					requestAutoThamNgo1.listVoCongSuDung.Clear();
					requestAutoThamNgo1.listVoCongSuDung.Add(listVoCongAutoThamNgo[0].ID);
				}
				else
				{
					requestAutoThamNgo1.listVoCongSuDung = new List<int>();
					requestAutoThamNgo1.listVoCongSuDung.Add(listVoCongAutoThamNgo[0].ID);
				}
				float tileThamNgo = ConfigManager.instance.GetTileThamNgo(m_Data, list);
				tileThamNgo *= 100f;
				if (tileThamNgo > 1f)
				{
					tileThamNgo = Mathf.Floor(tileThamNgo);
				}
				lbTyLeSuccess1.text = string.Format(Localization.instance.Get("TyLeThanhCongThamNgo"), tileThamNgo);
			}
			else
			{
				lbTyLeSuccess1.text = Localization.instance.Get("KhongDuBiKipThamNgoAuto");
			}
			if (listVoCongAutoThamNgo.Count >= 5)
			{
				List<UserInfo.VoCongData> list2 = new List<UserInfo.VoCongData>();
				requestAutoThamNgo5 = new ThamNgoVoCongRequest();
				list2.Add(listVoCongAutoThamNgo[0]);
				list2.Add(listVoCongAutoThamNgo[1]);
				list2.Add(listVoCongAutoThamNgo[2]);
				list2.Add(listVoCongAutoThamNgo[3]);
				list2.Add(listVoCongAutoThamNgo[4]);
				if (isThamNgoVCDefault)
				{
					requestAutoThamNgo5.DeTuID = m_Data.HID;
					requestAutoThamNgo5.VoCongID = 0;
				}
				else
				{
					requestAutoThamNgo5.VoCongID = m_Data.ID;
				}
				if (requestAutoThamNgo5.listVoCongSuDung != null)
				{
					requestAutoThamNgo5.listVoCongSuDung.Clear();
					requestAutoThamNgo5.listVoCongSuDung.Add(listVoCongAutoThamNgo[0].ID);
					requestAutoThamNgo5.listVoCongSuDung.Add(listVoCongAutoThamNgo[1].ID);
					requestAutoThamNgo5.listVoCongSuDung.Add(listVoCongAutoThamNgo[2].ID);
					requestAutoThamNgo5.listVoCongSuDung.Add(listVoCongAutoThamNgo[3].ID);
					requestAutoThamNgo5.listVoCongSuDung.Add(listVoCongAutoThamNgo[4].ID);
				}
				else
				{
					requestAutoThamNgo5.listVoCongSuDung = new List<int>();
					requestAutoThamNgo5.listVoCongSuDung.Add(listVoCongAutoThamNgo[0].ID);
					requestAutoThamNgo5.listVoCongSuDung.Add(listVoCongAutoThamNgo[1].ID);
					requestAutoThamNgo5.listVoCongSuDung.Add(listVoCongAutoThamNgo[2].ID);
					requestAutoThamNgo5.listVoCongSuDung.Add(listVoCongAutoThamNgo[3].ID);
					requestAutoThamNgo5.listVoCongSuDung.Add(listVoCongAutoThamNgo[4].ID);
				}
				float tileThamNgo2 = ConfigManager.instance.GetTileThamNgo(m_Data, list2);
				tileThamNgo2 *= 100f;
				if (tileThamNgo2 > 1f)
				{
					tileThamNgo2 = Mathf.Floor(tileThamNgo2);
				}
				lbTyLeSuccess5.text = string.Format(Localization.instance.Get("TyLeThanhCongThamNgo"), tileThamNgo2);
			}
			else
			{
				lbTyLeSuccess5.text = Localization.instance.Get("KhongDuBiKipThamNgoAuto");
			}
		}
		else
		{
			lbTyLeSuccess1.text = Localization.instance.Get("KhongDuBiKipThamNgoAuto");
			lbTyLeSuccess5.text = Localization.instance.Get("KhongDuBiKipThamNgoAuto");
		}
	}

	public List<UserInfo.VoCongData> getListVoCongAutoThamNgo()
	{
		List<int> ignoreVoCongIDList = getIgnoreVoCongIDList();
		List<UserInfo.VoCongData> list = new List<UserInfo.VoCongData>();
		if (GameManager.instance.m_GameClient.UserInfo.VoCongList != null && GameManager.instance.m_GameClient.UserInfo.VoCongList.Count > 0)
		{
			foreach (UserInfo.VoCongData voCong in GameManager.instance.m_GameClient.UserInfo.VoCongList)
			{
				CfgVoCong value;
				if (ConfigManager.instance.m_dicVCs.TryGetValue(voCong.Name, out value) && ((value.Hang == 1 && (value.m_Class == VCClass.NOI_CONG || value.m_Class == VCClass.BO_PHAP || value.m_Class == VCClass.CHIEU_THUC)) || (value.Hang == 2 && value.m_Class == VCClass.CHIEU_THUC)) && (ignoreVoCongIDList == null || !ignoreVoCongIDList.Contains(voCong.ID)))
				{
					list.Add(voCong);
				}
			}
			list.Sort((UserInfo.VoCongData x, UserInfo.VoCongData y) => CompareVoCongThamNgo(y.Name, y.Level, x.Name, x.Level));
		}
		return list;
	}

	public void updateScreenView(ThamNgoVoCongResponse response)
	{
		startPlayAnim();
		normalGroup.gameObject.SetActive(false);
		resultGroup.gameObject.SetActive(true);
		autoThamNgoGroup.gameObject.SetActive(false);
		dieuKienTNGroup.gameObject.SetActive(false);
		if (isThamNgoVCDefault)
		{
			UserInfo.HeroData heroData = GameManager.instance.m_GameClient.UserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == m_Data.HID);
			if (heroData != null)
			{
				m_Data.Name = response.VoCongName;
				m_Data.Level = heroData.VoCong1Level;
				m_Data.ThamNgoExp = heroData.VoCong1ThamNgoExp;
				voCongResultAvatar.Set(m_Data.Name, 0, m_Data.Level);
			}
		}
		else
		{
			UserInfo.VoCongData voCongData = GameManager.instance.m_GameClient.UserInfo.VoCongList.Find((UserInfo.VoCongData e) => e.Name == response.VoCongName && e.ID == m_Data.ID);
			if (voCongData != null)
			{
				m_Data = voCongData;
			}
			voCongResultAvatar.Set(response.VoCongName, 0, response.NewLevel);
		}
		for (int num = 0; num < listAvatar.Count; num++)
		{
			listAvatar[num].Set("plus");
			listAvatar[num].transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
		}
		if (response.NewLevel >= 10)
		{
			lbResultMaxLevel.gameObject.SetActive(true);
			lbResultThanhCong.gameObject.SetActive(false);
			lbResultThatBai.gameObject.SetActive(false);
		}
		else if (response.NewLevel > response.OldLevel)
		{
			lbResultThanhCong.gameObject.SetActive(true);
			lbResultThatBai.gameObject.SetActive(false);
			lbResultMaxLevel.gameObject.SetActive(false);
		}
		else
		{
			lbResultThatBai.gameObject.SetActive(true);
			lbResultMaxLevel.gameObject.SetActive(false);
			lbResultThanhCong.gameObject.SetActive(false);
		}
		mDicItemSelected.Clear();
	}

	public void vcAvatarAfter_OnClick()
	{
		if (m_Data != null)
		{
			PopupVoCong.CreateByNormalScreen(GameManager.instance.m_GameClient.UserInfo, m_Data, m_Data.Level + 1);
		}
	}

	public void vcAvatarBefore_OnClick()
	{
		if (m_Data != null)
		{
			PopupVoCong.CreateByNormalScreen(GameManager.instance.m_GameClient.UserInfo, m_Data, m_Data.Level);
		}
	}

	public void vcAvatarResult_OnClick()
	{
		if (m_Data != null)
		{
			PopupVoCong.CreateByNormalScreen(GameManager.instance.m_GameClient.UserInfo, m_Data, m_Data.Level);
		}
	}

	public void btnDongY_OnClick()
	{
		if (m_Data != null && m_Data.Level >= 10)
		{
			if (GUIManager.instance.LastScreen != GAME_SCREEN.ScreenVoCong)
			{
				GUIManager.instance.SetScreen(GUIManager.instance.LastScreen);
			}
			else
			{
				GUIManager.instance.SetScreen(GAME_SCREEN.ScreenVoCong);
			}
		}
		if (NGUITools.GetActive(resultGroup.gameObject))
		{
			displayInfo();
		}
	}

	public void startPlayAnim()
	{
		m_AnimResult.SetActive(true);
		m_AnimResult.GetComponent<ParticleSystem>().Simulate(0f, true, true);
		m_AnimResult.GetComponent<ParticleSystem>().Play();
	}

	public void btnHelp_OnClick()
	{
		ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
		screenHelpInfo.setByLevel(1, 3);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
		EGDebug.Log("btnHelp_OnClick");
	}

	public int CompareVoCongThamNgo(string codeName1, int level1, string codeName2, int level2)
	{
		if (!ConfigManager.instance.m_dicVCs.ContainsKey(codeName1) || !ConfigManager.instance.m_dicVCs.ContainsKey(codeName2))
		{
			return -1;
		}
		CfgVoCong cfgVoCong = ConfigManager.instance.m_dicVCs[codeName1];
		CfgVoCong cfgVoCong2 = ConfigManager.instance.m_dicVCs[codeName2];
		if (cfgVoCong.Hang > cfgVoCong2.Hang)
		{
			return -1;
		}
		if (cfgVoCong.Hang < cfgVoCong2.Hang)
		{
			return 1;
		}
		if (cfgVoCong.m_Class == VCClass.CHIEU_THUC && (cfgVoCong2.m_Class == VCClass.BO_PHAP || cfgVoCong2.m_Class == VCClass.NOI_CONG))
		{
			return -1;
		}
		if ((cfgVoCong.m_Class == VCClass.BO_PHAP || cfgVoCong.m_Class == VCClass.NOI_CONG) && cfgVoCong2.m_Class == VCClass.CHIEU_THUC)
		{
			return 1;
		}
		if (level1 > level2)
		{
			return -1;
		}
		if (level1 < level2)
		{
			return 1;
		}
		return codeName1.CompareTo(codeName2);
	}
}
