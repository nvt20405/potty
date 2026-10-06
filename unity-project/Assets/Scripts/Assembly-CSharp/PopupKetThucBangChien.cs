using UnityEngine;

public class PopupKetThucBangChien : MonoBehaviour
{
	public UISprite spriteKetQua;

	public UILabel labelMessage;

	public static PopupKetThucBangChien instance;

	public ParticleSystem particle;

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public static void Create(bool thanhCong, string message)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupKetThucBangChien"))).GetComponent<PopupKetThucBangChien>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = Vector3.one;
		instance.spriteKetQua.spriteName = ((!thanhCong) ? "thua_cuoc" : "chien_thang");
		instance.labelMessage.text = message;
		instance.labelMessage.color = ((!thanhCong) ? Color.white : Utils.MakeColor(255, 250, 79));
		if (thanhCong)
		{
			instance.particle.Play();
		}
	}

	private void OnBackBtnClick()
	{
		DestroyPopup();
	}
}
