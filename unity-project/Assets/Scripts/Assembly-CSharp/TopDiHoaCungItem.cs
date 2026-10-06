using UnityEngine;

public class TopDiHoaCungItem : MonoBehaviour
{
	public UILabel lbHang;

	public UILabel lbName;

	public UILabel lbNgocHoaThachName;

	public UILabel lbSoLuongNgocHoaThach;

	public UILabel lbMinNgocHoaThach;

	public UISprite spHomQua;

	private UserInfo.ServerData.DiHoaCungTop mTopData;

	public void setData(UserInfo.ServerData.DiHoaCungTop topData, TopDiHoaCungResponse.TopDiHoaCungData userData, int hang)
	{
		if (topData != null)
		{
			mTopData = topData;
			lbHang.text = string.Format(Localization.instance.Get("HangLabel"), hang);
			if (userData == null)
			{
				lbName.text = string.Empty;
				lbNgocHoaThachName.text = string.Empty;
				lbSoLuongNgocHoaThach.text = string.Empty;
				lbMinNgocHoaThach.text = string.Format(Localization.instance.Get("CanToiThieuNgocHoaThachTopItem"), topData.MinGiaTri);
			}
			else
			{
				lbName.text = userData.DisplayName;
				lbNgocHoaThachName.text = Localization.instance.Get("NgocHoaThachLabel");
				lbSoLuongNgocHoaThach.text = userData.SoGach.ToString();
				lbMinNgocHoaThach.text = string.Empty;
			}
		}
	}

	public void btnHomQua_OnClick(GameObject go)
	{
		if (mTopData != null)
		{
			PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
			phanThuongResponse.PhanThuongList = mTopData.ListPhanThuong;
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongSeNhanDuoc"), phanThuongResponse);
		}
	}
}
