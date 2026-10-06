using System.Collections.Generic;
using UnityEngine;

public class ScreenTinhLuyen : ScreenBase
{
	public OtherAvatar trangBiAvatar;

	public UILabel lbTrangBiName;

	public UILabel lbChuaTinhLuyen;

	public UILabel lbChiSoGiaTangBefore;

	public UILabel lbChiSoGiaTangAfter;

	public UILabel lbChiSoGTMonPhaiBefore;

	public UILabel lbChiSoGTMonPhaiAfter;

	public GameObject ItemRoot;

	public GameObject TrangBiYeuCauPrefab;

	public UISprite bgItemRoot;

	public UIButton btnTinhLuyen;

	public UILabel lbChiSoGiaTangMax;

	public UILabel lbChiSoGiaTangMonPhaiMax;

	public GameObject groupMaxTinhLuyen;

	public GameObject groupDetailTinhLuyen;

	public GameObject groupStarBefore;

	public GameObject groupStarAfter;

	public UserInfo.TrangBiData m_TrangBiData;

	private int currentPoint;

	private int currentLevel;

	private int tinhLuyenMaxExp;

	private List<UserInfo.ManhTrangBiData> ListData = new List<UserInfo.ManhTrangBiData>();

	private TinhLuyenTrangBiRequest tinhLuyenRequest;

	public GameObject m_AnimTinhLuyen;

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

	public void Set(UserInfo.TrangBiData data)
	{
		if (data != null)
		{
			m_TrangBiData = data;
		}
	}

	public override void OnActive()
	{
		m_AnimTinhLuyen.SetActive(false);
		tinhLuyenRequest = new TinhLuyenTrangBiRequest();
		tinhLuyenRequest.listManhSuDung.Clear();
		if (m_TrangBiData != null)
		{
			displayInfo(m_TrangBiData.TinhLuyenLevel);
		}
	}

	public void displayInfo(int currentLevel)
	{
		trangBiAvatar.Set(m_TrangBiData);
		TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[m_TrangBiData.Name];
		lbTrangBiName.text = trangBiCfg.TenHienThi;
		tinhLuyenRequest.TrangBiID = m_TrangBiData.ID;
		currentPoint = m_TrangBiData.TinhLuyenExp;
		tinhLuyenMaxExp = ConfigManager.instance.GetTinhLuyenMaxExp(m_TrangBiData.Name, currentLevel);
		lbChiSoGiaTangBefore.text = ConfigManager.GetMaxCuongHoaLevelByTinhLuyen(currentLevel) + "x " + Localization.instance.Get("CapMonPhaiLabel");
		lbChiSoGiaTangAfter.text = ConfigManager.GetMaxCuongHoaLevelByTinhLuyen(currentLevel + 1) + "x " + Localization.instance.Get("CapMonPhaiLabel");
		if (groupStarAfter != null)
		{
			foreach (Transform item in groupStarAfter.transform)
			{
				Transform transform2 = item;
				Object.Destroy(transform2.gameObject);
			}
		}
		if (groupStarBefore != null)
		{
			foreach (Transform item2 in groupStarBefore.transform)
			{
				Transform transform4 = item2;
				Object.Destroy(transform4.gameObject);
			}
		}
		if (currentLevel == 0)
		{
			lbChuaTinhLuyen.gameObject.SetActive(true);
		}
		else
		{
			lbChuaTinhLuyen.gameObject.SetActive(false);
			displayStar(currentLevel, groupStarBefore);
		}
		int num = currentLevel + 1;
		if (num >= 1)
		{
			displayStar(num, groupStarAfter);
		}
		int num2 = ConfigManager.instance.GetBonusFromTinhLuyenTrangBi(m_TrangBiData.TinhLuyenLevel) - 100;
		int num3 = ConfigManager.instance.GetBonusFromTinhLuyenTrangBi(m_TrangBiData.TinhLuyenLevel + 1) - 100;
		lbChiSoGTMonPhaiBefore.text = string.Format(Localization.instance.Get("ChiSoGiaTangTinhLuyenLabel"), num2);
		lbChiSoGTMonPhaiAfter.text = string.Format(Localization.instance.Get("ChiSoGiaTangTinhLuyenLabel"), num3);
		getListTrangBi();
	}

