using System.Collections;
using UnityEngine;

public class PopUpNewMess : MonoBehaviour
{
	public static PopUpNewMess instance;

	public UILabel lbMess;

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public static void Create(ChatItem newMess)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupNewMess"))).GetComponent<PopUpNewMess>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.lbMess.text = newMess.Name + ": " + newMess.Content;
		instance.startDisplayTime();
	}

	public void startDisplayTime()
	{
		StartCoroutine(closePopUp(3f));
	}

	public IEnumerator closePopUp(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		DestroyPopup();
	}
}
