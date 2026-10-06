using System.Collections.Generic;
using UnityEngine;

public class ScreenNauBanhChung : ScreenBase
{
	public UISprite spriteNoiBanh;

	public UISprite spriteNoiBanh2;

	public UISprite spriteNoiBanh3;

	public UISprite spriteExp;

	public UILabel labelExp;

	public Transform sliderExp;

	public OtherAvatar[] listPhanThuong;

	public UILabel labelGaoNep;

	public UILabel labelLaDong;

	public UILabel labelThitLon;

	public UILabel labelSoBanhHomNay;

	public GameObject mAnimGhep;

	public GameObject ItemParticle;

	public GameObject nhanThuongBtn1;

	public GameObject nhanThuongBtn2;

	public GameObject nhanThuongBtn3;

	private int levelNoi;

	private NguoiNauBanh nguoiNau;

	public void startPlayAnim(string banhChung)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(mAnimGhep);
		if (gameObject != null)
		{
			gameObject.transform.parent = ItemParticle.transform;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = Vector3.one;
			UISprite component = GameObject.Find("spTanChuong").GetComponent<UISprite>();
			component.spriteName = banhChung;
			gameObject.GetComponent<ParticleSystem>().Play();
			Object.Destroy(gameObject, 3f);
		}
	}

	private void OnNhanThuong1BtnClick()
	{
		GameManager.instance.m_GameClient.RequestBanhChungNhanThuong(1);
	}

	private void OnNhanThuong2BtnClick()
	{
		GameManager.instance.m_GameClient.RequestBanhChungNhanThuong(2);
	}

	private void OnNhanThuong3BtnClick()
	{
		GameManager.instance.m_GameClient.RequestBanhChungNhanThuong(3);
	}

	public void SyncWithNetworkData(NoiBanh noiBanh, NguoiNauBanh nguoiNau)
	{
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		int num = (levelNoi = userInfo.ServerInfo.BanhChungCfg.GetLevelNoiBanhFromSoBanh(noiBanh.SoBanh));
		this.nguoiNau = nguoiNau;
		spriteNoiBanh3.MakePixelPerfect();
		spriteNoiBanh.MakePixelPerfect();
		spriteNoiBanh2.MakePixelPerfect();
		if (num < 1)
		{
			sliderExp.gameObject.SetActive(true);
			spriteNoiBanh.transform.localScale *= 1.4f;
			sliderExp.transform.localPosition = new Vector3(spriteNoiBanh.transform.localPosition.x, sliderExp.transform.localPosition.y, sliderExp.localPosition.z);
			nhanThuongBtn1.gameObject.SetActive(false);
			nhanThuongBtn2.gameObject.SetActive(false);
			nhanThuongBtn3.gameObject.SetActive(false);
		}
		else if (num < 2)
		{
			sliderExp.gameObject.SetActive(true);
			spriteNoiBanh2.transform.localScale *= 1.4f;
			sliderExp.transform.localPosition = new Vector3(spriteNoiBanh2.transform.localPosition.x, sliderExp.transform.localPosition.y, sliderExp.localPosition.z);
			nhanThuongBtn1.gameObject.SetActive(nguoiNau.LayPT1 == 0);
			nhanThuongBtn2.gameObject.SetActive(false);
			nhanThuongBtn3.gameObject.SetActive(false);
		}
		else if (num < 3)
		{
			sliderExp.gameObject.SetActive(true);
			spriteNoiBanh3.transform.localScale *= 1.4f;
			sliderExp.localPosition = new Vector3(spriteNoiBanh3.transform.localPosition.x, sliderExp.transform.localPosition.y, sliderExp.localPosition.z);
			nhanThuongBtn1.gameObject.SetActive(nguoiNau.LayPT1 == 0);
			nhanThuongBtn2.gameObject.SetActive(nguoiNau.LayPT2 == 0);
			nhanThuongBtn3.gameObject.SetActive(false);
		}
		else
		{
			spriteNoiBanh3.MakePixelPerfect();
			spriteNoiBanh3.transform.localScale *= 1.4f;
			sliderExp.gameObject.SetActive(false);
			nhanThuongBtn1.gameObject.SetActive(nguoiNau.LayPT1 == 0);
			nhanThuongBtn2.gameObject.SetActive(nguoiNau.LayPT2 == 0);
			nhanThuongBtn3.gameObject.SetActive(nguoiNau.LayPT3 == 0);
		}
		UserInfo.VatPhamTieuThuData vatPhamTieuThuData = userInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_LA_DONG");
		labelLaDong.text = string.Format("{0}/{1}", (vatPhamTieuThuData != null) ? vatPhamTieuThuData.Quantity : 0, userInfo.ServerInfo.BanhChungCfg.SoNgLCan);
		vatPhamTieuThuData = userInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_GAO_NEP");
		labelGaoNep.text = string.Format("{0}/{1}", (vatPhamTieuThuData != null) ? vatPhamTieuThuData.Quantity : 0, userInfo.ServerInfo.BanhChungCfg.SoNgLCan);
		vatPhamTieuThuData = userInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_THIT_LON");
		labelThitLon.text = string.Format("{0}/{1}", (vatPhamTieuThuData != null) ? vatPhamTieuThuData.Quantity : 0, userInfo.ServerInfo.BanhChungCfg.SoNgLCan);
		labelSoBanhHomNay.text = string.Format(Localization.instance.Get("LabelDaNauBanh"), nguoiNau.SoBanh);
		float expNoiBanhFromSoBanh = userInfo.ServerInfo.BanhChungCfg.GetExpNoiBanhFromSoBanh(noiBanh.SoBanh);
		int num2 = 0;
		switch (num)
		{
		case 0:
			num2 = userInfo.ServerInfo.BanhChungCfg.Lvl1;
			break;
		case 1:
			num2 = userInfo.ServerInfo.BanhChungCfg.Lvl2;
			break;
		case 2:
			num2 = userInfo.ServerInfo.BanhChungCfg.Lvl3;
			break;
		}
		labelExp.text = string.Format("{0}/{1}", noiBanh.SoBanh, num2);
		spriteExp.fillAmount = expNoiBanhFromSoBanh;
	}

	private void OnNauBanhBtnClick()
	{
		GameManager.instance.m_GameClient.RequestBanhChungNauBanh();
	}

	private void OnCloseBtnClick()
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenBanhChung);
	}

	private void OnBXHBtnClick()
	{
		GameManager.instance.m_GameClient.RequestBanhChungGetBXH();
	}

	private void OnNoiSatBtnClick()
	{
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		if (userInfo.ServerInfo != null && userInfo.ServerInfo.BanhChungCfg != null)
		{
			int lvl = userInfo.ServerInfo.BanhChungCfg.Lvl1;
			if (nguoiNau != null && nguoiNau.LayPT1 != 0)
			{
				MessagePopup.Create(Localization.instance.Get("BanhChungDaNhanThuong"));
				return;
			}
			PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
			phanThuongResponse.PhanThuongList = new List<PhanThuongResponse.PhanThuong>(userInfo.ServerInfo.BanhChungCfg.Pt1);
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTopBanhChungTitle"), string.Format(Localization.instance.Get("PhanThuongNoiBanhDesc"), lvl), phanThuongResponse);
		}
	}

	private void OnNoiBacBtnClick()
	{
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		if (userInfo.ServerInfo != null && userInfo.ServerInfo.BanhChungCfg != null)
		{
			int lvl = userInfo.ServerInfo.BanhChungCfg.Lvl2;
			if (nguoiNau != null && nguoiNau.LayPT2 != 0)
			{
				MessagePopup.Create(Localization.instance.Get("BanhChungDaNhanThuong"));
				return;
			}
			PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
			phanThuongResponse.PhanThuongList = new List<PhanThuongResponse.PhanThuong>(userInfo.ServerInfo.BanhChungCfg.Pt2);
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTopBanhChungTitle"), string.Format(Localization.instance.Get("PhanThuongNoiBanhDesc"), lvl), phanThuongResponse);
		}
	}

	private void OnNoiVangBtnClick()
	{
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		if (userInfo.ServerInfo != null && userInfo.ServerInfo.BanhChungCfg != null)
		{
			int lvl = userInfo.ServerInfo.BanhChungCfg.Lvl3;
			if (nguoiNau != null && nguoiNau.LayPT3 != 0)
			{
				MessagePopup.Create(Localization.instance.Get("BanhChungDaNhanThuong"));
				return;
			}
			PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
			phanThuongResponse.PhanThuongList = new List<PhanThuongResponse.PhanThuong>(userInfo.ServerInfo.BanhChungCfg.Pt3);
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTopBanhChungTitle"), string.Format(Localization.instance.Get("PhanThuongNoiBanhDesc"), lvl), phanThuongResponse);
		}
	}
}
