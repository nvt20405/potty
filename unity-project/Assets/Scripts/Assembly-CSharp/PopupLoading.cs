using UnityEngine;

public class PopupLoading : MonoBehaviour
{
	public static PopupLoading instance;

	public UILabel loadingText;

	public UISprite fillingSprite;

	public UISprite logo;

	private int a;

	private string loading = string.Empty;

	private float updateTime;

	public GameObject animNhanVat;

	public static void Create()
	{
		DestroyPopup();
		Object obj = Object.Instantiate(Resources.Load("popup/PopupLoading"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		instance = gameObject.GetComponent<PopupLoading>();
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		if (instance.animNhanVat != null)
		{
			Utils.SetLayer(instance.animNhanVat.transform, "GUIPopUp", true);
			instance.animNhanVat.SetActive(true);
			instance.animNhanVat.GetComponent<ParticleSystem>().Simulate(0f, true, true);
			instance.animNhanVat.GetComponent<ParticleSystem>().Play();
		}
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public void SetAmmount(float a)
	{
		fillingSprite.fillAmount = a;
	}

	private void Awake()
	{
		loading = Localization.instance.Get("LoadingMsg");
		loadingText.text = loading + ".";
		fillingSprite.fillAmount = 0f;
		logo.MakePixelPerfect();
	}

	private void Update()
	{
		if (a % 3 == 0)
		{
			loadingText.text = loading + " .";
		}
		else if (a % 3 == 1)
		{
			loadingText.text = loading + " . .";
		}
		else if (a % 3 == 2)
		{
			loadingText.text = loading + " . . .";
		}
		updateTime += Time.deltaTime;
		if (updateTime > 0.5f)
		{
			a++;
			if (a > 29)
			{
				a = 0;
			}
			updateTime = 0f;
		}
	}
}
