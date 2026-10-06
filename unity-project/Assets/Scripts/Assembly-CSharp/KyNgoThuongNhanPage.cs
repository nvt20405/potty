using System;
using UnityEngine;

public class KyNgoThuongNhanPage : MonoBehaviour
{
	public UILabel timeLabel;

	public OtherAvatar vatPham;

	public GameObject btnGrp;

	public UILabel costLabel;

	private UserInfo.ThuongNhanData thuongNhan;

	private int index;

	private void Update()
	{
		if (thuongNhan != null)
		{
			TimeSpan timeSpan = thuongNhan.GioDi - GameManager.instance.m_GameClient.ServerTime;
			if (timeSpan.Ticks > 0)
			{
				timeLabel.text = string.Format(Localization.instance.Get("KyNgoGiangHoThuongNhanThoiGianMsg"), (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);
				btnGrp.SetActive(true);
			}
			else
			{
				timeLabel.text = Localization.instance.Get("KyNgoGiangHoThuongNhanRoiDiMsg");
				btnGrp.SetActive(false);
			}
		}
	}

	private void OnVatPhamClick(OtherAvatar avatar)
	{
		if (thuongNhan != null)
		{
			PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
			PhanThuongResponse.PhanThuong phanThuong = new PhanThuongResponse.PhanThuong();
			phanThuong.Name = PhanThuongResponse.GetPhanThuongCodeName(thuongNhan.TenVP);
			phanThuong.Level = 1;
			phanThuong.Loai = PhanThuongResponse.GetLoaiPhanThuongFromCode(thuongNhan.TenVP);
			phanThuong.Count = 1;
			phanThuongResponse.PhanThuongList.Add(phanThuong);
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("KyNgoThuongNhanTitlePhanThuong"), Localization.instance.Get("KyNgoThuongNhanDescPhanThuong"), phanThuongResponse);
		}
	}

	public void SetInfo(UserInfo.ThuongNhanData data, int idx)
	{
		if (data != null)
		{
			thuongNhan = data;
			TimeSpan timeSpan = data.GioDi - GameManager.instance.m_GameClient.ServerTime;
			vatPham.Set(data.TenVP);
			costLabel.text = data.Gia.ToString();
			vatPham.OnEventClick = OnVatPhamClick;
			if (timeSpan.Ticks > 0)
			{
				timeLabel.text = string.Format(Localization.instance.Get("KyNgoGiangHoThuongNhanThoiGianMsg"), (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);
				btnGrp.SetActive(true);
			}
			else
			{
				timeLabel.text = Localization.instance.Get("KyNgoGiangHoThuongNhanRoiDiMsg");
				btnGrp.SetActive(false);
			}
			index = idx;
		}
	}

	private void OnYesConfirmDataBtnClick()
	{
		GameManager.instance.m_GameClient.RequestThuongNhan(index, false);
	}

	private void OnDataBtnClick()
	{
		PopupYesNo.Create(Localization.instance.Get("ThuongNhanThamQuanConfirmMsg"), Localization.instance.Get("ThuongNhanThamQuanConfirmYes"), Localization.instance.Get("ThuongNhanThamQuanConfirmNo"), OnYesConfirmDataBtnClick, null);
	}

	private void OnHauTaBtnClick()
	{
		GameManager.instance.m_GameClient.RequestThuongNhan(index, true);
	}
}
