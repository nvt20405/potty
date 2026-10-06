using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public Transform player;

	public Vector3 relCameraPos;

	private bool delayLookAt = true;

	public void Init(Transform player)
	{
		this.player = player;
	}

	private void Awake()
	{
		relCameraPos = base.transform.position;
	}

	private Vector3 GetReflectVector(Vector3 relPos, Vector3 up)
	{
		float num = 2f * Vector3.Dot(relPos, up);
		return up * num - relPos;
	}

	private void Update()
	{
		Vector3 vector = Vector3.zero;
		if (player != null)
		{
			vector = player.position;
		}
		Vector3 position = vector + relCameraPos;
		base.transform.position = position;
	}

	private void SmoothLookAt()
	{
		Vector3 forward = player.position - base.transform.position;
		Quaternion rotation = Quaternion.LookRotation(forward, Vector3.up);
		base.transform.rotation = rotation;
	}
}
