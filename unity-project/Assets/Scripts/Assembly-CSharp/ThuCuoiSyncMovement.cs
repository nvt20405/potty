using UnityEngine;

public class ThuCuoiSyncMovement : MonoBehaviour
{
	public Transform Target;

	private Vector3 DeltaPos;

	public void StartSync(Transform target)
	{
		Target = target;
		DeltaPos = Target.position - base.transform.position;
		DeltaPos.x = 0f;
		DeltaPos.z = 0f;
		DeltaPos.y = 1.55f;
	}

	public void StopSync()
	{
		Target = null;
	}

	private void Update()
	{
		if (Target != null)
		{
			base.transform.position = Target.transform.position - DeltaPos + base.transform.forward * 0.4f;
		}
	}
}
