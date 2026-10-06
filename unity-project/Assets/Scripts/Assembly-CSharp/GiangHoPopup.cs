using System;
using System.Collections.Generic;
using UnityEngine;

public class GiangHoPopup : MonoBehaviour
{
	public UILabel nameLabel;

	public UIPanel listPanel;

	public UILabel luotLabel;

	public OtherAvatar reward;

	public NhanVatAvatar tanHonReward;

	public GameObject anGaBtn;

	public GameObject anGaGrp;

	public UITexture bgTitle;

	public UISprite star1;

	public UISprite star2;

	public UISprite star3;

	public static GiangHoPopup instance;

	private GiangHoCfg cfg;

	private UserInfo.GiangHoData giangHo;

	private int nv_detailIdx = -1;

	public GH_ChiTietNhiemVuItem chiTietNV;

	private int chiTietNVIdx = -1;

	private List<GH_NhiemVuItem> nvListItems = new List<GH_NhiemVuItem>();

	private int luotNhanThuongConLai;

	private DateTime gioDanhNhanh = DateTime.MinValue;

	public UILabel lbButtonTinhAnh;

	public bool isGHTinhAnh;

	private GameObject particlePhanThuong;

	public int giangHoIdx { get; private set; }

	private void Update()
	{
	}

	public static void Create(int ghIdx, UserInfo.GiangHoData giangHo, bool isGiangHoTinhAnh = false)
	{
		GiangHoCfg giangHoCfg;
		if (isGiangHoTinhAnh)
		{
			if (ghIdx < 0 || ghIdx >= ConfigManager.instance.m_listGiangHoTinhAnh.Count)
			{
				return;
			}
			giangHoCfg = ConfigManager.instance.m_listGiangHoTinhAnh[ghIdx];
		}
		else
		{
			if (ghIdx < 0 || ghIdx >= ConfigManager.instance.m_listGiangHo.Count)
			{
				return;
			}
			giangHoCfg = ConfigManager.instance.m_listGiangHo[ghIdx];
		}
		if (instance != null)
		{
			DestroyPopup();
		}
		UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("Popup/GiangHoPopup"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		instance = gameObject.GetComponent<GiangHoPopup>();
		instance.cfg = giangHoCfg;
		instance.giangHoIdx = ghIdx;
		instance.giangHo = giangHo;
		instance.isGHTinhAnh = isGiangHoTinhAnh;
		if (isGiangHoTinhAnh)
		{
			instance.lbButtonTinhAnh.text = Localization.instance.Get("GiangHoThuongLabel");
		}
		else
		{
			instance.lbButtonTinhAnh.text = Localization.instance.Get("GiangHoTinhAnhLabel");
		}
		if (ghIdx < 30)
		{
			UITexture uITexture = instance.bgTitle;
			UnityEngine.Object obj2 = Resources.Load("TextureGUI/DVL_worldmap");
			uITexture.mainTexture = (Texture)((obj2 is Texture) ? obj2 : null);
		}
		else if (ghIdx < 60)
		{
			UITexture uITexture2 = instance.bgTitle;
			UnityEngine.Object obj3 = Resources.Load("TextureGUI/DVL_worldmap_2");
			uITexture2.mainTexture = (Texture)((obj3 is Texture) ? obj3 : null);
		}
		else
		{
			UITexture uITexture3 = instance.bgTitle;
			UnityEngine.Object obj4 = Resources.Load("TextureGUI/DVL_worldmap_3");
			uITexture3.mainTexture = (Texture)((obj4 is Texture) ? obj4 : null);
		}
		float x = GUIManager.instance.GameFrame.transform.localScale.x;
		float y = instance.bgTitle.transform.localScale.y;
		instance.bgTitle.transform.localScale = new Vector3(x, y, 1f);
		float num = x / (float)instance.bgTitle.mainTexture.width / 2f;
		float num2 = y / (float)instance.bgTitle.mainTexture.height / 2f;
		float left = Mathf.Clamp01(giangHoCfg.UVTextureX - num / 2f);
		float top = Mathf.Clamp01(giangHoCfg.UVTextureY - num2 / 2f);
		Rect rect = default(Rect);
		rect = new Rect(left, top, num, num2);
		if (rect.xMax > 1f)
		{
			rect.xMin -= Mathf.Clamp01(rect.xMax - 1f);
		}
		if (rect.yMax > 1f)
		{
			rect.yMin -= Mathf.Clamp01(rect.yMax - 1f);
		}
		instance.bgTitle.uvRect = rect;
		instance.SyncWithNetworkData(giangHo, giangHoCfg);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		instance.reward.OnEventClick = instance.OnNhanThuongClick;
		instance.tanHonReward.OnEventClick = instance.OnNhanThuongClick;
	}

	private void SetStars(int starGH)
	{
		star1.color = ((starGH <= 0) ? Utils.MakeColor(66, 66, 66) : Color.white);
		star2.color = ((starGH <= 1) ? Utils.MakeColor(66, 66, 66) : Color.white);
		star3.color = ((starGH <= 2) ? Utils.MakeColor(66, 66, 66) : Color.white);
	}

	private void OnNhanThuongClick()
	{
		int num = 0;
		if (giangHo != null)
		{
			if (luotNhanThuongConLai < 1)
			{
				GameManager.instance.m_GameClient.RequestNhanThuongGH(giangHo.GiangHoIdx, isGHTinhAnh);
				return;
			}
			num = giangHo.NumNhanThuong;
		}
		if (cfg.PhanThuongList.Count > num)
		{
			PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
			phanThuongResponse.PhanThuongList = new List<PhanThuongResponse.PhanThuong>();
			PhanThuongResponse.PhanThuong phanThuong = new PhanThuongResponse.PhanThuong();
			phanThuong.Name = PhanThuongResponse.GetPhanThuongCodeName(cfg.PhanThuongList[num].PhanThuong);
			phanThuong.Level = 1;
			phanThuong.Count = cfg.PhanThuongList[num].Count;
			phanThuong.Loai = PhanThuongResponse.GetLoaiPhanThuongFromCode(cfg.PhanThuongList[num].PhanThuong);
			phanThuongResponse.PhanThuongList.Add(phanThuong);
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTitle"), string.Format(Localization.instance.Get("GiangHoPhanThuongToPopupDesc"), luotNhanThuongConLai), phanThuongResponse);
		}
	}

	private void OnNhanThuongClick(NhanVatAvatar avatar)
	{
		OnNhanThuongClick();
	}

	private void OnNhanThuongClick(OtherAvatar avatar)
	{
		OnNhanThuongClick();
	}

	private void OnAnGaClick()
	{
		if (giangHo != null)
		{
			GameManager.instance.m_GameClient.RequestAnGaGH(giangHo.GiangHoIdx, isGHTinhAnh);
		}
	}

	private void OnShowNhiemVuDetail(int nvIdx)
	{
		if (chiTietNV != null)
		{
			UnityEngine.Object.Destroy(chiTietNV.gameObject);
			chiTietNV = null;
		}
		if (nvIdx == nv_detailIdx)
		{
			nv_detailIdx = -1;
		}
		else
		{
			nv_detailIdx = nvIdx;
		}
		SyncWithNetworkData(giangHo, cfg);
	}

	private void OnDanhGiangHo(int nvIdx)
	{
		GameClient gameClient = GameManager.instance.m_GameClient;
		if (gameClient.UserInfo.GiaTriThoiGian.TheLuc <= 0)
		{
			PopUpCheckTheLuc.Create();
		}
		else
		{
			gameClient.RequestDanhGiangHo(giangHoIdx, nvIdx, isGHTinhAnh);
		}
	}

	private int GetStarGiangHo(UserInfo.GiangHoData giangHo, GiangHoCfg cfg)
	{
		if (giangHo == null)
		{
			return 0;
		}
		int num = 3;
		for (int i = 0; i < cfg.NhiemVuList.Count; i++)
		{
			if (i < giangHo.NhiemVu.Count)
			{
				if (giangHo.NhiemVu[i].S < num)
				{
					num = giangHo.NhiemVu[i].S;
				}
				continue;
			}
			num = 0;
			break;
		}
		return num;
	}

	private void CreateParticlePhanThuong()
	{
		UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("FX/Prefabs/GUI_NEWITEM"));
		particlePhanThuong = (GameObject)((obj is GameObject) ? obj : null);
		particlePhanThuong.transform.parent = base.transform;
		particlePhanThuong.transform.position = reward.transform.position;
		particlePhanThuong.transform.localScale = Vector3.one;
		particlePhanThuong.transform.localRotation = Quaternion.identity;
		Utils.SetLayer(particlePhanThuong.transform, "GUIPopUp", true);
	}

