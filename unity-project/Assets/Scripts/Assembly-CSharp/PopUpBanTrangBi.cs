using System;
using System.Collections.Generic;
using UnityEngine;

public class PopUpBanTrangBi : MonoBehaviour
{
	private const int maxItemCount = 12;

	public static PopUpBanTrangBi instance;

	public GameObject ItemRoot;

	public GameObject TrangBiBanItemPrefab;

	public UILabel messLabel;

	public UILabel bacNhanLaiLabel;

	public Func<List<int>, bool> OnFinish;

	private List<UserInfo.TrangBiData> listSelectedTrangBiData = new List<UserInfo.TrangBiData>();

	private List<UserInfo.TrangBiData> trangBiList = new List<UserInfo.TrangBiData>();

	private List<BanTrangBiItem> ItemList = new List<BanTrangBiItem>();

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private int startItemGUI_Idx;

	private void Start()
	{
		dragPanel = ItemRoot.GetComponent<UIDraggablePanel>();
		panel = ItemRoot.GetComponent<UIPanel>();
		bacNhanLaiLabel.text = Localization.instance.Get("NhanLaiLabel") + ": 0";
	}

	private void Update()
	{
		if (ItemList.Count > 0)
		{
			Vector4 clipRange = panel.clipRange;
			BanTrangBiItem banTrangBiItem = ItemList[ItemList.Count - 1];
			float y = banTrangBiItem.transform.localPosition.y;
			BanTrangBiItem banTrangBiItem2 = ItemList[0];
			float y2 = banTrangBiItem2.transform.localPosition.y;
			if (y - clipRange.y > -600f)
			{
				SwapDragListDown();
			}
			else if (y2 - clipRange.y < 600f)
			{
				SwapDragListUp();
			}
		}
	}

	public void SwapDragListDown()
	{
		int num = startItemGUI_Idx + 12;
		if (num < trangBiList.Count)
		{
			Vector3 vector = default(Vector3);
			vector = new Vector3(0f, 340f, 0f);
			Vector3 vector2 = default(Vector3);
			vector2 = new Vector3(0f, -125f, 0f);
			BanTrangBiItem banTrangBiItem = ItemList[0];
			BanTrangBiItem banTrangBiItem2 = ItemList[ItemList.Count - 1];
			ItemList.RemoveAt(0);
			ItemList.Add(banTrangBiItem);
			banTrangBiItem.transform.localPosition = banTrangBiItem2.transform.localPosition + vector2;
			banTrangBiItem.Set(trangBiList[num]);
			if (listSelectedTrangBiData != null && listSelectedTrangBiData.Count > 0 && listSelectedTrangBiData.Contains(trangBiList[num]))
			{
				banTrangBiItem.checkBox.isChecked = true;
			}
			else
			{
				banTrangBiItem.checkBox.isChecked = false;
			}
			startItemGUI_Idx++;
		}
	}

