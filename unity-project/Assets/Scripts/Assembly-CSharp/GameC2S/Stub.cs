using LitJson;
using Nettention.Proud;

namespace GameC2S
{
	public class Stub : IJsonStub
	{
		public delegate bool RequestNextLogonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestSetInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestSetHeroDataDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetBattleResultDelegate(HostID remote, RmiContext rmiContext, string name);

		public delegate bool RequestGetListLKDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestSetHeroCfgDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestChangeNextPosDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetListOtherPlayerDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetLuanKiemInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestCapNhatDiemLuanKiemDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestNhanThuongLuanKiemDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestDoiThuongLuanKiemDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestDauLuanKiemDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestDanhGiangHoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestSetVCSettingDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestSetTrangBiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestSetDoiHinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestSendChatMsgDelegate(HostID remote, RmiContext rmiContext, string msg);

		public delegate bool RequestSetVoCongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestSetTranHinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestStartBoiDuongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestEndBoiDuongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestTuLuyenDeTuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestTrieuHoiDeTuBangHonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestTruyenCongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetDongNhanInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestDanhDongNhanDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestCuongHoaTrangBiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestBanTrangBiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGhepManhTrangBiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGhepManhVoCongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestTinhLuyenTrangBiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestSetDoiHinhHoTroDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestOpenDoiHinhHoTroDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestThamNgoVoCongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestTinhLuyenVoCongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestLayDeTuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestBuyVatPhamDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestCaoNhanDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestThuongNhanDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestBanDoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestBangHuuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestTyThiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestBuyLeBaoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestHoiTheLucDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestSelectStartDeTuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestKyNgoThamBaiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestHuyetChienInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestStartHuyetChienDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestHoiSinhHuyetChienDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestHuyetChienTangThuocTinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestDanhHuyetChienDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestNhanThuongHuyetChienDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetTopHuyetChienDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestOpenHopDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestSetDoiHinhAndTranHinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestTestDongNhanDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestNhanThuongGiangHoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestDanhNhanhGiangHoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestAnGaGiangHoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestResetLuotGiangHoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestDanhDanhSonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestMoThuongDanhSonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestMoHetDanhSonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestVuotAiDanhSonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestChonDongDoiDanhSonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetDongDoiDanhSonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestHuaNguyenDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestLenCapNhanThuongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestThanTaiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestXocDiaInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestChoiXocDiaDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestCuuVienTieuPhongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestNhanRuongThachSanhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestPaymentConfirmDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestDangNhapNhanThuongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestUongRuouTieuPhongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetDoiRuouInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestDoiRuouDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestBatCocDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestChuocThanDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestSearchBanBeDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestThachDauDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestNhanThachDauDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestAddBanBeDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestAcceptBanBeDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestDeleteBanBeDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestBanBeCuuThuInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestChatInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestSendChatAllDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestDatTenMonPhaiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestULinhInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestDoiItemULinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestKnbRefreshULinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestDoiThuongULinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetTopULinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestKichHoatGiftCodeDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestUseCustomItemDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestBuyVatPhamAndUseDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestHighlightDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestUseMailPhanThuongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestReadAllMailDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestRefreshMailDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestUseRuongThanDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestBatThoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetFriendsDoiHinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestXemThongTinMonPhaiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestSendMailDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestDangNhapQuayXoSoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestTangTheLucDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestDoiDoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetDuaTopLevelInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetDuaTopLuanKiemInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestThamGiaCT2Delegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestCT2PlayerPosDelegate(HostID remote, RmiContext rmiContext, float pos_x, float pos_z, float vel_x, float vel_z);

		public delegate bool RequestListCT2Delegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestLapLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestTestCT2Delegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGiaNhapLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestChapNhanGiaNhapLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestThoatLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestFinishNhiemVuLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestResetNhiemVuLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestRutGiaNhapLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestDangHuongLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestDoiThuongLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestDoiMinhChuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestDoiPhoMinhChuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestNangCapCongTrinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestCT2EndBattleDelegate(HostID remote, RmiContext rmiContext);

		public delegate bool RequestKhamNgocDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGoNgocDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetTopLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestSearchLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetThongTinLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestNhanThuongDiHoaCungAllDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetDiHoaCungInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetTopDiHoaCungDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestDuoiKhoiLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestCT2BangXepHangTuanNayDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestCT2BangXepHangTuanTruocDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestThamGiaLuaTraiLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestLienMinhThoiLuaDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestRoiKhoiLuaTraiLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestSetGioLuaTraiLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestSuaThongBaoLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestSendChatLienMinhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestChatLienMinhInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestPhanRaTrangBiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetExpLuaTraiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestCT2GetBXHDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestUpdateLienMinhDataDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestCT2QuitDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestCardPaymentDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestLapNguyenKhiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestThaoNguyenKhiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestNangCapNguyenKhiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestMuaNguyenKhiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestCT2PlayerMoveDelegate(HostID remote, RmiContext rmiContext, float pos_x, float pos_z, float vel_x, float vel_z, long time);

		public delegate bool RequestCT2PlayerPos_Delegate(HostID remote, RmiContext rmiContext, float pos_x, float pos_z, float vel_x, float vel_z, long time);

		public delegate bool RequestBangChienGetInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestBangChienVaoThanhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestBangChienRoiThanhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestBangChienDenCongThanhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestBangChienCongThanhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestChonHatGiongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestLayHatGiongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestTrongCayDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestThuHoachDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestAnTromDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetAnTromListDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestBangChienUserMoveDelegate(HostID remote, RmiContext rmiContext, int thanhIdx, float x, float y, float z);

		public delegate bool RequestBangChienOtherUserDelegate(HostID remote, RmiContext rmiContext, int thanhIdx);

		public delegate bool RequestBangChienGetPhanThuongThuThanhDelegate(HostID remote, RmiContext rmiContext, int thanhIdx);

		public delegate bool RequestSendChatLienSrvDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetChatLienSrvDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetLinhDuocInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestBanhChungPlayerMoveDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestBanhChungGetInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestBanhChungNauBanhDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestBanhChungGetOthersDelegate(HostID remote, RmiContext rmiContext);

		public delegate bool RequestBanhChungNhatNguyenLieuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestBanhChungBXHDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestBanhChungGetPhanThuongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestDangNhapNhanThuongTetDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestThaoNguaDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestDungNguaDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestActiveNguaDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetGamerLinhDuocDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestCuongHoaBatQuaiTranDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestSetSoDoBatQuaiTranDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetLeagueDataDelegate(HostID remote, RmiContext rmiContext);

		public delegate bool RequestSetDoiHinhThienCangTranDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestOpenDoiHinhThienCangTranDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetSieuCupDataDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetSieuCupBattleDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestSieuCupDatCuocDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestSubmitDoiHinhLeagueDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestViewLeagueReplayDelegate(HostID remote, RmiContext rmiContext, int id);

		public delegate bool RequestThamBaiSieuCupDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestVongQuayDelegate(HostID remote, RmiContext rmiContext, int id);

		public delegate bool RequestLienDauDataDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestSieuCupChampionDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestQMDInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestQMDSelectDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestQMDGetChiTietNPCDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestQMDGetBXHDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestQMDXongPhaDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestBeQuanDeTuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestQMDTranHinhChienThuatDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestNhanThuongTichLuyNapDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestNhanThuongTichLuyTieuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetCacLoaiTopDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestDoiTenBangDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetThongTinLienServerDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetTopPhaoHoaDelegate(HostID remote, RmiContext rmiContext);

		public delegate bool RequestGetPhanThuongPhaoHoaDelegate(HostID remote, RmiContext rmiContext);

		public delegate bool RequestBanPhaoHoaEventDelegate(HostID remote, RmiContext rmiContext);

		public delegate bool RequestGetTopVongQuayDelegate(HostID remote, RmiContext rmiContext);

		public delegate bool RequestCreateCostumeDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestTinhLuyenCostumeDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestKhamNgocCostumeDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGoNgocCostumeDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestTakeOnCostumeDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestTakeOffCostumeDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestBanPhaoHoaDelegate(HostID remote, RmiContext rmiContext);

		public delegate bool RequestLinhThuongPhaoHoaEventDelegate(HostID remote, RmiContext rmiContext, int count);

		public delegate bool RequestChuyenSinhDeTuDelegate(HostID remote, RmiContext rmiContext, int heroId);

		public delegate bool RequestGetListOtherUserDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestBatThanThuDelegate(HostID remote, RmiContext rmiContext);

		public delegate bool RequestNhanThuongDapNieuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestTruongThanhThanThuDelegate(HostID remote, RmiContext rmiContext, int thanthuId);

		public delegate bool RequestNangPhamThanThuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestDoiThanThuDelegate(HostID remote, RmiContext rmiContext, int thanthuId);

		public delegate bool RequestThonPheThanThuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestTruyenCongThanThuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestSetBoPhapNhanVatBatQuaiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestSetNoiCongNhanVatBatQuaiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestSetTrangBiNhanVatBatQuaiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetThanThuDaoDelegate(HostID remote, RmiContext rmiContext);

		public delegate bool RequestAutoResolveThanThuDaoDelegate(HostID remote, RmiContext rmiContext);

		public delegate bool RequestSetThanThuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetGuiTietKiemDelegate(HostID remote, RmiContext rmiContext, int id);

		public delegate bool RequestThamGiaGuiTietKiemDelegate(HostID remote, RmiContext rmiContext);

		public delegate bool RequestGetThuong1MilUserDelegate(HostID remote, RmiContext rmiContext);

		public delegate bool RequestUseRuongThanBiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestThuHoachSonMonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestXayDungSonMonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestTanCongSonMonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestDoiHinhSonMonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestDoThamSonMonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestMuaDoThanBiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetTopSonMonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestDungLuyenTrangBiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestTayLuyenTrangBiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestKhaiQuangTrangBiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestConfirmTayLuyenTrangBiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetSonMonInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetLanhDiaInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestUnLockVoCongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestQuayBacMayManDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestMoveLanhDiaDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestQuayTuBaoBonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetTopMoRuongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestQuayThienMaHaPhongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetTayVucInfoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestMuaDoTayVucDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestSummonNienThuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestDanhNienThuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestThamGiaNienThuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestNhanThuongNapHangNgayDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetTopNienThuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestNopLenhBaiNienThuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetTopLanhDiaDelegate(HostID remote, RmiContext rmiContext);

		public delegate bool RequestSelectStartNguaDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetDiemMoRuongDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestUpdateDailyActivitiesDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestThuongDailyActivitiesDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestThienMaQuaySlotDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestThienMaLenhCreateDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestThienMaUpgradeSlotDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestThienMaOpenSlotDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestThienMaEquipDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestThienMaQuaySlotConfirmDelegate(HostID remote, RmiContext rmiContext, bool confirm);

		public delegate bool RequestSetTonHieuDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestSetTrangBiHoangKimDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetTopDaiHoiVoLamDelegate(HostID remote, RmiContext rmiContext);

		public delegate bool RequestDanhAnDanhCaoThuDelegate(HostID remote, RmiContext rmiContext);

		public delegate bool RequestStartBoiDuongTrangBiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestEndBoiDuongTrangBiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestRutQueTienNhanDelegate(HostID remote, RmiContext rmiContext);

		public delegate bool RequestGetBaoKhoInfoDelegate(HostID remote, RmiContext rmiContext);

		public delegate bool RequestCuopBaoKhoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestThuHoachBaoKhoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetListBaoKhoDelegate(HostID remote, RmiContext rmiContext);

		public delegate bool RequestQuayCamCungBiBaoDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestNhanThuongCamCungDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestGetHoaVangInfoDelegate(HostID remote, RmiContext rmiContext);

		public delegate bool RequestHoaVangDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestTestProudNetDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestTrangBiHuyenKhiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestThangCapHuyenKhiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestChienHonChangeBuffDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestStartBoiDuongChienHonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestEndBoiDuongChienHonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestTrieuHoiChienHonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestUseTanHonTangLevelChienHonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestDotPhaChienHonDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestCheTaoHuyenKhiDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestQuayDiemHoaVangDelegate(HostID remote, RmiContext rmiContext);

		public delegate bool RequestUseTuiThanDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestNhanThuongDailyKNBDelegate(HostID remote, RmiContext rmiContext);

		public RequestNextLogonDelegate RequestNextLogon;

		public RequestSetInfoDelegate RequestSetInfo;

		public RequestSetHeroDataDelegate RequestSetHeroData;

		public RequestGetBattleResultDelegate RequestGetBattleResult;

		public RequestGetListLKDelegate RequestGetListLK;

		public RequestSetHeroCfgDelegate RequestSetHeroCfg;

		public RequestGetInfoDelegate RequestGetInfo;

		public RequestChangeNextPosDelegate RequestChangeNextPos;

		public RequestGetListOtherPlayerDelegate RequestGetListOtherPlayer;

		public RequestGetLuanKiemInfoDelegate RequestGetLuanKiemInfo;

		public RequestCapNhatDiemLuanKiemDelegate RequestCapNhatDiemLuanKiem;

		public RequestNhanThuongLuanKiemDelegate RequestNhanThuongLuanKiem;

		public RequestDoiThuongLuanKiemDelegate RequestDoiThuongLuanKiem;

		public RequestDauLuanKiemDelegate RequestDauLuanKiem;

		public RequestDanhGiangHoDelegate RequestDanhGiangHo;

		public RequestSetVCSettingDelegate RequestSetVCSetting;

		public RequestSetTrangBiDelegate RequestSetTrangBi;

