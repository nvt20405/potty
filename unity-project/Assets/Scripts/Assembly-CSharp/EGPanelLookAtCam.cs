using UnityEngine;

public class EGPanelLookAtCam : MonoBehaviour
{
	protected void RotateTransTowardCam(Transform trans)
	{
		if (trans != null)
		{
			Vector3 forward = base.transform.position - Camera.main.transform.position;
			trans.transform.rotation = Quaternion.LookRotation(forward);
		}
	}

	private void Update()
	{
		RotateTransTowardCam(base.transform);
	}
}
