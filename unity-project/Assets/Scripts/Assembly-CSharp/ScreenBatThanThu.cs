using System.Collections;
using LitJson;
using UnityEngine;

public class ScreenBatThanThu : ScreenBase
{
	public ScreenBatThanThu3D screen3dObj;

	public GameObject MainGroup;

	public GameObject GameGroup;

	public GameObject SuccessGroup;

	public GameObject FailGroup;

	public UILabel TurnLabel;

	public UISprite AutoResolveGameSprite;

	public UILabel AutoResolveGameLabel;

	public UISprite AutoResolveFailSprite;

	public UILabel AutoResolveFailLabel;

	public UISprite NewGameMainSprite;

	public UILabel NewGameMainLabel;

	public UISprite NewGameGameSprite;

	public UILabel NewGameGameLabel;

	public UISprite NewGameFailSprite;

	public UILabel NewGameFailLabel;

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
		GUIManager.ShowGadgets(6);
		AudioListener component = GUIManager.instance.cam2D.GetComponent<AudioListener>();
		if (component != null)
		{
			component.enabled = false;
		}
		if (screen3dObj == null)
		{
			StartCoroutine(SpawnScreen3D());
		}
		Set();
	}

	private void CalculateCost()
	{
		int num = 0;
		int num2 = 0;
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("AutoBatThu"))
		{
			for (int i = 1; i < 20; i++)
			{
				if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("AutoBatThu" + i + ";"))
				{
					num2 = i * 10;
					break;
				}
			}
		}
		else
		{
			num2 = 10;
		}
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("ThanThuDao"))
		{
			for (int j = 1; j < 20; j++)
			{
				if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("ThanThuDao" + j + ";"))
				{
					num = ((j > 3) ? (j - 2) : 0) * 10;
					break;
				}
			}
		}
		else
		{
			num = 0;
		}
		if (num2 > 0)
		{
			AutoResolveGameLabel.text = num2.ToString();
			AutoResolveFailLabel.text = num2.ToString();
			AutoResolveFailSprite.gameObject.SetActive(true);
			AutoResolveGameSprite.gameObject.SetActive(true);
		}
		else
		{
			AutoResolveGameLabel.text = string.Empty;
			AutoResolveFailLabel.text = string.Empty;
			AutoResolveFailSprite.gameObject.SetActive(false);
			AutoResolveGameSprite.gameObject.SetActive(false);
		}
		if (num > 0)
		{
			NewGameMainLabel.text = num.ToString();
			NewGameGameLabel.text = num.ToString();
			NewGameFailLabel.text = num.ToString();
			NewGameMainSprite.gameObject.SetActive(true);
			NewGameGameSprite.gameObject.SetActive(true);
			NewGameFailSprite.gameObject.SetActive(true);
		}
		else
		{
			NewGameMainLabel.text = string.Empty;
			NewGameGameLabel.text = string.Empty;
			NewGameFailLabel.text = string.Empty;
			NewGameMainSprite.gameObject.SetActive(false);
			NewGameGameSprite.gameObject.SetActive(false);
			NewGameFailSprite.gameObject.SetActive(false);
		}
	}

	private IEnumerator SpawnScreen3D()
	{
		PopupLoading.Create();
		EGResourceAsyncLoader loader = EGResourceAsyncLoader.Load("gui/screens3d/ScreenBatThu3D");
		loader.OnLoading = (float e) =>
		{
			if (PopupLoading.instance != null)
			{
				PopupLoading.instance.SetAmmount(e);
			}
		};
		while (!loader.IsDone)
		{
			yield return null;
		}
		PopupLoading.DestroyPopup();
		Object obj = Object.Instantiate(loader.Asset);
		GameObject scr3D = (GameObject)((obj is GameObject) ? obj : null);
		scr3D.transform.parent = GUIManager.instance.ScreenContainer3D;
		scr3D.transform.localScale = Vector3.one;
		scr3D.transform.localRotation = Quaternion.identity;
		scr3D.transform.localPosition = Vector3.zero;
		child3Dscreen = scr3D;
		screen3dObj = scr3D.GetComponent<ScreenBatThanThu3D>();
		GUIManager.instance.cam2D.clearFlags = CameraClearFlags.Depth;
	}

	public void Set()
	{
		CalculateCost();
		GameGroup.SetActive(false);
		MainGroup.SetActive(true);
		SuccessGroup.SetActive(false);
		FailGroup.SetActive(false);
	}

	public override void OnDeactive()
	{
		AudioListener component = GUIManager.instance.cam2D.GetComponent<AudioListener>();
		if (component != null)
		{
			component.enabled = true;
		}
		base.OnDeactive();
		if (screen3dObj != null)
		{
			screen3dObj.gameObject.SetActive(false);
			Object.Destroy(screen3dObj.gameObject);
		}
		screen3dObj = null;
	}

	public void OnStartThanThuDaoBtn()
	{
		GameManager.instance.m_GameClient.RequestStartThanThuDao();
	}

	public void StartBatThanThu(string thanthudaoInfo)
	{
		CalculateCost();
		GameGroup.SetActive(true);
		MainGroup.SetActive(false);
		SuccessGroup.SetActive(false);
		FailGroup.SetActive(false);
		screen3dObj.SpawnMap(JsonMapper.ToObject<ThanThuDaoInfo>(thanthudaoInfo));
	}

	public void OnReceiveThanThu(UserInfo.PetInfo thanthu)
	{
		CalculateCost();
		GameGroup.SetActive(false);
		MainGroup.SetActive(true);
		SuccessGroup.SetActive(false);
		FailGroup.SetActive(false);
		PopupThanThuInfo.Create(thanthu, false);
	}

	public void OnGameOver()
	{
		CalculateCost();
		GameGroup.SetActive(false);
		MainGroup.SetActive(false);
		SuccessGroup.SetActive(false);
		FailGroup.SetActive(true);
	}

	public void OnAutoResolve()
	{
		GameManager.instance.m_GameClient.RequestAutoResolveThanThuDao();
	}

	public void OnHelp()
	{
		ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
		screenHelpInfo.setByLevel(11, 1);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
	}
}
