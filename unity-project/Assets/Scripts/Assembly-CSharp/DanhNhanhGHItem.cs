using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class DanhNhanhGHItem : MonoBehaviour
{
	public UILabel expMPLabel;

	public UILabel bacLabel;

	public UILabel deTuLabel;

	public UILabel phanThuongLabel;

	public UISprite bg;

	public UISprite topBorder;

	public UISprite botBorder;

	public float HeightBG { get; private set; }

	public void SetInfo(List<UserInfo.HeroData> listDeTu, long expMP, long bac, long expDeTu, PhanThuongResponse pt)
	{
		expMPLabel.text = string.Format("+ {0}", expMP);
		bacLabel.text = string.Format("+ {0}", bac);
		float num = 102f;
		StringBuilder stringBuilder = new StringBuilder(Localization.instance.Get("DanhNhanhGHDeTuKhieuChienLabel"));
		stringBuilder.AppendLine();
		for (int i = 0; i < listDeTu.Count; i++)
		{
			string tenHienThi = listDeTu[i].Name;
			NhanVatCfg value;
			if (ConfigManager.instance.m_dicNhanVats.TryGetValue(listDeTu[i].Name, out value) && value != null)
			{
				tenHienThi = value.TenHienThi;
			}
			if (i == 0)
			{
				stringBuilder.AppendFormat("{0} [d8231b]+{1}[-]", tenHienThi, expDeTu);
			}
			else
			{
				stringBuilder.AppendFormat(",{0} [d8231b]+{1}[-]", tenHienThi, expDeTu);
			}
		}
		deTuLabel.text = stringBuilder.ToString();
		num += (deTuLabel.relativeSize.y - 1f) * deTuLabel.transform.localScale.y;
		if (pt != null && pt.PhanThuongList.Count > 0)
		{
			stringBuilder = new StringBuilder(Localization.instance.Get("DanhNhanhGHPhanThuongLabel"));
			for (int j = 0; j < pt.PhanThuongList.Count; j++)
			{
				PhanThuongResponse.PhanThuong phanThuong = pt.PhanThuongList[j];
				if (j > 0)
				{
					stringBuilder.Append(", ");
				}
				switch (phanThuong.Loai)
				{
				case PhanThuongResponse.LoaiPhanThuong.CAO_NHAN:
					stringBuilder.AppendFormat("[d8231b]{0}[-]", Localization.instance.Get("PhanThuongKyNgoCaoNhan"));
					break;
				case PhanThuongResponse.LoaiPhanThuong.BAN_DO:
					stringBuilder.AppendFormat("[d8231b]{0}[-]", Localization.instance.Get("PhanThuongKyNgoBanDo"));
					break;
				case PhanThuongResponse.LoaiPhanThuong.TY_THI:
					stringBuilder.AppendFormat("[d8231b]{0}[-]", Localization.instance.Get("PhanThuongKyNgoTyThi"));
					break;
				case PhanThuongResponse.LoaiPhanThuong.BANG_HUU:
					stringBuilder.AppendFormat("[d8231b]{0}[-]", Localization.instance.Get("PhanThuongKyNgoBangHuu"));
					break;
				case PhanThuongResponse.LoaiPhanThuong.THUONG_NHAN:
					stringBuilder.AppendFormat("[d8231b]{0}[-]", Localization.instance.Get("PhanThuongKyNgoThuongNhan"));
					break;
				case PhanThuongResponse.LoaiPhanThuong.MANH_VO_CONG:
				{
					string text = phanThuong.Name;
					if (phanThuong.Name.StartsWith("MVC_"))
					{
						text = phanThuong.Name.Substring(1);
					}
					string arg = text;
					CfgVoCong value3;
					if ((ConfigManager.instance.m_dicVCs.TryGetValue(text, out value3) || ConfigManager.instance.m_dicVCs.TryGetValue(phanThuong.Name, out value3)) && value3 != null)
					{
						arg = value3.TenHienThi;
					}
					stringBuilder.AppendFormat("[d8231b]{0} {1}[-]", phanThuong.Count, string.Format(Localization.instance.Get("PhanThuongManhVoCong"), arg));
					break;
				}
				case PhanThuongResponse.LoaiPhanThuong.MANH_TRANG_BI:
				{
					string text3 = phanThuong.Name;
					if (phanThuong.Name.StartsWith("MVK_") || phanThuong.Name.StartsWith("MAG_") || phanThuong.Name.StartsWith("MMU_") || phanThuong.Name.StartsWith("MTS_"))
					{
						text3 = phanThuong.Name.Substring(1);
					}
					string arg3 = text3;
					TrangBiCfg value7;
					if ((ConfigManager.instance.m_dicTrangBi.TryGetValue(text3, out value7) || ConfigManager.instance.m_dicTrangBi.TryGetValue(phanThuong.Name, out value7)) && value7 != null)
					{
						arg3 = value7.TenHienThi;
					}
					stringBuilder.AppendFormat("[d8231b]{0} {1}[-]", phanThuong.Count, string.Format(Localization.instance.Get("PhanThuongManhTrangBi"), arg3));
					break;
				}
				case PhanThuongResponse.LoaiPhanThuong.TRANG_BI:
				{
					string tenHienThi4 = phanThuong.Name;
					TrangBiCfg value6;
					if (ConfigManager.instance.m_dicTrangBi.TryGetValue(phanThuong.Name, out value6) && value6 != null)
					{
						tenHienThi4 = value6.TenHienThi;
					}
					stringBuilder.AppendFormat("[d8231b]{0}[-]", string.Format(Localization.instance.Get("PhanThuongDanhNhanhTrangBi"), phanThuong.Count, tenHienThi4, phanThuong.Level));
					break;
				}
				case PhanThuongResponse.LoaiPhanThuong.VO_CONG:
				{
					string tenHienThi3 = phanThuong.Name;
					CfgVoCong value4;
					if (ConfigManager.instance.m_dicVCs.TryGetValue(phanThuong.Name, out value4) && value4 != null)
					{
						tenHienThi3 = value4.TenHienThi;
					}
					stringBuilder.AppendFormat("[d8231b]{0}[-]", string.Format(Localization.instance.Get("PhanThuongDanhNhanhVoCong"), phanThuong.Count, tenHienThi3, phanThuong.Level));
					break;
				}
				case PhanThuongResponse.LoaiPhanThuong.HON_NHAN_VAT:
				{
					string text2 = phanThuong.Name;
					if (text2.StartsWith("TH_"))
					{
						text2 = "NV_" + text2.Substring(3);
					}
					string arg2 = text2;
					NhanVatCfg value5;
					if ((ConfigManager.instance.m_dicNhanVats.TryGetValue(text2, out value5) || ConfigManager.instance.m_dicNhanVats.TryGetValue(phanThuong.Name, out value5)) && value5 != null)
					{
						arg2 = value5.TenHienThi;
					}
					stringBuilder.AppendFormat("[d8231b]{0}[-]", string.Format(Localization.instance.Get("PhanThuongDanhNhanhTanHon"), phanThuong.Count, arg2));
					break;
				}
				case PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU:
				{
					string tenHienThi2 = phanThuong.Name;
					VatPhamTieuThuCfg value2;
					if (ConfigManager.instance.m_dicVatPhamTieuThu.TryGetValue(phanThuong.Name, out value2) && value2 != null)
					{
						tenHienThi2 = value2.TenHienThi;
					}
					stringBuilder.AppendFormat("[d8231b]{0} {1}[-]", phanThuong.Count, tenHienThi2);
					break;
				}
				}
			}
			phanThuongLabel.gameObject.SetActive(true);
			phanThuongLabel.text = stringBuilder.ToString();
			phanThuongLabel.transform.localPosition = new Vector3(phanThuongLabel.transform.localPosition.x, 22f - num, phanThuongLabel.transform.localPosition.z);
			num += phanThuongLabel.relativeSize.y * phanThuongLabel.transform.localScale.y;
		}
		else
		{
			phanThuongLabel.gameObject.SetActive(false);
		}
		bg.transform.localScale = new Vector3(bg.transform.localScale.x, num, bg.transform.localScale.z);
		botBorder.transform.localPosition = new Vector3(0f, 0f - num - 6f, 0f);
		HeightBG = num + 30f;
	}
}
