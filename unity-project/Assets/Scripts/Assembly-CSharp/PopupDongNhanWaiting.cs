using System;
using System.Collections;
using UnityEngine;

public class PopupDongNhanWaiting : MonoBehaviour
{
	public UISprite loadingSprite;

	public UILabel loadingLabel;

	public UILabel costDanhNgayDongNhanLabel;

	private float lastTimeRotate;

	private float rot;

	private bool isRotting = true;

	public static PopupDongNhanWaiting instance;

	private DateTime timeEndWaiting = DateTime.MinValue;

	private void Start()
	{
		costDanhNgayDongNhanLabel.text = ConfigManager.instance.CostHoiSinhDanhDongNhan().ToString();
	}

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
		GameManager.instance.m_GameClient.RequestDanhDongNhan(false);
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			UnityEngine.Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public static void Create(DateTime timeEnd, int numVang)
	{
		DestroyPopup();
		instance = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Popup/PopupDongNhanWaiting"))).GetComponent<PopupDongNhanWaiting>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = Vector3.one;
		instance.timeEndWaiting = timeEnd;
		instance.isRotting = true;
		instance.Set(timeEnd, numVang);
	}

	private void Set(DateTime timeEnd, int numVang)
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
		}
	}

	private IEnumerator WaitForDanhDongNhan(int seconds)
	{
		yield return new WaitForSeconds(seconds);
		DestroyPopup();
		OnTimeArrive();
	}

	private void OnDanhNgayDongNhanClick()
	{
		ScreenDongNhan screenDongNhan = GUIManager.getScreen(GAME_SCREEN.ScreenDongNhan) as ScreenDongNhan;
		screenDongNhan.OnDanhNgayDongNhanClick();
		DestroyPopup();
	}
}
