using UnityEngine;

public class PopUpSkillThamNgo : MonoBehaviour
{
	public static PopUpSkillThamNgo instance;

	public static PopUpSkillThamNgo getInstance()
	{
		if (instance == null)
		{
			instance = Utils.instantiatePrefab("GUI/Controls/PopUpSkillThamNgo", GUIManager.instance.popUpContainer.transform).GetComponent<PopUpSkillThamNgo>();
			instance.gameObject.SetActive(false);
		}
		return instance;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