	public void getListTrangBi()
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		tinhLuyenRequest.listManhSuDung.Clear();
		if (m_TrangBiData != null)
		{
			OtherCfg.TinhLuyenCfg tinhLuyenCfgForTrangBi = ConfigManager.instance.OtherConfig.GetTinhLuyenCfgForTrangBi(m_TrangBiData);
			if (tinhLuyenCfgForTrangBi == null)
			{
				groupMaxTinhLuyen.gameObject.SetActive(true);
				bgItemRoot.gameObject.SetActive(false);
				btnTinhLuyen.gameObject.SetActive(false);
				groupDetailTinhLuyen.gameObject.SetActive(false);
				lbChiSoGiaTangMax.text = ConfigManager.GetMaxCuongHoaLevelByTinhLuyen(m_TrangBiData.TinhLuyenLevel) + "x " + Localization.instance.Get("CapMonPhaiLabel");
				int num = ConfigManager.instance.GetBonusFromTinhLuyenTrangBi(m_TrangBiData.TinhLuyenLevel) - 100;
				lbChiSoGiaTangMonPhaiMax.text = string.Format(Localization.instance.Get("ChiSoGiaTangTinhLuyenLabel"), num);
			}
			else
			{
				groupMaxTinhLuyen.gameObject.SetActive(false);
				bgItemRoot.gameObject.SetActive(true);
				btnTinhLuyen.gameObject.SetActive(true);
				groupDetailTinhLuyen.gameObject.SetActive(true);
				displayManhTrangBiCan(tinhLuyenCfgForTrangBi);
			}
		}
	}

	public void displayManhTrangBiCan(OtherCfg.TinhLuyenCfg tinhLuyenCfg)
	{
		if (m_TrangBiData == null || tinhLuyenCfg == null)
		{
			return;
		}
		TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[m_TrangBiData.Name];
		if (trangBiCfg == null || trangBiCfg.TinhLuyen == null || tinhLuyenCfg.SoLoai <= 0 || tinhLuyenCfg.SoLoai > trangBiCfg.TinhLuyen.Count || tinhLuyenCfg.SoLoai != tinhLuyenCfg.SoManh.Count)
		{
			return;
		}
		int num = 135;
		int num2 = tinhLuyenCfg.SoLoai;
		if (tinhLuyenCfg.SoTayTuyDan > 0)
		{
			num2++;
		}
		if (num2 >= 5)
		{
			num = 125;
		}
		int num3 = (num2 - 1) * num;
		for (int i = 0; i < num2; i++)
		{
			TrangBiTinhLuyenItem trangBiTinhLuyenItem = null;
			string codeName = string.Empty;
			int num4 = 0;
			int num5 = 0;
			if (num2 > tinhLuyenCfg.SoLoai && i == tinhLuyenCfg.SoLoai)
			{
				trangBiTinhLuyenItem = ((GameObject)Object.Instantiate(TrangBiYeuCauPrefab)).GetComponent<TrangBiTinhLuyenItem>();
				trangBiTinhLuyenItem.transform.parent = ItemRoot.transform;
				trangBiTinhLuyenItem.transform.localScale = new Vector3(1f, 1f, 1f);
				codeName = "VP_TAY_TUY_DAN";
				num4 = tinhLuyenCfg.SoTayTuyDan;
				num5 = 0;
				UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == codeName);
				if (vatPhamTieuThuData != null)
				{
					num5 = vatPhamTieuThuData.Quantity;
					tinhLuyenRequest.TayTuyDan_ID = vatPhamTieuThuData.ID;
				}
				else
				{
					vatPhamTieuThuData = new UserInfo.VatPhamTieuThuData();
					vatPhamTieuThuData.Name = "VP_TAY_TUY_DAN";
					vatPhamTieuThuData.Quantity = 0;
					tinhLuyenRequest.TayTuyDan_ID = 0;
				}
				trangBiTinhLuyenItem.setDataTayTuyDan(vatPhamTieuThuData, tinhLuyenCfg.SoTayTuyDan);
			}
			else
			{
				trangBiTinhLuyenItem = ((GameObject)Object.Instantiate(TrangBiYeuCauPrefab)).GetComponent<TrangBiTinhLuyenItem>();
				trangBiTinhLuyenItem.transform.parent = ItemRoot.transform;
				trangBiTinhLuyenItem.transform.localScale = new Vector3(1f, 1f, 1f);
				codeName = trangBiCfg.TinhLuyen[i];
				num4 = tinhLuyenCfg.SoManh[i];
				num5 = 0;
				TinhLuyenTrangBiRequest.ManhTrangBiSuDung manhTrangBiSuDung = new TinhLuyenTrangBiRequest.ManhTrangBiSuDung();
				UserInfo.ManhTrangBiData manhTrangBiData = GameManager.instance.m_GameClient.UserInfo.ManhTrangBiList.Find((UserInfo.ManhTrangBiData e) => e.Name == codeName);
				if (manhTrangBiData == null)
				{
					manhTrangBiData = GameManager.instance.m_GameClient.UserInfo.ManhTrangBiList.Find((UserInfo.ManhTrangBiData e) => e.Name == "M" + codeName);
				}
				if (manhTrangBiData != null)
				{
					num5 = manhTrangBiData.Quantity;
					manhTrangBiSuDung.ID = manhTrangBiData.ID;
					manhTrangBiSuDung.Quantity = num5;
				}
				else
				{
					manhTrangBiSuDung.ID = 0;
					manhTrangBiSuDung.Quantity = 0;
				}
				trangBiTinhLuyenItem.setData(codeName, num5, num4);
				tinhLuyenRequest.listManhSuDung.Add(manhTrangBiSuDung);
			}
			if (trangBiTinhLuyenItem != null)
			{
				trangBiTinhLuyenItem.transform.localPosition = new Vector3(i * num - num3 / 2, 0f, 0f);
			}
		}
	}

	public void updateInfo(TinhLuyenTrangBiResponse response)
	{
		UserInfo.TrangBiData trangBiData = GameManager.instance.m_GameClient.UserInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.ID == response.TrangBiID);
		if (trangBiData != null)
		{
			m_TrangBiData = trangBiData;
			displayInfo(response.NewTinhLuyenLvl);
		}
	}

	public void btnTinhLuyen_OnClick(GameObject go)
	{
		if (tinhLuyenRequest.listManhSuDung != null && tinhLuyenRequest.listManhSuDung.Count > 0)
		{
			GameManager.instance.m_GameClient.RequestTinhLuyenTrangBi(tinhLuyenRequest);
		}
	}

	public void btnBack_OnClick(GameObject go)
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenTrangBi);
	}

	public void startPlayAnim()
	{
		m_AnimTinhLuyen.SetActive(true);
		m_AnimTinhLuyen.GetComponent<ParticleSystem>().Simulate(0f, true, true);
		m_AnimTinhLuyen.GetComponent<ParticleSystem>().Play();
	}

	public void btnHelp_OnClick()
	{
		ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
		screenHelpInfo.setByLevel(2, 3);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
		EGDebug.Log("btnHelp_OnClick");
	}

	private void displayStar(int countStar, GameObject parentGO)
	{
		int num = 40;
		Vector3 vector = default(Vector3);
		vector = new Vector3(35f, 35f, 1f);
		int num2 = (countStar - 1) * num;
		for (int i = 0; i < countStar; i++)
		{
			GameObject gameObject = new GameObject("star" + i);
			UISprite uISprite = gameObject.AddComponent<UISprite>();
			uISprite.atlas = GUIManager.instance.otherAtlas;
			uISprite.spriteName = "sao";
			uISprite.depth = 10;
			uISprite.transform.parent = parentGO.transform;
			uISprite.transform.localScale = vector;
			uISprite.transform.localPosition = new Vector3(i * num - num2 / 2, 0f, 0f);
		}
	}

	public int CompareManhTrangBi(string codeName1, int count1, string codeName2, int count2)
	{
		if (!ConfigManager.instance.m_dicTrangBi.ContainsKey(codeName1) || !ConfigManager.instance.m_dicTrangBi.ContainsKey(codeName2))
		{
			return -1;
		}
		if (ConfigManager.instance.m_dicTrangBi[codeName1].Hang > ConfigManager.instance.m_dicTrangBi[codeName2].Hang)
		{
			return -1;
		}
		if (ConfigManager.instance.m_dicTrangBi[codeName1].Hang < ConfigManager.instance.m_dicTrangBi[codeName2].Hang)
		{
			return 1;
		}
		if (count1 > count2)
		{
			return -1;
		}
		if (count1 < count2)
		{
			return 1;
		}
		return codeName1.CompareTo(codeName2);
	}
}
