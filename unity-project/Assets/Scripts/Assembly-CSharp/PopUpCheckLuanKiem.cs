using UnityEngine;

public class PopUpCheckLuanKiem : MonoBehaviour
{
	public static PopUpCheckLuanKiem instance;

	public OtherAvatar vpAvatar;

	public GameObject priceGroup;

	public UILabel lbPrice;

	public UILabel lbTayNai;

	public UILabel lbTitle;

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	private void Start()
	{
		vpAvatar.Set("VP_LUAN_KIEM_LENH");
		VatPhamTieuThuCfg vatPhamTieuThuCfg = ConfigManager.instance.m_dicVatPhamTieuThu["VP_LUAN_KIEM_LENH"];
		lbTitle.text = string.Format(Localization.instance.Get("HetLuotLuanKiemMess"), vatPhamTieuThuCfg.TenHienThi);
		int soLuotMuaLuanKiemByString = ConfigManager.instance.GetSoLuotMuaLuanKiemByString(GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay);
		int giaMuaLuanKiemLenhByCount = ConfigManager.instance.GetGiaMuaLuanKiemLenhByCount(soLuotMuaLuanKiemByString);
		UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_LUAN_KIEM_LENH");
		if (vatPhamTieuThuData != null && vatPhamTieuThuData.Quantity > 0)
		{
			lbTayNai.gameObject.SetActive(true);
			priceGroup.gameObject.SetActive(false);
		}
		else
		{
			lbTayNai.gameObject.SetActive(false);
			priceGroup.gameObject.SetActive(true);
			lbPrice.text = giaMuaLuanKiemLenhByCount.ToString();
		}
	}

	public static void Create()
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("popup/PopUpCheckLuanKiem"))).GetComponent<PopUpCheckLuanKiem>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	public void btnDong_OnClick(GameObject go)
	{
		DestroyPopup();
	}

	public void btnDongY_OnClick(GameObject go)
	{
		UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_LUAN_KIEM_LENH");
		if (vatPhamTieuThuData != null && vatPhamTieuThuData.Quantity > 0)
		{
			UseCustomItemRequest useCustomItemRequest = new UseCustomItemRequest();
			useCustomItemRequest.ID = vatPhamTieuThuData.ID;
			GameManager.instance.m_GameClient.RequestUseCustomItem(useCustomItemRequest);
		}
		else
		{
			BuyVatPhamAndUseRequest buyVatPhamAndUseRequest = new BuyVatPhamAndUseRequest();
			buyVatPhamAndUseRequest.VatPhamName = "VP_LUAN_KIEM_LENH";
			GameManager.instance.m_GameClient.RequestBuyVatPhamAndUse(buyVatPhamAndUseRequest);
		}
		DestroyPopup();
	}
}
