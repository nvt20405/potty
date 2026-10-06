using System;
using System.Collections.Generic;
using UnityEngine;

public class PopupSelectHuyenKhi : MonoBehaviour
{
	public EGGUIGrid grid;

	public GameObject HuyenKhiPrefab;

	public static PopupSelectHuyenKhi instance;

	public Func<int, bool> OnFinish;

	public UILabel lbTitle;

	private UserInfo.HuyenKhi m_huyenKhiCuongHoa;

	private List<PopupSelectHuyenKhiItem> ItemList = new List<PopupSelectHuyenKhiItem>();

	private void Start()
	{
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			UnityEngine.Object.Destroy(instance.gameObject);
		}
		instance = null;
	}

	public static void Create(UserInfo.HuyenKhi cuongHoa, Func<int, bool> onFinish, bool ignoreUsed, int ignoreId, int equalCuongHoaLevel = -1, int equalLoaiHK = -1)
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("popup/PopupSelectHuyenKhi"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		instance = gameObject.GetComponent<PopupSelectHuyenKhi>();
		instance.Sync(cuongHoa, ignoreUsed, ignoreId, equalCuongHoaLevel, equalLoaiHK);
		instance.OnFinish = onFinish;
	}

	public void Sync(UserInfo.HuyenKhi cuongHoa, bool ignoreUsed, int ignoreId, int equalCuongHoaLevel = -1, int equalLoaiHK = -1)
	{
		m_huyenKhiCuongHoa = cuongHoa;
		ItemList.Clear();
		if (GameManager.instance.m_GameClient.UserInfo.HuyenKhiList != null)
		{
			GameManager.instance.m_GameClient.UserInfo.HuyenKhiList.Sort((UserInfo.HuyenKhi x, UserInfo.HuyenKhi y) => ConfigManager.instance.CompareLevel(x.Level, y.Level));
			foreach (Transform item in grid.transform)
			{
				Transform transform2 = item;
				UnityEngine.Object.Destroy(transform2.gameObject);
			}
			HuyenKhiPrefab.SetActive(true);
			foreach (UserInfo.HuyenKhi huyenKhi in GameManager.instance.m_GameClient.UserInfo.HuyenKhiList)
			{
				if (huyenKhi.ID == ignoreId || (equalCuongHoaLevel > 0 && huyenKhi.Level != equalCuongHoaLevel))
				{
					continue;
				}
				if (equalLoaiHK > 0)
				{
					HuyenKhiCfg value;
					ConfigManager.instance.m_dicHuyenKhi.TryGetValue(huyenKhi.Name, out value);
					if (value == null || value.Loai != equalLoaiHK)
					{
						continue;
					}
				}
				if (!ignoreUsed || (ignoreUsed && huyenKhi.ChienHonID == 0))
				{
					PopupSelectHuyenKhiItem component = ((GameObject)UnityEngine.Object.Instantiate(HuyenKhiPrefab)).GetComponent<PopupSelectHuyenKhiItem>();
					component.transform.parent = grid.transform;
					component.transform.localScale = new Vector3(0.9f, 0.9f, 1f);
					component.transform.localPosition = Vector3.zero;
					component.Set(huyenKhi);
					ItemList.Add(component);
				}
			}
			grid.repositionNow = true;
		}
		HuyenKhiPrefab.SetActive(false);
	}

	public void OnOkClick()
	{
		PopupSelectHuyenKhiItem popupSelectHuyenKhiItem = null;
		foreach (PopupSelectHuyenKhiItem item in ItemList)
		{
			UICheckbox componentInChildren = item.GetComponentInChildren<UICheckbox>();
			if (componentInChildren.isChecked)
			{
				popupSelectHuyenKhiItem = item;
				break;
			}
		}
		if ((bool)popupSelectHuyenKhiItem)
		{
			if (OnFinish != null)
			{
				OnFinish(popupSelectHuyenKhiItem.m_info.ID);
			}
			else
			{
				PopupCuongHoaHuyenKhi.Create(m_huyenKhiCuongHoa, popupSelectHuyenKhiItem.m_info.ID);
			}
			DestroyPopup();
		}
	}

	public void OnCancelClick()
	{
		DestroyPopup();
	}
}
