using LitJson;
using Nettention.Proud;

namespace GameC2S
{
	public class Proxy : IJsonProxy
	{
		public bool RequestNextLogon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestNextLogon", data);
			return true;
		}

		public bool RequestNextLogon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestNextLogon(HostID.Server, rmiContext, data);
		}

		public bool RequestSetInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSetInfo", data);
			return true;
		}

		public bool RequestSetInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSetInfo(HostID.Server, rmiContext, data);
		}

		public bool RequestSetHeroData(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSetHeroData", data);
			return true;
		}

		public bool RequestSetHeroData(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSetHeroData(HostID.Server, rmiContext, data);
		}

		public bool RequestGetBattleResult(HostID remote, RmiContext rmiContext, string name)
		{
			CJsonTransport.Send("RequestGetBattleResult", name);
			return true;
		}

		public bool RequestGetBattleResult(HostID[] remotes, RmiContext rmiContext, string name)
		{
			return RequestGetBattleResult(HostID.Server, rmiContext, name);
		}

		public bool RequestGetListLK(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetListLK", data);
			return true;
		}

		public bool RequestGetListLK(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetListLK(HostID.Server, rmiContext, data);
		}

		public bool RequestSetHeroCfg(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSetHeroCfg", data);
			return true;
		}

		public bool RequestSetHeroCfg(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSetHeroCfg(HostID.Server, rmiContext, data);
		}

		public bool RequestGetInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetInfo", data);
			return true;
		}

		public bool RequestGetInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetInfo(HostID.Server, rmiContext, data);
		}

		public bool RequestChangeNextPos(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestChangeNextPos", data);
			return true;
		}

		public bool RequestChangeNextPos(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestChangeNextPos(HostID.Server, rmiContext, data);
		}

		public bool RequestGetListOtherPlayer(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetListOtherPlayer", data);
			return true;
		}

		public bool RequestGetListOtherPlayer(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetListOtherPlayer(HostID.Server, rmiContext, data);
		}

		public bool RequestGetLuanKiemInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetLuanKiemInfo", data);
			return true;
		}

		public bool RequestGetLuanKiemInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetLuanKiemInfo(HostID.Server, rmiContext, data);
		}

		public bool RequestCapNhatDiemLuanKiem(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestCapNhatDiemLuanKiem", data);
			return true;
		}

		public bool RequestCapNhatDiemLuanKiem(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestCapNhatDiemLuanKiem(HostID.Server, rmiContext, data);
		}

		public bool RequestNhanThuongLuanKiem(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestNhanThuongLuanKiem", data);
			return true;
		}

		public bool RequestNhanThuongLuanKiem(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestNhanThuongLuanKiem(HostID.Server, rmiContext, data);
		}

		public bool RequestDoiThuongLuanKiem(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestDoiThuongLuanKiem", data);
			return true;
		}

		public bool RequestDoiThuongLuanKiem(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestDoiThuongLuanKiem(HostID.Server, rmiContext, data);
		}

		public bool RequestDauLuanKiem(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestDauLuanKiem", data);
			return true;
		}

		public bool RequestDauLuanKiem(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestDauLuanKiem(HostID.Server, rmiContext, data);
		}

		public bool RequestDanhGiangHo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestDanhGiangHo", data);
			return true;
		}

		public bool RequestDanhGiangHo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestDanhGiangHo(HostID.Server, rmiContext, data);
		}

		public bool RequestSetVCSetting(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSetVCSetting", data);
			return true;
		}

		public bool RequestSetVCSetting(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSetVCSetting(HostID.Server, rmiContext, data);
		}

		public bool RequestSetTrangBi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSetTrangBi", data);
			return true;
		}

		public bool RequestSetTrangBi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSetTrangBi(HostID.Server, rmiContext, data);
		}

		public bool RequestSetDoiHinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSetDoiHinh", data);
			return true;
		}

		public bool RequestSetDoiHinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSetDoiHinh(HostID.Server, rmiContext, data);
		}

		public bool RequestSendChatMsg(HostID remote, RmiContext rmiContext, string msg)
		{
			CJsonTransport.Send("RequestSendChatMsg", msg);
			return true;
		}

		public bool RequestSendChatMsg(HostID[] remotes, RmiContext rmiContext, string msg)
		{
			return RequestSendChatMsg(HostID.Server, rmiContext, msg);
		}

		public bool RequestSetVoCong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSetVoCong", data);
			return true;
		}

		public bool RequestSetVoCong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSetVoCong(HostID.Server, rmiContext, data);
		}

		public bool RequestSetTranHinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSetTranHinh", data);
			return true;
		}

		public bool RequestSetTranHinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSetTranHinh(HostID.Server, rmiContext, data);
		}

		public bool RequestStartBoiDuong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestStartBoiDuong", data);
			return true;
		}

		public bool RequestStartBoiDuong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestStartBoiDuong(HostID.Server, rmiContext, data);
		}

		public bool RequestEndBoiDuong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestEndBoiDuong", data);
			return true;
		}

		public bool RequestEndBoiDuong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestEndBoiDuong(HostID.Server, rmiContext, data);
		}

		public bool RequestTuLuyenDeTu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestTuLuyenDeTu", data);
			return true;
		}

		public bool RequestTuLuyenDeTu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestTuLuyenDeTu(HostID.Server, rmiContext, data);
		}

		public bool RequestTrieuHoiDeTuBangHon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestTrieuHoiDeTuBangHon", data);
			return true;
		}

		public bool RequestTrieuHoiDeTuBangHon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestTrieuHoiDeTuBangHon(HostID.Server, rmiContext, data);
		}

		public bool RequestTruyenCong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestTruyenCong", data);
			return true;
		}

		public bool RequestTruyenCong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestTruyenCong(HostID.Server, rmiContext, data);
		}

		public bool RequestGetDongNhanInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetDongNhanInfo", data);
			return true;
		}

		public bool RequestGetDongNhanInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetDongNhanInfo(HostID.Server, rmiContext, data);
		}

		public bool RequestDanhDongNhan(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestDanhDongNhan", data);
			return true;
		}

		public bool RequestDanhDongNhan(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestDanhDongNhan(HostID.Server, rmiContext, data);
		}

		public bool RequestCuongHoaTrangBi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestCuongHoaTrangBi", data);
			return true;
		}

		public bool RequestCuongHoaTrangBi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestCuongHoaTrangBi(HostID.Server, rmiContext, data);
		}

		public bool RequestBanTrangBi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestBanTrangBi", data);
			return true;
		}

		public bool RequestBanTrangBi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestBanTrangBi(HostID.Server, rmiContext, data);
		}

		public bool RequestGhepManhTrangBi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGhepManhTrangBi", data);
			return true;
		}

		public bool RequestGhepManhTrangBi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGhepManhTrangBi(HostID.Server, rmiContext, data);
		}

		public bool RequestGhepManhVoCong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGhepManhVoCong", data);
			return true;
		}

		public bool RequestGhepManhVoCong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGhepManhVoCong(HostID.Server, rmiContext, data);
		}

		public bool RequestTinhLuyenTrangBi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestTinhLuyenTrangBi", data);
			return true;
		}

		public bool RequestTinhLuyenTrangBi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestTinhLuyenTrangBi(HostID.Server, rmiContext, data);
		}

		public bool RequestSetDoiHinhHoTro(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSetDoiHinhHoTro", data);
			return true;
		}

		public bool RequestSetDoiHinhHoTro(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSetDoiHinhHoTro(HostID.Server, rmiContext, data);
		}

		public bool RequestOpenDoiHinhHoTro(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestOpenDoiHinhHoTro", data);
			return true;
		}

		public bool RequestOpenDoiHinhHoTro(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestOpenDoiHinhHoTro(HostID.Server, rmiContext, data);
		}

		public bool RequestThamNgoVoCong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestThamNgoVoCong", data);
			return true;
		}

		public bool RequestThamNgoVoCong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestThamNgoVoCong(HostID.Server, rmiContext, data);
		}

		public bool RequestTinhLuyenVoCong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestTinhLuyenVoCong", data);
			return true;
		}

		public bool RequestTinhLuyenVoCong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestTinhLuyenVoCong(HostID.Server, rmiContext, data);
		}

		public bool RequestLayDeTu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestLayDeTu", data);
			return true;
		}

		public bool RequestLayDeTu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestLayDeTu(HostID.Server, rmiContext, data);
		}

		public bool RequestBuyVatPham(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestBuyVatPham", data);
			return true;
		}

		public bool RequestBuyVatPham(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestBuyVatPham(HostID.Server, rmiContext, data);
		}

		public bool RequestCaoNhan(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestCaoNhan", data);
			return true;
		}

		public bool RequestCaoNhan(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestCaoNhan(HostID.Server, rmiContext, data);
		}

		public bool RequestThuongNhan(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestThuongNhan", data);
			return true;
		}

		public bool RequestThuongNhan(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestThuongNhan(HostID.Server, rmiContext, data);
		}

		public bool RequestBanDo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestBanDo", data);
			return true;
		}

		public bool RequestBanDo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestBanDo(HostID.Server, rmiContext, data);
		}

		public bool RequestBangHuu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestBangHuu", data);
			return true;
		}

		public bool RequestBangHuu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestBangHuu(HostID.Server, rmiContext, data);
		}

		public bool RequestTyThi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestTyThi", data);
			return true;
		}

		public bool RequestTyThi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestTyThi(HostID.Server, rmiContext, data);
		}

		public bool RequestBuyLeBao(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestBuyLeBao", data);
			return true;
		}

		public bool RequestBuyLeBao(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestBuyLeBao(HostID.Server, rmiContext, data);
		}

		public bool RequestHoiTheLuc(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestHoiTheLuc", data);
			return true;
		}

		public bool RequestHoiTheLuc(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestHoiTheLuc(HostID.Server, rmiContext, data);
		}

		public bool RequestSelectStartDeTu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSelectStartDeTu", data);
			return true;
		}

		public bool RequestSelectStartDeTu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSelectStartDeTu(HostID.Server, rmiContext, data);
		}

		public bool RequestKyNgoThamBai(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestKyNgoThamBai", data);
			return true;
		}

		public bool RequestKyNgoThamBai(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestKyNgoThamBai(HostID.Server, rmiContext, data);
		}

		public bool RequestHuyetChienInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestHuyetChienInfo", data);
			return true;
		}

		public bool RequestHuyetChienInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestHuyetChienInfo(HostID.Server, rmiContext, data);
		}

		public bool RequestStartHuyetChien(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestStartHuyetChien", data);
			return true;
		}

		public bool RequestStartHuyetChien(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestStartHuyetChien(HostID.Server, rmiContext, data);
		}

		public bool RequestHoiSinhHuyetChien(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestHoiSinhHuyetChien", data);
			return true;
		}

		public bool RequestHoiSinhHuyetChien(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestHoiSinhHuyetChien(HostID.Server, rmiContext, data);
		}

		public bool RequestHuyetChienTangThuocTinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestHuyetChienTangThuocTinh", data);
			return true;
		}

		public bool RequestHuyetChienTangThuocTinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestHuyetChienTangThuocTinh(HostID.Server, rmiContext, data);
		}

		public bool RequestDanhHuyetChien(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestDanhHuyetChien", data);
			return true;
		}

		public bool RequestDanhHuyetChien(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestDanhHuyetChien(HostID.Server, rmiContext, data);
		}

		public bool RequestNhanThuongHuyetChien(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestNhanThuongHuyetChien", data);
			return true;
		}

		public bool RequestNhanThuongHuyetChien(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestNhanThuongHuyetChien(HostID.Server, rmiContext, data);
		}

		public bool RequestGetTopHuyetChien(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetTopHuyetChien", data);
			return true;
		}

		public bool RequestGetTopHuyetChien(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetTopHuyetChien(HostID.Server, rmiContext, data);
		}

		public bool RequestOpenHop(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestOpenHop", data);
			return true;
		}

		public bool RequestOpenHop(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestOpenHop(HostID.Server, rmiContext, data);
		}

		public bool RequestSetDoiHinhAndTranHinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSetDoiHinhAndTranHinh", data);
			return true;
		}

		public bool RequestSetDoiHinhAndTranHinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSetDoiHinhAndTranHinh(HostID.Server, rmiContext, data);
		}

		public bool RequestTestDongNhan(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestTestDongNhan", data);
			return true;
		}

		public bool RequestTestDongNhan(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestTestDongNhan(HostID.Server, rmiContext, data);
		}

		public bool RequestNhanThuongGiangHo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestNhanThuongGiangHo", data);
			return true;
		}

		public bool RequestNhanThuongGiangHo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestNhanThuongGiangHo(HostID.Server, rmiContext, data);
		}

		public bool RequestDanhNhanhGiangHo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestDanhNhanhGiangHo", data);
			return true;
		}

		public bool RequestDanhNhanhGiangHo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestDanhNhanhGiangHo(HostID.Server, rmiContext, data);
		}

		public bool RequestAnGaGiangHo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestAnGaGiangHo", data);
			return true;
		}

		public bool RequestAnGaGiangHo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestAnGaGiangHo(HostID.Server, rmiContext, data);
		}

		public bool RequestResetLuotGiangHo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestResetLuotGiangHo", data);
			return true;
		}

		public bool RequestResetLuotGiangHo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestResetLuotGiangHo(HostID.Server, rmiContext, data);
		}

		public bool RequestDanhDanhSon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestDanhDanhSon", data);
			return true;
		}

		public bool RequestDanhDanhSon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestDanhDanhSon(HostID.Server, rmiContext, data);
		}

		public bool RequestMoThuongDanhSon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestMoThuongDanhSon", data);
			return true;
		}

		public bool RequestMoThuongDanhSon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestMoThuongDanhSon(HostID.Server, rmiContext, data);
		}

		public bool RequestMoHetDanhSon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestMoHetDanhSon", data);
			return true;
		}

		public bool RequestMoHetDanhSon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestMoHetDanhSon(HostID.Server, rmiContext, data);
		}

		public bool RequestVuotAiDanhSon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestVuotAiDanhSon", data);
			return true;
		}

		public bool RequestVuotAiDanhSon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestVuotAiDanhSon(HostID.Server, rmiContext, data);
		}

		public bool RequestChonDongDoiDanhSon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestChonDongDoiDanhSon", data);
			return true;
		}

		public bool RequestChonDongDoiDanhSon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestChonDongDoiDanhSon(HostID.Server, rmiContext, data);
		}

		public bool RequestGetDongDoiDanhSon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetDongDoiDanhSon", data);
			return true;
		}

		public bool RequestGetDongDoiDanhSon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetDongDoiDanhSon(HostID.Server, rmiContext, data);
		}

		public bool RequestHuaNguyen(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestHuaNguyen", data);
			return true;
		}

		public bool RequestHuaNguyen(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestHuaNguyen(HostID.Server, rmiContext, data);
		}

		public bool RequestLenCapNhanThuong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestLenCapNhanThuong", data);
			return true;
		}

		public bool RequestLenCapNhanThuong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestLenCapNhanThuong(HostID.Server, rmiContext, data);
		}

		public bool RequestThanTai(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestThanTai", data);
			return true;
		}

		public bool RequestThanTai(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestThanTai(HostID.Server, rmiContext, data);
		}

		public bool RequestXocDiaInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestXocDiaInfo", data);
			return true;
		}

		public bool RequestXocDiaInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestXocDiaInfo(HostID.Server, rmiContext, data);
		}

		public bool RequestChoiXocDia(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestChoiXocDia", data);
			return true;
		}

		public bool RequestChoiXocDia(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestChoiXocDia(HostID.Server, rmiContext, data);
		}

		public bool RequestCuuVienTieuPhong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestCuuVienTieuPhong", data);
			return true;
		}

		public bool RequestCuuVienTieuPhong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestCuuVienTieuPhong(HostID.Server, rmiContext, data);
		}

		public bool RequestNhanRuongThachSanh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestNhanRuongThachSanh", data);
			return true;
		}

		public bool RequestNhanRuongThachSanh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestNhanRuongThachSanh(HostID.Server, rmiContext, data);
		}

		public bool RequestPaymentConfirm(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestPaymentConfirm", data);
			return true;
		}

		public bool RequestPaymentConfirm(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestPaymentConfirm(HostID.Server, rmiContext, data);
		}

		public bool RequestDangNhapNhanThuong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestDangNhapNhanThuong", data);
			return true;
		}

		public bool RequestDangNhapNhanThuong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestDangNhapNhanThuong(HostID.Server, rmiContext, data);
		}

		public bool RequestUongRuouTieuPhong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestUongRuouTieuPhong", data);
			return true;
		}

		public bool RequestUongRuouTieuPhong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestUongRuouTieuPhong(HostID.Server, rmiContext, data);
		}

		public bool RequestGetDoiRuouInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetDoiRuouInfo", data);
			return true;
		}

		public bool RequestGetDoiRuouInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetDoiRuouInfo(HostID.Server, rmiContext, data);
		}

		public bool RequestDoiRuou(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestDoiRuou", data);
			return true;
		}

		public bool RequestDoiRuou(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestDoiRuou(HostID.Server, rmiContext, data);
		}

		public bool RequestBatCoc(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestBatCoc", data);
			return true;
		}

		public bool RequestBatCoc(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestBatCoc(HostID.Server, rmiContext, data);
		}

		public bool RequestChuocThan(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestChuocThan", data);
			return true;
		}

		public bool RequestChuocThan(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestChuocThan(HostID.Server, rmiContext, data);
		}

		public bool RequestSearchBanBe(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSearchBanBe", data);
			return true;
		}

		public bool RequestSearchBanBe(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSearchBanBe(HostID.Server, rmiContext, data);
		}

		public bool RequestThachDau(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestThachDau", data);
			return true;
		}

		public bool RequestThachDau(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestThachDau(HostID.Server, rmiContext, data);
		}

		public bool RequestNhanThachDau(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestNhanThachDau", data);
			return true;
		}

		public bool RequestNhanThachDau(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestNhanThachDau(HostID.Server, rmiContext, data);
		}

		public bool RequestAddBanBe(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestAddBanBe", data);
			return true;
		}

		public bool RequestAddBanBe(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestAddBanBe(HostID.Server, rmiContext, data);
		}

		public bool RequestAcceptBanBe(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestAcceptBanBe", data);
			return true;
		}

		public bool RequestAcceptBanBe(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestAcceptBanBe(HostID.Server, rmiContext, data);
		}

		public bool RequestDeleteBanBe(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestDeleteBanBe", data);
			return true;
		}

		public bool RequestDeleteBanBe(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestDeleteBanBe(HostID.Server, rmiContext, data);
		}

		public bool RequestBanBeCuuThuInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestBanBeCuuThuInfo", data);
			return true;
		}

		public bool RequestBanBeCuuThuInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestBanBeCuuThuInfo(HostID.Server, rmiContext, data);
		}

		public bool RequestChatInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestChatInfo", data);
			return true;
		}

		public bool RequestChatInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestChatInfo(HostID.Server, rmiContext, data);
		}

		public bool RequestSendChatAll(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSendChatAll", data);
			return true;
		}

		public bool RequestSendChatAll(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSendChatAll(HostID.Server, rmiContext, data);
		}

		public bool RequestDatTenMonPhai(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestDatTenMonPhai", data);
			return true;
		}

		public bool RequestDatTenMonPhai(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestDatTenMonPhai(HostID.Server, rmiContext, data);
		}

		public bool RequestULinhInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestULinhInfo", data);
			return true;
		}

		public bool RequestULinhInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestULinhInfo(HostID.Server, rmiContext, data);
		}

		public bool RequestDoiItemULinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestDoiItemULinh", data);
			return true;
		}

		public bool RequestDoiItemULinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestDoiItemULinh(HostID.Server, rmiContext, data);
		}

		public bool RequestKnbRefreshULinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestKnbRefreshULinh", data);
			return true;
		}

		public bool RequestKnbRefreshULinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestKnbRefreshULinh(HostID.Server, rmiContext, data);
		}

		public bool RequestDoiThuongULinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestDoiThuongULinh", data);
			return true;
		}

		public bool RequestDoiThuongULinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestDoiThuongULinh(HostID.Server, rmiContext, data);
		}

		public bool RequestGetTopULinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetTopULinh", data);
			return true;
		}

		public bool RequestGetTopULinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetTopULinh(HostID.Server, rmiContext, data);
		}

		public bool RequestKichHoatGiftCode(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestKichHoatGiftCode", data);
			return true;
		}

		public bool RequestKichHoatGiftCode(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestKichHoatGiftCode(HostID.Server, rmiContext, data);
		}

		public bool RequestUseCustomItem(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestUseCustomItem", data);
			return true;
		}

		public bool RequestUseCustomItem(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestUseCustomItem(HostID.Server, rmiContext, data);
		}

		public bool RequestBuyVatPhamAndUse(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestBuyVatPhamAndUse", data);
			return true;
		}

		public bool RequestBuyVatPhamAndUse(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestBuyVatPhamAndUse(HostID.Server, rmiContext, data);
		}

		public bool RequestHighlight(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestHighlight", data);
			return true;
		}

		public bool RequestHighlight(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestHighlight(HostID.Server, rmiContext, data);
		}

		public bool RequestUseMailPhanThuong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestUseMailPhanThuong", data);
			return true;
		}

		public bool RequestUseMailPhanThuong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestUseMailPhanThuong(HostID.Server, rmiContext, data);
		}

		public bool RequestReadAllMail(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestReadAllMail", data);
			return true;
		}

		public bool RequestReadAllMail(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestReadAllMail(HostID.Server, rmiContext, data);
		}

		public bool RequestRefreshMail(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestRefreshMail", data);
			return true;
		}

		public bool RequestRefreshMail(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestRefreshMail(HostID.Server, rmiContext, data);
		}

		public bool RequestUseRuongThan(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestUseRuongThan", data);
			return true;
		}

		public bool RequestUseRuongThan(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestUseRuongThan(HostID.Server, rmiContext, data);
		}

		public bool RequestBatTho(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestBatTho", data);
			return true;
		}

		public bool RequestBatTho(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestBatTho(HostID.Server, rmiContext, data);
		}

		public bool RequestGetFriendsDoiHinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetFriendsDoiHinh", data);
			return true;
		}

		public bool RequestGetFriendsDoiHinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetFriendsDoiHinh(HostID.Server, rmiContext, data);
		}

		public bool RequestXemThongTinMonPhai(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestXemThongTinMonPhai", data);
			return true;
		}

		public bool RequestXemThongTinMonPhai(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestXemThongTinMonPhai(HostID.Server, rmiContext, data);
		}

		public bool RequestSendMail(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSendMail", data);
			return true;
		}

		public bool RequestSendMail(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSendMail(HostID.Server, rmiContext, data);
		}

		public bool RequestDangNhapQuayXoSo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestDangNhapQuayXoSo", data);
			return true;
		}

		public bool RequestDangNhapQuayXoSo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestDangNhapQuayXoSo(HostID.Server, rmiContext, data);
		}

		public bool RequestTangTheLuc(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestTangTheLuc", data);
			return true;
		}

		public bool RequestTangTheLuc(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestTangTheLuc(HostID.Server, rmiContext, data);
		}

		public bool RequestDoiDo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestDoiDo", data);
			return true;
		}

		public bool RequestDoiDo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestDoiDo(HostID.Server, rmiContext, data);
		}

		public bool RequestGetDuaTopLevelInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetDuaTopLevelInfo", data);
			return true;
		}

		public bool RequestGetDuaTopLevelInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetDuaTopLevelInfo(HostID.Server, rmiContext, data);
		}

		public bool RequestGetDuaTopLuanKiemInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetDuaTopLuanKiemInfo", data);
			return true;
		}

		public bool RequestGetDuaTopLuanKiemInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetDuaTopLuanKiemInfo(HostID.Server, rmiContext, data);
		}

		public bool RequestThamGiaCT2(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestThamGiaCT2", data);
			return true;
		}

		public bool RequestThamGiaCT2(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestThamGiaCT2(HostID.Server, rmiContext, data);
		}

		public bool RequestCT2PlayerPos(HostID remote, RmiContext rmiContext, float pos_x, float pos_z, float vel_x, float vel_z)
		{
			CJsonTransport.Send("RequestCT2PlayerPos", JsonMapper.ToJson(new object[4] { pos_x, pos_z, vel_x, vel_z }));
			return true;
		}

		public bool RequestCT2PlayerPos(HostID[] remotes, RmiContext rmiContext, float pos_x, float pos_z, float vel_x, float vel_z)
		{
			return RequestCT2PlayerPos(HostID.Server, rmiContext, pos_x, pos_z, vel_x, vel_z);
		}

		public bool RequestListCT2(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestListCT2", data);
			return true;
		}

		public bool RequestListCT2(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestListCT2(HostID.Server, rmiContext, data);
		}

		public bool RequestLapLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestLapLienMinh", data);
			return true;
		}

		public bool RequestLapLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestLapLienMinh(HostID.Server, rmiContext, data);
		}

		public bool RequestTestCT2(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestTestCT2", data);
			return true;
		}

		public bool RequestTestCT2(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestTestCT2(HostID.Server, rmiContext, data);
		}

		public bool RequestGiaNhapLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGiaNhapLienMinh", data);
			return true;
		}

		public bool RequestGiaNhapLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGiaNhapLienMinh(HostID.Server, rmiContext, data);
		}

		public bool RequestChapNhanGiaNhapLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestChapNhanGiaNhapLienMinh", data);
			return true;
		}

		public bool RequestChapNhanGiaNhapLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestChapNhanGiaNhapLienMinh(HostID.Server, rmiContext, data);
		}

		public bool RequestThoatLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestThoatLienMinh", data);
			return true;
		}

		public bool RequestThoatLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestThoatLienMinh(HostID.Server, rmiContext, data);
		}

		public bool RequestFinishNhiemVuLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestFinishNhiemVuLienMinh", data);
			return true;
		}

		public bool RequestFinishNhiemVuLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestFinishNhiemVuLienMinh(HostID.Server, rmiContext, data);
		}

		public bool RequestResetNhiemVuLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestResetNhiemVuLienMinh", data);
			return true;
		}

		public bool RequestResetNhiemVuLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestResetNhiemVuLienMinh(HostID.Server, rmiContext, data);
		}

		public bool RequestRutGiaNhapLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestRutGiaNhapLienMinh", data);
			return true;
		}

		public bool RequestRutGiaNhapLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestRutGiaNhapLienMinh(HostID.Server, rmiContext, data);
		}

		public bool RequestDangHuongLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestDangHuongLienMinh", data);
			return true;
		}

		public bool RequestDangHuongLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestDangHuongLienMinh(HostID.Server, rmiContext, data);
		}

		public bool RequestDoiThuongLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestDoiThuongLienMinh", data);
			return true;
		}

		public bool RequestDoiThuongLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestDoiThuongLienMinh(HostID.Server, rmiContext, data);
		}

		public bool RequestDoiMinhChu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestDoiMinhChu", data);
			return true;
		}

		public bool RequestDoiMinhChu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestDoiMinhChu(HostID.Server, rmiContext, data);
		}

		public bool RequestDoiPhoMinhChu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestDoiPhoMinhChu", data);
			return true;
		}

		public bool RequestDoiPhoMinhChu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestDoiPhoMinhChu(HostID.Server, rmiContext, data);
		}

		public bool RequestNangCapCongTrinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestNangCapCongTrinh", data);
			return true;
		}

		public bool RequestNangCapCongTrinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestNangCapCongTrinh(HostID.Server, rmiContext, data);
		}

		public bool RequestCT2EndBattle(HostID remote, RmiContext rmiContext)
		{
			CJsonTransport.Send("RequestCT2EndBattle", "");
			return true;
		}

		public bool RequestCT2EndBattle(HostID[] remotes, RmiContext rmiContext)
		{
			return RequestCT2EndBattle(HostID.Server, rmiContext);
		}

		public bool RequestKhamNgoc(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestKhamNgoc", data);
			return true;
		}

		public bool RequestKhamNgoc(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestKhamNgoc(HostID.Server, rmiContext, data);
		}

		public bool RequestGoNgoc(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGoNgoc", data);
			return true;
		}

		public bool RequestGoNgoc(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGoNgoc(HostID.Server, rmiContext, data);
		}

		public bool RequestGetTopLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetTopLienMinh", data);
			return true;
		}

		public bool RequestGetTopLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetTopLienMinh(HostID.Server, rmiContext, data);
		}

		public bool RequestSearchLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSearchLienMinh", data);
			return true;
		}

		public bool RequestSearchLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSearchLienMinh(HostID.Server, rmiContext, data);
		}

		public bool RequestGetThongTinLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetThongTinLienMinh", data);
			return true;
		}

		public bool RequestGetThongTinLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetThongTinLienMinh(HostID.Server, rmiContext, data);
		}

		public bool RequestNhanThuongDiHoaCungAll(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestNhanThuongDiHoaCungAll", data);
			return true;
		}

		public bool RequestNhanThuongDiHoaCungAll(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestNhanThuongDiHoaCungAll(HostID.Server, rmiContext, data);
		}

		public bool RequestGetDiHoaCungInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetDiHoaCungInfo", data);
			return true;
		}

		public bool RequestGetDiHoaCungInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetDiHoaCungInfo(HostID.Server, rmiContext, data);
		}

		public bool RequestGetTopDiHoaCung(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetTopDiHoaCung", data);
			return true;
		}

		public bool RequestGetTopDiHoaCung(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetTopDiHoaCung(HostID.Server, rmiContext, data);
		}

		public bool RequestDuoiKhoiLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestDuoiKhoiLienMinh", data);
			return true;
		}

		public bool RequestDuoiKhoiLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestDuoiKhoiLienMinh(HostID.Server, rmiContext, data);
		}

		public bool RequestCT2BangXepHangTuanNay(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestCT2BangXepHangTuanNay", data);
			return true;
		}

		public bool RequestCT2BangXepHangTuanNay(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestCT2BangXepHangTuanNay(HostID.Server, rmiContext, data);
		}

		public bool RequestCT2BangXepHangTuanTruoc(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestCT2BangXepHangTuanTruoc", data);
			return true;
		}

		public bool RequestCT2BangXepHangTuanTruoc(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestCT2BangXepHangTuanTruoc(HostID.Server, rmiContext, data);
		}

		public bool RequestThamGiaLuaTraiLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestThamGiaLuaTraiLienMinh", data);
			return true;
		}

		public bool RequestThamGiaLuaTraiLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestThamGiaLuaTraiLienMinh(HostID.Server, rmiContext, data);
		}

		public bool RequestLienMinhThoiLua(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestLienMinhThoiLua", data);
			return true;
		}

		public bool RequestLienMinhThoiLua(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestLienMinhThoiLua(HostID.Server, rmiContext, data);
		}

		public bool RequestRoiKhoiLuaTraiLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestRoiKhoiLuaTraiLienMinh", data);
			return true;
		}

		public bool RequestRoiKhoiLuaTraiLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestRoiKhoiLuaTraiLienMinh(HostID.Server, rmiContext, data);
		}

		public bool RequestSetGioLuaTraiLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSetGioLuaTraiLienMinh", data);
			return true;
		}

		public bool RequestSetGioLuaTraiLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSetGioLuaTraiLienMinh(HostID.Server, rmiContext, data);
		}

		public bool RequestSuaThongBaoLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSuaThongBaoLienMinh", data);
			return true;
		}

		public bool RequestSuaThongBaoLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSuaThongBaoLienMinh(HostID.Server, rmiContext, data);
		}

		public bool RequestSendChatLienMinh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSendChatLienMinh", data);
			return true;
		}

		public bool RequestSendChatLienMinh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSendChatLienMinh(HostID.Server, rmiContext, data);
		}

		public bool RequestChatLienMinhInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestChatLienMinhInfo", data);
			return true;
		}

		public bool RequestChatLienMinhInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestChatLienMinhInfo(HostID.Server, rmiContext, data);
		}

		public bool RequestPhanRaTrangBi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestPhanRaTrangBi", data);
			return true;
		}

		public bool RequestPhanRaTrangBi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestPhanRaTrangBi(HostID.Server, rmiContext, data);
		}

		public bool RequestGetExpLuaTrai(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetExpLuaTrai", data);
			return true;
		}

		public bool RequestGetExpLuaTrai(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetExpLuaTrai(HostID.Server, rmiContext, data);
		}

		public bool RequestCT2GetBXH(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestCT2GetBXH", data);
			return true;
		}

		public bool RequestCT2GetBXH(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestCT2GetBXH(HostID.Server, rmiContext, data);
		}

		public bool RequestUpdateLienMinhData(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestUpdateLienMinhData", data);
			return true;
		}

		public bool RequestUpdateLienMinhData(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestUpdateLienMinhData(HostID.Server, rmiContext, data);
		}

		public bool RequestCT2Quit(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestCT2Quit", data);
			return true;
		}

		public bool RequestCT2Quit(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestCT2Quit(HostID.Server, rmiContext, data);
		}

		public bool RequestCardPayment(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestCardPayment", data);
			return true;
		}

		public bool RequestCardPayment(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestCardPayment(HostID.Server, rmiContext, data);
		}

		public bool RequestLapNguyenKhi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestLapNguyenKhi", data);
			return true;
		}

		public bool RequestLapNguyenKhi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestLapNguyenKhi(HostID.Server, rmiContext, data);
		}

		public bool RequestThaoNguyenKhi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestThaoNguyenKhi", data);
			return true;
		}

		public bool RequestThaoNguyenKhi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestThaoNguyenKhi(HostID.Server, rmiContext, data);
		}

		public bool RequestNangCapNguyenKhi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestNangCapNguyenKhi", data);
			return true;
		}

		public bool RequestNangCapNguyenKhi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestNangCapNguyenKhi(HostID.Server, rmiContext, data);
		}

		public bool RequestMuaNguyenKhi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestMuaNguyenKhi", data);
			return true;
		}

		public bool RequestMuaNguyenKhi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestMuaNguyenKhi(HostID.Server, rmiContext, data);
		}

		public bool RequestCT2PlayerMove(HostID remote, RmiContext rmiContext, float pos_x, float pos_z, float vel_x, float vel_z, long time)
		{
			CJsonTransport.Send("RequestCT2PlayerMove", JsonMapper.ToJson(new object[5] { pos_x, pos_z, vel_x, vel_z, time }));
			return true;
		}

		public bool RequestCT2PlayerMove(HostID[] remotes, RmiContext rmiContext, float pos_x, float pos_z, float vel_x, float vel_z, long time)
		{
			return RequestCT2PlayerMove(HostID.Server, rmiContext, pos_x, pos_z, vel_x, vel_z, time);
		}

		public bool RequestCT2PlayerPos_(HostID remote, RmiContext rmiContext, float pos_x, float pos_z, float vel_x, float vel_z, long time)
		{
			CJsonTransport.Send("RequestCT2PlayerPos_", JsonMapper.ToJson(new object[5] { pos_x, pos_z, vel_x, vel_z, time }));
			return true;
		}

		public bool RequestCT2PlayerPos_(HostID[] remotes, RmiContext rmiContext, float pos_x, float pos_z, float vel_x, float vel_z, long time)
		{
			return RequestCT2PlayerPos_(HostID.Server, rmiContext, pos_x, pos_z, vel_x, vel_z, time);
		}

		public bool RequestBangChienGetInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestBangChienGetInfo", data);
			return true;
		}

		public bool RequestBangChienGetInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestBangChienGetInfo(HostID.Server, rmiContext, data);
		}

		public bool RequestBangChienVaoThanh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestBangChienVaoThanh", data);
			return true;
		}

		public bool RequestBangChienVaoThanh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestBangChienVaoThanh(HostID.Server, rmiContext, data);
		}

		public bool RequestBangChienRoiThanh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestBangChienRoiThanh", data);
			return true;
		}

		public bool RequestBangChienRoiThanh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestBangChienRoiThanh(HostID.Server, rmiContext, data);
		}

		public bool RequestBangChienDenCongThanh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestBangChienDenCongThanh", data);
			return true;
		}

		public bool RequestBangChienDenCongThanh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestBangChienDenCongThanh(HostID.Server, rmiContext, data);
		}

		public bool RequestBangChienCongThanh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestBangChienCongThanh", data);
			return true;
		}

		public bool RequestBangChienCongThanh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestBangChienCongThanh(HostID.Server, rmiContext, data);
		}

		public bool RequestChonHatGiong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestChonHatGiong", data);
			return true;
		}

		public bool RequestChonHatGiong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestChonHatGiong(HostID.Server, rmiContext, data);
		}

		public bool RequestLayHatGiong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestLayHatGiong", data);
			return true;
		}

		public bool RequestLayHatGiong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestLayHatGiong(HostID.Server, rmiContext, data);
		}

		public bool RequestTrongCay(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestTrongCay", data);
			return true;
		}

		public bool RequestTrongCay(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestTrongCay(HostID.Server, rmiContext, data);
		}

		public bool RequestThuHoach(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestThuHoach", data);
			return true;
		}

		public bool RequestThuHoach(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestThuHoach(HostID.Server, rmiContext, data);
		}

		public bool RequestAnTrom(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestAnTrom", data);
			return true;
		}

		public bool RequestAnTrom(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestAnTrom(HostID.Server, rmiContext, data);
		}

		public bool RequestGetAnTromList(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetAnTromList", data);
			return true;
		}

		public bool RequestGetAnTromList(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetAnTromList(HostID.Server, rmiContext, data);
		}

		public bool RequestBangChienUserMove(HostID remote, RmiContext rmiContext, int thanhIdx, float x, float y, float z)
		{
			CJsonTransport.Send("RequestBangChienUserMove", JsonMapper.ToJson(new object[4] { thanhIdx, x, y, z }));
			return true;
		}

		public bool RequestBangChienUserMove(HostID[] remotes, RmiContext rmiContext, int thanhIdx, float x, float y, float z)
		{
			return RequestBangChienUserMove(HostID.Server, rmiContext, thanhIdx, x, y, z);
		}

		public bool RequestBangChienOtherUser(HostID remote, RmiContext rmiContext, int thanhIdx)
		{
			CJsonTransport.Send("RequestBangChienOtherUser", JsonMapper.ToJson(new object[1] { thanhIdx }));
			return true;
		}

		public bool RequestBangChienOtherUser(HostID[] remotes, RmiContext rmiContext, int thanhIdx)
		{
			return RequestBangChienOtherUser(HostID.Server, rmiContext, thanhIdx);
		}

		public bool RequestBangChienGetPhanThuongThuThanh(HostID remote, RmiContext rmiContext, int thanhIdx)
		{
			CJsonTransport.Send("RequestBangChienGetPhanThuongThuThanh", JsonMapper.ToJson(new object[1] { thanhIdx }));
			return true;
		}

		public bool RequestBangChienGetPhanThuongThuThanh(HostID[] remotes, RmiContext rmiContext, int thanhIdx)
		{
			return RequestBangChienGetPhanThuongThuThanh(HostID.Server, rmiContext, thanhIdx);
		}

		public bool RequestSendChatLienSrv(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSendChatLienSrv", data);
			return true;
		}

		public bool RequestSendChatLienSrv(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSendChatLienSrv(HostID.Server, rmiContext, data);
		}

		public bool RequestGetChatLienSrv(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetChatLienSrv", data);
			return true;
		}

		public bool RequestGetChatLienSrv(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetChatLienSrv(HostID.Server, rmiContext, data);
		}

		public bool RequestGetLinhDuocInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetLinhDuocInfo", data);
			return true;
		}

		public bool RequestGetLinhDuocInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetLinhDuocInfo(HostID.Server, rmiContext, data);
		}

		public bool RequestBanhChungPlayerMove(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestBanhChungPlayerMove", data);
			return true;
		}

		public bool RequestBanhChungPlayerMove(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestBanhChungPlayerMove(HostID.Server, rmiContext, data);
		}

		public bool RequestBanhChungGetInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestBanhChungGetInfo", data);
			return true;
		}

		public bool RequestBanhChungGetInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestBanhChungGetInfo(HostID.Server, rmiContext, data);
		}

		public bool RequestBanhChungNauBanh(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestBanhChungNauBanh", data);
			return true;
		}

		public bool RequestBanhChungNauBanh(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestBanhChungNauBanh(HostID.Server, rmiContext, data);
		}

		public bool RequestBanhChungGetOthers(HostID remote, RmiContext rmiContext)
		{
			CJsonTransport.Send("RequestBanhChungGetOthers", "");
			return true;
		}

		public bool RequestBanhChungGetOthers(HostID[] remotes, RmiContext rmiContext)
		{
			return RequestBanhChungGetOthers(HostID.Server, rmiContext);
		}

		public bool RequestBanhChungNhatNguyenLieu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestBanhChungNhatNguyenLieu", data);
			return true;
		}

		public bool RequestBanhChungNhatNguyenLieu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestBanhChungNhatNguyenLieu(HostID.Server, rmiContext, data);
		}

		public bool RequestBanhChungBXH(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestBanhChungBXH", data);
			return true;
		}

		public bool RequestBanhChungBXH(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestBanhChungBXH(HostID.Server, rmiContext, data);
		}

		public bool RequestBanhChungGetPhanThuong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestBanhChungGetPhanThuong", data);
			return true;
		}

		public bool RequestBanhChungGetPhanThuong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestBanhChungGetPhanThuong(HostID.Server, rmiContext, data);
		}

		public bool RequestDangNhapNhanThuongTet(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestDangNhapNhanThuongTet", data);
			return true;
		}

		public bool RequestDangNhapNhanThuongTet(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestDangNhapNhanThuongTet(HostID.Server, rmiContext, data);
		}

		public bool RequestThaoNgua(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestThaoNgua", data);
			return true;
		}

		public bool RequestThaoNgua(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestThaoNgua(HostID.Server, rmiContext, data);
		}

		public bool RequestDungNgua(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestDungNgua", data);
			return true;
		}

		public bool RequestDungNgua(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestDungNgua(HostID.Server, rmiContext, data);
		}

		public bool RequestActiveNgua(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestActiveNgua", data);
			return true;
		}

		public bool RequestActiveNgua(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestActiveNgua(HostID.Server, rmiContext, data);
		}

		public bool RequestGetGamerLinhDuoc(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetGamerLinhDuoc", data);
			return true;
		}

		public bool RequestGetGamerLinhDuoc(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetGamerLinhDuoc(HostID.Server, rmiContext, data);
		}

		public bool RequestCuongHoaBatQuaiTran(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestCuongHoaBatQuaiTran", data);
			return true;
		}

		public bool RequestCuongHoaBatQuaiTran(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestCuongHoaBatQuaiTran(HostID.Server, rmiContext, data);
		}

		public bool RequestSetSoDoBatQuaiTran(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSetSoDoBatQuaiTran", data);
			return true;
		}

		public bool RequestSetSoDoBatQuaiTran(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSetSoDoBatQuaiTran(HostID.Server, rmiContext, data);
		}

		public bool RequestGetLeagueData(HostID remote, RmiContext rmiContext)
		{
			CJsonTransport.Send("RequestGetLeagueData", "");
			return true;
		}

		public bool RequestGetLeagueData(HostID[] remotes, RmiContext rmiContext)
		{
			return RequestGetLeagueData(HostID.Server, rmiContext);
		}

		public bool RequestSetDoiHinhThienCangTran(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSetDoiHinhThienCangTran", data);
			return true;
		}

		public bool RequestSetDoiHinhThienCangTran(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSetDoiHinhThienCangTran(HostID.Server, rmiContext, data);
		}

		public bool RequestOpenDoiHinhThienCangTran(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestOpenDoiHinhThienCangTran", data);
			return true;
		}

		public bool RequestOpenDoiHinhThienCangTran(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestOpenDoiHinhThienCangTran(HostID.Server, rmiContext, data);
		}

		public bool RequestGetSieuCupData(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetSieuCupData", data);
			return true;
		}

		public bool RequestGetSieuCupData(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetSieuCupData(HostID.Server, rmiContext, data);
		}

		public bool RequestGetSieuCupBattle(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetSieuCupBattle", data);
			return true;
		}

		public bool RequestGetSieuCupBattle(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetSieuCupBattle(HostID.Server, rmiContext, data);
		}

		public bool RequestSieuCupDatCuoc(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSieuCupDatCuoc", data);
			return true;
		}

		public bool RequestSieuCupDatCuoc(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSieuCupDatCuoc(HostID.Server, rmiContext, data);
		}

		public bool RequestSubmitDoiHinhLeague(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSubmitDoiHinhLeague", data);
			return true;
		}

		public bool RequestSubmitDoiHinhLeague(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSubmitDoiHinhLeague(HostID.Server, rmiContext, data);
		}

		public bool RequestViewLeagueReplay(HostID remote, RmiContext rmiContext, int id)
		{
			CJsonTransport.Send("RequestViewLeagueReplay", JsonMapper.ToJson(new object[1] { id }));
			return true;
		}

		public bool RequestViewLeagueReplay(HostID[] remotes, RmiContext rmiContext, int id)
		{
			return RequestViewLeagueReplay(HostID.Server, rmiContext, id);
		}

		public bool RequestThamBaiSieuCup(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestThamBaiSieuCup", data);
			return true;
		}

		public bool RequestThamBaiSieuCup(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestThamBaiSieuCup(HostID.Server, rmiContext, data);
		}

		public bool RequestVongQuay(HostID remote, RmiContext rmiContext, int id)
		{
			CJsonTransport.Send("RequestVongQuay", JsonMapper.ToJson(new object[1] { id }));
			return true;
		}

		public bool RequestVongQuay(HostID[] remotes, RmiContext rmiContext, int id)
		{
			return RequestVongQuay(HostID.Server, rmiContext, id);
		}

		public bool RequestLienDauData(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestLienDauData", data);
			return true;
		}

		public bool RequestLienDauData(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestLienDauData(HostID.Server, rmiContext, data);
		}

		public bool RequestSieuCupChampion(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSieuCupChampion", data);
			return true;
		}

		public bool RequestSieuCupChampion(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSieuCupChampion(HostID.Server, rmiContext, data);
		}

		public bool RequestQMDInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestQMDInfo", data);
			return true;
		}

		public bool RequestQMDInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestQMDInfo(HostID.Server, rmiContext, data);
		}

		public bool RequestQMDSelect(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestQMDSelect", data);
			return true;
		}

		public bool RequestQMDSelect(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestQMDSelect(HostID.Server, rmiContext, data);
		}

		public bool RequestQMDGetChiTietNPC(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestQMDGetChiTietNPC", data);
			return true;
		}

		public bool RequestQMDGetChiTietNPC(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestQMDGetChiTietNPC(HostID.Server, rmiContext, data);
		}

		public bool RequestQMDGetBXH(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestQMDGetBXH", data);
			return true;
		}

		public bool RequestQMDGetBXH(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestQMDGetBXH(HostID.Server, rmiContext, data);
		}

		public bool RequestQMDXongPha(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestQMDXongPha", data);
			return true;
		}

		public bool RequestQMDXongPha(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestQMDXongPha(HostID.Server, rmiContext, data);
		}

		public bool RequestBeQuanDeTu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestBeQuanDeTu", data);
			return true;
		}

		public bool RequestBeQuanDeTu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestBeQuanDeTu(HostID.Server, rmiContext, data);
		}

		public bool RequestQMDTranHinhChienThuat(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestQMDTranHinhChienThuat", data);
			return true;
		}

		public bool RequestQMDTranHinhChienThuat(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestQMDTranHinhChienThuat(HostID.Server, rmiContext, data);
		}

		public bool RequestNhanThuongTichLuyNap(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestNhanThuongTichLuyNap", data);
			return true;
		}

		public bool RequestNhanThuongTichLuyNap(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestNhanThuongTichLuyNap(HostID.Server, rmiContext, data);
		}

		public bool RequestNhanThuongTichLuyTieu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestNhanThuongTichLuyTieu", data);
			return true;
		}

		public bool RequestNhanThuongTichLuyTieu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestNhanThuongTichLuyTieu(HostID.Server, rmiContext, data);
		}

		public bool RequestGetCacLoaiTop(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetCacLoaiTop", data);
			return true;
		}

		public bool RequestGetCacLoaiTop(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetCacLoaiTop(HostID.Server, rmiContext, data);
		}

		public bool RequestDoiTenBang(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestDoiTenBang", data);
			return true;
		}

		public bool RequestDoiTenBang(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestDoiTenBang(HostID.Server, rmiContext, data);
		}

		public bool RequestGetThongTinLienServer(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetThongTinLienServer", data);
			return true;
		}

		public bool RequestGetThongTinLienServer(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetThongTinLienServer(HostID.Server, rmiContext, data);
		}

		public bool RequestGetTopPhaoHoa(HostID remote, RmiContext rmiContext)
		{
			CJsonTransport.Send("RequestGetTopPhaoHoa", "");
			return true;
		}

		public bool RequestGetTopPhaoHoa(HostID[] remotes, RmiContext rmiContext)
		{
			return RequestGetTopPhaoHoa(HostID.Server, rmiContext);
		}

		public bool RequestGetPhanThuongPhaoHoa(HostID remote, RmiContext rmiContext)
		{
			CJsonTransport.Send("RequestGetPhanThuongPhaoHoa", "");
			return true;
		}

		public bool RequestGetPhanThuongPhaoHoa(HostID[] remotes, RmiContext rmiContext)
		{
			return RequestGetPhanThuongPhaoHoa(HostID.Server, rmiContext);
		}

		public bool RequestBanPhaoHoaEvent(HostID remote, RmiContext rmiContext)
		{
			CJsonTransport.Send("RequestBanPhaoHoaEvent", "");
			return true;
		}

		public bool RequestBanPhaoHoaEvent(HostID[] remotes, RmiContext rmiContext)
		{
			return RequestBanPhaoHoaEvent(HostID.Server, rmiContext);
		}

		public bool RequestGetTopVongQuay(HostID remote, RmiContext rmiContext)
		{
			CJsonTransport.Send("RequestGetTopVongQuay", "");
			return true;
		}

		public bool RequestGetTopVongQuay(HostID[] remotes, RmiContext rmiContext)
		{
			return RequestGetTopVongQuay(HostID.Server, rmiContext);
		}

		public bool RequestCreateCostume(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestCreateCostume", data);
			return true;
		}

		public bool RequestCreateCostume(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestCreateCostume(HostID.Server, rmiContext, data);
		}

		public bool RequestTinhLuyenCostume(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestTinhLuyenCostume", data);
			return true;
		}

		public bool RequestTinhLuyenCostume(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestTinhLuyenCostume(HostID.Server, rmiContext, data);
		}

		public bool RequestKhamNgocCostume(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestKhamNgocCostume", data);
			return true;
		}

		public bool RequestKhamNgocCostume(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestKhamNgocCostume(HostID.Server, rmiContext, data);
		}

		public bool RequestGoNgocCostume(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGoNgocCostume", data);
			return true;
		}

		public bool RequestGoNgocCostume(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGoNgocCostume(HostID.Server, rmiContext, data);
		}

		public bool RequestTakeOnCostume(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestTakeOnCostume", data);
			return true;
		}

		public bool RequestTakeOnCostume(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestTakeOnCostume(HostID.Server, rmiContext, data);
		}

		public bool RequestTakeOffCostume(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestTakeOffCostume", data);
			return true;
		}

		public bool RequestTakeOffCostume(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestTakeOffCostume(HostID.Server, rmiContext, data);
		}

		public bool RequestBanPhaoHoa(HostID remote, RmiContext rmiContext)
		{
			CJsonTransport.Send("RequestBanPhaoHoa", "");
			return true;
		}

		public bool RequestBanPhaoHoa(HostID[] remotes, RmiContext rmiContext)
		{
			return RequestBanPhaoHoa(HostID.Server, rmiContext);
		}

		public bool RequestLinhThuongPhaoHoaEvent(HostID remote, RmiContext rmiContext, int count)
		{
			CJsonTransport.Send("RequestLinhThuongPhaoHoaEvent", JsonMapper.ToJson(new object[1] { count }));
			return true;
		}

		public bool RequestLinhThuongPhaoHoaEvent(HostID[] remotes, RmiContext rmiContext, int count)
		{
			return RequestLinhThuongPhaoHoaEvent(HostID.Server, rmiContext, count);
		}

		public bool RequestChuyenSinhDeTu(HostID remote, RmiContext rmiContext, int heroId)
		{
			CJsonTransport.Send("RequestChuyenSinhDeTu", JsonMapper.ToJson(new object[1] { heroId }));
			return true;
		}

		public bool RequestChuyenSinhDeTu(HostID[] remotes, RmiContext rmiContext, int heroId)
		{
			return RequestChuyenSinhDeTu(HostID.Server, rmiContext, heroId);
		}

		public bool RequestGetListOtherUser(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetListOtherUser", data);
			return true;
		}

		public bool RequestGetListOtherUser(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetListOtherUser(HostID.Server, rmiContext, data);
		}

		public bool RequestBatThanThu(HostID remote, RmiContext rmiContext)
		{
			CJsonTransport.Send("RequestBatThanThu", "");
			return true;
		}

		public bool RequestBatThanThu(HostID[] remotes, RmiContext rmiContext)
		{
			return RequestBatThanThu(HostID.Server, rmiContext);
		}

		public bool RequestNhanThuongDapNieu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestNhanThuongDapNieu", data);
			return true;
		}

		public bool RequestNhanThuongDapNieu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestNhanThuongDapNieu(HostID.Server, rmiContext, data);
		}

		public bool RequestTruongThanhThanThu(HostID remote, RmiContext rmiContext, int thanthuId)
		{
			CJsonTransport.Send("RequestTruongThanhThanThu", JsonMapper.ToJson(new object[1] { thanthuId }));
			return true;
		}

		public bool RequestTruongThanhThanThu(HostID[] remotes, RmiContext rmiContext, int thanthuId)
		{
			return RequestTruongThanhThanThu(HostID.Server, rmiContext, thanthuId);
		}

		public bool RequestNangPhamThanThu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestNangPhamThanThu", data);
			return true;
		}

		public bool RequestNangPhamThanThu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestNangPhamThanThu(HostID.Server, rmiContext, data);
		}

		public bool RequestDoiThanThu(HostID remote, RmiContext rmiContext, int thanthuId)
		{
			CJsonTransport.Send("RequestDoiThanThu", JsonMapper.ToJson(new object[1] { thanthuId }));
			return true;
		}

		public bool RequestDoiThanThu(HostID[] remotes, RmiContext rmiContext, int thanthuId)
		{
			return RequestDoiThanThu(HostID.Server, rmiContext, thanthuId);
		}

		public bool RequestThonPheThanThu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestThonPheThanThu", data);
			return true;
		}

		public bool RequestThonPheThanThu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestThonPheThanThu(HostID.Server, rmiContext, data);
		}

		public bool RequestTruyenCongThanThu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestTruyenCongThanThu", data);
			return true;
		}

		public bool RequestTruyenCongThanThu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestTruyenCongThanThu(HostID.Server, rmiContext, data);
		}

		public bool RequestSetBoPhapNhanVatBatQuai(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSetBoPhapNhanVatBatQuai", data);
			return true;
		}

		public bool RequestSetBoPhapNhanVatBatQuai(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSetBoPhapNhanVatBatQuai(HostID.Server, rmiContext, data);
		}

		public bool RequestSetNoiCongNhanVatBatQuai(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSetNoiCongNhanVatBatQuai", data);
			return true;
		}

		public bool RequestSetNoiCongNhanVatBatQuai(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSetNoiCongNhanVatBatQuai(HostID.Server, rmiContext, data);
		}

		public bool RequestSetTrangBiNhanVatBatQuai(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSetTrangBiNhanVatBatQuai", data);
			return true;
		}

		public bool RequestSetTrangBiNhanVatBatQuai(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSetTrangBiNhanVatBatQuai(HostID.Server, rmiContext, data);
		}

		public bool RequestGetThanThuDao(HostID remote, RmiContext rmiContext)
		{
			CJsonTransport.Send("RequestGetThanThuDao", "");
			return true;
		}

		public bool RequestGetThanThuDao(HostID[] remotes, RmiContext rmiContext)
		{
			return RequestGetThanThuDao(HostID.Server, rmiContext);
		}

		public bool RequestAutoResolveThanThuDao(HostID remote, RmiContext rmiContext)
		{
			CJsonTransport.Send("RequestAutoResolveThanThuDao", "");
			return true;
		}

		public bool RequestAutoResolveThanThuDao(HostID[] remotes, RmiContext rmiContext)
		{
			return RequestAutoResolveThanThuDao(HostID.Server, rmiContext);
		}

		public bool RequestSetThanThu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSetThanThu", data);
			return true;
		}

		public bool RequestSetThanThu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSetThanThu(HostID.Server, rmiContext, data);
		}

		public bool RequestGetGuiTietKiem(HostID remote, RmiContext rmiContext, int id)
		{
			CJsonTransport.Send("RequestGetGuiTietKiem", JsonMapper.ToJson(new object[1] { id }));
			return true;
		}

		public bool RequestGetGuiTietKiem(HostID[] remotes, RmiContext rmiContext, int id)
		{
			return RequestGetGuiTietKiem(HostID.Server, rmiContext, id);
		}

		public bool RequestThamGiaGuiTietKiem(HostID remote, RmiContext rmiContext)
		{
			CJsonTransport.Send("RequestThamGiaGuiTietKiem", "");
			return true;
		}

		public bool RequestThamGiaGuiTietKiem(HostID[] remotes, RmiContext rmiContext)
		{
			return RequestThamGiaGuiTietKiem(HostID.Server, rmiContext);
		}

		public bool RequestGetThuong1MilUser(HostID remote, RmiContext rmiContext)
		{
			CJsonTransport.Send("RequestGetThuong1MilUser", "");
			return true;
		}

		public bool RequestGetThuong1MilUser(HostID[] remotes, RmiContext rmiContext)
		{
			return RequestGetThuong1MilUser(HostID.Server, rmiContext);
		}

		public bool RequestUseRuongThanBi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestUseRuongThanBi", data);
			return true;
		}

		public bool RequestUseRuongThanBi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestUseRuongThanBi(HostID.Server, rmiContext, data);
		}

		public bool RequestThuHoachSonMon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestThuHoachSonMon", data);
			return true;
		}

		public bool RequestThuHoachSonMon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestThuHoachSonMon(HostID.Server, rmiContext, data);
		}

		public bool RequestXayDungSonMon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestXayDungSonMon", data);
			return true;
		}

		public bool RequestXayDungSonMon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestXayDungSonMon(HostID.Server, rmiContext, data);
		}

		public bool RequestTanCongSonMon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestTanCongSonMon", data);
			return true;
		}

		public bool RequestTanCongSonMon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestTanCongSonMon(HostID.Server, rmiContext, data);
		}

		public bool RequestDoiHinhSonMon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestDoiHinhSonMon", data);
			return true;
		}

		public bool RequestDoiHinhSonMon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestDoiHinhSonMon(HostID.Server, rmiContext, data);
		}

		public bool RequestDoThamSonMon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestDoThamSonMon", data);
			return true;
		}

		public bool RequestDoThamSonMon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestDoThamSonMon(HostID.Server, rmiContext, data);
		}

		public bool RequestMuaDoThanBi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestMuaDoThanBi", data);
			return true;
		}

		public bool RequestMuaDoThanBi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestMuaDoThanBi(HostID.Server, rmiContext, data);
		}

		public bool RequestGetTopSonMon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetTopSonMon", data);
			return true;
		}

		public bool RequestGetTopSonMon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetTopSonMon(HostID.Server, rmiContext, data);
		}

		public bool RequestDungLuyenTrangBi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestDungLuyenTrangBi", data);
			return true;
		}

		public bool RequestDungLuyenTrangBi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestDungLuyenTrangBi(HostID.Server, rmiContext, data);
		}

		public bool RequestTayLuyenTrangBi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestTayLuyenTrangBi", data);
			return true;
		}

		public bool RequestTayLuyenTrangBi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestTayLuyenTrangBi(HostID.Server, rmiContext, data);
		}

		public bool RequestKhaiQuangTrangBi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestKhaiQuangTrangBi", data);
			return true;
		}

		public bool RequestKhaiQuangTrangBi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestKhaiQuangTrangBi(HostID.Server, rmiContext, data);
		}

		public bool RequestConfirmTayLuyenTrangBi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestConfirmTayLuyenTrangBi", data);
			return true;
		}

		public bool RequestConfirmTayLuyenTrangBi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestConfirmTayLuyenTrangBi(HostID.Server, rmiContext, data);
		}

		public bool RequestGetSonMonInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetSonMonInfo", data);
			return true;
		}

		public bool RequestGetSonMonInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetSonMonInfo(HostID.Server, rmiContext, data);
		}

		public bool RequestGetLanhDiaInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetLanhDiaInfo", data);
			return true;
		}

		public bool RequestGetLanhDiaInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetLanhDiaInfo(HostID.Server, rmiContext, data);
		}

		public bool RequestUnLockVoCong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestUnLockVoCong", data);
			return true;
		}

		public bool RequestUnLockVoCong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestUnLockVoCong(HostID.Server, rmiContext, data);
		}

		public bool RequestQuayBacMayMan(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestQuayBacMayMan", data);
			return true;
		}

		public bool RequestQuayBacMayMan(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestQuayBacMayMan(HostID.Server, rmiContext, data);
		}

		public bool RequestMoveLanhDia(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestMoveLanhDia", data);
			return true;
		}

		public bool RequestMoveLanhDia(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestMoveLanhDia(HostID.Server, rmiContext, data);
		}

		public bool RequestQuayTuBaoBon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestQuayTuBaoBon", data);
			return true;
		}

		public bool RequestQuayTuBaoBon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestQuayTuBaoBon(HostID.Server, rmiContext, data);
		}

		public bool RequestGetTopMoRuong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetTopMoRuong", data);
			return true;
		}

		public bool RequestGetTopMoRuong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetTopMoRuong(HostID.Server, rmiContext, data);
		}

		public bool RequestQuayThienMaHaPhong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestQuayThienMaHaPhong", data);
			return true;
		}

		public bool RequestQuayThienMaHaPhong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestQuayThienMaHaPhong(HostID.Server, rmiContext, data);
		}

		public bool RequestGetTayVucInfo(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetTayVucInfo", data);
			return true;
		}

		public bool RequestGetTayVucInfo(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetTayVucInfo(HostID.Server, rmiContext, data);
		}

		public bool RequestMuaDoTayVuc(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestMuaDoTayVuc", data);
			return true;
		}

		public bool RequestMuaDoTayVuc(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestMuaDoTayVuc(HostID.Server, rmiContext, data);
		}

		public bool RequestSummonNienThu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSummonNienThu", data);
			return true;
		}

		public bool RequestSummonNienThu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSummonNienThu(HostID.Server, rmiContext, data);
		}

		public bool RequestDanhNienThu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestDanhNienThu", data);
			return true;
		}

		public bool RequestDanhNienThu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestDanhNienThu(HostID.Server, rmiContext, data);
		}

		public bool RequestThamGiaNienThu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestThamGiaNienThu", data);
			return true;
		}

		public bool RequestThamGiaNienThu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestThamGiaNienThu(HostID.Server, rmiContext, data);
		}

		public bool RequestNhanThuongNapHangNgay(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestNhanThuongNapHangNgay", data);
			return true;
		}

		public bool RequestNhanThuongNapHangNgay(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestNhanThuongNapHangNgay(HostID.Server, rmiContext, data);
		}

		public bool RequestGetTopNienThu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetTopNienThu", data);
			return true;
		}

		public bool RequestGetTopNienThu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetTopNienThu(HostID.Server, rmiContext, data);
		}

		public bool RequestNopLenhBaiNienThu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestNopLenhBaiNienThu", data);
			return true;
		}

		public bool RequestNopLenhBaiNienThu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestNopLenhBaiNienThu(HostID.Server, rmiContext, data);
		}

		public bool RequestGetTopLanhDia(HostID remote, RmiContext rmiContext)
		{
			CJsonTransport.Send("RequestGetTopLanhDia", "");
			return true;
		}

		public bool RequestGetTopLanhDia(HostID[] remotes, RmiContext rmiContext)
		{
			return RequestGetTopLanhDia(HostID.Server, rmiContext);
		}

		public bool RequestSelectStartNgua(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSelectStartNgua", data);
			return true;
		}

		public bool RequestSelectStartNgua(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSelectStartNgua(HostID.Server, rmiContext, data);
		}

		public bool RequestGetDiemMoRuong(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestGetDiemMoRuong", data);
			return true;
		}

		public bool RequestGetDiemMoRuong(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestGetDiemMoRuong(HostID.Server, rmiContext, data);
		}

		public bool RequestUpdateDailyActivities(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestUpdateDailyActivities", data);
			return true;
		}

		public bool RequestUpdateDailyActivities(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestUpdateDailyActivities(HostID.Server, rmiContext, data);
		}

		public bool RequestThuongDailyActivities(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestThuongDailyActivities", data);
			return true;
		}

		public bool RequestThuongDailyActivities(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestThuongDailyActivities(HostID.Server, rmiContext, data);
		}

		public bool RequestThienMaQuaySlot(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestThienMaQuaySlot", data);
			return true;
		}

		public bool RequestThienMaQuaySlot(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestThienMaQuaySlot(HostID.Server, rmiContext, data);
		}

		public bool RequestThienMaLenhCreate(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestThienMaLenhCreate", data);
			return true;
		}

		public bool RequestThienMaLenhCreate(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestThienMaLenhCreate(HostID.Server, rmiContext, data);
		}

		public bool RequestThienMaUpgradeSlot(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestThienMaUpgradeSlot", data);
			return true;
		}

		public bool RequestThienMaUpgradeSlot(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestThienMaUpgradeSlot(HostID.Server, rmiContext, data);
		}

		public bool RequestThienMaOpenSlot(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestThienMaOpenSlot", data);
			return true;
		}

		public bool RequestThienMaOpenSlot(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestThienMaOpenSlot(HostID.Server, rmiContext, data);
		}

		public bool RequestThienMaEquip(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestThienMaEquip", data);
			return true;
		}

		public bool RequestThienMaEquip(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestThienMaEquip(HostID.Server, rmiContext, data);
		}

		public bool RequestThienMaQuaySlotConfirm(HostID remote, RmiContext rmiContext, bool confirm)
		{
			CJsonTransport.Send("RequestThienMaQuaySlotConfirm", JsonMapper.ToJson(new object[1] { confirm }));
			return true;
		}

		public bool RequestThienMaQuaySlotConfirm(HostID[] remotes, RmiContext rmiContext, bool confirm)
		{
			return RequestThienMaQuaySlotConfirm(HostID.Server, rmiContext, confirm);
		}

		public bool RequestSetTonHieu(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSetTonHieu", data);
			return true;
		}

		public bool RequestSetTonHieu(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSetTonHieu(HostID.Server, rmiContext, data);
		}

		public bool RequestSetTrangBiHoangKim(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestSetTrangBiHoangKim", data);
			return true;
		}

		public bool RequestSetTrangBiHoangKim(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestSetTrangBiHoangKim(HostID.Server, rmiContext, data);
		}

		public bool RequestGetTopDaiHoiVoLam(HostID remote, RmiContext rmiContext)
		{
			CJsonTransport.Send("RequestGetTopDaiHoiVoLam", "");
			return true;
		}

		public bool RequestGetTopDaiHoiVoLam(HostID[] remotes, RmiContext rmiContext)
		{
			return RequestGetTopDaiHoiVoLam(HostID.Server, rmiContext);
		}

		public bool RequestDanhAnDanhCaoThu(HostID remote, RmiContext rmiContext)
		{
			CJsonTransport.Send("RequestDanhAnDanhCaoThu", "");
			return true;
		}

		public bool RequestDanhAnDanhCaoThu(HostID[] remotes, RmiContext rmiContext)
		{
			return RequestDanhAnDanhCaoThu(HostID.Server, rmiContext);
		}

		public bool RequestStartBoiDuongTrangBi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestStartBoiDuongTrangBi", data);
			return true;
		}

		public bool RequestStartBoiDuongTrangBi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestStartBoiDuongTrangBi(HostID.Server, rmiContext, data);
		}

		public bool RequestEndBoiDuongTrangBi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestEndBoiDuongTrangBi", data);
			return true;
		}

		public bool RequestEndBoiDuongTrangBi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestEndBoiDuongTrangBi(HostID.Server, rmiContext, data);
		}

		public bool RequestRutQueTienNhan(HostID remote, RmiContext rmiContext)
		{
			CJsonTransport.Send("RequestRutQueTienNhan", "");
			return true;
		}

		public bool RequestRutQueTienNhan(HostID[] remotes, RmiContext rmiContext)
		{
			return RequestRutQueTienNhan(HostID.Server, rmiContext);
		}

		public bool RequestGetBaoKhoInfo(HostID remote, RmiContext rmiContext)
		{
			CJsonTransport.Send("RequestGetBaoKhoInfo", "");
			return true;
		}

		public bool RequestGetBaoKhoInfo(HostID[] remotes, RmiContext rmiContext)
		{
			return RequestGetBaoKhoInfo(HostID.Server, rmiContext);
		}

		public bool RequestCuopBaoKho(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestCuopBaoKho", data);
			return true;
		}

		public bool RequestCuopBaoKho(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestCuopBaoKho(HostID.Server, rmiContext, data);
		}

		public bool RequestThuHoachBaoKho(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestThuHoachBaoKho", data);
			return true;
		}

		public bool RequestThuHoachBaoKho(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestThuHoachBaoKho(HostID.Server, rmiContext, data);
		}

		public bool RequestGetListBaoKho(HostID remote, RmiContext rmiContext)
		{
			CJsonTransport.Send("RequestGetListBaoKho", "");
			return true;
		}

		public bool RequestGetListBaoKho(HostID[] remotes, RmiContext rmiContext)
		{
			return RequestGetListBaoKho(HostID.Server, rmiContext);
		}

		public bool RequestQuayCamCungBiBao(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestQuayCamCungBiBao", data);
			return true;
		}

		public bool RequestQuayCamCungBiBao(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestQuayCamCungBiBao(HostID.Server, rmiContext, data);
		}

		public bool RequestNhanThuongCamCung(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestNhanThuongCamCung", data);
			return true;
		}

		public bool RequestNhanThuongCamCung(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestNhanThuongCamCung(HostID.Server, rmiContext, data);
		}

		public bool RequestGetHoaVangInfo(HostID remote, RmiContext rmiContext)
		{
			CJsonTransport.Send("RequestGetHoaVangInfo", "");
			return true;
		}

		public bool RequestGetHoaVangInfo(HostID[] remotes, RmiContext rmiContext)
		{
			return RequestGetHoaVangInfo(HostID.Server, rmiContext);
		}

		public bool RequestHoaVang(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestHoaVang", data);
			return true;
		}

		public bool RequestHoaVang(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestHoaVang(HostID.Server, rmiContext, data);
		}

		public bool RequestTestProudNet(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestTestProudNet", data);
			return true;
		}

		public bool RequestTestProudNet(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestTestProudNet(HostID.Server, rmiContext, data);
		}

		public bool RequestTrangBiHuyenKhi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestTrangBiHuyenKhi", data);
			return true;
		}

		public bool RequestTrangBiHuyenKhi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestTrangBiHuyenKhi(HostID.Server, rmiContext, data);
		}

		public bool RequestThangCapHuyenKhi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestThangCapHuyenKhi", data);
			return true;
		}

		public bool RequestThangCapHuyenKhi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestThangCapHuyenKhi(HostID.Server, rmiContext, data);
		}

		public bool RequestCheTaoHuyenKhi(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestCheTaoHuyenKhi", data);
			return true;
		}

		public bool RequestCheTaoHuyenKhi(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestCheTaoHuyenKhi(HostID.Server, rmiContext, data);
		}

		public bool RequestDotPhaChienHon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestDotPhaChienHon", data);
			return true;
		}

		public bool RequestDotPhaChienHon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestDotPhaChienHon(HostID.Server, rmiContext, data);
		}

		public bool RequestUseTanHonTangLevelChienHon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestUseTanHonTangLevelChienHon", data);
			return true;
		}

		public bool RequestUseTanHonTangLevelChienHon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestUseTanHonTangLevelChienHon(HostID.Server, rmiContext, data);
		}

		public bool RequestTrieuHoiChienHon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestTrieuHoiChienHon", data);
			return true;
		}

		public bool RequestTrieuHoiChienHon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestTrieuHoiChienHon(HostID.Server, rmiContext, data);
		}

		public bool RequestEndBoiDuongChienHon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestEndBoiDuongChienHon", data);
			return true;
		}

		public bool RequestEndBoiDuongChienHon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestEndBoiDuongChienHon(HostID.Server, rmiContext, data);
		}

		public bool RequestStartBoiDuongChienHon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestStartBoiDuongChienHon", data);
			return true;
		}

		public bool RequestStartBoiDuongChienHon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestStartBoiDuongChienHon(HostID.Server, rmiContext, data);
		}

		public bool RequestChienHonChangeBuff(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestChienHonChangeBuff", data);
			return true;
		}

		public bool RequestChienHonChangeBuff(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestChienHonChangeBuff(HostID.Server, rmiContext, data);
		}

		public bool RequestQuayDiemHoaVang(HostID remote, RmiContext rmiContext)
		{
			CJsonTransport.Send("RequestQuayDiemHoaVang", "");
			return true;
		}

		public bool RequestQuayDiemHoaVang(HostID[] remotes, RmiContext rmiContext)
		{
			return RequestQuayDiemHoaVang(HostID.Server, rmiContext);
		}

		public bool RequestUseTuiThan(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestUseTuiThan", data);
			return true;
		}

		public bool RequestUseTuiThan(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestUseTuiThan(HostID.Server, rmiContext, data);
		}

		public bool RequestNhanThuongDailyKNB(HostID remote, RmiContext rmiContext)
		{
			CJsonTransport.Send("RequestNhanThuongDailyKNB", "");
			return true;
		}

		public bool RequestNhanThuongDailyKNB(HostID[] remotes, RmiContext rmiContext)
		{
			return RequestNhanThuongDailyKNB(HostID.Server, rmiContext);
		}
	}
}