		public RequestSetDoiHinhDelegate RequestSetDoiHinh;

		public RequestSendChatMsgDelegate RequestSendChatMsg;

		public RequestSetVoCongDelegate RequestSetVoCong;

		public RequestSetTranHinhDelegate RequestSetTranHinh;

		public RequestStartBoiDuongDelegate RequestStartBoiDuong;

		public RequestEndBoiDuongDelegate RequestEndBoiDuong;

		public RequestTuLuyenDeTuDelegate RequestTuLuyenDeTu;

		public RequestTrieuHoiDeTuBangHonDelegate RequestTrieuHoiDeTuBangHon;

		public RequestTruyenCongDelegate RequestTruyenCong;

		public RequestGetDongNhanInfoDelegate RequestGetDongNhanInfo;

		public RequestDanhDongNhanDelegate RequestDanhDongNhan;

		public RequestCuongHoaTrangBiDelegate RequestCuongHoaTrangBi;

		public RequestBanTrangBiDelegate RequestBanTrangBi;

		public RequestGhepManhTrangBiDelegate RequestGhepManhTrangBi;

		public RequestGhepManhVoCongDelegate RequestGhepManhVoCong;

		public RequestTinhLuyenTrangBiDelegate RequestTinhLuyenTrangBi;

		public RequestSetDoiHinhHoTroDelegate RequestSetDoiHinhHoTro;

		public RequestOpenDoiHinhHoTroDelegate RequestOpenDoiHinhHoTro;

		public RequestThamNgoVoCongDelegate RequestThamNgoVoCong;

		public RequestTinhLuyenVoCongDelegate RequestTinhLuyenVoCong;

		public RequestLayDeTuDelegate RequestLayDeTu;

		public RequestBuyVatPhamDelegate RequestBuyVatPham;

		public RequestCaoNhanDelegate RequestCaoNhan;

		public RequestThuongNhanDelegate RequestThuongNhan;

		public RequestBanDoDelegate RequestBanDo;

		public RequestBangHuuDelegate RequestBangHuu;

		public RequestTyThiDelegate RequestTyThi;

		public RequestBuyLeBaoDelegate RequestBuyLeBao;

		public RequestHoiTheLucDelegate RequestHoiTheLuc;

		public RequestSelectStartDeTuDelegate RequestSelectStartDeTu;

		public RequestKyNgoThamBaiDelegate RequestKyNgoThamBai;

		public RequestHuyetChienInfoDelegate RequestHuyetChienInfo;

		public RequestStartHuyetChienDelegate RequestStartHuyetChien;

		public RequestHoiSinhHuyetChienDelegate RequestHoiSinhHuyetChien;

		public RequestHuyetChienTangThuocTinhDelegate RequestHuyetChienTangThuocTinh;

		public RequestDanhHuyetChienDelegate RequestDanhHuyetChien;

		public RequestNhanThuongHuyetChienDelegate RequestNhanThuongHuyetChien;

		public RequestGetTopHuyetChienDelegate RequestGetTopHuyetChien;

		public RequestOpenHopDelegate RequestOpenHop;

		public RequestSetDoiHinhAndTranHinhDelegate RequestSetDoiHinhAndTranHinh;

		public RequestTestDongNhanDelegate RequestTestDongNhan;

		public RequestNhanThuongGiangHoDelegate RequestNhanThuongGiangHo;

		public RequestDanhNhanhGiangHoDelegate RequestDanhNhanhGiangHo;

		public RequestAnGaGiangHoDelegate RequestAnGaGiangHo;

		public RequestResetLuotGiangHoDelegate RequestResetLuotGiangHo;

		public RequestDanhDanhSonDelegate RequestDanhDanhSon;

		public RequestMoThuongDanhSonDelegate RequestMoThuongDanhSon;

		public RequestMoHetDanhSonDelegate RequestMoHetDanhSon;

		public RequestVuotAiDanhSonDelegate RequestVuotAiDanhSon;

		public RequestChonDongDoiDanhSonDelegate RequestChonDongDoiDanhSon;

		public RequestGetDongDoiDanhSonDelegate RequestGetDongDoiDanhSon;

		public RequestHuaNguyenDelegate RequestHuaNguyen;

		public RequestLenCapNhanThuongDelegate RequestLenCapNhanThuong;

		public RequestThanTaiDelegate RequestThanTai;

		public RequestXocDiaInfoDelegate RequestXocDiaInfo;

		public RequestChoiXocDiaDelegate RequestChoiXocDia;

		public RequestCuuVienTieuPhongDelegate RequestCuuVienTieuPhong;

		public RequestNhanRuongThachSanhDelegate RequestNhanRuongThachSanh;

		public RequestPaymentConfirmDelegate RequestPaymentConfirm;

		public RequestDangNhapNhanThuongDelegate RequestDangNhapNhanThuong;

		public RequestUongRuouTieuPhongDelegate RequestUongRuouTieuPhong;

		public RequestGetDoiRuouInfoDelegate RequestGetDoiRuouInfo;

		public RequestDoiRuouDelegate RequestDoiRuou;

		public RequestBatCocDelegate RequestBatCoc;

		public RequestChuocThanDelegate RequestChuocThan;

		public RequestSearchBanBeDelegate RequestSearchBanBe;

		public RequestThachDauDelegate RequestThachDau;

		public RequestNhanThachDauDelegate RequestNhanThachDau;

		public RequestAddBanBeDelegate RequestAddBanBe;

		public RequestAcceptBanBeDelegate RequestAcceptBanBe;

		public RequestDeleteBanBeDelegate RequestDeleteBanBe;

		public RequestBanBeCuuThuInfoDelegate RequestBanBeCuuThuInfo;

		public RequestChatInfoDelegate RequestChatInfo;

		public RequestSendChatAllDelegate RequestSendChatAll;

		public RequestDatTenMonPhaiDelegate RequestDatTenMonPhai;

		public RequestULinhInfoDelegate RequestULinhInfo;

		public RequestDoiItemULinhDelegate RequestDoiItemULinh;

		public RequestKnbRefreshULinhDelegate RequestKnbRefreshULinh;

		public RequestDoiThuongULinhDelegate RequestDoiThuongULinh;

		public RequestGetTopULinhDelegate RequestGetTopULinh;

		public RequestKichHoatGiftCodeDelegate RequestKichHoatGiftCode;

		public RequestUseCustomItemDelegate RequestUseCustomItem;

		public RequestBuyVatPhamAndUseDelegate RequestBuyVatPhamAndUse;

		public RequestHighlightDelegate RequestHighlight;

		public RequestUseMailPhanThuongDelegate RequestUseMailPhanThuong;

		public RequestReadAllMailDelegate RequestReadAllMail;

		public RequestRefreshMailDelegate RequestRefreshMail;

		public RequestUseRuongThanDelegate RequestUseRuongThan;

		public RequestBatThoDelegate RequestBatTho;

		public RequestGetFriendsDoiHinhDelegate RequestGetFriendsDoiHinh;

		public RequestXemThongTinMonPhaiDelegate RequestXemThongTinMonPhai;

		public RequestSendMailDelegate RequestSendMail;

		public RequestDangNhapQuayXoSoDelegate RequestDangNhapQuayXoSo;

		public RequestTangTheLucDelegate RequestTangTheLuc;

		public RequestDoiDoDelegate RequestDoiDo;

		public RequestGetDuaTopLevelInfoDelegate RequestGetDuaTopLevelInfo;

		public RequestGetDuaTopLuanKiemInfoDelegate RequestGetDuaTopLuanKiemInfo;

		public RequestThamGiaCT2Delegate RequestThamGiaCT2;

		public RequestCT2PlayerPosDelegate RequestCT2PlayerPos;

		public RequestListCT2Delegate RequestListCT2;

		public RequestLapLienMinhDelegate RequestLapLienMinh;

		public RequestTestCT2Delegate RequestTestCT2;

		public RequestGiaNhapLienMinhDelegate RequestGiaNhapLienMinh;

		public RequestChapNhanGiaNhapLienMinhDelegate RequestChapNhanGiaNhapLienMinh;

		public RequestThoatLienMinhDelegate RequestThoatLienMinh;

		public RequestFinishNhiemVuLienMinhDelegate RequestFinishNhiemVuLienMinh;

		public RequestResetNhiemVuLienMinhDelegate RequestResetNhiemVuLienMinh;

		public RequestRutGiaNhapLienMinhDelegate RequestRutGiaNhapLienMinh;

		public RequestDangHuongLienMinhDelegate RequestDangHuongLienMinh;

		public RequestDoiThuongLienMinhDelegate RequestDoiThuongLienMinh;

		public RequestDoiMinhChuDelegate RequestDoiMinhChu;

		public RequestDoiPhoMinhChuDelegate RequestDoiPhoMinhChu;

		public RequestNangCapCongTrinhDelegate RequestNangCapCongTrinh;

		public RequestCT2EndBattleDelegate RequestCT2EndBattle;

		public RequestKhamNgocDelegate RequestKhamNgoc;

		public RequestGoNgocDelegate RequestGoNgoc;

		public RequestGetTopLienMinhDelegate RequestGetTopLienMinh;

		public RequestSearchLienMinhDelegate RequestSearchLienMinh;

		public RequestGetThongTinLienMinhDelegate RequestGetThongTinLienMinh;

		public RequestNhanThuongDiHoaCungAllDelegate RequestNhanThuongDiHoaCungAll;

		public RequestGetDiHoaCungInfoDelegate RequestGetDiHoaCungInfo;

		public RequestGetTopDiHoaCungDelegate RequestGetTopDiHoaCung;

		public RequestDuoiKhoiLienMinhDelegate RequestDuoiKhoiLienMinh;

		public RequestCT2BangXepHangTuanNayDelegate RequestCT2BangXepHangTuanNay;

		public RequestCT2BangXepHangTuanTruocDelegate RequestCT2BangXepHangTuanTruoc;

		public RequestThamGiaLuaTraiLienMinhDelegate RequestThamGiaLuaTraiLienMinh;

		public RequestLienMinhThoiLuaDelegate RequestLienMinhThoiLua;

		public RequestRoiKhoiLuaTraiLienMinhDelegate RequestRoiKhoiLuaTraiLienMinh;

		public RequestSetGioLuaTraiLienMinhDelegate RequestSetGioLuaTraiLienMinh;

		public RequestSuaThongBaoLienMinhDelegate RequestSuaThongBaoLienMinh;

		public RequestSendChatLienMinhDelegate RequestSendChatLienMinh;

		public RequestChatLienMinhInfoDelegate RequestChatLienMinhInfo;

		public RequestPhanRaTrangBiDelegate RequestPhanRaTrangBi;

		public RequestGetExpLuaTraiDelegate RequestGetExpLuaTrai;

		public RequestCT2GetBXHDelegate RequestCT2GetBXH;

		public RequestUpdateLienMinhDataDelegate RequestUpdateLienMinhData;

		public RequestCT2QuitDelegate RequestCT2Quit;

		public RequestCardPaymentDelegate RequestCardPayment;

		public RequestLapNguyenKhiDelegate RequestLapNguyenKhi;

		public RequestThaoNguyenKhiDelegate RequestThaoNguyenKhi;

		public RequestNangCapNguyenKhiDelegate RequestNangCapNguyenKhi;

		public RequestMuaNguyenKhiDelegate RequestMuaNguyenKhi;

		public RequestCT2PlayerMoveDelegate RequestCT2PlayerMove;

		public RequestCT2PlayerPos_Delegate RequestCT2PlayerPos_;

		public RequestBangChienGetInfoDelegate RequestBangChienGetInfo;

		public RequestBangChienVaoThanhDelegate RequestBangChienVaoThanh;

		public RequestBangChienRoiThanhDelegate RequestBangChienRoiThanh;

		public RequestBangChienDenCongThanhDelegate RequestBangChienDenCongThanh;

		public RequestBangChienCongThanhDelegate RequestBangChienCongThanh;

		public RequestChonHatGiongDelegate RequestChonHatGiong;

		public RequestLayHatGiongDelegate RequestLayHatGiong;

		public RequestTrongCayDelegate RequestTrongCay;

		public RequestThuHoachDelegate RequestThuHoach;

		public RequestAnTromDelegate RequestAnTrom;

		public RequestGetAnTromListDelegate RequestGetAnTromList;

		public RequestBangChienUserMoveDelegate RequestBangChienUserMove;

		public RequestBangChienOtherUserDelegate RequestBangChienOtherUser;

		public RequestBangChienGetPhanThuongThuThanhDelegate RequestBangChienGetPhanThuongThuThanh;

		public RequestSendChatLienSrvDelegate RequestSendChatLienSrv;

		public RequestGetChatLienSrvDelegate RequestGetChatLienSrv;

		public RequestGetLinhDuocInfoDelegate RequestGetLinhDuocInfo;

		public RequestBanhChungPlayerMoveDelegate RequestBanhChungPlayerMove;

		public RequestBanhChungGetInfoDelegate RequestBanhChungGetInfo;

		public RequestBanhChungNauBanhDelegate RequestBanhChungNauBanh;

		public RequestBanhChungGetOthersDelegate RequestBanhChungGetOthers;

		public RequestBanhChungNhatNguyenLieuDelegate RequestBanhChungNhatNguyenLieu;

		public RequestBanhChungBXHDelegate RequestBanhChungBXH;

		public RequestBanhChungGetPhanThuongDelegate RequestBanhChungGetPhanThuong;

