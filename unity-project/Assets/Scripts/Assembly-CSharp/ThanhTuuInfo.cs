using System;
using UnityEngine;

public class ThanhTuuInfo : MonoBehaviour
{
	public NhanVatAvatar GiangHoHaoKiet;

	public NhanVatAvatar QuyTanThanPhan;

	public NhanVatAvatar ThanhDanhHienHach;

	public NhanVatAvatar CuuTinhLienHoan;

	public NhanVatAvatar DungSiXungTran;

	public NhanVatAvatar TuyTamVoHoc;

	public NhanVatAvatar LienMinhThanhVien;

	public NhanVatAvatar ThienHaVoSong;

	public NhanVatAvatar VoDuLuanBi;

	public NhanVatAvatar NhatDaiTonSu;

	public NhanVatAvatar NgaoThiQuanHung;

	public NhanVatAvatar HanHuuDichThu;

	public NhanVatAvatar LoHoaThuanThanh;

	public NhanVatAvatar GiaKinhTuuThuc;

	public NhanVatAvatar KinhTamDongPhach;

	public NhanVatAvatar KinhThienDongDia;

	public NhanVatAvatar KinhTheHaiTuc;

	public NhanVatAvatar ThanCongCaiThe;

	public NhanVatAvatar DangPhongTaoCuc;

	public UILabel nameLabel;

	public UILabel levelLabel;

	public UILabel khiTheLabel;

	public NhanVatAvatar khiTheAvatar;

	public UILabel descLabel;

	public UIGrid gridThanhTuu;

	private static readonly Color enableColor = Color.white;

	private static readonly Color disableColor = Utils.MakeColor(80, 80, 80);

	private UserInfo.DanhHieuData danhHieu = new UserInfo.DanhHieuData();

	private void Awake()
	{
		GiangHoHaoKiet.OnEventClick = SetGiangHoHaoKiet;
		QuyTanThanPhan.OnEventClick = SetQuyTanThanPhan;
		ThanhDanhHienHach.OnEventClick = SetThanhDanhHienHach;
		CuuTinhLienHoan.OnEventClick = SetCuuTinhLienHoan;
		DungSiXungTran.OnEventClick = SetDungSiXungTran;
		TuyTamVoHoc.OnEventClick = SetTuyTamVoHoc;
		LienMinhThanhVien.OnEventClick = SetLienMinhThanhVien;
		ThienHaVoSong.OnEventClick = SetThienHaVoSong;
		VoDuLuanBi.OnEventClick = SetVoDuLuanBi;
		NhatDaiTonSu.OnEventClick = SetNhatDaiTonSu;
		NgaoThiQuanHung.OnEventClick = SetNgaoThiQuanHung;
		HanHuuDichThu.OnEventClick = SetHanHuuDichThu;
		LoHoaThuanThanh.OnEventClick = SetLoHoaThuanThanh;
		GiaKinhTuuThuc.OnEventClick = SetGiaKinhTuuThuc;
		KinhTamDongPhach.OnEventClick = SetKinhTamDongPhach;
		KinhThienDongDia.OnEventClick = SetKinhThienDongDia;
		KinhTheHaiTuc.OnEventClick = SetKinhTheHaiTuc;
		ThanCongCaiThe.OnEventClick = SetThanCongCaiThe;
		DangPhongTaoCuc.OnEventClick = SetDangPhongTaoCuc;
	}

