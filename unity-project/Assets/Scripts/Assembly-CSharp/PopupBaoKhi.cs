using UnityEngine;

public class PopupBaoKhi : MonoBehaviour
{
	public OtherAvatar avatar1;

	public OtherAvatar avatar2;

	public OtherAvatar avatar3;

	public UILabel lbChiSo1;

	public UILabel lbChiSo2;

	public UILabel lbChiSo3;

	public UILabel lbInfo;

	public static PopupBaoKhi instance;

	public GameObject btnDoi;

	public GameObject btnNangCap;

	private UserInfo.ThienMaLenhInfo thienmaData;

	private int currSlotThienMa = -1;

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public static void Create(UserInfo.ThienMaLenhInfo thienMaData, bool isDiffUserView = false, int slot = -1)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupBaoKhi"))).GetComponent<PopupBaoKhi>();
		instance.thienmaData = thienMaData;
		instance.displayInfoResult(thienMaData);
		instance.currSlotThienMa = slot;
		if (isDiffUserView)
		{
			instance.btnDoi.gameObject.SetActive(false);
			instance.btnNangCap.gameObject.SetActive(false);
		}
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	public void displayInfoResult(UserInfo.ThienMaLenhInfo data)
	{
		avatar1.SetBaoKhiAvatar(data.Slot1);
		avatar2.SetBaoKhiAvatar(data.Slot2);
		avatar3.SetBaoKhiAvatar(data.Slot3);
		Utils.SetLayer(avatar1.gameObject.transform, "GUIPopUp", true);
		Utils.SetLayer(avatar2.gameObject.transform, "GUIPopUp", true);
		Utils.SetLayer(avatar3.gameObject.transform, "GUIPopUp", true);
		lbChiSo1.text = "+" + data.Value1 + "%";
		lbChiSo2.text = "+" + data.Value2 + "%";
		lbChiSo3.text = "+" + data.Value3 + "%";
		if (!string.IsNullOrEmpty(data.Slot1))
		{
			CfgVoCong cfgVoCong = ConfigManager.instance.m_dicVCs[data.Slot1];
			lbInfo.text = string.Format(Localization.instance.Get("BaoKhiInfo"), cfgVoCong.TenHienThi, data.Value1 + data.Value2 + data.Value3);
		}
		else
		{
			lbInfo.text = string.Empty;
		}
	}

	public void btnDoi_OnClick(GameObject go)
	{
		DestroyPopup();
		ScreenDoiHinh screenDoiHinh = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
		screenDoiHinh.NhanVatInfo.curSlotBaoKhiIdx = currSlotThienMa;
		screenDoiHinh.NhanVatInfo.displayPopUpSelectBaoKhi();
	}

	public void btnNangCap_OnClick(GameObject go)
	{
		if (thienmaData != null)
		{
			ScreenBaoKhi screenBaoKhi = GUIManager.getScreen(GAME_SCREEN.ScreenBaoKhi) as ScreenBaoKhi;
			screenBaoKhi.Set(thienmaData);
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenBaoKhi);
		}
		DestroyPopup();
	}

	public void OnCloseClick(GameObject go)
	{
		DestroyPopup();
	}
}
