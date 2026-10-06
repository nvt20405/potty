using LitJson;
using Nettention.Proud;

namespace GameS2C
{
	public class Stub : IJsonStub
	{
		public delegate bool NotifyNextLogonSuccessDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetBattleResultSuccessDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetListLKSuccessDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyAckDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyTingTingDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetInfoSuccessDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyListPositionInMainDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetLuanKiemInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyCapNhatDiemLuanKiemDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyNhanThuongLuanKiemDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDoiThuongLuanKiemDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDauLuanKiemDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDanhGiangHoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifySetTrangBiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifySetDoiHinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyChatMsgDelegate(HostID remote, RmiContext rmiContext, string msg);

		public delegate bool NotifySetHeroDataDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifySetVoCongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifySetTranHinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyStartBoiDuongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyEndBoiDuongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyTuLuyenDeTuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyTrieuHoiDeTuBangHonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyTruyenCongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyInTimeDanhDongNhanDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyOutTimeDanhDongNhanDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetDongNhanInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDanhDongNhanDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyCuongHoaTrangBiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyBanTrangBiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGhepManhTrangBiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGhepManhVoCongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyTinhLuyenTrangBiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifySetDoiHinhHoTroDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyOpenDoiHinhHoTroDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyThamNgoVoCongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyTinhLuyenVoCongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyLayDeTuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyBuyVatPhamDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyCaoNhanDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyThuongNhanDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyBanDoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyBangHuuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyTyThiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyBuyLeBaoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyHoiTheLucDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifySelectStartDeTuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyKyNgoThamBaiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyHuyetChienInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyStartHuyetChienDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyHoiSinhHuyetChienDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyHuyetChienTangThuocTinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDanhHuyetChienDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyNhanThuongHuyetChienDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetTopHuyetChienDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyOpenHopDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyRequestSetDoiHinhAndTranHinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyNhanThuongGiangHoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDanhNhanhGiangHoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyAnGaGiangHoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyResetLuotGiangHoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDanhDanhSonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyMoThuongDanhSonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyMoHetDanhSonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyVuotAiDanhSonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyChonDongDoiDanhSonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetDongDoiDanhSonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyHuaNguyenDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyLenCapNhanThuongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyThanTaiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyXocDiaInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyChoiXocDiaDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyCuuVienTieuPhongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyNhanRuongThachSanhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyPaymentConfirmDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDangNhapNhanThuongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyUongRuouTieuPhongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetDoiRuouInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDoiRuouDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyBatCocDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyChuocThanDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifySearchBanBeDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyNhanDuocThachDauDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyKetQuaThachDauDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDuocAddBanBeDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyAddBanBeDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyAcceptBanBeDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDeleteBanBeDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDangNhapTrungTKDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyBanBeCuuThuInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyChatInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetChatAllDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDatTenMonPhaiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyULinhInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDoiItemULinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyKnbRefreshULinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDoiThuongULinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetTopULinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyKichHoatGiftCodeDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyUseCustomItemDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyBuyVatPhamAndUseDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyHighlightDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyBaoTriServerDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyUseMailPhanThuongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyReadAllMailDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyRefreshMailDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyUseRuongThanDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyBatThoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetFriendsDoiHinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyXemThongTinMonPhaiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifySendMailDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDangNhapQuayXoSoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyMatDongBoDuLieuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyTangTheLucDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDuocTangTheLucDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDoiDoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetDuaTopLevelInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetDuaTopLuanKiemInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyListOnlineInMainDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyCT2PlayerAppearDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyThamGiaCT2Delegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyListCT2Delegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyLapLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyCT2PlayerPosDelegate(HostID remote, RmiContext rmiContext, int GID, int SID, float pos_x, float pos_z, float vel_x, float vel_z);

		public delegate bool NotifyCT2BattleResultDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGiaNhapLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyChapNhanGiaNhapLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyThoatLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyFinishNhiemVuLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyResetNhiemVuLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyRutGiaNhapLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDangHuongLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDoiThuongLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDoiMinhChuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDoiPhoMinhChuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyNangCapCongTrinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyCT2TeleportDelegate(HostID remote, RmiContext rmiContext, int GID, int SID, float pos_x, float pos_z);

		public delegate bool NotifyKhamNgocDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGoNgocDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetTopLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifySearchLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetThongTinLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyCT2NPCMoveDelegate(HostID remote, RmiContext rmiContext, int GID, int SID, float pos_x, float pos_z, float vel_x, float vel_z, byte state);

		public delegate bool NotifyCT2NPCIdleDelegate(HostID remote, RmiContext rmiContext, int GID, int SID, float pos_x, float pos_z);

		public delegate bool NotifyCT2NPCAttackDelegate(HostID remote, RmiContext rmiContext, int GID, int SID, float pos_x, float pos_z);

		public delegate bool NotifyCT2HPPercentDelegate(HostID remote, RmiContext rmiContext, int GID, int SID, float tyleHp, int sinhMenhValue);

		public delegate bool NotifyNhanThuongDiHoaCungAllDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetDiHoaCungInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetTopDiHoaCungDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDuoiKhoiLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyCT2RuneXuatHienDelegate(HostID remote, RmiContext rmiContext, int runeType, int runeIndex, float pos_x, float pos_z);

		public delegate bool NotifyCT2GotRuneDelegate(HostID remote, RmiContext rmiContext, int GID, int SID, int runeType, int runeIndex, float tyleHp, string teamInfo);

		public delegate bool NotifyCT2KetThucDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyCT2PlayerStateDelegate(HostID remote, RmiContext rmiContext, int GID, int SID, byte state);

		public delegate bool NotifyCT2BangXepHangTuanNayDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyCT2BangXepHangTuanTruocDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyCT2PhanThuongBXHTuanDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyThamGiaLuaTraiLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyLienMinhThoiLuaDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyRoiKhoiLuaTraiLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifySetGioLuaTraiLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetExpLuaTraiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifySuaThongBaoLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifySendChatLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyChatLienMinhInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyPhanRaTrangBiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyCT2BXHDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyUpdateLienMinhDataDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyLapNguyenKhiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyThaoNguyenKhiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyNangCapNguyenKhiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyMuaNguyenKhiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyBangChienGetInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyBangChienVaoThanhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyBangChienRoiThanhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyBangChienDenCongThanhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyBangChienCongThanhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyChonHatGiongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyLayHatGiongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyTrongCayDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyThuHoachDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyAnTromDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetAnTromListDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyBangChienListUserMoveDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyBangChienGetPhanThuongThuThanhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyBangChienKetThucDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifySendChatLienSrvDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetChatLienSrvDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetLinhDuocInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyBanhChungOthersDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyBanhChungGetInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyBanhChungNauBanhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyBanhChungNhatNguyenLieuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyBanhChungBXHDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyBanhChungGetPhanThuongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDangNhapNhanThuongTetDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyThaoNguaDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDungNguaDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyActiveNguaDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetGamerLinhDuocDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyCT2HPTeamDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyCuongHoaBatQuaiTranDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifySetSoDoBatQuaiTranDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetLeagueDataDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifySetDoiHinhThienCangTranDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyOpenDoiHinhThienCangTranDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetSieuCupDataDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetSieuCupBattleDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifySieuCupDatCuocDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifySubmitDoiHinhLeagueDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyViewLeagueReplayDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyThamBaiSieuCupDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyVongQuayDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyLienDauDataDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifySieuCupChampionDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyQMDInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyQMDSelectDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyQMDGetChiTietNPCDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyQMDGetBXHDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyQMDXongPhaDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyBeQuanDeTuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDenGioCTDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyNhanThuongTichLuyNapDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyNhanThuongTichLuyTieuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetCacLoaiTopDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDoiTenBangDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetThongTinLienServerDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyBanPhaoHoaDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetTopPhaoHoaDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetPhanThuongPhaoHoaDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyBanPhaoHoaEventDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetTopVongQuayDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyCreateCostumeDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyTinhLuyenCostumeDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyKhamNgocCostumeDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGoNgocCostumeDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyTakeOnCostumeDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyTakeOffCostumeDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyLinhThuongPhaoHoaEventDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyChuyenSinhDeTuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetListOtherUserDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyBatThanThuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyNhanThuongDapNieuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyTruongThanhThanThuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyNangPhamThanThuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDoiThanThuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyThonPheThanThuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyTruyenCongThanThuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifySetBoPhapNhanVatBatQuaiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifySetNoiCongNhanVatBatQuaiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifySetTrangBiNhanVatBatQuaiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetThanThuDaoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifySetThanThuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetGuiTietKiemDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyThamGiaGuiTietKiemDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetThuong1MilUserDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyPopupThuong1MilUserDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyThuHoachSonMonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyXayDungSonMonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyTanCongSonMonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDoiHinhSonMonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDoThamSonMonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyMuaDoThanBiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetTopSonMonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDungLuyenTrangBiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyTayLuyenTrangBiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyKhaiQuangTrangBiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyConfirmTayLuyenTrangBiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetSonMonInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetLanhDiaInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyUnLockVoCongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyQuayBacMayManDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyMoveLanhDiaDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyUpdateLanhDiaDataDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyQuayTuBaoBonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetTopMoRuongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyQuayThienMaHaPhongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetTayVucInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyMuaDoTayVucDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifySpawnNienThuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifySummonNienThuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDanhNienThuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyThamGiaNienThuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyNhanThuongNapHangNgayDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetTopNienThuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyNopLenhBaiNienThuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetTopLanhDiaDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifySelectStartNguaDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyUpdateNienThuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetDiemMoRuongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyUpdateDailyActivitiesDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyThuongDailyActivitiesDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyThienMaQuaySlotDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyThienMaLenhCreateDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyThienMaUpgradeSlotDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyThienMaOpenSlotDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyThienMaEquipDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyThienMaQuaySlotConfirmDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifySetTonHieuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifySetTrangBiHoangKimDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetTopDaiHoiVoLamDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDanhAnDanhCaoThuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyStartBoiDuongTrangBiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyEndBoiDuongTrangBiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyRutQueTienNhanDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetBaoKhoInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyCuopBaoKhoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyThuHoachBaoKhoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetListBaoKhoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyQuayCamCungBiBaoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyNhanThuongCamCungDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyGetHoaVangInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyHoaVangDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyTestProudNetDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyTrangBiHuyenKhiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyThangCapHuyenKhiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyChienHonChangeBuffDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyStartBoiDuongChienHonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyEndBoiDuongChienHonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyTrieuHoiChienHonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyUseTanHonTangLevelChienHonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyDotPhaChienHonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyCheTaoHuyenKhiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyQuayDiemHoaVangDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyUseTuiThanDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyNhanThuongDailyKNBDelegate(HostID remote, RmiContext rmiContext, string data);

		public NotifyNextLogonSuccessDelegate NotifyNextLogonSuccess;

		public NotifyGetBattleResultSuccessDelegate NotifyGetBattleResultSuccess;

		public NotifyGetListLKSuccessDelegate NotifyGetListLKSuccess;

		public NotifyAckDelegate NotifyAck;

		public NotifyTingTingDelegate NotifyTingTing;

		public NotifyGetInfoSuccessDelegate NotifyGetInfoSuccess;

		public NotifyListPositionInMainDelegate NotifyListPositionInMain;

		public NotifyGetLuanKiemInfoDelegate NotifyGetLuanKiemInfo;

		public NotifyCapNhatDiemLuanKiemDelegate NotifyCapNhatDiemLuanKiem;

		public NotifyNhanThuongLuanKiemDelegate NotifyNhanThuongLuanKiem;

		public NotifyDoiThuongLuanKiemDelegate NotifyDoiThuongLuanKiem;

		public NotifyDauLuanKiemDelegate NotifyDauLuanKiem;

		public NotifyDanhGiangHoDelegate NotifyDanhGiangHo;

		public NotifySetTrangBiDelegate NotifySetTrangBi;

		public NotifySetDoiHinhDelegate NotifySetDoiHinh;

		public NotifyChatMsgDelegate NotifyChatMsg;

		public NotifySetHeroDataDelegate NotifySetHeroData;

		public NotifySetVoCongDelegate NotifySetVoCong;

		public NotifySetTranHinhDelegate NotifySetTranHinh;

		public NotifyStartBoiDuongDelegate NotifyStartBoiDuong;

		public NotifyEndBoiDuongDelegate NotifyEndBoiDuong;

		public NotifyTuLuyenDeTuDelegate NotifyTuLuyenDeTu;

		public NotifyTrieuHoiDeTuBangHonDelegate NotifyTrieuHoiDeTuBangHon;

		public NotifyTruyenCongDelegate NotifyTruyenCong;

		public NotifyInTimeDanhDongNhanDelegate NotifyInTimeDanhDongNhan;

		public NotifyOutTimeDanhDongNhanDelegate NotifyOutTimeDanhDongNhan;

		public NotifyGetDongNhanInfoDelegate NotifyGetDongNhanInfo;

		public NotifyDanhDongNhanDelegate NotifyDanhDongNhan;

		public NotifyCuongHoaTrangBiDelegate NotifyCuongHoaTrangBi;

		public NotifyBanTrangBiDelegate NotifyBanTrangBi;

		public NotifyGhepManhTrangBiDelegate NotifyGhepManhTrangBi;

		public NotifyGhepManhVoCongDelegate NotifyGhepManhVoCong;

		public NotifyTinhLuyenTrangBiDelegate NotifyTinhLuyenTrangBi;

		public NotifySetDoiHinhHoTroDelegate NotifySetDoiHinhHoTro;

		public NotifyOpenDoiHinhHoTroDelegate NotifyOpenDoiHinhHoTro;

		public NotifyThamNgoVoCongDelegate NotifyThamNgoVoCong;

		public NotifyTinhLuyenVoCongDelegate NotifyTinhLuyenVoCong;

		public NotifyLayDeTuDelegate NotifyLayDeTu;

		public NotifyBuyVatPhamDelegate NotifyBuyVatPham;

		public NotifyCaoNhanDelegate NotifyCaoNhan;

		public NotifyThuongNhanDelegate NotifyThuongNhan;

		public NotifyBanDoDelegate NotifyBanDo;

		public NotifyBangHuuDelegate NotifyBangHuu;

