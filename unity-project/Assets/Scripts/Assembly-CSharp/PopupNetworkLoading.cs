using UnityEngine;

public class PopupNetworkLoading : MonoBehaviour
{
	public UISprite loadingSprite;

	public UILabel loadingLabel;

	public UISprite bg;

	private float lastTimeRotate;

	private float rot;

	private float _TimeSinceRequest = float.MaxValue;

	public static PopupNetworkLoading instance;

	public float TimeSinceRequest
	{
		get
		{
			return _TimeSinceRequest;
		}
		set
		{
			_TimeSinceRequest = value;
		}
	}

	public bool ShowMsg { get; set; }

	private void Start()
	{
	}

	private void Update()
	{
		rot += -210f * Time.deltaTime;
		if (rot <= -30f)
		{
			loadingSprite.cachedTransform.Rotate(new Vector3(0f, 0f, -30f));
			rot += 30f;
		}
		TimeSinceRequest -= Time.deltaTime;
		if (!(TimeSinceRequest <= 0f))
		{
			return;
		}
		TimeSinceRequest = float.MaxValue;
		if (ShowMsg)
		{
			MessagePopup.Create(Localization.instance.Get("ConnectNetworkError"));
			if (!NGUITools.GetActive(GameManager.instance.m_EntryClient.gameObject))
			{
				GameManager.instance.m_GameClient.ReConnect();
			}
		}
		DestroyPopup();
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public static void Create(string str, float time = 30f, bool showMsg = true)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupNetworkLoading"))).GetComponent<PopupNetworkLoading>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.loadingLabel.text = str;
		instance.TimeSinceRequest = time;
		instance.ShowMsg = showMsg;
	}
}
