using UnityEngine;

public class DeTuBeQuanItem : MonoBehaviour
{
	public NhanVatAvatar avatar;

	public UILabel nhanVatName;

	public UISprite duyenIcon;

	public UILabel duyenLabel;

	public UserInfo.HeroData m_Data;

	public void Set(UserInfo.HeroData data)
	{
		if (data == null)
		{
			return;
		}
		m_Data = data;
		avatar.Set(m_Data.Name);
		NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[data.Name];
		nhanVatName.text = nhanVatCfg.TenHienThi;
		if (nhanVatCfg.DuyenPhan.Count < 6)
		{
			return;
		}
		switch (nhanVatCfg.DuyenPhan[5].ChiSoDuyen)
		{
		case ChiSoDuyenPhan.Khi:
			duyenIcon.spriteName = "duyen_khi";
			break;
		case ChiSoDuyenPhan.Menh:
			duyenIcon.spriteName = "duyen_menh";
			break;
		case ChiSoDuyenPhan.Ngoai:
			duyenIcon.spriteName = "duyen_ngoai";
			break;
		case ChiSoDuyenPhan.ThanPhap:
			duyenIcon.spriteName = "duyen_than";
			break;
		case ChiSoDuyenPhan.DoDon:
			duyenIcon.spriteName = "duyen_dodon";
			break;
		case ChiSoDuyenPhan.Ne:
			duyenIcon.spriteName = "duyen_ne";
			break;
		case ChiSoDuyenPhan.Bao:
			duyenIcon.spriteName = "duyen_bao";
			break;
		}
		string empty = string.Empty;
		empty = empty + nhanVatCfg.DuyenPhan[5].TenHienThi + ": ";
		string text;
		if (nhanVatCfg.DuyenPhan[5].LoaiDuyenPhan == LoaiDuyenPhan.CungDoi)
		{
			empty = empty + Localization.instance.Get("CungDoiDuyenPhanLabel") + " ";
			if (nhanVatCfg.DuyenPhan[5].DoiTuong != null && nhanVatCfg.DuyenPhan[5].DoiTuong.Count > 0)
			{
				for (int i = 0; i < nhanVatCfg.DuyenPhan[5].DoiTuong.Count; i++)
				{
					NhanVatCfg nhanVatCfg2 = ConfigManager.instance.m_dicNhanVats[nhanVatCfg.DuyenPhan[5].DoiTuong[i]];
					if (nhanVatCfg2 != null)
					{
						empty = ((i != nhanVatCfg.DuyenPhan[5].DoiTuong.Count - 1) ? (empty + nhanVatCfg2.TenHienThi + ", ") : (empty + nhanVatCfg2.TenHienThi + " "));
					}
				}
			}
		}
		else if (nhanVatCfg.DuyenPhan[5].LoaiDuyenPhan == LoaiDuyenPhan.TrangBiDo)
		{
			empty = empty + Localization.instance.Get("TrangBiDoDuyenPhanLabel") + " ";
			if (nhanVatCfg.DuyenPhan[5].DoiTuong != null && nhanVatCfg.DuyenPhan[5].DoiTuong.Count > 0)
			{
				for (int j = 0; j < nhanVatCfg.DuyenPhan[5].DoiTuong.Count; j++)
				{
					TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[nhanVatCfg.DuyenPhan[5].DoiTuong[j]];
					if (trangBiCfg != null)
					{
						if (j == nhanVatCfg.DuyenPhan[5].DoiTuong.Count - 1)
						{
							empty = empty + trangBiCfg.TenHienThi + " ";
							continue;
						}
						text = empty;
						empty = text + trangBiCfg.TenHienThi + " " + Localization.instance.Get("OrMess") + " ";
					}
				}
			}
		}
		text = empty;
		empty = text + Localization.instance.Get("DuocNhanMess") + " + " + nhanVatCfg.DuyenPhan[5].HeSo + "% ";
		switch (nhanVatCfg.DuyenPhan[5].ChiSoDuyen)
		{
		case ChiSoDuyenPhan.Khi:
			empty += Localization.instance.Get("NoiTitle");
			break;
		case ChiSoDuyenPhan.Menh:
			empty += Localization.instance.Get("MenhTitle");
			break;
		case ChiSoDuyenPhan.Ngoai:
			empty += Localization.instance.Get("NgoaiTitle");
			break;
		case ChiSoDuyenPhan.ThanPhap:
			empty += Localization.instance.Get("ThanPhapTitle");
			break;
		case ChiSoDuyenPhan.DoDon:
			empty += Localization.instance.Get("DoDonTitle");
			break;
		case ChiSoDuyenPhan.Ne:
			empty += Localization.instance.Get("NeTitle");
			break;
		case ChiSoDuyenPhan.Bao:
			empty += Localization.instance.Get("BaoTitle");
			break;
		}
		duyenLabel.text = empty;
	}

	public void OnBeQuanClick()
	{
		UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_TAY_TUY_DAN");
		int vatPhamID = 0;
		if (vatPhamTieuThuData != null)
		{
			vatPhamID = vatPhamTieuThuData.ID;
		}
		BeQuanRequest beQuanRequest = new BeQuanRequest();
		beQuanRequest.HeroID = m_Data.HID;
		beQuanRequest.VatPhamID = vatPhamID;
		GameManager.instance.m_GameClient.RequestBeQuanDeTu(beQuanRequest);
	}
}