		public NotifyTyThiDelegate NotifyTyThi;

		public NotifyBuyLeBaoDelegate NotifyBuyLeBao;

		public NotifyHoiTheLucDelegate NotifyHoiTheLuc;

		public NotifySelectStartDeTuDelegate NotifySelectStartDeTu;

		public NotifyKyNgoThamBaiDelegate NotifyKyNgoThamBai;

		public NotifyHuyetChienInfoDelegate NotifyHuyetChienInfo;

		public NotifyStartHuyetChienDelegate NotifyStartHuyetChien;

		public NotifyHoiSinhHuyetChienDelegate NotifyHoiSinhHuyetChien;

		public NotifyHuyetChienTangThuocTinhDelegate NotifyHuyetChienTangThuocTinh;

		public NotifyDanhHuyetChienDelegate NotifyDanhHuyetChien;

		public NotifyNhanThuongHuyetChienDelegate NotifyNhanThuongHuyetChien;

		public NotifyGetTopHuyetChienDelegate NotifyGetTopHuyetChien;

		public NotifyOpenHopDelegate NotifyOpenHop;

		public NotifyRequestSetDoiHinhAndTranHinhDelegate NotifyRequestSetDoiHinhAndTranHinh;

		public NotifyNhanThuongGiangHoDelegate NotifyNhanThuongGiangHo;

		public NotifyDanhNhanhGiangHoDelegate NotifyDanhNhanhGiangHo;

		public NotifyAnGaGiangHoDelegate NotifyAnGaGiangHo;

		public NotifyResetLuotGiangHoDelegate NotifyResetLuotGiangHo;

		public NotifyDanhDanhSonDelegate NotifyDanhDanhSon;

		public NotifyMoThuongDanhSonDelegate NotifyMoThuongDanhSon;

		public NotifyMoHetDanhSonDelegate NotifyMoHetDanhSon;

		public NotifyVuotAiDanhSonDelegate NotifyVuotAiDanhSon;

		public NotifyChonDongDoiDanhSonDelegate NotifyChonDongDoiDanhSon;

		public NotifyGetDongDoiDanhSonDelegate NotifyGetDongDoiDanhSon;

		public NotifyHuaNguyenDelegate NotifyHuaNguyen;

		public NotifyLenCapNhanThuongDelegate NotifyLenCapNhanThuong;

		public NotifyThanTaiDelegate NotifyThanTai;

		public NotifyXocDiaInfoDelegate NotifyXocDiaInfo;

		public NotifyChoiXocDiaDelegate NotifyChoiXocDia;

		public NotifyCuuVienTieuPhongDelegate NotifyCuuVienTieuPhong;

		public NotifyNhanRuongThachSanhDelegate NotifyNhanRuongThachSanh;

		public NotifyPaymentConfirmDelegate NotifyPaymentConfirm;

		public NotifyDangNhapNhanThuongDelegate NotifyDangNhapNhanThuong;

		public NotifyUongRuouTieuPhongDelegate NotifyUongRuouTieuPhong;

		public NotifyGetDoiRuouInfoDelegate NotifyGetDoiRuouInfo;

		public NotifyDoiRuouDelegate NotifyDoiRuou;

		public NotifyBatCocDelegate NotifyBatCoc;

		public NotifyChuocThanDelegate NotifyChuocThan;

		public NotifySearchBanBeDelegate NotifySearchBanBe;

		public NotifyNhanDuocThachDauDelegate NotifyNhanDuocThachDau;

		public NotifyKetQuaThachDauDelegate NotifyKetQuaThachDau;

		public NotifyDuocAddBanBeDelegate NotifyDuocAddBanBe;

		public NotifyAddBanBeDelegate NotifyAddBanBe;

		public NotifyAcceptBanBeDelegate NotifyAcceptBanBe;

		public NotifyDeleteBanBeDelegate NotifyDeleteBanBe;

		public NotifyDangNhapTrungTKDelegate NotifyDangNhapTrungTK;

		public NotifyBanBeCuuThuInfoDelegate NotifyBanBeCuuThuInfo;

		public NotifyChatInfoDelegate NotifyChatInfo;

		public NotifyGetChatAllDelegate NotifyGetChatAll;

		public NotifyDatTenMonPhaiDelegate NotifyDatTenMonPhai;

		public NotifyULinhInfoDelegate NotifyULinhInfo;

		public NotifyDoiItemULinhDelegate NotifyDoiItemULinh;

		public NotifyKnbRefreshULinhDelegate NotifyKnbRefreshULinh;

		public NotifyDoiThuongULinhDelegate NotifyDoiThuongULinh;

		public NotifyGetTopULinhDelegate NotifyGetTopULinh;

		public NotifyKichHoatGiftCodeDelegate NotifyKichHoatGiftCode;

		public NotifyUseCustomItemDelegate NotifyUseCustomItem;

		public NotifyBuyVatPhamAndUseDelegate NotifyBuyVatPhamAndUse;

		public NotifyHighlightDelegate NotifyHighlight;

		public NotifyBaoTriServerDelegate NotifyBaoTriServer;

		public NotifyUseMailPhanThuongDelegate NotifyUseMailPhanThuong;

		public NotifyReadAllMailDelegate NotifyReadAllMail;

		public NotifyRefreshMailDelegate NotifyRefreshMail;

		public NotifyUseRuongThanDelegate NotifyUseRuongThan;

		public NotifyBatThoDelegate NotifyBatTho;

		public NotifyGetFriendsDoiHinhDelegate NotifyGetFriendsDoiHinh;

		public NotifyXemThongTinMonPhaiDelegate NotifyXemThongTinMonPhai;

		public NotifySendMailDelegate NotifySendMail;

		public NotifyDangNhapQuayXoSoDelegate NotifyDangNhapQuayXoSo;

		public NotifyMatDongBoDuLieuDelegate NotifyMatDongBoDuLieu;

		public NotifyTangTheLucDelegate NotifyTangTheLuc;

		public NotifyDuocTangTheLucDelegate NotifyDuocTangTheLuc;

		public NotifyDoiDoDelegate NotifyDoiDo;

		public NotifyGetDuaTopLevelInfoDelegate NotifyGetDuaTopLevelInfo;

		public NotifyGetDuaTopLuanKiemInfoDelegate NotifyGetDuaTopLuanKiemInfo;

		public NotifyListOnlineInMainDelegate NotifyListOnlineInMain;

		public NotifyCT2PlayerAppearDelegate NotifyCT2PlayerAppear;

		public NotifyThamGiaCT2Delegate NotifyThamGiaCT2;

		public NotifyListCT2Delegate NotifyListCT2;

		public NotifyLapLienMinhDelegate NotifyLapLienMinh;

		public NotifyCT2PlayerPosDelegate NotifyCT2PlayerPos;

		public NotifyCT2BattleResultDelegate NotifyCT2BattleResult;

		public NotifyGiaNhapLienMinhDelegate NotifyGiaNhapLienMinh;

		public NotifyChapNhanGiaNhapLienMinhDelegate NotifyChapNhanGiaNhapLienMinh;

		public NotifyThoatLienMinhDelegate NotifyThoatLienMinh;

		public NotifyFinishNhiemVuLienMinhDelegate NotifyFinishNhiemVuLienMinh;

		public NotifyResetNhiemVuLienMinhDelegate NotifyResetNhiemVuLienMinh;

		public NotifyRutGiaNhapLienMinhDelegate NotifyRutGiaNhapLienMinh;

		public NotifyDangHuongLienMinhDelegate NotifyDangHuongLienMinh;

		public NotifyDoiThuongLienMinhDelegate NotifyDoiThuongLienMinh;

		public NotifyDoiMinhChuDelegate NotifyDoiMinhChu;

		public NotifyDoiPhoMinhChuDelegate NotifyDoiPhoMinhChu;

		public NotifyNangCapCongTrinhDelegate NotifyNangCapCongTrinh;

		public NotifyCT2TeleportDelegate NotifyCT2Teleport;

		public NotifyKhamNgocDelegate NotifyKhamNgoc;

		public NotifyGoNgocDelegate NotifyGoNgoc;

		public NotifyGetTopLienMinhDelegate NotifyGetTopLienMinh;

		public NotifySearchLienMinhDelegate NotifySearchLienMinh;

		public NotifyGetThongTinLienMinhDelegate NotifyGetThongTinLienMinh;

		public NotifyCT2NPCMoveDelegate NotifyCT2NPCMove;

		public NotifyCT2NPCIdleDelegate NotifyCT2NPCIdle;

		public NotifyCT2NPCAttackDelegate NotifyCT2NPCAttack;

		public NotifyCT2HPPercentDelegate NotifyCT2HPPercent;

		public NotifyNhanThuongDiHoaCungAllDelegate NotifyNhanThuongDiHoaCungAll;

		public NotifyGetDiHoaCungInfoDelegate NotifyGetDiHoaCungInfo;

		public NotifyGetTopDiHoaCungDelegate NotifyGetTopDiHoaCung;

		public NotifyDuoiKhoiLienMinhDelegate NotifyDuoiKhoiLienMinh;

		public NotifyCT2RuneXuatHienDelegate NotifyCT2RuneXuatHien;

		public NotifyCT2GotRuneDelegate NotifyCT2GotRune;

		public NotifyCT2KetThucDelegate NotifyCT2KetThuc;

		public NotifyCT2PlayerStateDelegate NotifyCT2PlayerState;

		public NotifyCT2BangXepHangTuanNayDelegate NotifyCT2BangXepHangTuanNay;

		public NotifyCT2BangXepHangTuanTruocDelegate NotifyCT2BangXepHangTuanTruoc;

		public NotifyCT2PhanThuongBXHTuanDelegate NotifyCT2PhanThuongBXHTuan;

		public NotifyThamGiaLuaTraiLienMinhDelegate NotifyThamGiaLuaTraiLienMinh;

		public NotifyLienMinhThoiLuaDelegate NotifyLienMinhThoiLua;

		public NotifyRoiKhoiLuaTraiLienMinhDelegate NotifyRoiKhoiLuaTraiLienMinh;

		public NotifySetGioLuaTraiLienMinhDelegate NotifySetGioLuaTraiLienMinh;

		public NotifyGetExpLuaTraiDelegate NotifyGetExpLuaTrai;

		public NotifySuaThongBaoLienMinhDelegate NotifySuaThongBaoLienMinh;

		public NotifySendChatLienMinhDelegate NotifySendChatLienMinh;

		public NotifyChatLienMinhInfoDelegate NotifyChatLienMinhInfo;

		public NotifyPhanRaTrangBiDelegate NotifyPhanRaTrangBi;

		public NotifyCT2BXHDelegate NotifyCT2BXH;

		public NotifyUpdateLienMinhDataDelegate NotifyUpdateLienMinhData;

		public NotifyLapNguyenKhiDelegate NotifyLapNguyenKhi;

		public NotifyThaoNguyenKhiDelegate NotifyThaoNguyenKhi;

		public NotifyNangCapNguyenKhiDelegate NotifyNangCapNguyenKhi;

		public NotifyMuaNguyenKhiDelegate NotifyMuaNguyenKhi;

		public NotifyBangChienGetInfoDelegate NotifyBangChienGetInfo;

		public NotifyBangChienVaoThanhDelegate NotifyBangChienVaoThanh;

		public NotifyBangChienRoiThanhDelegate NotifyBangChienRoiThanh;

		public NotifyBangChienDenCongThanhDelegate NotifyBangChienDenCongThanh;

		public NotifyBangChienCongThanhDelegate NotifyBangChienCongThanh;

		public NotifyChonHatGiongDelegate NotifyChonHatGiong;

		public NotifyLayHatGiongDelegate NotifyLayHatGiong;

		public NotifyTrongCayDelegate NotifyTrongCay;

		public NotifyThuHoachDelegate NotifyThuHoach;

		public NotifyAnTromDelegate NotifyAnTrom;

		public NotifyGetAnTromListDelegate NotifyGetAnTromList;

		public NotifyBangChienListUserMoveDelegate NotifyBangChienListUserMove;

		public NotifyBangChienGetPhanThuongThuThanhDelegate NotifyBangChienGetPhanThuongThuThanh;

		public NotifyBangChienKetThucDelegate NotifyBangChienKetThuc;

		public NotifySendChatLienSrvDelegate NotifySendChatLienSrv;

		public NotifyGetChatLienSrvDelegate NotifyGetChatLienSrv;

		public NotifyGetLinhDuocInfoDelegate NotifyGetLinhDuocInfo;

		public NotifyBanhChungOthersDelegate NotifyBanhChungOthers;

		public NotifyBanhChungGetInfoDelegate NotifyBanhChungGetInfo;

		public NotifyBanhChungNauBanhDelegate NotifyBanhChungNauBanh;

		public NotifyBanhChungNhatNguyenLieuDelegate NotifyBanhChungNhatNguyenLieu;

		public NotifyBanhChungBXHDelegate NotifyBanhChungBXH;

		public NotifyBanhChungGetPhanThuongDelegate NotifyBanhChungGetPhanThuong;

		public NotifyDangNhapNhanThuongTetDelegate NotifyDangNhapNhanThuongTet;

		public NotifyThaoNguaDelegate NotifyThaoNgua;

		public NotifyDungNguaDelegate NotifyDungNgua;

		public NotifyActiveNguaDelegate NotifyActiveNgua;

		public NotifyGetGamerLinhDuocDelegate NotifyGetGamerLinhDuoc;

		public NotifyCT2HPTeamDelegate NotifyCT2HPTeam;

		public NotifyCuongHoaBatQuaiTranDelegate NotifyCuongHoaBatQuaiTran;

		public NotifySetSoDoBatQuaiTranDelegate NotifySetSoDoBatQuaiTran;

		public NotifyGetLeagueDataDelegate NotifyGetLeagueData;

		public NotifySetDoiHinhThienCangTranDelegate NotifySetDoiHinhThienCangTran;

		public NotifyOpenDoiHinhThienCangTranDelegate NotifyOpenDoiHinhThienCangTran;

		public NotifyGetSieuCupDataDelegate NotifyGetSieuCupData;

		public NotifyGetSieuCupBattleDelegate NotifyGetSieuCupBattle;

		public NotifySieuCupDatCuocDelegate NotifySieuCupDatCuoc;

