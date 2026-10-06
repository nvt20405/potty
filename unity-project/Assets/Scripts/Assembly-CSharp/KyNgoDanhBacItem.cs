using UnityEngine;

public class KyNgoDanhBacItem : MonoBehaviour
{
	public PhanThuongItem vatPhamFrom;

	public PhanThuongItem vatPhamTo;

	public UIButton btnDoiThuong;

	public UISprite vipYeuCau;

	public int currentVIP;

	public int index;

	public XocDiaInfoResponse.XocDiaItem m_Data;

	public void Set(XocDiaInfoResponse.XocDiaItem data)
	{
		if (data != null)
		{
			m_Data = data;
			PhanThuongResponse.PhanThuong vatPhamCuoc = data.VatPhamCuoc;
			PhanThuongResponse.PhanThuong phanThuong = data.PhanThuong;
			if (vatPhamCuoc.Name == string.Empty)
			{
				vatPhamFrom.setNullValue();
				vatPhamTo.setNullValue();
			}
			else
			{
				if (vatPhamCuoc != null)
				{
					if (vatPhamCuoc.Loai == PhanThuongResponse.LoaiPhanThuong.VANG)
					{
						vatPhamFrom.setVang(vatPhamCuoc.Count, true);
					}
					else if (vatPhamCuoc.Loai == PhanThuongResponse.LoaiPhanThuong.BAC)
					{
						vatPhamFrom.setBac(vatPhamCuoc.Count, true);
					}
					else
					{
						vatPhamFrom.Set(vatPhamCuoc, false);
					}
				}
				if (phanThuong != null)
				{
					if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.VANG)
					{
						vatPhamTo.setVang(phanThuong.Count, true);
					}
					else if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.BAC)
					{
						vatPhamTo.setBac(phanThuong.Count, true);
					}
					else
					{
						vatPhamTo.Set(phanThuong, false);
					}
				}
			}
		}
		vipYeuCau.gameObject.SetActive(false);
	}

	public void setVIP(int vip)
	{
		currentVIP = vip;
		switch (vip)
		{
		case 2:
			vipYeuCau.spriteName = "icon_vip2";
			vipYeuCau.MakePixelPerfect();
			break;
		case 4:
			vipYeuCau.spriteName = "icon_vip4";
			vipYeuCau.MakePixelPerfect();
			break;
		case 6:
			vipYeuCau.spriteName = "icon_vip6";
			vipYeuCau.MakePixelPerfect();
			break;
		}
		vipYeuCau.gameObject.SetActive(true);
	}
}
