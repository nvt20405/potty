using System.Collections;
using UnityEngine;

public class ScreenChuyenSinh : ScreenBase
{
	private UserInfo.HeroData m_HeroData;

	private UserInfo.HeroData m_HeroDataCS;

	public NhanVatAvatar avatar1;

	public NhanVatAvatar avatar2;

	public UILabel nameNhanVat1;

	public UILabel nameNhanVat2;

	public UILabel nameVatPhamCan;

	public UILabel soLuongVPCan;

	public OtherAvatar avatarVatPhamCan;

	public UILabel nameVPNhanDuoc;

	public UILabel soLuongVPNhanDuoc;

	public OtherAvatar avatarVatPhamNhanDuoc;

	public GameObject goButtonParticle;

	public GameObject goChuyenSinhParticle;

	public GameObject btnChuyenSinh;

	public GameObject grpAvaChuyenSinh;

	public GameObject grpAvaSauChuyenSinh;

	public NhanVatAvatar avaSauCS;

	public UILabel nameNhanVatSauCS;

	private void Start()
	{
		goButtonParticle.GetComponent<ParticleSystem>().Play();
	}

	public void Set(UserInfo.HeroData data)
	{
		if (data != null)
		{
			m_HeroData = data;
		}
	}

	public override void OnActive()
	{
		goChuyenSinhParticle.SetActive(false);
		if (m_HeroData != null)
		{
			displayInfo();
		}
	}

	public void displayInfo()
	{
		grpAvaChuyenSinh.gameObject.SetActive(true);
		grpAvaSauChuyenSinh.gameObject.SetActive(false);
		btnChuyenSinh.gameObject.SetActive(true);
		avatar1.Set(m_HeroData);
		NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[m_HeroData.Name];
		UILabel uILabel = nameNhanVat1;
		string tenHienThi = nhanVatCfg.TenHienThi;
		nameNhanVatSauCS.text = tenHienThi;
		tenHienThi = tenHienThi;
		nameNhanVat2.text = tenHienThi;
		uILabel.text = tenHienThi;
		m_HeroDataCS = new UserInfo.HeroData(m_HeroData);
		m_HeroDataCS.HID = 0;
		m_HeroDataCS.ChuyenSinh = 1;
		m_HeroDataCS.Level = 1;
		m_HeroDataCS.CapDotPha = 0;
		m_HeroDataCS.MenhBoiDuong = 0;
		m_HeroDataCS.NgoaiBoiDuong = 0;
		m_HeroDataCS.ThanBoiDuong = 0;
		m_HeroDataCS.KhiBoiDuong = 0;
		m_HeroDataCS.ChiSoGoc = null;
		avatar2.Set(m_HeroDataCS);
		if (ConfigManager.instance.m_dicVatPhamTieuThu.ContainsKey("VP_LENH_BAI_CHUYEN_SINH"))
		{
			VatPhamTieuThuCfg vatPhamTieuThuCfg = ConfigManager.instance.m_dicVatPhamTieuThu["VP_LENH_BAI_CHUYEN_SINH"];
			nameVatPhamCan.text = vatPhamTieuThuCfg.TenHienThi;
			avatarVatPhamCan.Set(vatPhamTieuThuCfg.Name);
		}
		else
		{
			nameVatPhamCan.text = string.Empty;
			avatarVatPhamCan.Set(string.Empty);
		}
		if (ConfigManager.instance.m_dicVatPhamTieuThu.ContainsKey("VP_HOA_THAN_DAN"))
		{
			VatPhamTieuThuCfg vatPhamTieuThuCfg2 = ConfigManager.instance.m_dicVatPhamTieuThu["VP_HOA_THAN_DAN"];
			nameVPNhanDuoc.text = vatPhamTieuThuCfg2.TenHienThi;
			avatarVatPhamNhanDuoc.Set(vatPhamTieuThuCfg2.Name);
		}
		else
		{
			nameVPNhanDuoc.text = string.Empty;
			avatarVatPhamNhanDuoc.Set(string.Empty);
		}
		OtherCfg.ChuyenSinhCfg chuyenSinhConfig = ConfigManager.instance.OtherConfig.ChuyenSinhConfig;
		int num = 0;
		num = ((chuyenSinhConfig != null) ? chuyenSinhConfig.LenhBaiChuyenSinhRequired : 0);
		UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_LENH_BAI_CHUYEN_SINH");
		string text = Localization.instance.Get("HienCoLabel") + ": ";
		if (vatPhamTieuThuData != null)
		{
			tenHienThi = text;
			text = tenHienThi + vatPhamTieuThuData.Quantity + "/" + num;
		}
		else
		{
			tenHienThi = text;
			text = tenHienThi + 0 + "/" + num;
		}
		soLuongVPCan.text = text;
		long chuyenSinhExpTraLai = ConfigManager.instance.GetChuyenSinhExpTraLai(m_HeroData);
		int num2 = (int)chuyenSinhExpTraLai / 1000000;
		soLuongVPNhanDuoc.text = Localization.instance.Get("SoLuongLabel") + ": " + num2;
	}

