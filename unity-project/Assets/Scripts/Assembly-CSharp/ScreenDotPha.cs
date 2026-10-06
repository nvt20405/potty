using System.Collections.Generic;
using UnityEngine;

public class ScreenDotPha : ScreenBase
{
	public UserInfo.VoCongData m_CurrentVCData;

	public OtherAvatar voCongAvatar;

	public UILabel voCongName;

	public UILabel phamFromLabel;

	public UILabel phamToLabel;

	private List<OtherAvatar> listDieuKienVC = new List<OtherAvatar>();

	private OtherAvatar currentAvaCanSelected;

	public UserInfo.VoCongData m_VoCongSelectedData;

	private Dictionary<OtherAvatar, UserInfo.VoCongData> mDicItemSelected;

	public UILabel voCongCanNameLabel;

	public UILabel descriptionLabel;

	public UIButton dotPhaBtn;

	private string strVoCongCan;

	private UIPanel panel;

	public GameObject ItemRoot;

	public GameObject VoCongPrefab;

	private int maxVCDieuKien = 5;

	public UISprite bgDieuKien;

	public GameObject m_AnimDotPha;

	public GameObject groupDotPhaNormal;

	public GameObject groupDotPhaMax;

	public UILabel lbName1;

	public UILabel lbName2;

	public OtherAvatar voCongAvatar1;

	public OtherAvatar voCongAvatar2;

	private void Start()
	{
		panel = ItemRoot.GetComponent<UIPanel>();
	}

	public void Set(UserInfo.VoCongData data)
	{
		if (data != null)
		{
			m_CurrentVCData = data;
		}
	}

	public override void OnActive()
	{
		m_AnimDotPha.SetActive(false);
		mDicItemSelected = new Dictionary<OtherAvatar, UserInfo.VoCongData>();
		if (m_CurrentVCData != null)
		{
			displayInfo();
		}
	}

	public void displayInfo()
	{
		CfgVoCong cfgVoCong = ConfigManager.instance.m_dicVCs[m_CurrentVCData.Name];
		if ((cfgVoCong.Hang == 2 && m_CurrentVCData.Name.EndsWith("_S")) || (cfgVoCong.Hang == 3 && m_CurrentVCData.Name.EndsWith("_SS")))
		{
			displayMaxInfo();
		}
		else
		{
			displayInfoNormal();
		}
	}

	public void displayInfoNormal()
	{
		groupDotPhaMax.gameObject.SetActive(false);
		groupDotPhaNormal.gameObject.SetActive(true);
		string text = string.Empty;
		string text2 = string.Empty;
		voCongAvatar1.Set(m_CurrentVCData);
		voCongAvatar2.Set(m_CurrentVCData);
		string text3 = m_CurrentVCData.Name;
		CfgVoCong cfgVoCong = ConfigManager.instance.m_dicVCs[m_CurrentVCData.Name];
		if (text3.EndsWith("_A"))
		{
			text = Localization.instance.Get("TrungLabel");
			text2 = Localization.instance.Get("ThuongLabel");
			text3 = text3.Substring(0, text3.Length - 2);
			text3 += "_S";
		}
		else if (text3.EndsWith("_B"))
		{
			text = Localization.instance.Get("HaLabel");
			text2 = Localization.instance.Get("TrungLabel");
			text3 = text3.Substring(0, text3.Length - 2);
			text3 += "_A";
		}
		else if (text3.EndsWith("_S") && cfgVoCong.Hang == 3)
		{
			text = Localization.instance.Get("ThuongLabel");
			text2 = Localization.instance.Get("CoPhoLabel");
			text3 += "S";
		}
		UserInfo.VoCongData voCongData = new UserInfo.VoCongData();
		voCongData.Name = text3;
		voCongData.Level = m_CurrentVCData.Level;
		voCongData.Type = m_CurrentVCData.Type;
		voCongAvatar2.Set(voCongData);
		CfgVoCong cfgVoCong2 = ConfigManager.instance.m_dicVCs[text3];
		lbName2.text = cfgVoCong2.TenHienThi;
		lbName1.text = cfgVoCong.TenHienThi;
		phamFromLabel.text = Localization.instance.Get("PhamLabel") + " " + text;
		phamToLabel.text = text2;
		string text4 = "[BC1901] X" + ConfigManager.instance.GetSoQuyenCanTinhLuyen(m_CurrentVCData.Name);
		strVoCongCan = m_CurrentVCData.Name.Remove(m_CurrentVCData.Name.Length - 2, 2);
		strVoCongCan += "_B";
		CfgVoCong cfgVoCong3 = ConfigManager.instance.m_dicVCs[strVoCongCan];
		voCongCanNameLabel.text = cfgVoCong3.TenHienThi + " " + text4;
		if (text3.EndsWith("_SS") && cfgVoCong.Hang == 3)
		{
			VatPhamTieuThuCfg vatPhamTieuThuCfg = ConfigManager.instance.m_dicVatPhamTieuThu["VP_CO_PHO_TAN_QUYEN"];
			UILabel uILabel = voCongCanNameLabel;
			string text5 = uILabel.text;
			uILabel.text = text5 + "\n[-]" + vatPhamTieuThuCfg.TenHienThi + "[BC1901] X" + 500;
			displayListDieuKien(ConfigManager.instance.GetSoQuyenCanTinhLuyen(m_CurrentVCData.Name), true);
		}
		else
		{
			displayListDieuKien(ConfigManager.instance.GetSoQuyenCanTinhLuyen(m_CurrentVCData.Name), false);
		}
		descriptionLabel.text = Localization.instance.Get("DotPhaDescription");
	}