		public RequestDangNhapNhanThuongTetDelegate RequestDangNhapNhanThuongTet;

		public RequestThaoNguaDelegate RequestThaoNgua;

		public RequestDungNguaDelegate RequestDungNgua;

		public RequestActiveNguaDelegate RequestActiveNgua;

		public RequestGetGamerLinhDuocDelegate RequestGetGamerLinhDuoc;

		public RequestCuongHoaBatQuaiTranDelegate RequestCuongHoaBatQuaiTran;

		public RequestSetSoDoBatQuaiTranDelegate RequestSetSoDoBatQuaiTran;

		public RequestGetLeagueDataDelegate RequestGetLeagueData;

		public RequestSetDoiHinhThienCangTranDelegate RequestSetDoiHinhThienCangTran;

		public RequestOpenDoiHinhThienCangTranDelegate RequestOpenDoiHinhThienCangTran;

		public RequestGetSieuCupDataDelegate RequestGetSieuCupData;

		public RequestGetSieuCupBattleDelegate RequestGetSieuCupBattle;

		public RequestSieuCupDatCuocDelegate RequestSieuCupDatCuoc;

		public RequestSubmitDoiHinhLeagueDelegate RequestSubmitDoiHinhLeague;

		public RequestViewLeagueReplayDelegate RequestViewLeagueReplay;

		public RequestThamBaiSieuCupDelegate RequestThamBaiSieuCup;

		public RequestVongQuayDelegate RequestVongQuay;

		public RequestLienDauDataDelegate RequestLienDauData;

		public RequestSieuCupChampionDelegate RequestSieuCupChampion;

		public RequestQMDInfoDelegate RequestQMDInfo;

		public RequestQMDSelectDelegate RequestQMDSelect;

		public RequestQMDGetChiTietNPCDelegate RequestQMDGetChiTietNPC;

		public RequestQMDGetBXHDelegate RequestQMDGetBXH;

		public RequestQMDXongPhaDelegate RequestQMDXongPha;

		public RequestBeQuanDeTuDelegate RequestBeQuanDeTu;

		public RequestQMDTranHinhChienThuatDelegate RequestQMDTranHinhChienThuat;

		public RequestNhanThuongTichLuyNapDelegate RequestNhanThuongTichLuyNap;

		public RequestNhanThuongTichLuyTieuDelegate RequestNhanThuongTichLuyTieu;

		public RequestGetCacLoaiTopDelegate RequestGetCacLoaiTop;

		public RequestDoiTenBangDelegate RequestDoiTenBang;

		public RequestGetThongTinLienServerDelegate RequestGetThongTinLienServer;

		public RequestGetTopPhaoHoaDelegate RequestGetTopPhaoHoa;

		public RequestGetPhanThuongPhaoHoaDelegate RequestGetPhanThuongPhaoHoa;

		public RequestBanPhaoHoaEventDelegate RequestBanPhaoHoaEvent;

		public RequestGetTopVongQuayDelegate RequestGetTopVongQuay;

		public RequestCreateCostumeDelegate RequestCreateCostume;

		public RequestTinhLuyenCostumeDelegate RequestTinhLuyenCostume;

		public RequestKhamNgocCostumeDelegate RequestKhamNgocCostume;

		public RequestGoNgocCostumeDelegate RequestGoNgocCostume;

		public RequestTakeOnCostumeDelegate RequestTakeOnCostume;

		public RequestTakeOffCostumeDelegate RequestTakeOffCostume;

		public RequestBanPhaoHoaDelegate RequestBanPhaoHoa;

		public RequestLinhThuongPhaoHoaEventDelegate RequestLinhThuongPhaoHoaEvent;

		public RequestChuyenSinhDeTuDelegate RequestChuyenSinhDeTu;

		public RequestGetListOtherUserDelegate RequestGetListOtherUser;

		public RequestBatThanThuDelegate RequestBatThanThu;

		public RequestNhanThuongDapNieuDelegate RequestNhanThuongDapNieu;

		public RequestTruongThanhThanThuDelegate RequestTruongThanhThanThu;

		public RequestNangPhamThanThuDelegate RequestNangPhamThanThu;

		public RequestDoiThanThuDelegate RequestDoiThanThu;

		public RequestThonPheThanThuDelegate RequestThonPheThanThu;

		public RequestTruyenCongThanThuDelegate RequestTruyenCongThanThu;

		public RequestSetBoPhapNhanVatBatQuaiDelegate RequestSetBoPhapNhanVatBatQuai;

		public RequestSetNoiCongNhanVatBatQuaiDelegate RequestSetNoiCongNhanVatBatQuai;

		public RequestSetTrangBiNhanVatBatQuaiDelegate RequestSetTrangBiNhanVatBatQuai;

		public RequestGetThanThuDaoDelegate RequestGetThanThuDao;

		public RequestAutoResolveThanThuDaoDelegate RequestAutoResolveThanThuDao;

		public RequestSetThanThuDelegate RequestSetThanThu;

		public RequestGetGuiTietKiemDelegate RequestGetGuiTietKiem;

		public RequestThamGiaGuiTietKiemDelegate RequestThamGiaGuiTietKiem;

		public RequestGetThuong1MilUserDelegate RequestGetThuong1MilUser;

		public RequestUseRuongThanBiDelegate RequestUseRuongThanBi;

		public RequestThuHoachSonMonDelegate RequestThuHoachSonMon;

		public RequestXayDungSonMonDelegate RequestXayDungSonMon;

		public RequestTanCongSonMonDelegate RequestTanCongSonMon;

		public RequestDoiHinhSonMonDelegate RequestDoiHinhSonMon;

		public RequestDoThamSonMonDelegate RequestDoThamSonMon;

		public RequestMuaDoThanBiDelegate RequestMuaDoThanBi;

		public RequestGetTopSonMonDelegate RequestGetTopSonMon;

		public RequestDungLuyenTrangBiDelegate RequestDungLuyenTrangBi;

		public RequestTayLuyenTrangBiDelegate RequestTayLuyenTrangBi;

		public RequestKhaiQuangTrangBiDelegate RequestKhaiQuangTrangBi;

		public RequestConfirmTayLuyenTrangBiDelegate RequestConfirmTayLuyenTrangBi;

		public RequestGetSonMonInfoDelegate RequestGetSonMonInfo;

		public RequestGetLanhDiaInfoDelegate RequestGetLanhDiaInfo;

		public RequestUnLockVoCongDelegate RequestUnLockVoCong;

		public RequestQuayBacMayManDelegate RequestQuayBacMayMan;

		public RequestMoveLanhDiaDelegate RequestMoveLanhDia;

		public RequestQuayTuBaoBonDelegate RequestQuayTuBaoBon;

		public RequestGetTopMoRuongDelegate RequestGetTopMoRuong;

		public RequestQuayThienMaHaPhongDelegate RequestQuayThienMaHaPhong;

		public RequestGetTayVucInfoDelegate RequestGetTayVucInfo;

		public RequestMuaDoTayVucDelegate RequestMuaDoTayVuc;

		public RequestSummonNienThuDelegate RequestSummonNienThu;

		public RequestDanhNienThuDelegate RequestDanhNienThu;

		public RequestThamGiaNienThuDelegate RequestThamGiaNienThu;

		public RequestNhanThuongNapHangNgayDelegate RequestNhanThuongNapHangNgay;

		public RequestGetTopNienThuDelegate RequestGetTopNienThu;

		public RequestNopLenhBaiNienThuDelegate RequestNopLenhBaiNienThu;

		public RequestGetTopLanhDiaDelegate RequestGetTopLanhDia;

		public RequestSelectStartNguaDelegate RequestSelectStartNgua;

		public RequestGetDiemMoRuongDelegate RequestGetDiemMoRuong;

		public RequestUpdateDailyActivitiesDelegate RequestUpdateDailyActivities;

		public RequestThuongDailyActivitiesDelegate RequestThuongDailyActivities;

		public RequestThienMaQuaySlotDelegate RequestThienMaQuaySlot;

		public RequestThienMaLenhCreateDelegate RequestThienMaLenhCreate;

		public RequestThienMaUpgradeSlotDelegate RequestThienMaUpgradeSlot;

		public RequestThienMaOpenSlotDelegate RequestThienMaOpenSlot;

		public RequestThienMaEquipDelegate RequestThienMaEquip;

		public RequestThienMaQuaySlotConfirmDelegate RequestThienMaQuaySlotConfirm;

		public RequestSetTonHieuDelegate RequestSetTonHieu;

		public RequestSetTrangBiHoangKimDelegate RequestSetTrangBiHoangKim;

		public RequestGetTopDaiHoiVoLamDelegate RequestGetTopDaiHoiVoLam;

		public RequestDanhAnDanhCaoThuDelegate RequestDanhAnDanhCaoThu;

		public RequestStartBoiDuongTrangBiDelegate RequestStartBoiDuongTrangBi;

		public RequestEndBoiDuongTrangBiDelegate RequestEndBoiDuongTrangBi;

		public RequestRutQueTienNhanDelegate RequestRutQueTienNhan;

		public RequestGetBaoKhoInfoDelegate RequestGetBaoKhoInfo;

		public RequestCuopBaoKhoDelegate RequestCuopBaoKho;

		public RequestThuHoachBaoKhoDelegate RequestThuHoachBaoKho;

		public RequestGetListBaoKhoDelegate RequestGetListBaoKho;

		public RequestQuayCamCungBiBaoDelegate RequestQuayCamCungBiBao;

		public RequestNhanThuongCamCungDelegate RequestNhanThuongCamCung;

		public RequestGetHoaVangInfoDelegate RequestGetHoaVangInfo;

		public RequestHoaVangDelegate RequestHoaVang;

		public RequestTestProudNetDelegate RequestTestProudNet;

		public RequestTrangBiHuyenKhiDelegate RequestTrangBiHuyenKhi;

		public RequestThangCapHuyenKhiDelegate RequestThangCapHuyenKhi;

		public RequestChienHonChangeBuffDelegate RequestChienHonChangeBuff;

		public RequestStartBoiDuongChienHonDelegate RequestStartBoiDuongChienHon;

		public RequestEndBoiDuongChienHonDelegate RequestEndBoiDuongChienHon;

		public RequestTrieuHoiChienHonDelegate RequestTrieuHoiChienHon;

		public RequestUseTanHonTangLevelChienHonDelegate RequestUseTanHonTangLevelChienHon;

		public RequestDotPhaChienHonDelegate RequestDotPhaChienHon;

		public RequestCheTaoHuyenKhiDelegate RequestCheTaoHuyenKhi;

		public RequestQuayDiemHoaVangDelegate RequestQuayDiemHoaVang;

		public RequestUseTuiThanDelegate RequestUseTuiThan;

		public RequestNhanThuongDailyKNBDelegate RequestNhanThuongDailyKNB;