	public void SyncWithNetworkData(UserInfo.DanhHieuData dh)
	{
		if (dh == null)
		{
			danhHieu = new UserInfo.DanhHieuData();
		}
		else
		{
			danhHieu = dh;
		}
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		GiangHoHaoKiet.avatar.color = ((danhHieu.GiangHoHaoKiet <= 0) ? disableColor : enableColor);
		QuyTanThanPhan.avatar.color = ((danhHieu.QuyTanThanPhan <= 0) ? disableColor : enableColor);
		ThanhDanhHienHach.avatar.color = ((danhHieu.ThanhDanhHienHach <= 0) ? disableColor : enableColor);
		CuuTinhLienHoan.avatar.color = ((danhHieu.DucHuyetPhanChien <= 0) ? disableColor : enableColor);
		DungSiXungTran.avatar.color = ((danhHieu.DungSiXungTran <= 0) ? disableColor : enableColor);
		TuyTamVoHoc.avatar.color = ((danhHieu.TuyTamVoHoc <= 0) ? disableColor : enableColor);
		LienMinhThanhVien.gameObject.SetActive(false);
		ThienHaVoSong.gameObject.SetActive(danhHieu.ThienHaVoSong > serverTime);
		VoDuLuanBi.gameObject.SetActive(danhHieu.VoDuLuanBi > serverTime);
		NhatDaiTonSu.gameObject.SetActive(danhHieu.NhatDaiTonSu > serverTime);
		NgaoThiQuanHung.gameObject.SetActive(danhHieu.NgaoThiQuanHung > serverTime);
		HanHuuDichThu.gameObject.SetActive(danhHieu.HanHuuDichThu > serverTime);
		LoHoaThuanThanh.gameObject.SetActive(danhHieu.LoHoaThuanThanh > serverTime);
		GiaKinhTuuThuc.gameObject.SetActive(danhHieu.GiaKinhTuuThuc > serverTime);
		KinhTamDongPhach.gameObject.SetActive(danhHieu.KinhTamDongPhach > serverTime);
		KinhThienDongDia.gameObject.SetActive(danhHieu.KinhThienDongDia > serverTime);
		KinhTheHaiTuc.gameObject.SetActive(danhHieu.KinhTheHaiTuc > serverTime);
		ThanCongCaiThe.gameObject.SetActive(danhHieu.ThanCongCaiThe > serverTime);
		DangPhongTaoCuc.gameObject.SetActive(danhHieu.DangPhongTaoCuc > serverTime);
		gridThanhTuu.Reposition();
	}

