using UnityEngine;

public class PopupDoiTenBang : MonoBehaviour
{
	public UIInput NameInput;

	public static PopupDoiTenBang instance;

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
	}

	public static void CreateDatTenLanDau()
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("popup/PopupDoiTenBang"))).GetComponent<PopupDoiTenBang>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.Set();
	}

	public static void Create()
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("popup/PopupDoiTenBang"))).GetComponent<PopupDoiTenBang>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.Set();
	}

	public void OnOkClick()
	{
		DatTenMonPhaiRequest datTenMonPhaiRequest = new DatTenMonPhaiRequest();
		datTenMonPhaiRequest.Name = NameInput.text;
		UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_DOI_TEN_BANG");
		if (vatPhamTieuThuData != null)
		{
			datTenMonPhaiRequest.VatPhamID = vatPhamTieuThuData.ID;
		}
		GameManager.instance.m_GameClient.RequestDoiTenLienMinh(datTenMonPhaiRequest.Name);
		DestroyPopup();
	}

	public void OnHuyClick()
	{
		DestroyPopup();
	}

	public void Set()
	{
		NameInput.text = GameManager.instance.m_GameClient.UserInfo.LienMinh.DisplayName;
	}
}
