using UnityEngine;

public class ScreenListComboHoaVang : ScreenBase
{
	public GameObject itemPrefab;

	public GameObject ItemRoot;

	private void Awake()
	{
		UIAnchor[] componentsInChildren = GetComponentsInChildren<UIAnchor>();
		UIAnchor[] array = componentsInChildren;
		UIAnchor[] array2 = array;
		foreach (UIAnchor uIAnchor in array2)
		{
			uIAnchor.widgetContainer = GUIManager.instance.GameFrame;
		}
	}

	public override void OnActive()
	{
		base.OnActive();
		initList();
	}

	public void initList()
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		if (ConfigManager.instance.HoaVangConfig.ComboPhanThuong != null && ConfigManager.instance.HoaVangConfig.ComboPhanThuong.Count > 0)
		{
			Vector3 vector = default(Vector3);
			vector = new Vector3(0f, 320f, 0f);
			Vector3 vector2 = default(Vector3);
			vector2 = new Vector3(0f, -200f, 0f);
			for (int i = 0; i < ConfigManager.instance.HoaVangConfig.ComboPhanThuong.Count; i++)
			{
				ComboHoaVangItem component = ((GameObject)Object.Instantiate(itemPrefab)).GetComponent<ComboHoaVangItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = vector;
				vector += vector2;
				component.setDataItem(ConfigManager.instance.HoaVangConfig.ComboPhanThuong[i]);
			}
			UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
			component2.ResetPosition();
		}
	}

	public void btnBack_OnClick(GameObject go)
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenQuayDiemHoaVang);
	}
}
