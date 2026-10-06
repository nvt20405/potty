using UnityEngine;

public class PopUpMua : MonoBehaviour
{
	public enum TabOpenByScreen
	{
		VATPHAM_MARKET = 0,
		LEBAO_MARKET = 1,
		CUAHANG_THANBI = 2
	}

	public UILabel lbTitle;

	public UILabel lbSoLuong;

	public UILabel lbPrice;

	public UIButton btnPlus;

	public UIButton btnDoublePlus;

	public UIButton btnMinus;

	public UIButton btnDoubleMinus;

	public static PopUpMua instance;

	private int soLuongItem;

	private bool isStartChoice;

	private string vpName;

	private int vpPrice;

	private int slotIndex = -1;

	public GameObject grpVang;

	public UILabel lbDiemCan;

	private TabOpenByScreen currentTab;

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public static void CreateByCuaHangThanBi(string _vpName, string tenHienThi, int DiemCan, int _slotIndex)
	{
		Create(_vpName, tenHienThi, DiemCan, TabOpenByScreen.CUAHANG_THANBI);
		instance.slotIndex = _slotIndex;
	}

	public static void CreateByVatPhamMarket(string _vpName, string tenHienThi, int giaVang)
	{
		Create(_vpName, tenHienThi, giaVang, TabOpenByScreen.VATPHAM_MARKET);
	}

	public static void CreateByLeBaoMarket(string _vpName, string tenHienThi, int giaVang)
	{
		Create(_vpName, tenHienThi, giaVang, TabOpenByScreen.LEBAO_MARKET);
	}

	public static void Create(string _vpName, string tenHienThi, int giaVang, TabOpenByScreen curTab)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("popup/PopUpMua"))).GetComponent<PopUpMua>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.vpName = _vpName;
		instance.vpPrice = giaVang;
		instance.currentTab = curTab;
		if (curTab == TabOpenByScreen.LEBAO_MARKET)
		{
			instance.btnPlus.gameObject.SetActive(false);
			instance.btnDoublePlus.gameObject.SetActive(false);
		}
		instance.btnMinus.gameObject.SetActive(false);
		instance.btnDoubleMinus.gameObject.SetActive(false);
		instance.isStartChoice = true;
		instance.soLuongItem = 1;
		instance.lbSoLuong.text = instance.soLuongItem.ToString();
		instance.lbTitle.text = string.Format(Localization.instance.Get("NhapSoLuongLabel"), "[FF8C00]" + tenHienThi + "[-]");
		if (curTab == TabOpenByScreen.CUAHANG_THANBI)
		{
			instance.grpVang.SetActive(false);
			instance.lbDiemCan.gameObject.SetActive(true);
			instance.lbDiemCan.text = Localization.instance.Get("DiemCanLabel") + ": " + instance.vpPrice;
		}
		else
		{
			instance.grpVang.SetActive(true);
			instance.lbDiemCan.gameObject.SetActive(false);
			instance.lbPrice.text = instance.vpPrice.ToString();
		}
	}

	public void btnDong_OnClick(GameObject go)
	{
		DestroyPopup();
	}

	public void btnMua_OnClick(GameObject go)
	{
		if (currentTab == TabOpenByScreen.CUAHANG_THANBI)
		{
			DoiItemThanBiRequest doiItemThanBiRequest = new DoiItemThanBiRequest();
			doiItemThanBiRequest.CodeName = vpName;
			doiItemThanBiRequest.SlotIdx = slotIndex;
			doiItemThanBiRequest.Count = soLuongItem;
			GameManager.instance.m_GameClient.RequestDoiDoThanBi(doiItemThanBiRequest);
		}
		else if (GameManager.instance.m_GameClient.checkKNB(soLuongItem * vpPrice))
		{
			if (currentTab == TabOpenByScreen.VATPHAM_MARKET)
			{
				BuyVatPhamRequest buyVatPhamRequest = new BuyVatPhamRequest();
				buyVatPhamRequest.name = vpName;
				buyVatPhamRequest.count = soLuongItem;
				GameManager.instance.m_GameClient.RequestBuyVatPham(buyVatPhamRequest);
				DestroyPopup();
			}
			else if (currentTab == TabOpenByScreen.LEBAO_MARKET)
			{
				BuyLeBaoRequest buyLeBaoRequest = new BuyLeBaoRequest();
				buyLeBaoRequest.name = vpName;
				GameManager.instance.m_GameClient.RequestBuyLeBao(buyLeBaoRequest);
				DestroyPopup();
			}
		}
	}

	public void btnPlus_OnClick(GameObject go)
	{
		if (currentTab != TabOpenByScreen.LEBAO_MARKET)
		{
			if (isStartChoice)
			{
				btnMinus.gameObject.SetActive(true);
				btnDoubleMinus.gameObject.SetActive(true);
				isStartChoice = false;
			}
			soLuongItem++;
			lbSoLuong.text = instance.soLuongItem.ToString();
			lbPrice.text = (soLuongItem * vpPrice).ToString();
			lbDiemCan.text = Localization.instance.Get("DiemCanLabel") + ": " + soLuongItem * vpPrice;
		}
	}

	public void btnDoublePlus_OnClick(GameObject go)
	{
		if (currentTab != TabOpenByScreen.LEBAO_MARKET)
		{
			if (isStartChoice)
			{
				btnMinus.gameObject.SetActive(true);
				btnDoubleMinus.gameObject.SetActive(true);
				isStartChoice = false;
			}
			soLuongItem += 10;
			lbSoLuong.text = instance.soLuongItem.ToString();
			lbPrice.text = (soLuongItem * vpPrice).ToString();
			lbDiemCan.text = Localization.instance.Get("DiemCanLabel") + ": " + soLuongItem * vpPrice;
		}
	}

	public void btnMinus_OnClick(GameObject go)
	{
		if (currentTab != TabOpenByScreen.LEBAO_MARKET)
		{
			soLuongItem--;
			if (soLuongItem == 1)
			{
				btnMinus.gameObject.SetActive(false);
				btnDoubleMinus.gameObject.SetActive(false);
				isStartChoice = true;
			}
			lbSoLuong.text = instance.soLuongItem.ToString();
			lbPrice.text = (soLuongItem * vpPrice).ToString();
			lbDiemCan.text = Localization.instance.Get("DiemCanLabel") + ": " + soLuongItem * vpPrice;
		}
	}

	public void btnDoubleMinus_OnClick(GameObject go)
	{
		if (currentTab != TabOpenByScreen.LEBAO_MARKET)
		{
			soLuongItem -= 10;
			if (soLuongItem <= 1)
			{
				soLuongItem = 1;
				btnMinus.gameObject.SetActive(false);
				btnDoubleMinus.gameObject.SetActive(false);
				isStartChoice = true;
			}
			lbSoLuong.text = instance.soLuongItem.ToString();
			lbPrice.text = (soLuongItem * vpPrice).ToString();
			lbDiemCan.text = Localization.instance.Get("DiemCanLabel") + ": " + soLuongItem * vpPrice;
		}
	}

	public void btnClose_OnClick(GameObject go)
	{
		DestroyPopup();
	}
}
