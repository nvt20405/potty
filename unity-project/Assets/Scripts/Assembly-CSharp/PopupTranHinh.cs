using System.Collections.Generic;
using UnityEngine;

public class PopupTranHinh : MonoBehaviour
{
	public NhanVatAvatar[] listAvatar;

	public UIPanel TranHinhPanel;

	public GameObject SaveBtn;

	public float PanelWidth;

	public float PanelHeight;

	private int indexOri;

	private int indexTarget;

	private bool ReadOnly;

	private UserInfo RefUserInfo;

	public static PopupTranHinh instance;

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

	public static void Create(bool isReadOnly, UserInfo info)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupTranHinh"))).GetComponent<PopupTranHinh>();
		instance.ReadOnly = isReadOnly;
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		if (info != null)
		{
			instance.RefUserInfo = info;
		}
		else
		{
			instance.RefUserInfo = GameManager.instance.m_GameClient.UserInfo;
		}
		instance.SyncWithNetworkData();
	}

	public void SyncWithNetworkData()
	{
		List<int> doi_hinh_list = RefUserInfo.DoiHinh.ListRaTran;
		for (int i = 0; i < listAvatar.Length; i++)
		{
			NGUITools.SetActive(listAvatar[i].gameObject, false);
		}
		if (ReadOnly)
		{
			NGUITools.SetActive(SaveBtn, false);
		}
		else
		{
			NGUITools.SetActive(SaveBtn, true);
		}
		int j;
		for (j = 0; j < doi_hinh_list.Count; j++)
		{
			UserInfo.HeroData heroData = RefUserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == doi_hinh_list[j]);
			if (heroData == null)
			{
				continue;
			}
			NGUITools.SetActive(listAvatar[j].gameObject, true);
			SetAvatarPos(j, heroData.DHPosX, heroData.DHPosY);
			if (j == indexOri)
			{
				EGDebug.Log("HERO ID: " + heroData.HID + " - POSDH: " + heroData.DHPosX + " - " + heroData.DHPosY);
				EGDebug.Log("HERO ID: " + heroData.HID + " - POSLocal: " + listAvatar[j].transform.localPosition.x + " - " + listAvatar[j].transform.localPosition.y);
			}
			if (ReadOnly)
			{
				EGGUIDragObject component = listAvatar[j].gameObject.GetComponent<EGGUIDragObject>();
				if (component != null)
				{
					component.enabled = false;
				}
			}
			listAvatar[j].Set(heroData.Name);
			listAvatar[j].GetComponent<EGGUIDragObject>().onDragEventGO -= AvatarDrag;
			listAvatar[j].GetComponent<EGGUIDragObject>().onPressEventGO -= AvatarPress;
			listAvatar[j].GetComponent<EGGUIDragObject>().onDragEventGO += AvatarDrag;
			listAvatar[j].GetComponent<EGGUIDragObject>().onPressEventGO += AvatarPress;
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
		GameManager.instance.m_GameClient.RequestSetTranHinh(RefUserInfo.DoiHinh.ListRaTran, list, list2);
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
		for (int j = 0; j < listAvatar.Length; j++)
		{
			if (NGUITools.GetActive(listAvatar[j].gameObject))
			{
				Vector3 vector = target.transform.localPosition - listAvatar[j].transform.localPosition;
				vector.z = 0f;
				float magnitude = vector.magnitude;
				if ((magnitude <= 40f && magnitude > 0f) || (magnitude < (float)num5 && magnitude > 0f && num4 < 4 && j < 4))
				{
					changedTargetPositionX = listAvatar[j].transform.localPosition.x;
					changedTargetPositionY = listAvatar[j].transform.localPosition.y;
					EGDebug.Log("changedTargetPosition x: " + changedTargetPositionX + " - y: " + changedTargetPositionY);
					swapDoiHinh(num4, j);
					return;
				}
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

	public void swapDoiHinh(int index1, int index2)
	{
		EGDebug.Log("SWAP DOI HINH: " + index1 + " - " + index2);
		if (index1 >= 0 && index2 >= 0 && index1 != index2)
		{
			SwapDoiHinhRequest swapDoiHinhRequest = new SwapDoiHinhRequest();
			swapDoiHinhRequest.Slot1 = index1;
			swapDoiHinhRequest.Slot2 = index2;
			SetTranHinhRequest setTranHinhRequest = new SetTranHinhRequest();
			setTranHinhRequest.DhXList = getDHXList(index1, index2);
			setTranHinhRequest.DhYList = getDHYList(index1, index2);
			setTranHinhRequest.HeroList = getListHero(index1, index2);
			if (setTranHinhRequest.DhXList != null && setTranHinhRequest.DhYList != null && setTranHinhRequest.HeroList != null && setTranHinhRequest.DhXList.Count == setTranHinhRequest.DhYList.Count && setTranHinhRequest.DhYList.Count == setTranHinhRequest.HeroList.Count)
			{
				SetDoiHinhAndTranHinhRequest setDoiHinhAndTranHinhRequest = new SetDoiHinhAndTranHinhRequest();
				setDoiHinhAndTranHinhRequest.swapDoiHinhRequest = swapDoiHinhRequest;
				setDoiHinhAndTranHinhRequest.tranHinhRequest = setTranHinhRequest;
				GameManager.instance.m_GameClient.RequestSetDoiHinhAndTranHinh(setDoiHinhAndTranHinhRequest);
			}
		}
	}

	public List<int> getListHero(int index1, int index2)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < RefUserInfo.DoiHinh.ListRaTran.Count; i++)
		{
			if (RefUserInfo.DoiHinh.ListRaTran[i] > 0)
			{
				list.Add(RefUserInfo.DoiHinh.ListRaTran[i]);
			}
		}
		return list;
	}

	public List<float> getDHXList(int indexOriginNV, int indexTargetNV)
	{
		if (indexOriginNV == indexTargetNV)
		{
			return null;
		}
		List<float> list = new List<float>();
		float num = 0f;
		float num2 = 0f;
		if (indexOriginNV >= 4 && indexTargetNV < 4)
		{
			num = changedTargetPositionX / (PanelWidth * -0.5f);
			num2 = targetPositionX / (PanelWidth * -0.5f);
		}
		else if ((indexOriginNV < 4 && indexTargetNV >= 4) || (indexOriginNV >= 4 && indexTargetNV >= 4) || (indexOriginNV < 4 && indexTargetNV < 4))
		{
			num = changedTargetPositionX / (PanelWidth * -0.5f);
			num2 = targetPositionX / (PanelWidth * -0.5f);
		}
		for (int i = 0; i < listAvatar.Length; i++)
		{
			if (NGUITools.GetActive(listAvatar[i].gameObject))
			{
				float item = ((i == indexOriginNV) ? num : ((i != indexTargetNV) ? (listAvatar[i].transform.localPosition.x / (PanelWidth * -0.5f)) : num2));
				list.Add(item);
			}
		}
		return list;
	}

	public List<float> getDHYList(int indexOriginNV, int indexTargetNV)
	{
		if (indexOriginNV == indexTargetNV)
		{
			return null;
		}
		List<float> list = new List<float>();
		float num = 0f;
		float num2 = 0f;
		if (indexOriginNV >= 4 && indexTargetNV < 4)
		{
			indexOri = indexOriginNV;
			indexTarget = indexTargetNV;
			num = (PanelHeight * 0.5f - changedTargetPositionY) / PanelHeight;
			num2 = (PanelHeight * 0.5f - targetPositionY) / PanelHeight;
		}
		else if ((indexOriginNV < 4 && indexTargetNV >= 4) || (indexOriginNV >= 4 && indexTargetNV >= 4) || (indexOriginNV < 4 && indexTargetNV < 4))
		{
			num = (PanelHeight * 0.5f - changedTargetPositionY) / PanelHeight;
			num2 = (PanelHeight * 0.5f - targetPositionY) / PanelHeight;
		}
		for (int i = 0; i < listAvatar.Length; i++)
		{
			if (NGUITools.GetActive(listAvatar[i].gameObject))
			{
				float item = ((i == indexOriginNV) ? num : ((i != indexTargetNV) ? ((PanelHeight * 0.5f - listAvatar[i].transform.localPosition.y) / PanelHeight) : num2));
				list.Add(item);
			}
		}
		return list;
	}

	public float validatePositionX(float currentPosX)
	{
		float result = currentPosX;
		float num = 0f - LeftSpaceLimit + currentPosX;
		float num2 = RightSpaceLimit - currentPosX;
		if (currentPosX < RightSpaceLimit && currentPosX > LeftSpaceLimit)
		{
			result = ((!(num < num2)) ? (RightSpaceLimit / (PanelWidth * -0.5f)) : (LeftSpaceLimit / (PanelWidth * -0.5f)));
		}
		return result;
	}

	public float validatePositionY(float currentPosY)
	{
		float result = currentPosY;
		float num = 0f - BottomSpaceLimit + currentPosY;
		float num2 = TopSpaceLimit - currentPosY;
		if (currentPosY < RightSpaceLimit && currentPosY > LeftSpaceLimit)
		{
			result = ((!(num < num2)) ? ((PanelHeight * 0.5f - TopSpaceLimit) / PanelHeight) : ((PanelHeight * 0.5f - BottomSpaceLimit) / PanelHeight));
		}
		return result;
	}

	public void btnHelp_OnClick()
	{
		if (!ReadOnly)
		{
			DestroyPopup();
			ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
			screenHelpInfo.setByLevel(4, 3);
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
			EGDebug.Log("btnHelp_OnClick");
		}
	}
}
