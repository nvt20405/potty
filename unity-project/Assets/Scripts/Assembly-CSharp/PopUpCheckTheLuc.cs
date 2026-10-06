using UnityEngine;

public class PopUpCheckTheLuc : MonoBehaviour
{
	public static PopUpCheckTheLuc instance;

	public OtherAvatar vpAvatar;

	public GameObject priceGroup;

	public UILabel lbPrice;

	public UILabel lbTayNai;

	public UILabel lbDescription;

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
		vpAvatar.Set("VP_GA_QUAY");
		UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_GA_QUAY");
		if (vatPhamTieuThuData != null && vatPhamTieuThuData.Quantity > 0)
		{
			lbTayNai.gameObject.SetActive(true);
			priceGroup.gameObject.SetActive(false);
			lbDescription.gameObject.SetActive(false);
			return;
		}
		lbTayNai.gameObject.SetActive(false);
		priceGroup.gameObject.SetActive(true);
		lbDescription.gameObject.SetActive(true);
		int soLuotMuaGaByString = ConfigManager.instance.GetSoLuotMuaGaByString(GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay);
		int maxMuaGaByVip = ConfigManager.instance.GetMaxMuaGaByVip(GameManager.instance.m_GameClient.UserInfo.Gamer.Vip);
		int nextVipMuaGa = ConfigManager.instance.GetNextVipMuaGa(GameManager.instance.m_GameClient.UserInfo.Gamer.Vip);
		int giaMuaGaByCount = ConfigManager.instance.GetGiaMuaGaByCount(soLuotMuaGaByString + 1);
		lbPrice.text = giaMuaGaByCount.ToString();
		if (nextVipMuaGa == -1)
		{
			lbDescription.text = string.Format(Localization.instance.Get("ToiDaAnGaMess"), soLuotMuaGaByString, maxMuaGaByVip);
			return;
		}
		int maxMuaGaByVip2 = ConfigManager.instance.GetMaxMuaGaByVip(nextVipMuaGa);
		lbDescription.text = string.Format(Localization.instance.Get("SoLanToiDaAnGaMess"), soLuotMuaGaByString, maxMuaGaByVip, nextVipMuaGa, maxMuaGaByVip2);
	}

	public static void Create()
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopUpCheckTheLuc"))).GetComponent<PopUpCheckTheLuc>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	public void btnDong_OnClick(GameObject go)
	{
		DestroyPopup();
	}

	public void btnDongY_OnClick(GameObject go)
	{
		UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_GA_QUAY");
		if (vatPhamTieuThuData != null && vatPhamTieuThuData.Quantity > 0)
		{
			UseCustomItemRequest useCustomItemRequest = new UseCustomItemRequest();
			useCustomItemRequest.ID = vatPhamTieuThuData.ID;
			GameManager.instance.m_GameClient.RequestUseCustomItem(useCustomItemRequest);
		}
		else
		{
			BuyVatPhamAndUseRequest buyVatPhamAndUseRequest = new BuyVatPhamAndUseRequest();
			buyVatPhamAndUseRequest.VatPhamName = "VP_GA_QUAY";
			GameManager.instance.m_GameClient.RequestBuyVatPhamAndUse(buyVatPhamAndUseRequest);
		}
		DestroyPopup();
	}
}
