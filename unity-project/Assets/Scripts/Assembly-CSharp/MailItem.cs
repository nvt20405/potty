using System;
using LitJson;
using UnityEngine;

public class MailItem : MonoBehaviour
{
	public UILabel contentLabel;

	public UILabel timeLabel;

	public UIButton btnOK;

	public UILabel labelButton;

	public int mailID;

	public UserInfo.MailData mMail_Data;

	public void SetMailData(UserInfo.MailData data)
	{
		if (data != null)
		{
			mMail_Data = data;
			displayInfo();
		}
	}

	public void displayInfo()
	{
		btnOK.gameObject.SetActive(false);
		if (mMail_Data.Type == UserInfo.MailData.MAIL_TYPE.QuaTang)
		{
			UserInfo.MailData.MailQuaTangContent mailQuaTangContent = JsonMapper.ToObject<UserInfo.MailData.MailQuaTangContent>(mMail_Data.Content);
			string text = string.Empty;
			if (mailQuaTangContent.ListPT != null && mailQuaTangContent.ListPT.Count > 0)
			{
				for (int i = 0; i < mailQuaTangContent.ListPT.Count; i++)
				{
					PhanThuongResponse.PhanThuong phanThuong = mailQuaTangContent.ListPT[i];
					if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.TRANG_BI)
					{
						TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[phanThuong.Name];
						text = text + trangBiCfg.TenHienThi + " x " + phanThuong.Count;
					}
					else if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.VO_CONG)
					{
						CfgVoCong cfgVoCong = ConfigManager.instance.m_dicVCs[phanThuong.Name];
						text = text + cfgVoCong.TenHienThi + " x " + phanThuong.Count;
					}
					else if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU)
					{
						VatPhamTieuThuCfg vatPhamTieuThuCfg = ConfigManager.instance.m_dicVatPhamTieuThu[phanThuong.Name];
						text = text + vatPhamTieuThuCfg.TenHienThi + " x " + phanThuong.Count;
					}
					else if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.BAC)
					{
						text = text + Localization.instance.Get("Bac") + " x " + phanThuong.Count;
					}
					else if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.VANG)
					{
						text = text + Localization.instance.Get("Vang") + " x " + phanThuong.Count;
					}
					else if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.HON_NHAN_VAT)
					{
						NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[phanThuong.Name];
						text = text + Localization.instance.Get("MailTanHonLabel") + " " + nhanVatCfg.TenHienThi + " x " + phanThuong.Count;
					}
					else if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.MANH_TRANG_BI)
					{
						TrangBiCfg trangBiCfg2 = ConfigManager.instance.m_dicTrangBi[phanThuong.Name.StartsWith("M") ? phanThuong.Name.Substring(1) : phanThuong.Name];
						text = text + Localization.instance.Get("MailManhLabel") + " " + trangBiCfg2.TenHienThi + " x " + phanThuong.Count;
					}
					else if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.MANH_VO_CONG)
					{
						CfgVoCong cfgVoCong2 = ConfigManager.instance.m_dicVCs[phanThuong.Name.StartsWith("MVC_") ? phanThuong.Name.Substring(1) : phanThuong.Name];
						text = text + Localization.instance.Get("MailManhLabel") + " " + cfgVoCong2.TenHienThi + " x " + phanThuong.Count;
					}
					else if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.THU_CUOI)
					{
						string displayName = phanThuong.Name;
						OtherCfg.ThuCuoiCfg value;
						if (ConfigManager.instance.OtherConfig.ThuCuoiConfig.TryGetValue(phanThuong.Name, out value))
						{
							displayName = value.DisplayName;
						}
						text = text + displayName + " (" + string.Format(Localization.instance.Get("TimeSuDungNguaLabel"), phanThuong.Count) + ")";
					}
					text = ((i != mailQuaTangContent.ListPT.Count - 1) ? (text + ", ") : (text + "."));
				}
				contentLabel.text = mailQuaTangContent.Msg + ": " + text;
			}
			else
			{
				contentLabel.text = mailQuaTangContent.Msg + ".";
			}
			if (mMail_Data.Status != UserInfo.MailData.MAIL_STATUS.Empty)
			{
				labelButton.text = Localization.instance.Get("NhanThuongBtn");
				btnOK.gameObject.SetActive(true);
			}
			else
			{
				btnOK.gameObject.SetActive(false);
			}
		}
		else
		{
			UserInfo.MailData.MailTinNhanContent mailTinNhanContent = JsonMapper.ToObject<UserInfo.MailData.MailTinNhanContent>(mMail_Data.Content);
			if (mMail_Data.Type == UserInfo.MailData.MAIL_TYPE.TinNhanHeThong)
			{
				contentLabel.text = mailTinNhanContent.Msg;
			}
			else if (mMail_Data.Type == UserInfo.MailData.MAIL_TYPE.TinNhan)
			{
				labelButton.text = Localization.instance.Get("GuiThuBtnLabel");
				btnOK.gameObject.SetActive(true);
				contentLabel.text = "[8E0707]" + mailTinNhanContent.Name + " " + Localization.instance.Get("NguoiGui") + "\n [-]" + mailTinNhanContent.Msg;
			}
		}
		DateTime dateTime = mMail_Data.RemoveTime - ConfigManager.GetTimeRemoveMail();
		TimeSpan timeSpan = GameManager.instance.m_GameClient.ServerTime - dateTime;
		if (timeSpan.TotalDays > 1.0)
		{
			timeLabel.text = (int)timeSpan.TotalDays + " " + Localization.instance.Get("ChatTimeMess4");
		}
		else if (timeSpan.TotalHours > 1.0)
		{
			timeLabel.text = (int)timeSpan.TotalHours + " " + Localization.instance.Get("ChatTimeMess3");
		}
		else if (timeSpan.TotalMinutes > 1.0)
		{
			timeLabel.text = (int)timeSpan.TotalMinutes + " " + Localization.instance.Get("ChatTimeMess2");
		}
		else
		{
			timeLabel.text = Localization.instance.Get("ChatTimeMess1");
		}
	}
}