	public void displayMaxInfo()
	{
		groupDotPhaMax.gameObject.SetActive(true);
		groupDotPhaNormal.gameObject.SetActive(false);
		voCongAvatar.Set(m_CurrentVCData);
		CfgVoCong cfgVoCong = ConfigManager.instance.m_dicVCs[m_CurrentVCData.Name];
		voCongName.text = cfgVoCong.TenHienThi;
		phamFromLabel.text = string.Empty;
		voCongCanNameLabel.text = string.Empty;
		descriptionLabel.text = Localization.instance.Get("MaxVoCongPhamMess");
		clearItemRoot();
	}

	public void clearItemRoot()
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		mDicItemSelected.Clear();
		listDieuKienVC.Clear();
	}

	public void displayListDieuKien(int soLuongCan, bool isDotPhaCP)
	{
		clearItemRoot();
		if (soLuongCan <= 0)
		{
			return;
		}
		int num = soLuongCan;
		if (isDotPhaCP)
		{
			num = soLuongCan + 1;
		}
		int num2 = 120;
		int num3 = (num - 1) * num2;
		for (int i = 0; i < num; i++)
		{
			OtherAvatar component = ((GameObject)Object.Instantiate(VoCongPrefab)).GetComponent<OtherAvatar>();
			component.transform.parent = ItemRoot.transform;
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			component.transform.localPosition = new Vector3(i * num2 - num3 / 2, 0f, 0f);
			Utils.SetLayer(component.transform, "Default", true);
			if (i == soLuongCan)
			{
				VatPhamTieuThuCfg vatPhamTieuThuCfg = ConfigManager.instance.m_dicVatPhamTieuThu["VP_CO_PHO_TAN_QUYEN"];
				UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_CO_PHO_TAN_QUYEN");
				if (vatPhamTieuThuData != null)
				{
					component.Set(vatPhamTieuThuData);
				}
				else
				{
					vatPhamTieuThuData = new UserInfo.VatPhamTieuThuData();
					vatPhamTieuThuData.Name = "VP_CO_PHO_TAN_QUYEN";
					vatPhamTieuThuData.Quantity = 0;
					component.Set(vatPhamTieuThuData);
				}
			}
			else
			{
				component.avatar.spriteName = "plus";
				component.lvlBkg.alpha = 0f;
				component.lvlLabel.text = string.Empty;
				component.countBkg.alpha = 0f;
				component.countLabel.text = string.Empty;
			}
			listDieuKienVC.Add(component);
			UIEventListener.Get(component.gameObject).onClick = changeVoCongCan;
		}
		ItemRoot.transform.localPosition = new Vector3(0f, -12f, 0f);
	}

	public void btnBack_OnClick(GameObject go)
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenVoCong);
	}

	public void dotPhaBtn_OnClick(GameObject go)
	{
		if (NGUITools.GetActive(groupDotPhaMax.gameObject))
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
			TinhLuyenVoCongRequest tinhLuyenVoCongRequest = new TinhLuyenVoCongRequest();
			tinhLuyenVoCongRequest.VoCongID = m_CurrentVCData.ID;
			tinhLuyenVoCongRequest.listVoCongSuDung = list;
			GameManager.instance.m_GameClient.RequestTinhLuyenVoCong(tinhLuyenVoCongRequest);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("ChuaChonVoCongDotPhaMess"));
		}
	}

	public void updateInfo()
	{
		startPlayAnim();
		m_CurrentVCData = GameManager.instance.m_GameClient.UserInfo.VoCongList.Find((UserInfo.VoCongData e) => e.ID == m_CurrentVCData.ID);
		displayInfo();
	}

	public void changeVoCongCan(GameObject go)
	{
		currentAvaCanSelected = go.transform.GetComponent<OtherAvatar>();
		if (!(currentAvaCanSelected.strCodeName == "VP_CO_PHO_TAN_QUYEN"))
		{
			CfgVoCong cfgVoCong = ConfigManager.instance.m_dicVCs[m_CurrentVCData.Name];
			PopupSelectVoCong.Create(onFinishSelectVoCong, getListVoCongUsedID(), cfgVoCong.m_Class);
		}
	}

	public bool onFinishSelectVoCong(int voCong_id)
	{
		if (GameManager.instance.m_GameClient.UserInfo.VoCongList != null && GameManager.instance.m_GameClient.UserInfo.VoCongList.Count > 0)
		{
			for (int i = 0; i < GameManager.instance.m_GameClient.UserInfo.VoCongList.Count; i++)
			{
				UserInfo.VoCongData voCongData = GameManager.instance.m_GameClient.UserInfo.VoCongList[i];
				if (voCongData.ID == voCong_id)
				{
					m_VoCongSelectedData = voCongData;
				}
			}
			if (m_VoCongSelectedData != null)
			{
				if (mDicItemSelected.ContainsKey(currentAvaCanSelected))
				{
					mDicItemSelected.Remove(currentAvaCanSelected);
				}
				mDicItemSelected.Add(currentAvaCanSelected, m_VoCongSelectedData);
				currentAvaCanSelected.Set(m_VoCongSelectedData.Name);
			}
		}
		return true;
	}

	public List<int> getListVoCongUsedID()
	{
		List<int> list = new List<int>();
		list.Add(m_CurrentVCData.ID);
		for (int i = 0; i < GameManager.instance.m_GameClient.UserInfo.VoCongList.Count; i++)
		{
			UserInfo.VoCongData voCongData = GameManager.instance.m_GameClient.UserInfo.VoCongList[i];
			if (voCongData.HID > 0 || voCongData.Name != strVoCongCan)
			{
				list.Add(voCongData.ID);
			}
		}
		if (mDicItemSelected.Count > 0)
		{
			foreach (KeyValuePair<OtherAvatar, UserInfo.VoCongData> item in mDicItemSelected)
			{
				list.Add(item.Value.ID);
			}
		}
		return list;
	}

	public void startPlayAnim()
	{
		m_AnimDotPha.SetActive(true);
		m_AnimDotPha.GetComponent<ParticleSystem>().Play();
	}

	public void btnHelp_OnClick()
	{
		ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
		screenHelpInfo.setByLevel(1, 4);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
		EGDebug.Log("btnHelp_OnClick");
	}

	public void OnAvatarClick()
	{
		if (m_CurrentVCData != null)
		{
			PopupVoCong.CreateByNormalScreen(GameManager.instance.m_GameClient.UserInfo, m_CurrentVCData, m_CurrentVCData.Level);
		}
	}

	public void onAvatar2Click()
	{
		if (m_CurrentVCData != null)
		{
			UserInfo.VoCongData voCongData = new UserInfo.VoCongData();
			voCongData.Name = m_CurrentVCData.Name;
			voCongData.Level = m_CurrentVCData.Level;
			voCongData.Type = m_CurrentVCData.Type;
			if (voCongData.Name.EndsWith("_A"))
			{
				voCongData.Name = voCongData.Name.Substring(0, voCongData.Name.Length - 2);
				voCongData.Name += "_S";
				PopupVoCong.CreateByNormalScreen(GameManager.instance.m_GameClient.UserInfo, voCongData, voCongData.Level);
			}
			else if (voCongData.Name.EndsWith("_B"))
			{
				voCongData.Name = voCongData.Name.Substring(0, voCongData.Name.Length - 2);
				voCongData.Name += "_A";
				PopupVoCong.CreateByNormalScreen(GameManager.instance.m_GameClient.UserInfo, voCongData, voCongData.Level);
			}
			else if (voCongData.Name.EndsWith("_S"))
			{
				voCongData.Name = voCongData.Name.Substring(0, voCongData.Name.Length - 2);
				voCongData.Name += "_SS";
				PopupVoCong.CreateByNormalScreen(GameManager.instance.m_GameClient.UserInfo, voCongData, voCongData.Level);
			}
			Debug.Log("vc data: " + voCongData.Name + " - " + voCongData.Level);
			Debug.Log("current vc data: " + m_CurrentVCData.Name + " - " + m_CurrentVCData.Level);
		}
	}
}
