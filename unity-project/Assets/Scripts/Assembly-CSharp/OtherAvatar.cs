using System;
using UnityEngine;

public class OtherAvatar : MonoBehaviour
{
	public UISprite avatar;

	public UISprite avatarBkg;

	public UISprite bkg;

	public UISprite countBkg;

	public UILabel countLabel;

	public UISprite lvlBkg;

	public UILabel lvlLabel;

	public UISprite manhSprite;

	public UserInfo.ManhTrangBiData m_ManhTrangBiData;

	public Action<OtherAvatar> OnEventClick;

	public GameObject groupLevelTinhLuyen;

	public string strCodeName;

	public UISprite hklvlBkg;

	private bool _isSelected;

	private int _currentQuantity;

	public UISprite spNgocTrangBi1;

	public UISprite spNgocTrangBi2;

	public UISprite spNgocTrangBi3;

	public GameObject goCoPho;

	public GameObject goDungLuyen;

	public GameObject goVoCongLv10;

	public GameObject goTrangBiHoangKim;

	public int currentQuantity
	{
		get
		{
			return _currentQuantity;
		}
		set
		{
			displayCount(value);
		}
	}

	public bool IsSelected
	{
		get
		{
			return _isSelected;
		}
		set
		{
			Select(value);
		}
	}

	public void OnAvatarClick()
	{
		EGDebug.Log("OtherAvatar click");
		if (OnEventClick != null)
		{
			OnEventClick(this);
		}
		else if (strCodeName.StartsWith("VC_"))
		{
			if (ConfigManager.instance.m_dicVCs.ContainsKey(strCodeName))
			{
				CfgVoCong vcData = ConfigManager.instance.m_dicVCs[strCodeName];
				PopupVoCong.CreateByVoLamPhoScreen(vcData);
			}
		}
		else if (strCodeName.StartsWith("VK_") || strCodeName.StartsWith("MU_") || strCodeName.StartsWith("AG_") || strCodeName.StartsWith("TS_"))
		{
			if (ConfigManager.instance.m_dicTrangBi.ContainsKey(strCodeName))
			{
				TrangBiCfg cfgData = ConfigManager.instance.m_dicTrangBi[strCodeName];
				PopupTrangBi.CreateByNormalScreen(cfgData);
			}
		}
		else if (strCodeName.StartsWith("VP_"))
		{
			if (ConfigManager.instance.m_dicVatPhamTieuThu.ContainsKey(strCodeName))
			{
				VatPhamTieuThuCfg data = ConfigManager.instance.m_dicVatPhamTieuThu[strCodeName];
				PopUpVatPham.CreateByConfigData(data);
			}
		}
		else if (strCodeName.StartsWith("MVK_") || strCodeName.StartsWith("MAG_") || strCodeName.StartsWith("MMU_") || strCodeName.StartsWith("MTS_"))
		{
			string key = strCodeName.Substring(1, strCodeName.Length - 1);
			TrangBiCfg cfgData2 = ConfigManager.instance.m_dicTrangBi[key];
			PopupTrangBi.CreateByNormalScreen(cfgData2);
		}
		else if (strCodeName.StartsWith("MVC_"))
		{
			string key2 = strCodeName.Substring(1, strCodeName.Length - 1);
			CfgVoCong vcData2 = ConfigManager.instance.m_dicVCs[key2];
			PopupVoCong.CreateByVoLamPhoScreen(vcData2);
		}
	}

	public bool IsEmpty()
	{
		return avatar.spriteName == "empty";
	}

	public void displayCount(int value)
	{
		_currentQuantity = value;
		countBkg.gameObject.SetActive(true);
		countLabel.gameObject.SetActive(true);
		if (value < 0)
		{
			countLabel.text = string.Empty;
		}
		else
		{
			countLabel.text = value.ToString();
		}
	}

	public void Select(bool isSelected)
	{
		if (isSelected)
		{
			bkg.transform.localScale = new Vector3(116f, 116f, 1f);
		}
		else
		{
			bkg.transform.localScale = new Vector3(110f, 110f, 1f);
		}
		_isSelected = isSelected;
	}

