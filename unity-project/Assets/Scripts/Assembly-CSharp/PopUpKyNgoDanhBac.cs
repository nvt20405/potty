using System.Collections;
using UnityEngine;

public class PopUpKyNgoDanhBac : MonoBehaviour
{
	public UILabel lbMess;

	public UIButton btnChan;

	public UIButton btnLe;

	public UIButton btnOK;

	public GameObject m_animDanhBac;

	public GameObject batDiaGrp;

	private int currentIndex;

	private int currentID;

	public UISprite xucxac1;

	public UISprite xucxac2;

	public UISprite xucxac3;

	public UISprite batTo;

	public static PopUpKyNgoDanhBac instance;

	private void Start()
	{
		m_animDanhBac.SetActive(false);
		batTo.gameObject.SetActive(true);
		btnOK.gameObject.SetActive(false);
		lbMess.text = Localization.instance.Get("ChuongMonChonCuaMess");
	}

	private void Update()
	{
	}

	public void OnCloseClick()
	{
		DestroyPopup();
	}

	public void onClick_BtnDongY()
	{
		DestroyPopup();
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public static void Create(int index, int VoCongOrTrangBiID)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("popup/PopupKyNgoDanhBac"))).GetComponent<PopUpKyNgoDanhBac>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.currentIndex = index;
		instance.currentID = VoCongOrTrangBiID;
	}

	public void onClick_BtnChan()
	{
		if (currentID >= 0)
		{
			hideButtonChanLe(true);
			ChoiXocDiaRequest choiXocDiaRequest = new ChoiXocDiaRequest();
			choiXocDiaRequest.Idx = currentIndex;
			choiXocDiaRequest.DatCua = ChoiXocDiaRequest.XocDiaChanLe.Chan;
			if (currentID > 0)
			{
				choiXocDiaRequest.TrangBiOrVoCongID = currentID;
			}
			GameManager.instance.m_GameClient.RequestChoiXocDia(choiXocDiaRequest);
		}
	}

	public void onClick_BtnLe()
	{
		if (currentID >= 0)
		{
			hideButtonChanLe(true);
			ChoiXocDiaRequest choiXocDiaRequest = new ChoiXocDiaRequest();
			choiXocDiaRequest.Idx = currentIndex;
			choiXocDiaRequest.DatCua = ChoiXocDiaRequest.XocDiaChanLe.Le;
			if (currentID > 0)
			{
				choiXocDiaRequest.TrangBiOrVoCongID = currentID;
			}
			GameManager.instance.m_GameClient.RequestChoiXocDia(choiXocDiaRequest);
		}
	}

	public void hideButtonChanLe(bool isHide)
	{
		if (isHide)
		{
			btnChan.gameObject.SetActive(false);
			btnLe.gameObject.SetActive(false);
		}
		else
		{
			btnChan.gameObject.SetActive(true);
			btnLe.gameObject.SetActive(true);
		}
	}

	public void startPlayAnim(ChoiXocDiaResponse response)
	{
		batDiaGrp.SetActive(false);
		m_animDanhBac.SetActive(true);
		m_animDanhBac.GetComponent<ParticleSystem>().Simulate(0f, true, true);
		m_animDanhBac.GetComponent<ParticleSystem>().Play();
		StartCoroutine(displayResult(2f, response));
	}

	public IEnumerator displayResult(float waitTime, ChoiXocDiaResponse response)
	{
		yield return new WaitForSeconds(waitTime);
		batDiaGrp.SetActive(true);
		batTo.gameObject.SetActive(false);
		displayXucXac(response.XucXac1, xucxac1);
		displayXucXac(response.XucXac2, xucxac2);
		displayXucXac(response.XucXac3, xucxac3);
		if (response.IsWin)
		{
			lbMess.text = string.Format(Localization.instance.Get("ChuongMonThangMess"), response.TongXucXac);
			StartCoroutine(openPopUpPhanThuong(2f, response));
		}
		else
		{
			lbMess.text = string.Format(Localization.instance.Get("ChuongMonThuaMess"), response.TongXucXac);
		}
		btnOK.gameObject.SetActive(true);
	}

	public IEnumerator openPopUpPhanThuong(float waitTime, ChoiXocDiaResponse response)
	{
		yield return new WaitForSeconds(waitTime);
		PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongNhanDuoc"), response.PhanThuongResponse);
		DestroyPopup();
	}

	private void displayXucXac(int xucxacValue, UISprite sprite)
	{
		switch (xucxacValue)
		{
		case 1:
			sprite.spriteName = "danhbac9";
			break;
		case 2:
			sprite.spriteName = "danhbac10";
			break;
		case 3:
			sprite.spriteName = "danhbac11";
			break;
		case 4:
			sprite.spriteName = "danhbac12";
			break;
		case 5:
			sprite.spriteName = "danhbac13";
			break;
		case 6:
			sprite.spriteName = "danhbac14";
			break;
		}
	}
}
