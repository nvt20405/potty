using System.Collections.Generic;
using UnityEngine;

public class PopupCamCungBiBaoInfo : MonoBehaviour
{
	public List<UILabel> listThuongCapTitle;

	public static PopupCamCungBiBaoInfo instance;

	public UISprite spFocus;

	public UILabel lbInfo1;

	public UILabel lbInfo2;

	public UILabel lbInfo3;

	public UILabel lbInfo4;

	public UILabel lbInfo5;

	public GameObject item1;

	public GameObject item2;

	public GameObject item3;

	public GameObject item4;

	public GameObject item5;

	private QuayCamCungType curType;

	public GameObject groupNormal;

	public GameObject groupMax;

	public void OnCloseClick()
	{
		DestroyPopup();
	}

	private void Start()
	{
		if (listThuongCapTitle != null && listThuongCapTitle.Count > 0)
		{
			for (int i = 0; i < listThuongCapTitle.Count; i++)
			{
				listThuongCapTitle[i].text = Localization.instance.Get("ThuongCapLabel") + " " + (i + 1);
			}
		}
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public static void Create(PhanThuongResponse.PhanThuong ptName, QuayCamCungType soLuongPhiThien, int LuotQuay)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupCamCungBiBaoInfo"))).GetComponent<PopupCamCungBiBaoInfo>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.curType = soLuongPhiThien;
		instance.Set(ptName, soLuongPhiThien, LuotQuay);
	}