	public void SetBac(int count, bool showCount = false)
	{
		string code = "OtherAvatar_Bac_0";
		if (count >= 500000)
		{
			code = "OtherAvatar_Bac_3";
		}
		else if (count >= 100000)
		{
			code = "OtherAvatar_Bac_2";
		}
		else if (count >= 20000)
		{
			code = "OtherAvatar_Bac_1";
		}
		Set(code, 0, -1, (!showCount) ? (-1) : count);
	}

	public void SetVang(int count, bool showCount = false)
	{
		string code = "OtherAvatar_Vang_0";
		if (count >= 500000)
		{
			code = "OtherAvatar_Vang_3";
		}
		else if (count >= 100000)
		{
			code = "OtherAvatar_Vang_2";
		}
		else if (count >= 20000)
		{
			code = "OtherAvatar_Vang_1";
		}
		Set(code, 0, -1, (!showCount) ? (-1) : count);
	}

	public void Set(string code, int tinhLuyenLvl = 0, int lvl = -1, int count = -1, bool displayRightLevel = false, bool isShowDungLuyen = false, int HoangKim = 0, bool isShowByPopUp = false)
	{
		strCodeName = code;
		if (code.StartsWith("MVK_") || code.StartsWith("MAG_") || code.StartsWith("MMU_") || code.StartsWith("MTS_") || code.StartsWith("MVC_"))
		{
			if (manhSprite != null)
			{
				manhSprite.gameObject.SetActive(true);
			}
			code = code.Substring(1);
		}
		else if (manhSprite != null)
		{
			manhSprite.gameObject.SetActive(false);
		}
		if (code.StartsWith("VC_") && (code.EndsWith("_S") || code.EndsWith("_A") || code.EndsWith("_B")))
		{
			avatar.spriteName = code.Substring(0, code.Length - 2);
		}
		else if (code.StartsWith("VC_") && code.EndsWith("_SS"))
		{
			avatar.spriteName = code.Substring(0, code.Length - 3);
		}
		else
		{
			avatar.spriteName = code;
		}
		avatar.MakePixelPerfect();
		int num = 0;
		if (code.StartsWith("VC_"))
		{
			CfgVoCong cfgVoCong = ConfigManager.instance.m_dicVCs[code];
			num = cfgVoCong.Hang;
			if (avatarBkg != null)
			{
				avatarBkg.spriteName = string.Format("vc_hang{0}_bkgnho", 3);
			}
			bkg.spriteName = string.Format("bkg_avatar{0}", num);
		}
		else if (code.StartsWith("VK_") || code.StartsWith("MU_") || code.StartsWith("AG_") || code.StartsWith("TS_"))
		{
			TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[code];
			num = (int)trangBiCfg.Hang;
			if (avatarBkg != null)
			{
				avatarBkg.spriteName = string.Format("tb_hang{0}_bkgnho", num);
			}
			bkg.spriteName = string.Format("bkg_avatar{0}", num);
		}
		else if (code.StartsWith("VP_"))
		{
			if (ConfigManager.instance.m_dicVatPhamTieuThu.ContainsKey(code))
			{
				VatPhamTieuThuCfg vatPhamTieuThuCfg = ConfigManager.instance.m_dicVatPhamTieuThu[code];
			}
			else
			{
				EGDebug.Log("khong co vat pham " + code);
			}
			avatarBkg.spriteName = "vp_bkgnho";
			bkg.spriteName = "bkg_avatar4";
		}
		else if (code.StartsWith("NGUA_"))
		{
			if (ConfigManager.instance.OtherConfig.ThuCuoiConfig != null)
			{
				OtherCfg.ThuCuoiCfg value = null;
				if (ConfigManager.instance.OtherConfig.ThuCuoiConfig.TryGetValue(code, out value))
				{
					num = value.Loai;
					if (avatarBkg != null)
					{
						avatarBkg.spriteName = string.Format("tb_hang2_bkgnho", num);
					}
					if (value.Loai == 0)
					{
						bkg.spriteName = string.Format("bkg_avatar3", num);
					}
					else
					{
						bkg.spriteName = string.Format("bkg_avatar{0}", num);
					}
				}
			}
		}
		else
		{
			avatar.MakePixelPerfect();
			if (avatarBkg != null)
			{
				avatarBkg.spriteName = "vp_bkgnho";
				avatarBkg.MakePixelPerfect();
			}
			bkg.spriteName = "bkg_avatar4";
		}
		if (lvlBkg != null)
		{
			lvlBkg.gameObject.SetActive(lvl >= 0);
		}
		if (lvlLabel != null)
		{
			lvlLabel.gameObject.SetActive(lvl >= 0);
		}
		if (lvl >= 0 && lvlBkg != null && lvlLabel != null)
		{
			lvlBkg.spriteName = string.Format("hang{0}_tl{1}_lvl_bkg", num, 1);
			lvlBkg.transform.localScale = new Vector3(60f, 60f, 60f);
			lvlLabel.text = lvl.ToString();
			if (displayRightLevel)
			{
				lvlBkg.transform.localPosition = new Vector3(50f, 0f, lvlBkg.transform.localPosition.z);
				lvlLabel.transform.localPosition = new Vector3(65f, 1f, lvlLabel.transform.localPosition.z);
				lvlBkg.transform.localRotation = Quaternion.Euler(new Vector3(0f, -180f, 0f));
			}
			else
			{
				lvlBkg.transform.localPosition = new Vector3(-55f, 0f, lvlBkg.transform.localPosition.z);
				lvlLabel.transform.localPosition = new Vector3(-70f, 1f, lvlLabel.transform.localPosition.z);
				lvlBkg.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
			}
		}
		if (countLabel != null)
		{
			countLabel.gameObject.SetActive(count >= 0);
		}
		if (countBkg != null)
		{
			countBkg.gameObject.SetActive(count >= 0);
		}
		if (count >= 0 && countLabel != null && countBkg != null)
		{
			countLabel.text = count.ToString();
			float num2 = countLabel.relativeSize.x * countLabel.transform.localScale.x + 10f;
			countBkg.transform.localScale = new Vector3((!(num2 < 24f)) ? num2 : 24f, countBkg.transform.localScale.y, countBkg.transform.localScale.z);
		}
		if (tinhLuyenLvl < 0)
		{
			tinhLuyenLvl = 0;
		}
		if (groupLevelTinhLuyen != null)
		{
			foreach (Transform item in groupLevelTinhLuyen.transform)
			{
				Transform transform2 = item;
				UnityEngine.Object.Destroy(transform2.gameObject);
			}
		}
		else
		{
			groupLevelTinhLuyen = new GameObject();
			groupLevelTinhLuyen.transform.parent = base.transform;
			groupLevelTinhLuyen.transform.localPosition = new Vector3(0f, -35f, 0f);
			groupLevelTinhLuyen.transform.localScale = new Vector3(1f, 1f, 1f);
			if (isShowByPopUp)
			{
				Utils.SetLayer(groupLevelTinhLuyen.gameObject.transform, "GUIPopUp", true);
			}
		}
		if (tinhLuyenLvl > 0)
		{
			displayStar(tinhLuyenLvl);
		}
		if (code.StartsWith("VC_"))
		{
			if (code.EndsWith("_SS"))
			{
				displayStar(3);
			}
			else if (code.EndsWith("_S"))
			{
				displayStar(2);
			}
			else if (code.EndsWith("_A"))
			{
				displayStar(1);
			}
		}
		if (goVoCongLv10 != null)
		{
			foreach (Transform item2 in goVoCongLv10.transform)
			{
				Transform transform4 = item2;
				UnityEngine.Object.Destroy(transform4.gameObject);
			}
		}
		else
		{
			goVoCongLv10 = new GameObject("lvl10");
			goVoCongLv10.transform.parent = base.transform;
			goVoCongLv10.transform.localPosition = new Vector3(0f, 0f, 0f);
			goVoCongLv10.transform.localScale = base.transform.localScale;
			if (isShowByPopUp)
			{
				Utils.SetLayer(goVoCongLv10.gameObject.transform, "GUIPopUp", true);
			}
		}
		if (code.StartsWith("VC_") && !code.EndsWith("_SS") && lvl == 10)
		{
			UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("FX/Prefabs/GUI_VC10"));
			GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
			if (gameObject != null)
			{
				gameObject.transform.parent = goVoCongLv10.transform;
				gameObject.transform.position = goVoCongLv10.transform.position;
				gameObject.transform.localScale = base.transform.localScale;
				gameObject.transform.localRotation = Quaternion.identity;
				if (isShowByPopUp)
				{
					Utils.SetLayer(gameObject.gameObject.transform, "GUIPopUp", true);
				}
			}
		}
		if (goCoPho != null)
		{
			foreach (Transform item3 in goCoPho.transform)
			{
				Transform transform6 = item3;
				UnityEngine.Object.Destroy(transform6.gameObject);
			}
		}
		else
		{
			goCoPho = new GameObject("copho");
			goCoPho.transform.parent = base.transform;
			goCoPho.transform.localPosition = new Vector3(0f, 0f, 0f);
			goCoPho.transform.localScale = base.transform.localScale;
		}
		if (code.StartsWith("VC_") && code.EndsWith("_SS"))
		{
			UnityEngine.Object obj2 = UnityEngine.Object.Instantiate(Resources.Load("FX/Prefabs/GUI_VOCONGCOPHO"));
			GameObject gameObject2 = (GameObject)((obj2 is GameObject) ? obj2 : null);
			if (gameObject2 != null)
			{
				gameObject2.transform.parent = goCoPho.transform;
				gameObject2.transform.position = goCoPho.transform.position;
				gameObject2.transform.localScale = base.transform.localScale;
				gameObject2.transform.localRotation = Quaternion.identity;
				if (isShowByPopUp)
				{
					Utils.SetLayer(gameObject2.gameObject.transform, "GUIPopUp", true);
				}
			}
		}
		if (goDungLuyen != null)
		{
			foreach (Transform item4 in goDungLuyen.transform)
			{
				Transform transform8 = item4;
				UnityEngine.Object.Destroy(transform8.gameObject);
			}
		}
		else
		{
			goDungLuyen = new GameObject("dungluyen");
			goDungLuyen.transform.parent = base.transform;
			goDungLuyen.transform.localPosition = new Vector3(0f, 0f, 0f);
			goDungLuyen.transform.localScale = base.transform.localScale;
			if (isShowByPopUp)
			{
				Utils.SetLayer(goDungLuyen.gameObject.transform, "GUIPopUp", true);
			}
		}
		if (isShowDungLuyen)
		{
			UnityEngine.Object obj3 = UnityEngine.Object.Instantiate(Resources.Load("FX/Prefabs/GUI_DUNGLUYENITEM"));
			GameObject gameObject3 = (GameObject)((obj3 is GameObject) ? obj3 : null);
			if (gameObject3 != null)
			{
				gameObject3.transform.parent = goCoPho.transform;
				gameObject3.transform.position = goCoPho.transform.position;
				gameObject3.transform.localScale = base.transform.localScale;
				gameObject3.transform.localRotation = Quaternion.identity;
				if (isShowByPopUp)
				{
					Utils.SetLayer(gameObject3.gameObject.transform, "GUIPopUp", true);
				}
			}
		}
		if (goTrangBiHoangKim != null)
		{
			foreach (Transform item5 in goTrangBiHoangKim.transform)
			{
				Transform transform10 = item5;
				UnityEngine.Object.Destroy(transform10.gameObject);
			}
		}
		else
		{
			goTrangBiHoangKim = new GameObject("tbHoangKim");
			goTrangBiHoangKim.transform.parent = base.transform;
			goTrangBiHoangKim.transform.localPosition = new Vector3(0f, 0f, 0f);
			goTrangBiHoangKim.transform.localScale = base.transform.localScale;
			if (isShowByPopUp)
			{
				Utils.SetLayer(goTrangBiHoangKim.gameObject.transform, "GUIPopUp", true);
			}
		}
		if ((!code.StartsWith("VK_") && !code.StartsWith("MU_") && !code.StartsWith("AG_") && !code.StartsWith("TS_")) || HoangKim < 1)
		{
			return;
		}
		GameObject gameObject4 = null;
		if (HoangKim == 1)
		{
			gameObject4 = ((!(base.transform.localScale == new Vector3(1.2f, 1.2f, 1f))) ? (UnityEngine.Object.Instantiate(Resources.Load("FX/Prefabs/GUI_HOANGKIMTRANGBI1")) as GameObject) : (UnityEngine.Object.Instantiate(Resources.Load("FX/Prefabs/GUI_HOANGKIMTRANGBI1_02")) as GameObject));
		}
		if (HoangKim == 2)
		{
			gameObject4 = ((!(base.transform.localScale == new Vector3(1.2f, 1.2f, 1f))) ? (UnityEngine.Object.Instantiate(Resources.Load("FX/Prefabs/GUI_HOANGKIMTRANGBI2")) as GameObject) : (UnityEngine.Object.Instantiate(Resources.Load("FX/Prefabs/GUI_HOANGKIMTRANGBI2_02")) as GameObject));
		}
		if (HoangKim == 3)
		{
			gameObject4 = ((!(base.transform.localScale == new Vector3(1.2f, 1.2f, 1f))) ? (UnityEngine.Object.Instantiate(Resources.Load("FX/Prefabs/GUI_HOANGKIMTRANGBI3")) as GameObject) : (UnityEngine.Object.Instantiate(Resources.Load("FX/Prefabs/GUI_HOANGKIMTRANGBI3_02")) as GameObject));
		}
		if (gameObject4 != null)
		{
			gameObject4.transform.parent = goTrangBiHoangKim.transform;
			gameObject4.transform.position = goTrangBiHoangKim.transform.position;
			gameObject4.transform.localScale = base.transform.localScale;
			gameObject4.transform.localRotation = Quaternion.identity;
			if (isShowByPopUp)
			{
				Utils.SetLayer(gameObject4.gameObject.transform, "GUIPopUp", true);
			}
		}
	}

	private void displayStar(int countStar)
	{
		Vector3 vector = default(Vector3);
		int num;
		switch (countStar)
		{
		case 4:
			num = 27;
			vector = new Vector3(22f, 22f, 1f);
			break;
		case 5:
			num = 20;
			vector = new Vector3(20f, 20f, 1f);
			break;
		default:
			num = 30;
			vector = new Vector3(25f, 25f, 1f);
			break;
		}
		int num2 = (countStar - 1) * num;
		for (int i = 0; i < countStar; i++)
		{
			GameObject gameObject = new GameObject("star" + i);
			UISprite uISprite = gameObject.AddComponent<UISprite>();
			uISprite.atlas = GUIManager.instance.otherAtlas;
			uISprite.spriteName = "sao";
			uISprite.depth = 10;
			uISprite.transform.parent = groupLevelTinhLuyen.transform;
			uISprite.transform.localScale = vector;
			uISprite.transform.localPosition = new Vector3(i * num - num2 / 2, 0f, 0f);
		}
	}

	public void Set(UserInfo.VoCongData data, bool isShowByPopUp = false)
	{
		Set(data.Name, 0, data.Level, -1, false, false, 0, isShowByPopUp);
	}

	public void Set(UserInfo.TrangBiData data, bool isDisplayRightLevel = false, int HoangKim = 0, bool isShowByPopUp = false)
	{
		Set(data.Name, data.TinhLuyenLevel, data.Level, -1, isDisplayRightLevel, data.IsShowHoangKimEff(), HoangKim, isShowByPopUp);
	}

	public void setNgocData(UserInfo.TrangBiData data)
	{
		SetTrangBiAvatar(data);
	}

	public void Set(UserInfo.VatPhamTieuThuData data)
	{
		Set(data.Name, 0, -1, data.Quantity);
	}

	public void Set(UserInfo.ManhTrangBiData data)
	{
		Set(data.Name, 0, -1, data.Quantity);
		if (manhSprite != null)
		{
			manhSprite.gameObject.SetActive(true);
		}
	}

	public void SetManhTrangBi(UserInfo.ManhTrangBiData data)
	{
		m_ManhTrangBiData = data;
		Set(data.Name, 0, -1, data.Quantity);
		currentQuantity = data.Quantity;
		if (manhSprite != null)
		{
			manhSprite.gameObject.SetActive(true);
		}
	}

	public void Set(UserInfo.ManhVoCongData data)
	{
		Set(data.Name, 0, -1, data.Quantity);
		if (manhSprite != null)
		{
			manhSprite.gameObject.SetActive(true);
		}
	}

	public void SetTrangBiAvatar(UserInfo.TrangBiData data)
	{
		setTrangBi(data.Name, data.TinhLuyenLevel, data.Level, data.Ngoc1Name, data.Ngoc1Lvl, data.Ngoc2Name, data.Ngoc2Lvl, data.Ngoc3Name, data.Ngoc3Lvl, data.IsShowHoangKimEff(), data.HoangKim);
	}

	public void SetBaoKhiAvatar(string codeName)
	{
		strCodeName = codeName;
		if (string.IsNullOrEmpty(strCodeName))
		{
			Set("lock");
			return;
		}
		if (codeName.StartsWith("VC_") && (codeName.EndsWith("_S") || codeName.EndsWith("_A") || codeName.EndsWith("_B")))
		{
			avatar.spriteName = codeName.Substring(0, codeName.Length - 2);
		}
		else if (codeName.StartsWith("VC_") && codeName.EndsWith("_SS"))
		{
			avatar.spriteName = codeName.Substring(0, codeName.Length - 3);
		}
		else
		{
			avatar.spriteName = codeName;
		}
		avatar.MakePixelPerfect();
		if (avatarBkg != null)
		{
			avatarBkg.spriteName = string.Format("vc_hang{0}_bkgnho", 3);
		}
		CfgVoCong cfgVoCong = ConfigManager.instance.m_dicVCs[codeName];
		bkg.spriteName = string.Format("bkg_avatar{0}", cfgVoCong.Hang);
	}

	public void SetHuyenKhi(UserInfo.HuyenKhi data)
	{
		if (data == null)
		{
			Set("empty");
			return;
		}
		UserInfo.TrangBiData trangBiData = new UserInfo.TrangBiData();
		trangBiData.GID = data.GID;
		trangBiData.ID = data.ID;
		trangBiData.Level = data.Level;
		trangBiData.Name = data.Name;
		SetTrangBiAvatar(trangBiData);
	}

	public void setTrangBi(string code, int tinhLuyenLvl = 0, int lvl = -1, string ngoc1Name = "", int ngoc1Level = 0, string ngoc2Name = "", int ngoc2Level = 0, string ngoc3Name = "", int ngoc3Level = 0, bool isShowDungLuyen = false, int HoangKim = 0)
	{
		strCodeName = code;
		if (manhSprite != null)
		{
			manhSprite.gameObject.SetActive(false);
		}
		avatar.spriteName = code;
		avatar.MakePixelPerfect();
		if (strCodeName.StartsWith("VK_") || strCodeName.StartsWith("MU_") || strCodeName.StartsWith("AG_") || strCodeName.StartsWith("TS_"))
		{
			int num = 0;
			TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[strCodeName];
			num = (int)trangBiCfg.Hang;
			if (avatarBkg != null)
			{
				avatarBkg.spriteName = string.Format("tb_hang{0}_bkgnho", num);
			}
			avatarBkg.MakePixelPerfect();
			bkg.spriteName = string.Format("bkg_avatar{0}_to", num);
			bkg.MakePixelPerfect();
			bkg.transform.localPosition = new Vector3(17f, 0f, bkg.transform.localPosition.z);
			if (spNgocTrangBi1 != null)
			{
				spNgocTrangBi1.gameObject.SetActive(false);
			}
			if (spNgocTrangBi2 != null)
			{
				spNgocTrangBi2.gameObject.SetActive(false);
			}
			if (spNgocTrangBi3 != null)
			{
				spNgocTrangBi3.gameObject.SetActive(false);
			}
			if (num >= 1 && spNgocTrangBi1 != null)
			{
				setIconNgoc(ngoc1Name, ngoc1Level, spNgocTrangBi1);
				spNgocTrangBi1.gameObject.SetActive(true);
			}
			if (num >= 2 && spNgocTrangBi2 != null)
			{
				setIconNgoc(ngoc2Name, ngoc2Level, spNgocTrangBi2);
				spNgocTrangBi2.gameObject.SetActive(true);
			}
			if (num == 3 && spNgocTrangBi3 != null)
			{
				setIconNgoc(ngoc3Name, ngoc3Level, spNgocTrangBi3);
				spNgocTrangBi3.gameObject.SetActive(true);
			}
		}
		else if (strCodeName.StartsWith("HK_"))
		{
			if (avatarBkg != null)
			{
				avatarBkg.spriteName = string.Format("tb_hang{0}_bkgnho", 3);
				avatarBkg.gameObject.SetActive(false);
				avatarBkg.MakePixelPerfect();
			}
			bkg.spriteName = "huyen_khi_bgr";
			bkg.MakePixelPerfect();
			bkg.transform.localPosition = new Vector3(0f, 0f, bkg.transform.localPosition.z);
			bkg.transform.localScale = Vector3.one * 100f;
			if (hklvlBkg != null)
			{
				hklvlBkg.gameObject.SetActive(true);
			}
			avatar.transform.localScale = Vector3.one * 100f;
			if (spNgocTrangBi1 != null)
			{
				spNgocTrangBi1.gameObject.SetActive(false);
			}
			if (spNgocTrangBi2 != null)
			{
				spNgocTrangBi2.gameObject.SetActive(false);
			}
			if (spNgocTrangBi3 != null)
			{
				spNgocTrangBi3.gameObject.SetActive(false);
			}
		}
		else
		{
			if (avatarBkg != null)
			{
				avatarBkg.spriteName = string.Format("tb_hang{0}_bkgnho", 3);
			}
			avatarBkg.MakePixelPerfect();
			bkg.spriteName = string.Format("bkg_avatar{0}", 3);
			bkg.MakePixelPerfect();
			bkg.transform.localPosition = new Vector3(0f, 0f, bkg.transform.localPosition.z);
			if (spNgocTrangBi1 != null)
			{
				spNgocTrangBi1.gameObject.SetActive(false);
			}
			if (spNgocTrangBi2 != null)
			{
				spNgocTrangBi2.gameObject.SetActive(false);
			}
			if (spNgocTrangBi3 != null)
			{
				spNgocTrangBi3.gameObject.SetActive(false);
			}
		}
		if (lvlBkg != null)
		{
			lvlBkg.gameObject.SetActive(false);
		}
		if (lvlLabel != null)
		{
			lvlLabel.gameObject.SetActive(lvl >= 0);
		}
		if (lvl >= 0 && lvlLabel != null)
		{
			lvlLabel.text = lvl.ToString();
		}
		if (groupLevelTinhLuyen != null)
		{
			foreach (Transform item in groupLevelTinhLuyen.transform)
			{
				Transform transform2 = item;
				UnityEngine.Object.Destroy(transform2.gameObject);
			}
		}
		else
		{
			groupLevelTinhLuyen = new GameObject();
			groupLevelTinhLuyen.transform.parent = base.transform;
			groupLevelTinhLuyen.transform.localPosition = new Vector3(0f, -35f, 0f);
			groupLevelTinhLuyen.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		if (tinhLuyenLvl > 0)
		{
			displayStar(tinhLuyenLvl);
		}
		if (goDungLuyen != null)
		{
			foreach (Transform item2 in goDungLuyen.transform)
			{
				Transform transform4 = item2;
				UnityEngine.Object.Destroy(transform4.gameObject);
			}
		}
		else
		{
			goDungLuyen = new GameObject("dungluyen");
			goDungLuyen.transform.parent = base.transform;
			goDungLuyen.transform.localPosition = new Vector3(0f, 0f, 0f);
			goDungLuyen.transform.localScale = base.transform.localScale;
		}
		if (isShowDungLuyen)
		{
			UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("FX/Prefabs/GUI_DUNGLUYENITEM"));
			GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
			if (gameObject != null)
			{
				gameObject.transform.parent = goDungLuyen.transform;
				gameObject.transform.position = goDungLuyen.transform.position;
				gameObject.transform.localScale = base.transform.localScale;
				gameObject.transform.localRotation = Quaternion.identity;
			}
		}
		if (goTrangBiHoangKim != null)
		{
			foreach (Transform item3 in goTrangBiHoangKim.transform)
			{
				Transform transform6 = item3;
				UnityEngine.Object.Destroy(transform6.gameObject);
			}
		}
		else
		{
			goTrangBiHoangKim = new GameObject("tbHoangKim");
			goTrangBiHoangKim.transform.parent = base.transform;
			goTrangBiHoangKim.transform.localPosition = new Vector3(0f, 0f, 0f);
			goTrangBiHoangKim.transform.localScale = base.transform.localScale;
		}
		if ((code.StartsWith("VK_") || code.StartsWith("MU_") || code.StartsWith("AG_") || code.StartsWith("TS_")) && HoangKim >= 1)
		{
			GameObject gameObject2 = null;
			if (HoangKim == 1)
			{
				gameObject2 = ((!(base.transform.localScale == new Vector3(1.2f, 1.2f, 1f))) ? (UnityEngine.Object.Instantiate(Resources.Load("FX/Prefabs/GUI_HOANGKIMTRANGBI1")) as GameObject) : (UnityEngine.Object.Instantiate(Resources.Load("FX/Prefabs/GUI_HOANGKIMTRANGBI1_02")) as GameObject));
			}
			if (HoangKim == 2)
			{
				gameObject2 = ((!(base.transform.localScale == new Vector3(1.2f, 1.2f, 1f))) ? (UnityEngine.Object.Instantiate(Resources.Load("FX/Prefabs/GUI_HOANGKIMTRANGBI2")) as GameObject) : (UnityEngine.Object.Instantiate(Resources.Load("FX/Prefabs/GUI_HOANGKIMTRANGBI2_02")) as GameObject));
			}
			if (HoangKim == 3)
			{
				gameObject2 = ((!(base.transform.localScale == new Vector3(1.2f, 1.2f, 1f))) ? (UnityEngine.Object.Instantiate(Resources.Load("FX/Prefabs/GUI_HOANGKIMTRANGBI3")) as GameObject) : (UnityEngine.Object.Instantiate(Resources.Load("FX/Prefabs/GUI_HOANGKIMTRANGBI3_02")) as GameObject));
			}
			if (gameObject2 != null)
			{
				gameObject2.transform.parent = goTrangBiHoangKim.transform;
				gameObject2.transform.position = goTrangBiHoangKim.transform.position;
				gameObject2.transform.localScale = base.transform.localScale;
				gameObject2.transform.localRotation = Quaternion.identity;
			}
		}
	}

	private void setIconNgoc(string ngocName, int ngocLevel, UISprite currentSprite)
	{
		UserInfo.TrangBiData.LoaiNgoc loaiNgoc = UserInfo.TrangBiData.GetLoaiNgoc(ngocName);
		setNgocData(loaiNgoc, ngocLevel, currentSprite);
	}

	private void setNgocData(UserInfo.TrangBiData.LoaiNgoc loaiNgoc, int ngocLevel, UISprite currentSprite)
	{
		string text = "icon_";
		string text2;
		if (ngocLevel <= 0 || ngocLevel > 10)
		{
			text2 = "icon_ngoc_empty";
		}
		else
		{
			string text3;
			switch (loaiNgoc)
			{
			case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_DO:
				text3 = text + "do_hang" + ngocLevel;
				break;
			case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_TIM:
				text3 = text + "tim_hang" + ngocLevel;
				break;
			case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_VANG:
				text3 = text + "vang_hang" + ngocLevel;
				break;
			case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_XANH:
				text3 = text + "xanh_hang" + ngocLevel;
				break;
			default:
				text3 = "icon_ngoc_empty";
				break;
			}
			text2 = text3;
		}
		text = text2;
		currentSprite.spriteName = text;
	}
}
