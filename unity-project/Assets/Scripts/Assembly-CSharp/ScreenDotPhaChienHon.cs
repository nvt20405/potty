public class ScreenDotPhaChienHon : ScreenBase
{
	public UILabel MenhLabel_1;

	public UILabel NgoaiLabel_1;

	public UILabel ThanLabel_1;

	public UILabel KhiLabel_1;

	public UILabel MenhLabel_2;

	public UILabel NgoaiLabel_2;

	public UILabel ThanLabel_2;

	public UILabel KhiLabel_2;

	public NhanVatAvatar nhanvatAvatar;

	public UILabel nhanVatName;

	public NhanVatAvatar tanHonAvatarNeed;

	public UILabel SoluongTanHonNeedLabel;

	public OtherAvatar tienLinhDanAvatar;

	public UISprite spPham;

	public UILabel datCapToiDaLabel;

	public UILabel desc;

	public UILabel SoluongTienLinhDanNeedLabel;

	public UserInfo.HeroData heroData { get; set; }

	public UserInfo.ChienHon chienHonData { get; set; }

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void btnBack_OnClick()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenChienHon);
	}

	public override void OnActive()
	{
		base.OnActive();
	}

	public void Sync(UserInfo.ChienHon chienhon, UserInfo.HeroData hero = null)
	{
		chienHonData = chienhon;
		if (hero != null)
		{
			heroData = hero;
		}
		nhanvatAvatar.Set(heroData.Name, chienHonData.DotPha, chienHonData.Level);
		UserInfo.HonNhanVatData honNhanVatData = GameManager.instance.m_GameClient.UserInfo.HonNhanVatList.Find((UserInfo.HonNhanVatData e) => e.Name == chienhon.Name);
		if (honNhanVatData == null)
		{
			honNhanVatData = new UserInfo.HonNhanVatData();
		}
		tanHonAvatarNeed.Set(honNhanVatData, false, honNhanVatData.Quantity);
		UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_CHIEN_THAN_DAN");
		if (vatPhamTieuThuData == null)
		{
			vatPhamTieuThuData = new UserInfo.VatPhamTieuThuData();
			vatPhamTieuThuData.Name = "VP_CHIEN_THAN_DAN";
		}
		tienLinhDanAvatar.Set(vatPhamTieuThuData);
		NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[chienhon.Name];
		ChienHonCfg chienHonCfg = ConfigManager.instance.m_dicChienHons[chienhon.Name];
		desc.text = chienHonCfg.Mota;
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
		nhanVatName.text = chienHonCfg.TenHienThi;
		ChiSoNhanVat chiSoCuoi = chienhon.GetChiSoCuoi(GameManager.instance.m_GameClient.UserInfo.HuyenKhiList);
		MenhLabel_1.text = ((long)chiSoCuoi.Menh).ToString();
		NgoaiLabel_1.text = ((long)chiSoCuoi.Ngoai).ToString();
		ThanLabel_1.text = ((long)chiSoCuoi.ThanPhap).ToString();
		KhiLabel_1.text = ((long)chiSoCuoi.Noi).ToString();
		if (chienhon.DotPha < ConfigManager.instance.OtherConfig.ChienHonDotPhaChiSoCfg.Count - 1)
		{
			ChiSoNhanVat chiSoByHuyenKhi = chienhon.GetChiSoByHuyenKhi(GameManager.instance.m_GameClient.UserInfo.HuyenKhiList);
			MenhLabel_2.text = ((long)(chiSoByHuyenKhi.Menh + (float)chienhon.MenhByDotPha(chienhon.DotPha + 1))).ToString();
			NgoaiLabel_2.text = ((long)(chiSoByHuyenKhi.Ngoai + (float)chienhon.NgoaiByDotPha(chienhon.DotPha + 1))).ToString();
			ThanLabel_2.text = ((long)(chiSoByHuyenKhi.ThanPhap + (float)chienhon.ThanByDotPha(chienhon.DotPha + 1))).ToString();
			KhiLabel_2.text = ((long)(chiSoByHuyenKhi.Noi + (float)chienhon.KhiByDotPha(chienhon.DotPha + 1))).ToString();
			SoluongTanHonNeedLabel.gameObject.SetActive(true);
			SoluongTanHonNeedLabel.text = string.Format(Localization.instance.Get("VatPhamDotPhaCanLabel"), ConfigManager.instance.OtherConfig.ChienHonDotPhaTanHonNeedCfg[chienhon.DotPha]);
			datCapToiDaLabel.gameObject.SetActive(false);
		}
		else
		{
			MenhLabel_2.text = ((long)chiSoCuoi.Menh).ToString();
			NgoaiLabel_2.text = ((long)chiSoCuoi.Ngoai).ToString();
			ThanLabel_2.text = ((long)chiSoCuoi.ThanPhap).ToString();
			KhiLabel_2.text = ((long)chiSoCuoi.Noi).ToString();
			SoluongTanHonNeedLabel.gameObject.SetActive(true);
			datCapToiDaLabel.gameObject.SetActive(true);
		}
		SoluongTienLinhDanNeedLabel.text = string.Format(Localization.instance.Get("VatPhamDotPhaCanLabel"), ConfigManager.instance.OtherConfig.ChienHonDotPhaChienThanDanNeed);
	}

	public void OnDotPhaClick()
	{
		GameManager.instance.m_GameClient.RequestDotPhaChienHon(chienHonData.ID);
	}
}
