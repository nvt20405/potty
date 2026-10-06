using System.Collections.Generic;
using UnityEngine;

public class ScreenVoLamPho : ScreenBase
{
	private enum ScreenVoLamPhoTab
	{
		TabDeTu = 0,
		TabTrangBi = 1,
		TabVoCong = 2
	}

	private const int maxItemCount = 12;

	public GameObject NhanVatPrefab;

	public GameObject TrangBiPrefab;

	public GameObject VoCongPrefab;

	public GameObject ItemRoot;

	private ScreenVoLamPhoTab m_Tab;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<DeTuVoLamPhoItem> AvatarItemList = new List<DeTuVoLamPhoItem>();

	private List<TrangBiVoLamPhoItem> TrangBiItemList = new List<TrangBiVoLamPhoItem>();

	private List<VoCongVoLamPhoItem> VoCongItemList = new List<VoCongVoLamPhoItem>();

	private List<OtherAvatar> OtherItemList = new List<OtherAvatar>();

	private int startItemGUI_Idx;

	private int colCount = 4;

	private int rowCount;

	private float hSpace = 50f;

	private float vSpace = 70f;

	private Dictionary<string, int> VoLamPhoStatus = new Dictionary<string, int>();

	public void RefreshVoLamPhoStatus()
	{
		VoLamPhoStatus.Clear();
		foreach (UserInfo.HeroData hero in GameManager.instance.m_GameClient.UserInfo.HeroList)
		{
			VoLamPhoStatus[hero.Name] = 0;
		}
		if (GameManager.instance.m_GameClient.UserInfo.HonNhanVatList != null)
		{
			foreach (UserInfo.HonNhanVatData honNhanVat in GameManager.instance.m_GameClient.UserInfo.HonNhanVatList)
			{
				VoLamPhoStatus[honNhanVat.Name] = 0;
			}
		}
		foreach (UserInfo.TrangBiData trangBi in GameManager.instance.m_GameClient.UserInfo.TrangBiList)
		{
			VoLamPhoStatus[trangBi.Name] = 0;
		}
		if (GameManager.instance.m_GameClient.UserInfo.ManhTrangBiList != null)
		{
			foreach (UserInfo.ManhTrangBiData manhTrangBi in GameManager.instance.m_GameClient.UserInfo.ManhTrangBiList)
			{
				VoLamPhoStatus[manhTrangBi.Name] = 0;
			}
		}
		foreach (UserInfo.VoCongData voCong in GameManager.instance.m_GameClient.UserInfo.VoCongList)
		{
			VoLamPhoStatus[voCong.Name] = 0;
		}
		if (GameManager.instance.m_GameClient.UserInfo.ManhVoCongList != null)
		{
			foreach (UserInfo.ManhVoCongData manhVoCong in GameManager.instance.m_GameClient.UserInfo.ManhVoCongList)
			{
				VoLamPhoStatus[manhVoCong.Name] = 0;
			}
		}
		foreach (KeyValuePair<string, NhanVatCfg> dicNhanVat in ConfigManager.instance.m_dicNhanVats)
		{
			if (!VoLamPhoStatus.ContainsKey(dicNhanVat.Value.Name))
			{
				VoLamPhoStatus[dicNhanVat.Value.Name] = GetVoLamPhoStatus(dicNhanVat.Value.Name);
			}
		}
		foreach (KeyValuePair<string, TrangBiCfg> item in ConfigManager.instance.m_dicTrangBi)
		{
			if (!VoLamPhoStatus.ContainsKey(item.Value.Name))
			{
				VoLamPhoStatus[item.Value.Name] = GetVoLamPhoStatus(item.Value.Name);
			}
		}
		foreach (KeyValuePair<string, CfgVoCong> dicVC in ConfigManager.instance.m_dicVCs)
		{
			if (!VoLamPhoStatus.ContainsKey(dicVC.Value.Name))
			{
				VoLamPhoStatus[dicVC.Value.Name] = GetVoLamPhoStatus(dicVC.Value.Name);
			}
		}
	}

	public static int GetVoLamPhoStatus(string name)
	{
		return PlayerPrefs.GetInt(GameManager.instance.m_GameClient.UserInfo.Gamer.UserName + "_" + name, 2);
	}

	public static void SetVoLamPhoStatus(string name, int status)
	{
		PlayerPrefs.SetInt(GameManager.instance.m_GameClient.UserInfo.Gamer.UserName + "_" + name, status);
	}

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

