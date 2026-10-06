using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class ScreenChienThuat : ScreenBase
{
	public NhanVatAvatar avatar1;

	public NhanVatAvatar avatar2;

	public NhanVatAvatar avatar3;

	public NhanVatAvatar avatar4;

	public NhanVatAvatar avatar5;

	public NhanVatAvatar avatar6;

	public NhanVatAvatar avatar7;

	public NhanVatAvatar avatar8;

	public GameObject PanelChieuThuc;

	public GameObject PanelTrangThai;

	public UICheckbox optionChuDong;

	public UICheckbox optionBiDong;

	public UICheckbox optionBaoVe;

	public GameObject btnDiDongDisabled;

	public GameObject PanelChuDong;

	public GameObject PanelBaoVeBiDong;

	public GameObject PanelTanCongBiDong;

	public UICheckbox optionChuDongCaoCap;

	public UICheckbox optionBiDongCaoCap;

	public UICheckbox optionKeThuMucTieuCaoCap;

	public UICheckbox optionChuDongGanNhat;

	public UICheckbox optionChuDongMauNhieuNhat;

	public UICheckbox optionChuDongMauItNhat;

	public UICheckbox optionChuDongThanPhapCaoNhat;

	public UICheckbox optionChuDongThanPhapThapNhat;

	public UICheckbox optionChuDongCongToNhat;

	public UICheckbox optionChuDongCongNhoNhat;

	public UICheckbox optionChuDongKhiLonNhat;

	public UICheckbox optionChuDongKhiNhoNhat;

	public UICheckbox optionChuDongSlot;

	public GameObject optionChuDongSlots;

	public GameObject optionChuDongSlot1;

	public GameObject optionChuDongSlot2;

	public GameObject optionChuDongSlot3;

	public GameObject optionChuDongSlot4;

	public GameObject optionChuDongAdvanceGrp;

	public GameObject optionChuDongBasicGrp;

	public UICheckbox optionBiDongGanNhat;

	public UICheckbox optionBiDongMauNhieuNhat;

	public UICheckbox optionBiDongMauItNhat;

	public UICheckbox optionBiDongThanPhapCaoNhat;

	public UICheckbox optionBiDongThanPhapThapNhat;

	public UICheckbox optionBiDongCongToNhat;

	public UICheckbox optionBiDongCongNhoNhat;

	public UICheckbox optionBiDongKhiLonNhat;

	public UICheckbox optionBiDongKhiNhoNhat;

	public UICheckbox optionBiDongSlot;

	public GameObject optionBiDongSlots;

	public GameObject optionBiDongSlot1;

	public GameObject optionBiDongSlot2;

	public GameObject optionBiDongSlot3;

	public GameObject optionBiDongSlot4;

	public GameObject optionBiDongAdvanceGrp;

	public GameObject optionBiDongBasicGrp;

	public MucTieuBaoVe[] targets;

	public OtherAvatar[] chieuThucAvatars;

	public GameObject DongDoiDungKhiGrp;

	public GameObject DongDoiMucTieuGrp;

	public GameObject DongDoiNgungKhiGrp;

	public GameObject KeThuDungKhiGrp;

	public GameObject KeThuMucTieuGrp;

	public GameObject KeThuNgungKhiGrp;

	public UICheckbox MucTieuChk;

	public UICheckbox DungKhiChk;

	public UICheckbox NgungKhiChk;

	public UICheckbox optionKeThuMucTieuDangTanCong;

	public UICheckbox optionKeThuMucTieuMauNhieuNhat;

	public UICheckbox optionKeThuMucTieuMauItNhat;

	public UICheckbox optionKeThuMucTieuThanPhapNhanhNhat;

	public UICheckbox optionKeThuMucTieuThanPhapChamNhat;

	public UICheckbox optionKeThuMucTieuCongToNhat;

	public UICheckbox optionKeThuMucTieuCongNhoNhat;

	public UICheckbox optionKeThuMucTieuKhiLonNhat;

	public UICheckbox optionKeThuMucTieuKhiNhoNhat;

	public UICheckbox optionKeThuMucTieuSlot;

	public GameObject optionKeThuMucTieuSlots;

	public GameObject optionKeThuMucTieuSlot1;

	public GameObject optionKeThuMucTieuSlot2;

	public GameObject optionKeThuMucTieuSlot3;

	public GameObject optionKeThuMucTieuSlot4;

	public GameObject optionKeThuMucTieuAdvanceGrp;

	public GameObject optionKeThuMucTieuBasicGrp;

	public UICheckbox optionKeThuDungKhiNgayKhiCoThe;

	public UICheckbox optionKeThuDungKhiMauMucTieuDuoi;

	public UISlider sliderKeThuDungKhiMauMucTieuDuoi;

	public UILabel labelKeThuDungKhiMauMucTieuDuoi;

	public GameObject btnKeThuDungKhiMauMucTieuDuoi;

	public UICheckbox optionKeThuDungKhiMauMucTieuTren;

	public UISlider sliderKeThuDungKhiMauMucTieuTren;

	public UILabel labelKeThuDungKhiMauMucTieuTren;

	public GameObject btnKeThuDungKhiMauMucTieuTren;

	public UICheckbox optionKeThuDungKhiMauBanThanDuoi;

	public UISlider sliderKeThuDungKhiMauBanThanDuoi;

	public UILabel labelKeThuDungKhiMauBanThanDuoi;

	public GameObject btnKeThuDungKhiMauBanThanDuoi;

	public UICheckbox optionKeThuNgungKhiNoiLucDuoi;

	public UISlider sliderKeThuNgungKhiNoiLucDuoi;

	public UILabel labelKeThuNgungKhiNoiLucDuoi;

	public GameObject btnKeThuNgungKhiNoiLucDuoi;

	public UICheckbox optionKeThuNgungKhiMauBanThanDuoi;

	public UISlider sliderKeThuNgungKhiMauBanThanDuoi;

	public UILabel labelKeThuNgungKhiMauBanThanDuoi;

	public GameObject btnKeThuNgungKhiMauBanThanDuoi;

	public MucTieuHoTro dongDoiMucTieu1Avatar;

	public MucTieuHoTro dongDoiMucTieu2Avatar;

	public MucTieuHoTro dongDoiMucTieu3Avatar;

	public MucTieuHoTro dongDoiMucTieu4Avatar;

	public UICheckbox optionDongDoiDungKhiNgayKhiCoThe;

	public UICheckbox optionDongDoiDungKhiMauMucTieuDuoi;

	public UISlider sliderDongDoiDungKhiMauMucTieuDuoi;

	public GameObject btnDongDoiDungKhiMauMucTieuDuoi;

	public UILabel labelDongDoiDungKhiMauMucTieuDuoi;

	public UICheckbox optionDongDoiDungKhiMauMucTieuTren;

	public UISlider sliderDongDoiDungKhiMauMucTieuTren;

	public GameObject btnDongDoiDungKhiMauMucTieuTren;

	public UILabel labelDongDoiDungKhiMauMucTieuTren;

	public UICheckbox optionDongDoiDungKhiMauBanThanDuoi;

	public UISlider sliderDongDoiDungKhiMauBanThanDuoi;

	public UILabel labelDongDoiDungKhiMauBanThanDuoi;

	public GameObject btnDongDoiDungKhiMauBanThanDuoi;

	public UICheckbox optionDongDoiDungKhiKhiBiTrangThaiXau;

	public UICheckbox optionDongDoiNgungKhiNoiLucDuoi;

	public UISlider sliderDongDoiNgungKhiNoiLucDuoi;

	public UILabel labelDongDoiNgungKhiNoiLucDuoi;

	public GameObject btnDongDoiNgungKhiNoiLucDuoi;

	public UICheckbox optionDongDoiNgungKhiMauBanThanDuoi;

	public UISlider sliderDongDoiNgungKhiMauBanThanDuoi;

	public UILabel labelDongDoiNgungKhiMauBanThanDuoi;

	public GameObject btnDongDoiNgungKhiMauBanThanDuoi;

	private int SelectedSlotNhanVat;

	private UserInfo.HeroData heroData;

	private UserInfo.VCThietLapData vcThietLap;

	private int selectedChieuThuc;

	private static readonly Vector3[] posOrder = new Vector3[4]
	{
		new Vector3(0f, 0f),
		new Vector3(120f, 0f),
		new Vector3(240f, 0f),
		new Vector3(360f, 0f)
	};

	private Vector3 previous1PosChuDong;

	private Vector3 previous2PosChuDong;

	private Vector3 previous3PosChuDong;

	private Vector3 previous4PosChuDong;

	private Vector3 previous1PosBiDong;

	private Vector3 previous2PosBiDong;

	private Vector3 previous3PosBiDong;

	private Vector3 previous4PosBiDong;

	private Vector3 previous1PosKeThuMucTieu;

	private Vector3 previous2PosKeThuMucTieu;

	private Vector3 previous3PosKeThuMucTieu;

	private Vector3 previous4PosKeThuMucTieu;

	private void LockBaoVeDongDoi(GameObject go)
	{
		MessagePopup.Create(Localization.instance.Get("TinhNangMoTrongPBMoi"));
	}

	private void Awake()
	{
		avatar1.OnEventClick = OnAvatarClick;
		avatar2.OnEventClick = OnAvatarClick;
		avatar3.OnEventClick = OnAvatarClick;
		avatar4.OnEventClick = OnAvatarClick;
		avatar5.OnEventClick = OnAvatarClick;
		avatar6.OnEventClick = OnAvatarClick;
		avatar7.OnEventClick = OnAvatarClick;
		avatar8.OnEventClick = OnAvatarClick;
		optionBiDong.gameObject.SetActive(true);
		optionBiDong.onStateChange = OnBiDongStateChange;
		optionChuDong.onStateChange = OnChuDongStateChange;
		btnDiDongDisabled.gameObject.SetActive(false);
		optionBaoVe.enabled = false;
		if (optionBaoVe.enabled)
		{
			optionBaoVe.onStateChange = OnBaoVeDongDoiStateChange;
		}
		UIEventListener.Get(optionBaoVe.gameObject).onClick = LockBaoVeDongDoi;
		optionChuDongGanNhat.onStateChange = OnActivateTrangThaiChuDongGanNhat;
		optionChuDongMauNhieuNhat.onStateChange = OnActivateTrangThaiChuDongMauNhieuNhat;
		optionChuDongMauItNhat.onStateChange = OnActivateTrangThaiChuDongMauItNhat;
		optionChuDongThanPhapCaoNhat.onStateChange = OnActivateTrangThaiChuDongThanPhapCaoNhat;
		optionChuDongThanPhapThapNhat.onStateChange = OnActivateTrangThaiChuDongThanPhapThapNhat;
		optionChuDongCongToNhat.onStateChange = OnActivateTrangThaiChuDongCongCaoNhat;
		optionChuDongCongNhoNhat.onStateChange = OnActivateTrangThaiChuDongCongThapNhat;
		optionChuDongKhiLonNhat.onStateChange = OnActivateTrangThaiChuDongKhiCaoNhat;
		optionChuDongKhiNhoNhat.onStateChange = OnActivateTrangThaiChuDongKhiThapNhat;
		optionChuDongSlot.onStateChange = OnActivateTrangThaiChuDongSlot;
		optionChuDongSlot1.GetComponent<EGGUIDragObject>().onPressEvent += OnChuDongSlot1Press;
		optionChuDongSlot1.GetComponent<EGGUIDragObject>().onDragEvent += OnChuDongSlot1Drag;
		optionChuDongSlot2.GetComponent<EGGUIDragObject>().onPressEvent += OnChuDongSlot2Press;
		optionChuDongSlot2.GetComponent<EGGUIDragObject>().onDragEvent += OnChuDongSlot2Drag;
		optionChuDongSlot3.GetComponent<EGGUIDragObject>().onPressEvent += OnChuDongSlot3Press;
		optionChuDongSlot3.GetComponent<EGGUIDragObject>().onDragEvent += OnChuDongSlot3Drag;
		optionChuDongSlot4.GetComponent<EGGUIDragObject>().onPressEvent += OnChuDongSlot4Press;
		optionChuDongSlot4.GetComponent<EGGUIDragObject>().onDragEvent += OnChuDongSlot4Drag;
		optionBiDongGanNhat.onStateChange = OnActivateTrangThaiBiDongGanNhat;
		optionBiDongMauNhieuNhat.onStateChange = OnActivateTrangThaiBiDongMauNhieuNhat;
		optionBiDongMauItNhat.onStateChange = OnActivateTrangThaiBiDongMauItNhat;
		optionBiDongThanPhapCaoNhat.onStateChange = OnActivateTrangThaiBiDongThanPhapCaoNhat;
		optionBiDongThanPhapThapNhat.onStateChange = OnActivateTrangThaiBiDongThanPhapThapNhat;
		optionBiDongCongToNhat.onStateChange = OnActivateTrangThaiBiDongCongCaoNhat;
		optionBiDongCongNhoNhat.onStateChange = OnActivateTrangThaiBiDongCongThapNhat;
		optionBiDongKhiLonNhat.onStateChange = OnActivateTrangThaiBiDongKhiCaoNhat;
		optionBiDongKhiNhoNhat.onStateChange = OnActivateTrangThaiBiDongKhiThapNhat;
		optionBiDongSlot.onStateChange = OnActivateTrangThaiBiDongSlot;
		optionBiDongSlot1.GetComponent<EGGUIDragObject>().onPressEvent += OnBiDongSlot1Press;
		optionBiDongSlot1.GetComponent<EGGUIDragObject>().onDragEvent += OnBiDongSlot1Drag;
		optionBiDongSlot2.GetComponent<EGGUIDragObject>().onPressEvent += OnBiDongSlot2Press;
		optionBiDongSlot2.GetComponent<EGGUIDragObject>().onDragEvent += OnBiDongSlot2Drag;
		optionBiDongSlot3.GetComponent<EGGUIDragObject>().onPressEvent += OnBiDongSlot3Press;
		optionBiDongSlot3.GetComponent<EGGUIDragObject>().onDragEvent += OnBiDongSlot3Drag;
		optionBiDongSlot4.GetComponent<EGGUIDragObject>().onPressEvent += OnBiDongSlot4Press;
		optionBiDongSlot4.GetComponent<EGGUIDragObject>().onDragEvent += OnBiDongSlot4Drag;
		for (int i = 0; i < targets.Length; i++)
		{
			targets[i].OnStateChange = OnBaoVeDongDoiSelected;
		}
		MucTieuChk.onStateChange = OnMucTieuStateChange;
		DungKhiChk.onStateChange = OnDungKhiStateChange;
		NgungKhiChk.onStateChange = OnNgungKhiStateChange;
		for (int j = 0; j < chieuThucAvatars.Length; j++)
		{
			chieuThucAvatars[j].OnEventClick = OnChieuThucClick;
		}
		optionKeThuMucTieuDangTanCong.onStateChange = OnActivateChieuThucKeThuMucTieuDangTanCong;
		optionKeThuMucTieuMauNhieuNhat.onStateChange = OnActivateChieuThucKeThuMucTieuMauNhieuNhat;
		optionKeThuMucTieuMauItNhat.onStateChange = OnActivateChieuThucKeThuMucTieuMauItNhat;
		optionKeThuMucTieuThanPhapNhanhNhat.onStateChange = OnActivateChieuThucKeThuMucTieuThanPhapCaoNhat;
		optionKeThuMucTieuThanPhapChamNhat.onStateChange = OnActivateChieuThucKeThuMucTieuThanPhapThapNhat;
		optionKeThuMucTieuCongToNhat.onStateChange = OnActivateChieuThucKeThuMucTieuCongCaoNhat;
		optionKeThuMucTieuCongNhoNhat.onStateChange = OnActivateChieuThucKeThuMucTieuCongThapNhat;
		optionKeThuMucTieuKhiLonNhat.onStateChange = OnActivateChieuThucKeThuMucTieuKhiCaoNhat;
		optionKeThuMucTieuKhiNhoNhat.onStateChange = OnActivateChieuThucKeThuMucTieuKhiThapNhat;
		optionKeThuMucTieuSlot.onStateChange = OnActivateChieuThucKeThuMucTieuSlot;
		optionKeThuMucTieuSlot1.GetComponent<EGGUIDragObject>().onPressEvent += OnKeThuMucTieuSlot1Press;
		optionKeThuMucTieuSlot1.GetComponent<EGGUIDragObject>().onDragEvent += OnKeThuMucTieuSlot1Drag;
		optionKeThuMucTieuSlot2.GetComponent<EGGUIDragObject>().onPressEvent += OnKeThuMucTieuSlot2Press;
		optionKeThuMucTieuSlot2.GetComponent<EGGUIDragObject>().onDragEvent += OnKeThuMucTieuSlot2Drag;
		optionKeThuMucTieuSlot3.GetComponent<EGGUIDragObject>().onPressEvent += OnKeThuMucTieuSlot3Press;
		optionKeThuMucTieuSlot3.GetComponent<EGGUIDragObject>().onDragEvent += OnKeThuMucTieuSlot3Drag;
		optionKeThuMucTieuSlot4.GetComponent<EGGUIDragObject>().onPressEvent += OnKeThuMucTieuSlot4Press;
		optionKeThuMucTieuSlot4.GetComponent<EGGUIDragObject>().onDragEvent += OnKeThuMucTieuSlot4Drag;
		optionKeThuDungKhiNgayKhiCoThe.onStateChange = OnActivateChieuThucKeThuDungKhiNgayKhiCoThe;
		optionKeThuDungKhiMauMucTieuDuoi.onStateChange = OnActivateChieuThucKeThuDungKhiMauMucTieuDuoi;
		optionKeThuDungKhiMauMucTieuTren.onStateChange = OnActivateChieuThucKeThuDungKhiMauMucTieuTren;
		optionKeThuDungKhiMauBanThanDuoi.onStateChange = OnActivateChieuThucKeThuDungKhiMauBanThanDuoi;
		sliderKeThuDungKhiMauMucTieuDuoi.onValueChange = OnChieuThucKeThuMauMucTieuDuoiValueChange;
		sliderKeThuDungKhiMauMucTieuTren.onValueChange = OnChieuThucKeThuMauMucTieuTrenValueChange;
		sliderKeThuDungKhiMauBanThanDuoi.onValueChange = OnChieuThucKeThuMauBanThanDuoiValueChange;
		UIEventListener.Get(btnKeThuDungKhiMauMucTieuDuoi).onClick = OnConfirmChieuThucKeThuMauMucTieuDuoiValue;
		UIEventListener.Get(btnKeThuDungKhiMauBanThanDuoi).onClick = OnConfirmChieuThucKeThuMauBanThanDuoiValue;
		UIEventListener.Get(btnKeThuDungKhiMauMucTieuTren).onClick = OnConfirmChieuThucKeThuMauMucTieuTrenValue;
		optionKeThuNgungKhiMauBanThanDuoi.onStateChange = OnActivateChieuThucKeThuNgungKhiMauDuoi;
		optionKeThuNgungKhiNoiLucDuoi.onStateChange = OnActivateChieuThucKeThuNgungKhiNoiLucDuoi;
		sliderKeThuNgungKhiMauBanThanDuoi.onValueChange = OnChieuThucKeThuNgungKhiMauDuoiValueChange;
		sliderKeThuNgungKhiNoiLucDuoi.onValueChange = OnChieuThucKeThuNgungKhiNoiLucDuoiValueChange;
		UIEventListener.Get(btnKeThuNgungKhiMauBanThanDuoi).onClick = OnConfirmChieuThucKeThuNgungKhiMauDuoiValue;
		UIEventListener.Get(btnKeThuNgungKhiNoiLucDuoi).onClick = OnConfirmChieuThucKeThuNgungKhiNoiLucDuoiValue;
		dongDoiMucTieu1Avatar.avatar.OnEventClick = OnDongDoiMucTieuClick;
		dongDoiMucTieu2Avatar.avatar.OnEventClick = OnDongDoiMucTieuClick;
		dongDoiMucTieu3Avatar.avatar.OnEventClick = OnDongDoiMucTieuClick;
		dongDoiMucTieu4Avatar.avatar.OnEventClick = OnDongDoiMucTieuClick;
		optionDongDoiDungKhiNgayKhiCoThe.onStateChange = OnActivateChieuThucDongDoiDungKhiNgayKhiCoThe;
		optionDongDoiDungKhiMauMucTieuDuoi.onStateChange = OnActivateChieuThucDongDoiDungKhiMauMucTieuDuoi;
		optionDongDoiDungKhiMauMucTieuTren.onStateChange = OnActivateChieuThucDongDoiDungKhiMauMucTieuTren;
		optionDongDoiDungKhiMauBanThanDuoi.onStateChange = OnActivateChieuThucDongDoiDungKhiMauBanThanDuoi;
		sliderDongDoiDungKhiMauMucTieuDuoi.onValueChange = OnChieuThucDongDoiMauMucTieuDuoiValueChange;
		sliderDongDoiDungKhiMauMucTieuTren.onValueChange = OnChieuThucDongDoiMauMucTieuTrenValueChange;
		sliderDongDoiDungKhiMauBanThanDuoi.onValueChange = OnChieuThucDongDoiMauBanThanDuoiValueChange;
		UIEventListener.Get(btnDongDoiDungKhiMauMucTieuDuoi).onClick = OnConfirmChieuThucDongDoiMauMucTieuDuoiValue;
		UIEventListener.Get(btnDongDoiDungKhiMauMucTieuTren).onClick = OnConfirmChieuThucDongDoiMauMucTieuTrenValue;
		UIEventListener.Get(btnDongDoiDungKhiMauBanThanDuoi).onClick = OnConfirmChieuThucDongDoiMauBanThanDuoiValue;
		optionDongDoiDungKhiKhiBiTrangThaiXau.onStateChange = OnActivateChieuThucDongDoiDungKhiKhiBiTrangThaiXau;
		optionDongDoiNgungKhiMauBanThanDuoi.onStateChange = OnActivateChieuThucDongDoiNgungKhiMauDuoi;
		optionDongDoiNgungKhiNoiLucDuoi.onStateChange = OnActivateChieuThucDongDoiNgungKhiNoiLucDuoi;
		sliderDongDoiNgungKhiMauBanThanDuoi.onValueChange = OnChieuThucDongDoiNgungKhiMauDuoiValueChange;
		sliderDongDoiNgungKhiNoiLucDuoi.onValueChange = OnChieuThucDongDoiNgungKhiNoiLucDuoiValueChange;
		UIEventListener.Get(btnDongDoiNgungKhiMauBanThanDuoi).onClick = OnConfirmChieuThucDongDoiNgungKhiMauDuoiValue;
		UIEventListener.Get(btnDongDoiNgungKhiNoiLucDuoi).onClick = OnConfirmChieuThucDongDoiNgungKhiNoiLucDuoiValue;
	}

	private void OnActivateCaoCapChuDong(bool isActive)
	{
		optionChuDongAdvanceGrp.SetActive(isActive);
		optionChuDongBasicGrp.SetActive(!isActive);
		optionChuDongCaoCap.GetComponentInChildren<UILabel>().text = Localization.instance.Get((!isActive) ? "ChienThuatCaoCap" : "ChienThuatCoBan");
	}

	private void OnActivateCaoCapBiDong(bool isActive)
	{
		optionBiDongAdvanceGrp.SetActive(isActive);
		optionBiDongBasicGrp.SetActive(!isActive);
		UILabel componentInChildren = optionBiDongCaoCap.GetComponentInChildren<UILabel>();
		string text = Localization.instance.Get((!isActive) ? "ChienThuatCaoCap" : "ChienThuatCoBan");
		componentInChildren.text = text;
	}

	private void OnActivateCaoCapKeThuMucTieu(bool isActive)
	{
		optionKeThuMucTieuAdvanceGrp.SetActive(isActive);
		optionKeThuMucTieuBasicGrp.SetActive(!isActive);
		optionKeThuMucTieuCaoCap.GetComponentInChildren<UILabel>().text = Localization.instance.Get((!isActive) ? "ChienThuatCaoCap" : "ChienThuatCoBan");
	}

	private void OnChieuThucClick(OtherAvatar chieuThuc)
	{
		if (SelectedSlotNhanVat < 1 || heroData == null)
		{
			MessagePopup.Create(Localization.instance.Get("ChienThuatChonNhanVat"));
		}
		else
		{
			if (chieuThuc.IsSelected || chieuThuc.IsEmpty())
			{
				return;
			}
			for (int i = 0; i < chieuThucAvatars.Length; i++)
			{
				if (chieuThuc == chieuThucAvatars[i])
				{
					chieuThucAvatars[i].IsSelected = true;
					selectedChieuThuc = i;
				}
				else
				{
					chieuThucAvatars[i].IsSelected = false;
				}
			}
			string spriteName = chieuThuc.avatar.spriteName;
			CfgVoCong value = null;
			if (!ConfigManager.instance.m_dicVCs.TryGetValue(spriteName, out value))
			{
				return;
			}
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(heroData.HID, value.Type);
			if (vCThietLapFromHero == null)
			{
				vcThietLap = new UserInfo.VCThietLapData(heroData.HID, value.Type);
			}
			else
			{
				vcThietLap = new UserInfo.VCThietLapData(vCThietLapFromHero);
			}
			if (value.m_Setting == VCSetting.Enemy)
			{
				if (MucTieuChk.isChecked)
				{
					KeThuMucTieuGrp.SetActive(true);
					DongDoiMucTieuGrp.SetActive(false);
					ShowChieuThucKeThuMucTieu(value.Type);
				}
				else if (DungKhiChk.isChecked)
				{
					KeThuDungKhiGrp.SetActive(true);
					DongDoiDungKhiGrp.SetActive(false);
					ShowChieuThucKeThuDungKhi(value.Type);
				}
				else if (NgungKhiChk.isChecked)
				{
					KeThuNgungKhiGrp.SetActive(true);
					DongDoiNgungKhiGrp.SetActive(false);
					ShowChieuThucKeThuNgungKhi(value.Type);
				}
			}
			else if (MucTieuChk.isChecked)
			{
				KeThuMucTieuGrp.SetActive(false);
				DongDoiMucTieuGrp.SetActive(true);
				ShowChieuThucDongDoiMucTieu(value.Type);
			}
			else if (DungKhiChk.isChecked)
			{
				KeThuDungKhiGrp.SetActive(false);
				DongDoiDungKhiGrp.SetActive(true);
				ShowChieuThucDongDoiDungKhi(value.Type);
			}
			else if (NgungKhiChk.isChecked)
			{
				KeThuNgungKhiGrp.SetActive(false);
				DongDoiNgungKhiGrp.SetActive(true);
				ShowChieuThucDongDoiNgungKhi(value.Type);
			}
		}
	}

	private void OnMucTieuStateChange(bool isActive)
	{
		if (isActive && SelectedSlotNhanVat < 1)
		{
			MessagePopup.Create(Localization.instance.Get("ChienThuatChonNhanVat"));
			MucTieuChk.isChecked = false;
		}
		else if (isActive && vcThietLap != null)
		{
			CfgVoCong voCongCfgByType = ConfigManager.instance.GetVoCongCfgByType(vcThietLap.Type);
			if (voCongCfgByType.m_Setting == VCSetting.Enemy)
			{
				KeThuMucTieuGrp.SetActive(true);
				ShowChieuThucKeThuMucTieu(voCongCfgByType.Type);
			}
			else
			{
				DongDoiMucTieuGrp.SetActive(true);
				ShowChieuThucDongDoiMucTieu(voCongCfgByType.Type);
			}
		}
		else
		{
			KeThuMucTieuGrp.SetActive(false);
			DongDoiMucTieuGrp.SetActive(false);
		}
	}

	private void OnDungKhiStateChange(bool isActive)
	{
		if (isActive && vcThietLap != null)
		{
			CfgVoCong voCongCfgByType = ConfigManager.instance.GetVoCongCfgByType(vcThietLap.Type);
			if (voCongCfgByType.m_Setting == VCSetting.Enemy)
			{
				KeThuDungKhiGrp.SetActive(true);
				ShowChieuThucKeThuDungKhi(voCongCfgByType.Type);
			}
			else
			{
				DongDoiDungKhiGrp.SetActive(true);
				ShowChieuThucDongDoiDungKhi(voCongCfgByType.Type);
			}
		}
		else
		{
			KeThuDungKhiGrp.SetActive(false);
			DongDoiDungKhiGrp.SetActive(false);
		}
	}

	private void OnNgungKhiStateChange(bool isActive)
	{
		if (isActive && vcThietLap != null)
		{
			CfgVoCong voCongCfgByType = ConfigManager.instance.GetVoCongCfgByType(vcThietLap.Type);
			if (voCongCfgByType.m_Setting == VCSetting.Enemy)
			{
				KeThuNgungKhiGrp.SetActive(true);
				ShowChieuThucKeThuNgungKhi(voCongCfgByType.Type);
			}
			else
			{
				DongDoiNgungKhiGrp.SetActive(true);
				ShowChieuThucDongDoiNgungKhi(voCongCfgByType.Type);
			}
		}
		else
		{
			KeThuNgungKhiGrp.SetActive(false);
			DongDoiNgungKhiGrp.SetActive(false);
		}
	}

	private void OnActivateTrangThaiChuDongGanNhat(bool isActive)
	{
		if (isActive && heroData != null)
		{
			if (heroData.TrangThai == null)
			{
				heroData.TrangThai = new UserInfo.HeroData.AI();
			}
			heroData.TrangThai.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_GAN_NHAT;
			UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
			if (heroFromDoiHinh.TrangThai.ChienThuat != heroData.TrangThai.ChienThuat)
			{
				GameManager.instance.m_GameClient.SetHeroData(heroData);
			}
		}
	}

	private void OnActivateTrangThaiChuDongMauNhieuNhat(bool isActive)
	{
		if (isActive && heroData != null)
		{
			if (heroData.TrangThai == null)
			{
				heroData.TrangThai = new UserInfo.HeroData.AI();
			}
			heroData.TrangThai.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_SINH_LUC_MAX;
			UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
			if (heroFromDoiHinh.TrangThai.ChienThuat != heroData.TrangThai.ChienThuat)
			{
				GameManager.instance.m_GameClient.SetHeroData(heroData);
			}
		}
	}

	private void OnActivateTrangThaiChuDongMauItNhat(bool isActive)
	{
		if (isActive && heroData != null)
		{
			if (heroData.TrangThai == null)
			{
				heroData.TrangThai = new UserInfo.HeroData.AI();
			}
			heroData.TrangThai.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_SINH_LUC_MIN;
			UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
			if (heroFromDoiHinh.TrangThai.ChienThuat != heroData.TrangThai.ChienThuat)
			{
				GameManager.instance.m_GameClient.SetHeroData(heroData);
			}
		}
	}

	private void OnActivateTrangThaiChuDongThanPhapCaoNhat(bool isActive)
	{
		if (isActive && heroData != null)
		{
			if (heroData.TrangThai == null)
			{
				heroData.TrangThai = new UserInfo.HeroData.AI();
			}
			heroData.TrangThai.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_THAN_PHAP_MAX;
			UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
			if (heroFromDoiHinh.TrangThai.ChienThuat != heroData.TrangThai.ChienThuat)
			{
				GameManager.instance.m_GameClient.SetHeroData(heroData);
			}
		}
	}

	private void OnActivateTrangThaiChuDongThanPhapThapNhat(bool isActive)
	{
		if (isActive && heroData != null)
		{
			if (heroData.TrangThai == null)
			{
				heroData.TrangThai = new UserInfo.HeroData.AI();
			}
			heroData.TrangThai.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_THAN_PHAP_MIN;
			UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
			if (heroFromDoiHinh.TrangThai.ChienThuat != heroData.TrangThai.ChienThuat)
			{
				GameManager.instance.m_GameClient.SetHeroData(heroData);
			}
		}
	}

	private void OnActivateTrangThaiChuDongCongCaoNhat(bool isActive)
	{
		if (isActive && heroData != null)
		{
			if (heroData.TrangThai == null)
			{
				heroData.TrangThai = new UserInfo.HeroData.AI();
			}
			heroData.TrangThai.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_CONG_MAX;
			UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
			if (heroFromDoiHinh.TrangThai.ChienThuat != heroData.TrangThai.ChienThuat)
			{
				GameManager.instance.m_GameClient.SetHeroData(heroData);
			}
		}
	}

	private void OnActivateTrangThaiChuDongCongThapNhat(bool isActive)
	{
		if (isActive && heroData != null)
		{
			if (heroData.TrangThai == null)
			{
				heroData.TrangThai = new UserInfo.HeroData.AI();
			}
			heroData.TrangThai.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_CONG_MIN;
			UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
			if (heroFromDoiHinh.TrangThai.ChienThuat != heroData.TrangThai.ChienThuat)
			{
				GameManager.instance.m_GameClient.SetHeroData(heroData);
			}
		}
	}

	private void OnActivateTrangThaiChuDongKhiCaoNhat(bool isActive)
	{
		if (isActive && heroData != null)
		{
			if (heroData.TrangThai == null)
			{
				heroData.TrangThai = new UserInfo.HeroData.AI();
			}
			heroData.TrangThai.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_NOI_MAX;
			UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
			if (heroFromDoiHinh.TrangThai.ChienThuat != heroData.TrangThai.ChienThuat)
			{
				GameManager.instance.m_GameClient.SetHeroData(heroData);
			}
		}
	}

	private void OnActivateTrangThaiChuDongKhiThapNhat(bool isActive)
	{
		if (isActive && heroData != null)
		{
			if (heroData.TrangThai == null)
			{
				heroData.TrangThai = new UserInfo.HeroData.AI();
			}
			heroData.TrangThai.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_NOI_MIN;
			UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
			if (heroFromDoiHinh.TrangThai.ChienThuat != heroData.TrangThai.ChienThuat)
			{
				GameManager.instance.m_GameClient.SetHeroData(heroData);
			}
		}
	}

	private void OnActivateTrangThaiChuDongSlot(bool isActive)
	{
		if (isActive && heroData != null)
		{
			if (heroData.TrangThai == null)
			{
				heroData.TrangThai = new UserInfo.HeroData.AI();
			}
			heroData.TrangThai.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_SLOT;
			UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
			ShowTrangThaiChuDongSlots();
			if (heroFromDoiHinh.TrangThai.ChienThuat != heroData.TrangThai.ChienThuat || JsonMapper.ToJson(heroData.TrangThai.TanCongSlotList) != JsonMapper.ToJson(heroFromDoiHinh.TrangThai.TanCongSlotList))
			{
				GameManager.instance.m_GameClient.SetHeroData(heroData);
			}
		}
		else if (!isActive)
		{
			optionChuDongSlots.SetActive(false);
		}
	}

	private void ShowTrangThaiChuDongSlots()
	{
		if (heroData == null || heroData.TrangThai == null || heroData.TrangThai.TanCongSlotList == null)
		{
			return;
		}
		optionChuDongSlots.SetActive(true);
		for (int i = 0; i < heroData.TrangThai.TanCongSlotList.Count; i++)
		{
			if (heroData.TrangThai.TanCongSlotList[i] == 0)
			{
				optionChuDongSlot1.transform.localPosition = posOrder[i];
			}
			if (heroData.TrangThai.TanCongSlotList[i] == 1)
			{
				optionChuDongSlot2.transform.localPosition = posOrder[i];
			}
			if (heroData.TrangThai.TanCongSlotList[i] == 2)
			{
				optionChuDongSlot3.transform.localPosition = posOrder[i];
			}
			if (heroData.TrangThai.TanCongSlotList[i] == 3)
			{
				optionChuDongSlot4.transform.localPosition = posOrder[i];
			}
		}
	}

	private void OnChuDongSlot1Press(bool isPress)
	{
		if (isPress)
		{
			previous1PosChuDong = optionChuDongSlot1.transform.localPosition;
			optionChuDongSlot1.transform.localScale = new Vector3(0.86f, 0.86f, 1f);
			optionChuDongSlot1.transform.localPosition = previous1PosChuDong + new Vector3(0f, 0f, -1f);
			return;
		}
		optionChuDongSlot1.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
		optionChuDongSlot1.transform.localPosition = previous1PosChuDong;
		if (heroData.TrangThai == null)
		{
			heroData.TrangThai = new UserInfo.HeroData.AI();
		}
		heroData.TrangThai.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_SLOT;
		UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
		if (heroFromDoiHinh.TrangThai.ChienThuat != heroData.TrangThai.ChienThuat || JsonMapper.ToJson(heroData.TrangThai.TanCongSlotList) != JsonMapper.ToJson(heroFromDoiHinh.TrangThai.TanCongSlotList))
		{
			GameManager.instance.m_GameClient.SetHeroData(heroData);
		}
	}

	private void OnChuDongSlot1Drag(Vector2 delta)
	{
		Vector3 vector = optionChuDongSlot1.transform.localPosition - optionChuDongSlot2.transform.localPosition;
		Vector3 vector2 = optionChuDongSlot1.transform.localPosition - optionChuDongSlot3.transform.localPosition;
		Vector3 vector3 = optionChuDongSlot1.transform.localPosition - optionChuDongSlot4.transform.localPosition;
		if (Mathf.Abs(vector.x) < 50f)
		{
			Vector3 localPosition = previous1PosChuDong;
			previous1PosChuDong = optionChuDongSlot2.transform.localPosition;
			optionChuDongSlot2.transform.localPosition = localPosition;
			if (heroData != null && heroData.TrangThai != null && heroData.TrangThai.TanCongSlotList != null)
			{
				for (int i = 0; i < heroData.TrangThai.TanCongSlotList.Count; i++)
				{
					if (heroData.TrangThai.TanCongSlotList[i] == 1)
					{
						heroData.TrangThai.TanCongSlotList[i] = 0;
					}
					else if (heroData.TrangThai.TanCongSlotList[i] == 0)
					{
						heroData.TrangThai.TanCongSlotList[i] = 1;
					}
				}
			}
		}
		if (Mathf.Abs(vector2.x) < 50f)
		{
			Vector3 localPosition2 = previous1PosChuDong;
			previous1PosChuDong = optionChuDongSlot3.transform.localPosition;
			optionChuDongSlot3.transform.localPosition = localPosition2;
			if (heroData != null && heroData.TrangThai != null && heroData.TrangThai.TanCongSlotList != null)
			{
				for (int j = 0; j < heroData.TrangThai.TanCongSlotList.Count; j++)
				{
					if (heroData.TrangThai.TanCongSlotList[j] == 2)
					{
						heroData.TrangThai.TanCongSlotList[j] = 0;
					}
					else if (heroData.TrangThai.TanCongSlotList[j] == 0)
					{
						heroData.TrangThai.TanCongSlotList[j] = 2;
					}
				}
			}
		}
		if (!(Mathf.Abs(vector3.x) < 50f))
		{
			return;
		}
		Vector3 localPosition3 = previous1PosChuDong;
		previous1PosChuDong = optionChuDongSlot4.transform.localPosition;
		optionChuDongSlot4.transform.localPosition = localPosition3;
		if (heroData == null || heroData.TrangThai == null || heroData.TrangThai.TanCongSlotList == null)
		{
			return;
		}
		for (int k = 0; k < heroData.TrangThai.TanCongSlotList.Count; k++)
		{
			if (heroData.TrangThai.TanCongSlotList[k] == 3)
			{
				heroData.TrangThai.TanCongSlotList[k] = 0;
			}
			else if (heroData.TrangThai.TanCongSlotList[k] == 0)
			{
				heroData.TrangThai.TanCongSlotList[k] = 3;
			}
		}
	}

	private void OnChuDongSlot2Press(bool isPress)
	{
		if (isPress)
		{
			previous2PosChuDong = optionChuDongSlot2.transform.localPosition;
			optionChuDongSlot2.transform.localScale = new Vector3(0.86f, 0.86f, 1f);
			optionChuDongSlot2.transform.localPosition = previous2PosChuDong + new Vector3(0f, 0f, -1f);
			return;
		}
		optionChuDongSlot2.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
		optionChuDongSlot2.transform.localPosition = previous2PosChuDong;
		if (heroData.TrangThai == null)
		{
			heroData.TrangThai = new UserInfo.HeroData.AI();
		}
		heroData.TrangThai.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_SLOT;
		UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
		if (heroFromDoiHinh.TrangThai.ChienThuat != heroData.TrangThai.ChienThuat || JsonMapper.ToJson(heroData.TrangThai.TanCongSlotList) != JsonMapper.ToJson(heroFromDoiHinh.TrangThai.TanCongSlotList))
		{
			GameManager.instance.m_GameClient.SetHeroData(heroData);
		}
	}

	private void OnChuDongSlot2Drag(Vector2 delta)
	{
		Vector3 vector = optionChuDongSlot2.transform.localPosition - optionChuDongSlot1.transform.localPosition;
		Vector3 vector2 = optionChuDongSlot2.transform.localPosition - optionChuDongSlot3.transform.localPosition;
		Vector3 vector3 = optionChuDongSlot2.transform.localPosition - optionChuDongSlot4.transform.localPosition;
		if (Mathf.Abs(vector.x) < 50f)
		{
			Vector3 localPosition = previous2PosChuDong;
			previous2PosChuDong = optionChuDongSlot1.transform.localPosition;
			optionChuDongSlot1.transform.localPosition = localPosition;
			if (heroData != null && heroData.TrangThai != null && heroData.TrangThai.TanCongSlotList != null)
			{
				for (int i = 0; i < heroData.TrangThai.TanCongSlotList.Count; i++)
				{
					if (heroData.TrangThai.TanCongSlotList[i] == 1)
					{
						heroData.TrangThai.TanCongSlotList[i] = 0;
					}
					else if (heroData.TrangThai.TanCongSlotList[i] == 0)
					{
						heroData.TrangThai.TanCongSlotList[i] = 1;
					}
				}
			}
		}
		if (Mathf.Abs(vector2.x) < 50f)
		{
			Vector3 localPosition2 = previous2PosChuDong;
			previous2PosChuDong = optionChuDongSlot3.transform.localPosition;
			optionChuDongSlot3.transform.localPosition = localPosition2;
			if (heroData != null && heroData.TrangThai != null && heroData.TrangThai.TanCongSlotList != null)
			{
				for (int j = 0; j < heroData.TrangThai.TanCongSlotList.Count; j++)
				{
					if (heroData.TrangThai.TanCongSlotList[j] == 1)
					{
						heroData.TrangThai.TanCongSlotList[j] = 2;
					}
					else if (heroData.TrangThai.TanCongSlotList[j] == 2)
					{
						heroData.TrangThai.TanCongSlotList[j] = 1;
					}
				}
			}
		}
		if (!(Mathf.Abs(vector3.x) < 50f))
		{
			return;
		}
		Vector3 localPosition3 = previous2PosChuDong;
		previous2PosChuDong = optionChuDongSlot4.transform.localPosition;
		optionChuDongSlot4.transform.localPosition = localPosition3;
		if (heroData == null || heroData.TrangThai == null || heroData.TrangThai.TanCongSlotList == null)
		{
			return;
		}
		for (int k = 0; k < heroData.TrangThai.TanCongSlotList.Count; k++)
		{
			if (heroData.TrangThai.TanCongSlotList[k] == 1)
			{
				heroData.TrangThai.TanCongSlotList[k] = 3;
			}
			else if (heroData.TrangThai.TanCongSlotList[k] == 3)
			{
				heroData.TrangThai.TanCongSlotList[k] = 1;
			}
		}
	}

	private void OnChuDongSlot3Press(bool isPress)
	{
		if (isPress)
		{
			previous3PosChuDong = optionChuDongSlot3.transform.localPosition;
			optionChuDongSlot3.transform.localScale = new Vector3(0.86f, 0.86f, 1f);
			optionChuDongSlot3.transform.localPosition = previous3PosChuDong + new Vector3(0f, 0f, -1f);
			return;
		}
		optionChuDongSlot3.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
		optionChuDongSlot3.transform.localPosition = previous3PosChuDong;
		if (heroData.TrangThai == null)
		{
			heroData.TrangThai = new UserInfo.HeroData.AI();
		}
		heroData.TrangThai.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_SLOT;
		UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
		if (heroFromDoiHinh.TrangThai.ChienThuat != heroData.TrangThai.ChienThuat || JsonMapper.ToJson(heroData.TrangThai.TanCongSlotList) != JsonMapper.ToJson(heroFromDoiHinh.TrangThai.TanCongSlotList))
		{
			GameManager.instance.m_GameClient.SetHeroData(heroData);
		}
	}

	private void OnChuDongSlot3Drag(Vector2 delta)
	{
		Vector3 vector = optionChuDongSlot3.transform.localPosition - optionChuDongSlot2.transform.localPosition;
		Vector3 vector2 = optionChuDongSlot3.transform.localPosition - optionChuDongSlot1.transform.localPosition;
		Vector3 vector3 = optionChuDongSlot3.transform.localPosition - optionChuDongSlot4.transform.localPosition;
		if (Mathf.Abs(vector.x) < 50f)
		{
			Vector3 localPosition = previous3PosChuDong;
			previous3PosChuDong = optionChuDongSlot2.transform.localPosition;
			optionChuDongSlot2.transform.localPosition = localPosition;
			if (heroData != null && heroData.TrangThai != null && heroData.TrangThai.TanCongSlotList != null)
			{
				for (int i = 0; i < heroData.TrangThai.TanCongSlotList.Count; i++)
				{
					if (heroData.TrangThai.TanCongSlotList[i] == 1)
					{
						heroData.TrangThai.TanCongSlotList[i] = 2;
					}
					else if (heroData.TrangThai.TanCongSlotList[i] == 2)
					{
						heroData.TrangThai.TanCongSlotList[i] = 1;
					}
				}
			}
		}
		if (Mathf.Abs(vector2.x) < 50f)
		{
			Vector3 localPosition2 = previous3PosChuDong;
			previous3PosChuDong = optionChuDongSlot1.transform.localPosition;
			optionChuDongSlot1.transform.localPosition = localPosition2;
			if (heroData != null && heroData.TrangThai != null && heroData.TrangThai.TanCongSlotList != null)
			{
				for (int j = 0; j < heroData.TrangThai.TanCongSlotList.Count; j++)
				{
					if (heroData.TrangThai.TanCongSlotList[j] == 2)
					{
						heroData.TrangThai.TanCongSlotList[j] = 0;
					}
					else if (heroData.TrangThai.TanCongSlotList[j] == 0)
					{
						heroData.TrangThai.TanCongSlotList[j] = 2;
					}
				}
			}
		}
		if (!(Mathf.Abs(vector3.x) < 50f))
		{
			return;
		}
		Vector3 localPosition3 = previous3PosChuDong;
		previous3PosChuDong = optionChuDongSlot4.transform.localPosition;
		optionChuDongSlot4.transform.localPosition = localPosition3;
		if (heroData == null || heroData.TrangThai == null || heroData.TrangThai.TanCongSlotList == null)
		{
			return;
		}
		for (int k = 0; k < heroData.TrangThai.TanCongSlotList.Count; k++)
		{
			if (heroData.TrangThai.TanCongSlotList[k] == 2)
			{
				heroData.TrangThai.TanCongSlotList[k] = 3;
			}
			else if (heroData.TrangThai.TanCongSlotList[k] == 3)
			{
				heroData.TrangThai.TanCongSlotList[k] = 2;
			}
		}
	}

	private void OnChuDongSlot4Press(bool isPress)
	{
		if (isPress)
		{
			previous4PosChuDong = optionChuDongSlot4.transform.localPosition;
			optionChuDongSlot4.transform.localScale = new Vector3(0.86f, 0.86f, 1f);
			optionChuDongSlot4.transform.localPosition = previous4PosChuDong + new Vector3(0f, 0f, -1f);
			return;
		}
		optionChuDongSlot4.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
		optionChuDongSlot4.transform.localPosition = previous4PosChuDong;
		if (heroData.TrangThai == null)
		{
			heroData.TrangThai = new UserInfo.HeroData.AI();
		}
		heroData.TrangThai.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_SLOT;
		UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
		if (heroFromDoiHinh.TrangThai.ChienThuat != heroData.TrangThai.ChienThuat || JsonMapper.ToJson(heroData.TrangThai.TanCongSlotList) != JsonMapper.ToJson(heroFromDoiHinh.TrangThai.TanCongSlotList))
		{
			GameManager.instance.m_GameClient.SetHeroData(heroData);
		}
	}

	private void OnChuDongSlot4Drag(Vector2 delta)
	{
		Vector3 vector = optionChuDongSlot4.transform.localPosition - optionChuDongSlot2.transform.localPosition;
		Vector3 vector2 = optionChuDongSlot4.transform.localPosition - optionChuDongSlot3.transform.localPosition;
		Vector3 vector3 = optionChuDongSlot4.transform.localPosition - optionChuDongSlot1.transform.localPosition;
		if (Mathf.Abs(vector.x) < 50f)
		{
			Vector3 localPosition = previous4PosChuDong;
			previous4PosChuDong = optionChuDongSlot2.transform.localPosition;
			optionChuDongSlot2.transform.localPosition = localPosition;
			if (heroData != null && heroData.TrangThai != null && heroData.TrangThai.TanCongSlotList != null)
			{
				for (int i = 0; i < heroData.TrangThai.TanCongSlotList.Count; i++)
				{
					if (heroData.TrangThai.TanCongSlotList[i] == 1)
					{
						heroData.TrangThai.TanCongSlotList[i] = 3;
					}
					else if (heroData.TrangThai.TanCongSlotList[i] == 3)
					{
						heroData.TrangThai.TanCongSlotList[i] = 1;
					}
				}
			}
		}
		if (Mathf.Abs(vector2.x) < 50f)
		{
			Vector3 localPosition2 = previous4PosChuDong;
			previous4PosChuDong = optionChuDongSlot3.transform.localPosition;
			optionChuDongSlot3.transform.localPosition = localPosition2;
			if (heroData != null && heroData.TrangThai != null && heroData.TrangThai.TanCongSlotList != null)
			{
				for (int j = 0; j < heroData.TrangThai.TanCongSlotList.Count; j++)
				{
					if (heroData.TrangThai.TanCongSlotList[j] == 3)
					{
						heroData.TrangThai.TanCongSlotList[j] = 2;
					}
					else if (heroData.TrangThai.TanCongSlotList[j] == 2)
					{
						heroData.TrangThai.TanCongSlotList[j] = 3;
					}
				}
			}
		}
		if (!(Mathf.Abs(vector3.x) < 50f))
		{
			return;
		}
		Vector3 localPosition3 = previous4PosChuDong;
		previous4PosChuDong = optionChuDongSlot1.transform.localPosition;
		optionChuDongSlot1.transform.localPosition = localPosition3;
		if (heroData == null || heroData.TrangThai == null || heroData.TrangThai.TanCongSlotList == null)
		{
			return;
		}
		for (int k = 0; k < heroData.TrangThai.TanCongSlotList.Count; k++)
		{
			if (heroData.TrangThai.TanCongSlotList[k] == 3)
			{
				heroData.TrangThai.TanCongSlotList[k] = 0;
			}
			else if (heroData.TrangThai.TanCongSlotList[k] == 0)
			{
				heroData.TrangThai.TanCongSlotList[k] = 3;
			}
		}
	}

	private void OnActivateTrangThaiBiDongGanNhat(bool isActive)
	{
		if (isActive && heroData != null)
		{
			if (heroData.TrangThai == null)
			{
				heroData.TrangThai = new UserInfo.HeroData.AI();
			}
			heroData.TrangThai.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_GAN_NHAT;
			UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
			if (heroFromDoiHinh.TrangThai.ChienThuat != heroData.TrangThai.ChienThuat)
			{
				GameManager.instance.m_GameClient.SetHeroData(heroData);
			}
		}
	}

	private void OnActivateTrangThaiBiDongMauNhieuNhat(bool isActive)
	{
		if (isActive && heroData != null)
		{
			if (heroData.TrangThai == null)
			{
				heroData.TrangThai = new UserInfo.HeroData.AI();
			}
			heroData.TrangThai.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_SINH_LUC_MAX;
			UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
			if (heroFromDoiHinh.TrangThai.ChienThuat != heroData.TrangThai.ChienThuat)
			{
				GameManager.instance.m_GameClient.SetHeroData(heroData);
			}
		}
	}

	private void OnActivateTrangThaiBiDongMauItNhat(bool isActive)
	{
		if (isActive && heroData != null)
		{
			if (heroData.TrangThai == null)
			{
				heroData.TrangThai = new UserInfo.HeroData.AI();
			}
			heroData.TrangThai.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_SINH_LUC_MIN;
			UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
			if (heroFromDoiHinh.TrangThai.ChienThuat != heroData.TrangThai.ChienThuat)
			{
				GameManager.instance.m_GameClient.SetHeroData(heroData);
			}
		}
	}

	private void OnActivateTrangThaiBiDongThanPhapCaoNhat(bool isActive)
	{
		if (isActive && heroData != null)
		{
			if (heroData.TrangThai == null)
			{
				heroData.TrangThai = new UserInfo.HeroData.AI();
			}
			heroData.TrangThai.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_THAN_PHAP_MAX;
			UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
			if (heroFromDoiHinh.TrangThai.ChienThuat != heroData.TrangThai.ChienThuat)
			{
				GameManager.instance.m_GameClient.SetHeroData(heroData);
			}
		}
	}

	private void OnActivateTrangThaiBiDongThanPhapThapNhat(bool isActive)
	{
		if (isActive && heroData != null)
		{
			if (heroData.TrangThai == null)
			{
				heroData.TrangThai = new UserInfo.HeroData.AI();
			}
			heroData.TrangThai.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_THAN_PHAP_MIN;
			UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
			if (heroFromDoiHinh.TrangThai.ChienThuat != heroData.TrangThai.ChienThuat)
			{
				GameManager.instance.m_GameClient.SetHeroData(heroData);
			}
		}
	}

	private void OnActivateTrangThaiBiDongCongCaoNhat(bool isActive)
	{
		if (isActive && heroData != null)
		{
			if (heroData.TrangThai == null)
			{
				heroData.TrangThai = new UserInfo.HeroData.AI();
			}
			heroData.TrangThai.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_CONG_MAX;
			UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
			if (heroFromDoiHinh.TrangThai.ChienThuat != heroData.TrangThai.ChienThuat)
			{
				GameManager.instance.m_GameClient.SetHeroData(heroData);
			}
		}
	}

	private void OnActivateTrangThaiBiDongCongThapNhat(bool isActive)
	{
		if (isActive && heroData != null)
		{
			if (heroData.TrangThai == null)
			{
				heroData.TrangThai = new UserInfo.HeroData.AI();
			}
			heroData.TrangThai.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_CONG_MIN;
			UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
			if (heroFromDoiHinh.TrangThai.ChienThuat != heroData.TrangThai.ChienThuat)
			{
				GameManager.instance.m_GameClient.SetHeroData(heroData);
			}
		}
	}

	private void OnActivateTrangThaiBiDongKhiCaoNhat(bool isActive)
	{
		if (isActive && heroData != null)
		{
			if (heroData.TrangThai == null)
			{
				heroData.TrangThai = new UserInfo.HeroData.AI();
			}
			heroData.TrangThai.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_NOI_MAX;
			UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
			if (heroFromDoiHinh.TrangThai.ChienThuat != heroData.TrangThai.ChienThuat)
			{
				GameManager.instance.m_GameClient.SetHeroData(heroData);
			}
		}
	}

	private void OnActivateTrangThaiBiDongKhiThapNhat(bool isActive)
	{
		if (isActive && heroData != null)
		{
			if (heroData.TrangThai == null)
			{
				heroData.TrangThai = new UserInfo.HeroData.AI();
			}
			heroData.TrangThai.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_NOI_MIN;
			UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
			if (heroFromDoiHinh.TrangThai.ChienThuat != heroData.TrangThai.ChienThuat)
			{
				GameManager.instance.m_GameClient.SetHeroData(heroData);
			}
		}
	}

	private void OnActivateTrangThaiBiDongSlot(bool isActive)
	{
		if (isActive && heroData != null)
		{
			if (heroData.TrangThai == null)
			{
				heroData.TrangThai = new UserInfo.HeroData.AI();
			}
			heroData.TrangThai.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_SLOT;
			UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
			ShowTrangThaiBiDongSlots();
			if (heroFromDoiHinh.TrangThai.ChienThuat != heroData.TrangThai.ChienThuat || JsonMapper.ToJson(heroData.TrangThai.TanCongSlotList) != JsonMapper.ToJson(heroFromDoiHinh.TrangThai.TanCongSlotList))
			{
				GameManager.instance.m_GameClient.SetHeroData(heroData);
			}
		}
		else if (!isActive)
		{
			optionBiDongSlots.SetActive(false);
		}
	}

	private void ShowTrangThaiBiDongSlots()
	{
		if (heroData == null || heroData.TrangThai == null || heroData.TrangThai.TanCongSlotList == null)
		{
			return;
		}
		optionBiDongSlots.SetActive(true);
		for (int i = 0; i < heroData.TrangThai.TanCongSlotList.Count; i++)
		{
			if (heroData.TrangThai.TanCongSlotList[i] == 0)
			{
				optionBiDongSlot1.transform.localPosition = posOrder[i];
			}
			if (heroData.TrangThai.TanCongSlotList[i] == 1)
			{
				optionBiDongSlot2.transform.localPosition = posOrder[i];
			}
			if (heroData.TrangThai.TanCongSlotList[i] == 2)
			{
				optionBiDongSlot3.transform.localPosition = posOrder[i];
			}
			if (heroData.TrangThai.TanCongSlotList[i] == 3)
			{
				optionBiDongSlot4.transform.localPosition = posOrder[i];
			}
		}
	}

	private void OnBiDongSlot1Press(bool isPress)
	{
		if (isPress)
		{
			previous1PosBiDong = optionBiDongSlot1.transform.localPosition;
			optionBiDongSlot1.transform.localScale = new Vector3(0.86f, 0.86f, 1f);
			optionBiDongSlot1.transform.localPosition = previous1PosBiDong + new Vector3(0f, 0f, -1f);
			return;
		}
		optionBiDongSlot1.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
		optionBiDongSlot1.transform.localPosition = previous1PosBiDong;
		if (heroData.TrangThai == null)
		{
			heroData.TrangThai = new UserInfo.HeroData.AI();
		}
		heroData.TrangThai.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_SLOT;
		UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
		if (heroFromDoiHinh.TrangThai.ChienThuat != heroData.TrangThai.ChienThuat || JsonMapper.ToJson(heroData.TrangThai.TanCongSlotList) != JsonMapper.ToJson(heroFromDoiHinh.TrangThai.TanCongSlotList))
		{
			GameManager.instance.m_GameClient.SetHeroData(heroData);
		}
	}

	private void OnBiDongSlot1Drag(Vector2 delta)
	{
		Vector3 vector = optionBiDongSlot1.transform.localPosition - optionBiDongSlot2.transform.localPosition;
		Vector3 vector2 = optionBiDongSlot1.transform.localPosition - optionBiDongSlot3.transform.localPosition;
		Vector3 vector3 = optionBiDongSlot1.transform.localPosition - optionBiDongSlot4.transform.localPosition;
		if (Mathf.Abs(vector.x) < 50f)
		{
			Vector3 localPosition = previous1PosBiDong;
			previous1PosBiDong = optionBiDongSlot2.transform.localPosition;
			optionBiDongSlot2.transform.localPosition = localPosition;
			if (heroData != null && heroData.TrangThai != null && heroData.TrangThai.TanCongSlotList != null)
			{
				for (int i = 0; i < heroData.TrangThai.TanCongSlotList.Count; i++)
				{
					if (heroData.TrangThai.TanCongSlotList[i] == 1)
					{
						heroData.TrangThai.TanCongSlotList[i] = 0;
					}
					else if (heroData.TrangThai.TanCongSlotList[i] == 0)
					{
						heroData.TrangThai.TanCongSlotList[i] = 1;
					}
				}
			}
		}
		if (Mathf.Abs(vector2.x) < 50f)
		{
			Vector3 localPosition2 = previous1PosBiDong;
			previous1PosBiDong = optionBiDongSlot3.transform.localPosition;
			optionBiDongSlot3.transform.localPosition = localPosition2;
			if (heroData != null && heroData.TrangThai != null && heroData.TrangThai.TanCongSlotList != null)
			{
				for (int j = 0; j < heroData.TrangThai.TanCongSlotList.Count; j++)
				{
					if (heroData.TrangThai.TanCongSlotList[j] == 2)
					{
						heroData.TrangThai.TanCongSlotList[j] = 0;
					}
					else if (heroData.TrangThai.TanCongSlotList[j] == 0)
					{
						heroData.TrangThai.TanCongSlotList[j] = 2;
					}
				}
			}
		}
		if (!(Mathf.Abs(vector3.x) < 50f))
		{
			return;
		}
		Vector3 localPosition3 = previous1PosBiDong;
		previous1PosBiDong = optionBiDongSlot4.transform.localPosition;
		optionBiDongSlot4.transform.localPosition = localPosition3;
		if (heroData == null || heroData.TrangThai == null || heroData.TrangThai.TanCongSlotList == null)
		{
			return;
		}
		for (int k = 0; k < heroData.TrangThai.TanCongSlotList.Count; k++)
		{
			if (heroData.TrangThai.TanCongSlotList[k] == 3)
			{
				heroData.TrangThai.TanCongSlotList[k] = 0;
			}
			else if (heroData.TrangThai.TanCongSlotList[k] == 0)
			{
				heroData.TrangThai.TanCongSlotList[k] = 3;
			}
		}
	}

	private void OnBiDongSlot2Press(bool isPress)
	{
		if (isPress)
		{
			previous2PosBiDong = optionBiDongSlot2.transform.localPosition;
			optionBiDongSlot2.transform.localScale = new Vector3(0.86f, 0.86f, 1f);
			optionBiDongSlot2.transform.localPosition = previous2PosBiDong + new Vector3(0f, 0f, -1f);
			return;
		}
		optionBiDongSlot2.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
		optionBiDongSlot2.transform.localPosition = previous2PosBiDong;
		if (heroData.TrangThai == null)
		{
			heroData.TrangThai = new UserInfo.HeroData.AI();
		}
		heroData.TrangThai.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_SLOT;
		UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
		if (heroFromDoiHinh.TrangThai.ChienThuat != heroData.TrangThai.ChienThuat || JsonMapper.ToJson(heroData.TrangThai.TanCongSlotList) != JsonMapper.ToJson(heroFromDoiHinh.TrangThai.TanCongSlotList))
		{
			GameManager.instance.m_GameClient.SetHeroData(heroData);
		}
	}

	private void OnBiDongSlot2Drag(Vector2 delta)
	{
		Vector3 vector = optionBiDongSlot2.transform.localPosition - optionBiDongSlot1.transform.localPosition;
		Vector3 vector2 = optionBiDongSlot2.transform.localPosition - optionBiDongSlot3.transform.localPosition;
		Vector3 vector3 = optionBiDongSlot2.transform.localPosition - optionBiDongSlot4.transform.localPosition;
		if (Mathf.Abs(vector.x) < 50f)
		{
			Vector3 localPosition = previous2PosBiDong;
			previous2PosBiDong = optionBiDongSlot1.transform.localPosition;
			optionBiDongSlot1.transform.localPosition = localPosition;
			if (heroData != null && heroData.TrangThai != null && heroData.TrangThai.TanCongSlotList != null)
			{
				for (int i = 0; i < heroData.TrangThai.TanCongSlotList.Count; i++)
				{
					if (heroData.TrangThai.TanCongSlotList[i] == 1)
					{
						heroData.TrangThai.TanCongSlotList[i] = 0;
					}
					else if (heroData.TrangThai.TanCongSlotList[i] == 0)
					{
						heroData.TrangThai.TanCongSlotList[i] = 1;
					}
				}
			}
		}
		if (Mathf.Abs(vector2.x) < 50f)
		{
			Vector3 localPosition2 = previous2PosBiDong;
			previous2PosBiDong = optionBiDongSlot3.transform.localPosition;
			optionBiDongSlot3.transform.localPosition = localPosition2;
			if (heroData != null && heroData.TrangThai != null && heroData.TrangThai.TanCongSlotList != null)
			{
				for (int j = 0; j < heroData.TrangThai.TanCongSlotList.Count; j++)
				{
					if (heroData.TrangThai.TanCongSlotList[j] == 1)
					{
						heroData.TrangThai.TanCongSlotList[j] = 2;
					}
					else if (heroData.TrangThai.TanCongSlotList[j] == 2)
					{
						heroData.TrangThai.TanCongSlotList[j] = 1;
					}
				}
			}
		}
		if (!(Mathf.Abs(vector3.x) < 50f))
		{
			return;
		}
		Vector3 localPosition3 = previous2PosBiDong;
		previous2PosBiDong = optionBiDongSlot4.transform.localPosition;
		optionBiDongSlot4.transform.localPosition = localPosition3;
		if (heroData == null || heroData.TrangThai == null || heroData.TrangThai.TanCongSlotList == null)
		{
			return;
		}
		for (int k = 0; k < heroData.TrangThai.TanCongSlotList.Count; k++)
		{
			if (heroData.TrangThai.TanCongSlotList[k] == 1)
			{
				heroData.TrangThai.TanCongSlotList[k] = 3;
			}
			else if (heroData.TrangThai.TanCongSlotList[k] == 3)
			{
				heroData.TrangThai.TanCongSlotList[k] = 1;
			}
		}
	}

	private void OnBiDongSlot3Press(bool isPress)
	{
		if (isPress)
		{
			previous3PosBiDong = optionBiDongSlot3.transform.localPosition;
			optionBiDongSlot3.transform.localScale = new Vector3(0.86f, 0.86f, 1f);
			optionBiDongSlot3.transform.localPosition = previous3PosBiDong + new Vector3(0f, 0f, -1f);
			return;
		}
		optionBiDongSlot3.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
		optionBiDongSlot3.transform.localPosition = previous3PosBiDong;
		if (heroData.TrangThai == null)
		{
			heroData.TrangThai = new UserInfo.HeroData.AI();
		}
		heroData.TrangThai.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_SLOT;
		UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
		if (heroFromDoiHinh.TrangThai.ChienThuat != heroData.TrangThai.ChienThuat || JsonMapper.ToJson(heroData.TrangThai.TanCongSlotList) != JsonMapper.ToJson(heroFromDoiHinh.TrangThai.TanCongSlotList))
		{
			GameManager.instance.m_GameClient.SetHeroData(heroData);
		}
	}

	private void OnBiDongSlot3Drag(Vector2 delta)
	{
		Vector3 vector = optionBiDongSlot3.transform.localPosition - optionBiDongSlot2.transform.localPosition;
		Vector3 vector2 = optionBiDongSlot3.transform.localPosition - optionBiDongSlot1.transform.localPosition;
		Vector3 vector3 = optionBiDongSlot3.transform.localPosition - optionBiDongSlot4.transform.localPosition;
		if (Mathf.Abs(vector.x) < 50f)
		{
			Vector3 localPosition = previous3PosBiDong;
			previous3PosBiDong = optionBiDongSlot2.transform.localPosition;
			optionBiDongSlot2.transform.localPosition = localPosition;
			if (heroData != null && heroData.TrangThai != null && heroData.TrangThai.TanCongSlotList != null)
			{
				for (int i = 0; i < heroData.TrangThai.TanCongSlotList.Count; i++)
				{
					if (heroData.TrangThai.TanCongSlotList[i] == 1)
					{
						heroData.TrangThai.TanCongSlotList[i] = 2;
					}
					else if (heroData.TrangThai.TanCongSlotList[i] == 2)
					{
						heroData.TrangThai.TanCongSlotList[i] = 1;
					}
				}
			}
		}
		if (Mathf.Abs(vector2.x) < 50f)
		{
			Vector3 localPosition2 = previous3PosBiDong;
			previous3PosBiDong = optionBiDongSlot1.transform.localPosition;
			optionBiDongSlot1.transform.localPosition = localPosition2;
			if (heroData != null && heroData.TrangThai != null && heroData.TrangThai.TanCongSlotList != null)
			{
				for (int j = 0; j < heroData.TrangThai.TanCongSlotList.Count; j++)
				{
					if (heroData.TrangThai.TanCongSlotList[j] == 2)
					{
						heroData.TrangThai.TanCongSlotList[j] = 0;
					}
					else if (heroData.TrangThai.TanCongSlotList[j] == 0)
					{
						heroData.TrangThai.TanCongSlotList[j] = 2;
					}
				}
			}
		}
		if (!(Mathf.Abs(vector3.x) < 50f))
		{
			return;
		}
		Vector3 localPosition3 = previous3PosBiDong;
		previous3PosBiDong = optionBiDongSlot4.transform.localPosition;
		optionBiDongSlot4.transform.localPosition = localPosition3;
		if (heroData == null || heroData.TrangThai == null || heroData.TrangThai.TanCongSlotList == null)
		{
			return;
		}
		for (int k = 0; k < heroData.TrangThai.TanCongSlotList.Count; k++)
		{
			if (heroData.TrangThai.TanCongSlotList[k] == 2)
			{
				heroData.TrangThai.TanCongSlotList[k] = 3;
			}
			else if (heroData.TrangThai.TanCongSlotList[k] == 3)
			{
				heroData.TrangThai.TanCongSlotList[k] = 2;
			}
		}
	}

	private void OnBiDongSlot4Press(bool isPress)
	{
		if (isPress)
		{
			previous4PosBiDong = optionBiDongSlot4.transform.localPosition;
			optionBiDongSlot4.transform.localScale = new Vector3(0.86f, 0.86f, 1f);
			optionBiDongSlot4.transform.localPosition = previous4PosBiDong + new Vector3(0f, 0f, -1f);
			return;
		}
		optionBiDongSlot4.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
		optionBiDongSlot4.transform.localPosition = previous4PosBiDong;
		if (heroData.TrangThai == null)
		{
			heroData.TrangThai = new UserInfo.HeroData.AI();
		}
		heroData.TrangThai.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_SLOT;
		UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
		if (heroFromDoiHinh.TrangThai.ChienThuat != heroData.TrangThai.ChienThuat || JsonMapper.ToJson(heroData.TrangThai.TanCongSlotList) != JsonMapper.ToJson(heroFromDoiHinh.TrangThai.TanCongSlotList))
		{
			GameManager.instance.m_GameClient.SetHeroData(heroData);
		}
	}

	private void OnBiDongSlot4Drag(Vector2 delta)
	{
		Vector3 vector = optionBiDongSlot4.transform.localPosition - optionBiDongSlot2.transform.localPosition;
		Vector3 vector2 = optionBiDongSlot4.transform.localPosition - optionBiDongSlot3.transform.localPosition;
		Vector3 vector3 = optionBiDongSlot4.transform.localPosition - optionBiDongSlot1.transform.localPosition;
		if (Mathf.Abs(vector.x) < 50f)
		{
			Vector3 localPosition = previous4PosBiDong;
			previous4PosBiDong = optionBiDongSlot2.transform.localPosition;
			optionBiDongSlot2.transform.localPosition = localPosition;
			if (heroData != null && heroData.TrangThai != null && heroData.TrangThai.TanCongSlotList != null)
			{
				for (int i = 0; i < heroData.TrangThai.TanCongSlotList.Count; i++)
				{
					if (heroData.TrangThai.TanCongSlotList[i] == 1)
					{
						heroData.TrangThai.TanCongSlotList[i] = 3;
					}
					else if (heroData.TrangThai.TanCongSlotList[i] == 3)
					{
						heroData.TrangThai.TanCongSlotList[i] = 1;
					}
				}
			}
		}
		if (Mathf.Abs(vector2.x) < 50f)
		{
			Vector3 localPosition2 = previous4PosBiDong;
			previous4PosBiDong = optionBiDongSlot3.transform.localPosition;
			optionBiDongSlot3.transform.localPosition = localPosition2;
			if (heroData != null && heroData.TrangThai != null && heroData.TrangThai.TanCongSlotList != null)
			{
				for (int j = 0; j < heroData.TrangThai.TanCongSlotList.Count; j++)
				{
					if (heroData.TrangThai.TanCongSlotList[j] == 3)
					{
						heroData.TrangThai.TanCongSlotList[j] = 2;
					}
					else if (heroData.TrangThai.TanCongSlotList[j] == 2)
					{
						heroData.TrangThai.TanCongSlotList[j] = 3;
					}
				}
			}
		}
		if (!(Mathf.Abs(vector3.x) < 50f))
		{
			return;
		}
		Vector3 localPosition3 = previous4PosBiDong;
		previous4PosBiDong = optionBiDongSlot1.transform.localPosition;
		optionBiDongSlot1.transform.localPosition = localPosition3;
		if (heroData == null || heroData.TrangThai == null || heroData.TrangThai.TanCongSlotList == null)
		{
			return;
		}
		for (int k = 0; k < heroData.TrangThai.TanCongSlotList.Count; k++)
		{
			if (heroData.TrangThai.TanCongSlotList[k] == 3)
			{
				heroData.TrangThai.TanCongSlotList[k] = 0;
			}
			else if (heroData.TrangThai.TanCongSlotList[k] == 0)
			{
				heroData.TrangThai.TanCongSlotList[k] = 3;
			}
		}
	}

	private void ShowChieuThucKeThuMucTieu(VCType vcType)
	{
		if (heroData == null)
		{
			return;
		}
		switch (vcThietLap.Parameter.MucTieuChieu)
		{
		case UserInfo.VCThietLapData.ENUM_MUC_TIEU_CHIEU.MUC_TIEU_DANG_TAN_CONG:
			if (!optionKeThuMucTieuCaoCap.isChecked)
			{
				OnActivateCaoCapKeThuMucTieu(false);
			}
			optionKeThuMucTieuCaoCap.isChecked = false;
			if (optionKeThuMucTieuDangTanCong.isChecked)
			{
				OnActivateChieuThucKeThuMucTieuDangTanCong(true);
			}
			optionKeThuMucTieuDangTanCong.isChecked = true;
			break;
		case UserInfo.VCThietLapData.ENUM_MUC_TIEU_CHIEU.MUC_TIEU_SINH_LUC_MAX:
			if (optionKeThuMucTieuCaoCap.isChecked)
			{
				OnActivateCaoCapKeThuMucTieu(true);
			}
			optionKeThuMucTieuCaoCap.isChecked = true;
			if (optionKeThuMucTieuMauNhieuNhat.isChecked)
			{
				OnActivateChieuThucKeThuMucTieuMauNhieuNhat(true);
			}
			optionKeThuMucTieuMauNhieuNhat.isChecked = true;
			break;
		case UserInfo.VCThietLapData.ENUM_MUC_TIEU_CHIEU.MUC_TIEU_SINH_LUC_MIN:
			if (optionKeThuMucTieuCaoCap.isChecked)
			{
				OnActivateCaoCapKeThuMucTieu(true);
			}
			optionKeThuMucTieuCaoCap.isChecked = true;
			if (optionKeThuMucTieuMauItNhat.isChecked)
			{
				OnActivateChieuThucKeThuMucTieuMauItNhat(true);
			}
			optionKeThuMucTieuMauItNhat.isChecked = true;
			break;
		case UserInfo.VCThietLapData.ENUM_MUC_TIEU_CHIEU.MUC_TIEU_THAN_PHAP_MAX:
			if (optionKeThuMucTieuCaoCap.isChecked)
			{
				OnActivateCaoCapKeThuMucTieu(true);
			}
			optionKeThuMucTieuCaoCap.isChecked = true;
			if (optionKeThuMucTieuThanPhapNhanhNhat.isChecked)
			{
				OnActivateChieuThucKeThuMucTieuThanPhapCaoNhat(true);
			}
			optionKeThuMucTieuThanPhapNhanhNhat.isChecked = true;
			break;
		case UserInfo.VCThietLapData.ENUM_MUC_TIEU_CHIEU.MUC_TIEU_THAN_PHAP_MIN:
			if (optionKeThuMucTieuCaoCap.isChecked)
			{
				OnActivateCaoCapKeThuMucTieu(true);
			}
			optionKeThuMucTieuCaoCap.isChecked = true;
			if (optionKeThuMucTieuThanPhapChamNhat.isChecked)
			{
				OnActivateChieuThucKeThuMucTieuThanPhapThapNhat(true);
			}
			optionKeThuMucTieuThanPhapChamNhat.isChecked = true;
			break;
		case UserInfo.VCThietLapData.ENUM_MUC_TIEU_CHIEU.MUC_TIEU_CONG_MAX:
			if (optionKeThuMucTieuCaoCap.isChecked)
			{
				OnActivateCaoCapKeThuMucTieu(true);
			}
			optionKeThuMucTieuCaoCap.isChecked = true;
			if (optionKeThuMucTieuCongToNhat.isChecked)
			{
				OnActivateChieuThucKeThuMucTieuCongCaoNhat(true);
			}
			optionKeThuMucTieuCongToNhat.isChecked = true;
			break;
		case UserInfo.VCThietLapData.ENUM_MUC_TIEU_CHIEU.MUC_TIEU_CONG_MIN:
			if (optionKeThuMucTieuCaoCap.isChecked)
			{
				OnActivateCaoCapKeThuMucTieu(true);
			}
			optionKeThuMucTieuCaoCap.isChecked = true;
			if (optionKeThuMucTieuCongNhoNhat.isChecked)
			{
				OnActivateChieuThucKeThuMucTieuCongThapNhat(true);
			}
			optionKeThuMucTieuCongNhoNhat.isChecked = true;
			break;
		case UserInfo.VCThietLapData.ENUM_MUC_TIEU_CHIEU.MUC_TIEU_NOI_MAX:
			if (optionKeThuMucTieuCaoCap.isChecked)
			{
				OnActivateCaoCapKeThuMucTieu(true);
			}
			optionKeThuMucTieuCaoCap.isChecked = true;
			if (optionKeThuMucTieuKhiLonNhat.isChecked)
			{
				OnActivateChieuThucKeThuMucTieuKhiCaoNhat(true);
			}
			optionKeThuMucTieuKhiLonNhat.isChecked = true;
			break;
		case UserInfo.VCThietLapData.ENUM_MUC_TIEU_CHIEU.MUC_TIEU_NOI_MIN:
			if (optionKeThuMucTieuCaoCap.isChecked)
			{
				OnActivateCaoCapKeThuMucTieu(true);
			}
			optionKeThuMucTieuCaoCap.isChecked = true;
			if (optionKeThuMucTieuKhiNhoNhat.isChecked)
			{
				OnActivateChieuThucKeThuMucTieuKhiThapNhat(true);
			}
			optionKeThuMucTieuKhiNhoNhat.isChecked = true;
			break;
		case UserInfo.VCThietLapData.ENUM_MUC_TIEU_CHIEU.MUC_TIEU_SLOT:
			if (!optionKeThuMucTieuCaoCap.isChecked)
			{
				OnActivateCaoCapKeThuMucTieu(false);
			}
			optionKeThuMucTieuCaoCap.isChecked = false;
			if (optionKeThuMucTieuSlot.isChecked)
			{
				OnActivateChieuThucKeThuMucTieuSlot(true);
			}
			optionKeThuMucTieuSlot.isChecked = true;
			break;
		default:
			if (!optionKeThuMucTieuCaoCap.isChecked)
			{
				OnActivateCaoCapKeThuMucTieu(false);
			}
			optionKeThuMucTieuCaoCap.isChecked = false;
			if (optionKeThuMucTieuDangTanCong.isChecked)
			{
				OnActivateChieuThucKeThuMucTieuDangTanCong(true);
			}
			optionKeThuMucTieuDangTanCong.isChecked = true;
			break;
		}
	}

	private void ShowChieuThucKeThuDungKhi(VCType vcType)
	{
		if (heroData == null || vcThietLap == null)
		{
			return;
		}
		switch (vcThietLap.Parameter.DungKhi)
		{
		case UserInfo.VCThietLapData.ENUM_DUNG_KHI.NGAY_KHI_CO_THE:
			if (optionKeThuDungKhiNgayKhiCoThe.isChecked)
			{
				OnActivateChieuThucKeThuDungKhiNgayKhiCoThe(true);
			}
			optionKeThuDungKhiNgayKhiCoThe.isChecked = true;
			break;
		case UserInfo.VCThietLapData.ENUM_DUNG_KHI.SINH_LUC_MUC_TIEU_XUONG_THAP:
			if (optionKeThuDungKhiMauMucTieuDuoi.isChecked)
			{
				OnActivateChieuThucKeThuDungKhiMauMucTieuDuoi(true);
			}
			optionKeThuDungKhiMauMucTieuDuoi.isChecked = true;
			break;
		case UserInfo.VCThietLapData.ENUM_DUNG_KHI.SINH_LUC_MUC_TIEU_TREN:
			if (optionKeThuDungKhiMauMucTieuTren.isChecked)
			{
				OnActivateChieuThucKeThuDungKhiMauMucTieuTren(true);
			}
			optionKeThuDungKhiMauMucTieuTren.isChecked = true;
			break;
		case UserInfo.VCThietLapData.ENUM_DUNG_KHI.SINH_LUC_BAN_THAN_XUONG_THAP:
			if (optionKeThuDungKhiMauBanThanDuoi.isChecked)
			{
				OnActivateChieuThucKeThuDungKhiMauBanThanDuoi(true);
			}
			optionKeThuDungKhiMauBanThanDuoi.isChecked = true;
			break;
		default:
			optionKeThuDungKhiNgayKhiCoThe.isChecked = true;
			break;
		}
	}

	private void ShowChieuThucKeThuNgungKhi(VCType vcType)
	{
		if (heroData == null || vcThietLap == null)
		{
			return;
		}
		switch (vcThietLap.Parameter.NgungDungKhi)
		{
		case UserInfo.VCThietLapData.ENUM_NGUNG_DUNG_KHI.SINH_LUC_THAP_HON:
			if (optionKeThuNgungKhiMauBanThanDuoi.isChecked)
			{
				OnActivateChieuThucKeThuNgungKhiMauDuoi(true);
			}
			optionKeThuNgungKhiMauBanThanDuoi.isChecked = true;
			break;
		case UserInfo.VCThietLapData.ENUM_NGUNG_DUNG_KHI.NOI_LUC_THAP_HON:
			if (optionKeThuNgungKhiNoiLucDuoi.isChecked)
			{
				OnActivateChieuThucKeThuNgungKhiNoiLucDuoi(true);
			}
			optionKeThuNgungKhiNoiLucDuoi.isChecked = true;
			break;
		default:
			optionKeThuNgungKhiMauBanThanDuoi.isChecked = true;
			break;
		}
	}

	private void OnActivateChieuThucKeThuMucTieuDangTanCong(bool isActive)
	{
		if (isActive && heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.MucTieuChieu = UserInfo.VCThietLapData.ENUM_MUC_TIEU_CHIEU.MUC_TIEU_DANG_TAN_CONG;
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.MucTieuChieu != vcThietLap.Parameter.MucTieuChieu)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
	}

	private void OnActivateChieuThucKeThuMucTieuMauNhieuNhat(bool isActive)
	{
		if (isActive && heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.MucTieuChieu = UserInfo.VCThietLapData.ENUM_MUC_TIEU_CHIEU.MUC_TIEU_SINH_LUC_MAX;
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.MucTieuChieu != vcThietLap.Parameter.MucTieuChieu)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
	}

	private void OnActivateChieuThucKeThuMucTieuMauItNhat(bool isActive)
	{
		if (isActive && heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.MucTieuChieu = UserInfo.VCThietLapData.ENUM_MUC_TIEU_CHIEU.MUC_TIEU_SINH_LUC_MIN;
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.MucTieuChieu != vcThietLap.Parameter.MucTieuChieu)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
	}

	private void OnActivateChieuThucKeThuMucTieuThanPhapCaoNhat(bool isActive)
	{
		if (isActive && heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.MucTieuChieu = UserInfo.VCThietLapData.ENUM_MUC_TIEU_CHIEU.MUC_TIEU_THAN_PHAP_MAX;
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.MucTieuChieu != vcThietLap.Parameter.MucTieuChieu)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
	}

	private void OnActivateChieuThucKeThuMucTieuThanPhapThapNhat(bool isActive)
	{
		if (isActive && heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.MucTieuChieu = UserInfo.VCThietLapData.ENUM_MUC_TIEU_CHIEU.MUC_TIEU_THAN_PHAP_MIN;
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.MucTieuChieu != vcThietLap.Parameter.MucTieuChieu)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
	}

	private void OnActivateChieuThucKeThuMucTieuCongCaoNhat(bool isActive)
	{
		if (isActive && heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.MucTieuChieu = UserInfo.VCThietLapData.ENUM_MUC_TIEU_CHIEU.MUC_TIEU_CONG_MAX;
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.MucTieuChieu != vcThietLap.Parameter.MucTieuChieu)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
	}

	private void OnActivateChieuThucKeThuMucTieuCongThapNhat(bool isActive)
	{
		if (isActive && heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.MucTieuChieu = UserInfo.VCThietLapData.ENUM_MUC_TIEU_CHIEU.MUC_TIEU_CONG_MIN;
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.MucTieuChieu != vcThietLap.Parameter.MucTieuChieu)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
	}

	private void OnActivateChieuThucKeThuMucTieuKhiCaoNhat(bool isActive)
	{
		if (isActive && heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.MucTieuChieu = UserInfo.VCThietLapData.ENUM_MUC_TIEU_CHIEU.MUC_TIEU_NOI_MAX;
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.MucTieuChieu != vcThietLap.Parameter.MucTieuChieu)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
	}

	private void OnActivateChieuThucKeThuMucTieuKhiThapNhat(bool isActive)
	{
		if (isActive && heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.MucTieuChieu = UserInfo.VCThietLapData.ENUM_MUC_TIEU_CHIEU.MUC_TIEU_NOI_MIN;
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.MucTieuChieu != vcThietLap.Parameter.MucTieuChieu)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
	}

	private void OnActivateChieuThucKeThuMucTieuSlot(bool isActive)
	{
		if (isActive && heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.MucTieuChieu = UserInfo.VCThietLapData.ENUM_MUC_TIEU_CHIEU.MUC_TIEU_SLOT;
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			ShowChieuThucKeThuMucTieuSlots();
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.MucTieuChieu != vcThietLap.Parameter.MucTieuChieu || JsonMapper.ToJson(vCThietLapFromHero.Parameter.MucTieuChieuSlots) != JsonMapper.ToJson(vcThietLap.Parameter.MucTieuChieuSlots))
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
		else if (!isActive)
		{
			optionKeThuMucTieuSlots.SetActive(false);
		}
	}

	private void ShowChieuThucKeThuMucTieuSlots()
	{
		if (heroData == null || vcThietLap == null)
		{
			return;
		}
		optionKeThuMucTieuSlots.SetActive(true);
		for (int i = 0; i < vcThietLap.Parameter.MucTieuChieuSlots.Count; i++)
		{
			if (vcThietLap.Parameter.MucTieuChieuSlots[i] == 0)
			{
				optionKeThuMucTieuSlot1.transform.localPosition = posOrder[i];
			}
			if (vcThietLap.Parameter.MucTieuChieuSlots[i] == 1)
			{
				optionKeThuMucTieuSlot2.transform.localPosition = posOrder[i];
			}
			if (vcThietLap.Parameter.MucTieuChieuSlots[i] == 2)
			{
				optionKeThuMucTieuSlot3.transform.localPosition = posOrder[i];
			}
			if (vcThietLap.Parameter.MucTieuChieuSlots[i] == 3)
			{
				optionKeThuMucTieuSlot4.transform.localPosition = posOrder[i];
			}
		}
	}

	private void OnKeThuMucTieuSlot1Press(bool isPress)
	{
		if (isPress)
		{
			previous1PosKeThuMucTieu = optionKeThuMucTieuSlot1.transform.localPosition;
			optionKeThuMucTieuSlot1.transform.localScale = new Vector3(0.86f, 0.86f, 1f);
			optionKeThuMucTieuSlot1.transform.localPosition = previous1PosKeThuMucTieu + new Vector3(0f, 0f, -1f);
			return;
		}
		optionKeThuMucTieuSlot1.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
		optionKeThuMucTieuSlot1.transform.localPosition = previous1PosKeThuMucTieu;
		if (heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.MucTieuChieu = UserInfo.VCThietLapData.ENUM_MUC_TIEU_CHIEU.MUC_TIEU_SLOT;
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.MucTieuChieu != vcThietLap.Parameter.MucTieuChieu || JsonMapper.ToJson(vCThietLapFromHero.Parameter.MucTieuChieuSlots) != JsonMapper.ToJson(vcThietLap.Parameter.MucTieuChieuSlots))
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
	}

	private void OnKeThuMucTieuSlot1Drag(Vector2 delta)
	{
		Vector3 vector = optionKeThuMucTieuSlot1.transform.localPosition - optionKeThuMucTieuSlot2.transform.localPosition;
		Vector3 vector2 = optionKeThuMucTieuSlot1.transform.localPosition - optionKeThuMucTieuSlot3.transform.localPosition;
		Vector3 vector3 = optionKeThuMucTieuSlot1.transform.localPosition - optionKeThuMucTieuSlot4.transform.localPosition;
		if (Mathf.Abs(vector.x) < 50f)
		{
			Vector3 localPosition = previous1PosKeThuMucTieu;
			previous1PosKeThuMucTieu = optionKeThuMucTieuSlot2.transform.localPosition;
			optionKeThuMucTieuSlot2.transform.localPosition = localPosition;
			if (heroData != null && vcThietLap != null && vcThietLap.Parameter.MucTieuChieuSlots != null)
			{
				for (int i = 0; i < vcThietLap.Parameter.MucTieuChieuSlots.Count; i++)
				{
					if (vcThietLap.Parameter.MucTieuChieuSlots[i] == 1)
					{
						vcThietLap.Parameter.MucTieuChieuSlots[i] = 0;
					}
					else if (vcThietLap.Parameter.MucTieuChieuSlots[i] == 0)
					{
						vcThietLap.Parameter.MucTieuChieuSlots[i] = 1;
					}
				}
			}
		}
		if (Mathf.Abs(vector2.x) < 50f)
		{
			Vector3 localPosition2 = previous1PosKeThuMucTieu;
			previous1PosKeThuMucTieu = optionKeThuMucTieuSlot3.transform.localPosition;
			optionKeThuMucTieuSlot3.transform.localPosition = localPosition2;
			if (heroData != null && vcThietLap != null && vcThietLap.Parameter.MucTieuChieuSlots != null)
			{
				for (int j = 0; j < vcThietLap.Parameter.MucTieuChieuSlots.Count; j++)
				{
					if (vcThietLap.Parameter.MucTieuChieuSlots[j] == 2)
					{
						vcThietLap.Parameter.MucTieuChieuSlots[j] = 0;
					}
					else if (vcThietLap.Parameter.MucTieuChieuSlots[j] == 0)
					{
						vcThietLap.Parameter.MucTieuChieuSlots[j] = 2;
					}
				}
			}
		}
		if (!(Mathf.Abs(vector3.x) < 50f))
		{
			return;
		}
		Vector3 localPosition3 = previous1PosKeThuMucTieu;
		previous1PosKeThuMucTieu = optionKeThuMucTieuSlot4.transform.localPosition;
		optionKeThuMucTieuSlot4.transform.localPosition = localPosition3;
		if (heroData == null || vcThietLap == null || vcThietLap.Parameter.MucTieuChieuSlots == null)
		{
			return;
		}
		for (int k = 0; k < vcThietLap.Parameter.MucTieuChieuSlots.Count; k++)
		{
			if (vcThietLap.Parameter.MucTieuChieuSlots[k] == 3)
			{
				vcThietLap.Parameter.MucTieuChieuSlots[k] = 0;
			}
			else if (vcThietLap.Parameter.MucTieuChieuSlots[k] == 0)
			{
				vcThietLap.Parameter.MucTieuChieuSlots[k] = 3;
			}
		}
	}

	private void OnKeThuMucTieuSlot2Press(bool isPress)
	{
		if (isPress)
		{
			previous2PosKeThuMucTieu = optionKeThuMucTieuSlot2.transform.localPosition;
			optionKeThuMucTieuSlot2.transform.localScale = new Vector3(0.86f, 0.86f, 1f);
			optionKeThuMucTieuSlot2.transform.localPosition = previous2PosKeThuMucTieu + new Vector3(0f, 0f, -1f);
			return;
		}
		optionKeThuMucTieuSlot2.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
		optionKeThuMucTieuSlot2.transform.localPosition = previous2PosKeThuMucTieu;
		if (heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.MucTieuChieu = UserInfo.VCThietLapData.ENUM_MUC_TIEU_CHIEU.MUC_TIEU_SLOT;
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.MucTieuChieu != vcThietLap.Parameter.MucTieuChieu || JsonMapper.ToJson(vCThietLapFromHero.Parameter.MucTieuChieuSlots) != JsonMapper.ToJson(vcThietLap.Parameter.MucTieuChieuSlots))
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
	}

	private void OnKeThuMucTieuSlot2Drag(Vector2 delta)
	{
		Vector3 vector = optionKeThuMucTieuSlot2.transform.localPosition - optionKeThuMucTieuSlot1.transform.localPosition;
		Vector3 vector2 = optionKeThuMucTieuSlot2.transform.localPosition - optionKeThuMucTieuSlot3.transform.localPosition;
		Vector3 vector3 = optionKeThuMucTieuSlot2.transform.localPosition - optionKeThuMucTieuSlot4.transform.localPosition;
		if (Mathf.Abs(vector.x) < 50f)
		{
			Vector3 localPosition = previous2PosKeThuMucTieu;
			previous2PosKeThuMucTieu = optionKeThuMucTieuSlot1.transform.localPosition;
			optionKeThuMucTieuSlot1.transform.localPosition = localPosition;
			if (heroData != null && vcThietLap != null && vcThietLap.Parameter.MucTieuChieuSlots != null)
			{
				for (int i = 0; i < vcThietLap.Parameter.MucTieuChieuSlots.Count; i++)
				{
					if (vcThietLap.Parameter.MucTieuChieuSlots[i] == 1)
					{
						vcThietLap.Parameter.MucTieuChieuSlots[i] = 0;
					}
					else if (vcThietLap.Parameter.MucTieuChieuSlots[i] == 0)
					{
						vcThietLap.Parameter.MucTieuChieuSlots[i] = 1;
					}
				}
			}
		}
		if (Mathf.Abs(vector2.x) < 50f)
		{
			Vector3 localPosition2 = previous2PosKeThuMucTieu;
			previous2PosKeThuMucTieu = optionKeThuMucTieuSlot3.transform.localPosition;
			optionKeThuMucTieuSlot3.transform.localPosition = localPosition2;
			if (heroData != null && vcThietLap != null && vcThietLap.Parameter.MucTieuChieuSlots != null)
			{
				for (int j = 0; j < vcThietLap.Parameter.MucTieuChieuSlots.Count; j++)
				{
					if (vcThietLap.Parameter.MucTieuChieuSlots[j] == 1)
					{
						vcThietLap.Parameter.MucTieuChieuSlots[j] = 2;
					}
					else if (vcThietLap.Parameter.MucTieuChieuSlots[j] == 2)
					{
						vcThietLap.Parameter.MucTieuChieuSlots[j] = 1;
					}
				}
			}
		}
		if (!(Mathf.Abs(vector3.x) < 50f))
		{
			return;
		}
		Vector3 localPosition3 = previous2PosKeThuMucTieu;
		previous2PosKeThuMucTieu = optionKeThuMucTieuSlot4.transform.localPosition;
		optionKeThuMucTieuSlot4.transform.localPosition = localPosition3;
		if (heroData == null || vcThietLap == null || vcThietLap.Parameter.MucTieuChieuSlots == null)
		{
			return;
		}
		for (int k = 0; k < vcThietLap.Parameter.MucTieuChieuSlots.Count; k++)
		{
			if (vcThietLap.Parameter.MucTieuChieuSlots[k] == 1)
			{
				vcThietLap.Parameter.MucTieuChieuSlots[k] = 3;
			}
			else if (vcThietLap.Parameter.MucTieuChieuSlots[k] == 3)
			{
				vcThietLap.Parameter.MucTieuChieuSlots[k] = 1;
			}
		}
	}

	private void OnKeThuMucTieuSlot3Press(bool isPress)
	{
		if (isPress)
		{
			previous3PosKeThuMucTieu = optionKeThuMucTieuSlot3.transform.localPosition;
			optionKeThuMucTieuSlot3.transform.localScale = new Vector3(0.86f, 0.86f, 1f);
			optionKeThuMucTieuSlot3.transform.localPosition = previous3PosKeThuMucTieu + new Vector3(0f, 0f, -1f);
			return;
		}
		optionKeThuMucTieuSlot3.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
		optionKeThuMucTieuSlot3.transform.localPosition = previous3PosKeThuMucTieu;
		if (heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.MucTieuChieu = UserInfo.VCThietLapData.ENUM_MUC_TIEU_CHIEU.MUC_TIEU_SLOT;
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.MucTieuChieu != vcThietLap.Parameter.MucTieuChieu || JsonMapper.ToJson(vCThietLapFromHero.Parameter.MucTieuChieuSlots) != JsonMapper.ToJson(vcThietLap.Parameter.MucTieuChieuSlots))
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
	}

	private void OnKeThuMucTieuSlot3Drag(Vector2 delta)
	{
		Vector3 vector = optionKeThuMucTieuSlot3.transform.localPosition - optionKeThuMucTieuSlot2.transform.localPosition;
		Vector3 vector2 = optionKeThuMucTieuSlot3.transform.localPosition - optionKeThuMucTieuSlot1.transform.localPosition;
		Vector3 vector3 = optionKeThuMucTieuSlot3.transform.localPosition - optionKeThuMucTieuSlot4.transform.localPosition;
		if (Mathf.Abs(vector.x) < 50f)
		{
			Vector3 localPosition = previous3PosKeThuMucTieu;
			previous3PosKeThuMucTieu = optionKeThuMucTieuSlot2.transform.localPosition;
			optionKeThuMucTieuSlot2.transform.localPosition = localPosition;
			if (heroData != null && vcThietLap != null && vcThietLap.Parameter.MucTieuChieuSlots != null)
			{
				for (int i = 0; i < vcThietLap.Parameter.MucTieuChieuSlots.Count; i++)
				{
					if (vcThietLap.Parameter.MucTieuChieuSlots[i] == 1)
					{
						vcThietLap.Parameter.MucTieuChieuSlots[i] = 2;
					}
					else if (vcThietLap.Parameter.MucTieuChieuSlots[i] == 2)
					{
						vcThietLap.Parameter.MucTieuChieuSlots[i] = 1;
					}
				}
			}
		}
		if (Mathf.Abs(vector2.x) < 50f)
		{
			Vector3 localPosition2 = previous3PosKeThuMucTieu;
			previous3PosKeThuMucTieu = optionKeThuMucTieuSlot1.transform.localPosition;
			optionKeThuMucTieuSlot1.transform.localPosition = localPosition2;
			if (heroData != null && vcThietLap != null && vcThietLap.Parameter.MucTieuChieuSlots != null)
			{
				for (int j = 0; j < vcThietLap.Parameter.MucTieuChieuSlots.Count; j++)
				{
					if (vcThietLap.Parameter.MucTieuChieuSlots[j] == 2)
					{
						vcThietLap.Parameter.MucTieuChieuSlots[j] = 0;
					}
					else if (vcThietLap.Parameter.MucTieuChieuSlots[j] == 0)
					{
						vcThietLap.Parameter.MucTieuChieuSlots[j] = 2;
					}
				}
			}
		}
		if (!(Mathf.Abs(vector3.x) < 50f))
		{
			return;
		}
		Vector3 localPosition3 = previous3PosKeThuMucTieu;
		previous3PosKeThuMucTieu = optionKeThuMucTieuSlot4.transform.localPosition;
		optionKeThuMucTieuSlot4.transform.localPosition = localPosition3;
		if (heroData == null || vcThietLap == null || vcThietLap.Parameter.MucTieuChieuSlots == null)
		{
			return;
		}
		for (int k = 0; k < vcThietLap.Parameter.MucTieuChieuSlots.Count; k++)
		{
			if (vcThietLap.Parameter.MucTieuChieuSlots[k] == 2)
			{
				vcThietLap.Parameter.MucTieuChieuSlots[k] = 3;
			}
			else if (vcThietLap.Parameter.MucTieuChieuSlots[k] == 3)
			{
				vcThietLap.Parameter.MucTieuChieuSlots[k] = 2;
			}
		}
	}

	private void OnKeThuMucTieuSlot4Press(bool isPress)
	{
		if (isPress)
		{
			previous4PosKeThuMucTieu = optionKeThuMucTieuSlot4.transform.localPosition;
			optionKeThuMucTieuSlot4.transform.localScale = new Vector3(0.86f, 0.86f, 1f);
			optionKeThuMucTieuSlot4.transform.localPosition = previous4PosKeThuMucTieu + new Vector3(0f, 0f, -1f);
			return;
		}
		optionKeThuMucTieuSlot4.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
		optionKeThuMucTieuSlot4.transform.localPosition = previous4PosKeThuMucTieu;
		if (heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.MucTieuChieu = UserInfo.VCThietLapData.ENUM_MUC_TIEU_CHIEU.MUC_TIEU_SLOT;
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.MucTieuChieu != vcThietLap.Parameter.MucTieuChieu || JsonMapper.ToJson(vCThietLapFromHero.Parameter.MucTieuChieuSlots) != JsonMapper.ToJson(vcThietLap.Parameter.MucTieuChieuSlots))
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
	}

	private void OnKeThuMucTieuSlot4Drag(Vector2 delta)
	{
		Vector3 vector = optionKeThuMucTieuSlot4.transform.localPosition - optionKeThuMucTieuSlot2.transform.localPosition;
		Vector3 vector2 = optionKeThuMucTieuSlot4.transform.localPosition - optionKeThuMucTieuSlot3.transform.localPosition;
		Vector3 vector3 = optionKeThuMucTieuSlot4.transform.localPosition - optionKeThuMucTieuSlot1.transform.localPosition;
		if (Mathf.Abs(vector.x) < 50f)
		{
			Vector3 localPosition = previous4PosKeThuMucTieu;
			previous4PosKeThuMucTieu = optionKeThuMucTieuSlot2.transform.localPosition;
			optionKeThuMucTieuSlot2.transform.localPosition = localPosition;
			if (heroData != null && vcThietLap != null && vcThietLap.Parameter.MucTieuChieuSlots != null)
			{
				for (int i = 0; i < vcThietLap.Parameter.MucTieuChieuSlots.Count; i++)
				{
					if (vcThietLap.Parameter.MucTieuChieuSlots[i] == 1)
					{
						vcThietLap.Parameter.MucTieuChieuSlots[i] = 3;
					}
					else if (vcThietLap.Parameter.MucTieuChieuSlots[i] == 3)
					{
						vcThietLap.Parameter.MucTieuChieuSlots[i] = 1;
					}
				}
			}
		}
		if (Mathf.Abs(vector2.x) < 50f)
		{
			Vector3 localPosition2 = previous4PosKeThuMucTieu;
			previous4PosKeThuMucTieu = optionKeThuMucTieuSlot3.transform.localPosition;
			optionKeThuMucTieuSlot3.transform.localPosition = localPosition2;
			if (heroData != null && vcThietLap != null && vcThietLap.Parameter.MucTieuChieuSlots != null)
			{
				for (int j = 0; j < vcThietLap.Parameter.MucTieuChieuSlots.Count; j++)
				{
					if (vcThietLap.Parameter.MucTieuChieuSlots[j] == 3)
					{
						vcThietLap.Parameter.MucTieuChieuSlots[j] = 2;
					}
					else if (vcThietLap.Parameter.MucTieuChieuSlots[j] == 2)
					{
						vcThietLap.Parameter.MucTieuChieuSlots[j] = 3;
					}
				}
			}
		}
		if (!(Mathf.Abs(vector3.x) < 50f))
		{
			return;
		}
		Vector3 localPosition3 = previous4PosKeThuMucTieu;
		previous4PosKeThuMucTieu = optionKeThuMucTieuSlot1.transform.localPosition;
		optionKeThuMucTieuSlot1.transform.localPosition = localPosition3;
		if (heroData == null || vcThietLap == null || vcThietLap.Parameter.MucTieuChieuSlots == null)
		{
			return;
		}
		for (int k = 0; k < vcThietLap.Parameter.MucTieuChieuSlots.Count; k++)
		{
			if (vcThietLap.Parameter.MucTieuChieuSlots[k] == 3)
			{
				vcThietLap.Parameter.MucTieuChieuSlots[k] = 0;
			}
			else if (vcThietLap.Parameter.MucTieuChieuSlots[k] == 0)
			{
				vcThietLap.Parameter.MucTieuChieuSlots[k] = 3;
			}
		}
	}

	private void OnChieuThucKeThuMauMucTieuDuoiValueChange(float val)
	{
		if (heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.DungKhi = UserInfo.VCThietLapData.ENUM_DUNG_KHI.SINH_LUC_MUC_TIEU_XUONG_THAP;
			vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent = (int)(val * 100f);
			labelKeThuDungKhiMauMucTieuDuoi.text = string.Format("{0} %", vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent);
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.DungKhi != vcThietLap.Parameter.DungKhi || (int)vCThietLapFromHero.Parameter.DungKhi_TieuChi_ThapHonPercent != (int)vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent)
			{
				btnKeThuDungKhiMauMucTieuDuoi.SetActive(true);
			}
			else
			{
				btnKeThuDungKhiMauMucTieuDuoi.SetActive(false);
			}
		}
	}

	private void OnChieuThucKeThuMauMucTieuTrenValueChange(float val)
	{
		if (heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.DungKhi = UserInfo.VCThietLapData.ENUM_DUNG_KHI.SINH_LUC_MUC_TIEU_TREN;
			vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent = (int)(val * 100f);
			labelKeThuDungKhiMauMucTieuTren.text = string.Format("{0} %", vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent);
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.DungKhi != vcThietLap.Parameter.DungKhi || (int)vCThietLapFromHero.Parameter.DungKhi_TieuChi_ThapHonPercent != (int)vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent)
			{
				btnKeThuDungKhiMauMucTieuTren.SetActive(true);
			}
			else
			{
				btnKeThuDungKhiMauMucTieuTren.SetActive(false);
			}
		}
	}

	private void OnChieuThucKeThuMauBanThanDuoiValueChange(float val)
	{
		if (heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.DungKhi = UserInfo.VCThietLapData.ENUM_DUNG_KHI.SINH_LUC_BAN_THAN_XUONG_THAP;
			vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent = (int)(val * 100f);
			labelKeThuDungKhiMauBanThanDuoi.text = string.Format("{0} %", vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent);
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.DungKhi != vcThietLap.Parameter.DungKhi || (int)vCThietLapFromHero.Parameter.DungKhi_TieuChi_ThapHonPercent != (int)vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent)
			{
				btnKeThuDungKhiMauBanThanDuoi.SetActive(true);
			}
			else
			{
				btnKeThuDungKhiMauBanThanDuoi.SetActive(false);
			}
		}
	}

	private void OnConfirmChieuThucKeThuMauMucTieuDuoiValue(GameObject go)
	{
		if (heroData != null && vcThietLap != null && vcThietLap.Parameter.DungKhi == UserInfo.VCThietLapData.ENUM_DUNG_KHI.SINH_LUC_MUC_TIEU_XUONG_THAP)
		{
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.DungKhi != vcThietLap.Parameter.DungKhi || (int)vCThietLapFromHero.Parameter.DungKhi_TieuChi_ThapHonPercent != (int)vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
	}

	private void OnConfirmChieuThucKeThuMauMucTieuTrenValue(GameObject go)
	{
		if (heroData != null && vcThietLap != null && vcThietLap.Parameter.DungKhi == UserInfo.VCThietLapData.ENUM_DUNG_KHI.SINH_LUC_MUC_TIEU_TREN)
		{
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.DungKhi != vcThietLap.Parameter.DungKhi || (int)vCThietLapFromHero.Parameter.DungKhi_TieuChi_ThapHonPercent != (int)vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
	}

	private void OnConfirmChieuThucKeThuMauBanThanDuoiValue(GameObject go)
	{
		if (heroData != null && vcThietLap != null && vcThietLap.Parameter.DungKhi == UserInfo.VCThietLapData.ENUM_DUNG_KHI.SINH_LUC_BAN_THAN_XUONG_THAP)
		{
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.DungKhi != vcThietLap.Parameter.DungKhi || (int)vCThietLapFromHero.Parameter.DungKhi_TieuChi_ThapHonPercent != (int)vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
	}

	private void OnActivateChieuThucKeThuDungKhiNgayKhiCoThe(bool isActive)
	{
		if (isActive && heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.DungKhi = UserInfo.VCThietLapData.ENUM_DUNG_KHI.NGAY_KHI_CO_THE;
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.DungKhi != vcThietLap.Parameter.DungKhi)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
	}

	private void OnActivateChieuThucKeThuDungKhiMauMucTieuDuoi(bool isActive)
	{
		if (isActive && heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.DungKhi = UserInfo.VCThietLapData.ENUM_DUNG_KHI.SINH_LUC_MUC_TIEU_XUONG_THAP;
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			sliderKeThuDungKhiMauMucTieuDuoi.gameObject.SetActive(true);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.DungKhi != vcThietLap.Parameter.DungKhi)
			{
				sliderKeThuDungKhiMauMucTieuDuoi.sliderValue = 0f;
				vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent = 0.0;
			}
			else
			{
				sliderKeThuDungKhiMauMucTieuDuoi.sliderValue = (float)(int)vCThietLapFromHero.Parameter.DungKhi_TieuChi_ThapHonPercent / 100f;
				vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent = vCThietLapFromHero.Parameter.DungKhi_TieuChi_ThapHonPercent;
			}
			sliderKeThuDungKhiMauMucTieuDuoi.ForceUpdate();
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.DungKhi != vcThietLap.Parameter.DungKhi || (int)vCThietLapFromHero.Parameter.DungKhi_TieuChi_ThapHonPercent != (int)vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
		else
		{
			sliderKeThuDungKhiMauMucTieuDuoi.gameObject.SetActive(false);
			btnKeThuDungKhiMauMucTieuDuoi.gameObject.SetActive(false);
		}
	}

	private void OnActivateChieuThucKeThuDungKhiMauMucTieuTren(bool isActive)
	{
		if (isActive && heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.DungKhi = UserInfo.VCThietLapData.ENUM_DUNG_KHI.SINH_LUC_MUC_TIEU_TREN;
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			sliderKeThuDungKhiMauMucTieuTren.gameObject.SetActive(true);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.DungKhi != vcThietLap.Parameter.DungKhi)
			{
				sliderKeThuDungKhiMauMucTieuTren.sliderValue = 0f;
				vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent = 0.0;
			}
			else
			{
				sliderKeThuDungKhiMauMucTieuTren.sliderValue = (float)(int)vCThietLapFromHero.Parameter.DungKhi_TieuChi_ThapHonPercent / 100f;
				vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent = vCThietLapFromHero.Parameter.DungKhi_TieuChi_ThapHonPercent;
			}
			sliderKeThuDungKhiMauMucTieuTren.ForceUpdate();
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.DungKhi != vcThietLap.Parameter.DungKhi || (int)vCThietLapFromHero.Parameter.DungKhi_TieuChi_ThapHonPercent != (int)vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
		else
		{
			sliderKeThuDungKhiMauMucTieuTren.gameObject.SetActive(false);
			btnKeThuDungKhiMauMucTieuTren.gameObject.SetActive(false);
		}
	}

	private void OnActivateChieuThucKeThuDungKhiMauBanThanDuoi(bool isActive)
	{
		if (isActive && heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.DungKhi = UserInfo.VCThietLapData.ENUM_DUNG_KHI.SINH_LUC_BAN_THAN_XUONG_THAP;
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			sliderKeThuDungKhiMauBanThanDuoi.gameObject.SetActive(true);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.DungKhi != vcThietLap.Parameter.DungKhi)
			{
				sliderKeThuDungKhiMauBanThanDuoi.sliderValue = 0f;
				vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent = 0.0;
			}
			else
			{
				sliderKeThuDungKhiMauBanThanDuoi.sliderValue = (float)(int)vCThietLapFromHero.Parameter.DungKhi_TieuChi_ThapHonPercent / 100f;
				vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent = vCThietLapFromHero.Parameter.DungKhi_TieuChi_ThapHonPercent;
			}
			sliderKeThuDungKhiMauBanThanDuoi.ForceUpdate();
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.DungKhi != vcThietLap.Parameter.DungKhi || (int)vCThietLapFromHero.Parameter.DungKhi_TieuChi_ThapHonPercent != (int)vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
		else
		{
			sliderKeThuDungKhiMauBanThanDuoi.gameObject.SetActive(false);
			btnKeThuDungKhiMauBanThanDuoi.gameObject.SetActive(false);
		}
	}

	private void OnChieuThucKeThuNgungKhiMauDuoiValueChange(float val)
	{
		if (heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.NgungDungKhi = UserInfo.VCThietLapData.ENUM_NGUNG_DUNG_KHI.SINH_LUC_THAP_HON;
			vcThietLap.Parameter.NgungDungKhi_TieuChi_ThapHonPercent = (int)(val * 100f);
			labelKeThuNgungKhiMauBanThanDuoi.text = string.Format("{0} %", vcThietLap.Parameter.NgungDungKhi_TieuChi_ThapHonPercent);
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.NgungDungKhi != vcThietLap.Parameter.NgungDungKhi || (int)vCThietLapFromHero.Parameter.NgungDungKhi_TieuChi_ThapHonPercent != (int)vcThietLap.Parameter.NgungDungKhi_TieuChi_ThapHonPercent)
			{
				btnKeThuNgungKhiMauBanThanDuoi.SetActive(true);
			}
			else
			{
				btnKeThuNgungKhiMauBanThanDuoi.SetActive(false);
			}
		}
	}

	private void OnChieuThucKeThuNgungKhiNoiLucDuoiValueChange(float val)
	{
		if (heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.NgungDungKhi = UserInfo.VCThietLapData.ENUM_NGUNG_DUNG_KHI.NOI_LUC_THAP_HON;
			vcThietLap.Parameter.NgungDungKhi_TieuChi_ThapHonPercent = (int)(val * 100f);
			labelKeThuNgungKhiNoiLucDuoi.text = string.Format("{0} %", vcThietLap.Parameter.NgungDungKhi_TieuChi_ThapHonPercent);
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.NgungDungKhi != vcThietLap.Parameter.NgungDungKhi || (int)vCThietLapFromHero.Parameter.NgungDungKhi_TieuChi_ThapHonPercent != (int)vcThietLap.Parameter.NgungDungKhi_TieuChi_ThapHonPercent)
			{
				btnKeThuNgungKhiNoiLucDuoi.SetActive(true);
			}
			else
			{
				btnKeThuNgungKhiNoiLucDuoi.SetActive(false);
			}
		}
	}

	private void OnConfirmChieuThucKeThuNgungKhiMauDuoiValue(GameObject go)
	{
		if (heroData != null && vcThietLap != null && vcThietLap.Parameter.NgungDungKhi == UserInfo.VCThietLapData.ENUM_NGUNG_DUNG_KHI.SINH_LUC_THAP_HON)
		{
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.NgungDungKhi != vcThietLap.Parameter.NgungDungKhi || (int)vCThietLapFromHero.Parameter.NgungDungKhi_TieuChi_ThapHonPercent != (int)vcThietLap.Parameter.NgungDungKhi_TieuChi_ThapHonPercent)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
	}

	private void OnConfirmChieuThucKeThuNgungKhiNoiLucDuoiValue(GameObject go)
	{
		if (heroData != null && vcThietLap != null && vcThietLap.Parameter.NgungDungKhi == UserInfo.VCThietLapData.ENUM_NGUNG_DUNG_KHI.NOI_LUC_THAP_HON)
		{
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.NgungDungKhi != vcThietLap.Parameter.NgungDungKhi || (int)vCThietLapFromHero.Parameter.NgungDungKhi_TieuChi_ThapHonPercent != (int)vcThietLap.Parameter.NgungDungKhi_TieuChi_ThapHonPercent)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
	}

	private void OnActivateChieuThucKeThuNgungKhiMauDuoi(bool isActive)
	{
		if (isActive && heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.NgungDungKhi = UserInfo.VCThietLapData.ENUM_NGUNG_DUNG_KHI.SINH_LUC_THAP_HON;
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			sliderKeThuNgungKhiMauBanThanDuoi.gameObject.SetActive(true);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.NgungDungKhi != vcThietLap.Parameter.NgungDungKhi)
			{
				sliderKeThuNgungKhiMauBanThanDuoi.sliderValue = 0f;
				vcThietLap.Parameter.NgungDungKhi_TieuChi_ThapHonPercent = 0.0;
			}
			else
			{
				sliderKeThuNgungKhiMauBanThanDuoi.sliderValue = (float)(int)vCThietLapFromHero.Parameter.NgungDungKhi_TieuChi_ThapHonPercent / 100f;
				vcThietLap.Parameter.NgungDungKhi_TieuChi_ThapHonPercent = vCThietLapFromHero.Parameter.NgungDungKhi_TieuChi_ThapHonPercent;
			}
			sliderKeThuNgungKhiMauBanThanDuoi.ForceUpdate();
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.NgungDungKhi != vcThietLap.Parameter.NgungDungKhi || (int)vCThietLapFromHero.Parameter.NgungDungKhi_TieuChi_ThapHonPercent != (int)vcThietLap.Parameter.NgungDungKhi_TieuChi_ThapHonPercent)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
		else
		{
			sliderKeThuNgungKhiMauBanThanDuoi.gameObject.SetActive(false);
			btnKeThuNgungKhiMauBanThanDuoi.gameObject.SetActive(false);
		}
	}

	private void OnActivateChieuThucKeThuNgungKhiNoiLucDuoi(bool isActive)
	{
		if (isActive && heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.NgungDungKhi = UserInfo.VCThietLapData.ENUM_NGUNG_DUNG_KHI.NOI_LUC_THAP_HON;
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			sliderKeThuNgungKhiNoiLucDuoi.gameObject.SetActive(true);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.NgungDungKhi != vcThietLap.Parameter.NgungDungKhi)
			{
				sliderKeThuNgungKhiNoiLucDuoi.sliderValue = 0f;
				vcThietLap.Parameter.NgungDungKhi_TieuChi_ThapHonPercent = 0.0;
			}
			else
			{
				sliderKeThuNgungKhiNoiLucDuoi.sliderValue = (float)(int)vCThietLapFromHero.Parameter.NgungDungKhi_TieuChi_ThapHonPercent / 100f;
				vcThietLap.Parameter.NgungDungKhi_TieuChi_ThapHonPercent = vCThietLapFromHero.Parameter.NgungDungKhi_TieuChi_ThapHonPercent;
			}
			sliderKeThuNgungKhiNoiLucDuoi.ForceUpdate();
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.NgungDungKhi != vcThietLap.Parameter.NgungDungKhi || (int)vCThietLapFromHero.Parameter.NgungDungKhi_TieuChi_ThapHonPercent != (int)vcThietLap.Parameter.NgungDungKhi_TieuChi_ThapHonPercent)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
		else
		{
			sliderKeThuNgungKhiNoiLucDuoi.gameObject.SetActive(false);
			btnKeThuNgungKhiNoiLucDuoi.gameObject.SetActive(false);
		}
	}

	private void ShowChieuThucDongDoiMucTieu(VCType vcType)
	{
		if (heroData == null || vcThietLap == null)
		{
			return;
		}
		CfgVoCong voCongCfgByType = ConfigManager.instance.GetVoCongCfgByType(vcType);
		if (voCongCfgByType.m_Setting == VCSetting.Alies)
		{
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			dongDoiMucTieu1Avatar.gameObject.SetActive(true);
			dongDoiMucTieu2Avatar.gameObject.SetActive(true);
			dongDoiMucTieu3Avatar.gameObject.SetActive(true);
			dongDoiMucTieu4Avatar.gameObject.SetActive(true);
			UserInfo.HeroData heroFromDoiHinh = userInfo.GetHeroFromDoiHinh(1);
			if (heroFromDoiHinh != null)
			{
				dongDoiMucTieu1Avatar.SetInfo(heroFromDoiHinh.HID, heroFromDoiHinh.Name, heroFromDoiHinh.Level);
				dongDoiMucTieu1Avatar.avatar.IsSelected = vcThietLap.Parameter.DongDoiMucTieuHoTros.Contains(heroFromDoiHinh.HID);
			}
			else
			{
				dongDoiMucTieu1Avatar.SetInfo(0, "emtpy", 0);
			}
			UserInfo.HeroData heroFromDoiHinh2 = userInfo.GetHeroFromDoiHinh(2);
			if (heroFromDoiHinh2 != null)
			{
				dongDoiMucTieu2Avatar.SetInfo(heroFromDoiHinh2.HID, heroFromDoiHinh2.Name, heroFromDoiHinh2.Level);
				dongDoiMucTieu2Avatar.avatar.IsSelected = vcThietLap.Parameter.DongDoiMucTieuHoTros.Contains(heroFromDoiHinh2.HID);
			}
			else
			{
				dongDoiMucTieu2Avatar.SetInfo(0, "emtpy", 0);
			}
			UserInfo.HeroData heroFromDoiHinh3 = userInfo.GetHeroFromDoiHinh(3);
			if (heroFromDoiHinh3 != null)
			{
				dongDoiMucTieu3Avatar.SetInfo(heroFromDoiHinh3.HID, heroFromDoiHinh3.Name, heroFromDoiHinh3.Level);
				dongDoiMucTieu3Avatar.avatar.IsSelected = vcThietLap.Parameter.DongDoiMucTieuHoTros.Contains(heroFromDoiHinh3.HID);
			}
			else
			{
				dongDoiMucTieu3Avatar.SetInfo(0, "emtpy", 0);
			}
			UserInfo.HeroData heroFromDoiHinh4 = userInfo.GetHeroFromDoiHinh(4);
			if (heroFromDoiHinh4 != null)
			{
				dongDoiMucTieu4Avatar.SetInfo(heroFromDoiHinh4.HID, heroFromDoiHinh4.Name, heroFromDoiHinh4.Level);
				dongDoiMucTieu4Avatar.avatar.IsSelected = vcThietLap.Parameter.DongDoiMucTieuHoTros.Contains(heroFromDoiHinh4.HID);
			}
			else
			{
				dongDoiMucTieu4Avatar.SetInfo(0, "emtpy", 0);
			}
		}
		else if (voCongCfgByType.m_Setting == VCSetting.Self)
		{
			dongDoiMucTieu1Avatar.gameObject.SetActive(false);
			dongDoiMucTieu2Avatar.gameObject.SetActive(false);
			dongDoiMucTieu3Avatar.gameObject.SetActive(true);
			dongDoiMucTieu4Avatar.gameObject.SetActive(false);
			dongDoiMucTieu3Avatar.SetInfo(heroData.HID, heroData.Name, heroData.Level);
			OnDongDoiMucTieuClick(dongDoiMucTieu3Avatar.avatar);
			dongDoiMucTieu3Avatar.avatar.IsSelected = true;
		}
	}

	private void ShowChieuThucDongDoiDungKhi(VCType vcType)
	{
		if (heroData == null || vcThietLap == null)
		{
			return;
		}
		switch (vcThietLap.Parameter.DungKhi)
		{
		case UserInfo.VCThietLapData.ENUM_DUNG_KHI.NGAY_KHI_CO_THE:
			if (optionDongDoiDungKhiNgayKhiCoThe.isChecked)
			{
				OnActivateChieuThucDongDoiDungKhiNgayKhiCoThe(true);
			}
			optionDongDoiDungKhiNgayKhiCoThe.isChecked = true;
			break;
		case UserInfo.VCThietLapData.ENUM_DUNG_KHI.SINH_LUC_MUC_TIEU_XUONG_THAP:
			if (optionDongDoiDungKhiMauMucTieuDuoi.isChecked)
			{
				OnActivateChieuThucDongDoiDungKhiMauMucTieuDuoi(true);
			}
			optionDongDoiDungKhiMauMucTieuDuoi.isChecked = true;
			break;
		case UserInfo.VCThietLapData.ENUM_DUNG_KHI.SINH_LUC_MUC_TIEU_TREN:
			if (optionDongDoiDungKhiMauMucTieuTren.isChecked)
			{
				OnActivateChieuThucDongDoiDungKhiMauMucTieuTren(true);
			}
			optionDongDoiDungKhiMauMucTieuTren.isChecked = true;
			break;
		case UserInfo.VCThietLapData.ENUM_DUNG_KHI.SINH_LUC_BAN_THAN_XUONG_THAP:
			if (optionDongDoiDungKhiMauBanThanDuoi.isChecked)
			{
				OnActivateChieuThucDongDoiDungKhiMauBanThanDuoi(true);
			}
			optionDongDoiDungKhiMauBanThanDuoi.isChecked = true;
			break;
		case UserInfo.VCThietLapData.ENUM_DUNG_KHI.NGAY_KHI_BI_TRANG_THAI_XAU:
			if (optionDongDoiDungKhiKhiBiTrangThaiXau.isChecked)
			{
				OnActivateChieuThucDongDoiDungKhiKhiBiTrangThaiXau(true);
			}
			optionDongDoiDungKhiKhiBiTrangThaiXau.isChecked = true;
			break;
		default:
			optionDongDoiDungKhiNgayKhiCoThe.isChecked = true;
			break;
		}
	}

	private void ShowChieuThucDongDoiNgungKhi(VCType vcType)
	{
		if (heroData == null || vcThietLap == null)
		{
			return;
		}
		switch (vcThietLap.Parameter.NgungDungKhi)
		{
		case UserInfo.VCThietLapData.ENUM_NGUNG_DUNG_KHI.SINH_LUC_THAP_HON:
			if (optionDongDoiNgungKhiMauBanThanDuoi.isChecked)
			{
				OnActivateChieuThucDongDoiNgungKhiMauDuoi(true);
			}
			optionDongDoiNgungKhiMauBanThanDuoi.isChecked = true;
			break;
		case UserInfo.VCThietLapData.ENUM_NGUNG_DUNG_KHI.NOI_LUC_THAP_HON:
			if (optionDongDoiNgungKhiNoiLucDuoi.isChecked)
			{
				OnActivateChieuThucDongDoiNgungKhiNoiLucDuoi(true);
			}
			optionDongDoiNgungKhiNoiLucDuoi.isChecked = true;
			break;
		default:
			optionDongDoiNgungKhiMauBanThanDuoi.isChecked = true;
			break;
		}
	}

	private void OnDongDoiMucTieuClick(NhanVatAvatar avatar)
	{
		MucTieuHoTro component = avatar.transform.parent.GetComponent<MucTieuHoTro>();
		if (heroData == null || vcThietLap == null || !(component != null))
		{
			return;
		}
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		CfgVoCong voCongCfgByType = ConfigManager.instance.GetVoCongCfgByType(vcThietLap.Type);
		vcThietLap.Parameter.MucTieuChieu = UserInfo.VCThietLapData.ENUM_MUC_TIEU_CHIEU.HO_TRO;
		UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
		if (voCongCfgByType.m_Setting == VCSetting.Self)
		{
			if (!vcThietLap.Parameter.DongDoiMucTieuHoTros.Contains(heroData.HID))
			{
				vcThietLap.Parameter.DongDoiMucTieuHoTros = new List<int> { heroData.HID };
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
				return;
			}
			bool flag = false;
			for (int i = 0; i < vcThietLap.Parameter.DongDoiMucTieuHoTros.Count; i++)
			{
				if (vcThietLap.Parameter.DongDoiMucTieuHoTros[i] != heroData.HID)
				{
					flag = true;
					vcThietLap.Parameter.DongDoiMucTieuHoTros.RemoveAt(i);
					i--;
				}
			}
			if ((vCThietLapFromHero == null || vCThietLapFromHero.Parameter.MucTieuChieu != vcThietLap.Parameter.MucTieuChieu) | flag)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
		else
		{
			if (voCongCfgByType.m_Setting != VCSetting.Alies)
			{
				return;
			}
			if (avatar.IsSelected)
			{
				if (vcThietLap.Parameter.DongDoiMucTieuHoTros.Contains(component.HID))
				{
					vcThietLap.Parameter.DongDoiMucTieuHoTros.Remove(component.HID);
				}
				avatar.IsSelected = false;
			}
			else
			{
				if (!vcThietLap.Parameter.DongDoiMucTieuHoTros.Contains(component.HID))
				{
					vcThietLap.Parameter.DongDoiMucTieuHoTros.Add(component.HID);
				}
				avatar.IsSelected = true;
			}
			GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
		}
	}

	private void OnChieuThucDongDoiMauMucTieuTrenValueChange(float val)
	{
		if (heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.DungKhi = UserInfo.VCThietLapData.ENUM_DUNG_KHI.SINH_LUC_MUC_TIEU_TREN;
			vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent = (int)(val * 100f);
			labelDongDoiDungKhiMauMucTieuTren.text = string.Format("{0} %", vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent);
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.DungKhi != vcThietLap.Parameter.DungKhi || (int)vCThietLapFromHero.Parameter.DungKhi_TieuChi_ThapHonPercent != (int)vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent)
			{
				btnDongDoiDungKhiMauMucTieuTren.SetActive(true);
			}
			else
			{
				btnDongDoiDungKhiMauMucTieuTren.SetActive(false);
			}
		}
	}

	private void OnChieuThucDongDoiMauMucTieuDuoiValueChange(float val)
	{
		if (heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.DungKhi = UserInfo.VCThietLapData.ENUM_DUNG_KHI.SINH_LUC_MUC_TIEU_XUONG_THAP;
			vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent = (int)(val * 100f);
			labelDongDoiDungKhiMauMucTieuDuoi.text = string.Format("{0} %", vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent);
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.DungKhi != vcThietLap.Parameter.DungKhi || (int)vCThietLapFromHero.Parameter.DungKhi_TieuChi_ThapHonPercent != (int)vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent)
			{
				btnDongDoiDungKhiMauMucTieuDuoi.SetActive(true);
			}
			else
			{
				btnDongDoiDungKhiMauMucTieuDuoi.SetActive(false);
			}
		}
	}

	private void OnChieuThucDongDoiMauBanThanDuoiValueChange(float val)
	{
		if (heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.DungKhi = UserInfo.VCThietLapData.ENUM_DUNG_KHI.SINH_LUC_BAN_THAN_XUONG_THAP;
			vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent = (int)(val * 100f);
			labelDongDoiDungKhiMauBanThanDuoi.text = string.Format("{0} %", vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent);
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.DungKhi != vcThietLap.Parameter.DungKhi || (int)vCThietLapFromHero.Parameter.DungKhi_TieuChi_ThapHonPercent != (int)vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent)
			{
				btnDongDoiDungKhiMauBanThanDuoi.SetActive(true);
			}
			else
			{
				btnDongDoiDungKhiMauBanThanDuoi.SetActive(false);
			}
		}
	}

	private void OnConfirmChieuThucDongDoiMauMucTieuDuoiValue(GameObject go)
	{
		if (heroData != null && vcThietLap != null && vcThietLap.Parameter.DungKhi == UserInfo.VCThietLapData.ENUM_DUNG_KHI.SINH_LUC_MUC_TIEU_XUONG_THAP)
		{
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.DungKhi != vcThietLap.Parameter.DungKhi || (int)vCThietLapFromHero.Parameter.DungKhi_TieuChi_ThapHonPercent != (int)vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
	}

	private void OnConfirmChieuThucDongDoiMauMucTieuTrenValue(GameObject go)
	{
		if (heroData != null && vcThietLap != null && vcThietLap.Parameter.DungKhi == UserInfo.VCThietLapData.ENUM_DUNG_KHI.SINH_LUC_MUC_TIEU_TREN)
		{
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.DungKhi != vcThietLap.Parameter.DungKhi || (int)vCThietLapFromHero.Parameter.DungKhi_TieuChi_ThapHonPercent != (int)vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
	}

	private void OnConfirmChieuThucDongDoiMauBanThanDuoiValue(GameObject go)
	{
		if (heroData != null && vcThietLap != null && vcThietLap.Parameter.DungKhi == UserInfo.VCThietLapData.ENUM_DUNG_KHI.SINH_LUC_BAN_THAN_XUONG_THAP)
		{
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.DungKhi != vcThietLap.Parameter.DungKhi || (int)vCThietLapFromHero.Parameter.DungKhi_TieuChi_ThapHonPercent != (int)vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
	}

	private void OnActivateChieuThucDongDoiDungKhiNgayKhiCoThe(bool isActive)
	{
		if (isActive && heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.DungKhi = UserInfo.VCThietLapData.ENUM_DUNG_KHI.NGAY_KHI_CO_THE;
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.DungKhi != vcThietLap.Parameter.DungKhi)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
	}

	private void OnActivateChieuThucDongDoiDungKhiKhiBiTrangThaiXau(bool isActive)
	{
		if (isActive && heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.DungKhi = UserInfo.VCThietLapData.ENUM_DUNG_KHI.NGAY_KHI_BI_TRANG_THAI_XAU;
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.DungKhi != vcThietLap.Parameter.DungKhi)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
	}

	private void OnActivateChieuThucDongDoiDungKhiMauMucTieuDuoi(bool isActive)
	{
		if (isActive && heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.DungKhi = UserInfo.VCThietLapData.ENUM_DUNG_KHI.SINH_LUC_MUC_TIEU_XUONG_THAP;
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			sliderDongDoiDungKhiMauMucTieuDuoi.gameObject.SetActive(true);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.DungKhi != vcThietLap.Parameter.DungKhi)
			{
				sliderDongDoiDungKhiMauMucTieuDuoi.sliderValue = 0f;
				vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent = 0.0;
			}
			else
			{
				sliderDongDoiDungKhiMauMucTieuDuoi.sliderValue = (float)(int)vCThietLapFromHero.Parameter.DungKhi_TieuChi_ThapHonPercent / 100f;
				vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent = vCThietLapFromHero.Parameter.DungKhi_TieuChi_ThapHonPercent;
			}
			sliderDongDoiDungKhiMauMucTieuDuoi.ForceUpdate();
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.DungKhi != vcThietLap.Parameter.DungKhi || (int)vCThietLapFromHero.Parameter.DungKhi_TieuChi_ThapHonPercent != (int)vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
		else
		{
			sliderDongDoiDungKhiMauMucTieuDuoi.gameObject.SetActive(false);
			btnDongDoiDungKhiMauMucTieuDuoi.gameObject.SetActive(false);
		}
	}

	private void OnActivateChieuThucDongDoiDungKhiMauMucTieuTren(bool isActive)
	{
		if (isActive && heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.DungKhi = UserInfo.VCThietLapData.ENUM_DUNG_KHI.SINH_LUC_MUC_TIEU_TREN;
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			sliderDongDoiDungKhiMauMucTieuTren.gameObject.SetActive(true);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.DungKhi != vcThietLap.Parameter.DungKhi)
			{
				sliderDongDoiDungKhiMauMucTieuTren.sliderValue = 0f;
				vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent = 0.0;
			}
			else
			{
				sliderDongDoiDungKhiMauMucTieuTren.sliderValue = (float)(int)vCThietLapFromHero.Parameter.DungKhi_TieuChi_ThapHonPercent / 100f;
				vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent = vCThietLapFromHero.Parameter.DungKhi_TieuChi_ThapHonPercent;
			}
			sliderDongDoiDungKhiMauMucTieuTren.ForceUpdate();
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.DungKhi != vcThietLap.Parameter.DungKhi || (int)vCThietLapFromHero.Parameter.DungKhi_TieuChi_ThapHonPercent != (int)vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
		else
		{
			sliderDongDoiDungKhiMauMucTieuTren.gameObject.SetActive(false);
			btnDongDoiDungKhiMauMucTieuTren.gameObject.SetActive(false);
		}
	}

	private void OnActivateChieuThucDongDoiDungKhiMauBanThanDuoi(bool isActive)
	{
		if (isActive && heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.DungKhi = UserInfo.VCThietLapData.ENUM_DUNG_KHI.SINH_LUC_BAN_THAN_XUONG_THAP;
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			sliderDongDoiDungKhiMauBanThanDuoi.gameObject.SetActive(true);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.DungKhi != vcThietLap.Parameter.DungKhi)
			{
				sliderDongDoiDungKhiMauBanThanDuoi.sliderValue = 0f;
				vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent = 0.0;
			}
			else
			{
				sliderDongDoiDungKhiMauBanThanDuoi.sliderValue = (float)(int)vCThietLapFromHero.Parameter.DungKhi_TieuChi_ThapHonPercent / 100f;
				vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent = vCThietLapFromHero.Parameter.DungKhi_TieuChi_ThapHonPercent;
			}
			sliderDongDoiDungKhiMauBanThanDuoi.ForceUpdate();
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.DungKhi != vcThietLap.Parameter.DungKhi || (int)vCThietLapFromHero.Parameter.DungKhi_TieuChi_ThapHonPercent != (int)vcThietLap.Parameter.DungKhi_TieuChi_ThapHonPercent)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
		else
		{
			sliderDongDoiDungKhiMauBanThanDuoi.gameObject.SetActive(false);
			btnDongDoiDungKhiMauBanThanDuoi.gameObject.SetActive(false);
		}
	}

	private void OnChieuThucDongDoiNgungKhiMauDuoiValueChange(float val)
	{
		if (heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.NgungDungKhi = UserInfo.VCThietLapData.ENUM_NGUNG_DUNG_KHI.SINH_LUC_THAP_HON;
			vcThietLap.Parameter.NgungDungKhi_TieuChi_ThapHonPercent = (int)(val * 100f);
			labelDongDoiNgungKhiMauBanThanDuoi.text = string.Format("{0} %", vcThietLap.Parameter.NgungDungKhi_TieuChi_ThapHonPercent);
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.NgungDungKhi != vcThietLap.Parameter.NgungDungKhi || (int)vCThietLapFromHero.Parameter.NgungDungKhi_TieuChi_ThapHonPercent != (int)vcThietLap.Parameter.NgungDungKhi_TieuChi_ThapHonPercent)
			{
				btnDongDoiNgungKhiMauBanThanDuoi.SetActive(true);
			}
			else
			{
				btnDongDoiNgungKhiMauBanThanDuoi.SetActive(false);
			}
		}
	}

	private void OnChieuThucDongDoiNgungKhiNoiLucDuoiValueChange(float val)
	{
		if (heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.NgungDungKhi = UserInfo.VCThietLapData.ENUM_NGUNG_DUNG_KHI.NOI_LUC_THAP_HON;
			vcThietLap.Parameter.NgungDungKhi_TieuChi_ThapHonPercent = (int)(val * 100f);
			labelDongDoiNgungKhiNoiLucDuoi.text = string.Format("{0} %", vcThietLap.Parameter.NgungDungKhi_TieuChi_ThapHonPercent);
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.NgungDungKhi != vcThietLap.Parameter.NgungDungKhi || (int)vCThietLapFromHero.Parameter.NgungDungKhi_TieuChi_ThapHonPercent != (int)vcThietLap.Parameter.NgungDungKhi_TieuChi_ThapHonPercent)
			{
				btnDongDoiNgungKhiNoiLucDuoi.SetActive(true);
			}
			else
			{
				btnDongDoiNgungKhiNoiLucDuoi.SetActive(false);
			}
		}
	}

	private void OnConfirmChieuThucDongDoiNgungKhiMauDuoiValue(GameObject go)
	{
		if (heroData != null && vcThietLap != null && vcThietLap.Parameter.NgungDungKhi == UserInfo.VCThietLapData.ENUM_NGUNG_DUNG_KHI.SINH_LUC_THAP_HON)
		{
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.NgungDungKhi != vcThietLap.Parameter.NgungDungKhi || (int)vCThietLapFromHero.Parameter.NgungDungKhi_TieuChi_ThapHonPercent != (int)vcThietLap.Parameter.NgungDungKhi_TieuChi_ThapHonPercent)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
	}

	private void OnConfirmChieuThucDongDoiNgungKhiNoiLucDuoiValue(GameObject go)
	{
		if (heroData != null && vcThietLap != null && vcThietLap.Parameter.NgungDungKhi == UserInfo.VCThietLapData.ENUM_NGUNG_DUNG_KHI.NOI_LUC_THAP_HON)
		{
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.NgungDungKhi != vcThietLap.Parameter.NgungDungKhi || (int)vCThietLapFromHero.Parameter.NgungDungKhi_TieuChi_ThapHonPercent != (int)vcThietLap.Parameter.NgungDungKhi_TieuChi_ThapHonPercent)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
	}

	private void OnActivateChieuThucDongDoiNgungKhiMauDuoi(bool isActive)
	{
		if (isActive && heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.NgungDungKhi = UserInfo.VCThietLapData.ENUM_NGUNG_DUNG_KHI.SINH_LUC_THAP_HON;
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			sliderDongDoiNgungKhiMauBanThanDuoi.gameObject.SetActive(true);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.NgungDungKhi != vcThietLap.Parameter.NgungDungKhi)
			{
				sliderDongDoiNgungKhiMauBanThanDuoi.sliderValue = 0f;
				vcThietLap.Parameter.NgungDungKhi_TieuChi_ThapHonPercent = 0.0;
			}
			else
			{
				sliderDongDoiNgungKhiMauBanThanDuoi.sliderValue = (float)(int)vCThietLapFromHero.Parameter.NgungDungKhi_TieuChi_ThapHonPercent / 100f;
				vcThietLap.Parameter.NgungDungKhi_TieuChi_ThapHonPercent = vCThietLapFromHero.Parameter.NgungDungKhi_TieuChi_ThapHonPercent;
			}
			sliderDongDoiNgungKhiMauBanThanDuoi.ForceUpdate();
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.NgungDungKhi != vcThietLap.Parameter.NgungDungKhi || (int)vCThietLapFromHero.Parameter.NgungDungKhi_TieuChi_ThapHonPercent != (int)vcThietLap.Parameter.NgungDungKhi_TieuChi_ThapHonPercent)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
		else
		{
			sliderDongDoiNgungKhiMauBanThanDuoi.gameObject.SetActive(false);
			btnDongDoiNgungKhiMauBanThanDuoi.gameObject.SetActive(false);
		}
	}

	private void OnActivateChieuThucDongDoiNgungKhiNoiLucDuoi(bool isActive)
	{
		if (isActive && heroData != null && vcThietLap != null)
		{
			vcThietLap.Parameter.NgungDungKhi = UserInfo.VCThietLapData.ENUM_NGUNG_DUNG_KHI.NOI_LUC_THAP_HON;
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.VCThietLapData vCThietLapFromHero = userInfo.GetVCThietLapFromHero(vcThietLap.HID, vcThietLap.Type);
			sliderDongDoiNgungKhiNoiLucDuoi.gameObject.SetActive(true);
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.NgungDungKhi != vcThietLap.Parameter.NgungDungKhi)
			{
				sliderDongDoiNgungKhiNoiLucDuoi.sliderValue = 0f;
				vcThietLap.Parameter.NgungDungKhi_TieuChi_ThapHonPercent = 0.0;
			}
			else
			{
				sliderDongDoiNgungKhiNoiLucDuoi.sliderValue = (float)(int)vCThietLapFromHero.Parameter.NgungDungKhi_TieuChi_ThapHonPercent / 100f;
				vcThietLap.Parameter.NgungDungKhi_TieuChi_ThapHonPercent = vCThietLapFromHero.Parameter.NgungDungKhi_TieuChi_ThapHonPercent;
			}
			sliderDongDoiNgungKhiNoiLucDuoi.ForceUpdate();
			if (vCThietLapFromHero == null || vCThietLapFromHero.Parameter.NgungDungKhi != vcThietLap.Parameter.NgungDungKhi || (int)vCThietLapFromHero.Parameter.NgungDungKhi_TieuChi_ThapHonPercent != (int)vcThietLap.Parameter.NgungDungKhi_TieuChi_ThapHonPercent)
			{
				GameManager.instance.m_GameClient.SetVCSetting(vcThietLap);
			}
		}
		else
		{
			sliderDongDoiNgungKhiNoiLucDuoi.gameObject.SetActive(false);
			btnDongDoiNgungKhiNoiLucDuoi.gameObject.SetActive(false);
		}
	}

	private void OnBaoVeDongDoiSelected(bool isActive, GameObject go)
	{
		if (!isActive || !(go != null) || heroData == null)
		{
			return;
		}
		MucTieuBaoVe component = go.GetComponent<MucTieuBaoVe>();
		if (component != null)
		{
			if (heroData.TrangThai == null)
			{
				heroData.TrangThai = new UserInfo.HeroData.AI();
			}
			heroData.TrangThai.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.BAO_VE;
			heroData.TrangThai.BaoVeDongDoi = component.HID;
			UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
			if (heroFromDoiHinh.TrangThai.ChienThuat != heroData.TrangThai.ChienThuat || heroFromDoiHinh.TrangThai.BaoVeDongDoi != heroData.TrangThai.BaoVeDongDoi)
			{
				GameManager.instance.m_GameClient.SetHeroData(heroData);
			}
		}
	}

	private void OnBaoVeDongDoiStateChange(bool isActive)
	{
		if (isActive)
		{
			PanelBaoVeBiDong.SetActive(true);
			ShowBaoVeBiDongPanel();
		}
		else
		{
			PanelBaoVeBiDong.SetActive(false);
		}
	}

	private void OnBiDongDisabledClick()
	{
		int index = ConfigManager.GiangHoUnlockChienThuatBiDong();
		GiangHoCfg giangHoCfg = ConfigManager.instance.m_listGiangHo[index];
		MessagePopup.Create(string.Format(Localization.instance.Get("ChienThuatBiDongLockedLabel"), giangHoCfg.TenHienThi));
	}

	private void OnBiDongStateChange(bool isActive)
	{
		if (isActive)
		{
			if (SelectedSlotNhanVat < 1)
			{
				MessagePopup.Create(Localization.instance.Get("ChienThuatChonNhanVat"));
				return;
			}
			PanelTanCongBiDong.SetActive(true);
			ShowTanCongBiDongPanel();
		}
		else
		{
			PanelTanCongBiDong.SetActive(false);
		}
	}

	private void ShowChuDongPanel()
	{
		if (heroData != null && heroData.TrangThai != null)
		{
			if (heroData.TrangThai.ChienThuat == UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_GAN_NHAT)
			{
				if (optionChuDongCaoCap.isChecked)
				{
					OnActivateCaoCapChuDong(true);
				}
				optionChuDongCaoCap.isChecked = true;
				if (optionChuDongGanNhat.isChecked)
				{
					OnActivateTrangThaiChuDongGanNhat(true);
				}
				optionChuDongGanNhat.isChecked = true;
			}
			else if (heroData.TrangThai.ChienThuat == UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_SINH_LUC_MAX)
			{
				if (optionChuDongCaoCap.isChecked)
				{
					OnActivateCaoCapChuDong(true);
				}
				optionChuDongCaoCap.isChecked = true;
				if (optionChuDongMauNhieuNhat.isChecked)
				{
					OnActivateTrangThaiChuDongMauNhieuNhat(true);
				}
				optionChuDongMauNhieuNhat.isChecked = true;
			}
			else if (heroData.TrangThai.ChienThuat == UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_SINH_LUC_MIN)
			{
				if (optionChuDongCaoCap.isChecked)
				{
					OnActivateCaoCapChuDong(true);
				}
				optionChuDongCaoCap.isChecked = true;
				if (optionChuDongMauItNhat.isChecked)
				{
					OnActivateTrangThaiChuDongMauItNhat(true);
				}
				optionChuDongMauItNhat.isChecked = true;
			}
			else if (heroData.TrangThai.ChienThuat == UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_THAN_PHAP_MAX)
			{
				if (optionChuDongCaoCap.isChecked)
				{
					OnActivateCaoCapChuDong(true);
				}
				optionChuDongCaoCap.isChecked = true;
				if (optionChuDongThanPhapCaoNhat.isChecked)
				{
					OnActivateTrangThaiChuDongThanPhapCaoNhat(true);
				}
				optionChuDongThanPhapCaoNhat.isChecked = true;
			}
			else if (heroData.TrangThai.ChienThuat == UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_THAN_PHAP_MIN)
			{
				if (optionChuDongCaoCap.isChecked)
				{
					OnActivateCaoCapChuDong(true);
				}
				optionChuDongCaoCap.isChecked = true;
				if (optionChuDongThanPhapThapNhat.isChecked)
				{
					OnActivateTrangThaiChuDongThanPhapThapNhat(true);
				}
				optionChuDongThanPhapThapNhat.isChecked = true;
			}
			else if (heroData.TrangThai.ChienThuat == UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_CONG_MAX)
			{
				if (optionChuDongCaoCap.isChecked)
				{
					OnActivateCaoCapChuDong(true);
				}
				optionChuDongCaoCap.isChecked = true;
				if (optionChuDongCongToNhat.isChecked)
				{
					OnActivateTrangThaiChuDongCongCaoNhat(true);
				}
				optionChuDongCongToNhat.isChecked = true;
			}
			else if (heroData.TrangThai.ChienThuat == UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_CONG_MIN)
			{
				if (optionChuDongCaoCap.isChecked)
				{
					OnActivateCaoCapChuDong(true);
				}
				optionChuDongCaoCap.isChecked = true;
				if (optionChuDongCongNhoNhat.isChecked)
				{
					OnActivateTrangThaiChuDongCongThapNhat(true);
				}
				optionChuDongCongNhoNhat.isChecked = true;
			}
			else if (heroData.TrangThai.ChienThuat == UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_NOI_MAX)
			{
				if (optionChuDongCaoCap.isChecked)
				{
					OnActivateCaoCapChuDong(true);
				}
				optionChuDongCaoCap.isChecked = true;
				if (optionChuDongKhiLonNhat.isChecked)
				{
					OnActivateTrangThaiChuDongKhiCaoNhat(true);
				}
				optionChuDongKhiLonNhat.isChecked = true;
			}
			else if (heroData.TrangThai.ChienThuat == UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_NOI_MIN)
			{
				if (optionChuDongCaoCap.isChecked)
				{
					OnActivateCaoCapChuDong(true);
				}
				optionChuDongCaoCap.isChecked = true;
				if (optionChuDongKhiNhoNhat.isChecked)
				{
					OnActivateTrangThaiChuDongKhiThapNhat(true);
				}
				optionChuDongKhiNhoNhat.isChecked = true;
			}
			else if (heroData.TrangThai.ChienThuat == UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_SLOT)
			{
				if (!optionChuDongCaoCap.isChecked)
				{
					OnActivateCaoCapChuDong(false);
				}
				optionChuDongCaoCap.isChecked = false;
				if (optionChuDongSlot.isChecked)
				{
					OnActivateTrangThaiChuDongSlot(true);
				}
				optionChuDongSlot.isChecked = true;
			}
			else
			{
				if (!optionChuDongCaoCap.isChecked)
				{
					OnActivateCaoCapChuDong(false);
				}
				optionChuDongCaoCap.isChecked = false;
				if (optionChuDongGanNhat.isChecked)
				{
					OnActivateTrangThaiChuDongGanNhat(true);
				}
				optionChuDongGanNhat.isChecked = true;
			}
		}
		else
		{
			if (optionChuDongCaoCap.isChecked)
			{
				OnActivateCaoCapChuDong(true);
			}
			optionChuDongCaoCap.isChecked = true;
			if (optionChuDongGanNhat.isChecked)
			{
				OnActivateTrangThaiChuDongGanNhat(true);
			}
			optionChuDongGanNhat.isChecked = true;
			optionChuDongMauNhieuNhat.isChecked = false;
			optionChuDongMauItNhat.isChecked = false;
			optionChuDongThanPhapCaoNhat.isChecked = false;
			optionChuDongThanPhapThapNhat.isChecked = false;
			optionChuDongCongToNhat.isChecked = false;
			optionChuDongCongNhoNhat.isChecked = false;
			optionChuDongKhiLonNhat.isChecked = false;
			optionChuDongKhiNhoNhat.isChecked = false;
		}
	}

	private void ShowBaoVeBiDongPanel()
	{
		if (heroData == null)
		{
			return;
		}
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		int num = 0;
		for (int i = 0; i < userInfo.DoiHinh.ListRaTran.Count; i++)
		{
			if (userInfo.DoiHinh.ListRaTran[i] == heroData.HID)
			{
				continue;
			}
			UserInfo.HeroData heroFromDoiHinh = userInfo.GetHeroFromDoiHinh(i + 1);
			if (heroFromDoiHinh == null || num >= targets.Length)
			{
				continue;
			}
			MucTieuBaoVe mucTieuBaoVe = targets[num];
			mucTieuBaoVe.SetInfo(heroFromDoiHinh.HID, heroFromDoiHinh.Name, heroFromDoiHinh.Level);
			if (heroData.TrangThai != null && heroData.TrangThai.BaoVeDongDoi == heroFromDoiHinh.HID)
			{
				mucTieuBaoVe.BeSelected(true);
				if (mucTieuBaoVe.OnStateChange != null)
				{
					mucTieuBaoVe.OnStateChange(true, mucTieuBaoVe.gameObject);
				}
			}
			else
			{
				mucTieuBaoVe.BeSelected(false);
				if (mucTieuBaoVe.OnStateChange != null)
				{
					mucTieuBaoVe.OnStateChange(false, mucTieuBaoVe.gameObject);
				}
			}
			num++;
		}
	}

	private void ShowTanCongBiDongPanel()
	{
		if (heroData != null && heroData.TrangThai != null)
		{
			if (heroData.TrangThai.ChienThuat == UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_GAN_NHAT)
			{
				if (optionBiDongCaoCap.isChecked)
				{
					OnActivateCaoCapBiDong(true);
				}
				optionBiDongCaoCap.isChecked = true;
				if (optionBiDongGanNhat.isChecked)
				{
					OnActivateTrangThaiBiDongGanNhat(true);
				}
				optionBiDongGanNhat.isChecked = true;
			}
			else if (heroData.TrangThai.ChienThuat == UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_SINH_LUC_MAX)
			{
				if (optionBiDongCaoCap.isChecked)
				{
					OnActivateCaoCapBiDong(true);
				}
				optionBiDongCaoCap.isChecked = true;
				if (optionBiDongMauNhieuNhat.isChecked)
				{
					OnActivateTrangThaiBiDongMauNhieuNhat(true);
				}
				optionBiDongMauNhieuNhat.isChecked = true;
			}
			else if (heroData.TrangThai.ChienThuat == UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_SINH_LUC_MIN)
			{
				if (optionBiDongCaoCap.isChecked)
				{
					OnActivateCaoCapBiDong(true);
				}
				optionBiDongCaoCap.isChecked = true;
				if (optionBiDongMauItNhat.isChecked)
				{
					OnActivateTrangThaiBiDongMauItNhat(true);
				}
				optionBiDongMauItNhat.isChecked = true;
			}
			else if (heroData.TrangThai.ChienThuat == UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_THAN_PHAP_MAX)
			{
				if (optionBiDongCaoCap.isChecked)
				{
					OnActivateCaoCapBiDong(true);
				}
				optionBiDongCaoCap.isChecked = true;
				if (optionBiDongThanPhapCaoNhat.isChecked)
				{
					OnActivateTrangThaiBiDongThanPhapCaoNhat(true);
				}
				optionBiDongThanPhapCaoNhat.isChecked = true;
			}
			else if (heroData.TrangThai.ChienThuat == UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_THAN_PHAP_MIN)
			{
				if (optionBiDongCaoCap.isChecked)
				{
					OnActivateCaoCapBiDong(true);
				}
				optionBiDongCaoCap.isChecked = true;
				if (optionBiDongThanPhapThapNhat.isChecked)
				{
					OnActivateTrangThaiBiDongThanPhapThapNhat(true);
				}
				optionBiDongThanPhapThapNhat.isChecked = true;
			}
			else if (heroData.TrangThai.ChienThuat == UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_CONG_MAX)
			{
				if (optionBiDongCaoCap.isChecked)
				{
					OnActivateCaoCapBiDong(true);
				}
				optionBiDongCaoCap.isChecked = true;
				if (optionBiDongCongToNhat.isChecked)
				{
					OnActivateTrangThaiBiDongCongCaoNhat(true);
				}
				optionBiDongCongToNhat.isChecked = true;
			}
			else if (heroData.TrangThai.ChienThuat == UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_CONG_MIN)
			{
				if (optionBiDongCaoCap.isChecked)
				{
					OnActivateCaoCapBiDong(true);
				}
				optionBiDongCaoCap.isChecked = true;
				if (optionBiDongCongNhoNhat.isChecked)
				{
					OnActivateTrangThaiBiDongCongThapNhat(true);
				}
				optionBiDongCongNhoNhat.isChecked = true;
			}
			else if (heroData.TrangThai.ChienThuat == UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_NOI_MAX)
			{
				if (optionBiDongCaoCap.isChecked)
				{
					OnActivateCaoCapBiDong(true);
				}
				optionBiDongCaoCap.isChecked = true;
				if (optionBiDongKhiLonNhat.isChecked)
				{
					OnActivateTrangThaiBiDongKhiCaoNhat(true);
				}
				optionBiDongKhiLonNhat.isChecked = true;
			}
			else if (heroData.TrangThai.ChienThuat == UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_NOI_MIN)
			{
				if (optionBiDongCaoCap.isChecked)
				{
					OnActivateCaoCapBiDong(true);
				}
				optionBiDongCaoCap.isChecked = true;
				if (optionBiDongKhiNhoNhat.isChecked)
				{
					OnActivateTrangThaiBiDongKhiThapNhat(true);
				}
				optionBiDongKhiNhoNhat.isChecked = true;
			}
			else if (heroData.TrangThai.ChienThuat == UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_SLOT)
			{
				if (!optionBiDongCaoCap.isChecked)
				{
					OnActivateCaoCapBiDong(false);
				}
				optionBiDongCaoCap.isChecked = false;
				if (optionBiDongSlot.isChecked)
				{
					OnActivateTrangThaiBiDongSlot(true);
				}
				optionBiDongSlot.isChecked = true;
			}
			else
			{
				if (!optionBiDongCaoCap.isChecked)
				{
					OnActivateCaoCapBiDong(false);
				}
				optionBiDongCaoCap.isChecked = false;
				if (optionBiDongGanNhat.isChecked)
				{
					OnActivateTrangThaiBiDongGanNhat(true);
				}
				optionBiDongGanNhat.isChecked = true;
			}
		}
		else
		{
			if (optionBiDongCaoCap.isChecked)
			{
				OnActivateCaoCapBiDong(true);
			}
			optionBiDongCaoCap.isChecked = true;
			if (optionBiDongGanNhat.isChecked)
			{
				OnActivateTrangThaiBiDongGanNhat(true);
			}
			optionBiDongGanNhat.isChecked = true;
			optionBiDongMauNhieuNhat.isChecked = false;
			optionBiDongMauItNhat.isChecked = false;
			optionBiDongThanPhapCaoNhat.isChecked = false;
			optionBiDongThanPhapThapNhat.isChecked = false;
			optionBiDongCongToNhat.isChecked = false;
			optionBiDongCongNhoNhat.isChecked = false;
			optionBiDongKhiLonNhat.isChecked = false;
			optionBiDongKhiNhoNhat.isChecked = false;
		}
	}

	private void OnChuDongStateChange(bool isActive)
	{
		if (isActive)
		{
			if (SelectedSlotNhanVat < 1)
			{
				MessagePopup.Create(Localization.instance.Get("ChienThuatChonNhanVat"));
				return;
			}
			PanelChuDong.SetActive(isActive);
			ShowChuDongPanel();
		}
		else
		{
			PanelChuDong.SetActive(isActive);
		}
	}

	private void OnAvatarClick(NhanVatAvatar go)
	{
		if (!go.IsSelected && !go.IsEmpty())
		{
			avatar1.IsSelected = ((go == avatar1) ? true : false);
			avatar2.IsSelected = ((go == avatar2) ? true : false);
			avatar3.IsSelected = ((go == avatar3) ? true : false);
			avatar4.IsSelected = ((go == avatar4) ? true : false);
			avatar5.IsSelected = ((go == avatar5) ? true : false);
			avatar6.IsSelected = ((go == avatar6) ? true : false);
			avatar7.IsSelected = ((go == avatar7) ? true : false);
			avatar8.IsSelected = ((go == avatar8) ? true : false);
			if (go == avatar1)
			{
				SelectedSlotNhanVat = 1;
			}
			else if (go == avatar2)
			{
				SelectedSlotNhanVat = 2;
			}
			else if (go == avatar3)
			{
				SelectedSlotNhanVat = 3;
			}
			else if (go == avatar4)
			{
				SelectedSlotNhanVat = 4;
			}
			else if (go == avatar5)
			{
				SelectedSlotNhanVat = 5;
			}
			else if (go == avatar6)
			{
				SelectedSlotNhanVat = 6;
			}
			else if (go == avatar7)
			{
				SelectedSlotNhanVat = 7;
			}
			else if (go == avatar8)
			{
				SelectedSlotNhanVat = 8;
			}
			else
			{
				SelectedSlotNhanVat = 0;
			}
			UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
			if (PanelTrangThai.gameObject.activeInHierarchy && heroFromDoiHinh != null)
			{
				heroData = new UserInfo.HeroData(heroFromDoiHinh);
				ShowTrangThai(heroData);
			}
			if (PanelChieuThuc.gameObject.activeInHierarchy && heroFromDoiHinh != null)
			{
				heroData = new UserInfo.HeroData(heroFromDoiHinh);
				ShowChieuThuc(heroData);
			}
		}
	}

	private void OnActivateChieuThuc(bool isActive)
	{
		PanelChieuThuc.SetActive(isActive);
		if (isActive)
		{
			if (heroData != null)
			{
				ShowChieuThuc(heroData);
				return;
			}
			UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
			if (heroFromDoiHinh != null)
			{
				heroData = new UserInfo.HeroData(heroFromDoiHinh);
				ShowChieuThuc(heroData);
			}
		}
		else
		{
			vcThietLap = null;
		}
	}

	private void OnActivateTrangThai(bool isActive)
	{
		PanelTrangThai.SetActive(isActive);
		if (!isActive)
		{
			return;
		}
		if (heroData != null)
		{
			ShowTrangThai(heroData);
			return;
		}
		UserInfo.HeroData heroFromDoiHinh = GameManager.instance.m_GameClient.UserInfo.GetHeroFromDoiHinh(SelectedSlotNhanVat);
		if (heroFromDoiHinh != null)
		{
			heroData = new UserInfo.HeroData(heroFromDoiHinh);
			ShowTrangThai(heroData);
		}
	}

	private void ShowTrangThai(UserInfo.HeroData heroData)
	{
		if (heroData != null && heroData.TrangThai != null)
		{
			if (heroData.TrangThai.TanCongSlotList.Count == 0)
			{
				heroData.TrangThai.TanCongSlotList = new List<int> { 0, 1, 2, 3 };
			}
			if (optionBaoVe.enabled)
			{
				if (optionBaoVe.isChecked && heroData.TrangThai.IsBaoVe())
				{
					OnBaoVeDongDoiStateChange(true);
				}
				optionBaoVe.isChecked = heroData.TrangThai.IsBaoVe();
			}
			else if (heroData.TrangThai.IsBaoVe())
			{
				optionChuDong.isChecked = true;
			}
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			int num = ConfigManager.GiangHoUnlockChienThuatBiDong();
			if (userInfo.GiangHo == null || userInfo.GiangHo.Count < num + 1 || userInfo.GiangHo[num].HoanThanh < 1)
			{
				optionBiDong.gameObject.SetActive(false);
				btnDiDongDisabled.gameObject.SetActive(true);
			}
			else
			{
				optionBiDong.gameObject.SetActive(true);
				btnDiDongDisabled.gameObject.SetActive(false);
				if (optionBiDong.isChecked && heroData.TrangThai.IsBiDong())
				{
					OnBiDongStateChange(true);
				}
				optionBiDong.isChecked = heroData.TrangThai.IsBiDong();
			}
			if (optionChuDong.isChecked && heroData.TrangThai.IsChuDong())
			{
				OnChuDongStateChange(true);
			}
			optionChuDong.isChecked = heroData.TrangThai.IsChuDong();
		}
		else
		{
			optionChuDong.isChecked = false;
			optionBiDong.isChecked = false;
			optionBaoVe.isChecked = false;
		}
	}

	private void ShowChieuThuc(UserInfo.HeroData heroData)
	{
		if (heroData != null)
		{
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			List<UserInfo.VoCongData> chieuThucFromHero = userInfo.GetChieuThucFromHero(heroData);
			for (int i = 0; i < chieuThucAvatars.Length; i++)
			{
				if (!(chieuThucAvatars[i] != null))
				{
					continue;
				}
				if (chieuThucFromHero.Count > i)
				{
					UserInfo.VoCongData voCongData = chieuThucFromHero[i];
					chieuThucAvatars[i].Set(voCongData.Name, -1, voCongData.Level);
					chieuThucAvatars[i].IsSelected = false;
					if (selectedChieuThuc == i)
					{
						OnChieuThucClick(chieuThucAvatars[i]);
					}
				}
				else
				{
					chieuThucAvatars[i].Set("empty");
				}
			}
		}
		else
		{
			MucTieuChk.isChecked = false;
			DungKhiChk.isChecked = false;
			NgungKhiChk.isChecked = false;
			for (int j = 0; j < chieuThucAvatars.Length; j++)
			{
				chieuThucAvatars[j].IsSelected = false;
			}
		}
	}

	public void SyncWithNetworkData()
	{
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		int numOpenDoiHinhSlotByLevel = ConfigManager.instance.GetNumOpenDoiHinhSlotByLevel(userInfo.Gamer.Level);
		if (numOpenDoiHinhSlotByLevel > 0)
		{
			avatar1.gameObject.SetActive(true);
			avatar1.Set(userInfo.GetHeroFromDoiHinh(1));
		}
		else
		{
			avatar1.gameObject.SetActive(false);
		}
		if (numOpenDoiHinhSlotByLevel > 1)
		{
			avatar2.gameObject.SetActive(true);
			avatar2.Set(userInfo.GetHeroFromDoiHinh(2));
		}
		else
		{
			avatar2.gameObject.SetActive(false);
		}
		if (numOpenDoiHinhSlotByLevel > 2)
		{
			avatar3.gameObject.SetActive(true);
			avatar3.Set(userInfo.GetHeroFromDoiHinh(3));
		}
		else
		{
			avatar3.gameObject.SetActive(false);
		}
		if (numOpenDoiHinhSlotByLevel > 3)
		{
			avatar4.gameObject.SetActive(true);
			avatar4.Set(userInfo.GetHeroFromDoiHinh(4));
		}
		else
		{
			avatar4.gameObject.SetActive(false);
		}
		if (numOpenDoiHinhSlotByLevel > 4)
		{
			avatar5.gameObject.SetActive(true);
			avatar5.Set(userInfo.GetHeroFromDoiHinh(5));
		}
		else
		{
			avatar5.gameObject.SetActive(false);
		}
		if (numOpenDoiHinhSlotByLevel > 5)
		{
			avatar6.gameObject.SetActive(true);
			avatar6.Set(userInfo.GetHeroFromDoiHinh(6));
		}
		else
		{
			avatar6.gameObject.SetActive(false);
		}
		if (numOpenDoiHinhSlotByLevel > 6)
		{
			avatar7.gameObject.SetActive(true);
			avatar7.Set(userInfo.GetHeroFromDoiHinh(7));
		}
		else
		{
			avatar7.gameObject.SetActive(false);
		}
		if (numOpenDoiHinhSlotByLevel > 7)
		{
			avatar8.gameObject.SetActive(true);
			avatar8.Set(userInfo.GetHeroFromDoiHinh(8));
		}
		else
		{
			avatar8.gameObject.SetActive(false);
		}
		if (SelectedSlotNhanVat == 1)
		{
			OnAvatarClick(avatar1);
		}
		else if (SelectedSlotNhanVat == 2)
		{
			OnAvatarClick(avatar2);
		}
		else if (SelectedSlotNhanVat == 3)
		{
			OnAvatarClick(avatar3);
		}
		else if (SelectedSlotNhanVat == 4)
		{
			OnAvatarClick(avatar4);
		}
		else if (SelectedSlotNhanVat == 5)
		{
			OnAvatarClick(avatar5);
		}
		else if (SelectedSlotNhanVat == 6)
		{
			OnAvatarClick(avatar6);
		}
		else if (SelectedSlotNhanVat == 7)
		{
			OnAvatarClick(avatar7);
		}
		else if (SelectedSlotNhanVat == 8)
		{
			OnAvatarClick(avatar8);
		}
		else
		{
			SelectedSlotNhanVat = 1;
			OnAvatarClick(avatar1);
		}
		OnActivateTrangThai(true);
		OnActivateChieuThuc(false);
	}

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(6);
		SyncWithNetworkData();
	}

	public override void OnDeactive()
	{
		base.OnDeactive();
	}

	public void btnHelp_OnClick()
	{
		ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
		if (optionChuDong.isChecked)
		{
			screenHelpInfo.setByLevel(4, 1);
		}
		else if (optionBiDong.isChecked)
		{
			screenHelpInfo.setByLevel(4, 2);
		}
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
		EGDebug.Log("btnHelp_OnClick");
	}
}
