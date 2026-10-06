using UnityEngine;

public class LanhDiaMapAxis : MonoBehaviour
{
	public GameObject Target;

	public Vector2 Axis;

	private void Start()
	{
	}

	private void Update()
	{
		if (Axis.x != 0f)
		{
			base.transform.position = new Vector3(Target.transform.position.x, base.transform.position.y, base.transform.position.z);
		}
		if (Axis.y != 0f)
		{
			base.transform.position = new Vector3(base.transform.position.x, Target.transform.position.y, base.transform.position.z);
		}
	}
}