	private void Set(PhanThuongResponse.PhanThuong ptResponse, QuayCamCungType soLuongPhiThien, int LuotQuay)
	{
		groupNormal.SetActive(true);
		groupMax.SetActive(false);
		if (LuotQuay <= 1)
		{
			spFocus.transform.localPosition = item1.transform.localPosition;
		}
		else
		{
			switch (LuotQuay)
			{
			case 2:
				spFocus.transform.localPosition = item2.transform.localPosition;
				break;
			case 3:
				spFocus.transform.localPosition = item3.transform.localPosition;
				break;
			case 4:
				spFocus.transform.localPosition = item4.transform.localPosition;
				break;
			case 5:
				spFocus.transform.localPosition = item5.transform.localPosition;
				groupNormal.SetActive(false);
				groupMax.SetActive(true);
				break;
			}
		}
		string text = string.Empty;
		if (ptResponse != null)
		{
			switch (ptResponse.Loai)
			{
			case PhanThuongResponse.LoaiPhanThuong.BAC:
				text = Localization.instance.Get("Bac");
				break;
			case PhanThuongResponse.LoaiPhanThuong.VANG:
				text = Localization.instance.Get("Vang");
				break;
			case PhanThuongResponse.LoaiPhanThuong.TRANG_BI:
				if (ConfigManager.instance.m_dicTrangBi.ContainsKey(ptResponse.Name))
				{
					text = ConfigManager.instance.m_dicTrangBi[ptResponse.Name].TenHienThi;
				}
				break;
			case PhanThuongResponse.LoaiPhanThuong.VO_CONG:
				if (ConfigManager.instance.m_dicVCs.ContainsKey(ptResponse.Name))
				{
					text = ConfigManager.instance.m_dicVCs[ptResponse.Name].TenHienThi;
				}
				break;
			case PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU:
				if (ConfigManager.instance.m_dicVatPhamTieuThu.ContainsKey(ptResponse.Name))
				{
					text = ConfigManager.instance.m_dicVatPhamTieuThu[ptResponse.Name].TenHienThi;
				}
				break;
			case PhanThuongResponse.LoaiPhanThuong.HON_NHAN_VAT:
			{
				string key2 = ptResponse.Name;
				if (ptResponse.Name.StartsWith("TH_"))
				{
					key2 = "NV_" + ptResponse.Name.Substring(3);
				}
				if (ConfigManager.instance.m_dicNhanVats.ContainsKey(key2))
				{
					text = string.Format(Localization.instance.Get("PhanThuongTanHonItem"), ConfigManager.instance.m_dicNhanVats[key2].TenHienThi);
				}
				break;
			}
			case PhanThuongResponse.LoaiPhanThuong.CAO_NHAN:
				text = string.Format(Localization.instance.Get("PhanThuongCaoNhanInfo"), ptResponse.Level);
				break;
			case PhanThuongResponse.LoaiPhanThuong.BAN_DO:
				text = string.Format(Localization.instance.Get("PhanThuongBanDoInfo"), ptResponse.Level);
				break;
			case PhanThuongResponse.LoaiPhanThuong.BANG_HUU:
				text = Localization.instance.Get("PhanThuongBangHuuInfo");
				break;
			case PhanThuongResponse.LoaiPhanThuong.THUONG_NHAN:
				text = Localization.instance.Get("PhanThuongThuongNhanInfo");
				break;
			case PhanThuongResponse.LoaiPhanThuong.TY_THI:
				text = Localization.instance.Get("PhanThuongTyThiInfo");
				break;
			case PhanThuongResponse.LoaiPhanThuong.MANH_VO_CONG:
			{
				string key3 = ptResponse.Name;
				if (ptResponse.Name.StartsWith("MVC_"))
				{
					key3 = ptResponse.Name.Substring(1);
				}
				if (ConfigManager.instance.m_dicVCs.ContainsKey(key3))
				{
					text = ConfigManager.instance.m_dicVCs[ptResponse.Name].TenHienThi;
				}
				break;
			}
			case PhanThuongResponse.LoaiPhanThuong.MANH_TRANG_BI:
			{
				string key = ptResponse.Name;
				if (ptResponse.Name.StartsWith("MVK_") || ptResponse.Name.StartsWith("MAG_") || ptResponse.Name.StartsWith("MTS_") || ptResponse.Name.StartsWith("MMU_"))
				{
					key = ptResponse.Name.Substring(1);
				}
				if (ConfigManager.instance.m_dicTrangBi.ContainsKey(key))
				{
					text = ConfigManager.instance.m_dicTrangBi[ptResponse.Name].TenHienThi;
				}
				break;
			}
			case PhanThuongResponse.LoaiPhanThuong.NGUYEN_KHI:
				if (ConfigManager.instance.OtherConfig.NguyenKhiConfig.ContainsKey(ptResponse.Name))
				{
					text = ConfigManager.instance.OtherConfig.NguyenKhiConfig[ptResponse.Name].DisplayName.ToString();
				}
				break;
			case PhanThuongResponse.LoaiPhanThuong.THU_CUOI:
				if (ConfigManager.instance.OtherConfig.ThuCuoiConfig != null && ConfigManager.instance.OtherConfig.ThuCuoiConfig.ContainsKey(ptResponse.Name))
				{
					text = ConfigManager.instance.OtherConfig.ThuCuoiConfig[ptResponse.Name].DisplayName.ToString();
				}
				break;
			case PhanThuongResponse.LoaiPhanThuong.THAN_THU:
				text = Localization.instance.Get(ptResponse.Name);
				break;
			}
		}
		int num = 1;
		switch (soLuongPhiThien)
		{
		case QuayCamCungType.USE_1_PHITHIENLENH:
			num = 1;
			break;
		case QuayCamCungType.USE_5_PHITHIENLENH:
			num = 5;
			break;
		case QuayCamCungType.USE_25_PHITHIENLENH:
			num = 25;
			break;
		}
		lbInfo1.text = (long)ptResponse.Count * (long)num + " " + text;
		lbInfo2.text = (long)ptResponse.Count * (long)num * 10 + " " + text;
		lbInfo3.text = (long)ptResponse.Count * (long)num * 100 + " " + text;
		lbInfo4.text = (long)ptResponse.Count * (long)num * 1000 + " " + text;
		lbInfo5.text = (long)ptResponse.Count * (long)num * 10000 + " " + text;
		EGDebug.Log("test test: " + ptResponse.Count * num);
		EGDebug.Log("test test: " + ptResponse.Count * num * 2);
		EGDebug.Log("test test: " + ptResponse.Count * num * 3);
		EGDebug.Log("test test: " + ptResponse.Count * num * 4);
		EGDebug.Log("test test: " + ptResponse.Count * num * 5);
	}

	public void NhanPhanThuong(GameObject go)
	{
		GameManager.instance.m_GameClient.RequestNhanThuongCamCung();
	}

	public void QuayTiep(GameObject go)
	{
		QuayCamCungBiBaoResquest quayCamCungBiBaoResquest = new QuayCamCungBiBaoResquest();
		quayCamCungBiBaoResquest.typeQuayCamCung = curType;
		GameManager.instance.m_GameClient.RequestQuayCamCungBiBao(quayCamCungBiBaoResquest);
	}
}
