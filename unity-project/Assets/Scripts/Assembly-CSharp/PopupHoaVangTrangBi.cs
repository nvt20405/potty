using System;
using System.Collections.Generic;
using UnityEngine;

public class PopupHoaVangTrangBi : MonoBehaviour
{
	private const int maxItemCount = 12;

	public static PopupHoaVangTrangBi instance;

	public GameObject ItemRoot;

	public GameObject TrangBiBanItemPrefab;

	public UILabel messLabel;

	public UILabel lbDiemNhanDuoc;

	public Func<List<int>, bool> OnFinish;

	private List<UserInfo.TrangBiData> listSelectedTrangBiData = new List<UserInfo.TrangBiData>();

	private List<UserInfo.TrangBiData> trangBiList = new List<UserInfo.TrangBiData>();

	private List<BanTrangBiItem> ItemList = new List<BanTrangBiItem>();

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private int startItemGUI_Idx;

	private int DiemNhanDuoc;

	private void Start()
	{
		dragPanel = ItemRoot.GetComponent<UIDraggablePanel>();
		panel = ItemRoot.GetComponent<UIPanel>();
		lbDiemNhanDuoc.text = Localization.instance.Get("DiemNhanLabelHoaVang") + ": 0";
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			UnityEngine.Object.Destroy(instance.gameObject);
		}
		instance = null;
	}

	public static void Create(Func<List<int>, bool> onFinish, string codeName, int DiemNhanDuoc)
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("popup/PopupHoaVangTrangBi"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		instance = gameObject.GetComponent<PopupHoaVangTrangBi>();
		if (instance.listSelectedTrangBiData != null)
		{
			instance.listSelectedTrangBiData.Clear();
		}
		instance.DiemNhanDuoc = DiemNhanDuoc;
		instance.SyncWithNetworkData(codeName);
		instance.OnFinish = onFinish;
	}

	public void SyncWithNetworkData(string codeName)
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			UnityEngine.Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
		trangBiList.Clear();
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 340f, -1f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, -125f, 0f);
		foreach (UserInfo.TrangBiData trangBi in GameManager.instance.m_GameClient.UserInfo.TrangBiList)
		{
			if (ConfigManager.instance.m_dicTrangBi.ContainsKey(trangBi.Name))
			{
				if (trangBi.Name == codeName && trangBi.HID <= 0)
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
		if (trangBiList.Count <= 0)
		{
			messLabel.text = Localization.instance.Get("KhongCoTrangBiDeHoaVangLabel");
		}
		else
		{
			messLabel.text = string.Empty;
		}
		for (int num = 0; num < trangBiList.Count; num++)
		{
			UserInfo.TrangBiData data = trangBiList[num];
			BanTrangBiItem component = ((GameObject)UnityEngine.Object.Instantiate(TrangBiBanItemPrefab)).GetComponent<BanTrangBiItem>();
			component.transform.parent = ItemRoot.transform;
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			component.transform.localPosition = vector;
			vector += vector2;
			component.Set(data);
			UIEventListener.Get(component.checkBox.gameObject).onClick = onClick_CheckBox;
			ItemList.Add(component);
		}
		UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
		component2.ResetPosition();
	}

	public void updateDiemNhanDuoc()
	{
		float num = 0f;
		if (listSelectedTrangBiData.Count > 0)
		{
			for (int i = 0; i < listSelectedTrangBiData.Count; i++)
			{
				num = ((listSelectedTrangBiData[i].TinhLuyenLevel <= 0) ? (num + (float)DiemNhanDuoc) : (num + ((float)DiemNhanDuoc + (float)(DiemNhanDuoc * listSelectedTrangBiData[i].TinhLuyenLevel * 10 / 100))));
			}
		}
		lbDiemNhanDuoc.text = Localization.instance.Get("DiemNhanLabelHoaVang") + ": " + num;
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
			updateDiemNhanDuoc();
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
