using UnityEngine;

public class PopupDuocThanhTuu : MonoBehaviour
{
	public UILabel descLabel;

	public UILabel nameLabel;

	public UILabel levelLabel;

	public UILabel khiTheLabel;

	public NhanVatAvatar avatar;

	public static PopupDuocThanhTuu instance;

	private static PopupDuocThanhTuu CreatePopup()
	{
		DestroyPopup();
		Object obj = Object.Instantiate(Resources.Load("Popup/PopupDuocThanhTuu"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		instance = gameObject.GetComponent<PopupDuocThanhTuu>();
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		return instance;
	}

	public static PopupDuocThanhTuu CreateGiangHoHaoKiet(UserInfo.DanhHieuData thanhTuu)
	{
		PopupDuocThanhTuu popupDuocThanhTuu = CreatePopup();
		popupDuocThanhTuu.SetGiangHoHaoKiet(thanhTuu);
		return popupDuocThanhTuu;
	}

	public static PopupDuocThanhTuu CreateQuyTanThanPhan(UserInfo.DanhHieuData thanhTuu)
	{
		PopupDuocThanhTuu popupDuocThanhTuu = CreatePopup();
		popupDuocThanhTuu.SetQuyTanThanPhan(thanhTuu);
		return popupDuocThanhTuu;
	}

	public static PopupDuocThanhTuu CreateThanhDanhHienHach(UserInfo.DanhHieuData thanhTuu)
	{
		PopupDuocThanhTuu popupDuocThanhTuu = CreatePopup();
		popupDuocThanhTuu.SetThanhDanhHienHach(thanhTuu);
		return popupDuocThanhTuu;
	}

	public static PopupDuocThanhTuu CreateCuuTinhLienHoan(UserInfo.DanhHieuData thanhTuu)
	{
		PopupDuocThanhTuu popupDuocThanhTuu = CreatePopup();
		popupDuocThanhTuu.SetCuuTinhLienHoan(thanhTuu);
		return popupDuocThanhTuu;
	}

	public static PopupDuocThanhTuu CreateDungSiXungTran(UserInfo.DanhHieuData thanhTuu)
	{
		PopupDuocThanhTuu popupDuocThanhTuu = CreatePopup();
		popupDuocThanhTuu.SetDungSiXungTran(thanhTuu);
		return popupDuocThanhTuu;
	}

	public static PopupDuocThanhTuu CreateTuyTamVoHoc(UserInfo.DanhHieuData thanhTuu)
	{
		PopupDuocThanhTuu popupDuocThanhTuu = CreatePopup();
		popupDuocThanhTuu.SetTuyTamVoHoc(thanhTuu);
		return popupDuocThanhTuu;
	}

	public static PopupDuocThanhTuu CreateLienMinhThanhVien(UserInfo.DanhHieuData thanhTuu)
	{
		PopupDuocThanhTuu popupDuocThanhTuu = CreatePopup();
		popupDuocThanhTuu.SetLienMinhThanhVien(thanhTuu);
		return popupDuocThanhTuu;
	}

	public void SetGiangHoHaoKiet(UserInfo.DanhHieuData thanhTuu)
	{
		if (thanhTuu != null)
		{
			nameLabel.text = Localization.instance.Get("ThanhTuuGiangHoName");
			int giangHoHaoKiet = thanhTuu.GiangHoHaoKiet;
			levelLabel.text = string.Format(Localization.instance.Get("ThanhTuuLevelLabel"), giangHoHaoKiet);
			int khiTheGiangHo = ConfigManager.GetKhiTheGiangHo(giangHoHaoKiet);
			khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), khiTheGiangHo);
			descLabel.text = Localization.instance.Get("PopupThanhTuuDesc");
			avatar.Set("DH_GIANG_HO_HAO_KIET");
		}
	}

	public void SetQuyTanThanPhan(UserInfo.DanhHieuData thanhTuu)
	{
		if (thanhTuu != null)
		{
			nameLabel.text = Localization.instance.Get("ThanhTuuVipName");
			int quyTanThanPhan = thanhTuu.QuyTanThanPhan;
			levelLabel.text = string.Format(Localization.instance.Get("ThanhTuuLevelLabel"), quyTanThanPhan);
			int khiTheVipEffect = ConfigManager.GetKhiTheVipEffect(quyTanThanPhan);
			khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel") + "%", khiTheVipEffect);
			descLabel.text = Localization.instance.Get("PopupThanhTuuDesc");
			avatar.Set("DH_QUY_TAN_THAN_PHAN");
		}
	}

