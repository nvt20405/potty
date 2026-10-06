using LitJson;
using Nettention.Proud;

namespace GameS2C
{
	public class Proxy : IJsonProxy
	{
		public bool NotifyNextLogonSuccess(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyNextLogonSuccess", data);
			return true;
		}

		public bool NotifyNextLogonSuccess(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyNextLogonSuccess(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetBattleResultSuccess(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetBattleResultSuccess", data);
			return true;
		}

		public bool NotifyGetBattleResultSuccess(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetBattleResultSuccess(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetListLKSuccess(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetListLKSuccess", data);
			return true;
		}

		public bool NotifyGetListLKSuccess(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetListLKSuccess(HostID.Server, rmiContext, data);
		}

		public bool NotifyAck(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyAck", data);
			return true;
		}

		public bool NotifyAck(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyAck(HostID.Server, rmiContext, data);
		}

		public bool NotifyTingTing(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyTingTing", data);
			return true;
		}

		public bool NotifyTingTing(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyTingTing(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetInfoSuccess(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetInfoSuccess", data);
			return true;
		}

		public bool NotifyGetInfoSuccess(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetInfoSuccess(HostID.Server, rmiContext, data);
		}

		public bool NotifyListPositionInMain(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyListPositionInMain", data);
			return true;
		}

		public bool NotifyListPositionInMain(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyListPositionInMain(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetLuanKiemInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetLuanKiemInfo", data);
			return true;
		}

		public bool NotifyGetLuanKiemInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetLuanKiemInfo(HostID.Server, rmiContext, data);
		}

		public bool NotifyCapNhatDiemLuanKiem(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyCapNhatDiemLuanKiem", data);
			return true;
		}

		public bool NotifyCapNhatDiemLuanKiem(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyCapNhatDiemLuanKiem(HostID.Server, rmiContext, data);
		}

		public bool NotifyNhanThuongLuanKiem(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyNhanThuongLuanKiem", data);
			return true;
		}

		public bool NotifyNhanThuongLuanKiem(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyNhanThuongLuanKiem(HostID.Server, rmiContext, data);
		}

		public bool NotifyDoiThuongLuanKiem(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDoiThuongLuanKiem", data);
			return true;
		}

		public bool NotifyDoiThuongLuanKiem(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDoiThuongLuanKiem(HostID.Server, rmiContext, data);
		}

		public bool NotifyDauLuanKiem(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDauLuanKiem", data);
			return true;
		}

		public bool NotifyDauLuanKiem(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDauLuanKiem(HostID.Server, rmiContext, data);
		}

		public bool NotifyDanhGiangHo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDanhGiangHo", data);
			return true;
		}

		public bool NotifyDanhGiangHo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDanhGiangHo(HostID.Server, rmiContext, data);
		}

		public bool NotifySetTrangBi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifySetTrangBi", data);
			return true;
		}

		public bool NotifySetTrangBi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifySetTrangBi(HostID.Server, rmiContext, data);
		}

		public bool NotifySetDoiHinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifySetDoiHinh", data);
			return true;
		}

		public bool NotifySetDoiHinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifySetDoiHinh(HostID.Server, rmiContext, data);
		}

		public bool NotifyChatMsg(HostID remote, RmiContext rmiContext, string msg)
		{
			CJsonTransport.Send("NotifyChatMsg", msg);
			return true;
		}

		public bool NotifyChatMsg(HostID[] remotes, RmiContext rmiContext, string msg)
		{
			return NotifyChatMsg(HostID.Server, rmiContext, msg);
		}

		public bool NotifySetHeroData(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifySetHeroData", data);
			return true;
		}

		public bool NotifySetHeroData(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifySetHeroData(HostID.Server, rmiContext, data);
		}

		public bool NotifySetVoCong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifySetVoCong", data);
			return true;
		}

