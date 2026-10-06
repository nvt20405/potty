using UnityEngine;

public class ScreenBoiDuongChienHonConfirm : ScreenBase
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

	public UILabel boiDuongDanValueConLai;

	public UIButton btnTiepNhan;

	public UIButton btnKhongNhan;

	private int soLuongBoiDuongDanSuDung;

	private UserInfo.HeroData m_HeroData;

	private UserInfo.ChienHon m_chienHonData;

	private StartBoiDuongChienHonResponse responseRequest;

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
		gameClient.RequestEndBoiDuongChienHon(m_chienHonData.ID);
	}

	public void btnBack_onClick(GameObject go)
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenBoiDuongChienHon);
	}

	public void openScreenBoiDuong()
	{
		ScreenBoiDuongChienHon screenBoiDuongChienHon = GUIManager.getScreen(GAME_SCREEN.ScreenBoiDuongChienHon) as ScreenBoiDuongChienHon;
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenBoiDuongChienHon);
	}

	public override void OnActive()
	{
		if (m_HeroData != null)
		{
		}
	}

	public void displayInfo(UserInfo.ChienHon chienHonData, UserInfo.HeroData hero, StartBoiDuongChienHonResponse response)
	{
		responseRequest = response;
		m_HeroData = hero;
		m_chienHonData = chienHonData;
		nhanvatAva.Set(m_HeroData);
		NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[m_HeroData.Name];
		nhanVatName.text = nhanVatCfg.TenHienThi;
		menhValueLabel.text = m_chienHonData.Menh.ToString();
		khiValueLabel.text = m_chienHonData.Noi.ToString();
		thanValueLabel.text = m_chienHonData.ThanPhap.ToString();
		ngoaiValueLabel.text = m_chienHonData.Ngoai.ToString();
		menhAfterValueLabel.text = (m_chienHonData.Menh + responseRequest.MenhThayDoi).ToString();
		khiAfterValueLabel.text = (m_chienHonData.Noi + responseRequest.KhiThayDoi).ToString();
		thanAfterValueLabel.text = (m_chienHonData.ThanPhap + responseRequest.ThanThayDoi).ToString();
		ngoaiAfterValueLabel.text = (m_chienHonData.Ngoai + responseRequest.NgoaiThayDoi).ToString();
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
		UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_DICH_CAN_DAN");
		if (vatPhamTieuThuData != null)
		{
			boiDuongDanValueConLai.text = vatPhamTieuThuData.Quantity.ToString();
		}
		else
		{
			boiDuongDanValueConLai.text = "0";
		}
	}
}
