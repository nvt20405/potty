using UnityEngine;

public class ScreenBoiDuongConfirm : ScreenBase
{
	public NhanVatAvatar nhanvatAva;

	public UILabel nhanVatName;

	public UILabel menhValueLabel;

	public UILabel ngoaiValueLabel;

	public UILabel khiValueLabel;

	public UILabel thanValueLabel;

	public UILabel menhAfterValueLabel;

	public UILabel ngoaiAfterValueLabel;

	public UILabel khiAfterValueLabel;

	public UILabel thanAfterValueLabel;

	public UILabel menhChangeValueLabel;

	public UILabel ngoaiChangeValueLabel;

	public UILabel khiChangeValueLabel;

	public UILabel thanChangeValueLabel;

	public UILabel boiDuongDanConLai;

	public UIButton btnTiepNhan;

	public UIButton btnKhongNhan;

	private int soLuongBoiDuongDanSuDung;

	private UserInfo.HeroData m_HeroData;

	private StartBoiDuongResponse responseRequest;

	private void Start()
	{
		UIEventListener.Get(btnKhongNhan.gameObject).onClick = btnBack_onClick;
		UIEventListener.Get(btnTiepNhan.gameObject).onClick = btnTiepNhan_onClick;
	}

	private void Update()
	{
	}

	public void btnTiepNhan_onClick(GameObject go)
	{
		GameClient gameClient = GameManager.instance.m_GameClient;
		gameClient.RequestEndBoiDuong(m_HeroData.HID);
	}

	public void btnBack_onClick(GameObject go)
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenBoiDuong);
	}

	public void openScreenBoiDuong()
	{
		ScreenBoiDuong screenBoiDuong = GUIManager.getScreen(GAME_SCREEN.ScreenBoiDuong) as ScreenBoiDuong;
		screenBoiDuong.Set(GameManager.instance.m_GameClient.UserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == m_HeroData.HID));
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenBoiDuong);
	}

	public void Set(UserInfo.HeroData data, StartBoiDuongResponse response)
	{
		if (data != null)
		{
			m_HeroData = data;
		}
		if (response != null)
		{
			responseRequest = response;
		}
	}

	public override void OnActive()
	{
		if (m_HeroData != null)
		{
			displayInfo();
		}
	}

	public void displayInfo()
	{
		nhanvatAva.Set(m_HeroData);
		NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[m_HeroData.Name];
		nhanVatName.text = nhanVatCfg.TenHienThi;
		menhValueLabel.text = m_HeroData.ChiSoGoc.Menh.ToString();
		khiValueLabel.text = m_HeroData.ChiSoGoc.Noi.ToString();
		thanValueLabel.text = m_HeroData.ChiSoGoc.ThanPhap.ToString();
		ngoaiValueLabel.text = m_HeroData.ChiSoGoc.Ngoai.ToString();
		menhAfterValueLabel.text = (m_HeroData.ChiSoGoc.Menh + responseRequest.MenhThayDoi).ToString();
		khiAfterValueLabel.text = (m_HeroData.ChiSoGoc.Noi + responseRequest.KhiThayDoi).ToString();
		thanAfterValueLabel.text = (m_HeroData.ChiSoGoc.ThanPhap + responseRequest.ThanThayDoi).ToString();
		ngoaiAfterValueLabel.text = (m_HeroData.ChiSoGoc.Ngoai + responseRequest.NgoaiThayDoi).ToString();
		if (responseRequest.LoaiCong == ChiSoCoBan.Menh)
		{
			menhChangeValueLabel.text = "[006400]+" + responseRequest.MenhThayDoi;
		}
		else if (responseRequest.LoaiTru == ChiSoCoBan.Menh)
		{
			menhChangeValueLabel.text = "[FF0000]" + responseRequest.MenhThayDoi;
		}
		else
		{
			menhChangeValueLabel.text = string.Empty;
		}
		if (responseRequest.LoaiCong == ChiSoCoBan.Noi)
		{
			khiChangeValueLabel.text = "[006400]+" + responseRequest.KhiThayDoi;
		}
		else if (responseRequest.LoaiTru == ChiSoCoBan.Noi)
		{
			khiChangeValueLabel.text = "[FF0000]" + responseRequest.KhiThayDoi;
		}
		else
		{
			khiChangeValueLabel.text = string.Empty;
		}
		if (responseRequest.LoaiCong == ChiSoCoBan.ThanPhap)
		{
			thanChangeValueLabel.text = "[006400]+" + responseRequest.ThanThayDoi;
		}
		else if (responseRequest.LoaiTru == ChiSoCoBan.ThanPhap)
		{
			thanChangeValueLabel.text = "[FF0000]" + responseRequest.ThanThayDoi;
		}
		else
		{
			thanChangeValueLabel.text = string.Empty;
		}
		if (responseRequest.LoaiCong == ChiSoCoBan.Ngoai)
		{
			ngoaiChangeValueLabel.text = "[006400]+" + responseRequest.NgoaiThayDoi;
		}
		else if (responseRequest.LoaiTru == ChiSoCoBan.Ngoai)
		{
			ngoaiChangeValueLabel.text = "[FF0000]" + responseRequest.NgoaiThayDoi;
		}
		else
		{
			ngoaiChangeValueLabel.text = string.Empty;
		}
		boiDuongDanConLai.text = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_BOI_DUONG_DAN").Quantity.ToString();
	}
}
