using UnityEngine;

public class PopupDatTenMonPhai : MonoBehaviour
{
	public UIInput NameInput;

	public UIInput GioiThieuInput;

	public OtherAvatar[] PhanThuong;

	public GameObject GioiThieuGroup;

	private bool IsDatTenLanDau;

	public static PopupDatTenMonPhai instance;

	public void OnCloseClick()
	{
		DestroyPopup();
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
		TutorialPopup.Release();
		GameManager.instance.isStartTutorial = false;
	}

	public static void CreateDatTenLanDau()
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("popup/PopupDatTenMonPhai"))).GetComponent<PopupDatTenMonPhai>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.IsDatTenLanDau = true;
		instance.Set();
		GameManager.instance.isStartTutorial = true;
	}

	public static void Create()
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("popup/PopupDatTenMonPhai"))).GetComponent<PopupDatTenMonPhai>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.IsDatTenLanDau = false;
		instance.Set();
	}

	public void OnOkClick()
	{
		DatTenMonPhaiRequest datTenMonPhaiRequest = new DatTenMonPhaiRequest();
		datTenMonPhaiRequest.Name = NameInput.text;
		UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_DOI_TEN");
		if (vatPhamTieuThuData != null)
		{
			datTenMonPhaiRequest.VatPhamID = vatPhamTieuThuData.ID;
		}
		datTenMonPhaiRequest.InviteCode = GioiThieuInput.text;
		GameManager.instance.m_GameClient.RequestDatTenMonPhai(datTenMonPhaiRequest);
		if (!IsDatTenLanDau)
		{
			DestroyPopup();
		}
	}

	public void OnNgauNhienClick()
	{
		int num = Random.Range(0, 10000);
		NameInput.text = ConfigManager.instance.GetRandomTenHienThi(num);
	}

	public void Set()
	{
		if (IsDatTenLanDau)
		{
			NGUITools.SetActive(GioiThieuGroup, true);
		}
		else
		{
			NGUITools.SetActive(GioiThieuGroup, false);
		}
		int num = Random.Range(0, 10000);
		NameInput.text = ConfigManager.instance.GetRandomTenHienThi(num);
		OtherAvatar[] phanThuong = PhanThuong;
		OtherAvatar[] array = phanThuong;
		foreach (OtherAvatar otherAvatar in array)
		{
			NGUITools.SetActive(otherAvatar.gameObject, false);
		}
		int num2 = 0;
		foreach (PhanThuongResponse.PhanThuong item in ConfigManager.instance.OtherConfig.ThuongNhapMaGioiThieu)
		{
			NGUITools.SetActive(PhanThuong[num2].gameObject, true);
			if (item.Loai == PhanThuongResponse.LoaiPhanThuong.BAC)
			{
				PhanThuong[num2].SetBac(item.Count, true);
			}
			else if (item.Loai == PhanThuongResponse.LoaiPhanThuong.VANG)
			{
				PhanThuong[num2].SetVang(item.Count, true);
			}
			else if (item.Loai == PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU)
			{
				PhanThuong[num2].Set(item.Name, 0, -1, item.Count);
			}
			else if (item.Loai == PhanThuongResponse.LoaiPhanThuong.TRANG_BI)
			{
				UserInfo.TrangBiData trangBiData = new UserInfo.TrangBiData();
				trangBiData.Name = item.Name;
				trangBiData.Level = item.Level;
				PhanThuong[num2].Set(trangBiData);
			}
			else if (item.Loai == PhanThuongResponse.LoaiPhanThuong.VO_CONG)
			{
				UserInfo.VoCongData voCongData = new UserInfo.VoCongData();
				voCongData.Name = item.Name;
				voCongData.Level = item.Level;
				PhanThuong[num2].Set(voCongData);
			}
			num2++;
			if (num2 > 3)
			{
				break;
			}
		}
	}
}
