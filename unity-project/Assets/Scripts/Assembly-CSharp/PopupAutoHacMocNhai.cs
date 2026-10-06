using UnityEngine;

public class PopupAutoHacMocNhai : MonoBehaviour
{
	private enum UuTienSelect
	{
		None = 0,
		UuTien1 = 1,
		UuTien2 = 2
	}

	public UISprite spUuTien1;

	public UISprite spUuTien2;

	public GameObject groupChiSo;

	public GameObject focusUuTien1;

	public GameObject focusUuTien2;

	private ChiSoCoBan chisoCurrDuocChon;

	private UuTienSelect uuTienDangChon;

	public static PopupAutoHacMocNhai instance;

	public static PopupAutoHacMocNhai Create()
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		Object obj = Object.Instantiate(Resources.Load("popup/PopupAutoHacMocNhai"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		instance = gameObject.GetComponent<PopupAutoHacMocNhai>();
		instance.displayInfo();
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		return instance;
	}

	public void btnClose_OnClick()
	{
		GUIManager.instance.autoHMN_ChiSoUuTien1 = ChiSoCoBan.None;
		GUIManager.instance.autoHMN_ChiSoUuTien2 = ChiSoCoBan.None;
		DestroyPopup();
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			instance.gameObject.SetActive(false);
			Object.Destroy(instance.gameObject);
		}
		instance = null;
	}

	private void displayInfo()
	{
		GUIManager.instance.autoHMN_ChiSoUuTien1 = ChiSoCoBan.None;
		GUIManager.instance.autoHMN_ChiSoUuTien2 = ChiSoCoBan.None;
		spUuTien1.gameObject.SetActive(false);
		spUuTien2.gameObject.SetActive(false);
		uuTienDangChon = UuTienSelect.UuTien1;
		focusUuTien1.gameObject.SetActive(false);
		focusUuTien2.gameObject.SetActive(false);
		groupChiSo.gameObject.SetActive(false);
	}

	private void btnThan_OnClick()
	{
		setLoaiUuTien(ChiSoCoBan.ThanPhap);
	}

	private void btnMenh_OnClick()
	{
		setLoaiUuTien(ChiSoCoBan.Menh);
	}

	private void btnNgoai_OnClick()
	{
		setLoaiUuTien(ChiSoCoBan.Ngoai);
	}

	private void btnKhi_OnClick()
	{
		setLoaiUuTien(ChiSoCoBan.Noi);
	}

	private void setLoaiUuTien(ChiSoCoBan chisoSelected)
	{
		if (NGUITools.GetActive(groupChiSo.gameObject))
		{
			groupChiSo.gameObject.SetActive(true);
		}
		if (uuTienDangChon == UuTienSelect.None)
		{
			return;
		}
		string text = string.Empty;
		switch (chisoSelected)
		{
		case ChiSoCoBan.Menh:
			text = "menh_title_autoHMN";
			chisoCurrDuocChon = ChiSoCoBan.Menh;
			break;
		case ChiSoCoBan.Ngoai:
			text = "ngoai_title_autoHMN";
			chisoCurrDuocChon = ChiSoCoBan.Ngoai;
			break;
		case ChiSoCoBan.ThanPhap:
			text = "than_title_autoHMN";
			chisoCurrDuocChon = ChiSoCoBan.ThanPhap;
			break;
		case ChiSoCoBan.Noi:
			text = "khi_title_autoHMN";
			chisoCurrDuocChon = ChiSoCoBan.Noi;
			break;
		}
		if (uuTienDangChon == UuTienSelect.UuTien1)
		{
			if (chisoCurrDuocChon != GUIManager.instance.autoHMN_ChiSoUuTien2)
			{
				if (text.Length > 0)
				{
					spUuTien1.spriteName = text;
					spUuTien1.MakePixelPerfect();
					spUuTien1.gameObject.SetActive(true);
				}
				GUIManager.instance.autoHMN_ChiSoUuTien1 = chisoCurrDuocChon;
			}
		}
		else if (uuTienDangChon == UuTienSelect.UuTien2 && chisoCurrDuocChon != GUIManager.instance.autoHMN_ChiSoUuTien1)
		{
			if (text.Length > 0)
			{
				spUuTien2.spriteName = text;
				spUuTien2.MakePixelPerfect();
				spUuTien2.gameObject.SetActive(true);
			}
			GUIManager.instance.autoHMN_ChiSoUuTien2 = chisoCurrDuocChon;
		}
	}

	private void btnUuTien1_OnClick()
	{
		if (NGUITools.GetActive(spUuTien1.gameObject))
		{
			spUuTien1.gameObject.SetActive(false);
			GUIManager.instance.autoHMN_ChiSoUuTien1 = ChiSoCoBan.None;
		}
		uuTienDangChon = UuTienSelect.UuTien1;
		groupChiSo.gameObject.SetActive(true);
		focusUuTien1.gameObject.SetActive(true);
		focusUuTien1.GetComponent<ParticleSystem>().Play();
		focusUuTien2.gameObject.SetActive(false);
	}

	private void btnUuTien2_OnClick()
	{
		if (NGUITools.GetActive(spUuTien2.gameObject))
		{
			spUuTien2.gameObject.SetActive(false);
			GUIManager.instance.autoHMN_ChiSoUuTien2 = ChiSoCoBan.None;
		}
		uuTienDangChon = UuTienSelect.UuTien2;
		groupChiSo.gameObject.SetActive(true);
		focusUuTien2.gameObject.SetActive(true);
		focusUuTien2.GetComponent<ParticleSystem>().Play();
		focusUuTien1.gameObject.SetActive(false);
	}

	private void startAutoHMN()
	{
		if ((GUIManager.instance.autoHMN_ChiSoUuTien1 == ChiSoCoBan.None && GUIManager.instance.autoHMN_ChiSoUuTien2 == ChiSoCoBan.None) || GUIManager.instance.autoHMN_ChiSoUuTien1 == ChiSoCoBan.None || GUIManager.instance.autoHMN_ChiSoUuTien2 == ChiSoCoBan.None)
		{
			MessagePopup.Create(Localization.instance.Get("ThongBaoChuaChonChiSoUuTien"));
			return;
		}
		ScreenHuyetChien screenHuyetChien = GUIManager.getScreen(GAME_SCREEN.ScreenHuyetChien) as ScreenHuyetChien;
		if (screenHuyetChien != null)
		{
			screenHuyetChien.endAutoGrp.SetActive(true);
			screenHuyetChien.startAutoGoHMN();
		}
		DestroyPopup();
	}
}
