using UnityEngine;

public class ScreenDanhSachHuyenKhi : ScreenBase
{
	public GameObject DSHuyenKhiPrefab;

	public EGGUIGrid grid;

	public void OnBack_Click()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHuyenKhiMain);
	}

	public override void OnActive()
	{
		base.OnActive();
		if (GameManager.instance.m_GameClient.UserInfo.HuyenKhiList != null)
		{
			GameManager.instance.m_GameClient.UserInfo.HuyenKhiList.Sort((UserInfo.HuyenKhi x, UserInfo.HuyenKhi y) => ConfigManager.instance.CompareLevel(x.Level, y.Level));
			foreach (Transform item in grid.transform)
			{
				Transform transform2 = item;
				Object.Destroy(transform2.gameObject);
			}
			DSHuyenKhiPrefab.SetActive(true);
			foreach (UserInfo.HuyenKhi huyenKhi in GameManager.instance.m_GameClient.UserInfo.HuyenKhiList)
			{
				DSSelectHuyenKhiItem component = ((GameObject)Object.Instantiate(DSHuyenKhiPrefab)).GetComponent<DSSelectHuyenKhiItem>();
				component.transform.parent = grid.transform;
				component.transform.localScale = new Vector3(0.95f, 0.9f, 1f);
				component.transform.localPosition = Vector3.zero;
				component.Set(huyenKhi);
			}
			grid.repositionNow = true;
		}
		DSHuyenKhiPrefab.SetActive(false);
	}

	public void UpdateHuyenKhi(UserInfo.HuyenKhi hk)
	{
		DSSelectHuyenKhiItem[] componentsInChildren = grid.transform.GetComponentsInChildren<DSSelectHuyenKhiItem>();
		DSSelectHuyenKhiItem[] array = componentsInChildren;
		DSSelectHuyenKhiItem[] array2 = array;
		foreach (DSSelectHuyenKhiItem dSSelectHuyenKhiItem in array2)
		{
			if (dSSelectHuyenKhiItem.m_info.ID == hk.ID)
			{
				dSSelectHuyenKhiItem.Set(hk);
				break;
			}
		}
	}
}
