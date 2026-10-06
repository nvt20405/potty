using System;
using UnityEngine;

public class ScreenWorldmap : ScreenBase
{
	public GameObject DragPanel;

	public Transform pathGrp;

	public GameObject kyNgoBtn;

	private WMDiaDanh[] listButtons;

	public UILabel lbInfoExp;

	public GameObject[] listPaths;

	private UICheckbox currentActiveBtn;

	private GameObject newKyNgoParticle;

	private GameObject lastBtnGHParticle;

	private void Awake()
	{
		if (GUIManager.instance == null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		UIAnchor[] componentsInChildren = GetComponentsInChildren<UIAnchor>();
		UIAnchor[] array = componentsInChildren;
		UIAnchor[] array2 = array;
		foreach (UIAnchor uIAnchor in array2)
		{
			uIAnchor.widgetContainer = GUIManager.instance.GameFrame;
		}
		listButtons = DragPanel.GetComponentsInChildren<WMDiaDanh>();
		WMDiaDanh[] array3 = listButtons;
		WMDiaDanh[] array4 = array3;
		foreach (WMDiaDanh wMDiaDanh in array4)
		{
			UIEventListener.Get(wMDiaDanh.gameObject).onClick = OnGiangHoBtnClick;
		}
		currentActiveBtn = null;
	}

	private void Start()
	{
		WMDiaDanh[] array = listButtons;
		WMDiaDanh[] array2 = array;
		foreach (WMDiaDanh wMDiaDanh in array2)
		{
			if (wMDiaDanh.GiangHoIdx >= 0 && wMDiaDanh.GiangHoIdx < ConfigManager.instance.m_listGiangHo.Count)
			{
				wMDiaDanh.nameLabel.text = ConfigManager.instance.m_listGiangHo[wMDiaDanh.GiangHoIdx].TenHienThi.Replace(" ", "\n");
			}
			UIEventListener.Get(wMDiaDanh.gameObject).onClick = OnGiangHoBtnClick;
		}
	}

	private void OnBackBtnClick(GameObject go)
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenMain);
	}

	private void OnGiangHoBtnClick(GameObject go)
	{
		if (TutorialPopup.instance != null)
		{
			TutorialPopup.instance.ShowNextTutorial();
		}
		WMDiaDanh component = go.GetComponent<WMDiaDanh>();
		if (component == null)
		{
			return;
		}
		int num = 0;
		bool flag = checkGiangHoTinhAnh();
		num = ((!flag) ? ConfigManager.instance.m_listGiangHo.Count : ConfigManager.instance.m_listGiangHoTinhAnh.Count);
		int giangHoIdx = component.GiangHoIdx;
		if (giangHoIdx < 0 || giangHoIdx >= num)
		{
			return;
		}
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		UserInfo.GiangHoData giangHo = null;
		if (flag && IsGiangHoTinhAnhActive(giangHoIdx))
		{
			int num2 = 0;
			while (userInfo.GiangHoTinhAnh != null && num2 < userInfo.GiangHoTinhAnh.Count)
			{
				if (userInfo.GiangHoTinhAnh[num2].GiangHoIdx == giangHoIdx)
				{
					giangHo = userInfo.GiangHoTinhAnh[num2];
				}
				num2++;
			}
			GiangHoPopup.Create(giangHoIdx, giangHo, true);
		}
		else if (IsGiangHoActive(giangHoIdx))
		{
			int num3 = 0;
			while (userInfo.GiangHo != null && num3 < userInfo.GiangHo.Count)
			{
				if (userInfo.GiangHo[num3].GiangHoIdx == giangHoIdx)
				{
					giangHo = userInfo.GiangHo[num3];
				}
				num3++;
			}
			GiangHoPopup.Create(giangHoIdx, giangHo);
		}
		else
		{
			GiangHoCfg giangHoCfg = ConfigManager.instance.m_listGiangHo[giangHoIdx];
			string text = string.Format(Localization.instance.Get("WorldmapNotOpened"), giangHoIdx);
			if (giangHoCfg != null)
			{
				text = string.Format(Localization.instance.Get("WorldmapNotOpened"), giangHoCfg.TenHienThi);
			}
			EGDebug.LogWarning(text);
			MessagePopup.Create(text, 1f);
		}
	}

	private bool IsGiangHoActive(int ghIndex)
	{
		if (ghIndex < 0 || ghIndex >= ConfigManager.instance.m_listGiangHo.Count)
		{
			return false;
		}
		if (ghIndex == 0)
		{
			return true;
		}
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		if (userInfo.GiangHo == null || userInfo.GiangHo.Count < ghIndex)
		{
			return false;
		}
		UserInfo.GiangHoData giangHoData = userInfo.GiangHo[ghIndex - 1];
		if (giangHoData.HoanThanh > 0)
		{
			return true;
		}
		return false;
	}

	private bool IsGiangHoTinhAnhActive(int ghIndex)
	{
		if (ghIndex < 0 || ghIndex >= ConfigManager.instance.m_listGiangHoTinhAnh.Count)
		{
			return false;
		}
		if (ghIndex == 0)
		{
			return true;
		}
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		if (userInfo.GiangHoTinhAnh == null || userInfo.GiangHoTinhAnh.Count < ghIndex)
		{
			return false;
		}
		UserInfo.GiangHoData giangHoData = userInfo.GiangHoTinhAnh[ghIndex - 1];
		if (giangHoData.HoanThanh > 0)
		{
			return true;
		}
		return false;
	}

	private void RandomPoint()
	{
		UISprite[] componentsInChildren = pathGrp.GetComponentsInChildren<UISprite>();
		UISprite[] array = componentsInChildren;
		UISprite[] array2 = array;
		foreach (UISprite uISprite in array2)
		{
			uISprite.transform.localRotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0, 360));
			float num = UnityEngine.Random.Range(14f, 18f);
			uISprite.transform.localScale = new Vector3(num, num, 1f);
			uISprite.color = new Color(1f, 1f, 1f, UnityEngine.Random.Range(0.8f, 1f));
		}
	}

	private void DisableButton(WMDiaDanh ghBtn)
	{
		int num = 0;
		num = ((!checkGiangHoTinhAnh()) ? ConfigManager.instance.m_listGiangHo.Count : ConfigManager.instance.m_listGiangHoTinhAnh.Count);
		if (ghBtn.GiangHoIdx < 0 || ghBtn.GiangHoIdx >= num)
		{
			ghBtn.grpInfo.SetActive(false);
			return;
		}
		ghBtn.grpInfo.SetActive(true);
		ghBtn.nameLabel.color = Utils.MakeColor(75, 75, 75);
		ghBtn.bkg.color = Utils.MakeColor(23, 23, 23, 222);
	}

	private void EnableButton(WMDiaDanh ghBtn)
	{
		int count = ConfigManager.instance.m_listGiangHo.Count;
		if (ghBtn.GiangHoIdx < 0 || ghBtn.GiangHoIdx >= count)
		{
			ghBtn.grpInfo.SetActive(false);
			return;
		}
		ghBtn.grpInfo.SetActive(true);
		ghBtn.bkg.spriteName = "Ten_map";
		ghBtn.nameLabel.color = Utils.MakeColor(213, 4, 3);
		ghBtn.bkg.color = Color.white;
	}

	private void EnableTinhAnhButton(WMDiaDanh ghBtn)
	{
		int count = ConfigManager.instance.m_listGiangHoTinhAnh.Count;
		if (ghBtn.GiangHoIdx < 0 || ghBtn.GiangHoIdx >= count)
		{
			ghBtn.grpInfo.SetActive(false);
			return;
		}
		ghBtn.grpInfo.SetActive(true);
		ghBtn.nameLabel.color = Utils.MakeColor(240, 234, 28);
		ghBtn.bkg.spriteName = "Ten_map_tinhanh";
	}

	public void SyncWithNetworkData()
	{
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		if (userInfo.GiaTriThoiGian != null)
		{
			if (userInfo.Gamer.BoostExpTurn > 0)
			{
				lbInfoExp.text = string.Format(Localization.instance.Get("InfoTangEXPGiangHo"), userInfo.Gamer.BoostExpTurn, getHeSoExp(userInfo.Gamer.Level));
			}
			else
			{
				lbInfoExp.text = string.Empty;
			}
		}
		WMDiaDanh wMDiaDanh = null;
		int num = -1;
		if (userInfo != null && userInfo.Gamer != null)
		{
			WMDiaDanh[] array = listButtons;
			WMDiaDanh[] array2 = array;
			foreach (WMDiaDanh wMDiaDanh2 in array2)
			{
				if (IsGiangHoActive(wMDiaDanh2.GiangHoIdx))
				{
					EnableButton(wMDiaDanh2);
					if (num < wMDiaDanh2.GiangHoIdx)
					{
						num = wMDiaDanh2.GiangHoIdx;
						wMDiaDanh = wMDiaDanh2;
					}
					if (listPaths != null && listPaths.Length > wMDiaDanh2.GiangHoIdx - 1 && wMDiaDanh2.GiangHoIdx > 0)
					{
						listPaths[wMDiaDanh2.GiangHoIdx - 1].SetActive(true);
					}
				}
				else
				{
					if (listPaths != null && listPaths.Length > wMDiaDanh2.GiangHoIdx - 1 && wMDiaDanh2.GiangHoIdx > 0)
					{
						listPaths[wMDiaDanh2.GiangHoIdx - 1].SetActive(false);
					}
					DisableButton(wMDiaDanh2);
				}
			}
			if (checkGiangHoTinhAnh())
			{
				wMDiaDanh = null;
				num = -1;
				WMDiaDanh[] array3 = listButtons;
				WMDiaDanh[] array4 = array3;
				foreach (WMDiaDanh wMDiaDanh3 in array4)
				{
					if (IsGiangHoTinhAnhActive(wMDiaDanh3.GiangHoIdx))
					{
						EnableTinhAnhButton(wMDiaDanh3);
						if (num < wMDiaDanh3.GiangHoIdx)
						{
							num = wMDiaDanh3.GiangHoIdx;
							wMDiaDanh = wMDiaDanh3;
						}
					}
				}
			}
		}
		RandomPoint();
		GoToGHBtn(wMDiaDanh);
		CreateLastGHBtnParticle(wMDiaDanh);
		CreateKyNgoParticle(userInfo);
	}

	public void setByScreenTinhLuyen(int lastIndex)
	{
		WMDiaDanh wMDiaDanh = null;
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		if (userInfo != null && userInfo.Gamer != null)
		{
			WMDiaDanh[] array = listButtons;
			WMDiaDanh[] array2 = array;
			foreach (WMDiaDanh wMDiaDanh2 in array2)
			{
				if (wMDiaDanh2.GiangHoIdx == lastIndex)
				{
					wMDiaDanh = wMDiaDanh2;
					break;
				}
			}
		}
		if (wMDiaDanh != null)
		{
			GoToGHBtn(wMDiaDanh);
			OnGiangHoBtnClick(wMDiaDanh.gameObject);
		}
	}

	private void CreateKyNgoParticle(UserInfo uInfo)
	{
		if (CheckHaveKyNgo(uInfo))
		{
			if (newKyNgoParticle == null)
			{
				UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("fx/prefabs/GUI_KYNGOBUTTON"));
				newKyNgoParticle = (GameObject)((obj is GameObject) ? obj : null);
				newKyNgoParticle.transform.parent = base.transform;
				newKyNgoParticle.transform.position = kyNgoBtn.transform.position;
				newKyNgoParticle.transform.localScale = Vector3.one;
				newKyNgoParticle.transform.localRotation = Quaternion.identity;
			}
			else
			{
				newKyNgoParticle.gameObject.SetActive(true);
			}
		}
		else if (newKyNgoParticle != null)
		{
			newKyNgoParticle.gameObject.SetActive(false);
		}
	}

	private void CreateLastGHBtnParticle(WMDiaDanh lastBtn)
	{
		if (lastBtn == null)
		{
			return;
		}
		if (lastBtnGHParticle == null)
		{
			if (checkGiangHoTinhAnh())
			{
				UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("fx/prefabs/GUI_GIANGHO_TINHANH_BUTTON"));
				lastBtnGHParticle = (GameObject)((obj is GameObject) ? obj : null);
			}
			else
			{
				UnityEngine.Object obj2 = UnityEngine.Object.Instantiate(Resources.Load("fx/prefabs/GUI_GIANGHOBUTTON"));
				lastBtnGHParticle = (GameObject)((obj2 is GameObject) ? obj2 : null);
			}
			lastBtnGHParticle.transform.parent = lastBtn.transform;
			lastBtnGHParticle.transform.position = lastBtn.transform.position;
			lastBtnGHParticle.transform.localScale = Vector3.one;
			lastBtnGHParticle.transform.localRotation = Quaternion.identity;
		}
		else
		{
			lastBtnGHParticle.transform.parent = lastBtn.transform;
			lastBtnGHParticle.transform.position = lastBtn.transform.position;
			lastBtnGHParticle.transform.localScale = Vector3.one;
			lastBtnGHParticle.transform.localRotation = Quaternion.identity;
		}
	}

	public void GoToGHBtn(WMDiaDanh btn)
	{
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		if (btn != null)
		{
			UIPanel component = DragPanel.GetComponent<UIPanel>();
			Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(DragPanel.transform, btn.transform);
			Bounds bounds2 = default(Bounds);
			bounds2 = new Bounds(bounds.center, bounds.size * 10f);
			Vector3 relative = component.CalculateConstrainOffset(bounds2.min, bounds2.max);
			if (relative.magnitude > 0.001f)
			{
				component.GetComponent<UIDraggablePanel>().MoveRelative(relative);
				component.GetComponent<UIDraggablePanel>().RestrictWithinBounds(false);
			}
		}
		else
		{
			DragPanel.GetComponent<UIDraggablePanel>().ResetPosition();
		}
	}

	public static bool CheckHaveKyNgo(UserInfo uInfo)
	{
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		if (uInfo != null && uInfo.Gamer != null)
		{
			if ((uInfo.Gamer.KyNgoBanDo != null && uInfo.Gamer.KyNgoBanDo.Count > 0) || (uInfo.Gamer.KyNgoCaoNhan != null && uInfo.Gamer.KyNgoCaoNhan.Count > 0))
			{
				return true;
			}
			if (uInfo.Gamer.KyNgoBangHuu != null && uInfo.Gamer.KyNgoBangHuu.Count > 0)
			{
				foreach (UserInfo.BangHuuData item in uInfo.Gamer.KyNgoBangHuu)
				{
					if (item.Time > serverTime)
					{
						return true;
					}
				}
			}
			if (uInfo.Gamer.KyNgoThuongNhan != null && uInfo.Gamer.KyNgoThuongNhan.Count > 0)
			{
				foreach (UserInfo.ThuongNhanData item2 in uInfo.Gamer.KyNgoThuongNhan)
				{
					if (item2.GioDi > serverTime)
					{
						return true;
					}
				}
			}
			if (uInfo.Gamer.KyNgoTyThi != null && uInfo.Gamer.KyNgoTyThi.Count > 0)
			{
				foreach (UserInfo.TyThiData item3 in uInfo.Gamer.KyNgoTyThi)
				{
					if (item3.GioDi > serverTime)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	private void OnKyNgoBtnClick()
	{
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		if (CheckHaveKyNgo(userInfo))
		{
			GUIManager.setScreen(GAME_SCREEN.ScreenKyNgoGiangHo);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("KhongCoKyNgoGiangHoMsg"));
		}
	}

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(6);
		SyncWithNetworkData();
	}

	public override void OnDeactive()
	{
		base.OnDeactive();
	}

	public bool checkGiangHoTinhAnh()
	{
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		if (userInfo.GiangHoTinhAnh != null && userInfo.GiangHoTinhAnh.Count > 0)
		{
			return true;
		}
		if (userInfo.GiangHo != null && userInfo.GiangHo.Count == ConfigManager.instance.m_listGiangHo.Count && userInfo.GiangHo[userInfo.GiangHo.Count - 1].HoanThanh > 0)
		{
			return true;
		}
		return false;
	}

	private int getHeSoExp(int level)
	{
		if (level <= 50)
		{
			return 200;
		}
		if (level >= 51 && level <= 65)
		{
			return 130;
		}
		if (level >= 66 && level <= 79)
		{
			return 50;
		}
		if (level >= 80 && level <= 90)
		{
			return 20;
		}
		if (level >= 91)
		{
			return 10;
		}
		return 0;
	}
}
