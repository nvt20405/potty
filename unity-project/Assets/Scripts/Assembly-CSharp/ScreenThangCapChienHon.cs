using UnityEngine;

public class ScreenThangCapChienHon : ScreenBase
{
	public UISlider expBarSlider;

	public UILabel expBarLabel;

	public EGGUIGrid grid;

	public UICheckbox optionDung1Lan;

	public UICheckbox optionDung10Lan;

	public UICheckbox optionDung100Lan;

	public NhanVatAvatar nhanvatAvatar;

	public UILabel nhanVatName;

	private UserInfo RefUserInfo;

	public UILabel MenhLabel;

	public UILabel NgoaiLabel;

	public UILabel ThanLabel;

	public UILabel KhiLabel;

	public UISprite spPham;

	public GameObject TanHonT;

	public OtherAvatar tienLinhDanAvatar;

	private UseTanHonTangLevelChienHonRequest m_requestData = new UseTanHonTangLevelChienHonRequest();

	public UserInfo.HeroData heroData { get; set; }

	public UserInfo.ChienHon chienHonData { get; set; }

	private void Awake()
	{
	}

	public void btnBack_OnClick()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenChienHon);
	}

	private void OnActivateDung1Lan(bool isActive)
	{
		if (isActive)
		{
			optionDung1Lan.isChecked = true;
			optionDung10Lan.isChecked = false;
			optionDung100Lan.isChecked = false;
			m_requestData.UseTienLinhDan = UseTanHonTangLevelChienHonRequest.DungTienLinhDan.Use1;
		}
		else if (!optionDung10Lan.isChecked && !optionDung100Lan.isChecked)
		{
			optionDung1Lan.isChecked = true;
			m_requestData.UseTienLinhDan = UseTanHonTangLevelChienHonRequest.DungTienLinhDan.Use1;
		}
	}

	private void OnActivateDung10Lan(bool isActive)
	{
		if (isActive)
		{
			optionDung1Lan.isChecked = false;
			optionDung10Lan.isChecked = true;
			optionDung100Lan.isChecked = false;
			m_requestData.UseTienLinhDan = UseTanHonTangLevelChienHonRequest.DungTienLinhDan.Use10;
		}
		else if (!optionDung1Lan.isChecked && !optionDung100Lan.isChecked)
		{
			optionDung10Lan.isChecked = true;
			m_requestData.UseTienLinhDan = UseTanHonTangLevelChienHonRequest.DungTienLinhDan.Use10;
		}
	}

	private void OnActivateDung100Lan(bool isActive)
	{
		if (isActive)
		{
			optionDung1Lan.isChecked = false;
			optionDung10Lan.isChecked = false;
			optionDung100Lan.isChecked = true;
			m_requestData.UseTienLinhDan = UseTanHonTangLevelChienHonRequest.DungTienLinhDan.Use100;
		}
		else if (!optionDung1Lan.isChecked && !optionDung10Lan.isChecked)
		{
			optionDung100Lan.isChecked = true;
			m_requestData.UseTienLinhDan = UseTanHonTangLevelChienHonRequest.DungTienLinhDan.Use100;
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public override void OnActive()
	{
		base.OnActive();
		if (RefUserInfo == null)
		{
			RefUserInfo = GameManager.instance.m_GameClient.UserInfo;
		}
		GUIManager.ShowGadgets(6);
		optionDung1Lan.onStateChange = OnActivateDung1Lan;
		optionDung10Lan.onStateChange = OnActivateDung10Lan;
		optionDung100Lan.onStateChange = OnActivateDung100Lan;
	}

	public void Sync(UserInfo.ChienHon chienhon, UserInfo.HeroData hero = null)
	{
		m_requestData.HonNVsNeed.Clear();
		m_requestData.ChienHonID = chienhon.ID;
		chienHonData = chienhon;
		if (hero != null)
		{
			heroData = hero;
		}
		nhanvatAvatar.Set(heroData.Name, chienHonData.DotPha, chienHonData.Level);
		NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[chienhon.Name];
		nhanVatName.text = nhanVatCfg.TenHienThi;
		if (nhanVatCfg.Hang == 3)
		{
			spPham.spriteName = "giap";
		}
		else if (nhanVatCfg.Hang == 2)
		{
			spPham.spriteName = "at";
		}
		else if (nhanVatCfg.Hang == 1)
		{
			spPham.spriteName = "binh";
		}
		nhanVatName.text = nhanVatCfg.TenHienThi;
		ChiSoNhanVat chiSoCuoi = chienhon.GetChiSoCuoi(GameManager.instance.m_GameClient.UserInfo.HuyenKhiList);
		MenhLabel.text = ((long)chiSoCuoi.Menh).ToString();
		NgoaiLabel.text = ((long)chiSoCuoi.Ngoai).ToString();
		ThanLabel.text = ((long)chiSoCuoi.ThanPhap).ToString();
		KhiLabel.text = ((long)chiSoCuoi.Noi).ToString();
		TanHonT.SetActive(true);
		foreach (Transform item in grid.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		if (GameManager.instance.m_GameClient.UserInfo.HonNhanVatList != null)
		{
			foreach (UserInfo.HonNhanVatData honNhanVat in GameManager.instance.m_GameClient.UserInfo.HonNhanVatList)
			{
				if (honNhanVat.Quantity > 0)
				{
					TanHonForChienHon component = ((GameObject)Object.Instantiate(TanHonT)).GetComponent<TanHonForChienHon>();
					component.transform.parent = grid.transform;
					component.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
					component.transform.localPosition = Vector3.zero;
					component.Set(honNhanVat);
				}
			}
			grid.repositionNow = true;
		}
		if (GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList != null)
		{
			UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_TIEN_LINH_DAN");
			if (vatPhamTieuThuData != null)
			{
				tienLinhDanAvatar.Set(vatPhamTieuThuData);
			}
			else
			{
				vatPhamTieuThuData = new UserInfo.VatPhamTieuThuData();
				vatPhamTieuThuData.Name = "VP_TIEN_LINH_DAN";
				tienLinhDanAvatar.Set(vatPhamTieuThuData);
			}
		}
		else
		{
			tienLinhDanAvatar.Set("empty");
		}
		TanHonT.SetActive(false);
		UpdateExpBar();
	}

	private void UpdateExpBar()
	{
		if (chienHonData.MaxExp == 0)
		{
			chienHonData.MaxExp = ConfigManager.instance.OtherConfig.GetChienHonMaxExpByLevel(1);
		}
		expBarLabel.text = string.Format("{0}/{1}", chienHonData.Exp, chienHonData.MaxExp);
		expBarSlider.sliderValue = (float)chienHonData.Exp / (float)chienHonData.MaxExp;
	}

	public void TanHonAvatar_OnClick(int id, string name)
	{
		NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[name];
		chienHonData = UserInfo.ChienHonGetExp(chienHonData, nhanVatCfg.ExpTanHon);
		nhanvatAvatar.Set(heroData.Name, chienHonData.DotPha, chienHonData.Level);
		UpdateExpBar();
		UseTanHonTangLevelChienHonRequest.HonNhanVat honNhanVat = m_requestData.HonNVsNeed.Find((UseTanHonTangLevelChienHonRequest.HonNhanVat e) => e.ID == id);
		if (honNhanVat == null)
		{
			honNhanVat = new UseTanHonTangLevelChienHonRequest.HonNhanVat();
			honNhanVat.ID = id;
			m_requestData.HonNVsNeed.Add(honNhanVat);
		}
		honNhanVat.Count++;
	}

	public void OnBtnThangCapChienHon()
	{
		m_requestData.ChienHonID = chienHonData.ID;
		GameManager.instance.m_GameClient.RequestUseTanHonTangLevelChienHon(m_requestData);
	}
}
