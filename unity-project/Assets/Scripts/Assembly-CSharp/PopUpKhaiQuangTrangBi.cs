using UnityEngine;

public class PopUpKhaiQuangTrangBi : MonoBehaviour
{
	public UILabel lbSoLuong;

	public UILabel lbTyLeSuccess;

	public UILabel lbHienCo;

	public UIButton btnPlus;

	public UIButton btnDoublePlus;

	public UIButton btnMinus;

	public UIButton btnDoubleMinus;

	private int soLuongSuDung;

	private bool isStartChoice;

	public static PopUpKhaiQuangTrangBi instance;

	public UserInfo.TrangBiData TrangBi;

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public static void Create(UserInfo.TrangBiData TrangBi)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopUpKhaiQuangTrangBi"))).GetComponent<PopUpKhaiQuangTrangBi>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.isStartChoice = true;
		instance.soLuongSuDung = 1;
		instance.lbSoLuong.text = instance.soLuongSuDung.ToString();
		instance.TrangBi = TrangBi;
		if (GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_HUYEN_THIET_THACH"))
		{
			instance.lbHienCo.text = string.Format(Localization.instance.Get("HienCoLabelPopUpKhaiQuang"), GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_HUYEN_THIET_THACH").Quantity);
		}
		else
		{
			instance.lbHienCo.text = string.Format(Localization.instance.Get("HienCoLabelPopUpKhaiQuang"), 0);
		}
		instance.lbTyLeSuccess.text = string.Format(Localization.instance.Get("TyLeThanhCongKhaiQuang"), CommonHelper.GetKhaiQuangTiLeThanhCong(TrangBi, 1));
	}

	public void btnDong_OnClick(GameObject go)
	{
		DestroyPopup();
	}

	public void btnKhaiQuang_OnClick(GameObject go)
	{
		GameManager.instance.m_GameClient.RequestKhaiQuangTrangBi(TrangBi.ID, soLuongSuDung);
		DestroyPopup();
	}

	public void btnPlus_OnClick(GameObject go)
	{
		if (isStartChoice)
		{
			btnMinus.gameObject.SetActive(true);
			btnDoubleMinus.gameObject.SetActive(true);
			isStartChoice = false;
		}
		soLuongSuDung++;
		lbSoLuong.text = instance.soLuongSuDung.ToString();
		lbTyLeSuccess.text = string.Format(Localization.instance.Get("TyLeThanhCongKhaiQuang"), CommonHelper.GetKhaiQuangTiLeThanhCong(TrangBi, soLuongSuDung));
	}

	public void btnDoublePlus_OnClick(GameObject go)
	{
		if (isStartChoice)
		{
			btnMinus.gameObject.SetActive(true);
			btnDoubleMinus.gameObject.SetActive(true);
			isStartChoice = false;
		}
		soLuongSuDung += 10;
		lbSoLuong.text = instance.soLuongSuDung.ToString();
		lbTyLeSuccess.text = string.Format(Localization.instance.Get("TyLeThanhCongKhaiQuang"), CommonHelper.GetKhaiQuangTiLeThanhCong(TrangBi, soLuongSuDung));
	}

	public void btnMinus_OnClick(GameObject go)
	{
		if (soLuongSuDung > 1)
		{
			soLuongSuDung--;
			if (soLuongSuDung == 1)
			{
				btnMinus.gameObject.SetActive(false);
				btnDoubleMinus.gameObject.SetActive(false);
				isStartChoice = true;
			}
			lbSoLuong.text = instance.soLuongSuDung.ToString();
			lbTyLeSuccess.text = string.Format(Localization.instance.Get("TyLeThanhCongKhaiQuang"), CommonHelper.GetKhaiQuangTiLeThanhCong(TrangBi, soLuongSuDung));
		}
	}

	public void btnDoubleMinus_OnClick(GameObject go)
	{
		soLuongSuDung -= 10;
		if (soLuongSuDung <= 1)
		{
			soLuongSuDung = 1;
			btnMinus.gameObject.SetActive(false);
			btnDoubleMinus.gameObject.SetActive(false);
			isStartChoice = true;
		}
		lbSoLuong.text = instance.soLuongSuDung.ToString();
		lbTyLeSuccess.text = string.Format(Localization.instance.Get("TyLeThanhCongKhaiQuang"), CommonHelper.GetKhaiQuangTiLeThanhCong(TrangBi, soLuongSuDung));
	}

	public void btnClose_OnClick(GameObject go)
	{
		DestroyPopup();
	}
}