	public void SyncWithNetworkData(UserInfo.GiangHoData giangHo, GiangHoCfg cfg)
	{
		this.giangHo = giangHo;
		nameLabel.text = cfg.TenHienThi;
		int num = ((giangHo != null) ? giangHo.LuotChoi : 0);
		luotLabel.gameObject.SetActive(false);
		if (listPanel.transform.childCount > 0)
		{
			foreach (Transform item in listPanel.transform)
			{
				Transform transform2 = item;
				transform2.gameObject.SetActive(false);
			}
		}
		int num2 = ((giangHo != null) ? giangHo.NumNhanThuong : 0);
		if (cfg.PhanThuongList != null && cfg.PhanThuongList.Count > 0)
		{
			if (num2 < cfg.PhanThuongList.Count)
			{
				luotLabel.gameObject.SetActive(true);
				luotLabel.text = string.Format(Localization.instance.Get("GiangHoLuotDanhLabel"), num, cfg.PhanThuongList[num2].LuotNhanThuong);
				string phanThuong = cfg.PhanThuongList[num2].PhanThuong;
				int count = cfg.PhanThuongList[num2].Count;
				if (PhanThuongResponse.GetLoaiPhanThuongFromCode(phanThuong) == PhanThuongResponse.LoaiPhanThuong.HON_NHAN_VAT)
				{
					tanHonReward.gameObject.SetActive(true);
					reward.gameObject.SetActive(false);
					tanHonReward.Set(phanThuong, 0, -1, false, (count <= 1) ? (-1) : count);
				}
				else
				{
					reward.gameObject.SetActive(true);
					tanHonReward.gameObject.SetActive(false);
					reward.Set(phanThuong, 0, -1, (count <= 1) ? (-1) : count);
				}
				luotNhanThuongConLai = cfg.PhanThuongList[num2].LuotNhanThuong - num;
				if (luotNhanThuongConLai <= 0)
				{
					if (particlePhanThuong == null)
					{
						CreateParticlePhanThuong();
					}
				}
				else if (particlePhanThuong != null)
				{
					UnityEngine.Object.Destroy(particlePhanThuong);
				}
			}
			else
			{
				luotLabel.gameObject.SetActive(false);
				reward.gameObject.SetActive(false);
				tanHonReward.gameObject.SetActive(false);
				if (particlePhanThuong != null)
				{
					UnityEngine.Object.Destroy(particlePhanThuong);
				}
			}
		}
		else if (particlePhanThuong != null)
		{
			UnityEngine.Object.Destroy(particlePhanThuong);
		}
		SetStars(GetStarGiangHo(giangHo, cfg));
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		gioDanhNhanh = userInfo.GiaTriThoiGian.LastTimeDanhNhanhGH + ConfigManager.GetTimeWaitForResetLuotDanhNhanhByVip(userInfo.Gamer.Vip);
		anGaBtn.collider.enabled = giangHo != null && giangHo.HoanThanh == 2;
		UIButton[] components = anGaBtn.GetComponents<UIButton>();
		UIButton[] array = components;
		foreach (UIButton uIButton in array)
		{
			uIButton.UpdateColor(anGaBtn.collider.enabled, true);
		}
		if (giangHo != null && giangHo.HoanThanh > 2)
		{
			anGaGrp.SetActive(false);
		}
		else
		{
			anGaGrp.SetActive(true);
		}
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 206f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, 126f);
		Vector3 vector3 = default(Vector3);
		vector3 = new Vector3(0f, 280f);
		Vector3 localPosition = vector;
		int num3 = 1;
		if (giangHo != null)
		{
			for (int j = 0; j < giangHo.NhiemVu.Count; j++)
			{
				if (giangHo.NhiemVu[j].S > 0)
				{
					num3++;
				}
			}
		}
		if (num3 > cfg.NhiemVuList.Count)
		{
			num3 = cfg.NhiemVuList.Count;
		}
		for (int k = 0; k < num3; k++)
		{
			int num4 = num3 - k - 1;
			GiangHoCfg.NhiemVu nhiemVu = cfg.NhiemVuList[num4];
			int num5 = nhiemVu.SoLuot1Ngay;
			int star = 0;
			if (giangHo != null && num4 < giangHo.NhiemVu.Count)
			{
				star = giangHo.NhiemVu[num4].S;
				num5 -= giangHo.NhiemVu[num4].T;
			}
			int code_nv = 0;
			if (num4 == cfg.NhiemVuList.Count - 1)
			{
				code_nv = 2;
			}
			else if (nhiemVu.PhanThuong.Count > 0)
			{
				code_nv = 1;
			}
			if (nvListItems.Count <= k)
			{
				GH_NhiemVuItem gH_NhiemVuItem = InstantiateNVItem();
				gH_NhiemVuItem.gameObject.transform.parent = listPanel.transform;
				gH_NhiemVuItem.gameObject.transform.localScale = Vector3.one;
				gH_NhiemVuItem.gameObject.transform.localPosition = localPosition;
				gH_NhiemVuItem.gameObject.name = "GH_NhiemVuItem" + k;
				nvListItems.Add(gH_NhiemVuItem);
				gH_NhiemVuItem.SetInfo(nhiemVu.TenHienThi, nhiemVu.NhanVatDaiDienCode, giangHoIdx, num4, num5, nhiemVu.BacThuong, nhiemVu.ExpThuong, star, code_nv, isGHTinhAnh);
				gH_NhiemVuItem.OnShowDetail = OnShowNhiemVuDetail;
				gH_NhiemVuItem.OnDanhGiangHo = OnDanhGiangHo;
			}
			else
			{
				nvListItems[k].transform.localPosition = localPosition;
				nvListItems[k].SetInfo(nhiemVu.TenHienThi, nhiemVu.NhanVatDaiDienCode, giangHoIdx, num4, num5, nhiemVu.BacThuong, nhiemVu.ExpThuong, star, code_nv, isGHTinhAnh);
			}
			nvListItems[k].gameObject.SetActive(true);
			if (num4 == nv_detailIdx)
			{
				if (chiTietNV != null)
				{
					UnityEngine.Object.Destroy(chiTietNV.gameObject);
				}
				GH_ChiTietNhiemVuItem gH_ChiTietNhiemVuItem = ((giangHo == null || giangHo.NhiemVu.Count <= num4) ? InstantiateNVDetail(null, nhiemVu, num4, isGHTinhAnh) : InstantiateNVDetail(giangHo.NhiemVu[num4], nhiemVu, num4, isGHTinhAnh));
				gH_ChiTietNhiemVuItem.gameObject.transform.parent = listPanel.transform;
				gH_ChiTietNhiemVuItem.gameObject.transform.localScale = Vector3.one;
				gH_ChiTietNhiemVuItem.gameObject.transform.localPosition = localPosition;
				chiTietNV = gH_ChiTietNhiemVuItem;
				chiTietNVIdx = num4;
				localPosition -= vector3;
			}
			else
			{
				localPosition -= vector2;
			}
		}
		GetComponent<UIPanel>().Refresh();
	}

	private GH_NhiemVuItem InstantiateNVItem()
	{
		UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("GiangHo/GH_NhiemVuItem"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		return gameObject.GetComponent<GH_NhiemVuItem>();
	}

	private GH_ChiTietNhiemVuItem InstantiateNVDetail(UserInfo.GiangHoData.NhiemVuRecord nvRecord, GiangHoCfg.NhiemVu nvCfg, int idx, bool isGHTA)
	{
		UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("GiangHo/GH_ChiTietNhiemVuItem"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		GH_ChiTietNhiemVuItem component = gameObject.GetComponent<GH_ChiTietNhiemVuItem>();
		int num = nvCfg.SoLuot1Ngay;
		if (nvRecord != null)
		{
			num -= nvRecord.T;
		}
		string rewardCode = string.Empty;
		int rewardcount = 0;
		if (nvCfg.PhanThuong != null && nvCfg.PhanThuong.Count > 0)
		{
			rewardCode = nvCfg.PhanThuong[0].PhanThuongCode;
			rewardcount = nvCfg.PhanThuong[0].Count;
		}
		component.SetInfo(giangHoIdx, idx, nvCfg.MoTa, num, rewardCode, rewardcount, gioDanhNhanh, isGHTA);
		return component;
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			UnityEngine.Object.Destroy(instance.gameObject);
		}
		instance = null;
	}

	private void OnCloseClick()
	{
		DestroyPopup();
		if (TutorialPopup.instance != null)
		{
			TutorialPopup.instance.ShowNextTutorial();
		}
	}

	private void OnNextGHBtnClick()
	{
		if (giangHo != null && giangHo.HoanThanh > 0)
		{
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			int num = giangHo.GiangHoIdx + 1;
			if (isGHTinhAnh && userInfo.GiangHoTinhAnh != null)
			{
				Create(num, (num >= userInfo.GiangHoTinhAnh.Count) ? null : userInfo.GiangHoTinhAnh[num]);
			}
			else
			{
				Create(num, (num >= userInfo.GiangHo.Count) ? null : userInfo.GiangHo[num]);
			}
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("NextGHBtnFailMsg"));
		}
	}

	public void OnCloseResultPanel()
	{
		if (!(instance != null))
		{
			return;
		}
		instance.gameObject.SetActive(true);
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		if (isGHTinhAnh)
		{
			if (userInfo.GiangHoTinhAnh != null && userInfo.GiangHoTinhAnh.Count > 0 && giangHoIdx < userInfo.GiangHoTinhAnh.Count)
			{
				UserInfo.GiangHoData giangHoData = userInfo.GiangHoTinhAnh.Find((UserInfo.GiangHoData e) => e.GiangHoIdx == giangHoIdx);
				if (ConfigManager.instance.m_listGiangHoTinhAnh != null)
				{
					if (giangHoIdx < 0 || giangHoIdx >= ConfigManager.instance.m_listGiangHoTinhAnh.Count)
					{
						return;
					}
					cfg = ConfigManager.instance.m_listGiangHoTinhAnh[giangHoIdx];
				}
				instance.SyncWithNetworkData(giangHoData, cfg);
			}
		}
		else if (userInfo.GiangHo != null && userInfo.GiangHo.Count > 0 && giangHoIdx < userInfo.GiangHo.Count)
		{
			UserInfo.GiangHoData giangHoData2 = GameManager.instance.m_GameClient.UserInfo.GiangHo.Find((UserInfo.GiangHoData e) => e.GiangHoIdx == giangHoIdx);
			if (ConfigManager.instance.m_listGiangHo != null)
			{
				if (giangHoIdx < 0 || giangHoIdx >= ConfigManager.instance.m_listGiangHo.Count)
				{
					return;
				}
				cfg = ConfigManager.instance.m_listGiangHo[giangHoIdx];
			}
			instance.SyncWithNetworkData(giangHoData2, cfg);
		}
		if (PopupUser1Tr.instance != null)
		{
			PopupUser1Tr.instance.gameObject.SetActive(true);
		}
		if (PopupDanhSachPhanThuong.instance != null)
		{
			PopupDanhSachPhanThuong.instance.gameObject.SetActive(true);
		}
		if (PopupKyNgoGiangHo.instance != null)
		{
			PopupKyNgoGiangHo.instance.gameObject.SetActive(true);
		}
		ScreenWorldmap screenWorldmap = GUIManager.getScreen(GAME_SCREEN.ScreenWorldmap) as ScreenWorldmap;
		screenWorldmap.SyncWithNetworkData();
	}

	public void OnClick_GHTinhAnh()
	{
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		UserInfo.GiangHoData giangHoData = null;
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains("GiangHoTinhAnh"))
		{
			MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
			return;
		}
		if (!isGHTinhAnh)
		{
			if (userInfo.GiangHo == null || (userInfo.GiangHo != null && userInfo.GiangHo.Count < ConfigManager.instance.m_listGiangHo.Count))
			{
				MessagePopup.Create(Localization.instance.Get("ThongBaoChuaMoGHTinhAnh"));
				return;
			}
			if (userInfo.GiangHoTinhAnh == null || (userInfo.GiangHoTinhAnh != null && giangHoIdx >= userInfo.GiangHoTinhAnh.Count && (giangHoIdx != 0 || userInfo.GiangHoTinhAnh.Count != 0 || userInfo.GiangHo.Count != ConfigManager.instance.m_listGiangHo.Count || userInfo.GiangHo[userInfo.GiangHo.Count - 1].HoanThanh <= 0)))
			{
				MessagePopup.Create(Localization.instance.Get("ThongBaoGiangHoTinhAnhNayChuaMo"));
				return;
			}
		}
		isGHTinhAnh = !isGHTinhAnh;
		if (isGHTinhAnh)
		{
			if (userInfo.GiangHoTinhAnh == null)
			{
				return;
			}
			lbButtonTinhAnh.text = Localization.instance.Get("GiangHoThuongLabel");
			if (userInfo.GiangHoTinhAnh.Count > 0 && giangHoIdx < userInfo.GiangHoTinhAnh.Count)
			{
				giangHoData = userInfo.GiangHoTinhAnh.Find((UserInfo.GiangHoData e) => e.GiangHoIdx == giangHoIdx);
			}
			if (ConfigManager.instance.m_listGiangHoTinhAnh != null)
			{
				if (giangHoIdx < 0 || giangHoIdx >= ConfigManager.instance.m_listGiangHoTinhAnh.Count)
				{
					return;
				}
				cfg = ConfigManager.instance.m_listGiangHoTinhAnh[giangHoIdx];
			}
			instance.SyncWithNetworkData(giangHoData, cfg);
		}
		else
		{
			if (userInfo.GiangHo == null || userInfo.GiangHo.Count <= 0 || giangHoIdx >= userInfo.GiangHo.Count)
			{
				return;
			}
			lbButtonTinhAnh.text = Localization.instance.Get("GiangHoTinhAnhLabel");
			giangHoData = GameManager.instance.m_GameClient.UserInfo.GiangHo.Find((UserInfo.GiangHoData e) => e.GiangHoIdx == giangHoIdx);
			if (ConfigManager.instance.m_listGiangHo != null)
			{
				if (giangHoIdx < 0 || giangHoIdx >= ConfigManager.instance.m_listGiangHo.Count)
				{
					return;
				}
				cfg = ConfigManager.instance.m_listGiangHo[giangHoIdx];
			}
			instance.SyncWithNetworkData(giangHoData, cfg);
		}
	}
}
