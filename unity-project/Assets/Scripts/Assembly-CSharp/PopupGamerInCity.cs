using System.Collections;
using UnityEngine;

public class PopupGamerInCity : MonoBehaviour
{
	public static PopupGamerInCity instance;

	public NhanVatAvatar avatar;

	public UILabel nameLabel;

	public GameObject panelGroup;

	public GameObject btnAddFriend;

	public GameObject btnThachDau;

	public GameObject btnBatCoc;

	public GameObject btnMoiRuou;

	public GameObject leftEdge;

	public int GID { get; set; }

	public bool IsOnline { get; set; }

	private void Awake()
	{
		if (GUIManager.instance != null)
		{
			leftEdge.GetComponent<UIAnchor>().widgetContainer = GUIManager.instance.GameFrame;
		}
	}

	public static PopupGamerInCity Create(int gid, string uName, string nvName, bool isOnline)
	{
		DestroyPopup();
		Object obj = Object.Instantiate(Resources.Load("Popup/PopupGamerInCity"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		instance = gameObject.GetComponent<PopupGamerInCity>();
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		instance.SetInfo(gid, uName, nvName, isOnline);
		instance.FlyIn();
		return instance;
	}

	public void FlyIn()
	{
		StartCoroutine(FlyInRoutine());
	}

	private IEnumerator FlyInRoutine()
	{
		float yPos = -180f;
		panelGroup.transform.localPosition = new Vector3(-500f, yPos);
		yield return null;
		yield return null;
		TweenPosition.Begin(panelGroup, 0.2f, new Vector3(leftEdge.transform.localPosition.x, yPos));
		Object.Destroy(instance.gameObject, 3f);
	}

	public void SetInfo(int gid, string uName, string nvName, bool isOnline)
	{
		GID = gid;
		string value = "BatCoc;";
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains(value))
		{
			btnBatCoc.SetActive(false);
		}
		if (uName.Contains("##khithe"))
		{
			uName = uName.Substring(11);
		}
		nameLabel.text = uName;
		avatar.Set(nvName);
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		bool flag = false;
		IsOnline = isOnline;
		if (userInfo.BanBeList != null)
		{
			for (int i = 0; i < userInfo.BanBeList.Count; i++)
			{
				if (userInfo.BanBeList[i].GID == GID)
				{
					flag = true;
					break;
				}
			}
		}
		btnAddFriend.collider.enabled = !flag;
		btnBatCoc.gameObject.SetActive(!isOnline);
		btnMoiRuou.gameObject.SetActive(isOnline);
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	private void OnThongTinClick()
	{
		XemThongTinMonPhaiRequest xemThongTinMonPhaiRequest = new XemThongTinMonPhaiRequest();
		xemThongTinMonPhaiRequest.TargetGID = GID;
		GameManager.instance.m_GameClient.RequestXemThongTinMonPhai(xemThongTinMonPhaiRequest);
		DestroyPopup();
	}

	private void OnMoiRuouBtnClick()
	{
		if (IsOnline)
		{
			GameManager.instance.m_GameClient.RequestTangTheLuc(GID);
		}
		else
		{
			MessagePopup.Create(string.Format(Localization.instance.Get("PopupGamerInCityMoiRuouOfflineMsg"), nameLabel.text));
		}
		DestroyPopup();
	}

	private void OnKetBanBtnClick()
	{
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		for (int i = 0; i < userInfo.BanBeList.Count; i++)
		{
			if (userInfo.BanBeList[i].GID == GID)
			{
				if (userInfo.BanBeList[i].Status == UserInfo.BanBeData.BanBeStatus.CONFIRMED)
				{
					MessagePopup.Create(Localization.instance.Get("DaLaBanBeRoi"));
					return;
				}
				if (userInfo.BanBeList[i].Status == UserInfo.BanBeData.BanBeStatus.REQUESTING)
				{
					MessagePopup.Create(Localization.instance.Get("DaGuiYeuCauKetBan"));
					return;
				}
			}
		}
		OpBanBeRequest opBanBeRequest = new OpBanBeRequest();
		opBanBeRequest.FriendGID = GID;
		GameManager.instance.m_GameClient.RequestAddBanBe(opBanBeRequest);
		DestroyPopup();
	}

	private void OnThachDauBtnClick()
	{
		if (IsOnline)
		{
			int level = 1;
			if (GameManager.instance != null && GameManager.instance.m_GameClient != null && GameManager.instance.m_GameClient.UserInfo != null && GameManager.instance.m_GameClient.UserInfo.Gamer != null)
			{
				level = GameManager.instance.m_GameClient.UserInfo.Gamer.Level;
			}
			PopupThachDau.Create(GID, nameLabel.text, level);
		}
		else
		{
			GameManager.instance.m_GameClient.RequestThachDau(GID, 0);
		}
		DestroyPopup();
	}

	private void OnBatCocBtnClick()
	{
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.Level < 16)
		{
			MessagePopup.Create(string.Format(Localization.instance.Get("LockTinhNangBatCoc"), 16));
			return;
		}
		GameManager.instance.m_GameClient.RequestBatCoc(GID);
		DestroyPopup();
	}
}
