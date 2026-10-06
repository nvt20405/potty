using System;
using System.Collections;
using UnityEngine;

public class PopupActionWaitForSeconds : MonoBehaviour
{
	public UISprite loadingSprite;

	public UILabel loadingLabel;

	private float lastTimeRotate;

	private float rot;

	private bool isRotting = true;

	public Action OnTimeAction;

	public static PopupActionWaitForSeconds instance;

	private DateTime timeEndWaiting = DateTime.MinValue;

	private void Update()
	{
		rot += -210f * Time.deltaTime;
		if (rot <= -30f && isRotting)
		{
			loadingSprite.cachedTransform.Rotate(new Vector3(0f, 0f, -30f));
			rot += 30f;
		}
		TimeSpan timeSpan = timeEndWaiting - GameManager.instance.m_GameClient.ServerTime;
		if (timeSpan.Ticks > 0)
		{
			instance.loadingLabel.gameObject.SetActive(true);
			instance.loadingLabel.text = string.Format(Localization.instance.Get("ChoThoiLuaBtnLabel"), (int)timeSpan.TotalSeconds + 1);
		}
		else
		{
			isRotting = false;
			instance.loadingLabel.gameObject.SetActive(false);
		}
	}

	private void OnTimeArrive()
	{
		if (OnTimeAction != null)
		{
			OnTimeAction();
		}
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			UnityEngine.Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public static void Create(DateTime timeEnd, Action onTimeAction)
	{
		DestroyPopup();
		instance = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("popup/PopupActionWaitForSeconds"))).GetComponent<PopupActionWaitForSeconds>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = Vector3.one;
		instance.timeEndWaiting = timeEnd;
		instance.OnTimeAction = onTimeAction;
		instance.Set(timeEnd);
		instance.isRotting = true;
	}

	private void Set(DateTime timeEnd)
	{
		TimeSpan timeSpan = timeEnd - GameManager.instance.m_GameClient.ServerTime;
		if (timeSpan.Ticks > 0)
		{
			instance.loadingLabel.gameObject.SetActive(true);
			instance.loadingLabel.text = string.Format(Localization.instance.Get("ChoThoiLuaBtnLabel"), (int)timeSpan.TotalSeconds + 1);
			StartCoroutine(WaitForDanhDongNhan((int)timeSpan.TotalSeconds + 1));
		}
		else
		{
			isRotting = false;
			instance.loadingLabel.gameObject.SetActive(false);
			DestroyPopup();
		}
	}

	private IEnumerator WaitForDanhDongNhan(int seconds)
	{
		yield return new WaitForSeconds(seconds);
		DestroyPopup();
		OnTimeArrive();
	}
}
