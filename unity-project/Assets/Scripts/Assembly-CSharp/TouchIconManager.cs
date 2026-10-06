using System.Collections.Generic;
using UnityEngine;

public class TouchIconManager : MonoBehaviour
{
	public GameObject iconContainer;

	public Dictionary<int, TouchIcon> icons;

	public static TouchIconManager instance;

	private void Awake()
	{
		instance = this;
		icons = new Dictionary<int, TouchIcon>();
	}

	private void Start()
	{
	}

	public void addIcon(int fingerId)
	{
		if (!icons.ContainsKey(fingerId))
		{
			GUIManager.log("addIcon " + fingerId);
			GameObject gameObject = Utils.instantiatePrefab("GUI/Controls/TouchIcon/TouchIcon", iconContainer.transform);
			TouchIcon component = gameObject.GetComponent<TouchIcon>();
			component.id = icons.Count;
			component.fingerId = fingerId;
			icons.Add(fingerId, component);
			gameObject.SetActive(false);
		}
	}

	private void Update()
	{
		if (icons != null && icons.Count < Input.touchCount)
		{
			for (int i = 0; i < Input.touches.Length; i++)
			{
				addIcon(Input.touches[i].fingerId);
			}
		}
		foreach (KeyValuePair<int, TouchIcon> icon in icons)
		{
			bool flag = false;
			Vector3 position = Vector3.zero;
			for (int j = 0; j < Input.touches.Length; j++)
			{
				if (Input.touches[j].fingerId == icon.Key)
				{
					flag = true;
					position = Input.touches[j].position;
				}
			}
			if (flag)
			{
				if (NGUITools.GetActive(icon.Value.gameObject))
				{
					position.x = Mathf.Clamp01(position.x / (float)Screen.width);
					position.y = Mathf.Clamp01(position.y / (float)Screen.height);
					icon.Value.transform.position = GUIManager.instance.cam2D.ViewportToWorldPoint(position);
					position = icon.Value.transform.localPosition;
					position.z = 0f;
					icon.Value.transform.localPosition = position;
				}
			}
			else
			{
				icon.Value.gameObject.SetActive(false);
			}
		}
	}
}
