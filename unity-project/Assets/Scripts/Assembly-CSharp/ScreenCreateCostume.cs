using System.Collections.Generic;
using UnityEngine;

public class ScreenCreateCostume : ScreenBase
{
	public GameObject costumeAvatarPrefab;

	public UILabel nameCostumeLabel;

	public Transform avatarParent;

	public List<CostumeAvatar> listCostumeAvatar = new List<CostumeAvatar>();

	public UILabel numToLuaLabel;

	public UILabel namePhoiGiapLabel;

	public OtherAvatar phoiGiapAvatar;

	public OtherAvatar toLuaAvatar;

	public GameObject grpPhoiGiap;

	public GameObject grpNguyenLieu;

	public Transform Avatar3DParent;

	public GameObject btnCreate;

	public GameObject btnTinhLuyen;

	public GameObject btnChiSo;

	public Avatar3D avatar3D;

	public UILabel tinhLuyenMaxLabel;

	private string currentSetCostume;

	private int PhoiGiapID;

	private int CostumeID;

	private string codeNamePhoiGiapNeed;

	private UserInfo.CostumeData m_CosData;

	private void Start()
	{
		phoiGiapAvatar.OnEventClick = OnPhoiGiapClick;
	}

	private void OnBtnChiSoClick()
	{
		UserInfo.CostumeData costumeData = null;
		if (GameManager.instance.m_GameClient.UserInfo.CostumeList != null)
		{
			costumeData = GameManager.instance.m_GameClient.UserInfo.CostumeList.Find((UserInfo.CostumeData e) => e.CodeName == currentSetCostume);
		}
		UserInfo.CostumeData costumeData2 = null;
		if (costumeData == null)
		{
			costumeData2 = new UserInfo.CostumeData();
			costumeData2.CodeName = currentSetCostume;
			costumeData2.TinhLuyen = 0;
		}
		else
		{
			costumeData2 = new UserInfo.CostumeData();
			costumeData2.CodeName = costumeData.CodeName;
			costumeData2.TinhLuyen = costumeData.TinhLuyen + 1;
			costumeData2.HoangNgoc = costumeData.HoangNgoc;
			costumeData2.LamNgoc = costumeData.LamNgoc;
			costumeData2.HongNgoc = costumeData.HongNgoc;
			costumeData2.TuNgoc = costumeData.TuNgoc;
		}
		if (costumeData2.TinhLuyen > 3)
		{
			PopupCostume.Create(costumeData, false, false);
		}
		else
		{
			PopupCostume.Create(costumeData2, true, false);
		}
	}

	private void OnPhoiGiapClick(OtherAvatar avatar)
	{
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		List<int> list = new List<int>();
		for (int i = 0; i < userInfo.TrangBiList.Count; i++)
		{
			UserInfo.TrangBiData trangBiData = userInfo.TrangBiList[i];
			if (trangBiData.Name != codeNamePhoiGiapNeed)
			{
				list.Add(trangBiData.ID);
			}
		}
		PopupSelectTrangBi.Create(OnSelectPhoiGiapOK, list, LoaiTrangBi.None);
	}