		public bool NotifySetVoCong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifySetVoCong(HostID.Server, rmiContext, data);
		}

		public bool NotifySetTranHinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifySetTranHinh", data);
			return true;
		}

		public bool NotifySetTranHinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifySetTranHinh(HostID.Server, rmiContext, data);
		}

		public bool NotifyStartBoiDuong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyStartBoiDuong", data);
			return true;
		}

		public bool NotifyStartBoiDuong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyStartBoiDuong(HostID.Server, rmiContext, data);
		}

		public bool NotifyEndBoiDuong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyEndBoiDuong", data);
			return true;
		}

		public bool NotifyEndBoiDuong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyEndBoiDuong(HostID.Server, rmiContext, data);
		}

		public bool NotifyTuLuyenDeTu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyTuLuyenDeTu", data);
			return true;
		}

		public bool NotifyTuLuyenDeTu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyTuLuyenDeTu(HostID.Server, rmiContext, data);
		}

		public bool NotifyTrieuHoiDeTuBangHon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyTrieuHoiDeTuBangHon", data);
			return true;
		}

		public bool NotifyTrieuHoiDeTuBangHon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyTrieuHoiDeTuBangHon(HostID.Server, rmiContext, data);
		}

		public bool NotifyTruyenCong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyTruyenCong", data);
			return true;
		}

		public bool NotifyTruyenCong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyTruyenCong(HostID.Server, rmiContext, data);
		}

		public bool NotifyInTimeDanhDongNhan(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyInTimeDanhDongNhan", data);
			return true;
		}

		public bool NotifyInTimeDanhDongNhan(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyInTimeDanhDongNhan(HostID.Server, rmiContext, data);
		}

		public bool NotifyOutTimeDanhDongNhan(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyOutTimeDanhDongNhan", data);
			return true;
		}

		public bool NotifyOutTimeDanhDongNhan(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyOutTimeDanhDongNhan(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetDongNhanInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetDongNhanInfo", data);
			return true;
		}

		public bool NotifyGetDongNhanInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetDongNhanInfo(HostID.Server, rmiContext, data);
		}

		public bool NotifyDanhDongNhan(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDanhDongNhan", data);
			return true;
		}

		public bool NotifyDanhDongNhan(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDanhDongNhan(HostID.Server, rmiContext, data);
		}

		public bool NotifyCuongHoaTrangBi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyCuongHoaTrangBi", data);
			return true;
		}

		public bool NotifyCuongHoaTrangBi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyCuongHoaTrangBi(HostID.Server, rmiContext, data);
		}

		public bool NotifyBanTrangBi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyBanTrangBi", data);
			return true;
		}

		public bool NotifyBanTrangBi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyBanTrangBi(HostID.Server, rmiContext, data);
		}

		public bool NotifyGhepManhTrangBi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGhepManhTrangBi", data);
			return true;
		}

		public bool NotifyGhepManhTrangBi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGhepManhTrangBi(HostID.Server, rmiContext, data);
		}

		public bool NotifyGhepManhVoCong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGhepManhVoCong", data);
			return true;
		}

		public bool NotifyGhepManhVoCong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGhepManhVoCong(HostID.Server, rmiContext, data);
		}

		public bool NotifyTinhLuyenTrangBi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyTinhLuyenTrangBi", data);
			return true;
		}

		public bool NotifyTinhLuyenTrangBi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyTinhLuyenTrangBi(HostID.Server, rmiContext, data);
		}

		public bool NotifySetDoiHinhHoTro(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifySetDoiHinhHoTro", data);
			return true;
		}

		public bool NotifySetDoiHinhHoTro(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifySetDoiHinhHoTro(HostID.Server, rmiContext, data);
		}

		public bool NotifyOpenDoiHinhHoTro(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyOpenDoiHinhHoTro", data);
			return true;
		}

		public bool NotifyOpenDoiHinhHoTro(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyOpenDoiHinhHoTro(HostID.Server, rmiContext, data);
		}

		public bool NotifyThamNgoVoCong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyThamNgoVoCong", data);
			return true;
		}

		public bool NotifyThamNgoVoCong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyThamNgoVoCong(HostID.Server, rmiContext, data);
		}

		public bool NotifyTinhLuyenVoCong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyTinhLuyenVoCong", data);
			return true;
		}

		public bool NotifyTinhLuyenVoCong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyTinhLuyenVoCong(HostID.Server, rmiContext, data);
		}

		public bool NotifyLayDeTu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyLayDeTu", data);
			return true;
		}

		public bool NotifyLayDeTu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyLayDeTu(HostID.Server, rmiContext, data);
		}

		public bool NotifyBuyVatPham(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyBuyVatPham", data);
			return true;
		}

		public bool NotifyBuyVatPham(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyBuyVatPham(HostID.Server, rmiContext, data);
		}

		public bool NotifyCaoNhan(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyCaoNhan", data);
			return true;
		}

		public bool NotifyCaoNhan(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyCaoNhan(HostID.Server, rmiContext, data);
		}

		public bool NotifyThuongNhan(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyThuongNhan", data);
			return true;
		}

		public bool NotifyThuongNhan(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyThuongNhan(HostID.Server, rmiContext, data);
		}

		public bool NotifyBanDo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyBanDo", data);
			return true;
		}

		public bool NotifyBanDo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyBanDo(HostID.Server, rmiContext, data);
		}

		public bool NotifyBangHuu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyBangHuu", data);
			return true;
		}

		public bool NotifyBangHuu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyBangHuu(HostID.Server, rmiContext, data);
		}

		public bool NotifyTyThi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyTyThi", data);
			return true;
		}

		public bool NotifyTyThi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyTyThi(HostID.Server, rmiContext, data);
		}

		public bool NotifyBuyLeBao(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyBuyLeBao", data);
			return true;
		}

		public bool NotifyBuyLeBao(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyBuyLeBao(HostID.Server, rmiContext, data);
		}

		public bool NotifyHoiTheLuc(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyHoiTheLuc", data);
			return true;
		}

		public bool NotifyHoiTheLuc(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyHoiTheLuc(HostID.Server, rmiContext, data);
		}

		public bool NotifySelectStartDeTu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifySelectStartDeTu", data);
			return true;
		}

		public bool NotifySelectStartDeTu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifySelectStartDeTu(HostID.Server, rmiContext, data);
		}

		public bool NotifyKyNgoThamBai(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyKyNgoThamBai", data);
			return true;
		}

		public bool NotifyKyNgoThamBai(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyKyNgoThamBai(HostID.Server, rmiContext, data);
		}

		public bool NotifyHuyetChienInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyHuyetChienInfo", data);
			return true;
		}

		public bool NotifyHuyetChienInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyHuyetChienInfo(HostID.Server, rmiContext, data);
		}

		public bool NotifyStartHuyetChien(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyStartHuyetChien", data);
			return true;
		}

		public bool NotifyStartHuyetChien(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyStartHuyetChien(HostID.Server, rmiContext, data);
		}

		public bool NotifyHoiSinhHuyetChien(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyHoiSinhHuyetChien", data);
			return true;
		}

		public bool NotifyHoiSinhHuyetChien(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyHoiSinhHuyetChien(HostID.Server, rmiContext, data);
		}

		public bool NotifyHuyetChienTangThuocTinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyHuyetChienTangThuocTinh", data);
			return true;
		}

		public bool NotifyHuyetChienTangThuocTinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyHuyetChienTangThuocTinh(HostID.Server, rmiContext, data);
		}

		public bool NotifyDanhHuyetChien(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDanhHuyetChien", data);
			return true;
		}

		public bool NotifyDanhHuyetChien(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDanhHuyetChien(HostID.Server, rmiContext, data);
		}

		public bool NotifyNhanThuongHuyetChien(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyNhanThuongHuyetChien", data);
			return true;
		}

		public bool NotifyNhanThuongHuyetChien(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyNhanThuongHuyetChien(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetTopHuyetChien(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetTopHuyetChien", data);
			return true;
		}

		public bool NotifyGetTopHuyetChien(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetTopHuyetChien(HostID.Server, rmiContext, data);
		}

		public bool NotifyOpenHop(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyOpenHop", data);
			return true;
		}

		public bool NotifyOpenHop(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyOpenHop(HostID.Server, rmiContext, data);
		}

		public bool NotifyRequestSetDoiHinhAndTranHinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyRequestSetDoiHinhAndTranHinh", data);
			return true;
		}

		public bool NotifyRequestSetDoiHinhAndTranHinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyRequestSetDoiHinhAndTranHinh(HostID.Server, rmiContext, data);
		}

		public bool NotifyNhanThuongGiangHo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyNhanThuongGiangHo", data);
			return true;
		}

		public bool NotifyNhanThuongGiangHo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyNhanThuongGiangHo(HostID.Server, rmiContext, data);
		}

		public bool NotifyDanhNhanhGiangHo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDanhNhanhGiangHo", data);
			return true;
		}

		public bool NotifyDanhNhanhGiangHo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDanhNhanhGiangHo(HostID.Server, rmiContext, data);
		}

		public bool NotifyAnGaGiangHo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyAnGaGiangHo", data);
			return true;
		}

		public bool NotifyAnGaGiangHo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyAnGaGiangHo(HostID.Server, rmiContext, data);
		}

		public bool NotifyResetLuotGiangHo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyResetLuotGiangHo", data);
			return true;
		}

		public bool NotifyResetLuotGiangHo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyResetLuotGiangHo(HostID.Server, rmiContext, data);
		}

		public bool NotifyDanhDanhSon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDanhDanhSon", data);
			return true;
		}

		public bool NotifyDanhDanhSon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDanhDanhSon(HostID.Server, rmiContext, data);
		}

		public bool NotifyMoThuongDanhSon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyMoThuongDanhSon", data);
			return true;
		}

		public bool NotifyMoThuongDanhSon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyMoThuongDanhSon(HostID.Server, rmiContext, data);
		}

		public bool NotifyMoHetDanhSon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyMoHetDanhSon", data);
			return true;
		}

		public bool NotifyMoHetDanhSon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyMoHetDanhSon(HostID.Server, rmiContext, data);
		}

		public bool NotifyVuotAiDanhSon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyVuotAiDanhSon", data);
			return true;
		}

		public bool NotifyVuotAiDanhSon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyVuotAiDanhSon(HostID.Server, rmiContext, data);
		}

		public bool NotifyChonDongDoiDanhSon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyChonDongDoiDanhSon", data);
			return true;
		}

		public bool NotifyChonDongDoiDanhSon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyChonDongDoiDanhSon(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetDongDoiDanhSon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetDongDoiDanhSon", data);
			return true;
		}

		public bool NotifyGetDongDoiDanhSon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetDongDoiDanhSon(HostID.Server, rmiContext, data);
		}

		public bool NotifyHuaNguyen(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyHuaNguyen", data);
			return true;
		}

		public bool NotifyHuaNguyen(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyHuaNguyen(HostID.Server, rmiContext, data);
		}

		public bool NotifyLenCapNhanThuong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyLenCapNhanThuong", data);
			return true;
		}

		public bool NotifyLenCapNhanThuong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyLenCapNhanThuong(HostID.Server, rmiContext, data);
		}

		public bool NotifyThanTai(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyThanTai", data);
			return true;
		}

		public bool NotifyThanTai(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyThanTai(HostID.Server, rmiContext, data);
		}

		public bool NotifyXocDiaInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyXocDiaInfo", data);
			return true;
		}

		public bool NotifyXocDiaInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyXocDiaInfo(HostID.Server, rmiContext, data);
		}

		public bool NotifyChoiXocDia(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyChoiXocDia", data);
			return true;
		}

		public bool NotifyChoiXocDia(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyChoiXocDia(HostID.Server, rmiContext, data);
		}

		public bool NotifyCuuVienTieuPhong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyCuuVienTieuPhong", data);
			return true;
		}

		public bool NotifyCuuVienTieuPhong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyCuuVienTieuPhong(HostID.Server, rmiContext, data);
		}

		public bool NotifyNhanRuongThachSanh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyNhanRuongThachSanh", data);
			return true;
		}

		public bool NotifyNhanRuongThachSanh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyNhanRuongThachSanh(HostID.Server, rmiContext, data);
		}

		public bool NotifyPaymentConfirm(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyPaymentConfirm", data);
			return true;
		}

		public bool NotifyPaymentConfirm(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyPaymentConfirm(HostID.Server, rmiContext, data);
		}

		public bool NotifyDangNhapNhanThuong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDangNhapNhanThuong", data);
			return true;
		}

		public bool NotifyDangNhapNhanThuong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDangNhapNhanThuong(HostID.Server, rmiContext, data);
		}

		public bool NotifyUongRuouTieuPhong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyUongRuouTieuPhong", data);
			return true;
		}

		public bool NotifyUongRuouTieuPhong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyUongRuouTieuPhong(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetDoiRuouInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetDoiRuouInfo", data);
			return true;
		}

		public bool NotifyGetDoiRuouInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetDoiRuouInfo(HostID.Server, rmiContext, data);
		}

		public bool NotifyDoiRuou(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDoiRuou", data);
			return true;
		}

		public bool NotifyDoiRuou(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDoiRuou(HostID.Server, rmiContext, data);
		}

		public bool NotifyBatCoc(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyBatCoc", data);
			return true;
		}

		public bool NotifyBatCoc(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyBatCoc(HostID.Server, rmiContext, data);
		}

		public bool NotifyChuocThan(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyChuocThan", data);
			return true;
		}

		public bool NotifyChuocThan(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyChuocThan(HostID.Server, rmiContext, data);
		}

		public bool NotifySearchBanBe(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifySearchBanBe", data);
			return true;
		}

		public bool NotifySearchBanBe(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifySearchBanBe(HostID.Server, rmiContext, data);
		}

		public bool NotifyNhanDuocThachDau(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyNhanDuocThachDau", data);
			return true;
		}

		public bool NotifyNhanDuocThachDau(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyNhanDuocThachDau(HostID.Server, rmiContext, data);
		}

		public bool NotifyKetQuaThachDau(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyKetQuaThachDau", data);
			return true;
		}

		public bool NotifyKetQuaThachDau(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyKetQuaThachDau(HostID.Server, rmiContext, data);
		}

		public bool NotifyDuocAddBanBe(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDuocAddBanBe", data);
			return true;
		}

		public bool NotifyDuocAddBanBe(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDuocAddBanBe(HostID.Server, rmiContext, data);
		}

		public bool NotifyAddBanBe(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyAddBanBe", data);
			return true;
		}

		public bool NotifyAddBanBe(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyAddBanBe(HostID.Server, rmiContext, data);
		}

		public bool NotifyAcceptBanBe(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyAcceptBanBe", data);
			return true;
		}

		public bool NotifyAcceptBanBe(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyAcceptBanBe(HostID.Server, rmiContext, data);
		}

		public bool NotifyDeleteBanBe(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDeleteBanBe", data);
			return true;
		}

		public bool NotifyDeleteBanBe(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDeleteBanBe(HostID.Server, rmiContext, data);
		}

		public bool NotifyDangNhapTrungTK(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDangNhapTrungTK", data);
			return true;
		}

		public bool NotifyDangNhapTrungTK(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDangNhapTrungTK(HostID.Server, rmiContext, data);
		}

		public bool NotifyBanBeCuuThuInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyBanBeCuuThuInfo", data);
			return true;
		}

		public bool NotifyBanBeCuuThuInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyBanBeCuuThuInfo(HostID.Server, rmiContext, data);
		}

		public bool NotifyChatInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyChatInfo", data);
			return true;
		}

		public bool NotifyChatInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyChatInfo(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetChatAll(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetChatAll", data);
			return true;
		}

		public bool NotifyGetChatAll(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetChatAll(HostID.Server, rmiContext, data);
		}

		public bool NotifyDatTenMonPhai(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDatTenMonPhai", data);
			return true;
		}

		public bool NotifyDatTenMonPhai(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDatTenMonPhai(HostID.Server, rmiContext, data);
		}

		public bool NotifyULinhInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyULinhInfo", data);
			return true;
		}

		public bool NotifyULinhInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyULinhInfo(HostID.Server, rmiContext, data);
		}

		public bool NotifyDoiItemULinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDoiItemULinh", data);
			return true;
		}

		public bool NotifyDoiItemULinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDoiItemULinh(HostID.Server, rmiContext, data);
		}

		public bool NotifyKnbRefreshULinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyKnbRefreshULinh", data);
			return true;
		}

		public bool NotifyKnbRefreshULinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyKnbRefreshULinh(HostID.Server, rmiContext, data);
		}

		public bool NotifyDoiThuongULinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDoiThuongULinh", data);
			return true;
		}

		public bool NotifyDoiThuongULinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDoiThuongULinh(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetTopULinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetTopULinh", data);
			return true;
		}

		public bool NotifyGetTopULinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetTopULinh(HostID.Server, rmiContext, data);
		}

		public bool NotifyKichHoatGiftCode(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyKichHoatGiftCode", data);
			return true;
		}

		public bool NotifyKichHoatGiftCode(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyKichHoatGiftCode(HostID.Server, rmiContext, data);
		}

		public bool NotifyUseCustomItem(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyUseCustomItem", data);
			return true;
		}

		public bool NotifyUseCustomItem(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyUseCustomItem(HostID.Server, rmiContext, data);
		}

		public bool NotifyBuyVatPhamAndUse(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyBuyVatPhamAndUse", data);
			return true;
		}

		public bool NotifyBuyVatPhamAndUse(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyBuyVatPhamAndUse(HostID.Server, rmiContext, data);
		}

		public bool NotifyHighlight(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyHighlight", data);
			return true;
		}

		public bool NotifyHighlight(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyHighlight(HostID.Server, rmiContext, data);
		}

		public bool NotifyBaoTriServer(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyBaoTriServer", data);
			return true;
		}

		public bool NotifyBaoTriServer(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyBaoTriServer(HostID.Server, rmiContext, data);
		}

		public bool NotifyUseMailPhanThuong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyUseMailPhanThuong", data);
			return true;
		}

		public bool NotifyUseMailPhanThuong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyUseMailPhanThuong(HostID.Server, rmiContext, data);
		}

		public bool NotifyReadAllMail(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyReadAllMail", data);
			return true;
		}

		public bool NotifyReadAllMail(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyReadAllMail(HostID.Server, rmiContext, data);
		}

		public bool NotifyRefreshMail(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyRefreshMail", data);
			return true;
		}

		public bool NotifyRefreshMail(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyRefreshMail(HostID.Server, rmiContext, data);
		}

		public bool NotifyUseRuongThan(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyUseRuongThan", data);
			return true;
		}

		public bool NotifyUseRuongThan(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyUseRuongThan(HostID.Server, rmiContext, data);
		}

		public bool NotifyBatTho(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyBatTho", data);
			return true;
		}

		public bool NotifyBatTho(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyBatTho(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetFriendsDoiHinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetFriendsDoiHinh", data);
			return true;
		}

		public bool NotifyGetFriendsDoiHinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetFriendsDoiHinh(HostID.Server, rmiContext, data);
		}

		public bool NotifyXemThongTinMonPhai(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyXemThongTinMonPhai", data);
			return true;
		}

		public bool NotifyXemThongTinMonPhai(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyXemThongTinMonPhai(HostID.Server, rmiContext, data);
		}

		public bool NotifySendMail(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifySendMail", data);
			return true;
		}

		public bool NotifySendMail(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifySendMail(HostID.Server, rmiContext, data);
		}

		public bool NotifyDangNhapQuayXoSo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDangNhapQuayXoSo", data);
			return true;
		}

		public bool NotifyDangNhapQuayXoSo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDangNhapQuayXoSo(HostID.Server, rmiContext, data);
		}

		public bool NotifyMatDongBoDuLieu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyMatDongBoDuLieu", data);
			return true;
		}

		public bool NotifyMatDongBoDuLieu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyMatDongBoDuLieu(HostID.Server, rmiContext, data);
		}

		public bool NotifyTangTheLuc(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyTangTheLuc", data);
			return true;
		}

		public bool NotifyTangTheLuc(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyTangTheLuc(HostID.Server, rmiContext, data);
		}

		public bool NotifyDuocTangTheLuc(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDuocTangTheLuc", data);
			return true;
		}

		public bool NotifyDuocTangTheLuc(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDuocTangTheLuc(HostID.Server, rmiContext, data);
		}

		public bool NotifyDoiDo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDoiDo", data);
			return true;
		}

		public bool NotifyDoiDo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDoiDo(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetDuaTopLevelInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetDuaTopLevelInfo", data);
			return true;
		}

		public bool NotifyGetDuaTopLevelInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetDuaTopLevelInfo(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetDuaTopLuanKiemInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetDuaTopLuanKiemInfo", data);
			return true;
		}

		public bool NotifyGetDuaTopLuanKiemInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetDuaTopLuanKiemInfo(HostID.Server, rmiContext, data);
		}

		public bool NotifyListOnlineInMain(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyListOnlineInMain", data);
			return true;
		}

		public bool NotifyListOnlineInMain(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyListOnlineInMain(HostID.Server, rmiContext, data);
		}

		public bool NotifyCT2PlayerAppear(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyCT2PlayerAppear", data);
			return true;
		}

		public bool NotifyCT2PlayerAppear(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyCT2PlayerAppear(HostID.Server, rmiContext, data);
		}

		public bool NotifyThamGiaCT2(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyThamGiaCT2", data);
			return true;
		}

		public bool NotifyThamGiaCT2(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyThamGiaCT2(HostID.Server, rmiContext, data);
		}

		public bool NotifyListCT2(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyListCT2", data);
			return true;
		}

		public bool NotifyListCT2(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyListCT2(HostID.Server, rmiContext, data);
		}

		public bool NotifyLapLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyLapLienMinh", data);
			return true;
		}

		public bool NotifyLapLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyLapLienMinh(HostID.Server, rmiContext, data);
		}

		public bool NotifyCT2PlayerPos(HostID remote, RmiContext rmiContext, int GID, int SID, float pos_x, float pos_z, float vel_x, float vel_z)
		{
			CJsonTransport.Send("NotifyCT2PlayerPos", JsonMapper.ToJson(new object[6] { GID, SID, pos_x, pos_z, vel_x, vel_z }));
			return true;
		}

		public bool NotifyCT2PlayerPos(HostID[] remotes, RmiContext rmiContext, int GID, int SID, float pos_x, float pos_z, float vel_x, float vel_z)
		{
			return NotifyCT2PlayerPos(HostID.Server, rmiContext, GID, SID, pos_x, pos_z, vel_x, vel_z);
		}

		public bool NotifyCT2BattleResult(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyCT2BattleResult", data);
			return true;
		}

		public bool NotifyCT2BattleResult(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyCT2BattleResult(HostID.Server, rmiContext, data);
		}

		public bool NotifyGiaNhapLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGiaNhapLienMinh", data);
			return true;
		}

		public bool NotifyGiaNhapLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGiaNhapLienMinh(HostID.Server, rmiContext, data);
		}

		public bool NotifyChapNhanGiaNhapLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyChapNhanGiaNhapLienMinh", data);
			return true;
		}

		public bool NotifyChapNhanGiaNhapLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyChapNhanGiaNhapLienMinh(HostID.Server, rmiContext, data);
		}

		public bool NotifyThoatLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyThoatLienMinh", data);
			return true;
		}

		public bool NotifyThoatLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyThoatLienMinh(HostID.Server, rmiContext, data);
		}

		public bool NotifyFinishNhiemVuLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyFinishNhiemVuLienMinh", data);
			return true;
		}

		public bool NotifyFinishNhiemVuLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyFinishNhiemVuLienMinh(HostID.Server, rmiContext, data);
		}

		public bool NotifyResetNhiemVuLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyResetNhiemVuLienMinh", data);
			return true;
		}

		public bool NotifyResetNhiemVuLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyResetNhiemVuLienMinh(HostID.Server, rmiContext, data);
		}

		public bool NotifyRutGiaNhapLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyRutGiaNhapLienMinh", data);
			return true;
		}

		public bool NotifyRutGiaNhapLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyRutGiaNhapLienMinh(HostID.Server, rmiContext, data);
		}

		public bool NotifyDangHuongLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDangHuongLienMinh", data);
			return true;
		}

		public bool NotifyDangHuongLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDangHuongLienMinh(HostID.Server, rmiContext, data);
		}

		public bool NotifyDoiThuongLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDoiThuongLienMinh", data);
			return true;
		}

		public bool NotifyDoiThuongLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDoiThuongLienMinh(HostID.Server, rmiContext, data);
		}

		public bool NotifyDoiMinhChu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDoiMinhChu", data);
			return true;
		}

		public bool NotifyDoiMinhChu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDoiMinhChu(HostID.Server, rmiContext, data);
		}

		public bool NotifyDoiPhoMinhChu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDoiPhoMinhChu", data);
			return true;
		}

		public bool NotifyDoiPhoMinhChu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDoiPhoMinhChu(HostID.Server, rmiContext, data);
		}

		public bool NotifyNangCapCongTrinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyNangCapCongTrinh", data);
			return true;
		}

		public bool NotifyNangCapCongTrinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyNangCapCongTrinh(HostID.Server, rmiContext, data);
		}

		public bool NotifyCT2Teleport(HostID remote, RmiContext rmiContext, int GID, int SID, float pos_x, float pos_z)
		{
			CJsonTransport.Send("NotifyCT2Teleport", JsonMapper.ToJson(new object[4] { GID, SID, pos_x, pos_z }));
			return true;
		}

		public bool NotifyCT2Teleport(HostID[] remotes, RmiContext rmiContext, int GID, int SID, float pos_x, float pos_z)
		{
			return NotifyCT2Teleport(HostID.Server, rmiContext, GID, SID, pos_x, pos_z);
		}

		public bool NotifyKhamNgoc(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyKhamNgoc", data);
			return true;
		}

		public bool NotifyKhamNgoc(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyKhamNgoc(HostID.Server, rmiContext, data);
		}

		public bool NotifyGoNgoc(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGoNgoc", data);
			return true;
		}

		public bool NotifyGoNgoc(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGoNgoc(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetTopLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetTopLienMinh", data);
			return true;
		}

		public bool NotifyGetTopLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetTopLienMinh(HostID.Server, rmiContext, data);
		}

		public bool NotifySearchLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifySearchLienMinh", data);
			return true;
		}

		public bool NotifySearchLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifySearchLienMinh(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetThongTinLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetThongTinLienMinh", data);
			return true;
		}

		public bool NotifyGetThongTinLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetThongTinLienMinh(HostID.Server, rmiContext, data);
		}

		public bool NotifyCT2NPCMove(HostID remote, RmiContext rmiContext, int GID, int SID, float pos_x, float pos_z, float vel_x, float vel_z, byte state)
		{
			CJsonTransport.Send("NotifyCT2NPCMove", JsonMapper.ToJson(new object[7] { GID, SID, pos_x, pos_z, vel_x, vel_z, state }));
			return true;
		}

		public bool NotifyCT2NPCMove(HostID[] remotes, RmiContext rmiContext, int GID, int SID, float pos_x, float pos_z, float vel_x, float vel_z, byte state)
		{
			return NotifyCT2NPCMove(HostID.Server, rmiContext, GID, SID, pos_x, pos_z, vel_x, vel_z, state);
		}

		public bool NotifyCT2NPCIdle(HostID remote, RmiContext rmiContext, int GID, int SID, float pos_x, float pos_z)
		{
			CJsonTransport.Send("NotifyCT2NPCIdle", JsonMapper.ToJson(new object[4] { GID, SID, pos_x, pos_z }));
			return true;
		}

		public bool NotifyCT2NPCIdle(HostID[] remotes, RmiContext rmiContext, int GID, int SID, float pos_x, float pos_z)
		{
			return NotifyCT2NPCIdle(HostID.Server, rmiContext, GID, SID, pos_x, pos_z);
		}

		public bool NotifyCT2NPCAttack(HostID remote, RmiContext rmiContext, int GID, int SID, float pos_x, float pos_z)
		{
			CJsonTransport.Send("NotifyCT2NPCAttack", JsonMapper.ToJson(new object[4] { GID, SID, pos_x, pos_z }));
			return true;
		}

		public bool NotifyCT2NPCAttack(HostID[] remotes, RmiContext rmiContext, int GID, int SID, float pos_x, float pos_z)
		{
			return NotifyCT2NPCAttack(HostID.Server, rmiContext, GID, SID, pos_x, pos_z);
		}

		public bool NotifyCT2HPPercent(HostID remote, RmiContext rmiContext, int GID, int SID, float tyleHp, int sinhMenhValue)
		{
			CJsonTransport.Send("NotifyCT2HPPercent", JsonMapper.ToJson(new object[4] { GID, SID, tyleHp, sinhMenhValue }));
			return true;
		}

		public bool NotifyCT2HPPercent(HostID[] remotes, RmiContext rmiContext, int GID, int SID, float tyleHp, int sinhMenhValue)
		{
			return NotifyCT2HPPercent(HostID.Server, rmiContext, GID, SID, tyleHp, sinhMenhValue);
		}

		public bool NotifyNhanThuongDiHoaCungAll(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyNhanThuongDiHoaCungAll", data);
			return true;
		}

		public bool NotifyNhanThuongDiHoaCungAll(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyNhanThuongDiHoaCungAll(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetDiHoaCungInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetDiHoaCungInfo", data);
			return true;
		}

		public bool NotifyGetDiHoaCungInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetDiHoaCungInfo(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetTopDiHoaCung(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetTopDiHoaCung", data);
			return true;
		}

		public bool NotifyGetTopDiHoaCung(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetTopDiHoaCung(HostID.Server, rmiContext, data);
		}

		public bool NotifyDuoiKhoiLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDuoiKhoiLienMinh", data);
			return true;
		}

		public bool NotifyDuoiKhoiLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDuoiKhoiLienMinh(HostID.Server, rmiContext, data);
		}

		public bool NotifyCT2RuneXuatHien(HostID remote, RmiContext rmiContext, int runeType, int runeIndex, float pos_x, float pos_z)
		{
			CJsonTransport.Send("NotifyCT2RuneXuatHien", JsonMapper.ToJson(new object[4] { runeType, runeIndex, pos_x, pos_z }));
			return true;
		}

		public bool NotifyCT2RuneXuatHien(HostID[] remotes, RmiContext rmiContext, int runeType, int runeIndex, float pos_x, float pos_z)
		{
			return NotifyCT2RuneXuatHien(HostID.Server, rmiContext, runeType, runeIndex, pos_x, pos_z);
		}

		public bool NotifyCT2GotRune(HostID remote, RmiContext rmiContext, int GID, int SID, int runeType, int runeIndex, float tyleHp, string teamInfo)
		{
			CJsonTransport.Send("NotifyCT2GotRune", JsonMapper.ToJson(new object[6] { GID, SID, runeType, runeIndex, tyleHp, teamInfo }));
			return true;
		}

		public bool NotifyCT2GotRune(HostID[] remotes, RmiContext rmiContext, int GID, int SID, int runeType, int runeIndex, float tyleHp, string teamInfo)
		{
			return NotifyCT2GotRune(HostID.Server, rmiContext, GID, SID, runeType, runeIndex, tyleHp, teamInfo);
		}

		public bool NotifyCT2KetThuc(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyCT2KetThuc", data);
			return true;
		}

		public bool NotifyCT2KetThuc(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyCT2KetThuc(HostID.Server, rmiContext, data);
		}

		public bool NotifyCT2PlayerState(HostID remote, RmiContext rmiContext, int GID, int SID, byte state)
		{
			CJsonTransport.Send("NotifyCT2PlayerState", JsonMapper.ToJson(new object[3] { GID, SID, state }));
			return true;
		}

		public bool NotifyCT2PlayerState(HostID[] remotes, RmiContext rmiContext, int GID, int SID, byte state)
		{
			return NotifyCT2PlayerState(HostID.Server, rmiContext, GID, SID, state);
		}

		public bool NotifyCT2BangXepHangTuanNay(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyCT2BangXepHangTuanNay", data);
			return true;
		}

		public bool NotifyCT2BangXepHangTuanNay(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyCT2BangXepHangTuanNay(HostID.Server, rmiContext, data);
		}

		public bool NotifyCT2BangXepHangTuanTruoc(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyCT2BangXepHangTuanTruoc", data);
			return true;
		}

		public bool NotifyCT2BangXepHangTuanTruoc(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyCT2BangXepHangTuanTruoc(HostID.Server, rmiContext, data);
		}

		public bool NotifyCT2PhanThuongBXHTuan(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyCT2PhanThuongBXHTuan", data);
			return true;
		}

		public bool NotifyCT2PhanThuongBXHTuan(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyCT2PhanThuongBXHTuan(HostID.Server, rmiContext, data);
		}

		public bool NotifyThamGiaLuaTraiLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyThamGiaLuaTraiLienMinh", data);
			return true;
		}

		public bool NotifyThamGiaLuaTraiLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyThamGiaLuaTraiLienMinh(HostID.Server, rmiContext, data);
		}

		public bool NotifyLienMinhThoiLua(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyLienMinhThoiLua", data);
			return true;
		}

		public bool NotifyLienMinhThoiLua(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyLienMinhThoiLua(HostID.Server, rmiContext, data);
		}

		public bool NotifyRoiKhoiLuaTraiLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyRoiKhoiLuaTraiLienMinh", data);
			return true;
		}

		public bool NotifyRoiKhoiLuaTraiLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyRoiKhoiLuaTraiLienMinh(HostID.Server, rmiContext, data);
		}

		public bool NotifySetGioLuaTraiLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifySetGioLuaTraiLienMinh", data);
			return true;
		}

		public bool NotifySetGioLuaTraiLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifySetGioLuaTraiLienMinh(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetExpLuaTrai(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetExpLuaTrai", data);
			return true;
		}

		public bool NotifyGetExpLuaTrai(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetExpLuaTrai(HostID.Server, rmiContext, data);
		}

		public bool NotifySuaThongBaoLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifySuaThongBaoLienMinh", data);
			return true;
		}

		public bool NotifySuaThongBaoLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifySuaThongBaoLienMinh(HostID.Server, rmiContext, data);
		}

		public bool NotifySendChatLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifySendChatLienMinh", data);
			return true;
		}

		public bool NotifySendChatLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifySendChatLienMinh(HostID.Server, rmiContext, data);
		}

		public bool NotifyChatLienMinhInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyChatLienMinhInfo", data);
			return true;
		}

		public bool NotifyChatLienMinhInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyChatLienMinhInfo(HostID.Server, rmiContext, data);
		}

		public bool NotifyPhanRaTrangBi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyPhanRaTrangBi", data);
			return true;
		}

		public bool NotifyPhanRaTrangBi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyPhanRaTrangBi(HostID.Server, rmiContext, data);
		}

		public bool NotifyCT2BXH(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyCT2BXH", data);
			return true;
		}

		public bool NotifyCT2BXH(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyCT2BXH(HostID.Server, rmiContext, data);
		}

		public bool NotifyUpdateLienMinhData(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyUpdateLienMinhData", data);
			return true;
		}

		public bool NotifyUpdateLienMinhData(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyUpdateLienMinhData(HostID.Server, rmiContext, data);
		}

		public bool NotifyLapNguyenKhi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyLapNguyenKhi", data);
			return true;
		}

		public bool NotifyLapNguyenKhi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyLapNguyenKhi(HostID.Server, rmiContext, data);
		}

		public bool NotifyThaoNguyenKhi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyThaoNguyenKhi", data);
			return true;
		}

		public bool NotifyThaoNguyenKhi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyThaoNguyenKhi(HostID.Server, rmiContext, data);
		}

		public bool NotifyNangCapNguyenKhi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyNangCapNguyenKhi", data);
			return true;
		}

		public bool NotifyNangCapNguyenKhi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyNangCapNguyenKhi(HostID.Server, rmiContext, data);
		}

		public bool NotifyMuaNguyenKhi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyMuaNguyenKhi", data);
			return true;
		}

		public bool NotifyMuaNguyenKhi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyMuaNguyenKhi(HostID.Server, rmiContext, data);
		}

		public bool NotifyBangChienGetInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyBangChienGetInfo", data);
			return true;
		}

		public bool NotifyBangChienGetInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyBangChienGetInfo(HostID.Server, rmiContext, data);
		}

		public bool NotifyBangChienVaoThanh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyBangChienVaoThanh", data);
			return true;
		}

		public bool NotifyBangChienVaoThanh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyBangChienVaoThanh(HostID.Server, rmiContext, data);
		}

		public bool NotifyBangChienRoiThanh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyBangChienRoiThanh", data);
			return true;
		}

		public bool NotifyBangChienRoiThanh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyBangChienRoiThanh(HostID.Server, rmiContext, data);
		}

		public bool NotifyBangChienDenCongThanh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyBangChienDenCongThanh", data);
			return true;
		}

		public bool NotifyBangChienDenCongThanh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyBangChienDenCongThanh(HostID.Server, rmiContext, data);
		}

		public bool NotifyBangChienCongThanh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyBangChienCongThanh", data);
			return true;
		}

		public bool NotifyBangChienCongThanh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyBangChienCongThanh(HostID.Server, rmiContext, data);
		}

		public bool NotifyChonHatGiong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyChonHatGiong", data);
			return true;
		}

		public bool NotifyChonHatGiong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyChonHatGiong(HostID.Server, rmiContext, data);
		}

		public bool NotifyLayHatGiong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyLayHatGiong", data);
			return true;
		}

		public bool NotifyLayHatGiong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyLayHatGiong(HostID.Server, rmiContext, data);
		}

		public bool NotifyTrongCay(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyTrongCay", data);
			return true;
		}

		public bool NotifyTrongCay(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyTrongCay(HostID.Server, rmiContext, data);
		}

		public bool NotifyThuHoach(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyThuHoach", data);
			return true;
		}

		public bool NotifyThuHoach(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyThuHoach(HostID.Server, rmiContext, data);
		}

		public bool NotifyAnTrom(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyAnTrom", data);
			return true;
		}

		public bool NotifyAnTrom(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyAnTrom(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetAnTromList(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetAnTromList", data);
			return true;
		}

		public bool NotifyGetAnTromList(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetAnTromList(HostID.Server, rmiContext, data);
		}

		public bool NotifyBangChienListUserMove(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyBangChienListUserMove", data);
			return true;
		}

		public bool NotifyBangChienListUserMove(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyBangChienListUserMove(HostID.Server, rmiContext, data);
		}

		public bool NotifyBangChienGetPhanThuongThuThanh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyBangChienGetPhanThuongThuThanh", data);
			return true;
		}

		public bool NotifyBangChienGetPhanThuongThuThanh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyBangChienGetPhanThuongThuThanh(HostID.Server, rmiContext, data);
		}

		public bool NotifyBangChienKetThuc(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyBangChienKetThuc", data);
			return true;
		}

		public bool NotifyBangChienKetThuc(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyBangChienKetThuc(HostID.Server, rmiContext, data);
		}

		public bool NotifySendChatLienSrv(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifySendChatLienSrv", data);
			return true;
		}

		public bool NotifySendChatLienSrv(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifySendChatLienSrv(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetChatLienSrv(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetChatLienSrv", data);
			return true;
		}

		public bool NotifyGetChatLienSrv(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetChatLienSrv(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetLinhDuocInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetLinhDuocInfo", data);
			return true;
		}

		public bool NotifyGetLinhDuocInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetLinhDuocInfo(HostID.Server, rmiContext, data);
		}

		public bool NotifyBanhChungOthers(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyBanhChungOthers", data);
			return true;
		}

		public bool NotifyBanhChungOthers(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyBanhChungOthers(HostID.Server, rmiContext, data);
		}

		public bool NotifyBanhChungGetInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyBanhChungGetInfo", data);
			return true;
		}

		public bool NotifyBanhChungGetInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyBanhChungGetInfo(HostID.Server, rmiContext, data);
		}

		public bool NotifyBanhChungNauBanh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyBanhChungNauBanh", data);
			return true;
		}

		public bool NotifyBanhChungNauBanh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyBanhChungNauBanh(HostID.Server, rmiContext, data);
		}

		public bool NotifyBanhChungNhatNguyenLieu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyBanhChungNhatNguyenLieu", data);
			return true;
		}

		public bool NotifyBanhChungNhatNguyenLieu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyBanhChungNhatNguyenLieu(HostID.Server, rmiContext, data);
		}

		public bool NotifyBanhChungBXH(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyBanhChungBXH", data);
			return true;
		}

		public bool NotifyBanhChungBXH(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyBanhChungBXH(HostID.Server, rmiContext, data);
		}

		public bool NotifyBanhChungGetPhanThuong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyBanhChungGetPhanThuong", data);
			return true;
		}

		public bool NotifyBanhChungGetPhanThuong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyBanhChungGetPhanThuong(HostID.Server, rmiContext, data);
		}

		public bool NotifyDangNhapNhanThuongTet(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDangNhapNhanThuongTet", data);
			return true;
		}

		public bool NotifyDangNhapNhanThuongTet(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDangNhapNhanThuongTet(HostID.Server, rmiContext, data);
		}

		public bool NotifyThaoNgua(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyThaoNgua", data);
			return true;
		}

		public bool NotifyThaoNgua(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyThaoNgua(HostID.Server, rmiContext, data);
		}

		public bool NotifyDungNgua(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDungNgua", data);
			return true;
		}

		public bool NotifyDungNgua(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDungNgua(HostID.Server, rmiContext, data);
		}

		public bool NotifyActiveNgua(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyActiveNgua", data);
			return true;
		}

		public bool NotifyActiveNgua(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyActiveNgua(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetGamerLinhDuoc(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetGamerLinhDuoc", data);
			return true;
		}

		public bool NotifyGetGamerLinhDuoc(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetGamerLinhDuoc(HostID.Server, rmiContext, data);
		}

		public bool NotifyCT2HPTeam(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyCT2HPTeam", data);
			return true;
		}

		public bool NotifyCT2HPTeam(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyCT2HPTeam(HostID.Server, rmiContext, data);
		}

		public bool NotifyCuongHoaBatQuaiTran(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyCuongHoaBatQuaiTran", data);
			return true;
		}

		public bool NotifyCuongHoaBatQuaiTran(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyCuongHoaBatQuaiTran(HostID.Server, rmiContext, data);
		}

		public bool NotifySetSoDoBatQuaiTran(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifySetSoDoBatQuaiTran", data);
			return true;
		}

		public bool NotifySetSoDoBatQuaiTran(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifySetSoDoBatQuaiTran(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetLeagueData(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetLeagueData", data);
			return true;
		}

		public bool NotifyGetLeagueData(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetLeagueData(HostID.Server, rmiContext, data);
		}

		public bool NotifySetDoiHinhThienCangTran(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifySetDoiHinhThienCangTran", data);
			return true;
		}

		public bool NotifySetDoiHinhThienCangTran(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifySetDoiHinhThienCangTran(HostID.Server, rmiContext, data);
		}

		public bool NotifyOpenDoiHinhThienCangTran(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyOpenDoiHinhThienCangTran", data);
			return true;
		}

		public bool NotifyOpenDoiHinhThienCangTran(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyOpenDoiHinhThienCangTran(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetSieuCupData(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetSieuCupData", data);
			return true;
		}

		public bool NotifyGetSieuCupData(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetSieuCupData(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetSieuCupBattle(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetSieuCupBattle", data);
			return true;
		}

		public bool NotifyGetSieuCupBattle(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetSieuCupBattle(HostID.Server, rmiContext, data);
		}

		public bool NotifySieuCupDatCuoc(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifySieuCupDatCuoc", data);
			return true;
		}

		public bool NotifySieuCupDatCuoc(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifySieuCupDatCuoc(HostID.Server, rmiContext, data);
		}

		public bool NotifySubmitDoiHinhLeague(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifySubmitDoiHinhLeague", data);
			return true;
		}

		public bool NotifySubmitDoiHinhLeague(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifySubmitDoiHinhLeague(HostID.Server, rmiContext, data);
		}

		public bool NotifyViewLeagueReplay(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyViewLeagueReplay", data);
			return true;
		}

		public bool NotifyViewLeagueReplay(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyViewLeagueReplay(HostID.Server, rmiContext, data);
		}

		public bool NotifyThamBaiSieuCup(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyThamBaiSieuCup", data);
			return true;
		}

		public bool NotifyThamBaiSieuCup(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyThamBaiSieuCup(HostID.Server, rmiContext, data);
		}

		public bool NotifyVongQuay(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyVongQuay", data);
			return true;
		}

		public bool NotifyVongQuay(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyVongQuay(HostID.Server, rmiContext, data);
		}

		public bool NotifyLienDauData(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyLienDauData", data);
			return true;
		}

		public bool NotifyLienDauData(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyLienDauData(HostID.Server, rmiContext, data);
		}

		public bool NotifySieuCupChampion(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifySieuCupChampion", data);
			return true;
		}

		public bool NotifySieuCupChampion(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifySieuCupChampion(HostID.Server, rmiContext, data);
		}

		public bool NotifyQMDInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyQMDInfo", data);
			return true;
		}

		public bool NotifyQMDInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyQMDInfo(HostID.Server, rmiContext, data);
		}

		public bool NotifyQMDSelect(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyQMDSelect", data);
			return true;
		}

		public bool NotifyQMDSelect(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyQMDSelect(HostID.Server, rmiContext, data);
		}

		public bool NotifyQMDGetChiTietNPC(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyQMDGetChiTietNPC", data);
			return true;
		}

		public bool NotifyQMDGetChiTietNPC(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyQMDGetChiTietNPC(HostID.Server, rmiContext, data);
		}

		public bool NotifyQMDGetBXH(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyQMDGetBXH", data);
			return true;
		}

		public bool NotifyQMDGetBXH(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyQMDGetBXH(HostID.Server, rmiContext, data);
		}

		public bool NotifyQMDXongPha(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyQMDXongPha", data);
			return true;
		}

		public bool NotifyQMDXongPha(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyQMDXongPha(HostID.Server, rmiContext, data);
		}

		public bool NotifyBeQuanDeTu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyBeQuanDeTu", data);
			return true;
		}

		public bool NotifyBeQuanDeTu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyBeQuanDeTu(HostID.Server, rmiContext, data);
		}

		public bool NotifyDenGioCT(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDenGioCT", data);
			return true;
		}

		public bool NotifyDenGioCT(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDenGioCT(HostID.Server, rmiContext, data);
		}

		public bool NotifyNhanThuongTichLuyNap(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyNhanThuongTichLuyNap", data);
			return true;
		}

		public bool NotifyNhanThuongTichLuyNap(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyNhanThuongTichLuyNap(HostID.Server, rmiContext, data);
		}

		public bool NotifyNhanThuongTichLuyTieu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyNhanThuongTichLuyTieu", data);
			return true;
		}

		public bool NotifyNhanThuongTichLuyTieu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyNhanThuongTichLuyTieu(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetCacLoaiTop(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetCacLoaiTop", data);
			return true;
		}

		public bool NotifyGetCacLoaiTop(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetCacLoaiTop(HostID.Server, rmiContext, data);
		}

		public bool NotifyDoiTenBang(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDoiTenBang", data);
			return true;
		}

		public bool NotifyDoiTenBang(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDoiTenBang(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetThongTinLienServer(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetThongTinLienServer", data);
			return true;
		}

		public bool NotifyGetThongTinLienServer(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetThongTinLienServer(HostID.Server, rmiContext, data);
		}

		public bool NotifyBanPhaoHoa(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyBanPhaoHoa", data);
			return true;
		}

		public bool NotifyBanPhaoHoa(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyBanPhaoHoa(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetTopPhaoHoa(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetTopPhaoHoa", data);
			return true;
		}

		public bool NotifyGetTopPhaoHoa(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetTopPhaoHoa(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetPhanThuongPhaoHoa(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetPhanThuongPhaoHoa", data);
			return true;
		}

		public bool NotifyGetPhanThuongPhaoHoa(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetPhanThuongPhaoHoa(HostID.Server, rmiContext, data);
		}

		public bool NotifyBanPhaoHoaEvent(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyBanPhaoHoaEvent", data);
			return true;
		}

		public bool NotifyBanPhaoHoaEvent(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyBanPhaoHoaEvent(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetTopVongQuay(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetTopVongQuay", data);
			return true;
		}

		public bool NotifyGetTopVongQuay(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetTopVongQuay(HostID.Server, rmiContext, data);
		}

		public bool NotifyCreateCostume(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyCreateCostume", data);
			return true;
		}

		public bool NotifyCreateCostume(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyCreateCostume(HostID.Server, rmiContext, data);
		}

		public bool NotifyTinhLuyenCostume(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyTinhLuyenCostume", data);
			return true;
		}

		public bool NotifyTinhLuyenCostume(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyTinhLuyenCostume(HostID.Server, rmiContext, data);
		}

		public bool NotifyKhamNgocCostume(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyKhamNgocCostume", data);
			return true;
		}

		public bool NotifyKhamNgocCostume(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyKhamNgocCostume(HostID.Server, rmiContext, data);
		}

		public bool NotifyGoNgocCostume(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGoNgocCostume", data);
			return true;
		}

		public bool NotifyGoNgocCostume(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGoNgocCostume(HostID.Server, rmiContext, data);
		}

		public bool NotifyTakeOnCostume(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyTakeOnCostume", data);
			return true;
		}

		public bool NotifyTakeOnCostume(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyTakeOnCostume(HostID.Server, rmiContext, data);
		}

		public bool NotifyTakeOffCostume(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyTakeOffCostume", data);
			return true;
		}

		public bool NotifyTakeOffCostume(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyTakeOffCostume(HostID.Server, rmiContext, data);
		}

		public bool NotifyLinhThuongPhaoHoaEvent(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyLinhThuongPhaoHoaEvent", data);
			return true;
		}

		public bool NotifyLinhThuongPhaoHoaEvent(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyLinhThuongPhaoHoaEvent(HostID.Server, rmiContext, data);
		}

		public bool NotifyChuyenSinhDeTu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyChuyenSinhDeTu", data);
			return true;
		}

		public bool NotifyChuyenSinhDeTu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyChuyenSinhDeTu(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetListOtherUser(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetListOtherUser", data);
			return true;
		}

		public bool NotifyGetListOtherUser(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetListOtherUser(HostID.Server, rmiContext, data);
		}

		public bool NotifyBatThanThu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyBatThanThu", data);
			return true;
		}

		public bool NotifyBatThanThu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyBatThanThu(HostID.Server, rmiContext, data);
		}

		public bool NotifyNhanThuongDapNieu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyNhanThuongDapNieu", data);
			return true;
		}

		public bool NotifyNhanThuongDapNieu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyNhanThuongDapNieu(HostID.Server, rmiContext, data);
		}

		public bool NotifyTruongThanhThanThu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyTruongThanhThanThu", data);
			return true;
		}

		public bool NotifyTruongThanhThanThu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyTruongThanhThanThu(HostID.Server, rmiContext, data);
		}

		public bool NotifyNangPhamThanThu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyNangPhamThanThu", data);
			return true;
		}

		public bool NotifyNangPhamThanThu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyNangPhamThanThu(HostID.Server, rmiContext, data);
		}

		public bool NotifyDoiThanThu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDoiThanThu", data);
			return true;
		}

		public bool NotifyDoiThanThu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDoiThanThu(HostID.Server, rmiContext, data);
		}

		public bool NotifyThonPheThanThu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyThonPheThanThu", data);
			return true;
		}

		public bool NotifyThonPheThanThu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyThonPheThanThu(HostID.Server, rmiContext, data);
		}

		public bool NotifyTruyenCongThanThu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyTruyenCongThanThu", data);
			return true;
		}

		public bool NotifyTruyenCongThanThu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyTruyenCongThanThu(HostID.Server, rmiContext, data);
		}

		public bool NotifySetBoPhapNhanVatBatQuai(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifySetBoPhapNhanVatBatQuai", data);
			return true;
		}

		public bool NotifySetBoPhapNhanVatBatQuai(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifySetBoPhapNhanVatBatQuai(HostID.Server, rmiContext, data);
		}

		public bool NotifySetNoiCongNhanVatBatQuai(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifySetNoiCongNhanVatBatQuai", data);
			return true;
		}

		public bool NotifySetNoiCongNhanVatBatQuai(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifySetNoiCongNhanVatBatQuai(HostID.Server, rmiContext, data);
		}

		public bool NotifySetTrangBiNhanVatBatQuai(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifySetTrangBiNhanVatBatQuai", data);
			return true;
		}

		public bool NotifySetTrangBiNhanVatBatQuai(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifySetTrangBiNhanVatBatQuai(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetThanThuDao(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetThanThuDao", data);
			return true;
		}

		public bool NotifyGetThanThuDao(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetThanThuDao(HostID.Server, rmiContext, data);
		}

		public bool NotifySetThanThu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifySetThanThu", data);
			return true;
		}

		public bool NotifySetThanThu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifySetThanThu(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetGuiTietKiem(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetGuiTietKiem", data);
			return true;
		}

		public bool NotifyGetGuiTietKiem(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetGuiTietKiem(HostID.Server, rmiContext, data);
		}

		public bool NotifyThamGiaGuiTietKiem(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyThamGiaGuiTietKiem", data);
			return true;
		}

		public bool NotifyThamGiaGuiTietKiem(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyThamGiaGuiTietKiem(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetThuong1MilUser(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetThuong1MilUser", data);
			return true;
		}

		public bool NotifyGetThuong1MilUser(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetThuong1MilUser(HostID.Server, rmiContext, data);
		}

		public bool NotifyPopupThuong1MilUser(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyPopupThuong1MilUser", data);
			return true;
		}

		public bool NotifyPopupThuong1MilUser(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyPopupThuong1MilUser(HostID.Server, rmiContext, data);
		}

		public bool NotifyThuHoachSonMon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyThuHoachSonMon", data);
			return true;
		}

		public bool NotifyThuHoachSonMon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyThuHoachSonMon(HostID.Server, rmiContext, data);
		}

		public bool NotifyXayDungSonMon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyXayDungSonMon", data);
			return true;
		}

		public bool NotifyXayDungSonMon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyXayDungSonMon(HostID.Server, rmiContext, data);
		}

		public bool NotifyTanCongSonMon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyTanCongSonMon", data);
			return true;
		}

		public bool NotifyTanCongSonMon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyTanCongSonMon(HostID.Server, rmiContext, data);
		}

		public bool NotifyDoiHinhSonMon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDoiHinhSonMon", data);
			return true;
		}

		public bool NotifyDoiHinhSonMon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDoiHinhSonMon(HostID.Server, rmiContext, data);
		}

		public bool NotifyDoThamSonMon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDoThamSonMon", data);
			return true;
		}

		public bool NotifyDoThamSonMon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDoThamSonMon(HostID.Server, rmiContext, data);
		}

		public bool NotifyMuaDoThanBi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyMuaDoThanBi", data);
			return true;
		}

		public bool NotifyMuaDoThanBi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyMuaDoThanBi(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetTopSonMon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetTopSonMon", data);
			return true;
		}

		public bool NotifyGetTopSonMon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetTopSonMon(HostID.Server, rmiContext, data);
		}

		public bool NotifyDungLuyenTrangBi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDungLuyenTrangBi", data);
			return true;
		}

		public bool NotifyDungLuyenTrangBi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDungLuyenTrangBi(HostID.Server, rmiContext, data);
		}

		public bool NotifyTayLuyenTrangBi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyTayLuyenTrangBi", data);
			return true;
		}

		public bool NotifyTayLuyenTrangBi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyTayLuyenTrangBi(HostID.Server, rmiContext, data);
		}

		public bool NotifyKhaiQuangTrangBi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyKhaiQuangTrangBi", data);
			return true;
		}

		public bool NotifyKhaiQuangTrangBi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyKhaiQuangTrangBi(HostID.Server, rmiContext, data);
		}

		public bool NotifyConfirmTayLuyenTrangBi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyConfirmTayLuyenTrangBi", data);
			return true;
		}

		public bool NotifyConfirmTayLuyenTrangBi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyConfirmTayLuyenTrangBi(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetSonMonInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetSonMonInfo", data);
			return true;
		}

		public bool NotifyGetSonMonInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetSonMonInfo(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetLanhDiaInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetLanhDiaInfo", data);
			return true;
		}

		public bool NotifyGetLanhDiaInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetLanhDiaInfo(HostID.Server, rmiContext, data);
		}

		public bool NotifyUnLockVoCong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyUnLockVoCong", data);
			return true;
		}

		public bool NotifyUnLockVoCong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyUnLockVoCong(HostID.Server, rmiContext, data);
		}

		public bool NotifyQuayBacMayMan(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyQuayBacMayMan", data);
			return true;
		}

		public bool NotifyQuayBacMayMan(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyQuayBacMayMan(HostID.Server, rmiContext, data);
		}

		public bool NotifyMoveLanhDia(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyMoveLanhDia", data);
			return true;
		}

		public bool NotifyMoveLanhDia(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyMoveLanhDia(HostID.Server, rmiContext, data);
		}

		public bool NotifyUpdateLanhDiaData(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyUpdateLanhDiaData", data);
			return true;
		}

		public bool NotifyUpdateLanhDiaData(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyUpdateLanhDiaData(HostID.Server, rmiContext, data);
		}

		public bool NotifyQuayTuBaoBon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyQuayTuBaoBon", data);
			return true;
		}

		public bool NotifyQuayTuBaoBon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyQuayTuBaoBon(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetTopMoRuong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetTopMoRuong", data);
			return true;
		}

		public bool NotifyGetTopMoRuong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetTopMoRuong(HostID.Server, rmiContext, data);
		}

		public bool NotifyQuayThienMaHaPhong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyQuayThienMaHaPhong", data);
			return true;
		}

		public bool NotifyQuayThienMaHaPhong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyQuayThienMaHaPhong(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetTayVucInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetTayVucInfo", data);
			return true;
		}

		public bool NotifyGetTayVucInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetTayVucInfo(HostID.Server, rmiContext, data);
		}

		public bool NotifyMuaDoTayVuc(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyMuaDoTayVuc", data);
			return true;
		}

		public bool NotifyMuaDoTayVuc(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyMuaDoTayVuc(HostID.Server, rmiContext, data);
		}

		public bool NotifySpawnNienThu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifySpawnNienThu", data);
			return true;
		}

		public bool NotifySpawnNienThu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifySpawnNienThu(HostID.Server, rmiContext, data);
		}

		public bool NotifySummonNienThu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifySummonNienThu", data);
			return true;
		}

		public bool NotifySummonNienThu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifySummonNienThu(HostID.Server, rmiContext, data);
		}

		public bool NotifyDanhNienThu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDanhNienThu", data);
			return true;
		}

		public bool NotifyDanhNienThu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDanhNienThu(HostID.Server, rmiContext, data);
		}

		public bool NotifyThamGiaNienThu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyThamGiaNienThu", data);
			return true;
		}

		public bool NotifyThamGiaNienThu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyThamGiaNienThu(HostID.Server, rmiContext, data);
		}

		public bool NotifyNhanThuongNapHangNgay(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyNhanThuongNapHangNgay", data);
			return true;
		}

		public bool NotifyNhanThuongNapHangNgay(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyNhanThuongNapHangNgay(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetTopNienThu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetTopNienThu", data);
			return true;
		}

		public bool NotifyGetTopNienThu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetTopNienThu(HostID.Server, rmiContext, data);
		}

		public bool NotifyNopLenhBaiNienThu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyNopLenhBaiNienThu", data);
			return true;
		}

		public bool NotifyNopLenhBaiNienThu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyNopLenhBaiNienThu(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetTopLanhDia(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetTopLanhDia", data);
			return true;
		}

		public bool NotifyGetTopLanhDia(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetTopLanhDia(HostID.Server, rmiContext, data);
		}

		public bool NotifySelectStartNgua(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifySelectStartNgua", data);
			return true;
		}

		public bool NotifySelectStartNgua(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifySelectStartNgua(HostID.Server, rmiContext, data);
		}

		public bool NotifyUpdateNienThu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyUpdateNienThu", data);
			return true;
		}

		public bool NotifyUpdateNienThu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyUpdateNienThu(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetDiemMoRuong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetDiemMoRuong", data);
			return true;
		}

		public bool NotifyGetDiemMoRuong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetDiemMoRuong(HostID.Server, rmiContext, data);
		}

		public bool NotifyUpdateDailyActivities(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyUpdateDailyActivities", data);
			return true;
		}

		public bool NotifyUpdateDailyActivities(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyUpdateDailyActivities(HostID.Server, rmiContext, data);
		}

		public bool NotifyThuongDailyActivities(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyThuongDailyActivities", data);
			return true;
		}

		public bool NotifyThuongDailyActivities(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyThuongDailyActivities(HostID.Server, rmiContext, data);
		}

		public bool NotifyThienMaQuaySlot(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyThienMaQuaySlot", data);
			return true;
		}

		public bool NotifyThienMaQuaySlot(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyThienMaQuaySlot(HostID.Server, rmiContext, data);
		}

		public bool NotifyThienMaLenhCreate(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyThienMaLenhCreate", data);
			return true;
		}

		public bool NotifyThienMaLenhCreate(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyThienMaLenhCreate(HostID.Server, rmiContext, data);
		}

		public bool NotifyThienMaUpgradeSlot(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyThienMaUpgradeSlot", data);
			return true;
		}

		public bool NotifyThienMaUpgradeSlot(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyThienMaUpgradeSlot(HostID.Server, rmiContext, data);
		}

		public bool NotifyThienMaOpenSlot(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyThienMaOpenSlot", data);
			return true;
		}

		public bool NotifyThienMaOpenSlot(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyThienMaOpenSlot(HostID.Server, rmiContext, data);
		}

		public bool NotifyThienMaEquip(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyThienMaEquip", data);
			return true;
		}

		public bool NotifyThienMaEquip(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyThienMaEquip(HostID.Server, rmiContext, data);
		}

		public bool NotifyThienMaQuaySlotConfirm(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyThienMaQuaySlotConfirm", data);
			return true;
		}

		public bool NotifyThienMaQuaySlotConfirm(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyThienMaQuaySlotConfirm(HostID.Server, rmiContext, data);
		}

		public bool NotifySetTonHieu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifySetTonHieu", data);
			return true;
		}

		public bool NotifySetTonHieu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifySetTonHieu(HostID.Server, rmiContext, data);
		}

		public bool NotifySetTrangBiHoangKim(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifySetTrangBiHoangKim", data);
			return true;
		}

		public bool NotifySetTrangBiHoangKim(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifySetTrangBiHoangKim(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetTopDaiHoiVoLam(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetTopDaiHoiVoLam", data);
			return true;
		}

		public bool NotifyGetTopDaiHoiVoLam(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetTopDaiHoiVoLam(HostID.Server, rmiContext, data);
		}

		public bool NotifyDanhAnDanhCaoThu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDanhAnDanhCaoThu", data);
			return true;
		}

		public bool NotifyDanhAnDanhCaoThu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDanhAnDanhCaoThu(HostID.Server, rmiContext, data);
		}

		public bool NotifyStartBoiDuongTrangBi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyStartBoiDuongTrangBi", data);
			return true;
		}

		public bool NotifyStartBoiDuongTrangBi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyStartBoiDuongTrangBi(HostID.Server, rmiContext, data);
		}

		public bool NotifyEndBoiDuongTrangBi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyEndBoiDuongTrangBi", data);
			return true;
		}

		public bool NotifyEndBoiDuongTrangBi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyEndBoiDuongTrangBi(HostID.Server, rmiContext, data);
		}

		public bool NotifyRutQueTienNhan(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyRutQueTienNhan", data);
			return true;
		}

		public bool NotifyRutQueTienNhan(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyRutQueTienNhan(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetBaoKhoInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetBaoKhoInfo", data);
			return true;
		}

		public bool NotifyGetBaoKhoInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetBaoKhoInfo(HostID.Server, rmiContext, data);
		}

		public bool NotifyCuopBaoKho(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyCuopBaoKho", data);
			return true;
		}

		public bool NotifyCuopBaoKho(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyCuopBaoKho(HostID.Server, rmiContext, data);
		}

		public bool NotifyThuHoachBaoKho(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyThuHoachBaoKho", data);
			return true;
		}

		public bool NotifyThuHoachBaoKho(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyThuHoachBaoKho(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetListBaoKho(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetListBaoKho", data);
			return true;
		}

		public bool NotifyGetListBaoKho(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetListBaoKho(HostID.Server, rmiContext, data);
		}

		public bool NotifyQuayCamCungBiBao(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyQuayCamCungBiBao", data);
			return true;
		}

		public bool NotifyQuayCamCungBiBao(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyQuayCamCungBiBao(HostID.Server, rmiContext, data);
		}

		public bool NotifyNhanThuongCamCung(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyNhanThuongCamCung", data);
			return true;
		}

		public bool NotifyNhanThuongCamCung(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyNhanThuongCamCung(HostID.Server, rmiContext, data);
		}

		public bool NotifyGetHoaVangInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyGetHoaVangInfo", data);
			return true;
		}

		public bool NotifyGetHoaVangInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyGetHoaVangInfo(HostID.Server, rmiContext, data);
		}

		public bool NotifyHoaVang(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyHoaVang", data);
			return true;
		}

		public bool NotifyHoaVang(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyHoaVang(HostID.Server, rmiContext, data);
		}

		public bool NotifyTestProudNet(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyTestProudNet", data);
			return true;
		}

		public bool NotifyTestProudNet(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyTestProudNet(HostID.Server, rmiContext, data);
		}

		public bool NotifyTrangBiHuyenKhi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyTrangBiHuyenKhi", data);
			return true;
		}

		public bool NotifyTrangBiHuyenKhi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyTrangBiHuyenKhi(HostID.Server, rmiContext, data);
		}

		public bool NotifyThangCapHuyenKhi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyThangCapHuyenKhi", data);
			return true;
		}

		public bool NotifyThangCapHuyenKhi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyThangCapHuyenKhi(HostID.Server, rmiContext, data);
		}

		public bool NotifyCheTaoHuyenKhi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyCheTaoHuyenKhi", data);
			return true;
		}

		public bool NotifyCheTaoHuyenKhi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyCheTaoHuyenKhi(HostID.Server, rmiContext, data);
		}

		public bool NotifyDotPhaChienHon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyDotPhaChienHon", data);
			return true;
		}

		public bool NotifyDotPhaChienHon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyDotPhaChienHon(HostID.Server, rmiContext, data);
		}

		public bool NotifyUseTanHonTangLevelChienHon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyUseTanHonTangLevelChienHon", data);
			return true;
		}

		public bool NotifyUseTanHonTangLevelChienHon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyUseTanHonTangLevelChienHon(HostID.Server, rmiContext, data);
		}

		public bool NotifyTrieuHoiChienHon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyTrieuHoiChienHon", data);
			return true;
		}

		public bool NotifyTrieuHoiChienHon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyTrieuHoiChienHon(HostID.Server, rmiContext, data);
		}

		public bool NotifyEndBoiDuongChienHon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyEndBoiDuongChienHon", data);
			return true;
		}

		public bool NotifyEndBoiDuongChienHon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyEndBoiDuongChienHon(HostID.Server, rmiContext, data);
		}

		public bool NotifyChienHonChangeBuff(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyChienHonChangeBuff", data);
			return true;
		}

		public bool NotifyChienHonChangeBuff(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyChienHonChangeBuff(HostID.Server, rmiContext, data);
		}

		public bool NotifyStartBoiDuongChienHon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyStartBoiDuongChienHon", data);
			return true;
		}

		public bool NotifyStartBoiDuongChienHon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyStartBoiDuongChienHon(HostID.Server, rmiContext, data);
		}

		public bool NotifyQuayDiemHoaVang(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyQuayDiemHoaVang", data);
			return true;
		}

		public bool NotifyQuayDiemHoaVang(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyQuayDiemHoaVang(HostID.Server, rmiContext, data);
		}

		public bool NotifyUseTuiThan(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyUseTuiThan", data);
			return true;
		}

		public bool NotifyUseTuiThan(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyUseTuiThan(HostID.Server, rmiContext, data);
		}

		public bool NotifyNhanThuongDailyKNB(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyNhanThuongDailyKNB", data);
			return true;
		}

		public bool NotifyNhanThuongDailyKNB(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyNhanThuongDailyKNB(HostID.Server, rmiContext, data);
		}
	}
}
