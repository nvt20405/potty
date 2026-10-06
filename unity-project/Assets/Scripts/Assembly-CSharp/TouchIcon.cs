using System.Collections.Generic;
using UnityEngine;

public class TouchIcon : MonoBehaviour
{
	public int id;

	public int fingerId;

	public GameObject container;

	public GameObject attachment;

	public Dictionary<TOUCH_ICON_ATTACHMENT, GameObject> tias;

	public UILabel label;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void addAttachment(TOUCH_ICON_ATTACHMENT tia)
	{
		if (tias == null)
		{
			tias = new Dictionary<TOUCH_ICON_ATTACHMENT, GameObject>();
		}
		if (!tias.ContainsKey(tia))
		{
			tias.Add(tia, Utils.instantiatePrefab("GUI/Controls/TouchIcon/" + tia, attachment.transform));
		}
	}

	public void removeAttachment(TOUCH_ICON_ATTACHMENT tia)
	{
		if (tias == null)
		{
			tias = new Dictionary<TOUCH_ICON_ATTACHMENT, GameObject>();
		}
		if (tias.ContainsKey(tia))
		{
			Object.Destroy(tias[tia]);
			tias.Remove(tia);
		}
	}

	public void reset()
	{
		if (tias != null)
		{
			foreach (GameObject value in tias.Values)
			{
				Object.Destroy(value);
			}
			tias = new Dictionary<TOUCH_ICON_ATTACHMENT, GameObject>();
		}
		container.transform.localRotation = Quaternion.Euler(Vector3.zero);
		if (label != null)
		{
			label.text = string.Empty;
		}
	}

	private void OnDisable()
	{
		reset();
	}
}
