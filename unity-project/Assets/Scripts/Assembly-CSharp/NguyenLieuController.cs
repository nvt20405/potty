using UnityEngine;

public class NguyenLieuController : MonoBehaviour
{
	public UIPanel guiPanel;

	private void Update()
	{
		RotateGUITowardCam();
	}

	private void RotateGUITowardCam()
	{
		if (guiPanel != null)
		{
			Vector3 forward = Camera.main.transform.forward;
			guiPanel.transform.rotation = Quaternion.LookRotation(forward);
		}
	}
}
