using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenVongQuay : ScreenBase
{
	public static ScreenVongQuay instance;

	public GameObject group;

	public GameObject selection;

	public List<GameObject> PhanThuongAvatarList;

	public GameObject ItemRoot;

	public Vector3 itemPos;

	public Vector3 itemOffset;

	public UILabel header;

	public UILabel quantityLabel;

	public static int id;

	public bool isSelect;

	private bool Spinning;

	public GameObject mainGrp;

	public GameObject topGrp;

	public GameObject topRoot;

	public TopVongQuayItem baseItem;

	public List<GameObject> ptPivots;

	private bool isQuay10Lan;

	private List<PhanThuongResponse.PhanThuong> listPT = new List<PhanThuongResponse.PhanThuong>();

	private int countQuayThuong;

	public override void OnActive()
	{
		base.OnActive();
		AudioListener component = GUIManager.instance.cam2D.GetComponent<AudioListener>();
		if (component != null)
		{
			component.enabled = false;
		}
		GUIManager.ShowGadgets(-1);
		topGrp.SetActive(false);
		mainGrp.SetActive(true);
		Debug.Log("Vong quay active");
		base.gameObject.SetActive(true);
		instance = this;
		Set(GameManager.instance.m_GameClient.UserInfo.ServerInfo.VongQuayInfo.ThongBao, GameManager.instance.m_GameClient.UserInfo.ServerInfo.VongQuayInfo.PhanThuongVongQuay);
		Spinning = false;
	}

	public void Use()
	{
		if (!Spinning)
		{
			if ((GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_VONG_QUAY") && GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_VONG_QUAY").Quantity <= 0) || !GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_VONG_QUAY"))
			{
				MessagePopup.Create(Localization.instance.Get("ThongBaoKhongDuVatPhamVongQuay"));
			}
			else
			{
				GameManager.instance.m_GameClient.RequestVongQuay(id);
			}
		}
	}

	public void Use10()
	{
		if (!Spinning)
		{
			if ((GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_VONG_QUAY") && GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_VONG_QUAY").Quantity < 10) || !GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_VONG_QUAY"))
			{
				MessagePopup.Create(Localization.instance.Get("ThongBaoKhongDuVatPhamVongQuay"));
			}
			else
			{
				startQuay10Lan();
			}
		}
	}

	public void startQuay10Lan()
	{
		isQuay10Lan = true;
		listPT.Clear();
		countQuayThuong = 1;
		GameManager.instance.m_GameClient.RequestVongQuay(id);
	}

	public void continueQuayThuong()
	{
		if (countQuayThuong <= 10)
		{
			if ((countQuayThuong == 10 && listPT.Count > 0) || (GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_VONG_QUAY").Quantity == 0 && listPT.Count > 0))
			{
				PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
				phanThuongResponse.PhanThuongList = listPT;
				PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTitle"), Localization.instance.Get("PhanThuongDesc"), phanThuongResponse, Show);
				Hide();
				isQuay10Lan = false;
				countQuayThuong = 0;
				listPT.Clear();
			}
			else
			{
				countQuayThuong++;
				GameManager.instance.m_GameClient.RequestVongQuay(id);
			}
		}
	}

	public void GetTop()
	{
		GameManager.instance.m_GameClient.RequestGetTopVongQuay();
	}

	public void ShowTop(Dictionary<string, int> TopList)
	{
		topGrp.SetActive(true);
		mainGrp.SetActive(false);
		foreach (Transform item in topRoot.transform)
		{
			Transform transform2 = item;
			if (transform2.gameObject != baseItem.gameObject)
			{
				Object.Destroy(transform2.gameObject);
			}
		}
		int num = 0;
		foreach (KeyValuePair<string, int> Top in TopList)
		{
			baseItem.Create(num + 1, Top.Key, Top.Value, GameManager.instance.m_GameClient.UserInfo.ServerInfo.VongQuayInfo.PhanThuongTopVongQuay[num]);
			num++;
		}
	}

	public void CloseTop()
	{
		topGrp.SetActive(false);
		mainGrp.SetActive(true);
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
			yield return new WaitForSeconds(0.1f);
		}
		selection.transform.position = PhanThuongAvatarList[pos].transform.position;
		yield return new WaitForSeconds(0.3f);
		if (pt != null)
		{
			if (isQuay10Lan)
			{
				for (int j = 0; j < pt.PhanThuongList.Count; j++)
				{
					listPT.Add(pt.PhanThuongList[j]);
				}
				continueQuayThuong();
			}
			else
			{
				PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTitle"), Localization.instance.Get("PhanThuongDesc"), pt, Show);
				Hide();
			}
		}
		Spinning = false;
	}

	public void Set(string header, List<PhanThuongResponse> listPhanThuong)
	{
		this.header.text = header;
		quantityLabel.text = Localization.instance.Get("SoLuongLabel") + ": 0";
		if (GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_VONG_QUAY"))
		{
			quantityLabel.text = Localization.instance.Get("SoLuongLabel") + ": " + GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_VONG_QUAY").Quantity;
		}
		CreateOtherAvatar(listPhanThuong);
	}

	public void OnCloseClick()
	{
		GUIManager.GoBackLastScreen();
		GUIManager.ShowGadgets(6);
	}

	private void CreateOtherAvatar(List<PhanThuongResponse> res)
	{
		for (int num = ItemRoot.transform.childCount - 1; num >= 0; num--)
		{
			Object.Destroy(ItemRoot.transform.GetChild(num).gameObject);
		}
		int num2 = 0;
		PhanThuongAvatarList = new List<GameObject>();
		foreach (PhanThuongResponse re in res)
		{
			num2++;
			PhanThuongItem component = ((GameObject)Object.Instantiate(Resources.Load("Prefabs/PhanThuongItem"))).GetComponent<PhanThuongItem>();
			component.transform.parent = ItemRoot.transform;
			component.transform.localScale = Vector3.one;
			component.transform.localPosition = ptPivots[num2 - 1].transform.localPosition - ptPivots[num2 - 1].transform.localPosition.z * Vector3.forward;
			component.nameLabel.gameObject.SetActive(false);
			component.countLabel.gameObject.SetActive(false);
			component.gameObject.layer = LayerMask.NameToLayer("Default");
			foreach (Transform item in component.transform)
			{
				Transform transform2 = item;
				transform2.gameObject.layer = LayerMask.NameToLayer("Default");
			}
			PhanThuongAvatarList.Add(component.gameObject);
			component.Set(re.PhanThuongList[0], false);
		}
	}
}
