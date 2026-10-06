using System;
using UnityEngine;

public class GH_NhiemVuItem : MonoBehaviour
{
	public UILabel nameLabel;

	public UILabel bacLabel;

	public UILabel expLabel;

	public NhanVatAvatar avatar;

	public UISprite[] stars;

	public UISprite vangSprite;

	public UISprite bg;

	public GameObject danhBtn;

	private int _nhiemVuIdx = -1;

	private int giangHoIdx = -1;

	public Action<int> OnShowDetail;

	public Action<int> OnDanhGiangHo;

	private int luotConLai;

	private bool isGHTinhAnh;

	public int NhiemVuIdx
	{
		get
		{
			return _nhiemVuIdx;
		}
		set
		{
			_nhiemVuIdx = value;
		}
	}

	public void SetInfo(string name, string nhanVatDaiDien, int ghIdx, int idx, int luotConLai, int bac, int exp, int star, int code_nv, bool isGHTA)
	{
		isGHTinhAnh = isGHTA;
		nameLabel.text = name;
		bacLabel.text = bac.ToString();
		expLabel.text = exp.ToString();
		avatar.Set(nhanVatDaiDien);
		for (int i = 0; i < stars.Length; i++)
		{
			stars[i].gameObject.SetActive(i < star);
		}
		vangSprite.gameObject.SetActive(luotConLai <= 0);
		this.luotConLai = luotConLai;
		_nhiemVuIdx = idx;
		giangHoIdx = ghIdx;
		switch (code_nv)
		{
		case 0:
			bg.spriteName = "bgr_tab9";
			break;
		case 1:
			bg.spriteName = "bgr_tab4";
			break;
		default:
			bg.spriteName = "tab7";
			break;
		}
	}

	private void DanhGiangHo()
	{
		if (OnDanhGiangHo != null)
		{
			OnDanhGiangHo(NhiemVuIdx);
		}
	}

	private void ResetLuotGH()
	{
		if (GameManager.instance.m_GameClient.checkKNB(ConfigManager.GetCostResetLuotGH()))
		{
			GameManager.instance.m_GameClient.RequestResetLuotNVGH(giangHoIdx, NhiemVuIdx, isGHTinhAnh);
		}
	}

	private void OnDanhClick()
	{
		if (luotConLai <= 0)
		{
			PopupYesNo.Create(string.Format(Localization.instance.Get("MessageResetLuotGH"), ConfigManager.GetCostResetLuotGH()), Localization.instance.Get("MessageResetLuotGHYes"), Localization.instance.Get("MessageResetLuotGHNo"), ResetLuotGH, null);
		}
		else
		{
			DanhGiangHo();
		}
	}

	private void OnShowDetailClick()
	{
		if (OnShowDetail != null)
		{
			OnShowDetail(_nhiemVuIdx);
		}
	}
}
