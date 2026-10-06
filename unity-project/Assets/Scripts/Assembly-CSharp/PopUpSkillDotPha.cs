using UnityEngine;

public class PopUpSkillDotPha : MonoBehaviour
{
	public static PopUpSkillDotPha instance;

	public static PopUpSkillDotPha getInstance()
	{
		if (instance == null)
		{
			instance = Utils.instantiatePrefab("GUI/Controls/PopUpSkillDotPha", GUIManager.instance.popUpContainer.transform).GetComponent<PopUpSkillDotPha>();
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
