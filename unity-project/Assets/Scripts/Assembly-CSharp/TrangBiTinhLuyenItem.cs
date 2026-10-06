using UnityEngine;

public class TrangBiTinhLuyenItem : MonoBehaviour
{
	public OtherAvatar trangBiAvatar;

	public UILabel lbCount;

	private string strCodeNameRequired = string.Empty;

	private int soLuongManh;

	private UserInfo.VatPhamTieuThuData vatPhamData;

	public void setData(string codeName, int soLuongCo, int soManhYeuCau)
	{
		strCodeNameRequired = codeName;
		soLuongManh = soLuongCo;
		vatPhamData = null;
		if (!codeName.StartsWith("M") || !codeName.StartsWith("MMU"))
		{
			codeName = "M" + codeName;
		}
		trangBiAvatar.Set(codeName);
		lbCount.text = soLuongCo + "/" + soManhYeuCau;
	}

	public void setDataTayTuyDan(UserInfo.VatPhamTieuThuData vpData, int soLuongCan)
	{
		vatPhamData = vpData;
		trangBiAvatar.Set(vpData.Name);
		strCodeNameRequired = vpData.Name;
		lbCount.text = vpData.Quantity + "/" + soLuongCan;
	}

	public void OnAvatarClick()
	{
		if (vatPhamData != null && strCodeNameRequired == "VP_TAY_TUY_DAN")
		{
			PopUpVatPham.Create(vatPhamData);
		}
		else if (strCodeNameRequired != "VP_TAY_TUY_DAN")
		{
			PopUpManhTrangBi.Create(strCodeNameRequired, soLuongManh);
		}
	}
}
