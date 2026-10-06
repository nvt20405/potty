using UnityEngine;

public class PopupSelectPhanThuong : MonoBehaviour
{
	public GameObject ItemRoot;

	public GameObject ItemPrefab;

	public UILabel popupLabel;

	public UILabel descLabel;

	public static PopupSelectPhanThuong instance;

	private UserInfo.VatPhamTieuThuData VatPhamData = new UserInfo.VatPhamTieuThuData();

	public static void Release()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	private void OnCloseBtnClick()
	{
		Release();
		if (TutorialPopup.instance != null)
		{
			TutorialPopup.instance.ShowNextTutorial();
		}
	}

	public void OnOkClick()
	{
		SelectPhanThuongItem selectPhanThuongItem = null;
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			UICheckbox componentInChildren = transform2.GetComponentInChildren<UICheckbox>();
			if (componentInChildren.isChecked)
			{
				selectPhanThuongItem = transform2.gameObject.GetComponent<SelectPhanThuongItem>();
				break;
			}
		}
		if ((bool)selectPhanThuongItem)
		{
			if (VatPhamData.Name.StartsWith("VP_TUITHAN"))
			{
				UseTuiThanRequest useTuiThanRequest = new UseTuiThanRequest();
				useTuiThanRequest.TuiThanID = VatPhamData.ID;
				useTuiThanRequest.indexPT = selectPhanThuongItem.index;
				GameManager.instance.m_GameClient.RequestUseTuiThan(useTuiThanRequest);
			}
			else
			{
				UseRuongThanRequest useRuongThanRequest = new UseRuongThanRequest();
				useRuongThanRequest.RuongThanID = VatPhamData.ID;
				useRuongThanRequest.BauVatName = selectPhanThuongItem.PhanThuong.Name;
				GameManager.instance.m_GameClient.RequestUseRuongThan(useRuongThanRequest);
			}
		}
	}

	public static PopupSelectPhanThuong Create(string popuptext, string desctext, PhanThuongResponse res, UserInfo.VatPhamTieuThuData vpData)
	{
		Release();
		instance = ((GameObject)Object.Instantiate(Resources.Load("popup/PopupSelectPhanThuong"))).GetComponent<PopupSelectPhanThuong>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = Vector3.one;
		instance.VatPhamData = vpData;
		instance.Set(popuptext, desctext, res);
		if (TutorialPopup.instance != null)
		{
			TutorialPopup.instance.ShowNextTutorial();
		}
		return instance;
	}

	public void Set(string popuptext, string desctext, PhanThuongResponse res)
	{
		popupLabel.text = popuptext;
		descLabel.text = desctext;
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		Vector3 vector = default(Vector3);
		vector = new Vector3(-135f, 160f, 0f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, -130f, 0f);
		for (int i = 0; i < res.PhanThuongList.Count; i++)
		{
			SelectPhanThuongItem component = ((GameObject)Object.Instantiate(ItemPrefab)).GetComponent<SelectPhanThuongItem>();
			component.transform.parent = ItemRoot.transform;
			component.transform.localScale = Vector3.one;
			component.transform.localPosition = vector;
			vector += vector2;
			component.Set(res.PhanThuongList[i], i);
		}
	}
}
