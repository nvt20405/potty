using System;
using System.Collections;
using UnityEngine;

public class ScreenLogin : ScreenBase
{
	public UIInput m_UserName;

	public UIInput m_UserPassword;

	public UILabel VersionLabel;

	public GameObject grpLogin;

	public GameObject grpPublisher;

	public UISprite logo;

	public UISprite bg;

	public GameObject animNhanVat;

	public GameObject GUIRoot;

	public UITexture IntroVideoTexture;

	private AudioSource audioS;

	private int adaptWidth;

	private int adaptHeight;

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(0);
		if (PlayerPrefs.GetInt("sounds_value", 1) == 1)
		{
			AudioListener.pause = false;
		}
		GameManager.instance.isPlayTrailer = false;
		SetGUI();
	}

	private IEnumerator PlayVideoCoroutine(string videoPath)
	{
		yield return new WaitForEndOfFrame();
		if (PlayerPrefs.GetInt("sounds_value", 1) == 1)
		{
			AudioListener.pause = false;
		}
		GameManager.instance.isPlayTrailer = false;
		SetGUI();
	}

	private void SetGUI()
	{
		GUIRoot.SetActive(true);
		if (IntroVideoTexture != null)
		{
			IntroVideoTexture.gameObject.SetActive(false);
		}
		int num = GameManager.GAMER_VERSION / 10000;
		int num2 = GameManager.GAMER_VERSION % 10000 / 100;
		int num3 = GameManager.GAMER_VERSION % 100;
		VersionLabel.text = Localization.instance.Get("GameVersion") + string.Format(" {0}.{1}.{2}", num, num2, num3);
		EnsureInputReferences();
		if (m_UserName != null)
		{
			m_UserName.text = PlayerPrefs.GetString("UserAccount", string.Empty);
		}
		if (m_UserPassword != null)
		{
			m_UserPassword.text = PlayerPrefs.GetString("UserPassword", string.Empty);
			m_UserPassword.UpdateLabel(true);
		}
		if (animNhanVat != null)
		{
			animNhanVat.SetActive(true);
			animNhanVat.GetComponent<ParticleSystem>().Simulate(0f, true, true);
			animNhanVat.GetComponent<ParticleSystem>().Play();
		}
		grpLogin.SetActive(true);
		grpPublisher.SetActive(false);
		logo.MakePixelPerfect();
		AdaptToScreen();
		StopAllCoroutines();
	}

	private void Update()
	{
		if (Screen.width != adaptWidth || Screen.height != adaptHeight)
		{
			AdaptToScreen();
		}
	}

	private void AdaptToScreen()
	{
		int width = Screen.width;
		int height = Screen.height;
		if (width <= 0 || height <= 0)
		{
			return;
		}
		adaptWidth = width;
		adaptHeight = height;
		float num = (float)height / (float)width;
		int num2 = ((num > 1.775f) ? Mathf.RoundToInt(1136f * num / 1.775f) : 1136);
		float num3 = num2;
		float num4 = num3 * (float)width / (float)height;
		UIRoot uIRoot = null;
		if (GUIManager.instance != null)
		{
			uIRoot = GUIManager.instance.GUI2DRoot;
		}
		if (uIRoot == null)
		{
			uIRoot = NGUITools.FindInParents<UIRoot>(base.gameObject);
		}
		if (uIRoot != null && uIRoot.manualHeight != num2)
		{
			uIRoot.manualHeight = num2;
		}
		if (bg != null)
		{
			Transform transform = bg.transform;
			AutoReflectWidth component = transform.GetComponent<AutoReflectWidth>();
			if (component != null)
			{
				component.enabled = false;
			}
			float z = transform.localPosition.z;
			transform.localScale = new Vector3(num4, num3, 1f);
			transform.localPosition = new Vector3(0f, 0f, z);
		}
		if (logo != null)
		{
			Transform transform2 = logo.transform;
			Vector3 localPosition = transform2.localPosition;
			float y = num3 * 0.5f - 42f - transform2.localScale.y * 0.5f;
			transform2.localPosition = new Vector3(localPosition.x, y, localPosition.z);
		}
		if (VersionLabel != null)
		{
			Transform transform3 = VersionLabel.transform;
			Vector3 localPosition2 = transform3.localPosition;
			float y2 = (0f - num3) * 0.5f + 9f + transform3.localScale.y * 0.5f;
			transform3.localPosition = new Vector3(localPosition2.x, y2, localPosition2.z);
		}
		if (grpPublisher != null)
		{
			Transform transform4 = grpPublisher.transform;
			float num5 = CalcHalfHeight(transform4);
			Vector3 localPosition3 = transform4.localPosition;
			float y3 = (0f - num3) * 0.5f + 69f + num5;
			transform4.localPosition = new Vector3(localPosition3.x, y3, localPosition3.z);
		}
		if (grpLogin != null)
		{
			Transform transform5 = grpLogin.transform;
			float num6 = CalcHalfWidth(transform5) * 2f;
			float num7 = ((num6 <= 0f) ? 1f : Mathf.Min(1f, (num4 - 16f) / num6));
			Vector3 localScale = transform5.localScale;
			if (Mathf.Abs(localScale.x - num7) > 0.001f)
			{
				transform5.localScale = new Vector3(num7, num7, localScale.z);
			}
		}
		if (IntroVideoTexture != null)
		{
			Transform transform6 = IntroVideoTexture.transform;
			float z2 = transform6.localPosition.z;
			transform6.localScale = new Vector3(num4, num3, 1f);
			transform6.localPosition = new Vector3(0f, 0f, z2);
		}
	}

	private static float CalcHalfHeight(Transform t)
	{
		float num = 0f;
		foreach (Transform item in t)
		{
			float num2 = Mathf.Abs(item.localPosition.y) + item.localScale.y * 0.5f;
			if (num2 > num)
			{
				num = num2;
			}
		}
		return num;
	}

	private static float CalcHalfWidth(Transform t)
	{
		float num = 0f;
		foreach (Transform item in t)
		{
			float num2 = Mathf.Abs(item.localPosition.x) + item.localScale.x * 0.5f;
			if (num2 > num)
			{
				num = num2;
			}
		}
		return num;
	}

	private void EnsureInputReferences()
	{
		if (!(m_UserName == null) && !(m_UserPassword == null))
		{
			return;
		}
		UIInput[] componentsInChildren = GetComponentsInChildren<UIInput>(true);
		UIInput[] array = componentsInChildren;
		foreach (UIInput uIInput in array)
		{
			string text = uIInput.name;
			if (text.IndexOf("Name", StringComparison.OrdinalIgnoreCase) >= 0 || text.IndexOf("User", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				m_UserName = uIInput;
			}
			else if (text.IndexOf("Pass", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				m_UserPassword = uIInput;
			}
		}
	}

	public override void OnDeactive()
	{
		base.OnDeactive();
	}

	private void OnClick()
	{
		EGDebug.Log("Clicked");
	}

	private void OnHover(bool isOver)
	{
		EGDebug.Log("OnHover");
	}

	private void OnPress(bool isDown)
	{
	}

	private void OnStartBtnClick()
	{
		OnConnect();
	}

	private void DirectLogin()
	{
		OnConnect();
	}

	private void OnConnect()
	{
		EnsureInputReferences();
		string text = string.Empty;
		string text2 = string.Empty;
		if (m_UserName != null)
		{
			text = m_UserName.text;
			if (string.IsNullOrEmpty(text) && m_UserName.label != null)
			{
				text = m_UserName.label.text;
			}
		}
		if (m_UserPassword != null)
		{
			text2 = m_UserPassword.text;
			if (string.IsNullOrEmpty(text2) && m_UserPassword.label != null)
			{
				text2 = m_UserPassword.label.text;
			}
		}
		text = (text ?? string.Empty).Trim();
		text2 = (text2 ?? string.Empty).Trim();
		if (string.IsNullOrEmpty(text))
		{
			text = PlayerPrefs.GetString("UserAccount", string.Empty);
		}
		if (string.IsNullOrEmpty(text2))
		{
			text2 = PlayerPrefs.GetString("UserPassword", string.Empty);
		}
		if (string.IsNullOrEmpty(text))
		{
			text = "player1";
		}
		if (string.IsNullOrEmpty(text2))
		{
			text2 = "1";
		}
		EGDebug.LogWarning("[ScreenLogin] OnConnect: user='" + text + "', pass='" + text2 + "'");
		PlayerPrefs.SetString("UserAccount", text);
		PlayerPrefs.SetString("UserPassword", text2);
		GameManager.instance.m_userName = text;
		GameManager.instance.m_userPass = text2;
		GameManager.instance.m_EntryClient.IssueConnect();
	}

	private void OnConnectVietID()
	{
	}

	private void OnConnectFacebook()
	{
	}

	private void OnFindPassword()
	{
	}

	private void OnRegister()
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenReg);
	}
}
