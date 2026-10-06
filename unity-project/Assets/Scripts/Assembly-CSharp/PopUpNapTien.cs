using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpNapTien : MonoBehaviour
{
	public UISprite spVIPHienTai;

	public UISprite spVIPLen;

	public UILabel lbNapThem;

	public UILabel labelTiGia;

	public GameObject NapTienLanDauGrp;

	public GameObject TiGiaGroup;

	public UIButton btnVangGapDoi;

	public PhanThuongItem phanThuong1;

	public PhanThuongItem phanThuong2;

	public PhanThuongItem phanThuong3;

	public UIButton btnNapThem;

	public UIButton btnUuDaiVIP;

	public UITexture imageTexture;

	public UILabel lbMaxVIP;

	public GameObject groupCurrentVIP;

	public static PopUpNapTien instance;

	private List<PhanThuongResponse.PhanThuong> listPhanThuong = new List<PhanThuongResponse.PhanThuong>();

	private void Awake()
	{
		TiGiaGroup.SetActive(true);
		labelTiGia.text = Localization.instance.Get("TiGiaNapTienLabel");
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public static void Create()
	{
		DestroyPopup();
	}

	public void displayUserInfo()
	{
		listPhanThuong.Clear();
		imageTexture.color = new Color(1f, 1f, 1f, 0f);
		if (GameManager.instance.m_GameClient.isIpV6)
		{
			imageTexture.alpha = 0f;
		}
		else
		{
			StartCoroutine(instance.DangTaiProfileImage(GameManager.instance.m_GameClient.UserInfo.ServerInfo.NapKnbImgUrl));
		}
		int vip = GameManager.instance.m_GameClient.UserInfo.Gamer.Vip;
		switch (vip)
		{
		case 0:
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
		case 7:
		case 8:
		case 9:
		case 10:
		case 11:
		case 12:
		case 13:
		case 14:
		{
			displayVIP(vip, spVIPHienTai);
			displayVIP(vip + 1, spVIPLen);
			int num = (int)ConfigManager.instance.GetSoVangNapLenVip(vip + 1) - GameManager.instance.m_GameClient.UserInfo.Gamer.KnbDaNap;
			if (num > 0)
			{
				lbNapThem.text = string.Format(Localization.instance.Get("NapThemLabel"), num);
			}
			else
			{
				lbNapThem.text = string.Format(Localization.instance.Get("NapThemLabel"), 0);
			}
			lbMaxVIP.gameObject.SetActive(false);
			break;
		}
		case 15:
			if (vip == 15)
			{
				groupCurrentVIP.gameObject.SetActive(false);
				lbMaxVIP.gameObject.SetActive(true);
			}
			break;
		default:
			groupCurrentVIP.gameObject.SetActive(false);
			lbMaxVIP.gameObject.SetActive(false);
			break;
		}
		if (!GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains("NapKNBLanDau;"))
		{
			listPhanThuong = ConfigManager.instance.OtherConfig.ThuongNapKnbLanDau;
			if (listPhanThuong != null)
			{
				if (listPhanThuong.Count >= 1)
				{
					phanThuong1.Set(listPhanThuong[0], true, false, true);
				}
				if (listPhanThuong.Count >= 2)
				{
					phanThuong2.Set(listPhanThuong[1], true, false, true);
				}
				if (listPhanThuong.Count >= 3)
				{
					phanThuong3.Set(listPhanThuong[2], true, false, true);
				}
			}
			NapTienLanDauGrp.gameObject.SetActive(true);
		}
		else
		{
			NapTienLanDauGrp.gameObject.SetActive(false);
			TiGiaGroup.transform.localPosition = new Vector3(TiGiaGroup.transform.localPosition.x, 80f, TiGiaGroup.transform.localPosition.z);
		}
	}

	public void displayVIP(int vip, UISprite sprite)
	{
		if (sprite != null && vip >= 0)
		{
			sprite.spriteName = "icon_vip" + vip;
			sprite.MakePixelPerfect();
		}
	}

	private IEnumerator DangTaiProfileImage(string url)
	{
		WWW www = new WWW(url);
		yield return www;
		imageTexture.mainTexture = www.texture;
		imageTexture.color = new Color(1f, 1f, 1f, 1f);
	}

	public void onClick_PhanThuong1(GameObject go)
	{
		displayDetailPopUp(listPhanThuong[0]);
	}

	public void onClick_PhanThuong2(GameObject go)
	{
		displayDetailPopUp(listPhanThuong[1]);
	}

	public void onClick_PhanThuong3(GameObject go)
	{
		displayDetailPopUp(listPhanThuong[2]);
	}

	public void displayDetailPopUp(PhanThuongResponse.PhanThuong phanThuong)
	{
		if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.TRANG_BI)
		{
			EGDebug.Log("LOAI PHAN THUONG: TRANG BI");
			UserInfo.TrangBiData trangBiData = new UserInfo.TrangBiData();
			trangBiData.Level = phanThuong.Level;
			trangBiData.Name = phanThuong.Name;
			PopupTrangBi.CreateByNormalScreen(trangBiData);
		}
		else if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU)
		{
			EGDebug.Log("LOAI PHAN THUONG: VAT PHAM TIEU THU");
			UserInfo.VatPhamTieuThuData vatPhamTieuThuData = new UserInfo.VatPhamTieuThuData();
			vatPhamTieuThuData.Name = phanThuong.Name;
			vatPhamTieuThuData.Quantity = phanThuong.Count;
			PopUpVatPham.Create(vatPhamTieuThuData);
		}
		else if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.VO_CONG)
		{
			EGDebug.Log("LOAI PHAN THUONG: VO CONG");
			UserInfo.VoCongData voCongData = new UserInfo.VoCongData();
			voCongData.Name = phanThuong.Name;
			voCongData.Level = phanThuong.Level;
			PopupVoCong.CreateByNormalScreen(GameManager.instance.m_GameClient.UserInfo, voCongData, voCongData.Level);
		}
	}

	public void OnClick_NapNgayBtn()
	{
		SohaSDKManager.instance.Payment();
	}

	public void OnClick_UuDaiVIPBtn()
	{
		PopUpUuDaiVIP.Create();
		DestroyPopup();
	}

	public void NhanKNBThang_OnClick()
	{
		GameManager.instance.m_GameClient.RequestNhanThuongDailyKNB();
	}

	public void OnClick_CloseBtn()
	{
		DestroyPopup();
	}
}
