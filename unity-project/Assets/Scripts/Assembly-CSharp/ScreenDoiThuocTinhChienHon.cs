using UnityEngine;

public class ScreenDoiThuocTinhChienHon : ScreenBase
{
	public UILabel MenhLabel;

	public UILabel NgoaiLabel;

	public UILabel ThanLabel;

	public UILabel KhiLabel;

	public NhanVatAvatar nhanvatAvatar;

	public UILabel nhanVatName;

	public UILabel bacNeedLabel;

	public OtherAvatar huyenNguyenDanAvatar;

	public UILabel Desc;

	public TweenScale MenhPingpong;

	public TweenScale NgoaiPingpong;

	public TweenScale ThanPingpong;

	public TweenScale KhiPingpong;

	public GameObject buffMenhIcon;

	public GameObject buffNgoaiIcon;

	public GameObject buffThanIcon;

	public GameObject buffNoiIcon;

	public UILabel huyenNguyenDanNeedLabel;

	public UILabel thuocTinhHoTro;

	public UserInfo.HeroData heroData { get; set; }

	public UserInfo.ChienHon chienHonData { get; set; }

	private void DisplayBuff()
	{
		if (chienHonData != null)
		{
			if (chienHonData.LoaiBuff == 0)
			{
				buffMenhIcon.SetActive(true);
				buffNgoaiIcon.SetActive(false);
				buffThanIcon.SetActive(false);
				buffNoiIcon.SetActive(false);
			}
			else if (chienHonData.LoaiBuff == 1)
			{
				buffMenhIcon.SetActive(false);
				buffNgoaiIcon.SetActive(true);
				buffThanIcon.SetActive(false);
				buffNoiIcon.SetActive(false);
			}
			else if (chienHonData.LoaiBuff == 2)
			{
				buffMenhIcon.SetActive(false);
				buffNgoaiIcon.SetActive(false);
				buffThanIcon.SetActive(true);
				buffNoiIcon.SetActive(false);
			}
			else
			{
				buffMenhIcon.SetActive(false);
				buffNgoaiIcon.SetActive(false);
				buffThanIcon.SetActive(false);
				buffNoiIcon.SetActive(true);
			}
		}
	}

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
		huyenNguyenDanNeedLabel.text = string.Format(Localization.instance.Get("huyenNguyenDanNeed"), ConfigManager.instance.OtherConfig.HuyenNguyenDanNeedChangeBuff);
		chienHonData = chienhon;
		if (hero != null)
		{
			heroData = hero;
		}
		nhanvatAvatar.Set(heroData.Name, chienHonData.DotPha, chienHonData.Level);
		UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_HUYEN_NGUYEN_DAN");
		if (vatPhamTieuThuData == null)
		{
			vatPhamTieuThuData = new UserInfo.VatPhamTieuThuData();
			vatPhamTieuThuData.Name = "VP_HUYEN_NGUYEN_DAN";
		}
		huyenNguyenDanAvatar.Set(vatPhamTieuThuData);
		NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[chienhon.Name];
		nhanVatName.text = nhanVatCfg.TenHienThi;
		ChiSoNhanVat chiSoCuoi = chienhon.GetChiSoCuoi(GameManager.instance.m_GameClient.UserInfo.HuyenKhiList);
		MenhLabel.text = ((long)chiSoCuoi.Menh).ToString();
		NgoaiLabel.text = ((long)chiSoCuoi.Ngoai).ToString();
		ThanLabel.text = ((long)chiSoCuoi.ThanPhap).ToString();
		KhiLabel.text = ((long)chiSoCuoi.Noi).ToString();
		thuocTinhHoTro.text = Localization.instance.Get("thuocTinhHoTro_" + chienHonData.LoaiBuff);
		if (chienHonData.LoaiBuff == 0)
		{
			MenhLabel.color = Color.red;
			NgoaiLabel.color = new Color(92f / 255f, 12f / 255f, 0f);
			ThanLabel.color = new Color(92f / 255f, 12f / 255f, 0f);
			KhiLabel.color = new Color(92f / 255f, 12f / 255f, 0f);
			MenhPingpong.enabled = true;
			NgoaiPingpong.enabled = false;
			ThanPingpong.enabled = false;
			KhiPingpong.enabled = false;
		}
		else if (chienHonData.LoaiBuff == 1)
		{
			NgoaiLabel.color = Color.red;
			MenhLabel.color = new Color(92f / 255f, 12f / 255f, 0f);
			ThanLabel.color = new Color(92f / 255f, 12f / 255f, 0f);
			KhiLabel.color = new Color(92f / 255f, 12f / 255f, 0f);
			MenhPingpong.enabled = false;
			NgoaiPingpong.enabled = true;
			ThanPingpong.enabled = false;
			KhiPingpong.enabled = false;
		}
		else if (chienHonData.LoaiBuff == 2)
		{
			ThanLabel.color = Color.red;
			MenhLabel.color = new Color(92f / 255f, 12f / 255f, 0f);
			NgoaiLabel.color = new Color(92f / 255f, 12f / 255f, 0f);
			KhiLabel.color = new Color(92f / 255f, 12f / 255f, 0f);
			MenhPingpong.enabled = false;
			NgoaiPingpong.enabled = false;
			ThanPingpong.enabled = true;
			KhiPingpong.enabled = false;
		}
		else
		{
			KhiLabel.color = Color.red;
			MenhLabel.color = new Color(92f / 255f, 12f / 255f, 0f);
			NgoaiLabel.color = new Color(92f / 255f, 12f / 255f, 0f);
			ThanLabel.color = new Color(92f / 255f, 12f / 255f, 0f);
			MenhPingpong.enabled = false;
			NgoaiPingpong.enabled = false;
			ThanPingpong.enabled = false;
			KhiPingpong.enabled = true;
		}
		DisplayBuff();
		bacNeedLabel.text = ConfigManager.instance.OtherConfig.HuyenNguyenDanNeedChangeBuff.ToString();
	}

	public void OnDoiThuocTinhClick()
	{
		GameManager.instance.m_GameClient.RequestChienHonChangeBuff(chienHonData.ID);
	}
}