	public void SetThanhDanhHienHach(UserInfo.DanhHieuData thanhTuu)
	{
		if (thanhTuu != null)
		{
			nameLabel.text = Localization.instance.Get("ThanhTuuLuanKiemName");
			int thanhDanhHienHach = thanhTuu.ThanhDanhHienHach;
			levelLabel.text = string.Format(Localization.instance.Get("ThanhTuuLevelLabel"), thanhDanhHienHach);
			int khiTheLuanKiem = ConfigManager.GetKhiTheLuanKiem(thanhDanhHienHach);
			khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), khiTheLuanKiem);
			descLabel.text = Localization.instance.Get("PopupThanhTuuDesc");
			avatar.Set("DH_THANH_DANH_HIEN_HACH");
		}
	}

	public void SetCuuTinhLienHoan(UserInfo.DanhHieuData thanhTuu)
	{
		if (thanhTuu != null)
		{
			nameLabel.text = Localization.instance.Get("ThanhTuuHuyetChienName");
			int ducHuyetPhanChien = thanhTuu.DucHuyetPhanChien;
			levelLabel.text = string.Format(Localization.instance.Get("ThanhTuuLevelLabel"), ducHuyetPhanChien);
			int khiTheHuyetChien = ConfigManager.GetKhiTheHuyetChien(ducHuyetPhanChien);
			khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), khiTheHuyetChien);
			descLabel.text = Localization.instance.Get("PopupThanhTuuDesc");
			avatar.Set("DH_CUU_TINH_LIEN_HOAN");
		}
	}

	public void SetDungSiXungTran(UserInfo.DanhHieuData thanhTuu)
	{
		if (thanhTuu != null)
		{
			nameLabel.text = Localization.instance.Get("ThanhTuuDongNhanName");
			int dungSiXungTran = thanhTuu.DungSiXungTran;
			levelLabel.text = string.Format(Localization.instance.Get("ThanhTuuLevelLabel"), dungSiXungTran);
			int khiTheDungSiXungTran = ConfigManager.GetKhiTheDungSiXungTran(dungSiXungTran);
			khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), khiTheDungSiXungTran);
			descLabel.text = Localization.instance.Get("PopupThanhTuuDesc");
			avatar.Set("DH_DUNG_SI_XUNG_TRAN");
		}
	}

	public void SetTuyTamVoHoc(UserInfo.DanhHieuData thanhTuu)
	{
		if (thanhTuu != null)
		{
			nameLabel.text = Localization.instance.Get("ThanhTuuTuyTamVoHocName");
			int tuyTamVoHoc = thanhTuu.TuyTamVoHoc;
			levelLabel.text = string.Format(Localization.instance.Get("ThanhTuuLevelLabel"), tuyTamVoHoc);
			int khiTheTuyTamVoHoc = ConfigManager.GetKhiTheTuyTamVoHoc(tuyTamVoHoc);
			khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), khiTheTuyTamVoHoc);
			descLabel.text = Localization.instance.Get("PopupThanhTuuDesc");
			avatar.Set("DH_TUY_TAM_VO_HOC");
		}
	}

	public void SetLienMinhThanhVien(UserInfo.DanhHieuData thanhTuu)
	{
		if (thanhTuu != null)
		{
			nameLabel.text = Localization.instance.Get("ThanhTuuLienMinhThanhVienName");
			int lienMinhThanhVien = thanhTuu.LienMinhThanhVien;
			levelLabel.text = string.Format(Localization.instance.Get("ThanhTuuLevelLabel"), lienMinhThanhVien);
			int khiTheLienMinhThanhVien = ConfigManager.GetKhiTheLienMinhThanhVien(lienMinhThanhVien);
			khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), khiTheLienMinhThanhVien);
			descLabel.text = Localization.instance.Get("PopupThanhTuuDesc");
			avatar.Set("DH_LIEN_MINH_THANH_VIEN");
		}
	}

	private void OnBackBtnClick()
	{
		DestroyPopup();
	}

	private void OnAvatarClick()
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenDoiHinh);
		ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
		screenDoiHinh.btnThanhTuu_OnClick(null);
		DestroyPopup();
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
		}
	}
}