	private bool OnSelectPhoiGiapOK(int tbID)
	{
		PhoiGiapID = tbID;
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		UserInfo.TrangBiData trangBiData = userInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.ID == tbID);
		if (trangBiData != null)
		{
			phoiGiapAvatar.SetTrangBiAvatar(trangBiData);
		}
		return true;
	}

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(6);
		InitListOfCostumeAvatarForMe();
	}

	public override void OnDeactive()
	{
		base.OnDeactive();
	}

	private void OnBtnTinhLuyenClick()
	{
		UserInfo.TrangBiData tb = GameManager.instance.m_GameClient.UserInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.ID == PhoiGiapID);
		if (tb != null)
		{
			if (tb.HID > 0)
			{
				UserInfo.HeroData heroData = GameManager.instance.m_GameClient.UserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == tb.HID);
				PopupYesNo.Create(string.Format(Localization.instance.Get("CostumeConfirmTinhLuyenPhoiGiapDangMac"), ConfigManager.instance.m_dicTrangBi[tb.Name].TenHienThi, ConfigManager.instance.m_dicNhanVats[heroData.Name].TenHienThi, ConfigManager.instance.m_dicCostumeCfg[currentSetCostume].TenHienThi), Localization.instance.Get("CostumeConfirmYes"), Localization.instance.Get("CostumeConfirmNo"), OnConfirmTinhLuyenYes, null);
			}
			else
			{
				PopupYesNo.Create(string.Format(Localization.instance.Get("CostumeConfirmTinhLuyenPhoiGiap"), ConfigManager.instance.m_dicTrangBi[tb.Name].TenHienThi, ConfigManager.instance.m_dicCostumeCfg[currentSetCostume].TenHienThi), Localization.instance.Get("CostumeConfirmYes"), Localization.instance.Get("CostumeConfirmNo"), OnConfirmTinhLuyenYes, null);
			}
		}
		else
		{
			PopupYesNo.Create(string.Format(Localization.instance.Get("CostumeConfirmTinhLuyen"), ConfigManager.instance.m_dicCostumeCfg[currentSetCostume].TenHienThi), Localization.instance.Get("CostumeConfirmYes"), Localization.instance.Get("CostumeConfirmNo"), OnConfirmTinhLuyenYes, null);
		}
	}

	private void OnBtnCreateClick()
	{
		UserInfo.TrangBiData tb = GameManager.instance.m_GameClient.UserInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.ID == PhoiGiapID);
		if (tb != null)
		{
			if (tb.HID > 0)
			{
				UserInfo.HeroData heroData = GameManager.instance.m_GameClient.UserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == tb.HID);
				PopupYesNo.Create(string.Format(Localization.instance.Get("CostumeConfirmCreatePhoiGiapDangMac"), ConfigManager.instance.m_dicTrangBi[tb.Name].TenHienThi, ConfigManager.instance.m_dicNhanVats[heroData.Name].TenHienThi, ConfigManager.instance.m_dicCostumeCfg[currentSetCostume].TenHienThi), Localization.instance.Get("CostumeConfirmYes"), Localization.instance.Get("CostumeConfirmNo"), OnConfirmCreateYes, null);
			}
			else
			{
				PopupYesNo.Create(string.Format(Localization.instance.Get("CostumeConfirmCreatePhoiGiap"), ConfigManager.instance.m_dicTrangBi[tb.Name].TenHienThi, ConfigManager.instance.m_dicCostumeCfg[currentSetCostume].TenHienThi), Localization.instance.Get("CostumeConfirmYes"), Localization.instance.Get("CostumeConfirmNo"), OnConfirmCreateYes, null);
			}
		}
		else
		{
			PopupYesNo.Create(string.Format(Localization.instance.Get("CostumeConfirmCreate"), ConfigManager.instance.m_dicCostumeCfg[currentSetCostume].TenHienThi), Localization.instance.Get("CostumeConfirmYes"), Localization.instance.Get("CostumeConfirmNo"), OnConfirmCreateYes, null);
		}
	}

	private void OnConfirmCreateYes()
	{
		GameManager.instance.m_GameClient.RequestCreateCostume(currentSetCostume, PhoiGiapID);
	}

	private void OnConfirmTinhLuyenYes()
	{
		GameManager.instance.m_GameClient.RequestTinhLuyenCostume(CostumeID, PhoiGiapID);
	}

	private void OnBackBtnClick()
	{
		GUIManager.GoBackLastScreen();
	}

	private void InitListOfCostumeAvatar(IEnumerable<string> listCostume)
	{
		for (int i = 0; i < listCostumeAvatar.Count; i++)
		{
			if (listCostumeAvatar[i] != null)
			{
				listCostumeAvatar[i].gameObject.SetActive(false);
				Object.Destroy(listCostumeAvatar[i].gameObject);
			}
		}
		listCostumeAvatar.Clear();
		Vector3 zero = Vector3.zero;
		Vector3 vector = default(Vector3);
		vector = new Vector3(150f, 0f, 0f);
		string costume;
		foreach (string item in listCostume)
		{
			costume = item;
			CostumeCfg costumeCfg = ConfigManager.instance.m_dicCostumeCfg[costume];
			Object obj = Object.Instantiate(costumeAvatarPrefab);
			GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
			gameObject.transform.parent = avatarParent;
			gameObject.transform.localScale = Vector3.one;
			gameObject.transform.localPosition = zero;
			gameObject.transform.localRotation = Quaternion.identity;
			zero += vector;
			gameObject.SetActive(true);
			CostumeAvatar component = gameObject.GetComponent<CostumeAvatar>();
			UserInfo.CostumeData costumeData = null;
			if (GameManager.instance.m_GameClient.UserInfo.CostumeList != null)
			{
				costumeData = GameManager.instance.m_GameClient.UserInfo.CostumeList.Find((UserInfo.CostumeData e) => e.CodeName == costume);
			}
			if (costumeData != null)
			{
				component.SetInfo(costume, costumeData.TinhLuyen);
			}
			else
			{
				component.SetInfo(costume);
			}
			component.onAvatarClick = OnCostumeAvatarClick;
			listCostumeAvatar.Add(component);
			if (currentSetCostume == costume)
			{
				OnCostumeAvatarClick(component);
			}
		}
		if (string.IsNullOrEmpty(currentSetCostume) && listCostumeAvatar.Count > 0)
		{
			OnCostumeAvatarClick(listCostumeAvatar[0]);
		}
	}

	public void InitAllListOfCostumesAvatar()
	{
		InitListOfCostumeAvatar(ConfigManager.instance.m_dicCostumeCfg.Keys);
	}

	public void InitListOfCostumeAvatarForMe()
	{
		List<string> list = new List<string>();
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		foreach (string key in ConfigManager.instance.m_dicCostumeCfg.Keys)
		{
			CostumeCfg cfg = ConfigManager.instance.m_dicCostumeCfg[key];
			if (userInfo.HeroList.Exists((UserInfo.HeroData e) => e.Name == cfg.NhanVat))
			{
				list.Add(key);
			}
		}
		InitListOfCostumeAvatar(list);
	}

	public void SelectFirstCreationForNhanVat(string nhanvat)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		NhanVatCfg value = null;
		if (!ConfigManager.instance.m_dicNhanVats.TryGetValue(nhanvat, out value))
		{
			return;
		}
		string costume;
		foreach (string key in ConfigManager.instance.m_dicCostumeCfg.Keys)
		{
			costume = key;
			if (ConfigManager.instance.m_dicCostumeCfg[costume].NhanVat == nhanvat)
			{
				if (string.IsNullOrEmpty(text2))
				{
					text2 = costume;
				}
				if (GameManager.instance.m_GameClient.UserInfo.CostumeList != null && !GameManager.instance.m_GameClient.UserInfo.CostumeList.Exists((UserInfo.CostumeData e) => e.CodeName == costume))
				{
					text = costume;
					break;
				}
			}
		}
		if (!string.IsNullOrEmpty(text))
		{
			SetInfo(text);
		}
		else if (!string.IsNullOrEmpty(text2))
		{
			SetInfo(text2);
		}
		else
		{
			MessagePopup.Create(string.Format(Localization.instance.Get("CostumeChuaCo"), value.TenHienThi));
		}
	}

	public void SetInfo(string costume)
	{
		CostumeCfg value = null;
		if (!ConfigManager.instance.m_dicCostumeCfg.TryGetValue(costume, out value))
		{
			return;
		}
		currentSetCostume = costume;
		nameCostumeLabel.text = value.TenHienThi;
		if (avatar3D != null && (costume != avatar3D.CostumeName || avatar3D.CodeName != value.NhanVat))
		{
			avatar3D.gameObject.SetActive(false);
			Object.Destroy(avatar3D.gameObject);
			avatar3D = null;
		}
		if (avatar3D == null)
		{
			avatar3D = GUIManager.instance.InstantiateAvatar3DWithBattleAnim(value.NhanVat, ConfigManager.instance.m_dicNhanVats[value.NhanVat].VuKhiMacDinh, string.Empty, string.Empty, costume);
			avatar3D.transform.parent = Avatar3DParent;
			avatar3D.transform.localPosition = Vector3.zero;
			avatar3D.transform.localScale = Vector3.one;
			avatar3D.transform.localRotation = Quaternion.identity;
		}
		avatar3D.PlayAnimBattle("idle", true);
		toLuaAvatar.Set("VP_TO_COSTUME", -1);
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		UserInfo.VatPhamTieuThuData vatPhamTieuThuData = userInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_TO_COSTUME");
		UserInfo.CostumeData costumeData = null;
		if (userInfo.CostumeList != null)
		{
			costumeData = userInfo.CostumeList.Find((UserInfo.CostumeData e) => e.CodeName == costume);
		}
		m_CosData = costumeData;
		int num = ((costumeData != null) ? (costumeData.TinhLuyen + 1) : 0);
		int num2 = ConfigManager.instance.OtherConfig.NumToLua3;
		tinhLuyenMaxLabel.gameObject.SetActive(false);
		switch (num)
		{
		case 0:
			CostumeID = 0;
			num2 = ConfigManager.instance.OtherConfig.NumToLua0;
			btnCreate.gameObject.SetActive(true);
			grpNguyenLieu.gameObject.SetActive(true);
			grpPhoiGiap.gameObject.SetActive(!string.IsNullOrEmpty(value.Stats_0.PhoiGiap));
			if (grpPhoiGiap.activeSelf)
			{
				codeNamePhoiGiapNeed = value.Stats_0.PhoiGiap;
				if (PhoiGiapID == 0)
				{
					phoiGiapAvatar.setTrangBi("empty", 0, -1, string.Empty, 0, string.Empty, 0, string.Empty);
				}
				else
				{
					UserInfo.TrangBiData trangBiData4 = userInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.ID == PhoiGiapID);
					if (trangBiData4 != null && trangBiData4.Name == codeNamePhoiGiapNeed)
					{
						phoiGiapAvatar.setTrangBi(codeNamePhoiGiapNeed, -1, -1, string.Empty, -1, string.Empty, -1, string.Empty, -1);
					}
					else
					{
						PhoiGiapID = 0;
						phoiGiapAvatar.setTrangBi("empty", 0, -1, string.Empty, 0, string.Empty, 0, string.Empty);
					}
				}
				namePhoiGiapLabel.text = ConfigManager.instance.m_dicTrangBi[value.Stats_0.PhoiGiap].TenHienThi;
			}
			else
			{
				PhoiGiapID = 0;
			}
			btnTinhLuyen.gameObject.SetActive(false);
			break;
		case 1:
			CostumeID = ((costumeData != null) ? costumeData.ID : 0);
			num2 = ConfigManager.instance.OtherConfig.NumToLua1;
			btnTinhLuyen.gameObject.SetActive(true);
			btnCreate.gameObject.SetActive(false);
			grpPhoiGiap.gameObject.SetActive(!string.IsNullOrEmpty(value.Stats_1.PhoiGiap));
			grpNguyenLieu.gameObject.SetActive(true);
			if (grpPhoiGiap.activeSelf)
			{
				codeNamePhoiGiapNeed = value.Stats_1.PhoiGiap;
				if (PhoiGiapID == 0)
				{
					phoiGiapAvatar.setTrangBi("empty", 0, -1, string.Empty, 0, string.Empty, 0, string.Empty);
				}
				else
				{
					UserInfo.TrangBiData trangBiData3 = userInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.ID == PhoiGiapID);
					if (trangBiData3 != null && trangBiData3.Name == codeNamePhoiGiapNeed)
					{
						phoiGiapAvatar.setTrangBi(codeNamePhoiGiapNeed, 0, -1, string.Empty, 0, string.Empty, 0, string.Empty);
					}
					else
					{
						PhoiGiapID = 0;
						phoiGiapAvatar.setTrangBi("empty", 0, -1, string.Empty, 0, string.Empty, 0, string.Empty);
					}
				}
				namePhoiGiapLabel.text = ConfigManager.instance.m_dicTrangBi[value.Stats_1.PhoiGiap].TenHienThi;
			}
			else
			{
				PhoiGiapID = 0;
			}
			break;
		case 2:
			CostumeID = ((costumeData != null) ? costumeData.ID : 0);
			num2 = ConfigManager.instance.OtherConfig.NumToLua2;
			btnTinhLuyen.gameObject.SetActive(true);
			btnCreate.gameObject.SetActive(false);
			grpPhoiGiap.gameObject.SetActive(!string.IsNullOrEmpty(value.Stats_2.PhoiGiap));
			grpNguyenLieu.gameObject.SetActive(true);
			if (grpPhoiGiap.activeSelf)
			{
				codeNamePhoiGiapNeed = value.Stats_2.PhoiGiap;
				if (PhoiGiapID == 0)
				{
					phoiGiapAvatar.setTrangBi("empty", 0, -1, string.Empty, 0, string.Empty, 0, string.Empty);
				}
				else
				{
					UserInfo.TrangBiData trangBiData2 = userInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.ID == PhoiGiapID);
					if (trangBiData2 != null && trangBiData2.Name == codeNamePhoiGiapNeed)
					{
						phoiGiapAvatar.setTrangBi(codeNamePhoiGiapNeed, 0, -1, string.Empty, 0, string.Empty, 0, string.Empty);
					}
					else
					{
						PhoiGiapID = 0;
						phoiGiapAvatar.setTrangBi("empty", 0, -1, string.Empty, 0, string.Empty, 0, string.Empty);
					}
				}
				namePhoiGiapLabel.text = ConfigManager.instance.m_dicTrangBi[value.Stats_2.PhoiGiap].TenHienThi;
			}
			else
			{
				PhoiGiapID = 0;
			}
			break;
		case 3:
			CostumeID = ((costumeData != null) ? costumeData.ID : 0);
			num2 = ConfigManager.instance.OtherConfig.NumToLua3;
			btnTinhLuyen.gameObject.SetActive(true);
			btnCreate.gameObject.SetActive(false);
			grpPhoiGiap.gameObject.SetActive(!string.IsNullOrEmpty(value.Stats_3.PhoiGiap));
			grpNguyenLieu.gameObject.SetActive(true);
			if (grpPhoiGiap.activeSelf)
			{
				codeNamePhoiGiapNeed = value.Stats_3.PhoiGiap;
				if (PhoiGiapID == 0)
				{
					phoiGiapAvatar.setTrangBi("empty", 0, -1, string.Empty, 0, string.Empty, 0, string.Empty);
				}
				else
				{
					UserInfo.TrangBiData trangBiData = userInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.ID == PhoiGiapID);
					if (trangBiData != null && trangBiData.Name == codeNamePhoiGiapNeed)
					{
						phoiGiapAvatar.setTrangBi(codeNamePhoiGiapNeed, 0, -1, string.Empty, 0, string.Empty, 0, string.Empty);
					}
					else
					{
						PhoiGiapID = 0;
						phoiGiapAvatar.setTrangBi("empty", 0, -1, string.Empty, 0, string.Empty, 0, string.Empty);
					}
				}
				namePhoiGiapLabel.text = ConfigManager.instance.m_dicTrangBi[value.Stats_3.PhoiGiap].TenHienThi;
			}
			else
			{
				PhoiGiapID = 0;
			}
			break;
		default:
			tinhLuyenMaxLabel.gameObject.SetActive(true);
			PhoiGiapID = 0;
			CostumeID = 0;
			btnTinhLuyen.gameObject.SetActive(false);
			btnCreate.gameObject.SetActive(false);
			grpPhoiGiap.gameObject.SetActive(false);
			grpNguyenLieu.gameObject.SetActive(false);
			break;
		}
		if (grpNguyenLieu.gameObject.activeSelf)
		{
			numToLuaLabel.text = string.Format("{0}/{1}", (vatPhamTieuThuData != null) ? vatPhamTieuThuData.Quantity : 0, num2);
		}
	}

	private void OnCostumeAvatarClick(CostumeAvatar avatar)
	{
		SetInfo(avatar.CodeName);
	}
}