	public void SwapDragListUp()
	{
		if (startItemGUI_Idx == 0)
		{
			return;
		}
		startItemGUI_Idx--;
		int num = startItemGUI_Idx;
		if (num >= 0)
		{
			Vector3 vector = default(Vector3);
			vector = new Vector3(0f, 340f, 0f);
			Vector3 vector2 = default(Vector3);
			vector2 = new Vector3(0f, -125f, 0f);
			BanTrangBiItem banTrangBiItem = ItemList[0];
			BanTrangBiItem banTrangBiItem2 = ItemList[ItemList.Count - 1];
			ItemList.RemoveAt(ItemList.Count - 1);
			ItemList.Insert(0, banTrangBiItem2);
			banTrangBiItem2.transform.localPosition = banTrangBiItem.transform.localPosition - vector2;
			banTrangBiItem2.Set(trangBiList[num]);
			if (listSelectedTrangBiData != null && listSelectedTrangBiData.Count > 0 && listSelectedTrangBiData.Contains(trangBiList[num]))
			{
				banTrangBiItem2.checkBox.isChecked = true;
			}
			else
			{
				banTrangBiItem2.checkBox.isChecked = false;
			}
		}
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			UnityEngine.Object.Destroy(instance.gameObject);
		}
		instance = null;
	}

	public static void Create(Func<List<int>, bool> onFinish, List<int> ignore_list, LoaiTrangBi loaiTrangBi)
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("popup/PopupBanTrangBi"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		instance = gameObject.GetComponent<PopUpBanTrangBi>();
		if (instance.listSelectedTrangBiData != null)
		{
			instance.listSelectedTrangBiData.Clear();
		}
		instance.SyncWithNetworkData(ignore_list, loaiTrangBi);
		instance.OnFinish = onFinish;
	}

	public void SyncWithNetworkData(List<int> ignore_list, LoaiTrangBi loaiTrangBi)
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			UnityEngine.Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
		trangBiList.Clear();
		startItemGUI_Idx = 0;
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 340f, 0f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, -125f, 0f);
		if (startItemGUI_Idx < 0)
		{
			startItemGUI_Idx = 0;
		}
		foreach (UserInfo.TrangBiData trangBi in GameManager.instance.m_GameClient.UserInfo.TrangBiList)
		{
			if (ConfigManager.instance.m_dicTrangBi.ContainsKey(trangBi.Name))
			{
				if ((loaiTrangBi == LoaiTrangBi.None || loaiTrangBi == TrangBiCfg.GetLoaiTrangBi(trangBi.Name)) && (ignore_list == null || !ignore_list.Contains(trangBi.ID)))
				{
					trangBiList.Add(trangBi);
				}
			}
			else
			{
				EGDebug.Log("Trang bị không tồn tại " + trangBi.Name);
			}
		}
		trangBiList.Sort((UserInfo.TrangBiData x, UserInfo.TrangBiData y) => ConfigManager.instance.CompareTrangBi(y.Name, y.Level, x.Name, x.Level));
		int num = 0;
		if (trangBiList.Count <= 0)
		{
			messLabel.text = Localization.instance.Get("KhongCoTrangBiDeBanLabel");
		}
		else
		{
			messLabel.text = string.Empty;
		}
		for (int num2 = startItemGUI_Idx; num2 < trangBiList.Count; num2++)
		{
			UserInfo.TrangBiData data = trangBiList[num2];
			BanTrangBiItem component = ((GameObject)UnityEngine.Object.Instantiate(TrangBiBanItemPrefab)).GetComponent<BanTrangBiItem>();
			component.transform.parent = ItemRoot.transform;
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			component.transform.localPosition = vector;
			vector += vector2;
			component.Set(data);
			UIEventListener.Get(component.checkBox.gameObject).onClick = onClick_CheckBox;
			ItemList.Add(component);
			num++;
			if (num >= 12)
			{
				break;
			}
		}
		UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
		component2.ResetPosition();
	}

	public void updateBacNhanLai()
	{
		long num = 0L;
		foreach (UserInfo.TrangBiData listSelectedTrangBiDatum in listSelectedTrangBiData)
		{
			num += ConfigManager.instance.GetGiaBanTrangBi(listSelectedTrangBiDatum);
		}
		bacNhanLaiLabel.text = Localization.instance.Get("NhanLaiLabel") + ": " + num;
	}

	public void onClick_CheckBox(GameObject go)
	{
		BanTrangBiItem component = go.transform.parent.GetComponent<BanTrangBiItem>();
		if (component != null && component.m_Data != null)
		{
			if (component.checkBox.isChecked && !listSelectedTrangBiData.Contains(component.m_Data))
			{
				listSelectedTrangBiData.Add(component.m_Data);
			}
			else if (!component.checkBox.isChecked && listSelectedTrangBiData.Contains(component.m_Data))
			{
				listSelectedTrangBiData.Remove(component.m_Data);
			}
			updateBacNhanLai();
		}
	}

	public void OnOkClick()
	{
		if (listSelectedTrangBiData != null && listSelectedTrangBiData.Count > 0)
		{
			List<int> list = new List<int>();
			foreach (UserInfo.TrangBiData listSelectedTrangBiDatum in listSelectedTrangBiData)
			{
				if (listSelectedTrangBiDatum != null)
				{
					list.Add(listSelectedTrangBiDatum.ID);
				}
			}
			OnFinish(list);
		}
		DestroyPopup();
	}

	public void OnCancelClick()
	{
		DestroyPopup();
	}
}
