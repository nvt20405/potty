using UnityEngine;

public class MenuButtonKyNgo : MonoBehaviour
{
	public enum KyNgoType
	{
		TheLuc = 0,
		ThamBai = 1,
		BeQuan = 2,
		LongMach = 3,
		OanTuTi = 4,
		HuaNguyen = 5,
		ThangCap = 6,
		DangNhapNhanThuong = 7,
		ThanTai = 8,
		DanhBac = 9,
		CuuTieuPhong = 10,
		RuongThachSanh = 11,
		DaiLy = 12,
		UongRuou = 13,
		SonTrang = 14,
		DoiDo = 15,
		ThuongNap = 16,
		DiHoaCung = 17,
		BanhChung = 18,
		TichLuyNap = 19,
		TichLuyTieu = 20,
		PhaoHoa = 21,
		DapNieu = 22,
		VongQuay = 23,
		GuiTietKiem = 24,
		BacMayMan = 25,
		TuBaoBon = 26,
		BaoRuong = 27,
		TichLuyNapHangNgay = 28,
		NapLanDau = 29,
		AnTheCaoThu = 30,
		CamCung = 31
	}

	public UISprite bkg;

	public UISprite focus;

	public GameObject notifyIcon;

	public KyNgoType kyNgoID;

	private KyNgoType m_Type;

	public void setID(KyNgoType type, bool isShowNotify = true)
	{
		m_Type = type;
		kyNgoID = type;
		notifyIcon.gameObject.SetActive(false);
		GadgetPanelBottom gadgetPanelBottom = GUIManager.instance.gadgetPanelBottom;
		switch (m_Type)
		{
		case KyNgoType.TheLuc:
			bkg.spriteName = "theluc";
			if (isShowNotify)
			{
				notifyIcon.gameObject.SetActive(gadgetPanelBottom.checkThongBaoTheLuc());
			}
			break;
		case KyNgoType.ThamBai:
			bkg.spriteName = "thambai";
			if (isShowNotify)
			{
				notifyIcon.gameObject.SetActive(gadgetPanelBottom.checkThongBaoThamBai());
			}
			break;
		case KyNgoType.BeQuan:
			bkg.spriteName = "bequan";
			break;
		case KyNgoType.LongMach:
			bkg.spriteName = "longmach";
			break;
		case KyNgoType.OanTuTi:
			bkg.spriteName = "oantuti";
			break;
		case KyNgoType.HuaNguyen:
			bkg.spriteName = "huanguyen";
			if (isShowNotify)
			{
				notifyIcon.gameObject.SetActive(gadgetPanelBottom.checkThongBaoHuaNguyen());
			}
			break;
		case KyNgoType.ThangCap:
			bkg.spriteName = "thangcap";
			if (isShowNotify)
			{
				notifyIcon.gameObject.SetActive(gadgetPanelBottom.checkThongBaoThangCap());
			}
			break;
		case KyNgoType.DangNhapNhanThuong:
			bkg.spriteName = "nhanthuong";
			if (isShowNotify)
			{
				notifyIcon.gameObject.SetActive(gadgetPanelBottom.checkThongBaoDNNhanThuong());
			}
			break;
		case KyNgoType.ThanTai:
			bkg.spriteName = "thantai";
			if (isShowNotify)
			{
				notifyIcon.gameObject.SetActive(gadgetPanelBottom.checkThongBaoThanTai());
			}
			break;
		case KyNgoType.DanhBac:
			bkg.spriteName = "danhbac";
			break;
		case KyNgoType.CuuTieuPhong:
			bkg.spriteName = "hiepkhach";
			if (isShowNotify)
			{
				notifyIcon.gameObject.SetActive(gadgetPanelBottom.checkThongBaoCuuTieuPhong());
			}
			break;
		case KyNgoType.RuongThachSanh:
			bkg.spriteName = "thachsanh";
			if (isShowNotify)
			{
				notifyIcon.gameObject.SetActive(gadgetPanelBottom.checkThongBaoRuongTS());
			}
			break;
		case KyNgoType.DaiLy:
			bkg.spriteName = "daily";
			break;
		case KyNgoType.UongRuou:
			bkg.spriteName = "uongruou";
			if (isShowNotify)
			{
				notifyIcon.gameObject.SetActive(gadgetPanelBottom.checkThongBaoUongRuou());
			}
			break;
		case KyNgoType.SonTrang:
			bkg.spriteName = "daily";
			break;
		case KyNgoType.DoiDo:
			bkg.spriteName = "doido";
			if (isShowNotify)
			{
				notifyIcon.gameObject.SetActive(gadgetPanelBottom.checkThongBaoDoiDo());
			}
			break;
		case KyNgoType.ThuongNap:
			bkg.spriteName = "thuongnap";
			break;
		case KyNgoType.DiHoaCung:
			bkg.spriteName = "dihoacung";
			break;
		case KyNgoType.BanhChung:
			bkg.spriteName = "banh_chung";
			break;
		case KyNgoType.TichLuyTieu:
			bkg.spriteName = "kim_tien_bang";
			if (isShowNotify)
			{
				notifyIcon.gameObject.SetActive(gadgetPanelBottom.checkThongBaoKimTienBang());
			}
			break;
		case KyNgoType.TichLuyNap:
			bkg.spriteName = "tuong_duong_te_the";
			if (isShowNotify)
			{
				notifyIcon.gameObject.SetActive(gadgetPanelBottom.checkThongBaoTuongDuongTeThe());
			}
			break;
		case KyNgoType.PhaoHoa:
			bkg.spriteName = "phao_hoa";
			break;
		case KyNgoType.DapNieu:
			bkg.spriteName = "dapnieu";
			break;
		case KyNgoType.VongQuay:
			bkg.spriteName = "vong_quay";
			break;
		case KyNgoType.GuiTietKiem:
			bkg.spriteName = "guitietkiem";
			break;
		case KyNgoType.BacMayMan:
			bkg.spriteName = "bacmayman";
			if (isShowNotify)
			{
				notifyIcon.gameObject.SetActive(gadgetPanelBottom.checkThongBaoBacMayMan());
			}
			break;
		case KyNgoType.TuBaoBon:
			bkg.spriteName = "tubaobon";
			if (isShowNotify)
			{
				notifyIcon.gameObject.SetActive(gadgetPanelBottom.checkThongBaoTuBaoBon());
			}
			break;
		case KyNgoType.TichLuyNapHangNgay:
			bkg.spriteName = "tichnap";
			if (isShowNotify)
			{
				notifyIcon.gameObject.SetActive(gadgetPanelBottom.checkThongBaoTichNapHangNgay());
			}
			break;
		case KyNgoType.BaoRuong:
			bkg.spriteName = "ruongbau";
			break;
		case KyNgoType.NapLanDau:
			bkg.spriteName = "naptienlandau";
			break;
		case KyNgoType.AnTheCaoThu:
			bkg.spriteName = "anthecaothu";
			break;
		case KyNgoType.CamCung:
			bkg.spriteName = "camcung";
			break;
		}
		bkg.MakePixelPerfect();
		focus.gameObject.SetActive(false);
	}

	public void isSelected(bool isActive)
	{
		focus.gameObject.SetActive(isActive);
	}
}
