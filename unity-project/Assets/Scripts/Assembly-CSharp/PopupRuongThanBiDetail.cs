using UnityEngine;

public class PopupRuongThanBiDetail : MonoBehaviour
{
	public GameObject ItemRoot;

	public GameObject PhanThuongPrefab;

	public UILabel popupTitle;

	public PhanThuongItem ptCoDinhItem;

	public GameObject btnClose;

	public GameObject groupButtonUse;

	public UserInfo.VatPhamTieuThuData VatPhamData;

	public static PopupRuongThanBiDetail instance;

	public void OnCloseBtnClick()
	{
		DestroyPopup();
	}

	public void OnUseBtnClick()
	{
		if (VatPhamData != null)
		{
			UseRuongThanRequest useRuongThanRequest = new UseRuongThanRequest();
			useRuongThanRequest.RuongThanID = VatPhamData.ID;
			GameManager.instance.m_GameClient.RequestUseRuongThanBi(useRuongThanRequest);
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

	public static void Create(UserInfo.VatPhamTieuThuData vpRuongThanBi, UserInfo.ServerData.EventRuongThanBiCfg cfgRuong, bool isOpenToUse = false)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("popup/PopupRuongThanBiDetail"))).GetComponent<PopupRuongThanBiDetail>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.Set(vpRuongThanBi, cfgRuong, isOpenToUse);
	}

	public void Set(UserInfo.VatPhamTieuThuData vpRuongThanBi, UserInfo.ServerData.EventRuongThanBiCfg cfgRuong, bool isOpenToUse)
	{
		if (vpRuongThanBi != null)
		{
			VatPhamData = vpRuongThanBi;
			VatPhamTieuThuCfg vatPhamTieuThuCfg = ConfigManager.instance.m_dicVatPhamTieuThu[VatPhamData.Name];
			popupTitle.text = vatPhamTieuThuCfg.TenHienThi;
		}
		if (isOpenToUse)
		{
			btnClose.gameObject.SetActive(false);
			groupButtonUse.gameObject.SetActive(true);
		}
		else
		{
			btnClose.gameObject.SetActive(true);
			groupButtonUse.gameObject.SetActive(false);
		}
		if (cfgRuong == null)
		{
			return;
		}
		if (cfgRuong.PhanThuongFix != null)
		{
			ptCoDinhItem.Set(cfgRuong.PhanThuongFix, true);
		}
		if (cfgRuong.ListPhanThuong == null || cfgRuong.ListPhanThuong.Count <= 0)
		{
			return;
		}
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		Vector3 vector = default(Vector3);
		vector = new Vector3(-145f, 20f, -1f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(200f, 0f, 0f);
		for (int i = 0; i < cfgRuong.ListPhanThuong.Count; i++)
		{
			PhanThuongResponse.PhanThuong phanThuong = cfgRuong.ListPhanThuong[i];
			if (phanThuong != null)
			{
				PhanThuongItem component = ((GameObject)Object.Instantiate(PhanThuongPrefab)).GetComponent<PhanThuongItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = vector;
				component.Set(phanThuong, true);
				vector += vector2;
			}
		}
	}
}
