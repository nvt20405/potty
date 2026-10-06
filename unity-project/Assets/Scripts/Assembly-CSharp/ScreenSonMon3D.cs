using System.Collections.Generic;
using UnityEngine;

public class ScreenSonMon3D : MonoBehaviour
{
	public SonMonBuilding ChinhSanhSlot;

	public List<SonMonBuilding> UtilitySlots;

	public UserInfo.SonMonBuildingInfo CurBuilding;

	public string CurSlot;

	public bool isReadOnly;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void UpdateCongTrinh(UserInfo.SonMonBuildingInfo congTrinh)
	{
		if (congTrinh.LoaiCongTrinh == UserInfo.SonMonBuildingInfo.SonMonBuildingType.CHINH_SANH)
		{
			ChinhSanhSlot.GetComponent<SonMonBuilding>().Set(congTrinh);
			return;
		}
		SonMonBuilding sonMonBuilding = UtilitySlots.Find((SonMonBuilding ct) => ct.info.ID == congTrinh.ID);
		sonMonBuilding.GetComponent<SonMonBuilding>().Set(congTrinh);
	}

	public void Create(UserInfo.SonMonInfo sonmon, bool isReadOnly = false)
	{
		if (isReadOnly)
		{
		}
		this.isReadOnly = isReadOnly;
		UserInfo.SonMonBuildingInfo sonMonBuildingInfo = sonmon.ListCongTrinh.Find((UserInfo.SonMonBuildingInfo c) => c.LoaiCongTrinh == UserInfo.SonMonBuildingInfo.SonMonBuildingType.CHINH_SANH);
		if (sonMonBuildingInfo != null && sonMonBuildingInfo.LoaiCongTrinh != UserInfo.SonMonBuildingInfo.SonMonBuildingType.NULL_CT)
		{
			ChinhSanhSlot.GetComponent<SonMonBuilding>().Set(sonMonBuildingInfo);
		}
		else
		{
			Debug.Log(sonmon.GID + " " + sonmon.SID);
			ChinhSanhSlot.GetComponent<SonMonBuilding>().Set(sonMonBuildingInfo);
		}
		int i;
		for (i = 0; i < UtilitySlots.Count; i++)
		{
			UserInfo.SonMonBuildingInfo sonMonBuildingInfo2 = sonmon.ListCongTrinh.Find((UserInfo.SonMonBuildingInfo c) => c.Slot == i + 1 && c.LoaiCongTrinh != UserInfo.SonMonBuildingInfo.SonMonBuildingType.CHINH_SANH);
			if (sonMonBuildingInfo2 != null && sonMonBuildingInfo2.LoaiCongTrinh != UserInfo.SonMonBuildingInfo.SonMonBuildingType.NULL_CT)
			{
				UtilitySlots[i].GetComponent<SonMonBuilding>().Set(sonMonBuildingInfo2);
			}
			else
			{
				UtilitySlots[i].GetComponent<SonMonBuilding>().Set(sonMonBuildingInfo2);
			}
		}
	}

	private void OnInfoBtn()
	{
		if (CurBuilding != null && CurBuilding.LoaiCongTrinh != UserInfo.SonMonBuildingInfo.SonMonBuildingType.NULL_CT)
		{
			CurBuilding = GameManager.instance.m_GameClient.UserInfo.SonMon.ListCongTrinh.Find((UserInfo.SonMonBuildingInfo ct) => ct.ID == CurBuilding.ID);
			PopupSonMonCongTrinh.Create(PopupSonMonCongTrinh.State.INFO, CurBuilding);
		}
		else if (CurSlot == "Slot10")
		{
			PopupSonMonXayDung.CreateChinhSanh(GameManager.instance.m_GameClient.UserInfo.SonMon.ListCongTrinh.Find((UserInfo.SonMonBuildingInfo ct) => ct.Slot == 10).ID);
		}
		else
		{
			PopupSonMonXayDung.CreateUtility(GameManager.instance.m_GameClient.UserInfo.SonMon.ListCongTrinh.Find((UserInfo.SonMonBuildingInfo ct) => ct.Slot == int.Parse(CurSlot.Replace("Slot", string.Empty))).ID);
		}
	}
}
