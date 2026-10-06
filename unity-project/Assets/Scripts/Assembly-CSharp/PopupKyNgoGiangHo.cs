using UnityEngine;

public class PopupKyNgoGiangHo : MonoBehaviour
{
	public static PopupKyNgoGiangHo instance;

	public UILabel titleLabel;

	public UILabel descLabel;

	public KyNgoAvatar avatar;

	public static PopupKyNgoGiangHo Create(PhanThuongResponse.PhanThuong pt)
	{
		DestroyPopup();
		Object obj = Object.Instantiate(Resources.Load("popup/PopupKyNgoGiangHo"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		PopupManager.instance.Add(gameObject);
		PopupKyNgoGiangHo component = gameObject.GetComponent<PopupKyNgoGiangHo>();
		component.SetInfo(pt);
		instance = component;
		gameObject.transform.localScale = Vector3.one;
		return component;
	}

	public void SetInfo(PhanThuongResponse.PhanThuong pt)
	{
		if (pt.Loai == PhanThuongResponse.LoaiPhanThuong.CAO_NHAN)
		{
			titleLabel.text = Localization.instance.Get("CaoNhanPopupTitle");
			descLabel.text = Localization.instance.Get("CaoNhanPopupDesc");
			avatar.SetInfo("kyngo_caonhan");
		}
		else if (pt.Loai == PhanThuongResponse.LoaiPhanThuong.BAN_DO)
		{
			titleLabel.text = Localization.instance.Get("BanDoPopupTitle");
			descLabel.text = Localization.instance.Get("BanDoPopupDesc");
			avatar.SetInfo("kyngo_khobau");
		}
		else if (pt.Loai == PhanThuongResponse.LoaiPhanThuong.BANG_HUU)
		{
			titleLabel.text = Localization.instance.Get("BangHuuPopupTitle");
			descLabel.text = Localization.instance.Get("BangHuuPopupDesc");
			avatar.SetInfo("kyngo_banghuu");
		}
		else if (pt.Loai == PhanThuongResponse.LoaiPhanThuong.THUONG_NHAN)
		{
			titleLabel.text = Localization.instance.Get("ThuongNhanPopupTitle");
			descLabel.text = Localization.instance.Get("ThuongNhanPopupDesc");
			avatar.SetInfo("kyngo_thuongnhan");
		}
		else if (pt.Loai == PhanThuongResponse.LoaiPhanThuong.TY_THI)
		{
			titleLabel.text = Localization.instance.Get("TyThiPopupTitle");
			descLabel.text = Localization.instance.Get("TyThiPopupDesc");
			avatar.SetInfo("kyngo_tythi");
		}
		avatar.nameLabel.gameObject.SetActive(false);
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	private void OnCloseClick()
	{
		DestroyPopup();
	}

	private void OnGoToClick()
	{
		if (GiangHoPopup.instance != null)
		{
			GiangHoPopup.instance.gameObject.SetActive(false);
		}
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenKyNgoGiangHo);
		DestroyPopup();
	}
}
