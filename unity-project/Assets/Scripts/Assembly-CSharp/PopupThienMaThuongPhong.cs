using UnityEngine;

public class PopupThienMaThuongPhong : MonoBehaviour
{
	public UILabel lbDescription;

	public UILabel lbCountVP;

	public GameObject btnKham;

	public GameObject btnCheTac;

	public GameObject btnCuongHoa;

	public GameObject btnMoKhoa;

	public UILabel lbTitle;

	public static PopupThienMaThuongPhong instance;

	private UserInfo.VatPhamTieuThuData vatphamCan;

	private UserInfo.ThienMaLenhInfo mThienMaData;

	private int SlotSelected = -1;

	public static PopupThienMaThuongPhong Create(UserInfo.ThienMaLenhInfo thienmaData, int curSlot, ThienMaAction actionType, string strDescription, string vpCanCodeName)
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		Object obj = Object.Instantiate(Resources.Load("Popup/PopupThienMaThuongPhong"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		instance = gameObject.GetComponent<PopupThienMaThuongPhong>();
		instance.btnCheTac.gameObject.SetActive(false);
		instance.btnKham.gameObject.SetActive(false);
		instance.btnCuongHoa.gameObject.SetActive(false);
		instance.btnMoKhoa.gameObject.SetActive(false);
		if (thienmaData != null)
		{
			instance.mThienMaData = thienmaData;
		}
		if (curSlot > 0)
		{
			instance.SlotSelected = curSlot;
		}
		instance.DisplayInfo(actionType, strDescription, vpCanCodeName);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		return instance;
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			instance.gameObject.SetActive(false);
			Object.Destroy(instance.gameObject);
		}
		instance = null;
	}

	public void DisplayInfo(ThienMaAction actionType, string strDescription, string vpCanCodeName)
	{
		lbDescription.text = strDescription;
		vatphamCan = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == vpCanCodeName);
		switch (actionType)
		{
		case ThienMaAction.CHE_TAC:
			if (vatphamCan == null)
			{
				lbCountVP.text = string.Format(Localization.instance.Get("SoLuongThienMaTanPhienLabel"), 0);
			}
			else
			{
				lbCountVP.text = string.Format(Localization.instance.Get("SoLuongThienMaTanPhienLabel"), vatphamCan.Quantity);
			}
			btnCheTac.gameObject.SetActive(true);
			lbTitle.text = Localization.instance.Get("CheTacTitlePopup");
			break;
		case ThienMaAction.KHAM:
			if (vatphamCan == null)
			{
				lbCountVP.text = string.Format(Localization.instance.Get("SoLuongHoangKimTieuDaoLabel"), 0);
			}
			else
			{
				lbCountVP.text = string.Format(Localization.instance.Get("SoLuongHoangKimTieuDaoLabel"), vatphamCan.Quantity);
			}
			btnKham.gameObject.SetActive(true);
			lbTitle.text = Localization.instance.Get("KhamTitlePopup");
			break;
		case ThienMaAction.CUONG_HOA:
			if (vatphamCan == null)
			{
				lbCountVP.text = string.Format(Localization.instance.Get("SoLuongHuyetNgocLabel"), 0);
			}
			else
			{
				lbCountVP.text = string.Format(Localization.instance.Get("SoLuongHuyetNgocLabel"), vatphamCan.Quantity);
			}
			btnCuongHoa.gameObject.SetActive(true);
			lbTitle.text = Localization.instance.Get("CuongHoaTitlePopup");
			break;
		case ThienMaAction.MO_KHOA:
			if (vatphamCan == null)
			{
				lbCountVP.text = string.Format(Localization.instance.Get("SoLuongHoangKimTieuDaoLabel"), 0);
			}
			else
			{
				lbCountVP.text = string.Format(Localization.instance.Get("SoLuongHoangKimTieuDaoLabel"), vatphamCan.Quantity);
			}
			btnMoKhoa.gameObject.SetActive(true);
			lbTitle.text = Localization.instance.Get("MoKhoaTitlePopup");
			break;
		}
	}

	public void OnCheTacClick(GameObject go)
	{
		if (vatphamCan != null && vatphamCan.Quantity > 0)
		{
			SetThienMaLenhRequest setThienMaLenhRequest = new SetThienMaLenhRequest();
			setThienMaLenhRequest.VatPhamID = vatphamCan.ID;
			GameManager.instance.m_GameClient.RequestThienMaLenhCreate(setThienMaLenhRequest);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("ThongBaoKhongDuTanPhien"));
		}
		DestroyPopup();
	}

	public void OnKhamClick(GameObject go)
	{
		if (mThienMaData != null && vatphamCan != null && vatphamCan.Quantity > 0 && SlotSelected > 0)
		{
			SetThienMaLenhRequest setThienMaLenhRequest = new SetThienMaLenhRequest();
			setThienMaLenhRequest.ID = mThienMaData.ID;
			setThienMaLenhRequest.VatPhamID = vatphamCan.ID;
			setThienMaLenhRequest.SlotID = SlotSelected;
			GameManager.instance.m_GameClient.RequestThienMaQuaySlot(setThienMaLenhRequest);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("ThongBaoKhongDuHKTieuDao"));
		}
		DestroyPopup();
	}

	public void OnCuongHoaClick(GameObject go)
	{
		if (vatphamCan != null && vatphamCan.Quantity > 0)
		{
			SetThienMaLenhRequest setThienMaLenhRequest = new SetThienMaLenhRequest();
			setThienMaLenhRequest.ID = mThienMaData.ID;
			setThienMaLenhRequest.VatPhamID = vatphamCan.ID;
			setThienMaLenhRequest.SlotID = SlotSelected;
			GameManager.instance.m_GameClient.RequestThienMaUpgradeSlot(setThienMaLenhRequest);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("ThongBaoKhongDuHuyetNgoc"));
		}
		DestroyPopup();
	}

	public void OnMoKhoaClick(GameObject go)
	{
		if (vatphamCan != null && vatphamCan.Quantity > 0)
		{
			SetThienMaLenhRequest setThienMaLenhRequest = new SetThienMaLenhRequest();
			setThienMaLenhRequest.ID = mThienMaData.ID;
			setThienMaLenhRequest.VatPhamID = vatphamCan.ID;
			setThienMaLenhRequest.SlotID = SlotSelected;
			GameManager.instance.m_GameClient.RequestThienMaOpenSlot(setThienMaLenhRequest);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("ThongBaoKhongDuHKTieuDaoUnLock"));
		}
		DestroyPopup();
	}

	public void OnNoClick(GameObject go)
	{
		DestroyPopup();
	}
}
