using UnityEngine;

public class VatPhamMarketItem : MonoBehaviour
{
	public OtherAvatar vatPhamAvatar;

	public UILabel lbVatPhamName;

	public UILabel lbDescription;

	public UILabel lbGiaBan;

	public UIButton btnMua;

	public int vatPhamID;

	public VatPhamTieuThuCfg m_Data;

	public void Set(VatPhamTieuThuCfg data)
	{
		if (data != null)
		{
			m_Data = data;
			displayInfo();
		}
	}

	public void displayInfo()
	{
		lbVatPhamName.text = m_Data.TenHienThi;
		lbDescription.text = m_Data.MoTa;
		lbGiaBan.text = m_Data.GiaVang.ToString();
		UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == m_Data.Name);
		if (vatPhamTieuThuData != null)
		{
			if (vatPhamTieuThuData.Quantity > 0)
			{
				vatPhamAvatar.Set(vatPhamTieuThuData.Name, 0, -1, vatPhamTieuThuData.Quantity);
			}
			else
			{
				vatPhamAvatar.Set(vatPhamTieuThuData);
			}
			EGDebug.Log("quantity : " + vatPhamTieuThuData.Quantity);
		}
		else
		{
			vatPhamTieuThuData = new UserInfo.VatPhamTieuThuData();
			vatPhamTieuThuData.Name = m_Data.Name;
			vatPhamAvatar.Set(vatPhamTieuThuData);
		}
	}
}