		public bool Dispatch(string rmiName, string data)
		{
			switch (rmiName)
			{
			case "RequestNextLogon":
				if (RequestNextLogon != null)
				{
					return RequestNextLogon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestSetInfo":
				if (RequestSetInfo != null)
				{
					return RequestSetInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestSetHeroData":
				if (RequestSetHeroData != null)
				{
					return RequestSetHeroData(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetBattleResult":
				if (RequestGetBattleResult != null)
				{
					return RequestGetBattleResult(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetListLK":
				if (RequestGetListLK != null)
				{
					return RequestGetListLK(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestSetHeroCfg":
				if (RequestSetHeroCfg != null)
				{
					return RequestSetHeroCfg(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetInfo":
				if (RequestGetInfo != null)
				{
					return RequestGetInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestChangeNextPos":
				if (RequestChangeNextPos != null)
				{
					return RequestChangeNextPos(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetListOtherPlayer":
				if (RequestGetListOtherPlayer != null)
				{
					return RequestGetListOtherPlayer(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetLuanKiemInfo":
				if (RequestGetLuanKiemInfo != null)
				{
					return RequestGetLuanKiemInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestCapNhatDiemLuanKiem":
				if (RequestCapNhatDiemLuanKiem != null)
				{
					return RequestCapNhatDiemLuanKiem(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestNhanThuongLuanKiem":
				if (RequestNhanThuongLuanKiem != null)
				{
					return RequestNhanThuongLuanKiem(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestDoiThuongLuanKiem":
				if (RequestDoiThuongLuanKiem != null)
				{
					return RequestDoiThuongLuanKiem(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestDauLuanKiem":
				if (RequestDauLuanKiem != null)
				{
					return RequestDauLuanKiem(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestDanhGiangHo":
				if (RequestDanhGiangHo != null)
				{
					return RequestDanhGiangHo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestSetVCSetting":
				if (RequestSetVCSetting != null)
				{
					return RequestSetVCSetting(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestSetTrangBi":
				if (RequestSetTrangBi != null)
				{
					return RequestSetTrangBi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestSetDoiHinh":
				if (RequestSetDoiHinh != null)
				{
					return RequestSetDoiHinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestSendChatMsg":
				if (RequestSendChatMsg != null)
				{
					return RequestSendChatMsg(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestSetVoCong":
				if (RequestSetVoCong != null)
				{
					return RequestSetVoCong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestSetTranHinh":
				if (RequestSetTranHinh != null)
				{
					return RequestSetTranHinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestStartBoiDuong":
				if (RequestStartBoiDuong != null)
				{
					return RequestStartBoiDuong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestEndBoiDuong":
				if (RequestEndBoiDuong != null)
				{
					return RequestEndBoiDuong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestTuLuyenDeTu":
				if (RequestTuLuyenDeTu != null)
				{
					return RequestTuLuyenDeTu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestTrieuHoiDeTuBangHon":
				if (RequestTrieuHoiDeTuBangHon != null)
				{
					return RequestTrieuHoiDeTuBangHon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestTruyenCong":
				if (RequestTruyenCong != null)
				{
					return RequestTruyenCong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetDongNhanInfo":
				if (RequestGetDongNhanInfo != null)
				{
					return RequestGetDongNhanInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestDanhDongNhan":
				if (RequestDanhDongNhan != null)
				{
					return RequestDanhDongNhan(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestCuongHoaTrangBi":
				if (RequestCuongHoaTrangBi != null)
				{
					return RequestCuongHoaTrangBi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestBanTrangBi":
				if (RequestBanTrangBi != null)
				{
					return RequestBanTrangBi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGhepManhTrangBi":
				if (RequestGhepManhTrangBi != null)
				{
					return RequestGhepManhTrangBi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGhepManhVoCong":
				if (RequestGhepManhVoCong != null)
				{
					return RequestGhepManhVoCong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestTinhLuyenTrangBi":
				if (RequestTinhLuyenTrangBi != null)
				{
					return RequestTinhLuyenTrangBi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestSetDoiHinhHoTro":
				if (RequestSetDoiHinhHoTro != null)
				{
					return RequestSetDoiHinhHoTro(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestOpenDoiHinhHoTro":
				if (RequestOpenDoiHinhHoTro != null)
				{
					return RequestOpenDoiHinhHoTro(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestThamNgoVoCong":
				if (RequestThamNgoVoCong != null)
				{
					return RequestThamNgoVoCong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestTinhLuyenVoCong":
				if (RequestTinhLuyenVoCong != null)
				{
					return RequestTinhLuyenVoCong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestLayDeTu":
				if (RequestLayDeTu != null)
				{
					return RequestLayDeTu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestBuyVatPham":
				if (RequestBuyVatPham != null)
				{
					return RequestBuyVatPham(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestCaoNhan":
				if (RequestCaoNhan != null)
				{
					return RequestCaoNhan(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestThuongNhan":
				if (RequestThuongNhan != null)
				{
					return RequestThuongNhan(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestBanDo":
				if (RequestBanDo != null)
				{
					return RequestBanDo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestBangHuu":
				if (RequestBangHuu != null)
				{
					return RequestBangHuu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestTyThi":
				if (RequestTyThi != null)
				{
					return RequestTyThi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestBuyLeBao":
				if (RequestBuyLeBao != null)
				{
					return RequestBuyLeBao(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestHoiTheLuc":
				if (RequestHoiTheLuc != null)
				{
					return RequestHoiTheLuc(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestSelectStartDeTu":
				if (RequestSelectStartDeTu != null)
				{
					return RequestSelectStartDeTu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestKyNgoThamBai":
				if (RequestKyNgoThamBai != null)
				{
					return RequestKyNgoThamBai(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestHuyetChienInfo":
				if (RequestHuyetChienInfo != null)
				{
					return RequestHuyetChienInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestStartHuyetChien":
				if (RequestStartHuyetChien != null)
				{
					return RequestStartHuyetChien(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestHoiSinhHuyetChien":
				if (RequestHoiSinhHuyetChien != null)
				{
					return RequestHoiSinhHuyetChien(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestHuyetChienTangThuocTinh":
				if (RequestHuyetChienTangThuocTinh != null)
				{
					return RequestHuyetChienTangThuocTinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestDanhHuyetChien":
				if (RequestDanhHuyetChien != null)
				{
					return RequestDanhHuyetChien(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestNhanThuongHuyetChien":
				if (RequestNhanThuongHuyetChien != null)
				{
					return RequestNhanThuongHuyetChien(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetTopHuyetChien":
				if (RequestGetTopHuyetChien != null)
				{
					return RequestGetTopHuyetChien(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestOpenHop":
				if (RequestOpenHop != null)
				{
					return RequestOpenHop(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestSetDoiHinhAndTranHinh":
				if (RequestSetDoiHinhAndTranHinh != null)
				{
					return RequestSetDoiHinhAndTranHinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestTestDongNhan":
				if (RequestTestDongNhan != null)
				{
					return RequestTestDongNhan(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestNhanThuongGiangHo":
				if (RequestNhanThuongGiangHo != null)
				{
					return RequestNhanThuongGiangHo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestDanhNhanhGiangHo":
				if (RequestDanhNhanhGiangHo != null)
				{
					return RequestDanhNhanhGiangHo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestAnGaGiangHo":
				if (RequestAnGaGiangHo != null)
				{
					return RequestAnGaGiangHo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestResetLuotGiangHo":
				if (RequestResetLuotGiangHo != null)
				{
					return RequestResetLuotGiangHo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestDanhDanhSon":
				if (RequestDanhDanhSon != null)
				{
					return RequestDanhDanhSon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestMoThuongDanhSon":
				if (RequestMoThuongDanhSon != null)
				{
					return RequestMoThuongDanhSon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestMoHetDanhSon":
				if (RequestMoHetDanhSon != null)
				{
					return RequestMoHetDanhSon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestVuotAiDanhSon":
				if (RequestVuotAiDanhSon != null)
				{
					return RequestVuotAiDanhSon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestChonDongDoiDanhSon":
				if (RequestChonDongDoiDanhSon != null)
				{
					return RequestChonDongDoiDanhSon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetDongDoiDanhSon":
				if (RequestGetDongDoiDanhSon != null)
				{
					return RequestGetDongDoiDanhSon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestHuaNguyen":
				if (RequestHuaNguyen != null)
				{
					return RequestHuaNguyen(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestLenCapNhanThuong":
				if (RequestLenCapNhanThuong != null)
				{
					return RequestLenCapNhanThuong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestThanTai":
				if (RequestThanTai != null)
				{
					return RequestThanTai(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestXocDiaInfo":
				if (RequestXocDiaInfo != null)
				{
					return RequestXocDiaInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestChoiXocDia":
				if (RequestChoiXocDia != null)
				{
					return RequestChoiXocDia(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestCuuVienTieuPhong":
				if (RequestCuuVienTieuPhong != null)
				{
					return RequestCuuVienTieuPhong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestNhanRuongThachSanh":
				if (RequestNhanRuongThachSanh != null)
				{
					return RequestNhanRuongThachSanh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestPaymentConfirm":
				if (RequestPaymentConfirm != null)
				{
					return RequestPaymentConfirm(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestDangNhapNhanThuong":
				if (RequestDangNhapNhanThuong != null)
				{
					return RequestDangNhapNhanThuong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestUongRuouTieuPhong":
				if (RequestUongRuouTieuPhong != null)
				{
					return RequestUongRuouTieuPhong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetDoiRuouInfo":
				if (RequestGetDoiRuouInfo != null)
				{
					return RequestGetDoiRuouInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestDoiRuou":
				if (RequestDoiRuou != null)
				{
					return RequestDoiRuou(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestBatCoc":
				if (RequestBatCoc != null)
				{
					return RequestBatCoc(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestChuocThan":
				if (RequestChuocThan != null)
				{
					return RequestChuocThan(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestSearchBanBe":
				if (RequestSearchBanBe != null)
				{
					return RequestSearchBanBe(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestThachDau":
				if (RequestThachDau != null)
				{
					return RequestThachDau(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestNhanThachDau":
				if (RequestNhanThachDau != null)
				{
					return RequestNhanThachDau(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestAddBanBe":
				if (RequestAddBanBe != null)
				{
					return RequestAddBanBe(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestAcceptBanBe":
				if (RequestAcceptBanBe != null)
				{
					return RequestAcceptBanBe(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestDeleteBanBe":
				if (RequestDeleteBanBe != null)
				{
					return RequestDeleteBanBe(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestBanBeCuuThuInfo":
				if (RequestBanBeCuuThuInfo != null)
				{
					return RequestBanBeCuuThuInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestChatInfo":
				if (RequestChatInfo != null)
				{
					return RequestChatInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestSendChatAll":
				if (RequestSendChatAll != null)
				{
					return RequestSendChatAll(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestDatTenMonPhai":
				if (RequestDatTenMonPhai != null)
				{
					return RequestDatTenMonPhai(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestULinhInfo":
				if (RequestULinhInfo != null)
				{
					return RequestULinhInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestDoiItemULinh":
				if (RequestDoiItemULinh != null)
				{
					return RequestDoiItemULinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestKnbRefreshULinh":
				if (RequestKnbRefreshULinh != null)
				{
					return RequestKnbRefreshULinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestDoiThuongULinh":
				if (RequestDoiThuongULinh != null)
				{
					return RequestDoiThuongULinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetTopULinh":
				if (RequestGetTopULinh != null)
				{
					return RequestGetTopULinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestKichHoatGiftCode":
				if (RequestKichHoatGiftCode != null)
				{
					return RequestKichHoatGiftCode(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestUseCustomItem":
				if (RequestUseCustomItem != null)
				{
					return RequestUseCustomItem(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestBuyVatPhamAndUse":
				if (RequestBuyVatPhamAndUse != null)
				{
					return RequestBuyVatPhamAndUse(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestHighlight":
				if (RequestHighlight != null)
				{
					return RequestHighlight(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestUseMailPhanThuong":
				if (RequestUseMailPhanThuong != null)
				{
					return RequestUseMailPhanThuong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestReadAllMail":
				if (RequestReadAllMail != null)
				{
					return RequestReadAllMail(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestRefreshMail":
				if (RequestRefreshMail != null)
				{
					return RequestRefreshMail(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestUseRuongThan":
				if (RequestUseRuongThan != null)
				{
					return RequestUseRuongThan(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestBatTho":
				if (RequestBatTho != null)
				{
					return RequestBatTho(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetFriendsDoiHinh":
				if (RequestGetFriendsDoiHinh != null)
				{
					return RequestGetFriendsDoiHinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestXemThongTinMonPhai":
				if (RequestXemThongTinMonPhai != null)
				{
					return RequestXemThongTinMonPhai(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestSendMail":
				if (RequestSendMail != null)
				{
					return RequestSendMail(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestDangNhapQuayXoSo":
				if (RequestDangNhapQuayXoSo != null)
				{
					return RequestDangNhapQuayXoSo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestTangTheLuc":
				if (RequestTangTheLuc != null)
				{
					return RequestTangTheLuc(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestDoiDo":
				if (RequestDoiDo != null)
				{
					return RequestDoiDo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetDuaTopLevelInfo":
				if (RequestGetDuaTopLevelInfo != null)
				{
					return RequestGetDuaTopLevelInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetDuaTopLuanKiemInfo":
				if (RequestGetDuaTopLuanKiemInfo != null)
				{
					return RequestGetDuaTopLuanKiemInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestThamGiaCT2":
				if (RequestThamGiaCT2 != null)
				{
					return RequestThamGiaCT2(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestCT2PlayerPos":
				if (RequestCT2PlayerPos != null)
				{
					JsonData jsonData2 = JsonMapper.ToObject(data);
					return RequestCT2PlayerPos(HostID.Server, new RmiContext(), (long)jsonData2[0], (long)jsonData2[1], (long)jsonData2[2], (long)jsonData2[3]);
				}
				return true;
			case "RequestListCT2":
				if (RequestListCT2 != null)
				{
					return RequestListCT2(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestLapLienMinh":
				if (RequestLapLienMinh != null)
				{
					return RequestLapLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestTestCT2":
				if (RequestTestCT2 != null)
				{
					return RequestTestCT2(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGiaNhapLienMinh":
				if (RequestGiaNhapLienMinh != null)
				{
					return RequestGiaNhapLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestChapNhanGiaNhapLienMinh":
				if (RequestChapNhanGiaNhapLienMinh != null)
				{
					return RequestChapNhanGiaNhapLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestThoatLienMinh":
				if (RequestThoatLienMinh != null)
				{
					return RequestThoatLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestFinishNhiemVuLienMinh":
				if (RequestFinishNhiemVuLienMinh != null)
				{
					return RequestFinishNhiemVuLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestResetNhiemVuLienMinh":
				if (RequestResetNhiemVuLienMinh != null)
				{
					return RequestResetNhiemVuLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestRutGiaNhapLienMinh":
				if (RequestRutGiaNhapLienMinh != null)
				{
					return RequestRutGiaNhapLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestDangHuongLienMinh":
				if (RequestDangHuongLienMinh != null)
				{
					return RequestDangHuongLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestDoiThuongLienMinh":
				if (RequestDoiThuongLienMinh != null)
				{
					return RequestDoiThuongLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestDoiMinhChu":
				if (RequestDoiMinhChu != null)
				{
					return RequestDoiMinhChu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestDoiPhoMinhChu":
				if (RequestDoiPhoMinhChu != null)
				{
					return RequestDoiPhoMinhChu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestNangCapCongTrinh":
				if (RequestNangCapCongTrinh != null)
				{
					return RequestNangCapCongTrinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestCT2EndBattle":
				if (RequestCT2EndBattle != null)
				{
					return RequestCT2EndBattle(HostID.Server, new RmiContext());
				}
				return true;
			case "RequestKhamNgoc":
				if (RequestKhamNgoc != null)
				{
					return RequestKhamNgoc(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGoNgoc":
				if (RequestGoNgoc != null)
				{
					return RequestGoNgoc(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetTopLienMinh":
				if (RequestGetTopLienMinh != null)
				{
					return RequestGetTopLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestSearchLienMinh":
				if (RequestSearchLienMinh != null)
				{
					return RequestSearchLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetThongTinLienMinh":
				if (RequestGetThongTinLienMinh != null)
				{
					return RequestGetThongTinLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestNhanThuongDiHoaCungAll":
				if (RequestNhanThuongDiHoaCungAll != null)
				{
					return RequestNhanThuongDiHoaCungAll(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetDiHoaCungInfo":
				if (RequestGetDiHoaCungInfo != null)
				{
					return RequestGetDiHoaCungInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetTopDiHoaCung":
				if (RequestGetTopDiHoaCung != null)
				{
					return RequestGetTopDiHoaCung(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestDuoiKhoiLienMinh":
				if (RequestDuoiKhoiLienMinh != null)
				{
					return RequestDuoiKhoiLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestCT2BangXepHangTuanNay":
				if (RequestCT2BangXepHangTuanNay != null)
				{
					return RequestCT2BangXepHangTuanNay(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestCT2BangXepHangTuanTruoc":
				if (RequestCT2BangXepHangTuanTruoc != null)
				{
					return RequestCT2BangXepHangTuanTruoc(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestThamGiaLuaTraiLienMinh":
				if (RequestThamGiaLuaTraiLienMinh != null)
				{
					return RequestThamGiaLuaTraiLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestLienMinhThoiLua":
				if (RequestLienMinhThoiLua != null)
				{
					return RequestLienMinhThoiLua(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestRoiKhoiLuaTraiLienMinh":
				if (RequestRoiKhoiLuaTraiLienMinh != null)
				{
					return RequestRoiKhoiLuaTraiLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestSetGioLuaTraiLienMinh":
				if (RequestSetGioLuaTraiLienMinh != null)
				{
					return RequestSetGioLuaTraiLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestSuaThongBaoLienMinh":
				if (RequestSuaThongBaoLienMinh != null)
				{
					return RequestSuaThongBaoLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestSendChatLienMinh":
				if (RequestSendChatLienMinh != null)
				{
					return RequestSendChatLienMinh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestChatLienMinhInfo":
				if (RequestChatLienMinhInfo != null)
				{
					return RequestChatLienMinhInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestPhanRaTrangBi":
				if (RequestPhanRaTrangBi != null)
				{
					return RequestPhanRaTrangBi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetExpLuaTrai":
				if (RequestGetExpLuaTrai != null)
				{
					return RequestGetExpLuaTrai(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestCT2GetBXH":
				if (RequestCT2GetBXH != null)
				{
					return RequestCT2GetBXH(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestUpdateLienMinhData":
				if (RequestUpdateLienMinhData != null)
				{
					return RequestUpdateLienMinhData(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestCT2Quit":
				if (RequestCT2Quit != null)
				{
					return RequestCT2Quit(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestCardPayment":
				if (RequestCardPayment != null)
				{
					return RequestCardPayment(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestLapNguyenKhi":
				if (RequestLapNguyenKhi != null)
				{
					return RequestLapNguyenKhi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestThaoNguyenKhi":
				if (RequestThaoNguyenKhi != null)
				{
					return RequestThaoNguyenKhi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestNangCapNguyenKhi":
				if (RequestNangCapNguyenKhi != null)
				{
					return RequestNangCapNguyenKhi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestMuaNguyenKhi":
				if (RequestMuaNguyenKhi != null)
				{
					return RequestMuaNguyenKhi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestCT2PlayerMove":
				if (RequestCT2PlayerMove != null)
				{
					JsonData jsonData5 = JsonMapper.ToObject(data);
					return RequestCT2PlayerMove(HostID.Server, new RmiContext(), (long)jsonData5[0], (long)jsonData5[1], (long)jsonData5[2], (long)jsonData5[3], (long)jsonData5[4]);
				}
				return true;
			case "RequestCT2PlayerPos_":
				if (RequestCT2PlayerPos_ != null)
				{
					JsonData jsonData4 = JsonMapper.ToObject(data);
					return RequestCT2PlayerPos_(HostID.Server, new RmiContext(), (long)jsonData4[0], (long)jsonData4[1], (long)jsonData4[2], (long)jsonData4[3], (long)jsonData4[4]);
				}
				return true;
			case "RequestBangChienGetInfo":
				if (RequestBangChienGetInfo != null)
				{
					return RequestBangChienGetInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestBangChienVaoThanh":
				if (RequestBangChienVaoThanh != null)
				{
					return RequestBangChienVaoThanh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestBangChienRoiThanh":
				if (RequestBangChienRoiThanh != null)
				{
					return RequestBangChienRoiThanh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestBangChienDenCongThanh":
				if (RequestBangChienDenCongThanh != null)
				{
					return RequestBangChienDenCongThanh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestBangChienCongThanh":
				if (RequestBangChienCongThanh != null)
				{
					return RequestBangChienCongThanh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestChonHatGiong":
				if (RequestChonHatGiong != null)
				{
					return RequestChonHatGiong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestLayHatGiong":
				if (RequestLayHatGiong != null)
				{
					return RequestLayHatGiong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestTrongCay":
				if (RequestTrongCay != null)
				{
					return RequestTrongCay(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestThuHoach":
				if (RequestThuHoach != null)
				{
					return RequestThuHoach(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestAnTrom":
				if (RequestAnTrom != null)
				{
					return RequestAnTrom(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetAnTromList":
				if (RequestGetAnTromList != null)
				{
					return RequestGetAnTromList(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestBangChienUserMove":
				if (RequestBangChienUserMove != null)
				{
					JsonData jsonData3 = JsonMapper.ToObject(data);
					return RequestBangChienUserMove(HostID.Server, new RmiContext(), (int)jsonData3[0], (long)jsonData3[1], (long)jsonData3[2], (long)jsonData3[3]);
				}
				return true;
			case "RequestBangChienOtherUser":
				if (RequestBangChienOtherUser != null)
				{
					return RequestBangChienOtherUser(HostID.Server, new RmiContext(), int.Parse(data));
				}
				return true;
			case "RequestBangChienGetPhanThuongThuThanh":
				if (RequestBangChienGetPhanThuongThuThanh != null)
				{
					return RequestBangChienGetPhanThuongThuThanh(HostID.Server, new RmiContext(), int.Parse(data));
				}
				return true;
			case "RequestSendChatLienSrv":
				if (RequestSendChatLienSrv != null)
				{
					return RequestSendChatLienSrv(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetChatLienSrv":
				if (RequestGetChatLienSrv != null)
				{
					return RequestGetChatLienSrv(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetLinhDuocInfo":
				if (RequestGetLinhDuocInfo != null)
				{
					return RequestGetLinhDuocInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestBanhChungPlayerMove":
				if (RequestBanhChungPlayerMove != null)
				{
					return RequestBanhChungPlayerMove(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestBanhChungGetInfo":
				if (RequestBanhChungGetInfo != null)
				{
					return RequestBanhChungGetInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestBanhChungNauBanh":
				if (RequestBanhChungNauBanh != null)
				{
					return RequestBanhChungNauBanh(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestBanhChungGetOthers":
				if (RequestBanhChungGetOthers != null)
				{
					return RequestBanhChungGetOthers(HostID.Server, new RmiContext());
				}
				return true;
			case "RequestBanhChungNhatNguyenLieu":
				if (RequestBanhChungNhatNguyenLieu != null)
				{
					return RequestBanhChungNhatNguyenLieu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestBanhChungBXH":
				if (RequestBanhChungBXH != null)
				{
					return RequestBanhChungBXH(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestBanhChungGetPhanThuong":
				if (RequestBanhChungGetPhanThuong != null)
				{
					return RequestBanhChungGetPhanThuong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestDangNhapNhanThuongTet":
				if (RequestDangNhapNhanThuongTet != null)
				{
					return RequestDangNhapNhanThuongTet(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestThaoNgua":
				if (RequestThaoNgua != null)
				{
					return RequestThaoNgua(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestDungNgua":
				if (RequestDungNgua != null)
				{
					return RequestDungNgua(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestActiveNgua":
				if (RequestActiveNgua != null)
				{
					return RequestActiveNgua(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetGamerLinhDuoc":
				if (RequestGetGamerLinhDuoc != null)
				{
					return RequestGetGamerLinhDuoc(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestCuongHoaBatQuaiTran":
				if (RequestCuongHoaBatQuaiTran != null)
				{
					return RequestCuongHoaBatQuaiTran(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestSetSoDoBatQuaiTran":
				if (RequestSetSoDoBatQuaiTran != null)
				{
					return RequestSetSoDoBatQuaiTran(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetLeagueData":
				if (RequestGetLeagueData != null)
				{
					return RequestGetLeagueData(HostID.Server, new RmiContext());
				}
				return true;
			case "RequestSetDoiHinhThienCangTran":
				if (RequestSetDoiHinhThienCangTran != null)
				{
					return RequestSetDoiHinhThienCangTran(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestOpenDoiHinhThienCangTran":
				if (RequestOpenDoiHinhThienCangTran != null)
				{
					return RequestOpenDoiHinhThienCangTran(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetSieuCupData":
				if (RequestGetSieuCupData != null)
				{
					return RequestGetSieuCupData(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetSieuCupBattle":
				if (RequestGetSieuCupBattle != null)
				{
					return RequestGetSieuCupBattle(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestSieuCupDatCuoc":
				if (RequestSieuCupDatCuoc != null)
				{
					return RequestSieuCupDatCuoc(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestSubmitDoiHinhLeague":
				if (RequestSubmitDoiHinhLeague != null)
				{
					return RequestSubmitDoiHinhLeague(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestViewLeagueReplay":
				if (RequestViewLeagueReplay != null)
				{
					return RequestViewLeagueReplay(HostID.Server, new RmiContext(), int.Parse(data));
				}
				return true;
			case "RequestThamBaiSieuCup":
				if (RequestThamBaiSieuCup != null)
				{
					return RequestThamBaiSieuCup(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestVongQuay":
				if (RequestVongQuay != null)
				{
					return RequestVongQuay(HostID.Server, new RmiContext(), int.Parse(data));
				}
				return true;
			case "RequestLienDauData":
				if (RequestLienDauData != null)
				{
					return RequestLienDauData(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestSieuCupChampion":
				if (RequestSieuCupChampion != null)
				{
					return RequestSieuCupChampion(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestQMDInfo":
				if (RequestQMDInfo != null)
				{
					return RequestQMDInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestQMDSelect":
				if (RequestQMDSelect != null)
				{
					return RequestQMDSelect(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestQMDGetChiTietNPC":
				if (RequestQMDGetChiTietNPC != null)
				{
					return RequestQMDGetChiTietNPC(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestQMDGetBXH":
				if (RequestQMDGetBXH != null)
				{
					return RequestQMDGetBXH(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestQMDXongPha":
				if (RequestQMDXongPha != null)
				{
					return RequestQMDXongPha(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestBeQuanDeTu":
				if (RequestBeQuanDeTu != null)
				{
					return RequestBeQuanDeTu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestQMDTranHinhChienThuat":
				if (RequestQMDTranHinhChienThuat != null)
				{
					return RequestQMDTranHinhChienThuat(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestNhanThuongTichLuyNap":
				if (RequestNhanThuongTichLuyNap != null)
				{
					return RequestNhanThuongTichLuyNap(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestNhanThuongTichLuyTieu":
				if (RequestNhanThuongTichLuyTieu != null)
				{
					return RequestNhanThuongTichLuyTieu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetCacLoaiTop":
				if (RequestGetCacLoaiTop != null)
				{
					return RequestGetCacLoaiTop(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestDoiTenBang":
				if (RequestDoiTenBang != null)
				{
					return RequestDoiTenBang(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetThongTinLienServer":
				if (RequestGetThongTinLienServer != null)
				{
					return RequestGetThongTinLienServer(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetTopPhaoHoa":
				if (RequestGetTopPhaoHoa != null)
				{
					return RequestGetTopPhaoHoa(HostID.Server, new RmiContext());
				}
				return true;
			case "RequestGetPhanThuongPhaoHoa":
				if (RequestGetPhanThuongPhaoHoa != null)
				{
					return RequestGetPhanThuongPhaoHoa(HostID.Server, new RmiContext());
				}
				return true;
			case "RequestBanPhaoHoaEvent":
				if (RequestBanPhaoHoaEvent != null)
				{
					return RequestBanPhaoHoaEvent(HostID.Server, new RmiContext());
				}
				return true;
			case "RequestGetTopVongQuay":
				if (RequestGetTopVongQuay != null)
				{
					return RequestGetTopVongQuay(HostID.Server, new RmiContext());
				}
				return true;
			case "RequestCreateCostume":
				if (RequestCreateCostume != null)
				{
					return RequestCreateCostume(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestTinhLuyenCostume":
				if (RequestTinhLuyenCostume != null)
				{
					return RequestTinhLuyenCostume(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestKhamNgocCostume":
				if (RequestKhamNgocCostume != null)
				{
					return RequestKhamNgocCostume(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGoNgocCostume":
				if (RequestGoNgocCostume != null)
				{
					return RequestGoNgocCostume(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestTakeOnCostume":
				if (RequestTakeOnCostume != null)
				{
					return RequestTakeOnCostume(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestTakeOffCostume":
				if (RequestTakeOffCostume != null)
				{
					return RequestTakeOffCostume(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestBanPhaoHoa":
				if (RequestBanPhaoHoa != null)
				{
					return RequestBanPhaoHoa(HostID.Server, new RmiContext());
				}
				return true;
			case "RequestLinhThuongPhaoHoaEvent":
				if (RequestLinhThuongPhaoHoaEvent != null)
				{
					return RequestLinhThuongPhaoHoaEvent(HostID.Server, new RmiContext(), int.Parse(data));
				}
				return true;
			case "RequestChuyenSinhDeTu":
				if (RequestChuyenSinhDeTu != null)
				{
					return RequestChuyenSinhDeTu(HostID.Server, new RmiContext(), int.Parse(data));
				}
				return true;
			case "RequestGetListOtherUser":
				if (RequestGetListOtherUser != null)
				{
					return RequestGetListOtherUser(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestBatThanThu":
				if (RequestBatThanThu != null)
				{
					return RequestBatThanThu(HostID.Server, new RmiContext());
				}
				return true;
			case "RequestNhanThuongDapNieu":
				if (RequestNhanThuongDapNieu != null)
				{
					return RequestNhanThuongDapNieu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestTruongThanhThanThu":
				if (RequestTruongThanhThanThu != null)
				{
					return RequestTruongThanhThanThu(HostID.Server, new RmiContext(), int.Parse(data));
				}
				return true;
			case "RequestNangPhamThanThu":
				if (RequestNangPhamThanThu != null)
				{
					return RequestNangPhamThanThu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestDoiThanThu":
				if (RequestDoiThanThu != null)
				{
					return RequestDoiThanThu(HostID.Server, new RmiContext(), int.Parse(data));
				}
				return true;
			case "RequestThonPheThanThu":
				if (RequestThonPheThanThu != null)
				{
					return RequestThonPheThanThu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestTruyenCongThanThu":
				if (RequestTruyenCongThanThu != null)
				{
					return RequestTruyenCongThanThu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestSetBoPhapNhanVatBatQuai":
				if (RequestSetBoPhapNhanVatBatQuai != null)
				{
					return RequestSetBoPhapNhanVatBatQuai(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestSetNoiCongNhanVatBatQuai":
				if (RequestSetNoiCongNhanVatBatQuai != null)
				{
					return RequestSetNoiCongNhanVatBatQuai(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestSetTrangBiNhanVatBatQuai":
				if (RequestSetTrangBiNhanVatBatQuai != null)
				{
					return RequestSetTrangBiNhanVatBatQuai(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetThanThuDao":
				if (RequestGetThanThuDao != null)
				{
					return RequestGetThanThuDao(HostID.Server, new RmiContext());
				}
				return true;
			case "RequestAutoResolveThanThuDao":
				if (RequestAutoResolveThanThuDao != null)
				{
					return RequestAutoResolveThanThuDao(HostID.Server, new RmiContext());
				}
				return true;
			case "RequestSetThanThu":
				if (RequestSetThanThu != null)
				{
					return RequestSetThanThu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetGuiTietKiem":
				if (RequestGetGuiTietKiem != null)
				{
					return RequestGetGuiTietKiem(HostID.Server, new RmiContext(), int.Parse(data));
				}
				return true;
			case "RequestThamGiaGuiTietKiem":
				if (RequestThamGiaGuiTietKiem != null)
				{
					return RequestThamGiaGuiTietKiem(HostID.Server, new RmiContext());
				}
				return true;
			case "RequestGetThuong1MilUser":
				if (RequestGetThuong1MilUser != null)
				{
					return RequestGetThuong1MilUser(HostID.Server, new RmiContext());
				}
				return true;
			case "RequestUseRuongThanBi":
				if (RequestUseRuongThanBi != null)
				{
					return RequestUseRuongThanBi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestThuHoachSonMon":
				if (RequestThuHoachSonMon != null)
				{
					return RequestThuHoachSonMon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestXayDungSonMon":
				if (RequestXayDungSonMon != null)
				{
					return RequestXayDungSonMon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestTanCongSonMon":
				if (RequestTanCongSonMon != null)
				{
					return RequestTanCongSonMon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestDoiHinhSonMon":
				if (RequestDoiHinhSonMon != null)
				{
					return RequestDoiHinhSonMon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestDoThamSonMon":
				if (RequestDoThamSonMon != null)
				{
					return RequestDoThamSonMon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestMuaDoThanBi":
				if (RequestMuaDoThanBi != null)
				{
					return RequestMuaDoThanBi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetTopSonMon":
				if (RequestGetTopSonMon != null)
				{
					return RequestGetTopSonMon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestDungLuyenTrangBi":
				if (RequestDungLuyenTrangBi != null)
				{
					return RequestDungLuyenTrangBi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestTayLuyenTrangBi":
				if (RequestTayLuyenTrangBi != null)
				{
					return RequestTayLuyenTrangBi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestKhaiQuangTrangBi":
				if (RequestKhaiQuangTrangBi != null)
				{
					return RequestKhaiQuangTrangBi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestConfirmTayLuyenTrangBi":
				if (RequestConfirmTayLuyenTrangBi != null)
				{
					return RequestConfirmTayLuyenTrangBi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetSonMonInfo":
				if (RequestGetSonMonInfo != null)
				{
					return RequestGetSonMonInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetLanhDiaInfo":
				if (RequestGetLanhDiaInfo != null)
				{
					return RequestGetLanhDiaInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestUnLockVoCong":
				if (RequestUnLockVoCong != null)
				{
					return RequestUnLockVoCong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestQuayBacMayMan":
				if (RequestQuayBacMayMan != null)
				{
					return RequestQuayBacMayMan(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestMoveLanhDia":
				if (RequestMoveLanhDia != null)
				{
					return RequestMoveLanhDia(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestQuayTuBaoBon":
				if (RequestQuayTuBaoBon != null)
				{
					return RequestQuayTuBaoBon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetTopMoRuong":
				if (RequestGetTopMoRuong != null)
				{
					return RequestGetTopMoRuong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestQuayThienMaHaPhong":
				if (RequestQuayThienMaHaPhong != null)
				{
					return RequestQuayThienMaHaPhong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetTayVucInfo":
				if (RequestGetTayVucInfo != null)
				{
					return RequestGetTayVucInfo(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestMuaDoTayVuc":
				if (RequestMuaDoTayVuc != null)
				{
					return RequestMuaDoTayVuc(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestSummonNienThu":
				if (RequestSummonNienThu != null)
				{
					return RequestSummonNienThu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestDanhNienThu":
				if (RequestDanhNienThu != null)
				{
					return RequestDanhNienThu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestThamGiaNienThu":
				if (RequestThamGiaNienThu != null)
				{
					return RequestThamGiaNienThu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestNhanThuongNapHangNgay":
				if (RequestNhanThuongNapHangNgay != null)
				{
					return RequestNhanThuongNapHangNgay(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetTopNienThu":
				if (RequestGetTopNienThu != null)
				{
					return RequestGetTopNienThu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestNopLenhBaiNienThu":
				if (RequestNopLenhBaiNienThu != null)
				{
					return RequestNopLenhBaiNienThu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetTopLanhDia":
				if (RequestGetTopLanhDia != null)
				{
					return RequestGetTopLanhDia(HostID.Server, new RmiContext());
				}
				return true;
			case "RequestSelectStartNgua":
				if (RequestSelectStartNgua != null)
				{
					return RequestSelectStartNgua(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetDiemMoRuong":
				if (RequestGetDiemMoRuong != null)
				{
					return RequestGetDiemMoRuong(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestUpdateDailyActivities":
				if (RequestUpdateDailyActivities != null)
				{
					return RequestUpdateDailyActivities(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestThuongDailyActivities":
				if (RequestThuongDailyActivities != null)
				{
					return RequestThuongDailyActivities(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestThienMaQuaySlot":
				if (RequestThienMaQuaySlot != null)
				{
					return RequestThienMaQuaySlot(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestThienMaLenhCreate":
				if (RequestThienMaLenhCreate != null)
				{
					return RequestThienMaLenhCreate(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestThienMaUpgradeSlot":
				if (RequestThienMaUpgradeSlot != null)
				{
					return RequestThienMaUpgradeSlot(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestThienMaOpenSlot":
				if (RequestThienMaOpenSlot != null)
				{
					return RequestThienMaOpenSlot(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestThienMaEquip":
				if (RequestThienMaEquip != null)
				{
					return RequestThienMaEquip(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestThienMaQuaySlotConfirm":
				if (RequestThienMaQuaySlotConfirm != null)
				{
					JsonData jsonData = JsonMapper.ToObject(data);
					return RequestThienMaQuaySlotConfirm(HostID.Server, new RmiContext(), (bool)jsonData[0]);
				}
				return true;
			case "RequestSetTonHieu":
				if (RequestSetTonHieu != null)
				{
					return RequestSetTonHieu(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestSetTrangBiHoangKim":
				if (RequestSetTrangBiHoangKim != null)
				{
					return RequestSetTrangBiHoangKim(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetTopDaiHoiVoLam":
				if (RequestGetTopDaiHoiVoLam != null)
				{
					return RequestGetTopDaiHoiVoLam(HostID.Server, new RmiContext());
				}
				return true;
			case "RequestDanhAnDanhCaoThu":
				if (RequestDanhAnDanhCaoThu != null)
				{
					return RequestDanhAnDanhCaoThu(HostID.Server, new RmiContext());
				}
				return true;
			case "RequestStartBoiDuongTrangBi":
				if (RequestStartBoiDuongTrangBi != null)
				{
					return RequestStartBoiDuongTrangBi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestEndBoiDuongTrangBi":
				if (RequestEndBoiDuongTrangBi != null)
				{
					return RequestEndBoiDuongTrangBi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestRutQueTienNhan":
				if (RequestRutQueTienNhan != null)
				{
					return RequestRutQueTienNhan(HostID.Server, new RmiContext());
				}
				return true;
			case "RequestGetBaoKhoInfo":
				if (RequestGetBaoKhoInfo != null)
				{
					return RequestGetBaoKhoInfo(HostID.Server, new RmiContext());
				}
				return true;
			case "RequestCuopBaoKho":
				if (RequestCuopBaoKho != null)
				{
					return RequestCuopBaoKho(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestThuHoachBaoKho":
				if (RequestThuHoachBaoKho != null)
				{
					return RequestThuHoachBaoKho(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetListBaoKho":
				if (RequestGetListBaoKho != null)
				{
					return RequestGetListBaoKho(HostID.Server, new RmiContext());
				}
				return true;
			case "RequestQuayCamCungBiBao":
				if (RequestQuayCamCungBiBao != null)
				{
					return RequestQuayCamCungBiBao(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestNhanThuongCamCung":
				if (RequestNhanThuongCamCung != null)
				{
					return RequestNhanThuongCamCung(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestGetHoaVangInfo":
				if (RequestGetHoaVangInfo != null)
				{
					return RequestGetHoaVangInfo(HostID.Server, new RmiContext());
				}
				return true;
			case "RequestHoaVang":
				if (RequestHoaVang != null)
				{
					return RequestHoaVang(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestTestProudNet":
				if (RequestTestProudNet != null)
				{
					return RequestTestProudNet(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestTrangBiHuyenKhi":
				if (RequestTrangBiHuyenKhi != null)
				{
					return RequestTrangBiHuyenKhi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestThangCapHuyenKhi":
				if (RequestThangCapHuyenKhi != null)
				{
					return RequestThangCapHuyenKhi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestChienHonChangeBuff":
				if (RequestChienHonChangeBuff != null)
				{
					return RequestChienHonChangeBuff(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestStartBoiDuongChienHon":
				if (RequestStartBoiDuongChienHon != null)
				{
					return RequestStartBoiDuongChienHon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestEndBoiDuongChienHon":
				if (RequestEndBoiDuongChienHon != null)
				{
					return RequestEndBoiDuongChienHon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestTrieuHoiChienHon":
				if (RequestTrieuHoiChienHon != null)
				{
					return RequestTrieuHoiChienHon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestUseTanHonTangLevelChienHon":
				if (RequestUseTanHonTangLevelChienHon != null)
				{
					return RequestUseTanHonTangLevelChienHon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestDotPhaChienHon":
				if (RequestDotPhaChienHon != null)
				{
					return RequestDotPhaChienHon(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestCheTaoHuyenKhi":
				if (RequestCheTaoHuyenKhi != null)
				{
					return RequestCheTaoHuyenKhi(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestQuayDiemHoaVang":
				if (RequestQuayDiemHoaVang != null)
				{
					return RequestQuayDiemHoaVang(HostID.Server, new RmiContext());
				}
				return true;
			case "RequestUseTuiThan":
				if (RequestUseTuiThan != null)
				{
					return RequestUseTuiThan(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "RequestNhanThuongDailyKNB":
				if (RequestNhanThuongDailyKNB != null)
				{
					return RequestNhanThuongDailyKNB(HostID.Server, new RmiContext());
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
			case "RequestNextLogon":
				return RequestNextLogon != null;
			case "RequestSetInfo":
				return RequestSetInfo != null;
			case "RequestSetHeroData":
				return RequestSetHeroData != null;
			case "RequestGetBattleResult":
				return RequestGetBattleResult != null;
			case "RequestGetListLK":
				return RequestGetListLK != null;
			case "RequestSetHeroCfg":
				return RequestSetHeroCfg != null;
			case "RequestGetInfo":
				return RequestGetInfo != null;
			case "RequestChangeNextPos":
				return RequestChangeNextPos != null;
			case "RequestGetListOtherPlayer":
				return RequestGetListOtherPlayer != null;
			case "RequestGetLuanKiemInfo":
				return RequestGetLuanKiemInfo != null;
			case "RequestCapNhatDiemLuanKiem":
				return RequestCapNhatDiemLuanKiem != null;
			case "RequestNhanThuongLuanKiem":
				return RequestNhanThuongLuanKiem != null;
			case "RequestDoiThuongLuanKiem":
				return RequestDoiThuongLuanKiem != null;
			case "RequestDauLuanKiem":
				return RequestDauLuanKiem != null;
			case "RequestDanhGiangHo":
				return RequestDanhGiangHo != null;
			case "RequestSetVCSetting":
				return RequestSetVCSetting != null;
			case "RequestSetTrangBi":
				return RequestSetTrangBi != null;
			case "RequestSetDoiHinh":
				return RequestSetDoiHinh != null;
			case "RequestSendChatMsg":
				return RequestSendChatMsg != null;
			case "RequestSetVoCong":
				return RequestSetVoCong != null;
			case "RequestSetTranHinh":
				return RequestSetTranHinh != null;
			case "RequestStartBoiDuong":
				return RequestStartBoiDuong != null;
			case "RequestEndBoiDuong":
				return RequestEndBoiDuong != null;
			case "RequestTuLuyenDeTu":
				return RequestTuLuyenDeTu != null;
			case "RequestTrieuHoiDeTuBangHon":
				return RequestTrieuHoiDeTuBangHon != null;
			case "RequestTruyenCong":
				return RequestTruyenCong != null;
			case "RequestGetDongNhanInfo":
				return RequestGetDongNhanInfo != null;
			case "RequestDanhDongNhan":
				return RequestDanhDongNhan != null;
			case "RequestCuongHoaTrangBi":
				return RequestCuongHoaTrangBi != null;
			case "RequestBanTrangBi":
				return RequestBanTrangBi != null;
			case "RequestGhepManhTrangBi":
				return RequestGhepManhTrangBi != null;
			case "RequestGhepManhVoCong":
				return RequestGhepManhVoCong != null;
			case "RequestTinhLuyenTrangBi":
				return RequestTinhLuyenTrangBi != null;
			case "RequestSetDoiHinhHoTro":
				return RequestSetDoiHinhHoTro != null;
			case "RequestOpenDoiHinhHoTro":
				return RequestOpenDoiHinhHoTro != null;
			case "RequestThamNgoVoCong":
				return RequestThamNgoVoCong != null;
			case "RequestTinhLuyenVoCong":
				return RequestTinhLuyenVoCong != null;
			case "RequestLayDeTu":
				return RequestLayDeTu != null;
			case "RequestBuyVatPham":
				return RequestBuyVatPham != null;
			case "RequestCaoNhan":
				return RequestCaoNhan != null;
			case "RequestThuongNhan":
				return RequestThuongNhan != null;
			case "RequestBanDo":
				return RequestBanDo != null;
			case "RequestBangHuu":
				return RequestBangHuu != null;
			case "RequestTyThi":
				return RequestTyThi != null;
			case "RequestBuyLeBao":
				return RequestBuyLeBao != null;
			case "RequestHoiTheLuc":
				return RequestHoiTheLuc != null;
			case "RequestSelectStartDeTu":
				return RequestSelectStartDeTu != null;
			case "RequestKyNgoThamBai":
				return RequestKyNgoThamBai != null;
			case "RequestHuyetChienInfo":
				return RequestHuyetChienInfo != null;
			case "RequestStartHuyetChien":
				return RequestStartHuyetChien != null;
			case "RequestHoiSinhHuyetChien":
				return RequestHoiSinhHuyetChien != null;
			case "RequestHuyetChienTangThuocTinh":
				return RequestHuyetChienTangThuocTinh != null;
			case "RequestDanhHuyetChien":
				return RequestDanhHuyetChien != null;
			case "RequestNhanThuongHuyetChien":
				return RequestNhanThuongHuyetChien != null;
			case "RequestGetTopHuyetChien":
				return RequestGetTopHuyetChien != null;
			case "RequestOpenHop":
				return RequestOpenHop != null;
			case "RequestSetDoiHinhAndTranHinh":
				return RequestSetDoiHinhAndTranHinh != null;
			case "RequestTestDongNhan":
				return RequestTestDongNhan != null;
			case "RequestNhanThuongGiangHo":
				return RequestNhanThuongGiangHo != null;
			case "RequestDanhNhanhGiangHo":
				return RequestDanhNhanhGiangHo != null;
			case "RequestAnGaGiangHo":
				return RequestAnGaGiangHo != null;
			case "RequestResetLuotGiangHo":
				return RequestResetLuotGiangHo != null;
			case "RequestDanhDanhSon":
				return RequestDanhDanhSon != null;
			case "RequestMoThuongDanhSon":
				return RequestMoThuongDanhSon != null;
			case "RequestMoHetDanhSon":
				return RequestMoHetDanhSon != null;
			case "RequestVuotAiDanhSon":
				return RequestVuotAiDanhSon != null;
			case "RequestChonDongDoiDanhSon":
				return RequestChonDongDoiDanhSon != null;
			case "RequestGetDongDoiDanhSon":
				return RequestGetDongDoiDanhSon != null;
			case "RequestHuaNguyen":
				return RequestHuaNguyen != null;
			case "RequestLenCapNhanThuong":
				return RequestLenCapNhanThuong != null;
			case "RequestThanTai":
				return RequestThanTai != null;
			case "RequestXocDiaInfo":
				return RequestXocDiaInfo != null;
			case "RequestChoiXocDia":
				return RequestChoiXocDia != null;
			case "RequestCuuVienTieuPhong":
				return RequestCuuVienTieuPhong != null;
			case "RequestNhanRuongThachSanh":
				return RequestNhanRuongThachSanh != null;
			case "RequestPaymentConfirm":
				return RequestPaymentConfirm != null;
			case "RequestDangNhapNhanThuong":
				return RequestDangNhapNhanThuong != null;
			case "RequestUongRuouTieuPhong":
				return RequestUongRuouTieuPhong != null;
			case "RequestGetDoiRuouInfo":
				return RequestGetDoiRuouInfo != null;
			case "RequestDoiRuou":
				return RequestDoiRuou != null;
			case "RequestBatCoc":
				return RequestBatCoc != null;
			case "RequestChuocThan":
				return RequestChuocThan != null;
			case "RequestSearchBanBe":
				return RequestSearchBanBe != null;
			case "RequestThachDau":
				return RequestThachDau != null;
			case "RequestNhanThachDau":
				return RequestNhanThachDau != null;
			case "RequestAddBanBe":
				return RequestAddBanBe != null;
			case "RequestAcceptBanBe":
				return RequestAcceptBanBe != null;
			case "RequestDeleteBanBe":
				return RequestDeleteBanBe != null;
			case "RequestBanBeCuuThuInfo":
				return RequestBanBeCuuThuInfo != null;
			case "RequestChatInfo":
				return RequestChatInfo != null;
			case "RequestSendChatAll":
				return RequestSendChatAll != null;
			case "RequestDatTenMonPhai":
				return RequestDatTenMonPhai != null;
			case "RequestULinhInfo":
				return RequestULinhInfo != null;
			case "RequestDoiItemULinh":
				return RequestDoiItemULinh != null;
			case "RequestKnbRefreshULinh":
				return RequestKnbRefreshULinh != null;
			case "RequestDoiThuongULinh":
				return RequestDoiThuongULinh != null;
			case "RequestGetTopULinh":
				return RequestGetTopULinh != null;
			case "RequestKichHoatGiftCode":
				return RequestKichHoatGiftCode != null;
			case "RequestUseCustomItem":
				return RequestUseCustomItem != null;
			case "RequestBuyVatPhamAndUse":
				return RequestBuyVatPhamAndUse != null;
			case "RequestHighlight":
				return RequestHighlight != null;
			case "RequestUseMailPhanThuong":
				return RequestUseMailPhanThuong != null;
			case "RequestReadAllMail":
				return RequestReadAllMail != null;
			case "RequestRefreshMail":
				return RequestRefreshMail != null;
			case "RequestUseRuongThan":
				return RequestUseRuongThan != null;
			case "RequestBatTho":
				return RequestBatTho != null;
			case "RequestGetFriendsDoiHinh":
				return RequestGetFriendsDoiHinh != null;
			case "RequestXemThongTinMonPhai":
				return RequestXemThongTinMonPhai != null;
			case "RequestSendMail":
				return RequestSendMail != null;
			case "RequestDangNhapQuayXoSo":
				return RequestDangNhapQuayXoSo != null;
			case "RequestTangTheLuc":
				return RequestTangTheLuc != null;
			case "RequestDoiDo":
				return RequestDoiDo != null;
			case "RequestGetDuaTopLevelInfo":
				return RequestGetDuaTopLevelInfo != null;
			case "RequestGetDuaTopLuanKiemInfo":
				return RequestGetDuaTopLuanKiemInfo != null;
			case "RequestThamGiaCT2":
				return RequestThamGiaCT2 != null;
			case "RequestCT2PlayerPos":
				return RequestCT2PlayerPos != null;
			case "RequestListCT2":
				return RequestListCT2 != null;
			case "RequestLapLienMinh":
				return RequestLapLienMinh != null;
			case "RequestTestCT2":
				return RequestTestCT2 != null;
			case "RequestGiaNhapLienMinh":
				return RequestGiaNhapLienMinh != null;
			case "RequestChapNhanGiaNhapLienMinh":
				return RequestChapNhanGiaNhapLienMinh != null;
			case "RequestThoatLienMinh":
				return RequestThoatLienMinh != null;
			case "RequestFinishNhiemVuLienMinh":
				return RequestFinishNhiemVuLienMinh != null;
			case "RequestResetNhiemVuLienMinh":
				return RequestResetNhiemVuLienMinh != null;
			case "RequestRutGiaNhapLienMinh":
				return RequestRutGiaNhapLienMinh != null;
			case "RequestDangHuongLienMinh":
				return RequestDangHuongLienMinh != null;
			case "RequestDoiThuongLienMinh":
				return RequestDoiThuongLienMinh != null;
			case "RequestDoiMinhChu":
				return RequestDoiMinhChu != null;
			case "RequestDoiPhoMinhChu":
				return RequestDoiPhoMinhChu != null;
			case "RequestNangCapCongTrinh":
				return RequestNangCapCongTrinh != null;
			case "RequestCT2EndBattle":
				return RequestCT2EndBattle != null;
			case "RequestKhamNgoc":
				return RequestKhamNgoc != null;
			case "RequestGoNgoc":
				return RequestGoNgoc != null;
			case "RequestGetTopLienMinh":
				return RequestGetTopLienMinh != null;
			case "RequestSearchLienMinh":
				return RequestSearchLienMinh != null;
			case "RequestGetThongTinLienMinh":
				return RequestGetThongTinLienMinh != null;
			case "RequestNhanThuongDiHoaCungAll":
				return RequestNhanThuongDiHoaCungAll != null;
			case "RequestGetDiHoaCungInfo":
				return RequestGetDiHoaCungInfo != null;
			case "RequestGetTopDiHoaCung":
				return RequestGetTopDiHoaCung != null;
			case "RequestDuoiKhoiLienMinh":
				return RequestDuoiKhoiLienMinh != null;
			case "RequestCT2BangXepHangTuanNay":
				return RequestCT2BangXepHangTuanNay != null;
			case "RequestCT2BangXepHangTuanTruoc":
				return RequestCT2BangXepHangTuanTruoc != null;
			case "RequestThamGiaLuaTraiLienMinh":
				return RequestThamGiaLuaTraiLienMinh != null;
			case "RequestLienMinhThoiLua":
				return RequestLienMinhThoiLua != null;
			case "RequestRoiKhoiLuaTraiLienMinh":
				return RequestRoiKhoiLuaTraiLienMinh != null;
			case "RequestSetGioLuaTraiLienMinh":
				return RequestSetGioLuaTraiLienMinh != null;
			case "RequestSuaThongBaoLienMinh":
				return RequestSuaThongBaoLienMinh != null;
			case "RequestSendChatLienMinh":
				return RequestSendChatLienMinh != null;
			case "RequestChatLienMinhInfo":
				return RequestChatLienMinhInfo != null;
			case "RequestPhanRaTrangBi":
				return RequestPhanRaTrangBi != null;
			case "RequestGetExpLuaTrai":
				return RequestGetExpLuaTrai != null;
			case "RequestCT2GetBXH":
				return RequestCT2GetBXH != null;
			case "RequestUpdateLienMinhData":
				return RequestUpdateLienMinhData != null;
			case "RequestCT2Quit":
				return RequestCT2Quit != null;
			case "RequestCardPayment":
				return RequestCardPayment != null;
			case "RequestLapNguyenKhi":
				return RequestLapNguyenKhi != null;
			case "RequestThaoNguyenKhi":
				return RequestThaoNguyenKhi != null;
			case "RequestNangCapNguyenKhi":
				return RequestNangCapNguyenKhi != null;
			case "RequestMuaNguyenKhi":
				return RequestMuaNguyenKhi != null;
			case "RequestCT2PlayerMove":
				return RequestCT2PlayerMove != null;
			case "RequestCT2PlayerPos_":
				return RequestCT2PlayerPos_ != null;
			case "RequestBangChienGetInfo":
				return RequestBangChienGetInfo != null;
			case "RequestBangChienVaoThanh":
				return RequestBangChienVaoThanh != null;
			case "RequestBangChienRoiThanh":
				return RequestBangChienRoiThanh != null;
			case "RequestBangChienDenCongThanh":
				return RequestBangChienDenCongThanh != null;
			case "RequestBangChienCongThanh":
				return RequestBangChienCongThanh != null;
			case "RequestChonHatGiong":
				return RequestChonHatGiong != null;
			case "RequestLayHatGiong":
				return RequestLayHatGiong != null;
			case "RequestTrongCay":
				return RequestTrongCay != null;
			case "RequestThuHoach":
				return RequestThuHoach != null;
			case "RequestAnTrom":
				return RequestAnTrom != null;
			case "RequestGetAnTromList":
				return RequestGetAnTromList != null;
			case "RequestBangChienUserMove":
				return RequestBangChienUserMove != null;
			case "RequestBangChienOtherUser":
				return RequestBangChienOtherUser != null;
			case "RequestBangChienGetPhanThuongThuThanh":
				return RequestBangChienGetPhanThuongThuThanh != null;
			case "RequestSendChatLienSrv":
				return RequestSendChatLienSrv != null;
			case "RequestGetChatLienSrv":
				return RequestGetChatLienSrv != null;
			case "RequestGetLinhDuocInfo":
				return RequestGetLinhDuocInfo != null;
			case "RequestBanhChungPlayerMove":
				return RequestBanhChungPlayerMove != null;
			case "RequestBanhChungGetInfo":
				return RequestBanhChungGetInfo != null;
			case "RequestBanhChungNauBanh":
				return RequestBanhChungNauBanh != null;
			case "RequestBanhChungGetOthers":
				return RequestBanhChungGetOthers != null;
			case "RequestBanhChungNhatNguyenLieu":
				return RequestBanhChungNhatNguyenLieu != null;
			case "RequestBanhChungBXH":
				return RequestBanhChungBXH != null;
			case "RequestBanhChungGetPhanThuong":
				return RequestBanhChungGetPhanThuong != null;
			case "RequestDangNhapNhanThuongTet":
				return RequestDangNhapNhanThuongTet != null;
			case "RequestThaoNgua":
				return RequestThaoNgua != null;
			case "RequestDungNgua":
				return RequestDungNgua != null;
			case "RequestActiveNgua":
				return RequestActiveNgua != null;
			case "RequestGetGamerLinhDuoc":
				return RequestGetGamerLinhDuoc != null;
			case "RequestCuongHoaBatQuaiTran":
				return RequestCuongHoaBatQuaiTran != null;
			case "RequestSetSoDoBatQuaiTran":
				return RequestSetSoDoBatQuaiTran != null;
			case "RequestGetLeagueData":
				return RequestGetLeagueData != null;
			case "RequestSetDoiHinhThienCangTran":
				return RequestSetDoiHinhThienCangTran != null;
			case "RequestOpenDoiHinhThienCangTran":
				return RequestOpenDoiHinhThienCangTran != null;
			case "RequestGetSieuCupData":
				return RequestGetSieuCupData != null;
			case "RequestGetSieuCupBattle":
				return RequestGetSieuCupBattle != null;
			case "RequestSieuCupDatCuoc":
				return RequestSieuCupDatCuoc != null;
			case "RequestSubmitDoiHinhLeague":
				return RequestSubmitDoiHinhLeague != null;
			case "RequestViewLeagueReplay":
				return RequestViewLeagueReplay != null;
			case "RequestThamBaiSieuCup":
				return RequestThamBaiSieuCup != null;
			case "RequestVongQuay":
				return RequestVongQuay != null;
			case "RequestLienDauData":
				return RequestLienDauData != null;
			case "RequestSieuCupChampion":
				return RequestSieuCupChampion != null;
			case "RequestQMDInfo":
				return RequestQMDInfo != null;
			case "RequestQMDSelect":
				return RequestQMDSelect != null;
			case "RequestQMDGetChiTietNPC":
				return RequestQMDGetChiTietNPC != null;
			case "RequestQMDGetBXH":
				return RequestQMDGetBXH != null;
			case "RequestQMDXongPha":
				return RequestQMDXongPha != null;
			case "RequestBeQuanDeTu":
				return RequestBeQuanDeTu != null;
			case "RequestQMDTranHinhChienThuat":
				return RequestQMDTranHinhChienThuat != null;
			case "RequestNhanThuongTichLuyNap":
				return RequestNhanThuongTichLuyNap != null;
			case "RequestNhanThuongTichLuyTieu":
				return RequestNhanThuongTichLuyTieu != null;
			case "RequestGetCacLoaiTop":
				return RequestGetCacLoaiTop != null;
			case "RequestDoiTenBang":
				return RequestDoiTenBang != null;
			case "RequestGetThongTinLienServer":
				return RequestGetThongTinLienServer != null;
			case "RequestGetTopPhaoHoa":
				return RequestGetTopPhaoHoa != null;
			case "RequestGetPhanThuongPhaoHoa":
				return RequestGetPhanThuongPhaoHoa != null;
			case "RequestBanPhaoHoaEvent":
				return RequestBanPhaoHoaEvent != null;
			case "RequestGetTopVongQuay":
				return RequestGetTopVongQuay != null;
			case "RequestCreateCostume":
				return RequestCreateCostume != null;
			case "RequestTinhLuyenCostume":
				return RequestTinhLuyenCostume != null;
			case "RequestKhamNgocCostume":
				return RequestKhamNgocCostume != null;
			case "RequestGoNgocCostume":
				return RequestGoNgocCostume != null;
			case "RequestTakeOnCostume":
				return RequestTakeOnCostume != null;
			case "RequestTakeOffCostume":
				return RequestTakeOffCostume != null;
			case "RequestBanPhaoHoa":
				return RequestBanPhaoHoa != null;
			case "RequestLinhThuongPhaoHoaEvent":
				return RequestLinhThuongPhaoHoaEvent != null;
			case "RequestChuyenSinhDeTu":
				return RequestChuyenSinhDeTu != null;
			case "RequestGetListOtherUser":
				return RequestGetListOtherUser != null;
			case "RequestBatThanThu":
				return RequestBatThanThu != null;
			case "RequestNhanThuongDapNieu":
				return RequestNhanThuongDapNieu != null;
			case "RequestTruongThanhThanThu":
				return RequestTruongThanhThanThu != null;
			case "RequestNangPhamThanThu":
				return RequestNangPhamThanThu != null;
			case "RequestDoiThanThu":
				return RequestDoiThanThu != null;
			case "RequestThonPheThanThu":
				return RequestThonPheThanThu != null;
			case "RequestTruyenCongThanThu":
				return RequestTruyenCongThanThu != null;
			case "RequestSetBoPhapNhanVatBatQuai":
				return RequestSetBoPhapNhanVatBatQuai != null;
			case "RequestSetNoiCongNhanVatBatQuai":
				return RequestSetNoiCongNhanVatBatQuai != null;
			case "RequestSetTrangBiNhanVatBatQuai":
				return RequestSetTrangBiNhanVatBatQuai != null;
			case "RequestGetThanThuDao":
				return RequestGetThanThuDao != null;
			case "RequestAutoResolveThanThuDao":
				return RequestAutoResolveThanThuDao != null;
			case "RequestSetThanThu":
				return RequestSetThanThu != null;
			case "RequestGetGuiTietKiem":
				return RequestGetGuiTietKiem != null;
			case "RequestThamGiaGuiTietKiem":
				return RequestThamGiaGuiTietKiem != null;
			case "RequestGetThuong1MilUser":
				return RequestGetThuong1MilUser != null;
			case "RequestUseRuongThanBi":
				return RequestUseRuongThanBi != null;
			case "RequestThuHoachSonMon":
				return RequestThuHoachSonMon != null;
			case "RequestXayDungSonMon":
				return RequestXayDungSonMon != null;
			case "RequestTanCongSonMon":
				return RequestTanCongSonMon != null;
			case "RequestDoiHinhSonMon":
				return RequestDoiHinhSonMon != null;
			case "RequestDoThamSonMon":
				return RequestDoThamSonMon != null;
			case "RequestMuaDoThanBi":
				return RequestMuaDoThanBi != null;
			case "RequestGetTopSonMon":
				return RequestGetTopSonMon != null;
			case "RequestDungLuyenTrangBi":
				return RequestDungLuyenTrangBi != null;
			case "RequestTayLuyenTrangBi":
				return RequestTayLuyenTrangBi != null;
			case "RequestKhaiQuangTrangBi":
				return RequestKhaiQuangTrangBi != null;
			case "RequestConfirmTayLuyenTrangBi":
				return RequestConfirmTayLuyenTrangBi != null;
			case "RequestGetSonMonInfo":
				return RequestGetSonMonInfo != null;
			case "RequestGetLanhDiaInfo":
				return RequestGetLanhDiaInfo != null;
			case "RequestUnLockVoCong":
				return RequestUnLockVoCong != null;
			case "RequestQuayBacMayMan":
				return RequestQuayBacMayMan != null;
			case "RequestMoveLanhDia":
				return RequestMoveLanhDia != null;
			case "RequestQuayTuBaoBon":
				return RequestQuayTuBaoBon != null;
			case "RequestGetTopMoRuong":
				return RequestGetTopMoRuong != null;
			case "RequestQuayThienMaHaPhong":
				return RequestQuayThienMaHaPhong != null;
			case "RequestGetTayVucInfo":
				return RequestGetTayVucInfo != null;
			case "RequestMuaDoTayVuc":
				return RequestMuaDoTayVuc != null;
			case "RequestSummonNienThu":
				return RequestSummonNienThu != null;
			case "RequestDanhNienThu":
				return RequestDanhNienThu != null;
			case "RequestThamGiaNienThu":
				return RequestThamGiaNienThu != null;
			case "RequestNhanThuongNapHangNgay":
				return RequestNhanThuongNapHangNgay != null;
			case "RequestGetTopNienThu":
				return RequestGetTopNienThu != null;
			case "RequestNopLenhBaiNienThu":
				return RequestNopLenhBaiNienThu != null;
			case "RequestGetTopLanhDia":
				return RequestGetTopLanhDia != null;
			case "RequestSelectStartNgua":
				return RequestSelectStartNgua != null;
			case "RequestGetDiemMoRuong":
				return RequestGetDiemMoRuong != null;
			case "RequestUpdateDailyActivities":
				return RequestUpdateDailyActivities != null;
			case "RequestThuongDailyActivities":
				return RequestThuongDailyActivities != null;
			case "RequestThienMaQuaySlot":
				return RequestThienMaQuaySlot != null;
			case "RequestThienMaLenhCreate":
				return RequestThienMaLenhCreate != null;
			case "RequestThienMaUpgradeSlot":
				return RequestThienMaUpgradeSlot != null;
			case "RequestThienMaOpenSlot":
				return RequestThienMaOpenSlot != null;
			case "RequestThienMaEquip":
				return RequestThienMaEquip != null;
			case "RequestThienMaQuaySlotConfirm":
				return RequestThienMaQuaySlotConfirm != null;
			case "RequestSetTonHieu":
				return RequestSetTonHieu != null;
			case "RequestSetTrangBiHoangKim":
				return RequestSetTrangBiHoangKim != null;
			case "RequestGetTopDaiHoiVoLam":
				return RequestGetTopDaiHoiVoLam != null;
			case "RequestDanhAnDanhCaoThu":
				return RequestDanhAnDanhCaoThu != null;
			case "RequestStartBoiDuongTrangBi":
				return RequestStartBoiDuongTrangBi != null;
			case "RequestEndBoiDuongTrangBi":
				return RequestEndBoiDuongTrangBi != null;
			case "RequestRutQueTienNhan":
				return RequestRutQueTienNhan != null;
			case "RequestGetBaoKhoInfo":
				return RequestGetBaoKhoInfo != null;
			case "RequestCuopBaoKho":
				return RequestCuopBaoKho != null;
			case "RequestThuHoachBaoKho":
				return RequestThuHoachBaoKho != null;
			case "RequestGetListBaoKho":
				return RequestGetListBaoKho != null;
			case "RequestQuayCamCungBiBao":
				return RequestQuayCamCungBiBao != null;
			case "RequestNhanThuongCamCung":
				return RequestNhanThuongCamCung != null;
			case "RequestGetHoaVangInfo":
				return RequestGetHoaVangInfo != null;
			case "RequestHoaVang":
				return RequestHoaVang != null;
			case "RequestTestProudNet":
				return RequestTestProudNet != null;
			case "RequestTrangBiHuyenKhi":
				return RequestTrangBiHuyenKhi != null;
			case "RequestThangCapHuyenKhi":
				return RequestThangCapHuyenKhi != null;
			case "RequestChienHonChangeBuff":
				return RequestChienHonChangeBuff != null;
			case "RequestStartBoiDuongChienHon":
				return RequestStartBoiDuongChienHon != null;
			case "RequestEndBoiDuongChienHon":
				return RequestEndBoiDuongChienHon != null;
			case "RequestTrieuHoiChienHon":
				return RequestTrieuHoiChienHon != null;
			case "RequestUseTanHonTangLevelChienHon":
				return RequestUseTanHonTangLevelChienHon != null;
			case "RequestDotPhaChienHon":
				return RequestDotPhaChienHon != null;
			case "RequestCheTaoHuyenKhi":
				return RequestCheTaoHuyenKhi != null;
			case "RequestQuayDiemHoaVang":
				return RequestQuayDiemHoaVang != null;
			case "RequestUseTuiThan":
				return RequestUseTuiThan != null;
			case "RequestNhanThuongDailyKNB":
				return RequestNhanThuongDailyKNB != null;
			default:
				return false;
			}
		}
	}
}