		public NotifySubmitDoiHinhLeagueDelegate NotifySubmitDoiHinhLeague;

		public NotifyViewLeagueReplayDelegate NotifyViewLeagueReplay;

		public NotifyThamBaiSieuCupDelegate NotifyThamBaiSieuCup;

		public NotifyVongQuayDelegate NotifyVongQuay;

		public NotifyLienDauDataDelegate NotifyLienDauData;

		public NotifySieuCupChampionDelegate NotifySieuCupChampion;

		public NotifyQMDInfoDelegate NotifyQMDInfo;

		public NotifyQMDSelectDelegate NotifyQMDSelect;

		public NotifyQMDGetChiTietNPCDelegate NotifyQMDGetChiTietNPC;

		public NotifyQMDGetBXHDelegate NotifyQMDGetBXH;

		public NotifyQMDXongPhaDelegate NotifyQMDXongPha;

		public NotifyBeQuanDeTuDelegate NotifyBeQuanDeTu;

		public NotifyDenGioCTDelegate NotifyDenGioCT;

		public NotifyNhanThuongTichLuyNapDelegate NotifyNhanThuongTichLuyNap;

		public NotifyNhanThuongTichLuyTieuDelegate NotifyNhanThuongTichLuyTieu;

		public NotifyGetCacLoaiTopDelegate NotifyGetCacLoaiTop;

		public NotifyDoiTenBangDelegate NotifyDoiTenBang;

		public NotifyGetThongTinLienServerDelegate NotifyGetThongTinLienServer;

		public NotifyBanPhaoHoaDelegate NotifyBanPhaoHoa;

		public NotifyGetTopPhaoHoaDelegate NotifyGetTopPhaoHoa;

		public NotifyGetPhanThuongPhaoHoaDelegate NotifyGetPhanThuongPhaoHoa;

		public NotifyBanPhaoHoaEventDelegate NotifyBanPhaoHoaEvent;

		public NotifyGetTopVongQuayDelegate NotifyGetTopVongQuay;

		public NotifyCreateCostumeDelegate NotifyCreateCostume;

		public NotifyTinhLuyenCostumeDelegate NotifyTinhLuyenCostume;

		public NotifyKhamNgocCostumeDelegate NotifyKhamNgocCostume;

		public NotifyGoNgocCostumeDelegate NotifyGoNgocCostume;

		public NotifyTakeOnCostumeDelegate NotifyTakeOnCostume;

		public NotifyTakeOffCostumeDelegate NotifyTakeOffCostume;

		public NotifyLinhThuongPhaoHoaEventDelegate NotifyLinhThuongPhaoHoaEvent;

		public NotifyChuyenSinhDeTuDelegate NotifyChuyenSinhDeTu;

		public NotifyGetListOtherUserDelegate NotifyGetListOtherUser;

		public NotifyBatThanThuDelegate NotifyBatThanThu;

		public NotifyNhanThuongDapNieuDelegate NotifyNhanThuongDapNieu;

		public NotifyTruongThanhThanThuDelegate NotifyTruongThanhThanThu;

		public NotifyNangPhamThanThuDelegate NotifyNangPhamThanThu;

		public NotifyDoiThanThuDelegate NotifyDoiThanThu;

		public NotifyThonPheThanThuDelegate NotifyThonPheThanThu;

		public NotifyTruyenCongThanThuDelegate NotifyTruyenCongThanThu;

		public NotifySetBoPhapNhanVatBatQuaiDelegate NotifySetBoPhapNhanVatBatQuai;

		public NotifySetNoiCongNhanVatBatQuaiDelegate NotifySetNoiCongNhanVatBatQuai;

		public NotifySetTrangBiNhanVatBatQuaiDelegate NotifySetTrangBiNhanVatBatQuai;

		public NotifyGetThanThuDaoDelegate NotifyGetThanThuDao;

		public NotifySetThanThuDelegate NotifySetThanThu;

		public NotifyGetGuiTietKiemDelegate NotifyGetGuiTietKiem;

		public NotifyThamGiaGuiTietKiemDelegate NotifyThamGiaGuiTietKiem;

		public NotifyGetThuong1MilUserDelegate NotifyGetThuong1MilUser;

		public NotifyPopupThuong1MilUserDelegate NotifyPopupThuong1MilUser;

		public NotifyThuHoachSonMonDelegate NotifyThuHoachSonMon;

		public NotifyXayDungSonMonDelegate NotifyXayDungSonMon;

		public NotifyTanCongSonMonDelegate NotifyTanCongSonMon;

		public NotifyDoiHinhSonMonDelegate NotifyDoiHinhSonMon;

		public NotifyDoThamSonMonDelegate NotifyDoThamSonMon;

		public NotifyMuaDoThanBiDelegate NotifyMuaDoThanBi;

		public NotifyGetTopSonMonDelegate NotifyGetTopSonMon;

		public NotifyDungLuyenTrangBiDelegate NotifyDungLuyenTrangBi;

		public NotifyTayLuyenTrangBiDelegate NotifyTayLuyenTrangBi;

		public NotifyKhaiQuangTrangBiDelegate NotifyKhaiQuangTrangBi;

		public NotifyConfirmTayLuyenTrangBiDelegate NotifyConfirmTayLuyenTrangBi;

		public NotifyGetSonMonInfoDelegate NotifyGetSonMonInfo;

		public NotifyGetLanhDiaInfoDelegate NotifyGetLanhDiaInfo;

		public NotifyUnLockVoCongDelegate NotifyUnLockVoCong;

		public NotifyQuayBacMayManDelegate NotifyQuayBacMayMan;

		public NotifyMoveLanhDiaDelegate NotifyMoveLanhDia;

		public NotifyUpdateLanhDiaDataDelegate NotifyUpdateLanhDiaData;

		public NotifyQuayTuBaoBonDelegate NotifyQuayTuBaoBon;

		public NotifyGetTopMoRuongDelegate NotifyGetTopMoRuong;

		public NotifyQuayThienMaHaPhongDelegate NotifyQuayThienMaHaPhong;

		public NotifyGetTayVucInfoDelegate NotifyGetTayVucInfo;

		public NotifyMuaDoTayVucDelegate NotifyMuaDoTayVuc;

		public NotifySpawnNienThuDelegate NotifySpawnNienThu;

		public NotifySummonNienThuDelegate NotifySummonNienThu;

		public NotifyDanhNienThuDelegate NotifyDanhNienThu;

		public NotifyThamGiaNienThuDelegate NotifyThamGiaNienThu;

		public NotifyNhanThuongNapHangNgayDelegate NotifyNhanThuongNapHangNgay;

		public NotifyGetTopNienThuDelegate NotifyGetTopNienThu;

		public NotifyNopLenhBaiNienThuDelegate NotifyNopLenhBaiNienThu;

		public NotifyGetTopLanhDiaDelegate NotifyGetTopLanhDia;

		public NotifySelectStartNguaDelegate NotifySelectStartNgua;

		public NotifyUpdateNienThuDelegate NotifyUpdateNienThu;

		public NotifyGetDiemMoRuongDelegate NotifyGetDiemMoRuong;

		public NotifyUpdateDailyActivitiesDelegate NotifyUpdateDailyActivities;

		public NotifyThuongDailyActivitiesDelegate NotifyThuongDailyActivities;

		public NotifyThienMaQuaySlotDelegate NotifyThienMaQuaySlot;

		public NotifyThienMaLenhCreateDelegate NotifyThienMaLenhCreate;

		public NotifyThienMaUpgradeSlotDelegate NotifyThienMaUpgradeSlot;

		public NotifyThienMaOpenSlotDelegate NotifyThienMaOpenSlot;

		public NotifyThienMaEquipDelegate NotifyThienMaEquip;

		public NotifyThienMaQuaySlotConfirmDelegate NotifyThienMaQuaySlotConfirm;

		public NotifySetTonHieuDelegate NotifySetTonHieu;

		public NotifySetTrangBiHoangKimDelegate NotifySetTrangBiHoangKim;

		public NotifyGetTopDaiHoiVoLamDelegate NotifyGetTopDaiHoiVoLam;

		public NotifyDanhAnDanhCaoThuDelegate NotifyDanhAnDanhCaoThu;

		public NotifyStartBoiDuongTrangBiDelegate NotifyStartBoiDuongTrangBi;

		public NotifyEndBoiDuongTrangBiDelegate NotifyEndBoiDuongTrangBi;

		public NotifyRutQueTienNhanDelegate NotifyRutQueTienNhan;

		public NotifyGetBaoKhoInfoDelegate NotifyGetBaoKhoInfo;

		public NotifyCuopBaoKhoDelegate NotifyCuopBaoKho;

		public NotifyThuHoachBaoKhoDelegate NotifyThuHoachBaoKho;

		public NotifyGetListBaoKhoDelegate NotifyGetListBaoKho;

		public NotifyQuayCamCungBiBaoDelegate NotifyQuayCamCungBiBao;

		public NotifyNhanThuongCamCungDelegate NotifyNhanThuongCamCung;

		public NotifyGetHoaVangInfoDelegate NotifyGetHoaVangInfo;

		public NotifyHoaVangDelegate NotifyHoaVang;

		public NotifyTestProudNetDelegate NotifyTestProudNet;

		public NotifyTrangBiHuyenKhiDelegate NotifyTrangBiHuyenKhi;

		public NotifyThangCapHuyenKhiDelegate NotifyThangCapHuyenKhi;

		public NotifyChienHonChangeBuffDelegate NotifyChienHonChangeBuff;

		public NotifyStartBoiDuongChienHonDelegate NotifyStartBoiDuongChienHon;

		public NotifyEndBoiDuongChienHonDelegate NotifyEndBoiDuongChienHon;

		public NotifyTrieuHoiChienHonDelegate NotifyTrieuHoiChienHon;

		public NotifyUseTanHonTangLevelChienHonDelegate NotifyUseTanHonTangLevelChienHon;

		public NotifyDotPhaChienHonDelegate NotifyDotPhaChienHon;

		public NotifyCheTaoHuyenKhiDelegate NotifyCheTaoHuyenKhi;

		public NotifyQuayDiemHoaVangDelegate NotifyQuayDiemHoaVang;

		public NotifyUseTuiThanDelegate NotifyUseTuiThan;

		public NotifyNhanThuongDailyKNBDelegate NotifyNhanThuongDailyKNB;

