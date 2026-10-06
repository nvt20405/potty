using System;
using UnityEngine;

public class GH_ChiTietNhiemVuItem : MonoBehaviour
{
	public UILabel motaLbl;

	public UILabel luotLbl;

	public NhanVatAvatar tanHonReward;

	public OtherAvatar reward;

	public UILabel rewardLabel;

	public UILabel timeDanhNhanh;

	public UISprite vangSprite;

	public GameObject danhNhanhBtn;

	private DateTime gioDanhNhanh = DateTime.MinValue;

	private int GiangHoIdx;

	private int NVIdx;

	private bool enableDanhNhanh;

	private int luotDanhNhanh;

	private string ptCodename = string.Empty;

	private int ptCount;

	private bool isGHTinhAnh;

	private void Awake()
	{
		reward.OnEventClick = OnRewardClick;
		tanHonReward.OnEventClick = OnRewardClick;
	}

	private void OnRewardClick()
	{
		if (!string.IsNullOrEmpty(ptCodename))
		{
			PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
			PhanThuongResponse.PhanThuong phanThuong = new PhanThuongResponse.PhanThuong();
			phanThuong.Name = PhanThuongResponse.GetPhanThuongCodeName(ptCodename);
			phanThuong.Level = 1;
			phanThuong.Count = ptCount;
			phanThuong.Loai = PhanThuongResponse.GetLoaiPhanThuongFromCode(ptCodename);
			phanThuongResponse.PhanThuongList.Add(phanThuong);
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTitle"), Localization.instance.Get("GiangHoPhanThuongNhiemVuDesc"), phanThuongResponse);
		}
	}

	private void OnRewardClick(OtherAvatar avatar)
	{
		OnRewardClick();
	}

	private void OnRewardClick(NhanVatAvatar avatar)
	{
		OnRewardClick();
	}

	public void SetInfo(int ghIdx, int nvIdx, string mota, int luotConLai, string rewardCode, int rewardcount, DateTime timeDanhNhanhGH, bool isGHTA)
	{
		motaLbl.text = mota;
		luotLbl.text = string.Format(Localization.instance.Get("GiangHoLuotConLaiLabel"), luotConLai);
		if (string.IsNullOrEmpty(rewardCode))
		{
			tanHonReward.gameObject.SetActive(false);
			reward.gameObject.SetActive(false);
			rewardLabel.gameObject.SetActive(false);
			ptCodename = string.Empty;
			ptCount = 0;
		}
		else
		{
			if (PhanThuongResponse.GetLoaiPhanThuongFromCode(rewardCode) == PhanThuongResponse.LoaiPhanThuong.HON_NHAN_VAT)
			{
				tanHonReward.gameObject.SetActive(true);
				reward.gameObject.SetActive(false);
				tanHonReward.Set(rewardCode, 0, -1, false, (rewardcount <= 1) ? (-1) : rewardcount);
			}
			else
			{
				tanHonReward.gameObject.SetActive(false);
				reward.gameObject.SetActive(true);
				reward.Set(rewardCode, 0, -1, (rewardcount <= 1) ? (-1) : rewardcount);
			}
			rewardLabel.gameObject.SetActive(true);
			ptCodename = rewardCode;
			ptCount = rewardcount;
		}
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		UserInfo.GiangHoData.NhiemVuRecord nhiemVuRecord = null;
		isGHTinhAnh = isGHTA;
		if (isGHTA)
		{
			if (userInfo.GiangHoTinhAnh != null && userInfo.GiangHoTinhAnh.Count > ghIdx && userInfo.GiangHoTinhAnh[ghIdx].NhiemVu.Count > nvIdx)
			{
				nhiemVuRecord = userInfo.GiangHoTinhAnh[ghIdx].NhiemVu[nvIdx];
			}
		}
		else if (userInfo.GiangHo != null && userInfo.GiangHo.Count > ghIdx && userInfo.GiangHo[ghIdx].NhiemVu.Count > nvIdx)
		{
			nhiemVuRecord = userInfo.GiangHo[ghIdx].NhiemVu[nvIdx];
		}
		int vip = GameManager.instance.m_GameClient.UserInfo.Gamer.Vip;
		if (vip < ConfigManager.GetVipEnableDanhNhanhGiangHo())
		{
			enableDanhNhanh = false;
		}
		else
		{
			enableDanhNhanh = true;
		}
		luotDanhNhanh = ConfigManager.instance.GetLuotDanhNhanh(userInfo.GiaTriThoiGian.TheLuc, luotConLai);
		if (luotDanhNhanh > 0 && nhiemVuRecord != null && nhiemVuRecord.S > 0)
		{
			enableDanhNhanh = true;
			danhNhanhBtn.gameObject.SetActive(true);
			danhNhanhBtn.GetComponentInChildren<UILabel>().text = string.Format(Localization.instance.Get("GiangHoDanhNhanhLuot"), luotDanhNhanh);
		}
		else
		{
			enableDanhNhanh = false;
			danhNhanhBtn.gameObject.SetActive(false);
		}
		gioDanhNhanh = timeDanhNhanhGH;
		GiangHoIdx = ghIdx;
		NVIdx = nvIdx;
	}

	private void OnConfirmDanhNhanh()
	{
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		TimeSpan duration = GameManager.instance.m_GameClient.ServerTime - userInfo.GiaTriThoiGian.LastTimeDanhNhanhGH;
		int costDanhNhanhGiangHo = ConfigManager.GetCostDanhNhanhGiangHo(userInfo.Gamer.Vip, duration);
		if (GameManager.instance.m_GameClient.checkKNB(costDanhNhanhGiangHo))
		{
			GameManager.instance.m_GameClient.RequestDanhNhanhGH(GiangHoIdx, NVIdx, isGHTinhAnh);
		}
	}

	private void OnDanhNhanhClick()
	{
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		TimeSpan duration = GameManager.instance.m_GameClient.ServerTime - userInfo.GiaTriThoiGian.LastTimeDanhNhanhGH;
		GiangHoCfg.NhiemVu nhiemVu = ConfigManager.instance.m_listGiangHo[GiangHoIdx].NhiemVuList[NVIdx];
		int costDanhNhanhGiangHo = ConfigManager.GetCostDanhNhanhGiangHo(userInfo.Gamer.Vip, duration);
		if (costDanhNhanhGiangHo > 0)
		{
			string message = string.Format(Localization.instance.Get("DanhNhanhConfirmMsg"), costDanhNhanhGiangHo, luotDanhNhanh, nhiemVu.TenHienThi);
			PopupYesNo.Create(message, Localization.instance.Get("DanhNhanhConfirmBtn"), Localization.instance.Get("DanhNhanhDeniedBtn"), OnConfirmDanhNhanh, null);
		}
		else
		{
			OnConfirmDanhNhanh();
		}
	}

	private void Update()
	{
		if (luotDanhNhanh <= 0 || gioDanhNhanh < GameManager.instance.m_GameClient.ServerTime || !enableDanhNhanh)
		{
			timeDanhNhanh.gameObject.SetActive(false);
			vangSprite.gameObject.SetActive(false);
			return;
		}
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		TimeSpan timeSpan = gioDanhNhanh - serverTime;
		timeDanhNhanh.gameObject.SetActive(true);
		vangSprite.gameObject.SetActive(true);
		timeDanhNhanh.text = string.Format("{0:00} : {1:00}", (int)timeSpan.TotalMinutes, timeSpan.Seconds);
	}
}
