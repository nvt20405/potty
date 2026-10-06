using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupVongQuay : MonoBehaviour
{
	public static PopupVongQuay instance;

	public GameObject group;

	public GameObject selection;

	public List<GameObject> PhanThuongAvatarList;

	public GameObject ItemRoot;

	public Vector3 itemPos;

	public Vector3 itemOffset;

	public UILabel header;

	public UILabel quantityLabel;

	private int id;

	public bool isSelect;

	private bool Spinning;

	public static PopupVongQuay Create(int id, string header, List<PhanThuongResponse> listPhanThuong)
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		Object obj = Object.Instantiate(Resources.Load("popup/PopupVongQuay"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		instance = gameObject.GetComponent<PopupVongQuay>();
		instance.Set(id, header, GameManager.instance.m_GameClient.UserInfo.ServerInfo.VongQuayInfo.PhanThuongVongQuay);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		return instance;
	}

	public void Close()
	{
		DestroyPopup();
	}

	public void Use()
	{
		GameManager.instance.m_GameClient.RequestVongQuay(id);
	}

	public static void Hide()
	{
		if (instance != null)
		{
			instance.gameObject.SetActive(false);
		}
	}

	public static void Show()
	{
		if (instance != null)
		{
			instance.gameObject.SetActive(true);
		}
	}

	public void BeginSpin(int pos, PhanThuongResponse pt)
	{
		if (Spinning)
		{
			return;
		}
		quantityLabel.text = Localization.instance.Get("SoLuongLabel") + ": 0";
		if (GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_VONG_QUAY"))
		{
			quantityLabel.text = Localization.instance.Get("SoLuongLabel") + ": " + GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_VONG_QUAY").Quantity;
		}
		StartCoroutine(Spin(pos, pt));
	}

	private IEnumerator Spin(int pos, PhanThuongResponse pt)
	{
		Spinning = true;
		int count = 0;
		int round = Random.Range(1, 3);
		for (int i = 1; i <= round * 8 + pos; i++)
		{
			selection.transform.position = PhanThuongAvatarList[count % PhanThuongAvatarList.Count].transform.position;
			count++;
			yield return new WaitForSeconds(0.2f);
		}
		selection.transform.position = PhanThuongAvatarList[pos].transform.position;
		yield return new WaitForSeconds(0.5f);
		if (pt != null)
		{
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTitle"), Localization.instance.Get("PhanThuongDesc"), pt, Show);
			Hide();
		}
		Spinning = false;
	}

	public void Set(int id, string header, List<PhanThuongResponse> listPhanThuong)
	{
		this.id = id;
		this.header.text = header;
		quantityLabel.text = Localization.instance.Get("SoLuongLabel") + ": 0";
		if (GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_VONG_QUAY"))
		{
			quantityLabel.text = Localization.instance.Get("SoLuongLabel") + ": " + GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_VONG_QUAY").Quantity;
		}
		instance.CreateOtherAvatar(listPhanThuong);
	}

	public void Update()
	{
		if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBattle)
		{
			NGUITools.SetActive(group, false);
		}
		else
		{
			NGUITools.SetActive(group, true);
		}
	}

	public void OnCloseClick()
	{
		DestroyPopup();
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			instance.gameObject.SetActive(false);
			Object.Destroy(instance.gameObject);
		}
		instance = null;
	}

	private void CreateOtherAvatar(List<PhanThuongResponse> res)
	{
		int num = 0;
		PhanThuongAvatarList = new List<GameObject>();
		foreach (PhanThuongResponse re in res)
		{
			num++;
			PhanThuongItem component = ((GameObject)Object.Instantiate(Resources.Load("prefabs/PhanThuongItem"))).GetComponent<PhanThuongItem>();
			component.transform.parent = ItemRoot.transform;
			component.transform.localScale = Vector3.one;
			component.transform.localPosition = itemPos;
			if (num <= 2)
			{
				itemPos += itemOffset.x * Vector3.right;
			}
			else if (num <= 4)
			{
				itemPos += itemOffset.y * Vector3.down;
			}
			else if (num <= 6)
			{
				itemPos += itemOffset.x * Vector3.left;
			}
			else
			{
				itemPos += itemOffset.y * Vector3.up;
			}
			component.nameLabel.gameObject.SetActive(false);
			component.countLabel.gameObject.SetActive(false);
			PhanThuongAvatarList.Add(component.gameObject);
			component.Set(re.PhanThuongList[0], true);
		}
	}
}