	public void SetGiangHoHaoKiet(NhanVatAvatar avatar)
	{
		int giangHoHaoKiet = danhHieu.GiangHoHaoKiet;
		nameLabel.text = Localization.instance.Get("ThanhTuuGiangHoName");
		levelLabel.text = string.Format(Localization.instance.Get("ThanhTuuLevelLabel"), giangHoHaoKiet);
		int khiTheGiangHo = ConfigManager.GetKhiTheGiangHo(giangHoHaoKiet);
		khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), khiTheGiangHo);
		GiangHoCfg giangHoByLevelDanhHieu = ConfigManager.GetGiangHoByLevelDanhHieu(giangHoHaoKiet + 1);
		khiTheAvatar.Set("DH_GIANG_HO_HAO_KIET");
		if (giangHoByLevelDanhHieu == null)
		{
			descLabel.text = Localization.instance.Get("ThanhTuuMaxLevel");
		}
		else
		{
			descLabel.text = string.Format(Localization.instance.Get("ThanhTuuGiangHoDesc"), giangHoByLevelDanhHieu.TenHienThi, ConfigManager.GetKhiTheGiangHo(giangHoHaoKiet + 1));
		}
	}

	public void SetQuyTanThanPhan(NhanVatAvatar avatar)
	{
		int quyTanThanPhan = danhHieu.QuyTanThanPhan;
		nameLabel.text = Localization.instance.Get("ThanhTuuVipName");
		levelLabel.text = string.Format(Localization.instance.Get("ThanhTuuLevelLabel"), quyTanThanPhan);
		int khiTheVipEffect = ConfigManager.GetKhiTheVipEffect(quyTanThanPhan);
		khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel") + "%", khiTheVipEffect);
		int vipByLevelDanhHieu = ConfigManager.GetVipByLevelDanhHieu(quyTanThanPhan + 1);
		khiTheAvatar.Set("DH_QUY_TAN_THAN_PHAN");
		descLabel.text = string.Format(Localization.instance.Get("ThanhTuuVipDesc"), vipByLevelDanhHieu, ConfigManager.GetKhiTheVipEffect(quyTanThanPhan + 1));
	}

	public void SetThanhDanhHienHach(NhanVatAvatar avatar)
	{
		int thanhDanhHienHach = danhHieu.ThanhDanhHienHach;
		nameLabel.text = Localization.instance.Get("ThanhTuuLuanKiemName");
		levelLabel.text = string.Format(Localization.instance.Get("ThanhTuuLevelLabel"), thanhDanhHienHach);
		int khiTheLuanKiem = ConfigManager.GetKhiTheLuanKiem(thanhDanhHienHach);
		khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), khiTheLuanKiem);
		int hangLuanKiemByLevelDanhHieu = ConfigManager.GetHangLuanKiemByLevelDanhHieu(thanhDanhHienHach + 1);
		int khiTheLuanKiem2 = ConfigManager.GetKhiTheLuanKiem(thanhDanhHienHach + 1);
		khiTheAvatar.Set("DH_THANH_DANH_HIEN_HACH");
		if (hangLuanKiemByLevelDanhHieu == 1)
		{
			descLabel.text = Localization.instance.Get("ThanhTuuMaxLevel");
		}
		else
		{
			descLabel.text = string.Format(Localization.instance.Get("ThanhTuuLuanKiemDesc"), hangLuanKiemByLevelDanhHieu, khiTheLuanKiem2);
		}
	}

	public void SetCuuTinhLienHoan(NhanVatAvatar avatar)
	{
		int ducHuyetPhanChien = danhHieu.DucHuyetPhanChien;
		nameLabel.text = Localization.instance.Get("ThanhTuuHuyetChienName");
		levelLabel.text = string.Format(Localization.instance.Get("ThanhTuuLevelLabel"), ducHuyetPhanChien);
		int khiTheHuyetChien = ConfigManager.GetKhiTheHuyetChien(ducHuyetPhanChien);
		khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), khiTheHuyetChien);
		int luotHuyetChienByLevelDanhHieu = ConfigManager.GetLuotHuyetChienByLevelDanhHieu(ducHuyetPhanChien + 1);
		int khiTheHuyetChien2 = ConfigManager.GetKhiTheHuyetChien(ducHuyetPhanChien + 1);
		khiTheAvatar.Set("DH_CUU_TINH_LIEN_HOAN");
		if (khiTheHuyetChien2 == khiTheHuyetChien)
		{
			descLabel.text = Localization.instance.Get("ThanhTuuMaxLevel");
		}
		else
		{
			descLabel.text = string.Format(Localization.instance.Get("ThanhTuuHuyetChienDesc"), luotHuyetChienByLevelDanhHieu, khiTheHuyetChien2);
		}
	}

	public void SetDungSiXungTran(NhanVatAvatar avatar)
	{
		int dungSiXungTran = danhHieu.DungSiXungTran;
		nameLabel.text = Localization.instance.Get("ThanhTuuDongNhanName");
		levelLabel.text = string.Format(Localization.instance.Get("ThanhTuuLevelLabel"), dungSiXungTran);
		int khiTheDungSiXungTran = ConfigManager.GetKhiTheDungSiXungTran(dungSiXungTran);
		khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), khiTheDungSiXungTran);
		int luotDongNhanByLevelDanhHieu = ConfigManager.GetLuotDongNhanByLevelDanhHieu(dungSiXungTran + 1);
		int khiTheDungSiXungTran2 = ConfigManager.GetKhiTheDungSiXungTran(dungSiXungTran + 1);
		khiTheAvatar.Set("DH_DUNG_SI_XUNG_TRAN");
		if (khiTheDungSiXungTran2 == khiTheDungSiXungTran)
		{
			descLabel.text = Localization.instance.Get("ThanhTuuMaxLevel");
		}
		else
		{
			descLabel.text = string.Format(Localization.instance.Get("ThanhTuuDongNhanDesc"), luotDongNhanByLevelDanhHieu, khiTheDungSiXungTran2);
		}
	}

	public void SetTuyTamVoHoc(NhanVatAvatar avatar)
	{
		int tuyTamVoHoc = danhHieu.TuyTamVoHoc;
		nameLabel.text = Localization.instance.Get("ThanhTuuTuyTamVoHocName");
		levelLabel.text = string.Format(Localization.instance.Get("ThanhTuuLevelLabel"), tuyTamVoHoc);
		int khiTheTuyTamVoHoc = ConfigManager.GetKhiTheTuyTamVoHoc(tuyTamVoHoc);
		khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), khiTheTuyTamVoHoc);
		int luotTuyTamVoHocByLevelDanhHieu = ConfigManager.GetLuotTuyTamVoHocByLevelDanhHieu(tuyTamVoHoc + 1);
		int khiTheTuyTamVoHoc2 = ConfigManager.GetKhiTheTuyTamVoHoc(tuyTamVoHoc + 1);
		khiTheAvatar.Set("DH_TUY_TAM_VO_HOC");
		if (khiTheTuyTamVoHoc2 == khiTheTuyTamVoHoc)
		{
			descLabel.text = Localization.instance.Get("ThanhTuuMaxLevel");
		}
		else
		{
			descLabel.text = string.Format(Localization.instance.Get("ThanhTuuTuyTamVoHocDesc"), luotTuyTamVoHocByLevelDanhHieu, khiTheTuyTamVoHoc2);
		}
	}

	public void SetLienMinhThanhVien(NhanVatAvatar avatar)
	{
		int lienMinhThanhVien = danhHieu.LienMinhThanhVien;
		nameLabel.text = Localization.instance.Get("ThanhTuuLienMinhThanhVienName");
		levelLabel.text = string.Format(Localization.instance.Get("ThanhTuuLevelLabel"), lienMinhThanhVien);
		int khiTheLienMinhThanhVien = ConfigManager.GetKhiTheLienMinhThanhVien(lienMinhThanhVien);
		khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), khiTheLienMinhThanhVien);
		int levelLienMinhFromLevelDanhHieu = ConfigManager.GetLevelLienMinhFromLevelDanhHieu(lienMinhThanhVien + 1);
		int khiTheLienMinhThanhVien2 = ConfigManager.GetKhiTheLienMinhThanhVien(lienMinhThanhVien + 1);
		khiTheAvatar.Set("DH_LIEN_MINH_THANH_VIEN");
		if (khiTheLienMinhThanhVien2 == khiTheLienMinhThanhVien)
		{
			descLabel.text = Localization.instance.Get("ThanhTuuMaxLevel");
		}
		else
		{
			descLabel.text = string.Format(Localization.instance.Get("ThanhTuuLienMinhThanhVienDesc"), levelLienMinhFromLevelDanhHieu, khiTheLienMinhThanhVien2);
		}
	}

	public void SetThienHaVoSong(NhanVatAvatar avatar)
	{
		if (danhHieu.ThienHaVoSong > GameManager.instance.m_GameClient.ServerTime)
		{
			int num = 40;
			khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), num);
		}
		else
		{
			int num2 = 0;
			khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), num2);
		}
		nameLabel.text = Localization.instance.Get("ThanhTuuThienHaVoSongName");
		levelLabel.text = string.Empty;
		khiTheAvatar.Set("DH_THIEN_HA_VO_SONG");
		TimeSpan timeSpan = danhHieu.ThienHaVoSong - GameManager.instance.m_GameClient.ServerTime;
		descLabel.text = string.Format(Localization.instance.Get("ThanhTuuThienHaVoSongDesc"), string.Format(Localization.instance.Get("ThoiGianFormatFullHHMM"), (int)timeSpan.TotalHours, timeSpan.Minutes + 1));
	}

	public void SetVoDuLuanBi(NhanVatAvatar avatar)
	{
		if (danhHieu.VoDuLuanBi > GameManager.instance.m_GameClient.ServerTime)
		{
			int num = 35;
			khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), num);
		}
		else
		{
			int num2 = 0;
			khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), num2);
		}
		nameLabel.text = Localization.instance.Get("ThanhTuuVoDuLuanBiName");
		levelLabel.text = string.Empty;
		khiTheAvatar.Set("DH_VO_DU_LUAN_BI");
		TimeSpan timeSpan = danhHieu.VoDuLuanBi - GameManager.instance.m_GameClient.ServerTime;
		EGDebug.Log(timeSpan.Minutes.ToString());
		EGDebug.Log(GameManager.instance.m_GameClient.ServerTime.ToString());
		EGDebug.Log(danhHieu.VoDuLuanBi.ToString());
		descLabel.text = string.Format(Localization.instance.Get("ThanhTuuVoDuLuanBiDesc"), string.Format(Localization.instance.Get("ThoiGianFormatFullHHMM"), (int)timeSpan.TotalHours, timeSpan.Minutes + 1));
	}

	public void SetNhatDaiTonSu(NhanVatAvatar avatar)
	{
		if (danhHieu.NhatDaiTonSu > GameManager.instance.m_GameClient.ServerTime)
		{
			int num = 30;
			khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), num);
		}
		else
		{
			int num2 = 0;
			khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), num2);
		}
		nameLabel.text = Localization.instance.Get("ThanhTuuNhatDaiTonSuName");
		levelLabel.text = string.Empty;
		khiTheAvatar.Set("DH_NHAT_DAI_TON_SU");
		TimeSpan timeSpan = danhHieu.NhatDaiTonSu - GameManager.instance.m_GameClient.ServerTime;
		descLabel.text = string.Format(Localization.instance.Get("ThanhTuuNhatDaiTonSuDesc"), string.Format(Localization.instance.Get("ThoiGianFormatFullHHMM"), (int)timeSpan.TotalHours, timeSpan.Minutes + 1));
	}

	public void SetNgaoThiQuanHung(NhanVatAvatar avatar)
	{
		if (danhHieu.NgaoThiQuanHung > GameManager.instance.m_GameClient.ServerTime)
		{
			int num = 20;
			khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), num);
		}
		else
		{
			int num2 = 0;
			khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), num2);
		}
		nameLabel.text = Localization.instance.Get("ThanhTuuNgaoThiQuanHungName");
		levelLabel.text = string.Empty;
		khiTheAvatar.Set("DH_NGAO_THI_QUAN_HUNG");
		TimeSpan timeSpan = danhHieu.NgaoThiQuanHung - GameManager.instance.m_GameClient.ServerTime;
		descLabel.text = string.Format(Localization.instance.Get("ThanhTuuNgaoThiQuanHungDesc"), string.Format(Localization.instance.Get("ThoiGianFormatFullHHMM"), (int)timeSpan.TotalHours, timeSpan.Minutes + 1));
	}

	public void SetHanHuuDichThu(NhanVatAvatar avatar)
	{
		if (danhHieu.HanHuuDichThu > GameManager.instance.m_GameClient.ServerTime)
		{
			int num = 15;
			khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), num);
		}
		else
		{
			int num2 = 0;
			khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), num2);
		}
		nameLabel.text = Localization.instance.Get("ThanhTuuHanHuuDichThuName");
		levelLabel.text = string.Empty;
		khiTheAvatar.Set("DH_HAN_HUU_DICH_THU");
		TimeSpan timeSpan = danhHieu.HanHuuDichThu - GameManager.instance.m_GameClient.ServerTime;
		descLabel.text = string.Format(Localization.instance.Get("ThanhTuuHanHuuDichThuDesc"), string.Format(Localization.instance.Get("ThoiGianFormatFullHHMM"), (int)timeSpan.TotalHours, timeSpan.Minutes + 1));
	}

	public void SetLoHoaThuanThanh(NhanVatAvatar avatar)
	{
		if (danhHieu.LoHoaThuanThanh > GameManager.instance.m_GameClient.ServerTime)
		{
			int num = 10;
			khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), num);
		}
		else
		{
			int num2 = 0;
			khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), num2);
		}
		nameLabel.text = Localization.instance.Get("ThanhTuuLoHoaThuanThanhName");
		levelLabel.text = string.Empty;
		khiTheAvatar.Set("DH_LO_HOA_THUAN_THANH");
		TimeSpan timeSpan = danhHieu.LoHoaThuanThanh - GameManager.instance.m_GameClient.ServerTime;
		descLabel.text = string.Format(Localization.instance.Get("ThanhTuuLoHoaThuanThanhDesc"), string.Format(Localization.instance.Get("ThoiGianFormatFullHHMM"), (int)timeSpan.TotalHours, timeSpan.Minutes + 1));
	}

	public void SetGiaKinhTuuThuc(NhanVatAvatar avatar)
	{
		if (danhHieu.GiaKinhTuuThuc > GameManager.instance.m_GameClient.ServerTime)
		{
			int num = 5;
			khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), num);
		}
		else
		{
			int num2 = 0;
			khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), num2);
		}
		nameLabel.text = Localization.instance.Get("ThanhTuuGiaKinhTuuThucName");
		levelLabel.text = string.Empty;
		khiTheAvatar.Set("DH_GIA_KHINH_TUU_THUC");
		TimeSpan timeSpan = danhHieu.GiaKinhTuuThuc - GameManager.instance.m_GameClient.ServerTime;
		descLabel.text = string.Format(Localization.instance.Get("ThanhTuuGiaKinhTuuThucDesc"), string.Format(Localization.instance.Get("ThoiGianFormatFullHHMM"), (int)timeSpan.TotalHours, timeSpan.Minutes + 1));
	}

	public void SetKinhTamDongPhach(NhanVatAvatar avatar)
	{
		if (danhHieu.KinhTamDongPhach > GameManager.instance.m_GameClient.ServerTime)
		{
			int num = 40;
			khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), num);
		}
		else
		{
			int num2 = 0;
			khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), num2);
		}
		nameLabel.text = Localization.instance.Get("ThanhTuuKinhTamDongPhachName");
		levelLabel.text = string.Empty;
		khiTheAvatar.Set("DH_KINH_TAM_DONG_PHACH");
		TimeSpan timeSpan = danhHieu.KinhTamDongPhach - GameManager.instance.m_GameClient.ServerTime;
		descLabel.text = string.Format(Localization.instance.Get("ThanhTuuKinhTamDongPhachDesc"), string.Format(Localization.instance.Get("ThoiGianFormatFullHHMM"), (int)timeSpan.TotalHours, timeSpan.Minutes + 1));
	}

	public void SetKinhThienDongDia(NhanVatAvatar avatar)
	{
		if (danhHieu.KinhThienDongDia > GameManager.instance.m_GameClient.ServerTime)
		{
			int num = 30;
			khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), num);
		}
		else
		{
			int num2 = 0;
			khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), num2);
		}
		nameLabel.text = Localization.instance.Get("ThanhTuuKinhThienDongDiaName");
		levelLabel.text = string.Empty;
		khiTheAvatar.Set("DH_KINH_THIEN_DONG_DIA");
		TimeSpan timeSpan = danhHieu.KinhThienDongDia - GameManager.instance.m_GameClient.ServerTime;
		descLabel.text = string.Format(Localization.instance.Get("ThanhTuuKinhThienDongDiaDesc"), string.Format(Localization.instance.Get("ThoiGianFormatFullHHMM"), (int)timeSpan.TotalHours, timeSpan.Minutes + 1));
	}

	public void SetKinhTheHaiTuc(NhanVatAvatar avatar)
	{
		if (danhHieu.KinhTheHaiTuc > GameManager.instance.m_GameClient.ServerTime)
		{
			int num = 20;
			khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), num);
		}
		else
		{
			int num2 = 0;
			khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), num2);
		}
		nameLabel.text = Localization.instance.Get("ThanhTuuKinhTheHaiTucName");
		levelLabel.text = string.Empty;
		khiTheAvatar.Set("DH_KINH_THE_HAI_TUC");
		TimeSpan timeSpan = danhHieu.KinhTheHaiTuc - GameManager.instance.m_GameClient.ServerTime;
		descLabel.text = string.Format(Localization.instance.Get("ThanhTuuKinhTheHaiTucDesc"), string.Format(Localization.instance.Get("ThoiGianFormatFullHHMM"), (int)timeSpan.TotalHours, timeSpan.Minutes + 1));
	}

	public void SetThanCongCaiThe(NhanVatAvatar avatar)
	{
		if (danhHieu.ThanCongCaiThe > GameManager.instance.m_GameClient.ServerTime)
		{
			int num = 10;
			khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), num);
		}
		else
		{
			int num2 = 0;
			khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), num2);
		}
		nameLabel.text = Localization.instance.Get("ThanhTuuThanCongCaiTheName");
		levelLabel.text = string.Empty;
		khiTheAvatar.Set("DH_THAN_CONG_CAI_THE");
		TimeSpan timeSpan = danhHieu.ThanCongCaiThe - GameManager.instance.m_GameClient.ServerTime;
		descLabel.text = string.Format(Localization.instance.Get("ThanhTuuThanCongCaiTheDesc"), string.Format(Localization.instance.Get("ThoiGianFormatFullHHMM"), (int)timeSpan.TotalHours, timeSpan.Minutes + 1));
	}

	public void SetDangPhongTaoCuc(NhanVatAvatar avatar)
	{
		if (danhHieu.DangPhongTaoCuc > GameManager.instance.m_GameClient.ServerTime)
		{
			int num = 40;
			khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), num);
		}
		else
		{
			int num2 = 0;
			khiTheLabel.text = string.Format(Localization.instance.Get("ThanhTuuKhiTheLabel"), num2);
		}
		nameLabel.text = Localization.instance.Get("ThanhTuuDangPhongTaoCucName");
		levelLabel.text = string.Empty;
		khiTheAvatar.Set("DH_DANG_PHONG_TAO_CUC");
		TimeSpan timeSpan = danhHieu.DangPhongTaoCuc - GameManager.instance.m_GameClient.ServerTime;
		descLabel.text = string.Format(Localization.instance.Get("ThanhTuuDangPhongTaoCucDesc"), string.Format(Localization.instance.Get("ThoiGianFormatFullHHMM"), (int)timeSpan.TotalHours, timeSpan.Minutes + 1));
	}
}
