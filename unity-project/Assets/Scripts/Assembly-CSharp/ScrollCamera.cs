using UnityEngine;

public class ScrollCamera : MonoBehaviour
{
	public Vector3 lastPos;

	private void Start()
	{
		lastPos = Vector3.zero;
		lastPos.z = -100f;
	}

	private void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			lastPos.z = -100f;
		}
		if ((PopupManager.instance.transform.childCount != 0 && (PopupManager.instance.transform.childCount != 1 || !(MessagePopup.instance != null))) || !Input.GetMouseButton(0))
		{
			return;
		}
		if (lastPos.z != -100f)
		{
			Vector3 vector = Input.mousePosition - lastPos;
			vector.z = vector.y;
			vector.y = 0f;
			if ((base.transform.localPosition.z > 68f && vector.z > 0f) || (base.transform.localPosition.z < 27f && vector.z < 0f))
			{
				vector.z = 0f;
			}
			if ((base.transform.localPosition.x > 40f && vector.x > 0f) || (base.transform.localPosition.x < -40f && vector.x < 0f))
			{
				vector.x = 0f;
			}
			base.transform.localPosition += 3f * Time.deltaTime * vector;
		}
		lastPos = Input.mousePosition;
	}
}
