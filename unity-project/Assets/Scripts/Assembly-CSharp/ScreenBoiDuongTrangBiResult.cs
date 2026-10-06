using System.Collections.Generic;
using UnityEngine;

public class ScreenBoiDuongTrangBiResult : ScreenBase
{
	public OtherAvatar trangbiAva;

	public UILabel lbTrangBiName;

	public UILabel lbMenh1;

	public UILabel lbNgoai1;

	public UILabel lbThan1;

	public UILabel lbKhi1;

	public UILabel lbMenh2;

	public UILabel lbNgoai2;

	public UILabel lbThan2;

	public UILabel lbKhi2;

	public UILabel lbSoLuongVPCan;

	public UserInfo.TrangBiData mTrangBiData;

	private StartBoiDuongTrangBiResponse mTBResponse;

	public void setData(UserInfo.TrangBiData tbData, StartBoiDuongTrangBiResponse response)
	{
		if (tbData != null)
		{
			mTrangBiData = tbData;
		}
		if (response != null)
		{
			mTBResponse = response;
		}
	}

	public override void OnActive()
	{
		if (mTrangBiData != null)
		{
			displayInfo();
		}
	}

	public void displayInfo()
	{
		trangbiAva.Set(mTrangBiData, false, mTrangBiData.HoangKim);
		TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[mTrangBiData.Name];
		lbTrangBiName.text = trangBiCfg.TenHienThi;
		List<int> chiSoBoiDuong = trangBiCfg.GetChiSoBoiDuong(mTrangBiData);
		if (chiSoBoiDuong != null && chiSoBoiDuong.Count == 4)
		{
			UILabel uILabel = lbMenh1;
			string text = chiSoBoiDuong[0].ToString();
			lbMenh2.text = text;
			uILabel.text = text;
			UILabel uILabel2 = lbNgoai1;
			text = chiSoBoiDuong[1].ToString();
			lbNgoai2.text = text;
			uILabel2.text = text;
			UILabel uILabel3 = lbThan1;
			text = chiSoBoiDuong[2].ToString();
			lbThan2.text = text;
			uILabel3.text = text;
			UILabel uILabel4 = lbKhi1;
			text = chiSoBoiDuong[3].ToString();
			lbKhi2.text = text;
			uILabel4.text = text;
		}
		else
		{
			UILabel uILabel5 = lbMenh1;
			string text2 = "0";
			lbMenh2.text = text2;
			uILabel5.text = text2;
			UILabel uILabel6 = lbNgoai1;
			text2 = "0";
			lbNgoai2.text = text2;
			uILabel6.text = text2;
			UILabel uILabel7 = lbThan1;
			text2 = "0";
			lbThan2.text = text2;
			uILabel7.text = text2;
			UILabel uILabel8 = lbKhi1;
			text2 = "0";
			lbKhi2.text = text2;
			uILabel8.text = text2;
		}
		if (mTBResponse != null)
		{
			switch (mTBResponse.LoaiCong)
			{
			case ChiSoCoBan.Menh:
				lbMenh2.text = chiSoBoiDuong[0] + mTBResponse.MenhThayDoi + "[006400]   +" + mTBResponse.MenhThayDoi;
				break;
			case ChiSoCoBan.Ngoai:
				lbNgoai2.text = chiSoBoiDuong[1] + mTBResponse.NgoaiThayDoi + "[006400]   +" + mTBResponse.NgoaiThayDoi;
				break;
			case ChiSoCoBan.ThanPhap:
				lbThan2.text = chiSoBoiDuong[2] + mTBResponse.ThanThayDoi + "[006400]   +" + mTBResponse.ThanThayDoi;
				break;
			case ChiSoCoBan.Noi:
				lbKhi2.text = chiSoBoiDuong[3] + mTBResponse.KhiThayDoi + "[006400]   +" + mTBResponse.KhiThayDoi;
				break;
			}
			switch (mTBResponse.LoaiTru)
			{
			case ChiSoCoBan.Menh:
				lbMenh2.text = chiSoBoiDuong[0] + mTBResponse.MenhThayDoi + "[FF0000]    " + mTBResponse.MenhThayDoi;
				break;
			case ChiSoCoBan.Ngoai:
				lbNgoai2.text = chiSoBoiDuong[1] + mTBResponse.NgoaiThayDoi + "[FF0000]    " + mTBResponse.NgoaiThayDoi;
				break;
			case ChiSoCoBan.ThanPhap:
				lbThan2.text = chiSoBoiDuong[2] + mTBResponse.ThanThayDoi + "[FF0000]    " + mTBResponse.ThanThayDoi;
				break;
			case ChiSoCoBan.Noi:
				lbKhi2.text = chiSoBoiDuong[3] + mTBResponse.KhiThayDoi + "[FF0000]    " + mTBResponse.KhiThayDoi;
				break;
			}
		}
		lbSoLuongVPCan.text = ConfigManager.instance.m_dicVatPhamTieuThu["VP_NGU_SAC_BAO_THACH"].TenHienThi + ": " + GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_NGU_SAC_BAO_THACH").Quantity;
	}

	public void btnBack_OnClick(GameObject go)
	{
		if (mTrangBiData != null)
		{
			ScreenBoiDuongTrangBiHoangKim screenBoiDuongTrangBiHoangKim = GUIManager.getScreen(GAME_SCREEN.ScreenBoiDuongTrangBiHoangKim) as ScreenBoiDuongTrangBiHoangKim;
			screenBoiDuongTrangBiHoangKim.displayInfo(mTrangBiData);
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenBoiDuongTrangBiHoangKim);
		}
	}

	public void btnNhan_OnClick(GameObject go)
	{
		EndBoiDuongTrangBiRequest endBoiDuongTrangBiRequest = new EndBoiDuongTrangBiRequest();
		endBoiDuongTrangBiRequest.TrangBiID = mTrangBiData.ID;
		GameManager.instance.m_GameClient.RequestEndBoiDuongTrangBi(endBoiDuongTrangBiRequest);
	}

	public void updateResult(EndBoiDuongTrangBiResponse response)
	{
		mTrangBiData = GameManager.instance.m_GameClient.UserInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.ID == response.TrangBiID);
		if (mTrangBiData != null)
		{
			ScreenBoiDuongTrangBiHoangKim screenBoiDuongTrangBiHoangKim = GUIManager.getScreen(GAME_SCREEN.ScreenBoiDuongTrangBiHoangKim) as ScreenBoiDuongTrangBiHoangKim;
			screenBoiDuongTrangBiHoangKim.displayInfo(mTrangBiData);
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenBoiDuongTrangBiHoangKim);
		}
	}
}