	public void OnAvatar1Click(GameObject go)
	{
		if (m_HeroData != null)
		{
			PopupNhanVat.CreateByNormalScreens(GameManager.instance.m_GameClient.UserInfo, m_HeroData);
		}
	}

	public void OnAvatar2Click(GameObject go)
	{
		if (m_HeroDataCS != null)
		{
			PopupNhanVat.CreateByNormalScreens(GameManager.instance.m_GameClient.UserInfo, m_HeroDataCS);
		}
	}

	public void btnChuyenSinh_OnClick(GameObject go)
	{
		PopupYesNo.Create(Localization.instance.Get("ChuyenSinhConfirmMess"), Localization.instance.Get("DongYLabelBtn"), Localization.instance.Get("TuChoiLabelBtn"), OnChuyenSinhNow, null);
	}

	public void OnChuyenSinhNow()
	{
		if (m_HeroData != null)
		{
			GameManager.instance.m_GameClient.RequestChuyenSinhDeTu(m_HeroData.HID);
		}
	}

	public void btnBack_OnClick(GameObject go)
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenDeTu);
	}

	public void btnHelp_OnClick()
	{
		ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
		screenHelpInfo.setByLevel(0, 10);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
	}

	public void updateView(PhanThuongResponse phanThuongResponse)
	{
		m_HeroData = GameManager.instance.m_GameClient.UserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == m_HeroData.HID);
		goChuyenSinhParticle.gameObject.SetActive(true);
		goChuyenSinhParticle.GetComponent<ParticleSystem>().Simulate(0f, true, true);
		goChuyenSinhParticle.GetComponent<ParticleSystem>().Play();
		StartCoroutine(updateInfoAgain(1.8f, phanThuongResponse));
	}

	public IEnumerator updateInfoAgain(float waitTime, PhanThuongResponse phanThuong)
	{
		yield return new WaitForSeconds(waitTime);
		OtherCfg.ChuyenSinhCfg csConfig = ConfigManager.instance.OtherConfig.ChuyenSinhConfig;
		int soLuongYeuCau = ((csConfig != null) ? csConfig.LenhBaiChuyenSinhRequired : 0);
		UserInfo.VatPhamTieuThuData dataLenhBai = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_LENH_BAI_CHUYEN_SINH");
		string temp = Localization.instance.Get("HienCoLabel") + ": ";
		if (dataLenhBai != null)
		{
			string text = temp;
			temp = text + dataLenhBai.Quantity + "/" + soLuongYeuCau;
		}
		else
		{
			string text2 = temp;
			temp = text2 + 0 + "/" + soLuongYeuCau;
		}
		soLuongVPCan.text = temp;
		avaSauCS.Set(m_HeroData);
		btnChuyenSinh.gameObject.SetActive(false);
		grpAvaChuyenSinh.gameObject.SetActive(false);
		grpAvaSauChuyenSinh.gameObject.SetActive(true);
		PopupDanhSachPhanThuong.Create(Localization.instance.Get("TitlePopUpPTChuyenSinh"), Localization.instance.Get("MessPopUpPTChuyenSinh"), phanThuong);
	}
}
