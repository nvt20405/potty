using System.Collections.Generic;
using UnityEngine;

public class PopupQMDTranHinh : MonoBehaviour
{
	public NhanVatAvatar[] listAvatar;

	public UIPanel TranHinhPanel;

	public GameObject SaveBtn;

	public float PanelWidth;

	public float PanelHeight;

	private int indexOri;

	private int indexTarget;

	public static PopupQMDTranHinh instance;

	private float LeftSpaceLimit = -245f;

	private float RightSpaceLimit = 245f;

	private float BottomSpaceLimit = -225f;

	private float TopSpaceLimit = 220f;

	private float targetPositionX;

	private float targetPositionY;

	private float changedTargetPositionX;

	private float changedTargetPositionY;

	private void Start()
	{
	}

	public void OnCloseClick()
	{
		DestroyPopup();
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public static void Create(bool isReadOnly, QMDInfo info)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupQMDTranHinh"))).GetComponent<PopupQMDTranHinh>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.SyncWithNetworkData(info);
	}

	public void SyncWithNetworkData(QMDInfo info)
	{
		List<QMDInfo.QMDNhanVat> list = new List<QMDInfo.QMDNhanVat>();
		list.Add(info.NV1);
		list.Add(info.NV2);
		list.Add(info.NV3);
		list.Add(info.NV4);
		for (int i = 0; i < 4; i++)
		{
			NGUITools.SetActive(listAvatar[i].gameObject, true);
			SetAvatarPos(i, list[i].DHPosX, list[i].DHPosY);
			listAvatar[i].Set(list[i].Name);
			listAvatar[i].GetComponent<EGGUIDragObject>().onDragEventGO -= AvatarDrag;
			listAvatar[i].GetComponent<EGGUIDragObject>().onPressEventGO -= AvatarPress;
			listAvatar[i].GetComponent<EGGUIDragObject>().onDragEventGO += AvatarDrag;
			listAvatar[i].GetComponent<EGGUIDragObject>().onPressEventGO += AvatarPress;
		}
	}

	public void SetAvatarPos(int idx, float dhx, float dhy)
	{
		float x = dhx * PanelWidth * -0.5f;
		float y = PanelHeight * 0.5f - dhy * PanelHeight;
		listAvatar[idx].transform.localPosition = new Vector3(x, y, 0f);
	}

	public void OnSaveClick()
	{
		List<float> list = new List<float>();
		List<float> list2 = new List<float>();
		for (int i = 0; i < listAvatar.Length; i++)
		{
			float item = listAvatar[i].transform.localPosition.x / (PanelWidth * -0.5f);
			float item2 = (PanelHeight * 0.5f - listAvatar[i].transform.localPosition.y) / PanelHeight;
			list.Add(item);
			list2.Add(item2);
		}
		ScreenQuangMinhDinh screenQuangMinhDinh = GUIManager.getScreen(GAME_SCREEN.ScreenQuangMinhDinh) as ScreenQuangMinhDinh;
		screenQuangMinhDinh.m_Current_Info.NV1.DHPosX = list[0];
		screenQuangMinhDinh.m_Current_Info.NV1.DHPosY = list2[0];
		screenQuangMinhDinh.m_Current_Info.NV2.DHPosX = list[1];
		screenQuangMinhDinh.m_Current_Info.NV2.DHPosY = list2[1];
		screenQuangMinhDinh.m_Current_Info.NV3.DHPosX = list[2];
		screenQuangMinhDinh.m_Current_Info.NV3.DHPosY = list2[2];
		screenQuangMinhDinh.m_Current_Info.NV4.DHPosX = list[3];
		screenQuangMinhDinh.m_Current_Info.NV4.DHPosY = list2[3];
		QMDTranHinhRequest qMDTranHinhRequest = new QMDTranHinhRequest();
		qMDTranHinhRequest.info = screenQuangMinhDinh.m_Current_Info;
		GameManager.instance.m_GameClient.RequestQMDSelect(qMDTranHinhRequest);
	}

	private void AvatarDrag(Vector2 delta, GameObject target)
	{
		if (target.transform.localPosition.y > TopSpaceLimit)
		{
			target.transform.localPosition = new Vector3(target.transform.localPosition.x, TopSpaceLimit, target.transform.localPosition.z);
		}
		if (target.transform.localPosition.y < BottomSpaceLimit)
		{
			target.transform.localPosition = new Vector3(target.transform.localPosition.x, BottomSpaceLimit, target.transform.localPosition.z);
		}
		if (target.transform.localPosition.x > RightSpaceLimit)
		{
			target.transform.localPosition = new Vector3(RightSpaceLimit, target.transform.localPosition.y, target.transform.localPosition.z);
		}
		if (target.transform.localPosition.x < LeftSpaceLimit)
		{
			target.transform.localPosition = new Vector3(LeftSpaceLimit, target.transform.localPosition.y, target.transform.localPosition.z);
		}
		int num = 0;
		NhanVatAvatar component = target.transform.GetComponent<NhanVatAvatar>();
		for (int i = 0; i < listAvatar.Length; i++)
		{
			if (component == listAvatar[i])
			{
				num = i;
			}
		}
	}

	private void AvatarPress(bool isPressed, GameObject target)
	{
		if (isPressed)
		{
			targetPositionX = target.gameObject.transform.localPosition.x;
			targetPositionY = target.gameObject.transform.localPosition.y;
		}
		if (isPressed)
		{
			return;
		}
		Vector3 localPosition = target.gameObject.transform.localPosition;
		float num = 0f - LeftSpaceLimit + localPosition.x;
		float num2 = RightSpaceLimit - localPosition.x;
		float num3 = localPosition.y - BottomSpaceLimit;
		int num4 = 0;
		int num5 = 80;
		NhanVatAvatar component = target.transform.GetComponent<NhanVatAvatar>();
		for (int i = 0; i < listAvatar.Length; i++)
		{
			if (component == listAvatar[i])
			{
				num4 = i;
			}
		}
		if (num4 <= 3)
		{
			return;
		}
		if (localPosition.y > TopSpaceLimit)
		{
			if (num2 < num)
			{
				target.gameObject.transform.localPosition = new Vector3(RightSpaceLimit, TopSpaceLimit, localPosition.z);
			}
			else
			{
				target.gameObject.transform.localPosition = new Vector3(LeftSpaceLimit, TopSpaceLimit, localPosition.z);
			}
		}
		else if (num2 < num)
		{
			if (num2 < num3)
			{
				target.gameObject.transform.localPosition = new Vector3(RightSpaceLimit, localPosition.y, localPosition.z);
			}
			else
			{
				target.gameObject.transform.localPosition = new Vector3(localPosition.x, BottomSpaceLimit, localPosition.z);
			}
		}
		else if (num < num3)
		{
			target.gameObject.transform.localPosition = new Vector3(LeftSpaceLimit, localPosition.y, localPosition.z);
		}
		else
		{
			target.gameObject.transform.localPosition = new Vector3(localPosition.x, BottomSpaceLimit, localPosition.z);
		}
	}
}
