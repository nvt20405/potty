using System.Collections.Generic;
using UnityEngine;

public class PopupSonMonChienSu : MonoBehaviour
{
	public static PopupSonMonChienSu instance;

	public PopupSonMonChienSuItem BaseItemSonMon;

	public float ItemDeltaPos;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public static void Create()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
		}
		instance = ((GameObject)Object.Instantiate(Resources.Load("popup/PopupSonMonChienSu"))).GetComponent<PopupSonMonChienSu>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = Vector3.one;
		instance.SetSonMon();
	}

	public void SetSonMon()
	{
		BaseItemSonMon.gameObject.SetActive(true);
		foreach (Transform item in BaseItemSonMon.transform.parent)
		{
			Transform transform2 = item;
			if (transform2 != BaseItemSonMon.transform)
			{
				Object.Destroy(transform2.gameObject);
			}
		}
		int num = 0;
		List<UserInfo.SonMonLog> list = GameManager.instance.m_GameClient.UserInfo.SonMon.ChienSu.FindAll((UserInfo.SonMonLog cs) => cs.GID2 == GameManager.instance.m_GameClient.UserInfo.Gamer.ID && cs.SID2 == GameManager.instance.ServerID);
		for (int num2 = 0; num2 < list.Count; num2++)
		{
			PopupSonMonChienSuItem component = ((GameObject)Object.Instantiate(BaseItemSonMon.gameObject)).GetComponent<PopupSonMonChienSuItem>();
			component.transform.parent = BaseItemSonMon.transform.parent;
			component.transform.localScale = Vector3.one;
			component.transform.localPosition = BaseItemSonMon.transform.localPosition + num * Vector3.down * ItemDeltaPos;
			component.Set(list[num2], false);
			num++;
		}
		BaseItemSonMon.gameObject.SetActive(false);
	}

	public void SetGiangHo()
	{
		BaseItemSonMon.gameObject.SetActive(true);
		foreach (Transform item in BaseItemSonMon.transform.parent)
		{
			Transform transform2 = item;
			if (transform2 != BaseItemSonMon.transform)
			{
				Object.Destroy(transform2.gameObject);
			}
		}
		int num = 0;
		List<UserInfo.SonMonLog> list = GameManager.instance.m_GameClient.UserInfo.SonMon.ChienSu.FindAll((UserInfo.SonMonLog cs) => cs.GID2 != GameManager.instance.m_GameClient.UserInfo.Gamer.ID && (cs.SID2 == GameManager.instance.ServerID || cs.SID1 == GameManager.instance.ServerID));
		for (int num2 = 0; num2 < list.Count; num2++)
		{
			PopupSonMonChienSuItem component = ((GameObject)Object.Instantiate(BaseItemSonMon.gameObject)).GetComponent<PopupSonMonChienSuItem>();
			component.transform.parent = BaseItemSonMon.transform.parent;
			component.transform.localScale = Vector3.one;
			component.transform.localPosition = BaseItemSonMon.transform.localPosition + num * Vector3.down * ItemDeltaPos;
			component.Set(list[num2], true);
			num++;
		}
		BaseItemSonMon.gameObject.SetActive(false);
	}

	public void Close()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}
}
