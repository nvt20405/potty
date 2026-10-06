using UnityEngine;

public class ScreenSelectTheFirstPet : ScreenBase
{
	public GameObject pet1;

	public GameObject pet2;

	public GameObject pet3;

	public GameObject Pet1_3D;

	public GameObject Pet2_3D;

	public GameObject Pet3_3D;

	private GameObject avatar1;

	private GameObject avatar2;

	private GameObject avatar3;

	private string pet1CodeName = "PET_THAN_LONG";

	private string pet2CodeName = "PET_KY_LAN";

	private string pet3CodeName = "PET_PHUONG_HOANG";

	private void Start()
	{
		UIEventListener.Get(pet1.gameObject).onClick = onClick_Pet1;
		UIEventListener.Get(pet2.gameObject).onClick = onClick_Pet2;
		UIEventListener.Get(pet3.gameObject).onClick = onClick_Pet3;
	}

	public override void OnActive()
	{
		base.OnActive();
	}

	public void onClick_Pet1(GameObject go)
	{
		SelectStartThanThuRequest selectStartThanThuRequest = new SelectStartThanThuRequest();
		selectStartThanThuRequest.Name = pet1CodeName;
		GameManager.instance.m_GameClient.RequestSetThanThu(selectStartThanThuRequest);
	}

	public void onClick_Pet2(GameObject go)
	{
		SelectStartThanThuRequest selectStartThanThuRequest = new SelectStartThanThuRequest();
		selectStartThanThuRequest.Name = pet2CodeName;
		GameManager.instance.m_GameClient.RequestSetThanThu(selectStartThanThuRequest);
	}

	public void onClick_Pet3(GameObject go)
	{
		SelectStartThanThuRequest selectStartThanThuRequest = new SelectStartThanThuRequest();
		selectStartThanThuRequest.Name = pet3CodeName;
		GameManager.instance.m_GameClient.RequestSetThanThu(selectStartThanThuRequest);
	}
}