	private void Start()
	{
	}

	public override void OnActive()
	{
		base.OnActive();
		RefreshVoLamPhoStatus();
		dragPanel = ItemRoot.GetComponent<UIDraggablePanel>();
		panel = ItemRoot.GetComponent<UIPanel>();
		SyncWithNetworkData();
	}

	public void onClick_DetuTab(bool isActive)
	{
		if (isActive && m_Tab != ScreenVoLamPhoTab.TabDeTu)
		{
			m_Tab = ScreenVoLamPhoTab.TabDeTu;
			SyncWithNetworkData();
		}
	}

	public void onClick_TrangBiTab(bool isActive)
	{
		if (isActive && m_Tab != ScreenVoLamPhoTab.TabTrangBi)
		{
			m_Tab = ScreenVoLamPhoTab.TabTrangBi;
			SyncWithNetworkData();
		}
	}

	public void onClick_VoCongTab(bool isActive)
	{
		if (isActive && m_Tab != ScreenVoLamPhoTab.TabVoCong)
		{
			m_Tab = ScreenVoLamPhoTab.TabVoCong;
			SyncWithNetworkData();
		}
	}

	public void SyncWithNetworkData()
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		AvatarItemList.Clear();
		OtherItemList.Clear();
		startItemGUI_Idx = 0;
		if (m_Tab == ScreenVoLamPhoTab.TabDeTu)
		{
			if (ConfigManager.instance.m_dicNhanVats.Count > 0)
			{
				List<NhanVatCfg> list = new List<NhanVatCfg>(ConfigManager.instance.m_dicNhanVats.Values);
				list.Sort((NhanVatCfg nhanVatCfg, NhanVatCfg y) => ConfigManager.instance.CompareNhanVat(nhanVatCfg.Name, 0, y.Name, 0));
				int num = -1;
				foreach (NhanVatCfg item2 in list)
				{
					if (item2.Name.StartsWith("NV_"))
					{
						num++;
						DeTuVoLamPhoItem component = ((GameObject)Object.Instantiate(NhanVatPrefab)).GetComponent<DeTuVoLamPhoItem>();
						component.transform.parent = ItemRoot.transform;
						component.transform.localScale = new Vector3(1f, 1f, 1f);
						float x = (float)(num % colCount) * (component.DeTuAvatar.bkg.transform.localScale.x + hSpace) - panel.clipRange.z / 2f + 70f;
						float num2 = Mathf.Floor(num / colCount) * (component.DeTuAvatar.bkg.transform.localScale.x + vSpace) + panel.clipRange.w / 2f;
						component.transform.localPosition = new Vector3(x, 0f - num2, 0f);
						DeTuVoLamPhoItem.DeTuVoLamPhoType type = (DeTuVoLamPhoItem.DeTuVoLamPhoType)VoLamPhoStatus[item2.Name];
						component.setData(item2, type);
						UIEventListener.Get(component.gameObject).onClick = DeTu_OnClick;
						AvatarItemList.Add(component);
					}
				}
			}
		}
		else if (m_Tab == ScreenVoLamPhoTab.TabTrangBi)
		{
			if (ConfigManager.instance.m_dicTrangBi.Count > 0)
			{
				List<TrangBiCfg> list2 = new List<TrangBiCfg>(ConfigManager.instance.m_dicTrangBi.Values);
				list2.Sort((TrangBiCfg trangBiCfg, TrangBiCfg y) => ConfigManager.instance.CompareTrangBi(trangBiCfg.Name, 0, y.Name, 0));
				int num3 = -1;
				foreach (TrangBiCfg item3 in list2)
				{
					num3++;
					TrangBiVoLamPhoItem component2 = ((GameObject)Object.Instantiate(TrangBiPrefab)).GetComponent<TrangBiVoLamPhoItem>();
					component2.transform.parent = ItemRoot.transform;
					component2.transform.localScale = new Vector3(1f, 1f, 1f);
					float x2 = (float)(num3 % colCount) * (component2.trangBiAvatar.bkg.transform.localScale.x + hSpace) - panel.clipRange.z / 2f + 70f;
					float num4 = Mathf.Floor(num3 / colCount) * (component2.trangBiAvatar.bkg.transform.localScale.x + vSpace) + panel.clipRange.w / 2f;
					component2.transform.localPosition = new Vector3(x2, 0f - num4, 0f);
					TrangBiVoLamPhoItem.TrangBiVoLamPhoType type2 = (TrangBiVoLamPhoItem.TrangBiVoLamPhoType)VoLamPhoStatus[item3.Name];
					component2.setData(item3, type2);
					UIEventListener.Get(component2.gameObject).onClick = TrangBi_OnClick;
					TrangBiItemList.Add(component2);
				}
			}
		}
		else if (m_Tab == ScreenVoLamPhoTab.TabVoCong && ConfigManager.instance.m_dicVCs.Count > 0)
		{
			List<CfgVoCong> list3 = new List<CfgVoCong>(ConfigManager.instance.m_dicVCs.Values);
			list3.Sort((CfgVoCong cfgVoCong, CfgVoCong y) => CompareVoCong(cfgVoCong.Name, y.Name));
			int num5 = -1;
			foreach (CfgVoCong item4 in list3)
			{
				num5++;
				VoCongVoLamPhoItem component3 = ((GameObject)Object.Instantiate(VoCongPrefab)).GetComponent<VoCongVoLamPhoItem>();
				component3.transform.parent = ItemRoot.transform;
				component3.transform.localScale = new Vector3(1f, 1f, 1f);
				float x3 = (float)(num5 % colCount) * (component3.voCongAvatar.bkg.transform.localScale.x + hSpace) - panel.clipRange.z / 2f + 70f;
				float num6 = Mathf.Floor(num5 / colCount) * (component3.voCongAvatar.bkg.transform.localScale.x + vSpace) + panel.clipRange.w / 2f;
				component3.transform.localPosition = new Vector3(x3, 0f - num6, 0f);
				VoCongVoLamPhoItem.VoCongVoLamPhoType type3 = (VoCongVoLamPhoItem.VoCongVoLamPhoType)VoLamPhoStatus[item4.Name];
				component3.setData(item4, type3);
				UIEventListener.Get(component3.gameObject).onClick = VoCong_OnClick;
				VoCongItemList.Add(component3);
			}
		}
		UIDraggablePanel component4 = ItemRoot.GetComponent<UIDraggablePanel>();
		component4.ResetPosition();
	}

	public void DeTu_OnClick(GameObject go)
	{
		DeTuVoLamPhoItem component = go.gameObject.GetComponent<DeTuVoLamPhoItem>();
		if (component != null && component.m_Data != null)
		{
			checkStatusDeTu(component.m_Data);
		}
	}

	public void VoCong_OnClick(GameObject go)
	{
		VoCongVoLamPhoItem component = go.gameObject.GetComponent<VoCongVoLamPhoItem>();
		if (component != null && component.m_Data != null)
		{
			checkStatusVoCong(component.m_Data);
		}
	}

	public void TrangBi_OnClick(GameObject go)
	{
		TrangBiVoLamPhoItem component = go.gameObject.GetComponent<TrangBiVoLamPhoItem>();
		if (component != null && component.m_Data != null)
		{
			checkStatusTrangBi(component.m_Data);
		}
	}

	public void checkStatusDeTu(NhanVatCfg cfgNhanVat)
	{
		PopupNhanVat.CreateByVoLamPhoScreen(cfgNhanVat);
	}

	public void checkStatusVoCong(CfgVoCong cfgVoCong)
	{
		PopupVoCong.CreateByVoLamPhoScreen(cfgVoCong);
	}

	public void checkStatusTrangBi(TrangBiCfg cfgTrangBi)
	{
		PopupTrangBi.CreateByNormalScreen(cfgTrangBi);
	}

	public void btnBack_onClick()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenSettings);
	}

	public int CompareVoCong(string codeName1, string codeName2)
	{
		if (ConfigManager.instance.m_dicVCs[codeName1].Hang > ConfigManager.instance.m_dicVCs[codeName2].Hang)
		{
			return -1;
		}
		if (ConfigManager.instance.m_dicVCs[codeName1].Hang < ConfigManager.instance.m_dicVCs[codeName2].Hang)
		{
			return 1;
		}
		if (ConfigManager.instance.m_dicVCs[codeName1].m_Class > ConfigManager.instance.m_dicVCs[codeName2].m_Class)
		{
			return -1;
		}
		if (ConfigManager.instance.m_dicVCs[codeName1].m_Class < ConfigManager.instance.m_dicVCs[codeName2].m_Class)
		{
			return 1;
		}
		return codeName1.CompareTo(codeName2);
	}
}
