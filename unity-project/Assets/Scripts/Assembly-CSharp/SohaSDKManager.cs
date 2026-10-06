using System;
using System.Collections;
using UnityEngine;

public class SohaSDKManager : MonoBehaviour
{
	public static SohaSDKManager instance;

	public static bool isSohaBtnActive;

	public string countNotification = string.Empty;

	public Action<string> onSohaUpdateNotification;

	private AndroidJavaClass jc;

	private string lastOrderId = string.Empty;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			Init();
		}
		UnityEngine.Object.DontDestroyOnLoad(instance.gameObject);
	}

	public void Init()
	{
		jc = new AndroidJavaClass("vn.shg.mobi.mongvolam.UnityActivity");
		AndroidJavaClass androidJavaClass = jc;
		EGDebug.Log("-----------------> " + ((androidJavaClass != null) ? androidJavaClass.ToString() : null));
	}

	public void AddSohaButtonLeftAnchor(float yfromTop)
	{
	}

	public void ShowSohaDashBoardButton()
	{
		isSohaBtnActive = true;
		jc.CallStatic("RequestShowSoha");
	}

	public void HideSohaDashBoardButton()
	{
		if (isSohaBtnActive)
		{
			jc.CallStatic("RequestHideSoha");
			isSohaBtnActive = false;
		}
	}

	public void SetUserInfo(string areaId, string roleId)
	{
		try
		{
			if (jc != null)
			{
				jc.CallStatic("SetUserConfig", areaId, roleId);
			}
		}
		catch (Exception)
		{
		}
	}

	private IEnumerator SetSohaUserInfo(int serverID, int gid, string token)
	{
		string login_url = string.Format("http://soap.soha.vn/api/a/GET/mobile/logplayuser?app_id=eefffb5332badbe91c91d395f6a03a13&areaid={0}&roleid={1}&gver=2.0.0&sdkver=0.0.0&clientname=sohagame&access_token={2}", serverID, gid, token);
		yield return new WWW(login_url);
	}

	public void SetUserInfo(string areaId, string areaName, string roleId, string roleName, string level)
	{
		try
		{
			if (jc != null)
			{
				jc.CallStatic("SetUserConfig", areaId, roleId, roleName, level);
			}
		}
		catch (Exception)
		{
		}
	}

	public void Login()
	{
		jc.CallStatic("RequestLoginSoha");
		Debug.Log("Call login!");
	}

	public void Logout()
	{
		jc.CallStatic("RequestLogoutSoha");
	}

	public void Payment()
	{
		MessagePopup.Create("May chu da tat tinh nang nap.");
	}

	public bool IsLoggedFacebook()
	{
		return PlayerPrefs.GetInt(GameManager.instance.m_userName + "facebook", 0) != 0;
	}

	public void ShareFacebook()
	{
		PlayerPrefs.SetInt(GameManager.instance.m_userName + "facebook", 1);
	}

	public void ShareFacebookUrl(string text, string title, string url, string content, string picture)
	{
		PlayerPrefs.SetInt(GameManager.instance.m_userName + "facebook", 1);
	}

	public void FinishPayment()
	{
	}

	public void OnSohaLoginSuccess(string idToken)
	{
		PlayerPrefs.SetInt("login", 1);
		Debug.Log("Login success!");
		if (!string.IsNullOrEmpty(idToken))
		{
			string[] array = idToken.Split(" ".ToCharArray());
			if (array.Length >= 2 && !string.IsNullOrEmpty(array[0]))
			{
				string userName = array[0];
				string accessToken = array[1];
				GameManager.instance.m_userName = userName;
				GameManager.instance.m_accessToken = accessToken;
				GameManager.instance.m_EntryClient.IssueConnect();
			}
		}
	}

	private void OnSohaLogoutSuccess(string message)
	{
		PlayerPrefs.SetInt("login", 0);
		Debug.Log("Logout success!");
		GameManager.instance.m_GameClient.LogOffGameServer();
	}

	private void OnSohaLoginFail(string message)
	{
		MessagePopup.Create("Xảy ra lỗi khi đăng nhập, xin vui lòng thử lại");
	}

	public void OnSohaPaymentSuccess(string data)
	{
		string[] array = data.Split(" ".ToCharArray());
		Debug.Log("Payment success " + data);
		if (array.Length > 1)
		{
			string text = array[1];
			string text2 = instance.lastOrderId;
			if (text2 == text)
			{
				EGDebug.LogWarning("Dupplicate payment success callback. OrderId = " + text);
				return;
			}
			text2 = text;
			PaymentRequest paymentRequest = new PaymentRequest();
			paymentRequest.OrderID = text;
			GameManager.instance.m_GameClient.RequestPaymentConfirm(paymentRequest);
		}
	}

	private void OnSohaPaymentFail(string message)
	{
		MessagePopup.Create(message);
	}

	public void RequestShowSoha()
	{
		jc.CallStatic("RequestShowSoha");
	}

	public void RequestHideSoha()
	{
		jc.CallStatic("RequestHideSoha");
	}
}
