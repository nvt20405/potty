using UnityEngine;

public class LienMinhPlayerMovement : MonoBehaviour
{
	public int gid;

	public string displayName;

	public string codeName;

	private RaycastHit mousePoint;

	private bool unpressed;

	private bool pressed;

	private void Start()
	{
	}

	private void FixedUpdate()
	{
		if (Input.GetMouseButtonUp(0))
		{
			Ray ray = GUIManager.instance.lienMinh3D.cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo = default(RaycastHit);
			if (Physics.Raycast(ray, out hitInfo))
			{
				mousePoint = hitInfo;
			}
		}
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			unpressed = false;
			if (UICamera.hoveredObject == null)
			{
				pressed = true;
			}
		}
		if (Input.GetMouseButtonUp(0))
		{
			unpressed = true;
			pressed = false;
		}
		if (pressed && !unpressed && mousePoint.collider != null)
		{
			if (mousePoint.collider.gameObject.layer == 30)
			{
			}
		}
		else if (unpressed && UICamera.hoveredObject == null && !UICamera.isDragging && mousePoint.collider != null)
		{
			LienMinhPlayerMovement lienMinhPlayerMovement = NGUITools.FindInParents<LienMinhPlayerMovement>(mousePoint.collider.gameObject);
			if (lienMinhPlayerMovement != null && lienMinhPlayerMovement.gid != GameManager.instance.m_GameClient.UserInfo.Gamer.ID)
			{
				EGDebug.Log("Clicked " + lienMinhPlayerMovement.displayName);
				PopupGamerInCity.Create(lienMinhPlayerMovement.gid, lienMinhPlayerMovement.displayName, lienMinhPlayerMovement.codeName, true);
			}
			mousePoint = default(RaycastHit);
			unpressed = false;
		}
	}
}
