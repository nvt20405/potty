using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenKyNgo_HuaNguyen : ScreenBase
{
	private UserInfo.HeroData m_HeroSelectedData;

	public List<NhanVatAvatar> ListAvatar;

	public UISprite bgFocus;

	public GameObject groupDeTu;

	public GameObject nhanVatAvatar3D;

	private GameObject NhanVat3D;

	public UILabel lbHuaNguyenCount;

	public OtherAvatar vpHuaNguyen;

	public UILabel lbDescription;

	public GameObject groupInfo;

	private bool isPlayFocus;

	private int currentFocusAvatar;

	private int countPlayFocus;

	private float timeGap = 0.0001f;

	private float maxTimeGap = 0.01f;

	private float icrTimeGap = 0.001f;

	private int targetIndex;

	public UILabel lbBtnHuaNguyen;

	private PhanThuongResponse phanThuongResponse;

	public UIButton btnHuaNguyen;

	public UIButton btnChonDeTu;

	private void Start()
	{
	}

	private void Update()
	{
		if (!isPlayFocus)
		{
			return;
		}
		if (currentFocusAvatar == 0)
		{
		}
		if (timeGap <= 0f)
		{
			if (currentFocusAvatar == 9)
			{
				currentFocusAvatar = 0;
				if (countPlayFocus < 4)
				{
					countPlayFocus++;
				}
			}
			else
			{
				currentFocusAvatar++;
			}
			timeGap = maxTimeGap + icrTimeGap * (float)countPlayFocus;
			maxTimeGap = timeGap;
			bgFocus.transform.localPosition = ListAvatar[currentFocusAvatar].transform.localPosition;
			if (currentFocusAvatar == targetIndex && countPlayFocus >= 4)
			{
				EGDebug.Log("currentFocusAvatar: " + currentFocusAvatar);
				isPlayFocus = false;
				countPlayFocus = 0;
				targetIndex = 0;
				currentFocusAvatar = 0;
				timeGap = 0.0001f;
				maxTimeGap = 0.001f;
				if (phanThuongResponse != null)
				{
					StartCoroutine(openPopUpPhanThuong(1.5f));
				}
				return;
			}
		}
		else
		{
			timeGap -= Time.deltaTime;
		}
		if (timeGap > 80f)
		{
			isPlayFocus = false;
			countPlayFocus = 0;
			targetIndex = 0;
			currentFocusAvatar = 0;
			timeGap = 0.0001f;
			maxTimeGap = 0.001f;
		}
	}

	public IEnumerator openPopUpPhanThuong(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongNhanDuoc"), phanThuongResponse);
		phanThuongResponse = null;
		btnHuaNguyen.gameObject.SetActive(true);
		btnChonDeTu.gameObject.SetActive(true);
	}

	public override void OnActive()
	{
		goToHuaNguyen();
	}

	public void goToHuaNguyen()
	{
		for (int i = 0; i < 10; i++)
		{
			ListAvatar[i].avatar.spriteName = "empty";
		}
		lbHuaNguyenCount.text = GameManager.instance.m_GameClient.UserInfo.Gamer.HuaNguyenCount + "/" + ConfigManager.instance.GetHuaNguyenByVip(GameManager.instance.m_GameClient.UserInfo.Gamer.Vip);
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.HuaNguyenCount >= 1)
		{
			UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_HUA_NGUYEN");
			if (vatPhamTieuThuData != null)
			{
				vpHuaNguyen.Set(vatPhamTieuThuData);
				vpHuaNguyen.displayCount(vatPhamTieuThuData.Quantity);
			}
			else
			{
				vpHuaNguyen.Set("VP_HUA_NGUYEN");
				vpHuaNguyen.displayCount(0);
			}
			lbBtnHuaNguyen.text = Localization.instance.Get("HuaNguyenBtnLabel");
			vpHuaNguyen.gameObject.SetActive(true);
		}
		else
		{
			vpHuaNguyen.gameObject.SetActive(false);
			lbBtnHuaNguyen.text = Localization.instance.Get("HuaNguyenMienPhiBtnLabel");
		}
		string key = "HuaNguyen" + GameManager.instance.m_GameClient.UserInfo.Gamer.ID;
		string text = PlayerPrefs.GetString(key);
		if (text != null && text.Length > 0 && GameManager.instance.m_GameClient.UserInfo.HeroList != null && GameManager.instance.m_GameClient.UserInfo.HeroList.Count > 0)
		{
			for (int num = 0; num < GameManager.instance.m_GameClient.UserInfo.HeroList.Count; num++)
			{
				UserInfo.HeroData heroData = GameManager.instance.m_GameClient.UserInfo.HeroList[num];
				if (heroData.Name == text)
				{
					m_HeroSelectedData = heroData;
					displaySelectedNhanVat();
				}
			}
		}
		setPlayHuaNguyen(false);
	}

	public void setPlayHuaNguyen(bool isPlay)
	{
		isPlayFocus = isPlay;
		bgFocus.gameObject.SetActive(isPlay);
		countPlayFocus = 0;
		btnHuaNguyen.gameObject.SetActive(!isPlay);
		btnChonDeTu.gameObject.SetActive(!isPlay);
	}

	public void SelectNhanVatHuaNguyen()
	{
		List<int> list = new List<int>();
		for (int i = 0; i < GameManager.instance.m_GameClient.UserInfo.HeroList.Count; i++)
		{
			NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[GameManager.instance.m_GameClient.UserInfo.HeroList[i].Name];
			if (nhanVatCfg.Hang != 3)
			{
				list.Add(GameManager.instance.m_GameClient.UserInfo.HeroList[i].HID);
			}
		}
		PopupSelectNhanVat.CreateByKyNgoHuaNguyen(onSelectNhanVatFinish, list);
	}

	public void btnHuaNguyen_OnClick()
	{
		EGDebug.Log("GetHuaNguyenByVip: " + ConfigManager.instance.GetHuaNguyenByVip(GameManager.instance.m_GameClient.UserInfo.Gamer.Vip));
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.HuaNguyenCount == ConfigManager.instance.GetHuaNguyenByVip(GameManager.instance.m_GameClient.UserInfo.Gamer.Vip))
		{
			MessagePopup.Create(Localization.instance.Get("MaxSoLanHuaNguyen"));
			return;
		}
		if (m_HeroSelectedData == null)
		{
			MessagePopup.Create(Localization.instance.Get("ChuaChonDeTuDeHuaNguyenMess"));
			return;
		}
		UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_HUA_NGUYEN");
		HuaNguyenRequest huaNguyenRequest = new HuaNguyenRequest();
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.HuaNguyenCount == 0)
		{
			huaNguyenRequest.DeTuID = m_HeroSelectedData.HID;
		}
		else
		{
			if (vatPhamTieuThuData == null || (vatPhamTieuThuData != null && vatPhamTieuThuData.Quantity < 1))
			{
				MessagePopup.Create(Localization.instance.Get("KhongCoVPHuaNguyen"));
				return;
			}
			huaNguyenRequest.DeTuID = m_HeroSelectedData.HID;
			huaNguyenRequest.VatPhamID = vatPhamTieuThuData.ID;
		}
		btnHuaNguyen.gameObject.SetActive(false);
		GameManager.instance.m_GameClient.RequestHuaNguyen(huaNguyenRequest);
	}

	public bool onSelectNhanVatFinish(int hero_id)
	{
		if (GameManager.instance.m_GameClient.UserInfo.HeroList != null && GameManager.instance.m_GameClient.UserInfo.HeroList.Count > 0)
		{
			for (int i = 0; i < GameManager.instance.m_GameClient.UserInfo.HeroList.Count; i++)
			{
				UserInfo.HeroData heroData = GameManager.instance.m_GameClient.UserInfo.HeroList[i];
				if (heroData.HID == hero_id)
				{
					m_HeroSelectedData = heroData;
					string key = "HuaNguyen" + heroData.GID;
					PlayerPrefs.SetString(key, heroData.Name);
					displaySelectedNhanVat();
				}
			}
		}
		return true;
	}

	public void displaySelectedNhanVat()
	{
		if (m_HeroSelectedData == null)
		{
			return;
		}
		NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[m_HeroSelectedData.Name];
		EGDebug.Log("HANG NHAN VAT: " + nhanVatCfg.Hang);
		if (nhanVatCfg.Hang < 3)
		{
			MessagePopup.Create(Localization.instance.Get("DieuKienNVHuongNguyenLabel"));
			return;
		}
		displayNhanVat3D();
		string huaNguyen = nhanVatCfg.HuaNguyen;
		if (huaNguyen.Length > 3)
		{
			for (int i = 0; i < 10; i++)
			{
				if (i < 3)
				{
					ListAvatar[i].Set(huaNguyen);
				}
				else
				{
					ListAvatar[i].Set(m_HeroSelectedData.Name);
				}
			}
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("KhongCoNVHuongNguyenLabel"));
		}
	}

	public void displayNhanVat3D()
	{
		if (m_HeroSelectedData != null)
		{
			if (NhanVat3D != null)
			{
				Object.Destroy(NhanVat3D);
				NhanVat3D = null;
			}
			Avatar3D avatar3D = GUIManager.instance.InstantiateAvatar3D(m_HeroSelectedData.Name, string.Empty, string.Empty, string.Empty, null, null, string.Empty, string.Empty, string.Empty);
			NhanVat3D = avatar3D.gameObject;
			if (NhanVat3D != null)
			{
				NhanVat3D.transform.parent = nhanVatAvatar3D.transform;
				NhanVat3D.transform.localPosition = Vector3.zero;
				NhanVat3D.transform.localRotation = Quaternion.Euler(0f, -180f, 0f);
				NhanVat3D.transform.localScale = Vector3.one;
				avatar3D.PlayAnimIdle();
			}
		}
	}

	public void updateInfo(HuaNguyenResponse response)
	{
		setPlayHuaNguyen(true);
		if (response != null)
		{
			targetIndex = response.KetQuaIdx;
			EGDebug.Log("TARGET INDEX : " + response.KetQuaIdx);
			lbHuaNguyenCount.text = GameManager.instance.m_GameClient.UserInfo.Gamer.HuaNguyenCount + "/" + ConfigManager.instance.GetHuaNguyenByVip(GameManager.instance.m_GameClient.UserInfo.Gamer.Vip);
			UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_HUA_NGUYEN");
			if (vatPhamTieuThuData != null)
			{
				vpHuaNguyen.Set(vatPhamTieuThuData);
				vpHuaNguyen.displayCount(vatPhamTieuThuData.Quantity);
			}
			else
			{
				vpHuaNguyen.Set("VP_HUA_NGUYEN");
				vpHuaNguyen.displayCount(0);
			}
			if (GameManager.instance.m_GameClient.UserInfo.Gamer.HuaNguyenCount > 0)
			{
				vpHuaNguyen.gameObject.SetActive(true);
				lbBtnHuaNguyen.text = Localization.instance.Get("HuaNguyenBtnLabel");
			}
			phanThuongResponse = response.phanthuong;
		}
	}

	public void updateMainMenuView()
	{
		GadgetPanelBottom gadgetPanelBottom = GUIManager.instance.gadgetPanelBottom;
		if (!(gadgetPanelBottom != null))
		{
			return;
		}
		gadgetPanelBottom.checkDisplayThongBaoSuKien();
		if (gadgetPanelBottom.listKyNgoMenu == null || gadgetPanelBottom.listKyNgoMenu.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < gadgetPanelBottom.listKyNgoMenu.Count; i++)
		{
			if (gadgetPanelBottom.listKyNgoMenu[i].kyNgoID == MenuButtonKyNgo.KyNgoType.HuaNguyen)
			{
				if (gadgetPanelBottom.checkThongBaoHuaNguyen())
				{
					gadgetPanelBottom.listKyNgoMenu[i].notifyIcon.gameObject.SetActive(true);
				}
				else
				{
					gadgetPanelBottom.listKyNgoMenu[i].notifyIcon.gameObject.SetActive(false);
				}
			}
		}
	}
}
