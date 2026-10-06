using System;
using UnityEngine;

public class DongNhanAvatar3D : MonoBehaviour
{
	public UILabel label;

	public UIPanel ui;

	public GameObject avatar;

	public Action<PlayerController> OnPlayerEnterTrigger;

	public Action<PlayerController> OnPlayerStayInTrigger;

	private void Start()
	{
		label.text = ConfigManager.instance.m_dicNhanVats["NC_DONG_NHAN_COC"].TenHienThi;
		avatar.GetComponent<Animation>().wrapMode = WrapMode.Loop;
		avatar.GetComponent<Animation>().CrossFade("idle");
	}

	private void Update()
	{
		RotateGUITowardCam();
	}

	protected void RotateGUITowardCam()
	{
		if (ui != null)
		{
			Vector3 vector = Camera.main.transform.rotation * Vector3.forward;
			Vector3 forward = vector - Vector3.up * Vector3.Dot(Vector3.up, vector);
			ui.transform.rotation = Quaternion.LookRotation(forward);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (GUIManager.instance != null && GUIManager.instance.homeCity != null && GUIManager.instance.homeCity.mainAvatar != null && other.gameObject == GUIManager.instance.homeCity.mainAvatar.gameObject && OnPlayerEnterTrigger != null)
		{
			OnPlayerEnterTrigger(GUIManager.instance.homeCity.mainAvatar);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (GUIManager.instance != null && GUIManager.instance.homeCity != null && GUIManager.instance.homeCity.mainAvatar != null && other.gameObject == GUIManager.instance.homeCity.mainAvatar.gameObject && OnPlayerStayInTrigger != null)
		{
			OnPlayerStayInTrigger(GUIManager.instance.homeCity.mainAvatar);
		}
	}
}
