using System.Collections.Generic;
using UnityEngine;

public class ScreenBoiDuongTrangBiHoangKim : ScreenBase
{
	public OtherAvatar trangbiAva;

	public OtherAvatar vpCanAva;

	public UILabel lbTrangBiName;

	public UILabel lbChiSoMenh;

	public UILabel lbChiSoNgoai;

	public UILabel lbChiSoThan;

	public UILabel lbChiSoKhi;

	private UserInfo.TrangBiData mTrangBiData;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void displayInfo(UserInfo.TrangBiData trangBiData)
	{
		if (trangBiData != null)
		{
			mTrangBiData = trangBiData;
			trangbiAva.Set(mTrangBiData, false, mTrangBiData.HoangKim);
			TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[mTrangBiData.Name];
			lbTrangBiName.text = trangBiCfg.TenHienThi;
			List<int> chiSoBoiDuong = trangBiCfg.GetChiSoBoiDuong(mTrangBiData);
			if (chiSoBoiDuong != null && chiSoBoiDuong.Count == 4)
			{
				lbChiSoMenh.text = chiSoBoiDuong[0].ToString();
				lbChiSoNgoai.text = chiSoBoiDuong[1].ToString();
				lbChiSoThan.text = chiSoBoiDuong[2].ToString();
				lbChiSoKhi.text = chiSoBoiDuong[3].ToString();
			}
			else
			{
				lbChiSoMenh.text = "0";
				lbChiSoNgoai.text = "0";
				lbChiSoThan.text = "0";
				lbChiSoKhi.text = "0";
			}
			UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_NGU_SAC_BAO_THACH");
			if (vatPhamTieuThuData == null)
			{
				vatPhamTieuThuData = new UserInfo.VatPhamTieuThuData();
				vatPhamTieuThuData.Name = "VP_NGU_SAC_BAO_THACH";
				vatPhamTieuThuData.Quantity = 0;
			}
			vpCanAva.Set(vatPhamTieuThuData);
			VatPhamTieuThuCfg vatPhamTieuThuCfg = ConfigManager.instance.m_dicVatPhamTieuThu["VP_NGU_SAC_BAO_THACH"];
		}
	}

	public void btnLuyenHoa_OnClick()
	{
		BoiDuongTrangBiHoangKimRequest boiDuongTrangBiHoangKimRequest = new BoiDuongTrangBiHoangKimRequest();
		boiDuongTrangBiHoangKimRequest.loai = BoiDuongTrangBiHoangKimRequest.LoaiLuyenHoa.BoiDuong1Lan;
		boiDuongTrangBiHoangKimRequest.TrangBiID = mTrangBiData.ID;
		GameManager.instance.m_GameClient.RequestStartBoiDuongTrangBi(boiDuongTrangBiHoangKimRequest);
	}

	public void btnLuyenHoa10Lan_OnClick()
	{
		BoiDuongTrangBiHoangKimRequest boiDuongTrangBiHoangKimRequest = new BoiDuongTrangBiHoangKimRequest();
		boiDuongTrangBiHoangKimRequest.loai = BoiDuongTrangBiHoangKimRequest.LoaiLuyenHoa.BoiDuong10Lan;
		boiDuongTrangBiHoangKimRequest.TrangBiID = mTrangBiData.ID;
		GameManager.instance.m_GameClient.RequestStartBoiDuongTrangBi(boiDuongTrangBiHoangKimRequest);
	}

	public void btnBack_OnClick(GameObject go)
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenListTrangBiHoangKim);
	}

	public void openScreenBDConfirm(StartBoiDuongTrangBiResponse response)
	{
		ScreenBoiDuongTrangBiResult screenBoiDuongTrangBiResult = GUIManager.getScreen(GAME_SCREEN.ScreenBoiDuongTrangBiResult) as ScreenBoiDuongTrangBiResult;
		screenBoiDuongTrangBiResult.setData(mTrangBiData, response);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenBoiDuongTrangBiResult);
	}
}