		public bool Dispatch(string rmiName, string data)
		{
			switch (rmiName)
			{
			case "NotifyNextLogonSuccess":
				if (NotifyNextLogonSuccess != null)
				{
					return NotifyNextLogonSuccess(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetBattleResultSuccess":
				if (NotifyGetBattleResultSuccess != null)
				{
					return NotifyGetBattleResultSuccess(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetListLKSuccess":
				if (NotifyGetListLKSuccess != null)
				{
					return NotifyGetListLKSuccess(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyAck":
				if (NotifyAck != null)
				{
					return NotifyAck(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyTingTing":
				if (NotifyTingTing != null)
				{
					return NotifyTingTing(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetInfoSuccess":
				if (NotifyGetInfoSuccess != null)
				{
					return NotifyGetInfoSuccess(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyListPositionInMain":
				if (NotifyListPositionInMain != null)
				{
					return NotifyListPositionInMain(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetLuanKiemInfo":
				if (NotifyGetLuanKiemInfo != null)
				{
					return NotifyGetLuanKiemInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyCapNhatDiemLuanKiem":
				if (NotifyCapNhatDiemLuanKiem != null)
				{
					return NotifyCapNhatDiemLuanKiem(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyNhanThuongLuanKiem":
				if (NotifyNhanThuongLuanKiem != null)
				{
					return NotifyNhanThuongLuanKiem(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDoiThuongLuanKiem":
				if (NotifyDoiThuongLuanKiem != null)
				{
					return NotifyDoiThuongLuanKiem(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDauLuanKiem":
				if (NotifyDauLuanKiem != null)
				{
					return NotifyDauLuanKiem(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDanhGiangHo":
				if (NotifyDanhGiangHo != null)
				{
					return NotifyDanhGiangHo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifySetTrangBi":
				if (NotifySetTrangBi != null)
				{
					return NotifySetTrangBi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifySetDoiHinh":
				if (NotifySetDoiHinh != null)
				{
					return NotifySetDoiHinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyChatMsg":
				if (NotifyChatMsg != null)
				{
					return NotifyChatMsg(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifySetHeroData":
				if (NotifySetHeroData != null)
				{
					return NotifySetHeroData(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifySetVoCong":
				if (NotifySetVoCong != null)
				{
					return NotifySetVoCong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifySetTranHinh":
				if (NotifySetTranHinh != null)
				{
					return NotifySetTranHinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyStartBoiDuong":
				if (NotifyStartBoiDuong != null)
				{
					return NotifyStartBoiDuong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyEndBoiDuong":
				if (NotifyEndBoiDuong != null)
				{
					return NotifyEndBoiDuong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyTuLuyenDeTu":
				if (NotifyTuLuyenDeTu != null)
				{
					return NotifyTuLuyenDeTu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyTrieuHoiDeTuBangHon":
				if (NotifyTrieuHoiDeTuBangHon != null)
				{
					return NotifyTrieuHoiDeTuBangHon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyTruyenCong":
				if (NotifyTruyenCong != null)
				{
					return NotifyTruyenCong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyInTimeDanhDongNhan":
				if (NotifyInTimeDanhDongNhan != null)
				{
					return NotifyInTimeDanhDongNhan(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyOutTimeDanhDongNhan":
				if (NotifyOutTimeDanhDongNhan != null)
				{
					return NotifyOutTimeDanhDongNhan(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetDongNhanInfo":
				if (NotifyGetDongNhanInfo != null)
				{
					return NotifyGetDongNhanInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDanhDongNhan":
				if (NotifyDanhDongNhan != null)
				{
					return NotifyDanhDongNhan(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyCuongHoaTrangBi":
				if (NotifyCuongHoaTrangBi != null)
				{
					return NotifyCuongHoaTrangBi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyBanTrangBi":
				if (NotifyBanTrangBi != null)
				{
					return NotifyBanTrangBi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGhepManhTrangBi":
				if (NotifyGhepManhTrangBi != null)
				{
					return NotifyGhepManhTrangBi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGhepManhVoCong":
				if (NotifyGhepManhVoCong != null)
				{
					return NotifyGhepManhVoCong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyTinhLuyenTrangBi":
				if (NotifyTinhLuyenTrangBi != null)
				{
					return NotifyTinhLuyenTrangBi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifySetDoiHinhHoTro":
				if (NotifySetDoiHinhHoTro != null)
				{
					return NotifySetDoiHinhHoTro(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyOpenDoiHinhHoTro":
				if (NotifyOpenDoiHinhHoTro != null)
				{
					return NotifyOpenDoiHinhHoTro(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyThamNgoVoCong":
				if (NotifyThamNgoVoCong != null)
				{
					return NotifyThamNgoVoCong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyTinhLuyenVoCong":
				if (NotifyTinhLuyenVoCong != null)
				{
					return NotifyTinhLuyenVoCong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyLayDeTu":
				if (NotifyLayDeTu != null)
				{
					return NotifyLayDeTu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyBuyVatPham":
				if (NotifyBuyVatPham != null)
				{
					return NotifyBuyVatPham(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyCaoNhan":
				if (NotifyCaoNhan != null)
				{
					return NotifyCaoNhan(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyThuongNhan":
				if (NotifyThuongNhan != null)
				{
					return NotifyThuongNhan(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyBanDo":
				if (NotifyBanDo != null)
				{
					return NotifyBanDo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyBangHuu":
				if (NotifyBangHuu != null)
				{
					return NotifyBangHuu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyTyThi":
				if (NotifyTyThi != null)
				{
					return NotifyTyThi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyBuyLeBao":
				if (NotifyBuyLeBao != null)
				{
					return NotifyBuyLeBao(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyHoiTheLuc":
				if (NotifyHoiTheLuc != null)
				{
					return NotifyHoiTheLuc(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifySelectStartDeTu":
				if (NotifySelectStartDeTu != null)
				{
					return NotifySelectStartDeTu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyKyNgoThamBai":
				if (NotifyKyNgoThamBai != null)
				{
					return NotifyKyNgoThamBai(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyHuyetChienInfo":
				if (NotifyHuyetChienInfo != null)
				{
					return NotifyHuyetChienInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyStartHuyetChien":
				if (NotifyStartHuyetChien != null)
				{
					return NotifyStartHuyetChien(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyHoiSinhHuyetChien":
				if (NotifyHoiSinhHuyetChien != null)
				{
					return NotifyHoiSinhHuyetChien(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyHuyetChienTangThuocTinh":
				if (NotifyHuyetChienTangThuocTinh != null)
				{
					return NotifyHuyetChienTangThuocTinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDanhHuyetChien":
				if (NotifyDanhHuyetChien != null)
				{
					return NotifyDanhHuyetChien(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyNhanThuongHuyetChien":
				if (NotifyNhanThuongHuyetChien != null)
				{
					return NotifyNhanThuongHuyetChien(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetTopHuyetChien":
				if (NotifyGetTopHuyetChien != null)
				{
					return NotifyGetTopHuyetChien(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyOpenHop":
				if (NotifyOpenHop != null)
				{
					return NotifyOpenHop(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyRequestSetDoiHinhAndTranHinh":
				if (NotifyRequestSetDoiHinhAndTranHinh != null)
				{
					return NotifyRequestSetDoiHinhAndTranHinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyNhanThuongGiangHo":
				if (NotifyNhanThuongGiangHo != null)
				{
					return NotifyNhanThuongGiangHo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDanhNhanhGiangHo":
				if (NotifyDanhNhanhGiangHo != null)
				{
					return NotifyDanhNhanhGiangHo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyAnGaGiangHo":
				if (NotifyAnGaGiangHo != null)
				{
					return NotifyAnGaGiangHo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyResetLuotGiangHo":
				if (NotifyResetLuotGiangHo != null)
				{
					return NotifyResetLuotGiangHo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDanhDanhSon":
				if (NotifyDanhDanhSon != null)
				{
					return NotifyDanhDanhSon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyMoThuongDanhSon":
				if (NotifyMoThuongDanhSon != null)
				{
					return NotifyMoThuongDanhSon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyMoHetDanhSon":
				if (NotifyMoHetDanhSon != null)
				{
					return NotifyMoHetDanhSon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyVuotAiDanhSon":
				if (NotifyVuotAiDanhSon != null)
				{
					return NotifyVuotAiDanhSon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyChonDongDoiDanhSon":
				if (NotifyChonDongDoiDanhSon != null)
				{
					return NotifyChonDongDoiDanhSon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetDongDoiDanhSon":
				if (NotifyGetDongDoiDanhSon != null)
				{
					return NotifyGetDongDoiDanhSon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyHuaNguyen":
				if (NotifyHuaNguyen != null)
				{
					return NotifyHuaNguyen(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyLenCapNhanThuong":
				if (NotifyLenCapNhanThuong != null)
				{
					return NotifyLenCapNhanThuong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyThanTai":
				if (NotifyThanTai != null)
				{
					return NotifyThanTai(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyXocDiaInfo":
				if (NotifyXocDiaInfo != null)
				{
					return NotifyXocDiaInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyChoiXocDia":
				if (NotifyChoiXocDia != null)
				{
					return NotifyChoiXocDia(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyCuuVienTieuPhong":
				if (NotifyCuuVienTieuPhong != null)
				{
					return NotifyCuuVienTieuPhong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyNhanRuongThachSanh":
				if (NotifyNhanRuongThachSanh != null)
				{
					return NotifyNhanRuongThachSanh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyPaymentConfirm":
				if (NotifyPaymentConfirm != null)
				{
					return NotifyPaymentConfirm(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDangNhapNhanThuong":
				if (NotifyDangNhapNhanThuong != null)
				{
					return NotifyDangNhapNhanThuong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyUongRuouTieuPhong":
				if (NotifyUongRuouTieuPhong != null)
				{
					return NotifyUongRuouTieuPhong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetDoiRuouInfo":
				if (NotifyGetDoiRuouInfo != null)
				{
					return NotifyGetDoiRuouInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDoiRuou":
				if (NotifyDoiRuou != null)
				{
					return NotifyDoiRuou(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyBatCoc":
				if (NotifyBatCoc != null)
				{
					return NotifyBatCoc(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyChuocThan":
				if (NotifyChuocThan != null)
				{
					return NotifyChuocThan(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifySearchBanBe":
				if (NotifySearchBanBe != null)
				{
					return NotifySearchBanBe(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyNhanDuocThachDau":
				if (NotifyNhanDuocThachDau != null)
				{
					return NotifyNhanDuocThachDau(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyKetQuaThachDau":
				if (NotifyKetQuaThachDau != null)
				{
					return NotifyKetQuaThachDau(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDuocAddBanBe":
				if (NotifyDuocAddBanBe != null)
				{
					return NotifyDuocAddBanBe(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyAddBanBe":
				if (NotifyAddBanBe != null)
				{
					return NotifyAddBanBe(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyAcceptBanBe":
				if (NotifyAcceptBanBe != null)
				{
					return NotifyAcceptBanBe(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDeleteBanBe":
				if (NotifyDeleteBanBe != null)
				{
					return NotifyDeleteBanBe(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDangNhapTrungTK":
				if (NotifyDangNhapTrungTK != null)
				{
					return NotifyDangNhapTrungTK(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyBanBeCuuThuInfo":
				if (NotifyBanBeCuuThuInfo != null)
				{
					return NotifyBanBeCuuThuInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyChatInfo":
				if (NotifyChatInfo != null)
				{
					return NotifyChatInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetChatAll":
				if (NotifyGetChatAll != null)
				{
					return NotifyGetChatAll(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDatTenMonPhai":
				if (NotifyDatTenMonPhai != null)
				{
					return NotifyDatTenMonPhai(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyULinhInfo":
				if (NotifyULinhInfo != null)
				{
					return NotifyULinhInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDoiItemULinh":
				if (NotifyDoiItemULinh != null)
				{
					return NotifyDoiItemULinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyKnbRefreshULinh":
				if (NotifyKnbRefreshULinh != null)
				{
					return NotifyKnbRefreshULinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDoiThuongULinh":
				if (NotifyDoiThuongULinh != null)
				{
					return NotifyDoiThuongULinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetTopULinh":
				if (NotifyGetTopULinh != null)
				{
					return NotifyGetTopULinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyKichHoatGiftCode":
				if (NotifyKichHoatGiftCode != null)
				{
					return NotifyKichHoatGiftCode(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyUseCustomItem":
				if (NotifyUseCustomItem != null)
				{
					return NotifyUseCustomItem(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyBuyVatPhamAndUse":
				if (NotifyBuyVatPhamAndUse != null)
				{
					return NotifyBuyVatPhamAndUse(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyHighlight":
				if (NotifyHighlight != null)
				{
					return NotifyHighlight(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyBaoTriServer":
				if (NotifyBaoTriServer != null)
				{
					return NotifyBaoTriServer(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyUseMailPhanThuong":
				if (NotifyUseMailPhanThuong != null)
				{
					return NotifyUseMailPhanThuong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyReadAllMail":
				if (NotifyReadAllMail != null)
				{
					return NotifyReadAllMail(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyRefreshMail":
				if (NotifyRefreshMail != null)
				{
					return NotifyRefreshMail(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyUseRuongThan":
				if (NotifyUseRuongThan != null)
				{
					return NotifyUseRuongThan(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyBatTho":
				if (NotifyBatTho != null)
				{
					return NotifyBatTho(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetFriendsDoiHinh":
				if (NotifyGetFriendsDoiHinh != null)
				{
					return NotifyGetFriendsDoiHinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyXemThongTinMonPhai":
				if (NotifyXemThongTinMonPhai != null)
				{
					return NotifyXemThongTinMonPhai(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifySendMail":
				if (NotifySendMail != null)
				{
					return NotifySendMail(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDangNhapQuayXoSo":
				if (NotifyDangNhapQuayXoSo != null)
				{
					return NotifyDangNhapQuayXoSo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyMatDongBoDuLieu":
				if (NotifyMatDongBoDuLieu != null)
				{
					return NotifyMatDongBoDuLieu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyTangTheLuc":
				if (NotifyTangTheLuc != null)
				{
					return NotifyTangTheLuc(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDuocTangTheLuc":
				if (NotifyDuocTangTheLuc != null)
				{
					return NotifyDuocTangTheLuc(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDoiDo":
				if (NotifyDoiDo != null)
				{
					return NotifyDoiDo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetDuaTopLevelInfo":
				if (NotifyGetDuaTopLevelInfo != null)
				{
					return NotifyGetDuaTopLevelInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetDuaTopLuanKiemInfo":
				if (NotifyGetDuaTopLuanKiemInfo != null)
				{
					return NotifyGetDuaTopLuanKiemInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyListOnlineInMain":
				if (NotifyListOnlineInMain != null)
				{
					return NotifyListOnlineInMain(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyCT2PlayerAppear":
				if (NotifyCT2PlayerAppear != null)
				{
					return NotifyCT2PlayerAppear(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyThamGiaCT2":
				if (NotifyThamGiaCT2 != null)
				{
					return NotifyThamGiaCT2(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyListCT2":
				if (NotifyListCT2 != null)
				{
					return NotifyListCT2(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyLapLienMinh":
				if (NotifyLapLienMinh != null)
				{
					return NotifyLapLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyCT2PlayerPos":
				if (NotifyCT2PlayerPos != null)
				{
					JsonData jsonData5 = JsonMapper.ToObject(data);
					return NotifyCT2PlayerPos(HostID.Server, new RmiContext(), (int)jsonData5[0], (int)jsonData5[1], (long)jsonData5[2], (long)jsonData5[3], (long)jsonData5[4], (long)jsonData5[5]);
				}
				return true;
			case "NotifyCT2BattleResult":
				if (NotifyCT2BattleResult != null)
				{
					return NotifyCT2BattleResult(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGiaNhapLienMinh":
				if (NotifyGiaNhapLienMinh != null)
				{
					return NotifyGiaNhapLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyChapNhanGiaNhapLienMinh":
				if (NotifyChapNhanGiaNhapLienMinh != null)
				{
					return NotifyChapNhanGiaNhapLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyThoatLienMinh":
				if (NotifyThoatLienMinh != null)
				{
					return NotifyThoatLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyFinishNhiemVuLienMinh":
				if (NotifyFinishNhiemVuLienMinh != null)
				{
					return NotifyFinishNhiemVuLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyResetNhiemVuLienMinh":
				if (NotifyResetNhiemVuLienMinh != null)
				{
					return NotifyResetNhiemVuLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyRutGiaNhapLienMinh":
				if (NotifyRutGiaNhapLienMinh != null)
				{
					return NotifyRutGiaNhapLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDangHuongLienMinh":
				if (NotifyDangHuongLienMinh != null)
				{
					return NotifyDangHuongLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDoiThuongLienMinh":
				if (NotifyDoiThuongLienMinh != null)
				{
					return NotifyDoiThuongLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDoiMinhChu":
				if (NotifyDoiMinhChu != null)
				{
					return NotifyDoiMinhChu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDoiPhoMinhChu":
				if (NotifyDoiPhoMinhChu != null)
				{
					return NotifyDoiPhoMinhChu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyNangCapCongTrinh":
				if (NotifyNangCapCongTrinh != null)
				{
					return NotifyNangCapCongTrinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyCT2Teleport":
				if (NotifyCT2Teleport != null)
				{
					JsonData jsonData4 = JsonMapper.ToObject(data);
					return NotifyCT2Teleport(HostID.Server, new RmiContext(), (int)jsonData4[0], (int)jsonData4[1], (long)jsonData4[2], (long)jsonData4[3]);
				}
				return true;
			case "NotifyKhamNgoc":
				if (NotifyKhamNgoc != null)
				{
					return NotifyKhamNgoc(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGoNgoc":
				if (NotifyGoNgoc != null)
				{
					return NotifyGoNgoc(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetTopLienMinh":
				if (NotifyGetTopLienMinh != null)
				{
					return NotifyGetTopLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifySearchLienMinh":
				if (NotifySearchLienMinh != null)
				{
					return NotifySearchLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetThongTinLienMinh":
				if (NotifyGetThongTinLienMinh != null)
				{
					return NotifyGetThongTinLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyCT2NPCMove":
				if (NotifyCT2NPCMove != null)
				{
					JsonData jsonData3 = JsonMapper.ToObject(data);
					return NotifyCT2NPCMove(HostID.Server, new RmiContext(), (int)jsonData3[0], (int)jsonData3[1], (long)jsonData3[2], (long)jsonData3[3], (long)jsonData3[4], (long)jsonData3[5], (byte)(int)jsonData3[6]);
				}
				return true;
			case "NotifyCT2NPCIdle":
				if (NotifyCT2NPCIdle != null)
				{
					JsonData jsonData2 = JsonMapper.ToObject(data);
					return NotifyCT2NPCIdle(HostID.Server, new RmiContext(), (int)jsonData2[0], (int)jsonData2[1], (long)jsonData2[2], (long)jsonData2[3]);
				}
				return true;
			case "NotifyCT2NPCAttack":
				if (NotifyCT2NPCAttack != null)
				{
					JsonData jsonData = JsonMapper.ToObject(data);
					return NotifyCT2NPCAttack(HostID.Server, new RmiContext(), (int)jsonData[0], (int)jsonData[1], (long)jsonData[2], (long)jsonData[3]);
				}
				return true;
			case "NotifyCT2HPPercent":
				if (NotifyCT2HPPercent != null)
				{
					JsonData jsonData9 = JsonMapper.ToObject(data);
					return NotifyCT2HPPercent(HostID.Server, new RmiContext(), (int)jsonData9[0], (int)jsonData9[1], (long)jsonData9[2], (int)jsonData9[3]);
				}
				return true;
			case "NotifyNhanThuongDiHoaCungAll":
				if (NotifyNhanThuongDiHoaCungAll != null)
				{
					return NotifyNhanThuongDiHoaCungAll(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetDiHoaCungInfo":
				if (NotifyGetDiHoaCungInfo != null)
				{
					return NotifyGetDiHoaCungInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetTopDiHoaCung":
				if (NotifyGetTopDiHoaCung != null)
				{
					return NotifyGetTopDiHoaCung(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDuoiKhoiLienMinh":
				if (NotifyDuoiKhoiLienMinh != null)
				{
					return NotifyDuoiKhoiLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyCT2RuneXuatHien":
				if (NotifyCT2RuneXuatHien != null)
				{
					JsonData jsonData8 = JsonMapper.ToObject(data);
					return NotifyCT2RuneXuatHien(HostID.Server, new RmiContext(), (int)jsonData8[0], (int)jsonData8[1], (long)jsonData8[2], (long)jsonData8[3]);
				}
				return true;
			case "NotifyCT2GotRune":
				if (NotifyCT2GotRune != null)
				{
					JsonData jsonData7 = JsonMapper.ToObject(data);
					return NotifyCT2GotRune(HostID.Server, new RmiContext(), (int)jsonData7[0], (int)jsonData7[1], (int)jsonData7[2], (int)jsonData7[3], (long)jsonData7[4], (string)jsonData7[5]);
				}
				return true;
			case "NotifyCT2KetThuc":
				if (NotifyCT2KetThuc != null)
				{
					return NotifyCT2KetThuc(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyCT2PlayerState":
				if (NotifyCT2PlayerState != null)
				{
					JsonData jsonData6 = JsonMapper.ToObject(data);
					return NotifyCT2PlayerState(HostID.Server, new RmiContext(), (int)jsonData6[0], (int)jsonData6[1], (byte)(int)jsonData6[2]);
				}
				return true;
			case "NotifyCT2BangXepHangTuanNay":
				if (NotifyCT2BangXepHangTuanNay != null)
				{
					return NotifyCT2BangXepHangTuanNay(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyCT2BangXepHangTuanTruoc":
				if (NotifyCT2BangXepHangTuanTruoc != null)
				{
					return NotifyCT2BangXepHangTuanTruoc(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyCT2PhanThuongBXHTuan":
				if (NotifyCT2PhanThuongBXHTuan != null)
				{
					return NotifyCT2PhanThuongBXHTuan(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyThamGiaLuaTraiLienMinh":
				if (NotifyThamGiaLuaTraiLienMinh != null)
				{
					return NotifyThamGiaLuaTraiLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyLienMinhThoiLua":
				if (NotifyLienMinhThoiLua != null)
				{
					return NotifyLienMinhThoiLua(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyRoiKhoiLuaTraiLienMinh":
				if (NotifyRoiKhoiLuaTraiLienMinh != null)
				{
					return NotifyRoiKhoiLuaTraiLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifySetGioLuaTraiLienMinh":
				if (NotifySetGioLuaTraiLienMinh != null)
				{
					return NotifySetGioLuaTraiLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetExpLuaTrai":
				if (NotifyGetExpLuaTrai != null)
				{
					return NotifyGetExpLuaTrai(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifySuaThongBaoLienMinh":
				if (NotifySuaThongBaoLienMinh != null)
				{
					return NotifySuaThongBaoLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifySendChatLienMinh":
				if (NotifySendChatLienMinh != null)
				{
					return NotifySendChatLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyChatLienMinhInfo":
				if (NotifyChatLienMinhInfo != null)
				{
					return NotifyChatLienMinhInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyPhanRaTrangBi":
				if (NotifyPhanRaTrangBi != null)
				{
					return NotifyPhanRaTrangBi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyCT2BXH":
				if (NotifyCT2BXH != null)
				{
					return NotifyCT2BXH(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyUpdateLienMinhData":
				if (NotifyUpdateLienMinhData != null)
				{
					return NotifyUpdateLienMinhData(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyLapNguyenKhi":
				if (NotifyLapNguyenKhi != null)
				{
					return NotifyLapNguyenKhi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyThaoNguyenKhi":
				if (NotifyThaoNguyenKhi != null)
				{
					return NotifyThaoNguyenKhi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyNangCapNguyenKhi":
				if (NotifyNangCapNguyenKhi != null)
				{
					return NotifyNangCapNguyenKhi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyMuaNguyenKhi":
				if (NotifyMuaNguyenKhi != null)
				{
					return NotifyMuaNguyenKhi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyBangChienGetInfo":
				if (NotifyBangChienGetInfo != null)
				{
					return NotifyBangChienGetInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyBangChienVaoThanh":
				if (NotifyBangChienVaoThanh != null)
				{
					return NotifyBangChienVaoThanh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyBangChienRoiThanh":
				if (NotifyBangChienRoiThanh != null)
				{
					return NotifyBangChienRoiThanh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyBangChienDenCongThanh":
				if (NotifyBangChienDenCongThanh != null)
				{
					return NotifyBangChienDenCongThanh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyBangChienCongThanh":
				if (NotifyBangChienCongThanh != null)
				{
					return NotifyBangChienCongThanh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyChonHatGiong":
				if (NotifyChonHatGiong != null)
				{
					return NotifyChonHatGiong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyLayHatGiong":
				if (NotifyLayHatGiong != null)
				{
					return NotifyLayHatGiong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyTrongCay":
				if (NotifyTrongCay != null)
				{
					return NotifyTrongCay(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyThuHoach":
				if (NotifyThuHoach != null)
				{
					return NotifyThuHoach(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyAnTrom":
				if (NotifyAnTrom != null)
				{
					return NotifyAnTrom(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetAnTromList":
				if (NotifyGetAnTromList != null)
				{
					return NotifyGetAnTromList(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyBangChienListUserMove":
				if (NotifyBangChienListUserMove != null)
				{
					return NotifyBangChienListUserMove(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyBangChienGetPhanThuongThuThanh":
				if (NotifyBangChienGetPhanThuongThuThanh != null)
				{
					return NotifyBangChienGetPhanThuongThuThanh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyBangChienKetThuc":
				if (NotifyBangChienKetThuc != null)
				{
					return NotifyBangChienKetThuc(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifySendChatLienSrv":
				if (NotifySendChatLienSrv != null)
				{
					return NotifySendChatLienSrv(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetChatLienSrv":
				if (NotifyGetChatLienSrv != null)
				{
					return NotifyGetChatLienSrv(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetLinhDuocInfo":
				if (NotifyGetLinhDuocInfo != null)
				{
					return NotifyGetLinhDuocInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyBanhChungOthers":
				if (NotifyBanhChungOthers != null)
				{
					return NotifyBanhChungOthers(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyBanhChungGetInfo":
				if (NotifyBanhChungGetInfo != null)
				{
					return NotifyBanhChungGetInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyBanhChungNauBanh":
				if (NotifyBanhChungNauBanh != null)
				{
					return NotifyBanhChungNauBanh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyBanhChungNhatNguyenLieu":
				if (NotifyBanhChungNhatNguyenLieu != null)
				{
					return NotifyBanhChungNhatNguyenLieu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyBanhChungBXH":
				if (NotifyBanhChungBXH != null)
				{
					return NotifyBanhChungBXH(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyBanhChungGetPhanThuong":
				if (NotifyBanhChungGetPhanThuong != null)
				{
					return NotifyBanhChungGetPhanThuong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDangNhapNhanThuongTet":
				if (NotifyDangNhapNhanThuongTet != null)
				{
					return NotifyDangNhapNhanThuongTet(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyThaoNgua":
				if (NotifyThaoNgua != null)
				{
					return NotifyThaoNgua(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDungNgua":
				if (NotifyDungNgua != null)
				{
					return NotifyDungNgua(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyActiveNgua":
				if (NotifyActiveNgua != null)
				{
					return NotifyActiveNgua(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetGamerLinhDuoc":
				if (NotifyGetGamerLinhDuoc != null)
				{
					return NotifyGetGamerLinhDuoc(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyCT2HPTeam":
				if (NotifyCT2HPTeam != null)
				{
					return NotifyCT2HPTeam(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyCuongHoaBatQuaiTran":
				if (NotifyCuongHoaBatQuaiTran != null)
				{
					return NotifyCuongHoaBatQuaiTran(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifySetSoDoBatQuaiTran":
				if (NotifySetSoDoBatQuaiTran != null)
				{
					return NotifySetSoDoBatQuaiTran(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetLeagueData":
				if (NotifyGetLeagueData != null)
				{
					return NotifyGetLeagueData(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifySetDoiHinhThienCangTran":
				if (NotifySetDoiHinhThienCangTran != null)
				{
					return NotifySetDoiHinhThienCangTran(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyOpenDoiHinhThienCangTran":
				if (NotifyOpenDoiHinhThienCangTran != null)
				{
					return NotifyOpenDoiHinhThienCangTran(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetSieuCupData":
				if (NotifyGetSieuCupData != null)
				{
					return NotifyGetSieuCupData(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetSieuCupBattle":
				if (NotifyGetSieuCupBattle != null)
				{
					return NotifyGetSieuCupBattle(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifySieuCupDatCuoc":
				if (NotifySieuCupDatCuoc != null)
				{
					return NotifySieuCupDatCuoc(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifySubmitDoiHinhLeague":
				if (NotifySubmitDoiHinhLeague != null)
				{
					return NotifySubmitDoiHinhLeague(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyViewLeagueReplay":
				if (NotifyViewLeagueReplay != null)
				{
					return NotifyViewLeagueReplay(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyThamBaiSieuCup":
				if (NotifyThamBaiSieuCup != null)
				{
					return NotifyThamBaiSieuCup(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyVongQuay":
				if (NotifyVongQuay != null)
				{
					return NotifyVongQuay(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyLienDauData":
				if (NotifyLienDauData != null)
				{
					return NotifyLienDauData(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifySieuCupChampion":
				if (NotifySieuCupChampion != null)
				{
					return NotifySieuCupChampion(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyQMDInfo":
				if (NotifyQMDInfo != null)
				{
					return NotifyQMDInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyQMDSelect":
				if (NotifyQMDSelect != null)
				{
					return NotifyQMDSelect(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyQMDGetChiTietNPC":
				if (NotifyQMDGetChiTietNPC != null)
				{
					return NotifyQMDGetChiTietNPC(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyQMDGetBXH":
				if (NotifyQMDGetBXH != null)
				{
					return NotifyQMDGetBXH(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyQMDXongPha":
				if (NotifyQMDXongPha != null)
				{
					return NotifyQMDXongPha(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyBeQuanDeTu":
				if (NotifyBeQuanDeTu != null)
				{
					return NotifyBeQuanDeTu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDenGioCT":
				if (NotifyDenGioCT != null)
				{
					return NotifyDenGioCT(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyNhanThuongTichLuyNap":
				if (NotifyNhanThuongTichLuyNap != null)
				{
					return NotifyNhanThuongTichLuyNap(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyNhanThuongTichLuyTieu":
				if (NotifyNhanThuongTichLuyTieu != null)
				{
					return NotifyNhanThuongTichLuyTieu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetCacLoaiTop":
				if (NotifyGetCacLoaiTop != null)
				{
					return NotifyGetCacLoaiTop(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDoiTenBang":
				if (NotifyDoiTenBang != null)
				{
					return NotifyDoiTenBang(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetThongTinLienServer":
				if (NotifyGetThongTinLienServer != null)
				{
					return NotifyGetThongTinLienServer(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyBanPhaoHoa":
				if (NotifyBanPhaoHoa != null)
				{
					return NotifyBanPhaoHoa(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetTopPhaoHoa":
				if (NotifyGetTopPhaoHoa != null)
				{
					return NotifyGetTopPhaoHoa(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetPhanThuongPhaoHoa":
				if (NotifyGetPhanThuongPhaoHoa != null)
				{
					return NotifyGetPhanThuongPhaoHoa(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyBanPhaoHoaEvent":
				if (NotifyBanPhaoHoaEvent != null)
				{
					return NotifyBanPhaoHoaEvent(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetTopVongQuay":
				if (NotifyGetTopVongQuay != null)
				{
					return NotifyGetTopVongQuay(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyCreateCostume":
				if (NotifyCreateCostume != null)
				{
					return NotifyCreateCostume(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyTinhLuyenCostume":
				if (NotifyTinhLuyenCostume != null)
				{
					return NotifyTinhLuyenCostume(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyKhamNgocCostume":
				if (NotifyKhamNgocCostume != null)
				{
					return NotifyKhamNgocCostume(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGoNgocCostume":
				if (NotifyGoNgocCostume != null)
				{
					return NotifyGoNgocCostume(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyTakeOnCostume":
				if (NotifyTakeOnCostume != null)
				{
					return NotifyTakeOnCostume(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyTakeOffCostume":
				if (NotifyTakeOffCostume != null)
				{
					return NotifyTakeOffCostume(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyLinhThuongPhaoHoaEvent":
				if (NotifyLinhThuongPhaoHoaEvent != null)
				{
					return NotifyLinhThuongPhaoHoaEvent(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyChuyenSinhDeTu":
				if (NotifyChuyenSinhDeTu != null)
				{
					return NotifyChuyenSinhDeTu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetListOtherUser":
				if (NotifyGetListOtherUser != null)
				{
					return NotifyGetListOtherUser(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyBatThanThu":
				if (NotifyBatThanThu != null)
				{
					return NotifyBatThanThu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyNhanThuongDapNieu":
				if (NotifyNhanThuongDapNieu != null)
				{
					return NotifyNhanThuongDapNieu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyTruongThanhThanThu":
				if (NotifyTruongThanhThanThu != null)
				{
					return NotifyTruongThanhThanThu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyNangPhamThanThu":
				if (NotifyNangPhamThanThu != null)
				{
					return NotifyNangPhamThanThu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDoiThanThu":
				if (NotifyDoiThanThu != null)
				{
					return NotifyDoiThanThu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyThonPheThanThu":
				if (NotifyThonPheThanThu != null)
				{
					return NotifyThonPheThanThu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyTruyenCongThanThu":
				if (NotifyTruyenCongThanThu != null)
				{
					return NotifyTruyenCongThanThu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifySetBoPhapNhanVatBatQuai":
				if (NotifySetBoPhapNhanVatBatQuai != null)
				{
					return NotifySetBoPhapNhanVatBatQuai(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifySetNoiCongNhanVatBatQuai":
				if (NotifySetNoiCongNhanVatBatQuai != null)
				{
					return NotifySetNoiCongNhanVatBatQuai(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifySetTrangBiNhanVatBatQuai":
				if (NotifySetTrangBiNhanVatBatQuai != null)
				{
					return NotifySetTrangBiNhanVatBatQuai(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetThanThuDao":
				if (NotifyGetThanThuDao != null)
				{
					return NotifyGetThanThuDao(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifySetThanThu":
				if (NotifySetThanThu != null)
				{
					return NotifySetThanThu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetGuiTietKiem":
				if (NotifyGetGuiTietKiem != null)
				{
					return NotifyGetGuiTietKiem(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyThamGiaGuiTietKiem":
				if (NotifyThamGiaGuiTietKiem != null)
				{
					return NotifyThamGiaGuiTietKiem(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetThuong1MilUser":
				if (NotifyGetThuong1MilUser != null)
				{
					return NotifyGetThuong1MilUser(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyPopupThuong1MilUser":
				if (NotifyPopupThuong1MilUser != null)
				{
					return NotifyPopupThuong1MilUser(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyThuHoachSonMon":
				if (NotifyThuHoachSonMon != null)
				{
					return NotifyThuHoachSonMon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyXayDungSonMon":
				if (NotifyXayDungSonMon != null)
				{
					return NotifyXayDungSonMon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyTanCongSonMon":
				if (NotifyTanCongSonMon != null)
				{
					return NotifyTanCongSonMon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDoiHinhSonMon":
				if (NotifyDoiHinhSonMon != null)
				{
					return NotifyDoiHinhSonMon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDoThamSonMon":
				if (NotifyDoThamSonMon != null)
				{
					return NotifyDoThamSonMon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyMuaDoThanBi":
				if (NotifyMuaDoThanBi != null)
				{
					return NotifyMuaDoThanBi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetTopSonMon":
				if (NotifyGetTopSonMon != null)
				{
					return NotifyGetTopSonMon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDungLuyenTrangBi":
				if (NotifyDungLuyenTrangBi != null)
				{
					return NotifyDungLuyenTrangBi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyTayLuyenTrangBi":
				if (NotifyTayLuyenTrangBi != null)
				{
					return NotifyTayLuyenTrangBi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyKhaiQuangTrangBi":
				if (NotifyKhaiQuangTrangBi != null)
				{
					return NotifyKhaiQuangTrangBi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyConfirmTayLuyenTrangBi":
				if (NotifyConfirmTayLuyenTrangBi != null)
				{
					return NotifyConfirmTayLuyenTrangBi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetSonMonInfo":
				if (NotifyGetSonMonInfo != null)
				{
					return NotifyGetSonMonInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetLanhDiaInfo":
				if (NotifyGetLanhDiaInfo != null)
				{
					return NotifyGetLanhDiaInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyUnLockVoCong":
				if (NotifyUnLockVoCong != null)
				{
					return NotifyUnLockVoCong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyQuayBacMayMan":
				if (NotifyQuayBacMayMan != null)
				{
					return NotifyQuayBacMayMan(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyMoveLanhDia":
				if (NotifyMoveLanhDia != null)
				{
					return NotifyMoveLanhDia(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyUpdateLanhDiaData":
				if (NotifyUpdateLanhDiaData != null)
				{
					return NotifyUpdateLanhDiaData(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyQuayTuBaoBon":
				if (NotifyQuayTuBaoBon != null)
				{
					return NotifyQuayTuBaoBon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetTopMoRuong":
				if (NotifyGetTopMoRuong != null)
				{
					return NotifyGetTopMoRuong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyQuayThienMaHaPhong":
				if (NotifyQuayThienMaHaPhong != null)
				{
					return NotifyQuayThienMaHaPhong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetTayVucInfo":
				if (NotifyGetTayVucInfo != null)
				{
					return NotifyGetTayVucInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyMuaDoTayVuc":
				if (NotifyMuaDoTayVuc != null)
				{
					return NotifyMuaDoTayVuc(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifySpawnNienThu":
				if (NotifySpawnNienThu != null)
				{
					return NotifySpawnNienThu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifySummonNienThu":
				if (NotifySummonNienThu != null)
				{
					return NotifySummonNienThu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDanhNienThu":
				if (NotifyDanhNienThu != null)
				{
					return NotifyDanhNienThu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyThamGiaNienThu":
				if (NotifyThamGiaNienThu != null)
				{
					return NotifyThamGiaNienThu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyNhanThuongNapHangNgay":
				if (NotifyNhanThuongNapHangNgay != null)
				{
					return NotifyNhanThuongNapHangNgay(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetTopNienThu":
				if (NotifyGetTopNienThu != null)
				{
					return NotifyGetTopNienThu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyNopLenhBaiNienThu":
				if (NotifyNopLenhBaiNienThu != null)
				{
					return NotifyNopLenhBaiNienThu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetTopLanhDia":
				if (NotifyGetTopLanhDia != null)
				{
					return NotifyGetTopLanhDia(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifySelectStartNgua":
				if (NotifySelectStartNgua != null)
				{
					return NotifySelectStartNgua(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyUpdateNienThu":
				if (NotifyUpdateNienThu != null)
				{
					return NotifyUpdateNienThu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetDiemMoRuong":
				if (NotifyGetDiemMoRuong != null)
				{
					return NotifyGetDiemMoRuong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyUpdateDailyActivities":
				if (NotifyUpdateDailyActivities != null)
				{
					return NotifyUpdateDailyActivities(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyThuongDailyActivities":
				if (NotifyThuongDailyActivities != null)
				{
					return NotifyThuongDailyActivities(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyThienMaQuaySlot":
				if (NotifyThienMaQuaySlot != null)
				{
					return NotifyThienMaQuaySlot(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyThienMaLenhCreate":
				if (NotifyThienMaLenhCreate != null)
				{
					return NotifyThienMaLenhCreate(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyThienMaUpgradeSlot":
				if (NotifyThienMaUpgradeSlot != null)
				{
					return NotifyThienMaUpgradeSlot(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyThienMaOpenSlot":
				if (NotifyThienMaOpenSlot != null)
				{
					return NotifyThienMaOpenSlot(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyThienMaEquip":
				if (NotifyThienMaEquip != null)
				{
					return NotifyThienMaEquip(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyThienMaQuaySlotConfirm":
				if (NotifyThienMaQuaySlotConfirm != null)
				{
					return NotifyThienMaQuaySlotConfirm(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifySetTonHieu":
				if (NotifySetTonHieu != null)
				{
					return NotifySetTonHieu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifySetTrangBiHoangKim":
				if (NotifySetTrangBiHoangKim != null)
				{
					return NotifySetTrangBiHoangKim(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetTopDaiHoiVoLam":
				if (NotifyGetTopDaiHoiVoLam != null)
				{
					return NotifyGetTopDaiHoiVoLam(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDanhAnDanhCaoThu":
				if (NotifyDanhAnDanhCaoThu != null)
				{
					return NotifyDanhAnDanhCaoThu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyStartBoiDuongTrangBi":
				if (NotifyStartBoiDuongTrangBi != null)
				{
					return NotifyStartBoiDuongTrangBi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyEndBoiDuongTrangBi":
				if (NotifyEndBoiDuongTrangBi != null)
				{
					return NotifyEndBoiDuongTrangBi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyRutQueTienNhan":
				if (NotifyRutQueTienNhan != null)
				{
					return NotifyRutQueTienNhan(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetBaoKhoInfo":
				if (NotifyGetBaoKhoInfo != null)
				{
					return NotifyGetBaoKhoInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyCuopBaoKho":
				if (NotifyCuopBaoKho != null)
				{
					return NotifyCuopBaoKho(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyThuHoachBaoKho":
				if (NotifyThuHoachBaoKho != null)
				{
					return NotifyThuHoachBaoKho(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetListBaoKho":
				if (NotifyGetListBaoKho != null)
				{
					return NotifyGetListBaoKho(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyQuayCamCungBiBao":
				if (NotifyQuayCamCungBiBao != null)
				{
					return NotifyQuayCamCungBiBao(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyNhanThuongCamCung":
				if (NotifyNhanThuongCamCung != null)
				{
					return NotifyNhanThuongCamCung(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyGetHoaVangInfo":
				if (NotifyGetHoaVangInfo != null)
				{
					return NotifyGetHoaVangInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyHoaVang":
				if (NotifyHoaVang != null)
				{
					return NotifyHoaVang(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyTestProudNet":
				if (NotifyTestProudNet != null)
				{
					return NotifyTestProudNet(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyTrangBiHuyenKhi":
				if (NotifyTrangBiHuyenKhi != null)
				{
					return NotifyTrangBiHuyenKhi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyThangCapHuyenKhi":
				if (NotifyThangCapHuyenKhi != null)
				{
					return NotifyThangCapHuyenKhi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyChienHonChangeBuff":
				if (NotifyChienHonChangeBuff != null)
				{
					return NotifyChienHonChangeBuff(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyStartBoiDuongChienHon":
				if (NotifyStartBoiDuongChienHon != null)
				{
					return NotifyStartBoiDuongChienHon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyEndBoiDuongChienHon":
				if (NotifyEndBoiDuongChienHon != null)
				{
					return NotifyEndBoiDuongChienHon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyTrieuHoiChienHon":
				if (NotifyTrieuHoiChienHon != null)
				{
					return NotifyTrieuHoiChienHon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyUseTanHonTangLevelChienHon":
				if (NotifyUseTanHonTangLevelChienHon != null)
				{
					return NotifyUseTanHonTangLevelChienHon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyDotPhaChienHon":
				if (NotifyDotPhaChienHon != null)
				{
					return NotifyDotPhaChienHon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyCheTaoHuyenKhi":
				if (NotifyCheTaoHuyenKhi != null)
				{
					return NotifyCheTaoHuyenKhi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyQuayDiemHoaVang":
				if (NotifyQuayDiemHoaVang != null)
				{
					return NotifyQuayDiemHoaVang(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyUseTuiThan":
				if (NotifyUseTuiThan != null)
				{
					return NotifyUseTuiThan(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyNhanThuongDailyKNB":
				if (NotifyNhanThuongDailyKNB != null)
				{
					return NotifyNhanThuongDailyKNB(HostID.Server, new RmiContext(), data);
				}
				return true;
			default:
				return true;
			}
		}

		public bool HasHandler(string rmiName)
		{
			switch (rmiName)
			{
			case "NotifyNextLogonSuccess":
				return NotifyNextLogonSuccess != null;
			case "NotifyGetBattleResultSuccess":
				return NotifyGetBattleResultSuccess != null;
			case "NotifyGetListLKSuccess":
				return NotifyGetListLKSuccess != null;
			case "NotifyAck":
				return NotifyAck != null;
			case "NotifyTingTing":
				return NotifyTingTing != null;
			case "NotifyGetInfoSuccess":
				return NotifyGetInfoSuccess != null;
			case "NotifyListPositionInMain":
				return NotifyListPositionInMain != null;
			case "NotifyGetLuanKiemInfo":
				return NotifyGetLuanKiemInfo != null;
			case "NotifyCapNhatDiemLuanKiem":
				return NotifyCapNhatDiemLuanKiem != null;
			case "NotifyNhanThuongLuanKiem":
				return NotifyNhanThuongLuanKiem != null;
			case "NotifyDoiThuongLuanKiem":
				return NotifyDoiThuongLuanKiem != null;
			case "NotifyDauLuanKiem":
				return NotifyDauLuanKiem != null;
			case "NotifyDanhGiangHo":
				return NotifyDanhGiangHo != null;
			case "NotifySetTrangBi":
				return NotifySetTrangBi != null;
			case "NotifySetDoiHinh":
				return NotifySetDoiHinh != null;
			case "NotifyChatMsg":
				return NotifyChatMsg != null;
			case "NotifySetHeroData":
				return NotifySetHeroData != null;
			case "NotifySetVoCong":
				return NotifySetVoCong != null;
			case "NotifySetTranHinh":
				return NotifySetTranHinh != null;
			case "NotifyStartBoiDuong":
				return NotifyStartBoiDuong != null;
			case "NotifyEndBoiDuong":
				return NotifyEndBoiDuong != null;
			case "NotifyTuLuyenDeTu":
				return NotifyTuLuyenDeTu != null;
			case "NotifyTrieuHoiDeTuBangHon":
				return NotifyTrieuHoiDeTuBangHon != null;
			case "NotifyTruyenCong":
				return NotifyTruyenCong != null;
			case "NotifyInTimeDanhDongNhan":
				return NotifyInTimeDanhDongNhan != null;
			case "NotifyOutTimeDanhDongNhan":
				return NotifyOutTimeDanhDongNhan != null;
			case "NotifyGetDongNhanInfo":
				return NotifyGetDongNhanInfo != null;
			case "NotifyDanhDongNhan":
				return NotifyDanhDongNhan != null;
			case "NotifyCuongHoaTrangBi":
				return NotifyCuongHoaTrangBi != null;
			case "NotifyBanTrangBi":
				return NotifyBanTrangBi != null;
			case "NotifyGhepManhTrangBi":
				return NotifyGhepManhTrangBi != null;
			case "NotifyGhepManhVoCong":
				return NotifyGhepManhVoCong != null;
			case "NotifyTinhLuyenTrangBi":
				return NotifyTinhLuyenTrangBi != null;
			case "NotifySetDoiHinhHoTro":
				return NotifySetDoiHinhHoTro != null;
			case "NotifyOpenDoiHinhHoTro":
				return NotifyOpenDoiHinhHoTro != null;
			case "NotifyThamNgoVoCong":
				return NotifyThamNgoVoCong != null;
			case "NotifyTinhLuyenVoCong":
				return NotifyTinhLuyenVoCong != null;
			case "NotifyLayDeTu":
				return NotifyLayDeTu != null;
			case "NotifyBuyVatPham":
				return NotifyBuyVatPham != null;
			case "NotifyCaoNhan":
				return NotifyCaoNhan != null;
			case "NotifyThuongNhan":
				return NotifyThuongNhan != null;
			case "NotifyBanDo":
				return NotifyBanDo != null;
			case "NotifyBangHuu":
				return NotifyBangHuu != null;
			case "NotifyTyThi":
				return NotifyTyThi != null;
			case "NotifyBuyLeBao":
				return NotifyBuyLeBao != null;
			case "NotifyHoiTheLuc":
				return NotifyHoiTheLuc != null;
			case "NotifySelectStartDeTu":
				return NotifySelectStartDeTu != null;
			case "NotifyKyNgoThamBai":
				return NotifyKyNgoThamBai != null;
			case "NotifyHuyetChienInfo":
				return NotifyHuyetChienInfo != null;
			case "NotifyStartHuyetChien":
				return NotifyStartHuyetChien != null;
			case "NotifyHoiSinhHuyetChien":
				return NotifyHoiSinhHuyetChien != null;
			case "NotifyHuyetChienTangThuocTinh":
				return NotifyHuyetChienTangThuocTinh != null;
			case "NotifyDanhHuyetChien":
				return NotifyDanhHuyetChien != null;
			case "NotifyNhanThuongHuyetChien":
				return NotifyNhanThuongHuyetChien != null;
			case "NotifyGetTopHuyetChien":
				return NotifyGetTopHuyetChien != null;
			case "NotifyOpenHop":
				return NotifyOpenHop != null;
			case "NotifyRequestSetDoiHinhAndTranHinh":
				return NotifyRequestSetDoiHinhAndTranHinh != null;
			case "NotifyNhanThuongGiangHo":
				return NotifyNhanThuongGiangHo != null;
			case "NotifyDanhNhanhGiangHo":
				return NotifyDanhNhanhGiangHo != null;
			case "NotifyAnGaGiangHo":
				return NotifyAnGaGiangHo != null;
			case "NotifyResetLuotGiangHo":
				return NotifyResetLuotGiangHo != null;
			case "NotifyDanhDanhSon":
				return NotifyDanhDanhSon != null;
			case "NotifyMoThuongDanhSon":
				return NotifyMoThuongDanhSon != null;
			case "NotifyMoHetDanhSon":
				return NotifyMoHetDanhSon != null;
			case "NotifyVuotAiDanhSon":
				return NotifyVuotAiDanhSon != null;
			case "NotifyChonDongDoiDanhSon":
				return NotifyChonDongDoiDanhSon != null;
			case "NotifyGetDongDoiDanhSon":
				return NotifyGetDongDoiDanhSon != null;
			case "NotifyHuaNguyen":
				return NotifyHuaNguyen != null;
			case "NotifyLenCapNhanThuong":
				return NotifyLenCapNhanThuong != null;
			case "NotifyThanTai":
				return NotifyThanTai != null;
			case "NotifyXocDiaInfo":
				return NotifyXocDiaInfo != null;
			case "NotifyChoiXocDia":
				return NotifyChoiXocDia != null;
			case "NotifyCuuVienTieuPhong":
				return NotifyCuuVienTieuPhong != null;
			case "NotifyNhanRuongThachSanh":
				return NotifyNhanRuongThachSanh != null;
			case "NotifyPaymentConfirm":
				return NotifyPaymentConfirm != null;
			case "NotifyDangNhapNhanThuong":
				return NotifyDangNhapNhanThuong != null;
			case "NotifyUongRuouTieuPhong":
				return NotifyUongRuouTieuPhong != null;
			case "NotifyGetDoiRuouInfo":
				return NotifyGetDoiRuouInfo != null;
			case "NotifyDoiRuou":
				return NotifyDoiRuou != null;
			case "NotifyBatCoc":
				return NotifyBatCoc != null;
			case "NotifyChuocThan":
				return NotifyChuocThan != null;
			case "NotifySearchBanBe":
				return NotifySearchBanBe != null;
			case "NotifyNhanDuocThachDau":
				return NotifyNhanDuocThachDau != null;
			case "NotifyKetQuaThachDau":
				return NotifyKetQuaThachDau != null;
			case "NotifyDuocAddBanBe":
				return NotifyDuocAddBanBe != null;
			case "NotifyAddBanBe":
				return NotifyAddBanBe != null;
			case "NotifyAcceptBanBe":
				return NotifyAcceptBanBe != null;
			case "NotifyDeleteBanBe":
				return NotifyDeleteBanBe != null;
			case "NotifyDangNhapTrungTK":
				return NotifyDangNhapTrungTK != null;
			case "NotifyBanBeCuuThuInfo":
				return NotifyBanBeCuuThuInfo != null;
			case "NotifyChatInfo":
				return NotifyChatInfo != null;
			case "NotifyGetChatAll":
				return NotifyGetChatAll != null;
			case "NotifyDatTenMonPhai":
				return NotifyDatTenMonPhai != null;
			case "NotifyULinhInfo":
				return NotifyULinhInfo != null;
			case "NotifyDoiItemULinh":
				return NotifyDoiItemULinh != null;
			case "NotifyKnbRefreshULinh":
				return NotifyKnbRefreshULinh != null;
			case "NotifyDoiThuongULinh":
				return NotifyDoiThuongULinh != null;
			case "NotifyGetTopULinh":
				return NotifyGetTopULinh != null;
			case "NotifyKichHoatGiftCode":
				return NotifyKichHoatGiftCode != null;
			case "NotifyUseCustomItem":
				return NotifyUseCustomItem != null;
			case "NotifyBuyVatPhamAndUse":
				return NotifyBuyVatPhamAndUse != null;
			case "NotifyHighlight":
				return NotifyHighlight != null;
			case "NotifyBaoTriServer":
				return NotifyBaoTriServer != null;
			case "NotifyUseMailPhanThuong":
				return NotifyUseMailPhanThuong != null;
			case "NotifyReadAllMail":
				return NotifyReadAllMail != null;
			case "NotifyRefreshMail":
				return NotifyRefreshMail != null;
			case "NotifyUseRuongThan":
				return NotifyUseRuongThan != null;
			case "NotifyBatTho":
				return NotifyBatTho != null;
			case "NotifyGetFriendsDoiHinh":
				return NotifyGetFriendsDoiHinh != null;
			case "NotifyXemThongTinMonPhai":
				return NotifyXemThongTinMonPhai != null;
			case "NotifySendMail":
				return NotifySendMail != null;
			case "NotifyDangNhapQuayXoSo":
				return NotifyDangNhapQuayXoSo != null;
			case "NotifyMatDongBoDuLieu":
				return NotifyMatDongBoDuLieu != null;
			case "NotifyTangTheLuc":
				return NotifyTangTheLuc != null;
			case "NotifyDuocTangTheLuc":
				return NotifyDuocTangTheLuc != null;
			case "NotifyDoiDo":
				return NotifyDoiDo != null;
			case "NotifyGetDuaTopLevelInfo":
				return NotifyGetDuaTopLevelInfo != null;
			case "NotifyGetDuaTopLuanKiemInfo":
				return NotifyGetDuaTopLuanKiemInfo != null;
			case "NotifyListOnlineInMain":
				return NotifyListOnlineInMain != null;
			case "NotifyCT2PlayerAppear":
				return NotifyCT2PlayerAppear != null;
			case "NotifyThamGiaCT2":
				return NotifyThamGiaCT2 != null;
			case "NotifyListCT2":
				return NotifyListCT2 != null;
			case "NotifyLapLienMinh":
				return NotifyLapLienMinh != null;
			case "NotifyCT2PlayerPos":
				return NotifyCT2PlayerPos != null;
			case "NotifyCT2BattleResult":
				return NotifyCT2BattleResult != null;
			case "NotifyGiaNhapLienMinh":
				return NotifyGiaNhapLienMinh != null;
			case "NotifyChapNhanGiaNhapLienMinh":
				return NotifyChapNhanGiaNhapLienMinh != null;
			case "NotifyThoatLienMinh":
				return NotifyThoatLienMinh != null;
			case "NotifyFinishNhiemVuLienMinh":
				return NotifyFinishNhiemVuLienMinh != null;
			case "NotifyResetNhiemVuLienMinh":
				return NotifyResetNhiemVuLienMinh != null;
			case "NotifyRutGiaNhapLienMinh":
				return NotifyRutGiaNhapLienMinh != null;
			case "NotifyDangHuongLienMinh":
				return NotifyDangHuongLienMinh != null;
			case "NotifyDoiThuongLienMinh":
				return NotifyDoiThuongLienMinh != null;
			case "NotifyDoiMinhChu":
				return NotifyDoiMinhChu != null;
			case "NotifyDoiPhoMinhChu":
				return NotifyDoiPhoMinhChu != null;
			case "NotifyNangCapCongTrinh":
				return NotifyNangCapCongTrinh != null;
			case "NotifyCT2Teleport":
				return NotifyCT2Teleport != null;
			case "NotifyKhamNgoc":
				return NotifyKhamNgoc != null;
			case "NotifyGoNgoc":
				return NotifyGoNgoc != null;
			case "NotifyGetTopLienMinh":
				return NotifyGetTopLienMinh != null;
			case "NotifySearchLienMinh":
				return NotifySearchLienMinh != null;
			case "NotifyGetThongTinLienMinh":
				return NotifyGetThongTinLienMinh != null;
			case "NotifyCT2NPCMove":
				return NotifyCT2NPCMove != null;
			case "NotifyCT2NPCIdle":
				return NotifyCT2NPCIdle != null;
			case "NotifyCT2NPCAttack":
				return NotifyCT2NPCAttack != null;
			case "NotifyCT2HPPercent":
				return NotifyCT2HPPercent != null;
			case "NotifyNhanThuongDiHoaCungAll":
				return NotifyNhanThuongDiHoaCungAll != null;
			case "NotifyGetDiHoaCungInfo":
				return NotifyGetDiHoaCungInfo != null;
			case "NotifyGetTopDiHoaCung":
				return NotifyGetTopDiHoaCung != null;
			case "NotifyDuoiKhoiLienMinh":
				return NotifyDuoiKhoiLienMinh != null;
			case "NotifyCT2RuneXuatHien":
				return NotifyCT2RuneXuatHien != null;
			case "NotifyCT2GotRune":
				return NotifyCT2GotRune != null;
			case "NotifyCT2KetThuc":
				return NotifyCT2KetThuc != null;
			case "NotifyCT2PlayerState":
				return NotifyCT2PlayerState != null;
			case "NotifyCT2BangXepHangTuanNay":
				return NotifyCT2BangXepHangTuanNay != null;
			case "NotifyCT2BangXepHangTuanTruoc":
				return NotifyCT2BangXepHangTuanTruoc != null;
			case "NotifyCT2PhanThuongBXHTuan":
				return NotifyCT2PhanThuongBXHTuan != null;
			case "NotifyThamGiaLuaTraiLienMinh":
				return NotifyThamGiaLuaTraiLienMinh != null;
			case "NotifyLienMinhThoiLua":
				return NotifyLienMinhThoiLua != null;
			case "NotifyRoiKhoiLuaTraiLienMinh":
				return NotifyRoiKhoiLuaTraiLienMinh != null;
			case "NotifySetGioLuaTraiLienMinh":
				return NotifySetGioLuaTraiLienMinh != null;
			case "NotifyGetExpLuaTrai":
				return NotifyGetExpLuaTrai != null;
			case "NotifySuaThongBaoLienMinh":
				return NotifySuaThongBaoLienMinh != null;
			case "NotifySendChatLienMinh":
				return NotifySendChatLienMinh != null;
			case "NotifyChatLienMinhInfo":
				return NotifyChatLienMinhInfo != null;
			case "NotifyPhanRaTrangBi":
				return NotifyPhanRaTrangBi != null;
			case "NotifyCT2BXH":
				return NotifyCT2BXH != null;
			case "NotifyUpdateLienMinhData":
				return NotifyUpdateLienMinhData != null;
			case "NotifyLapNguyenKhi":
				return NotifyLapNguyenKhi != null;
			case "NotifyThaoNguyenKhi":
				return NotifyThaoNguyenKhi != null;
			case "NotifyNangCapNguyenKhi":
				return NotifyNangCapNguyenKhi != null;
			case "NotifyMuaNguyenKhi":
				return NotifyMuaNguyenKhi != null;
			case "NotifyBangChienGetInfo":
				return NotifyBangChienGetInfo != null;
			case "NotifyBangChienVaoThanh":
				return NotifyBangChienVaoThanh != null;
			case "NotifyBangChienRoiThanh":
				return NotifyBangChienRoiThanh != null;
			case "NotifyBangChienDenCongThanh":
				return NotifyBangChienDenCongThanh != null;
			case "NotifyBangChienCongThanh":
				return NotifyBangChienCongThanh != null;
			case "NotifyChonHatGiong":
				return NotifyChonHatGiong != null;
			case "NotifyLayHatGiong":
				return NotifyLayHatGiong != null;
			case "NotifyTrongCay":
				return NotifyTrongCay != null;
			case "NotifyThuHoach":
				return NotifyThuHoach != null;
			case "NotifyAnTrom":
				return NotifyAnTrom != null;
			case "NotifyGetAnTromList":
				return NotifyGetAnTromList != null;
			case "NotifyBangChienListUserMove":
				return NotifyBangChienListUserMove != null;
			case "NotifyBangChienGetPhanThuongThuThanh":
				return NotifyBangChienGetPhanThuongThuThanh != null;
			case "NotifyBangChienKetThuc":
				return NotifyBangChienKetThuc != null;
			case "NotifySendChatLienSrv":
				return NotifySendChatLienSrv != null;
			case "NotifyGetChatLienSrv":
				return NotifyGetChatLienSrv != null;
			case "NotifyGetLinhDuocInfo":
				return NotifyGetLinhDuocInfo != null;
			case "NotifyBanhChungOthers":
				return NotifyBanhChungOthers != null;
			case "NotifyBanhChungGetInfo":
				return NotifyBanhChungGetInfo != null;
			case "NotifyBanhChungNauBanh":
				return NotifyBanhChungNauBanh != null;
			case "NotifyBanhChungNhatNguyenLieu":
				return NotifyBanhChungNhatNguyenLieu != null;
			case "NotifyBanhChungBXH":
				return NotifyBanhChungBXH != null;
			case "NotifyBanhChungGetPhanThuong":
				return NotifyBanhChungGetPhanThuong != null;
			case "NotifyDangNhapNhanThuongTet":
				return NotifyDangNhapNhanThuongTet != null;
			case "NotifyThaoNgua":
				return NotifyThaoNgua != null;
			case "NotifyDungNgua":
				return NotifyDungNgua != null;
			case "NotifyActiveNgua":
				return NotifyActiveNgua != null;
			case "NotifyGetGamerLinhDuoc":
				return NotifyGetGamerLinhDuoc != null;
			case "NotifyCT2HPTeam":
				return NotifyCT2HPTeam != null;
			case "NotifyCuongHoaBatQuaiTran":
				return NotifyCuongHoaBatQuaiTran != null;
			case "NotifySetSoDoBatQuaiTran":
				return NotifySetSoDoBatQuaiTran != null;
			case "NotifyGetLeagueData":
				return NotifyGetLeagueData != null;
			case "NotifySetDoiHinhThienCangTran":
				return NotifySetDoiHinhThienCangTran != null;
			case "NotifyOpenDoiHinhThienCangTran":
				return NotifyOpenDoiHinhThienCangTran != null;
			case "NotifyGetSieuCupData":
				return NotifyGetSieuCupData != null;
			case "NotifyGetSieuCupBattle":
				return NotifyGetSieuCupBattle != null;
			case "NotifySieuCupDatCuoc":
				return NotifySieuCupDatCuoc != null;
			case "NotifySubmitDoiHinhLeague":
				return NotifySubmitDoiHinhLeague != null;
			case "NotifyViewLeagueReplay":
				return NotifyViewLeagueReplay != null;
			case "NotifyThamBaiSieuCup":
				return NotifyThamBaiSieuCup != null;
			case "NotifyVongQuay":
				return NotifyVongQuay != null;
			case "NotifyLienDauData":
				return NotifyLienDauData != null;
			case "NotifySieuCupChampion":
				return NotifySieuCupChampion != null;
			case "NotifyQMDInfo":
				return NotifyQMDInfo != null;
			case "NotifyQMDSelect":
				return NotifyQMDSelect != null;
			case "NotifyQMDGetChiTietNPC":
				return NotifyQMDGetChiTietNPC != null;
			case "NotifyQMDGetBXH":
				return NotifyQMDGetBXH != null;
			case "NotifyQMDXongPha":
				return NotifyQMDXongPha != null;
			case "NotifyBeQuanDeTu":
				return NotifyBeQuanDeTu != null;
			case "NotifyDenGioCT":
				return NotifyDenGioCT != null;
			case "NotifyNhanThuongTichLuyNap":
				return NotifyNhanThuongTichLuyNap != null;
			case "NotifyNhanThuongTichLuyTieu":
				return NotifyNhanThuongTichLuyTieu != null;
			case "NotifyGetCacLoaiTop":
				return NotifyGetCacLoaiTop != null;
			case "NotifyDoiTenBang":
				return NotifyDoiTenBang != null;
			case "NotifyGetThongTinLienServer":
				return NotifyGetThongTinLienServer != null;
			case "NotifyBanPhaoHoa":
				return NotifyBanPhaoHoa != null;
			case "NotifyGetTopPhaoHoa":
				return NotifyGetTopPhaoHoa != null;
			case "NotifyGetPhanThuongPhaoHoa":
				return NotifyGetPhanThuongPhaoHoa != null;
			case "NotifyBanPhaoHoaEvent":
				return NotifyBanPhaoHoaEvent != null;
			case "NotifyGetTopVongQuay":
				return NotifyGetTopVongQuay != null;
			case "NotifyCreateCostume":
				return NotifyCreateCostume != null;
			case "NotifyTinhLuyenCostume":
				return NotifyTinhLuyenCostume != null;
			case "NotifyKhamNgocCostume":
				return NotifyKhamNgocCostume != null;
			case "NotifyGoNgocCostume":
				return NotifyGoNgocCostume != null;
			case "NotifyTakeOnCostume":
				return NotifyTakeOnCostume != null;
			case "NotifyTakeOffCostume":
				return NotifyTakeOffCostume != null;
			case "NotifyLinhThuongPhaoHoaEvent":
				return NotifyLinhThuongPhaoHoaEvent != null;
			case "NotifyChuyenSinhDeTu":
				return NotifyChuyenSinhDeTu != null;
			case "NotifyGetListOtherUser":
				return NotifyGetListOtherUser != null;
			case "NotifyBatThanThu":
				return NotifyBatThanThu != null;
			case "NotifyNhanThuongDapNieu":
				return NotifyNhanThuongDapNieu != null;
			case "NotifyTruongThanhThanThu":
				return NotifyTruongThanhThanThu != null;
			case "NotifyNangPhamThanThu":
				return NotifyNangPhamThanThu != null;
			case "NotifyDoiThanThu":
				return NotifyDoiThanThu != null;
			case "NotifyThonPheThanThu":
				return NotifyThonPheThanThu != null;
			case "NotifyTruyenCongThanThu":
				return NotifyTruyenCongThanThu != null;
			case "NotifySetBoPhapNhanVatBatQuai":
				return NotifySetBoPhapNhanVatBatQuai != null;
			case "NotifySetNoiCongNhanVatBatQuai":
				return NotifySetNoiCongNhanVatBatQuai != null;
			case "NotifySetTrangBiNhanVatBatQuai":
				return NotifySetTrangBiNhanVatBatQuai != null;
			case "NotifyGetThanThuDao":
				return NotifyGetThanThuDao != null;
			case "NotifySetThanThu":
				return NotifySetThanThu != null;
			case "NotifyGetGuiTietKiem":
				return NotifyGetGuiTietKiem != null;
			case "NotifyThamGiaGuiTietKiem":
				return NotifyThamGiaGuiTietKiem != null;
			case "NotifyGetThuong1MilUser":
				return NotifyGetThuong1MilUser != null;
			case "NotifyPopupThuong1MilUser":
				return NotifyPopupThuong1MilUser != null;
			case "NotifyThuHoachSonMon":
				return NotifyThuHoachSonMon != null;
			case "NotifyXayDungSonMon":
				return NotifyXayDungSonMon != null;
			case "NotifyTanCongSonMon":
				return NotifyTanCongSonMon != null;
			case "NotifyDoiHinhSonMon":
				return NotifyDoiHinhSonMon != null;
			case "NotifyDoThamSonMon":
				return NotifyDoThamSonMon != null;
			case "NotifyMuaDoThanBi":
				return NotifyMuaDoThanBi != null;
			case "NotifyGetTopSonMon":
				return NotifyGetTopSonMon != null;
			case "NotifyDungLuyenTrangBi":
				return NotifyDungLuyenTrangBi != null;
			case "NotifyTayLuyenTrangBi":
				return NotifyTayLuyenTrangBi != null;
			case "NotifyKhaiQuangTrangBi":
				return NotifyKhaiQuangTrangBi != null;
			case "NotifyConfirmTayLuyenTrangBi":
				return NotifyConfirmTayLuyenTrangBi != null;
			case "NotifyGetSonMonInfo":
				return NotifyGetSonMonInfo != null;
			case "NotifyGetLanhDiaInfo":
				return NotifyGetLanhDiaInfo != null;
			case "NotifyUnLockVoCong":
				return NotifyUnLockVoCong != null;
			case "NotifyQuayBacMayMan":
				return NotifyQuayBacMayMan != null;
			case "NotifyMoveLanhDia":
				return NotifyMoveLanhDia != null;
			case "NotifyUpdateLanhDiaData":
				return NotifyUpdateLanhDiaData != null;
			case "NotifyQuayTuBaoBon":
				return NotifyQuayTuBaoBon != null;
			case "NotifyGetTopMoRuong":
				return NotifyGetTopMoRuong != null;
			case "NotifyQuayThienMaHaPhong":
				return NotifyQuayThienMaHaPhong != null;
			case "NotifyGetTayVucInfo":
				return NotifyGetTayVucInfo != null;
			case "NotifyMuaDoTayVuc":
				return NotifyMuaDoTayVuc != null;
			case "NotifySpawnNienThu":
				return NotifySpawnNienThu != null;
			case "NotifySummonNienThu":
				return NotifySummonNienThu != null;
			case "NotifyDanhNienThu":
				return NotifyDanhNienThu != null;
			case "NotifyThamGiaNienThu":
				return NotifyThamGiaNienThu != null;
			case "NotifyNhanThuongNapHangNgay":
				return NotifyNhanThuongNapHangNgay != null;
			case "NotifyGetTopNienThu":
				return NotifyGetTopNienThu != null;
			case "NotifyNopLenhBaiNienThu":
				return NotifyNopLenhBaiNienThu != null;
			case "NotifyGetTopLanhDia":
				return NotifyGetTopLanhDia != null;
			case "NotifySelectStartNgua":
				return NotifySelectStartNgua != null;
			case "NotifyUpdateNienThu":
				return NotifyUpdateNienThu != null;
			case "NotifyGetDiemMoRuong":
				return NotifyGetDiemMoRuong != null;
			case "NotifyUpdateDailyActivities":
				return NotifyUpdateDailyActivities != null;
			case "NotifyThuongDailyActivities":
				return NotifyThuongDailyActivities != null;
			case "NotifyThienMaQuaySlot":
				return NotifyThienMaQuaySlot != null;
			case "NotifyThienMaLenhCreate":
				return NotifyThienMaLenhCreate != null;
			case "NotifyThienMaUpgradeSlot":
				return NotifyThienMaUpgradeSlot != null;
			case "NotifyThienMaOpenSlot":
				return NotifyThienMaOpenSlot != null;
			case "NotifyThienMaEquip":
				return NotifyThienMaEquip != null;
			case "NotifyThienMaQuaySlotConfirm":
				return NotifyThienMaQuaySlotConfirm != null;
			case "NotifySetTonHieu":
				return NotifySetTonHieu != null;
			case "NotifySetTrangBiHoangKim":
				return NotifySetTrangBiHoangKim != null;
			case "NotifyGetTopDaiHoiVoLam":
				return NotifyGetTopDaiHoiVoLam != null;
			case "NotifyDanhAnDanhCaoThu":
				return NotifyDanhAnDanhCaoThu != null;
			case "NotifyStartBoiDuongTrangBi":
				return NotifyStartBoiDuongTrangBi != null;
			case "NotifyEndBoiDuongTrangBi":
				return NotifyEndBoiDuongTrangBi != null;
			case "NotifyRutQueTienNhan":
				return NotifyRutQueTienNhan != null;
			case "NotifyGetBaoKhoInfo":
				return NotifyGetBaoKhoInfo != null;
			case "NotifyCuopBaoKho":
				return NotifyCuopBaoKho != null;
			case "NotifyThuHoachBaoKho":
				return NotifyThuHoachBaoKho != null;
			case "NotifyGetListBaoKho":
				return NotifyGetListBaoKho != null;
			case "NotifyQuayCamCungBiBao":
				return NotifyQuayCamCungBiBao != null;
			case "NotifyNhanThuongCamCung":
				return NotifyNhanThuongCamCung != null;
			case "NotifyGetHoaVangInfo":
				return NotifyGetHoaVangInfo != null;
			case "NotifyHoaVang":
				return NotifyHoaVang != null;
			case "NotifyTestProudNet":
				return NotifyTestProudNet != null;
			case "NotifyTrangBiHuyenKhi":
				return NotifyTrangBiHuyenKhi != null;
			case "NotifyThangCapHuyenKhi":
				return NotifyThangCapHuyenKhi != null;
			case "NotifyChienHonChangeBuff":
				return NotifyChienHonChangeBuff != null;
			case "NotifyStartBoiDuongChienHon":
				return NotifyStartBoiDuongChienHon != null;
			case "NotifyEndBoiDuongChienHon":
				return NotifyEndBoiDuongChienHon != null;
			case "NotifyTrieuHoiChienHon":
				return NotifyTrieuHoiChienHon != null;
			case "NotifyUseTanHonTangLevelChienHon":
				return NotifyUseTanHonTangLevelChienHon != null;
			case "NotifyDotPhaChienHon":
				return NotifyDotPhaChienHon != null;
			case "NotifyCheTaoHuyenKhi":
				return NotifyCheTaoHuyenKhi != null;
			case "NotifyQuayDiemHoaVang":
				return NotifyQuayDiemHoaVang != null;
			case "NotifyUseTuiThan":
				return NotifyUseTuiThan != null;
			case "NotifyNhanThuongDailyKNB":
				return NotifyNhanThuongDailyKNB != null;
			default:
				return false;
			}
		}
	}
}
