using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScreenTayNai : ScreenBase
{
	public enum ScreenTayNaiTab
	{
		TabVatPham = 0,
		TabManhTrangBi = 1,
		TabTanChuong = 2
	}

	private const int maxItemCount = 12;

	public GameObject TayNaiPerfab;

	public GameObject ManhTrangBiPerfab;

	public GameObject TanChuongPerfab;

	public GameObject ItemRoot;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<TayNaiItem> VatPhamItemList = new List<TayNaiItem>();

	private List<ManhTrangBiItem> ManhTBItemList = new List<ManhTrangBiItem>();

	private List<TanChuongItem> TanChuongItemList = new List<TanChuongItem>();

	private int startItemGUI_Idx;

	private List<UserInfo.VatPhamTieuThuData> ListVPData = new List<UserInfo.VatPhamTieuThuData>();

	private List<UserInfo.ManhTrangBiData> ListMTBData = new List<UserInfo.ManhTrangBiData>();

	private List<UserInfo.ManhVoCongData> ListMVCData = new List<UserInfo.ManhVoCongData>();

	public GameObject mAnimGhep;

	public GameObject ItemParticle;

	public ScreenTayNaiTab m_Tab;

	private Vector3 itemPos;

	private bool dangGhepManh;

	private int lastVatPhamId;

	private void Awake()
	{
		UIAnchor[] componentsInChildren = GetComponentsInChildren<UIAnchor>();
		UIAnchor[] array = componentsInChildren;
		UIAnchor[] array2 = array;
		foreach (UIAnchor uIAnchor in array2)
		{
			uIAnchor.widgetContainer = GUIManager.instance.GameFrame;
		}
	}

	private void Start()
	{
		dragPanel = ItemRoot.GetComponent<UIDraggablePanel>();
		panel = ItemRoot.GetComponent<UIPanel>();
	}

	private void Update()
	{
		Vector4 clipRange = panel.clipRange;
		float num = 0f;
		float num2 = 0f;
		if (VatPhamItemList.Count > 0 && m_Tab == ScreenTayNaiTab.TabVatPham)
		{
			TayNaiItem tayNaiItem = VatPhamItemList[VatPhamItemList.Count - 1];
			num = tayNaiItem.transform.localPosition.y;
			TayNaiItem tayNaiItem2 = VatPhamItemList[0];
			num2 = tayNaiItem2.transform.localPosition.y;
		}
		else if (ManhTBItemList.Count > 0 && m_Tab == ScreenTayNaiTab.TabManhTrangBi)
		{
			ManhTrangBiItem manhTrangBiItem = ManhTBItemList[ManhTBItemList.Count - 1];
			num = manhTrangBiItem.transform.localPosition.y;
			ManhTrangBiItem manhTrangBiItem2 = ManhTBItemList[0];
			num2 = manhTrangBiItem2.transform.localPosition.y;
		}
		else if (TanChuongItemList.Count > 0 && m_Tab == ScreenTayNaiTab.TabTanChuong)
		{
			TanChuongItem tanChuongItem = TanChuongItemList[TanChuongItemList.Count - 1];
			num = tanChuongItem.transform.localPosition.y;
			TanChuongItem tanChuongItem2 = TanChuongItemList[0];
			num2 = tanChuongItem2.transform.localPosition.y;
		}
		if (num - clipRange.y > -250f)
		{
			SwapDragListDown();
		}
		else if (num2 - clipRange.y < 250f)
		{
			SwapDragListUp();
		}
	}

	public void SwapDragListDown()
	{
		int num = startItemGUI_Idx + 12;
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 320f, 0f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, -230f, 0f);
		if (m_Tab == ScreenTayNaiTab.TabVatPham)
		{
			if (num >= ListVPData.Count)
			{
				return;
			}
			TayNaiItem tayNaiItem = VatPhamItemList[0];
			tayNaiItem.TayNaiID = num;
			TayNaiItem tayNaiItem2 = VatPhamItemList[VatPhamItemList.Count - 1];
			VatPhamItemList.RemoveAt(0);
			VatPhamItemList.Add(tayNaiItem);
			tayNaiItem.transform.localPosition = tayNaiItem2.transform.localPosition + vector2;
			tayNaiItem.SetTayNaiData(ListVPData[num]);
		}
		else if (m_Tab == ScreenTayNaiTab.TabManhTrangBi)
		{
			if (num >= ListMTBData.Count)
			{
				return;
			}
			ManhTrangBiItem manhTrangBiItem = ManhTBItemList[0];
			manhTrangBiItem.ManhTrangBiID = num;
			ManhTrangBiItem manhTrangBiItem2 = ManhTBItemList[ManhTBItemList.Count - 1];
			ManhTBItemList.RemoveAt(0);
			ManhTBItemList.Add(manhTrangBiItem);
			manhTrangBiItem.transform.localPosition = manhTrangBiItem2.transform.localPosition + vector2;
			manhTrangBiItem.SetManhTBData(ListMTBData[num]);
		}
		else if (m_Tab == ScreenTayNaiTab.TabTanChuong)
		{
			if (num >= ListMVCData.Count)
			{
				return;
			}
			TanChuongItem tanChuongItem = TanChuongItemList[0];
			tanChuongItem.TanChuongID = num;
			TanChuongItem tanChuongItem2 = TanChuongItemList[TanChuongItemList.Count - 1];
			TanChuongItemList.RemoveAt(0);
			TanChuongItemList.Add(tanChuongItem);
			tanChuongItem.transform.localPosition = tanChuongItem2.transform.localPosition + vector2;
			tanChuongItem.SetTanChuongData(ListMVCData[num]);
		}
		startItemGUI_Idx++;
	}

	public void SwapDragListUp()
	{
		if (startItemGUI_Idx == 0)
		{
			return;
		}
		startItemGUI_Idx--;
		int num = startItemGUI_Idx;
		if (num >= 0)
		{
			Vector3 vector = default(Vector3);
			vector = new Vector3(0f, 320f, 0f);
			Vector3 vector2 = default(Vector3);
			vector2 = new Vector3(0f, -230f, 0f);
			if (m_Tab == ScreenTayNaiTab.TabVatPham)
			{
				TayNaiItem tayNaiItem = VatPhamItemList[0];
				TayNaiItem tayNaiItem2 = VatPhamItemList[VatPhamItemList.Count - 1];
				tayNaiItem2.TayNaiID = num;
				VatPhamItemList.RemoveAt(VatPhamItemList.Count - 1);
				VatPhamItemList.Insert(0, tayNaiItem2);
				tayNaiItem2.transform.localPosition = tayNaiItem.transform.localPosition - vector2;
				tayNaiItem2.SetTayNaiData(ListVPData[num]);
			}
			else if (m_Tab == ScreenTayNaiTab.TabManhTrangBi)
			{
				ManhTrangBiItem manhTrangBiItem = ManhTBItemList[0];
				ManhTrangBiItem manhTrangBiItem2 = ManhTBItemList[ManhTBItemList.Count - 1];
				manhTrangBiItem2.ManhTrangBiID = num;
				ManhTBItemList.RemoveAt(ManhTBItemList.Count - 1);
				ManhTBItemList.Insert(0, manhTrangBiItem2);
				manhTrangBiItem2.transform.localPosition = manhTrangBiItem.transform.localPosition - vector2;
				manhTrangBiItem2.SetManhTBData(ListMTBData[num]);
			}
			else if (m_Tab == ScreenTayNaiTab.TabTanChuong)
			{
				TanChuongItem tanChuongItem = TanChuongItemList[0];
				TanChuongItem tanChuongItem2 = TanChuongItemList[TanChuongItemList.Count - 1];
				tanChuongItem2.TanChuongID = num;
				TanChuongItemList.RemoveAt(TanChuongItemList.Count - 1);
				TanChuongItemList.Insert(0, tanChuongItem2);
				tanChuongItem2.transform.localPosition = tanChuongItem.transform.localPosition - vector2;
				tanChuongItem2.SetTanChuongData(ListMVCData[num]);
			}
		}
	}

	public void ClearGUIItem()
	{
		startItemGUI_Idx = 0;
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		VatPhamItemList.Clear();
		ManhTBItemList.Clear();
		TanChuongItemList.Clear();
	}

	public void SyncWithNetworkData(bool forceRecreate = false)
	{
		ListVPData.Clear();
		ListMTBData.Clear();
		ListMVCData.Clear();
		if (m_Tab == ScreenTayNaiTab.TabVatPham)
		{
			if (GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList != null || GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Count > 0)
			{
				List<UserInfo.VatPhamTieuThuData> list = sortVatPhamTieuThu(GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList);
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i].Quantity > 0)
					{
						ListVPData.Add(list[i]);
					}
				}
			}
		}
		else if (m_Tab == ScreenTayNaiTab.TabManhTrangBi)
		{
			if (GameManager.instance.m_GameClient.UserInfo.ManhTrangBiList != null && GameManager.instance.m_GameClient.UserInfo.ManhTrangBiList.Count >= 0)
			{
				GameManager.instance.m_GameClient.UserInfo.ManhTrangBiList.Sort((UserInfo.ManhTrangBiData x, UserInfo.ManhTrangBiData y) => CompareManhTrangBi(x.Name, x.Quantity, y.Name, y.Quantity));
				for (int num = 0; num < GameManager.instance.m_GameClient.UserInfo.ManhTrangBiList.Count; num++)
				{
					if (GameManager.instance.m_GameClient.UserInfo.ManhTrangBiList[num].Quantity > 0)
					{
						ListMTBData.Add(GameManager.instance.m_GameClient.UserInfo.ManhTrangBiList[num]);
					}
				}
			}
		}
		else if (m_Tab == ScreenTayNaiTab.TabTanChuong && GameManager.instance.m_GameClient.UserInfo.ManhVoCongList != null && GameManager.instance.m_GameClient.UserInfo.ManhVoCongList.Count >= 0)
		{
			GameManager.instance.m_GameClient.UserInfo.ManhVoCongList.Sort((UserInfo.ManhVoCongData x, UserInfo.ManhVoCongData y) => CompareManhVoCong(x.Name, x.Quantity, y.Name, y.Quantity));
			for (int num2 = 0; num2 < GameManager.instance.m_GameClient.UserInfo.ManhVoCongList.Count; num2++)
			{
				if (GameManager.instance.m_GameClient.UserInfo.ManhVoCongList[num2].Quantity > 0)
				{
					ListMVCData.Add(GameManager.instance.m_GameClient.UserInfo.ManhVoCongList[num2]);
				}
			}
		}
		itemPos = new Vector3(0f, 320f, 0f);
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, -230f, 0f);
		int num3 = 0;
		if ((ItemRoot.transform.childCount == 0) | forceRecreate)
		{
			ClearGUIItem();
			if (m_Tab == ScreenTayNaiTab.TabVatPham && ListVPData.Count > 0)
			{
				for (int num4 = startItemGUI_Idx; num4 < ListVPData.Count; num4++)
				{
					UserInfo.VatPhamTieuThuData vatPhamTieuThuData = ListVPData[num4];
					TayNaiItem component = ((GameObject)Object.Instantiate(TayNaiPerfab)).GetComponent<TayNaiItem>();
					component.TayNaiID = num4;
					component.transform.parent = ItemRoot.transform;
					component.transform.localScale = new Vector3(1f, 1f, 1f);
					component.transform.localPosition = itemPos;
					component.name = vatPhamTieuThuData.Name;
					itemPos += vector;
					UIEventListener.Get(component.btnUse100Lan.gameObject).onClick = dung100VatPham_OnClick;
					UIEventListener.Get(component.btnUse10Lan.gameObject).onClick = dung10VatPham_OnClick;
					UIEventListener.Get(component.btnUse.gameObject).onClick = dungVatPham_OnClick;
					UIEventListener.Get(component.avatar.gameObject).onClick = vpAvatar_OnClick;
					component.SetTayNaiData(vatPhamTieuThuData);
					VatPhamItemList.Add(component);
					num3++;
					if (num3 >= 12)
					{
						break;
					}
				}
			}
			else if (m_Tab == ScreenTayNaiTab.TabManhTrangBi && ListMTBData.Count > 0)
			{
				for (int num5 = startItemGUI_Idx; num5 < ListMTBData.Count; num5++)
				{
					UserInfo.ManhTrangBiData manhTBData = ListMTBData[num5];
					ManhTrangBiItem component2 = ((GameObject)Object.Instantiate(ManhTrangBiPerfab)).GetComponent<ManhTrangBiItem>();
					component2.ManhTrangBiID = num5;
					component2.transform.parent = ItemRoot.transform;
					component2.transform.localScale = new Vector3(1f, 1f, 1f);
					component2.transform.localPosition = itemPos;
					itemPos += vector;
					component2.SetManhTBData(manhTBData);
					UIEventListener.Get(component2.btnGhep.gameObject).onClick = ghepManhTB_OnClick;
					ManhTBItemList.Add(component2);
					num3++;
					if (num3 >= 12)
					{
						break;
					}
				}
			}
			else if (m_Tab == ScreenTayNaiTab.TabTanChuong && ListMVCData.Count > 0)
			{
				for (int num6 = startItemGUI_Idx; num6 < ListMVCData.Count; num6++)
				{
					UserInfo.ManhVoCongData tanChuongData = ListMVCData[num6];
					TanChuongItem component3 = ((GameObject)Object.Instantiate(TanChuongPerfab)).GetComponent<TanChuongItem>();
					component3.TanChuongID = num6;
					component3.transform.parent = ItemRoot.transform;
					component3.transform.localScale = new Vector3(1f, 1f, 1f);
					component3.transform.localPosition = itemPos;
					itemPos += vector;
					component3.SetTanChuongData(tanChuongData);
					UIEventListener.Get(component3.btnGhep.gameObject).onClick = ghepTanChuong_OnClick;
					UIEventListener.Get(component3.btnGhep10Lan.gameObject).onClick = ghep10TanChuong_OnClick;
					TanChuongItemList.Add(component3);
					num3++;
					if (num3 >= 12)
					{
						break;
					}
				}
			}
			UIDraggablePanel component4 = ItemRoot.GetComponent<UIDraggablePanel>();
			component4.ResetPosition();
		}
		else if (m_Tab == ScreenTayNaiTab.TabVatPham)
		{
			if (ListVPData.Count >= 12)
			{
				if (startItemGUI_Idx + 12 >= ListVPData.Count)
				{
					startItemGUI_Idx = ListVPData.Count - 12;
				}
			}
			else
			{
				startItemGUI_Idx = 0;
			}
			for (int num7 = startItemGUI_Idx; num7 < ListVPData.Count; num7++)
			{
				UserInfo.VatPhamTieuThuData tayNaiData = ListVPData[num7];
				if (num3 < VatPhamItemList.Count)
				{
					VatPhamItemList[num3].TayNaiID = num7;
					VatPhamItemList[num3].SetTayNaiData(tayNaiData);
				}
				else
				{
					TayNaiItem component5 = ((GameObject)Object.Instantiate(TayNaiPerfab)).GetComponent<TayNaiItem>();
					component5.TayNaiID = num7;
					component5.transform.parent = ItemRoot.transform;
					component5.transform.localScale = new Vector3(1f, 1f, 1f);
					component5.transform.localPosition = VatPhamItemList[VatPhamItemList.Count - 1].transform.localPosition + vector;
					UIEventListener.Get(component5.btnUse100Lan.gameObject).onClick = dung100VatPham_OnClick;
					UIEventListener.Get(component5.btnUse10Lan.gameObject).onClick = dung10VatPham_OnClick;
					UIEventListener.Get(component5.btnUse.gameObject).onClick = dungVatPham_OnClick;
					UIEventListener.Get(component5.avatar.gameObject).onClick = vpAvatar_OnClick;
					component5.SetTayNaiData(tayNaiData);
					VatPhamItemList.Add(component5);
				}
				num3++;
				if (num3 >= 12)
				{
					break;
				}
			}
			if (ListVPData.Count < 12 && VatPhamItemList.Count > ListVPData.Count)
			{
				int index = num3;
				int num8 = 0;
				for (; num3 < VatPhamItemList.Count; num3++)
				{
					Object.Destroy(VatPhamItemList[num3].gameObject);
					num8++;
				}
				if (num8 > 0)
				{
					VatPhamItemList.RemoveRange(index, num8);
				}
				UIDraggablePanel component6 = ItemRoot.GetComponent<UIDraggablePanel>();
				component6.ResetPosition();
			}
		}
		else if (m_Tab == ScreenTayNaiTab.TabManhTrangBi)
		{
			if (ListMTBData.Count >= 12)
			{
				if (startItemGUI_Idx + 12 >= ListMTBData.Count)
				{
					startItemGUI_Idx = ListMTBData.Count - 12;
				}
			}
			else
			{
				startItemGUI_Idx = 0;
			}
			for (int num9 = startItemGUI_Idx; num9 < ListMTBData.Count; num9++)
			{
				UserInfo.ManhTrangBiData manhTBData2 = ListMTBData[num9];
				if (num3 < ManhTBItemList.Count)
				{
					ManhTBItemList[num3].ManhTrangBiID = num9;
					ManhTBItemList[num3].SetManhTBData(manhTBData2);
				}
				else
				{
					ManhTrangBiItem component7 = ((GameObject)Object.Instantiate(ManhTrangBiPerfab)).GetComponent<ManhTrangBiItem>();
					component7.ManhTrangBiID = num9;
					component7.transform.parent = ItemRoot.transform;
					component7.transform.localScale = new Vector3(1f, 1f, 1f);
					component7.transform.localPosition = ManhTBItemList[ManhTBItemList.Count - 1].transform.localPosition + vector;
					component7.SetManhTBData(manhTBData2);
					UIEventListener.Get(component7.btnGhep.gameObject).onClick = ghepManhTB_OnClick;
					ManhTBItemList.Add(component7);
				}
				num3++;
				if (num3 >= 12)
				{
					break;
				}
			}
			if (ListMTBData.Count < 12 && ManhTBItemList.Count > ListMTBData.Count)
			{
				int index2 = num3;
				int num10 = 0;
				for (; num3 < ManhTBItemList.Count; num3++)
				{
					Object.Destroy(ManhTBItemList[num3].gameObject);
					num10++;
				}
				if (num10 > 0)
				{
					ManhTBItemList.RemoveRange(index2, num10);
				}
				UIDraggablePanel component8 = ItemRoot.GetComponent<UIDraggablePanel>();
				component8.ResetPosition();
			}
		}
		else
		{
			if (m_Tab != ScreenTayNaiTab.TabTanChuong)
			{
				return;
			}
			if (ListMVCData.Count >= 12)
			{
				if (startItemGUI_Idx + 12 >= ListMVCData.Count)
				{
					startItemGUI_Idx = ListMVCData.Count - 12;
				}
			}
			else
			{
				startItemGUI_Idx = 0;
			}
			for (int num11 = startItemGUI_Idx; num11 < ListMVCData.Count; num11++)
			{
				UserInfo.ManhVoCongData tanChuongData2 = ListMVCData[num11];
				if (num3 < TanChuongItemList.Count)
				{
					TanChuongItemList[num3].TanChuongID = num11;
					TanChuongItemList[num3].SetTanChuongData(tanChuongData2);
				}
				else
				{
					TanChuongItem component9 = ((GameObject)Object.Instantiate(TanChuongPerfab)).GetComponent<TanChuongItem>();
					component9.TanChuongID = num11;
					component9.transform.parent = ItemRoot.transform;
					component9.transform.localScale = new Vector3(1f, 1f, 1f);
					component9.transform.localPosition = TanChuongItemList[TanChuongItemList.Count - 1].transform.localPosition + vector;
					component9.SetTanChuongData(tanChuongData2);
					UIEventListener.Get(component9.btnGhep.gameObject).onClick = ghepTanChuong_OnClick;
					TanChuongItemList.Add(component9);
				}
				num3++;
				if (num3 >= 12)
				{
					break;
				}
			}
			if (ListMVCData.Count < 12 && TanChuongItemList.Count > ListMVCData.Count)
			{
				int index3 = num3;
				int num12 = 0;
				for (; num3 < TanChuongItemList.Count; num3++)
				{
					Object.Destroy(TanChuongItemList[num3].gameObject);
					num12++;
				}
				if (num12 > 0)
				{
					TanChuongItemList.RemoveRange(index3, num12);
				}
				UIDraggablePanel component10 = ItemRoot.GetComponent<UIDraggablePanel>();
				component10.ResetPosition();
			}
		}
	}

	public List<UserInfo.VatPhamTieuThuData> sortVatPhamTieuThu(List<UserInfo.VatPhamTieuThuData> listBanDau)
	{
		List<UserInfo.VatPhamTieuThuData> list = new List<UserInfo.VatPhamTieuThuData>();
		if (ConfigManager.instance.m_dicVatPhamTieuThu != null && listBanDau != null && ConfigManager.instance.m_dicVatPhamTieuThu.Count > 0 && listBanDau.Count > 0)
		{
			for (int i = 0; i < ConfigManager.instance.m_dicVatPhamTieuThu.Count; i++)
			{
				for (int j = 0; j < listBanDau.Count; j++)
				{
					if (ConfigManager.instance.m_dicVatPhamTieuThu.Values.ElementAt(i).Name == listBanDau[j].Name)
					{
						list.Add(listBanDau[j]);
						break;
					}
				}
			}
		}
		return list;
	}

	private static string ManhTrangBiCfgKey(string codeName)
	{
		string text = codeName ?? string.Empty;
		if (text.StartsWith("MMAG_"))
		{
			text = text.Substring(1);
		}
		if (text.StartsWith("MVK_") || text.StartsWith("MMU_") || text.StartsWith("MAG_") || text.StartsWith("MTS_"))
		{
			text = text.Substring(1);
		}
		return text;
	}

	private static string ManhVoCongCfgKey(string codeName)
	{
		string text = codeName ?? string.Empty;
		if (text.StartsWith("MVC_"))
		{
			text = "VC_" + text.Substring(4);
		}
		return text;
	}

	public int CompareManhTrangBi(string codeName1, int quantity1, string codeName2, int quantity2)
	{
		TrangBiCfg value = null;
		TrangBiCfg value2 = null;
		ConfigManager.instance.m_dicTrangBi.TryGetValue(ManhTrangBiCfgKey(codeName1), out value);
		if (value == null)
		{
			ConfigManager.instance.m_dicTrangBi.TryGetValue(codeName1, out value);
		}
		ConfigManager.instance.m_dicTrangBi.TryGetValue(ManhTrangBiCfgKey(codeName2), out value2);
		if (value2 == null)
		{
			ConfigManager.instance.m_dicTrangBi.TryGetValue(codeName2, out value2);
		}
		int num = (int)((value != null) ? value.Hang : ((ItemClass)(-1)));
		int num2 = (int)((value2 != null) ? value2.Hang : ((ItemClass)(-1)));
		if (num != num2)
		{
			return (num <= num2) ? 1 : (-1);
		}
		if (quantity1 != quantity2)
		{
			return (quantity1 <= quantity2) ? 1 : (-1);
		}
		return string.CompareOrdinal(codeName1, codeName2);
	}

	public int CompareManhVoCong(string codeName1, int quantity1, string codeName2, int quantity2)
	{
		CfgVoCong value = null;
		CfgVoCong value2 = null;
		ConfigManager.instance.m_dicVCs.TryGetValue(ManhVoCongCfgKey(codeName1), out value);
		if (value == null)
		{
			ConfigManager.instance.m_dicVCs.TryGetValue(codeName1, out value);
		}
		ConfigManager.instance.m_dicVCs.TryGetValue(ManhVoCongCfgKey(codeName2), out value2);
		if (value2 == null)
		{
			ConfigManager.instance.m_dicVCs.TryGetValue(codeName2, out value2);
		}
		int num = ((value != null) ? value.Hang : (-1));
		int num2 = ((value2 != null) ? value2.Hang : (-1));
		if (num != num2)
		{
			return (num <= num2) ? 1 : (-1);
		}
		if (quantity1 != quantity2)
		{
			return (quantity1 <= quantity2) ? 1 : (-1);
		}
		return string.CompareOrdinal(codeName1, codeName2);
	}

	public void ghepTanChuong_OnClick(GameObject go)
	{
		if (!dangGhepManh)
		{
			TanChuongItem component = go.transform.parent.parent.GetComponent<TanChuongItem>();
			GhepManhVoCongRequest ghepManhVoCongRequest = new GhepManhVoCongRequest();
			ghepManhVoCongRequest.ManhID = component.m_Data.ID;
			dangGhepManh = true;
			GameManager.instance.m_GameClient.RequestGhepManhVoCong(ghepManhVoCongRequest);
		}
	}

	public void ghep10TanChuong_OnClick(GameObject go)
	{
		if (!dangGhepManh)
		{
			TanChuongItem component = go.transform.parent.parent.GetComponent<TanChuongItem>();
			GhepManhVoCongRequest ghepManhVoCongRequest = new GhepManhVoCongRequest();
			ghepManhVoCongRequest.ManhID = component.m_Data.ID;
			dangGhepManh = true;
			ghepManhVoCongRequest.Count = 10;
			GameManager.instance.m_GameClient.RequestGhepManhVoCong(ghepManhVoCongRequest);
		}
	}

	public void ghepManhTB_OnClick(GameObject go)
	{
		if (!dangGhepManh)
		{
			ManhTrangBiItem component = go.transform.parent.parent.GetComponent<ManhTrangBiItem>();
			GhepManhTrangBiRequest ghepManhTrangBiRequest = new GhepManhTrangBiRequest();
			ghepManhTrangBiRequest.ManhID = component.m_Data.ID;
			dangGhepManh = true;
			GameManager.instance.m_GameClient.RequestGhepManhTrangBi(ghepManhTrangBiRequest);
		}
	}

	public void dungVatPham_OnClick(GameObject go)
	{
		TayNaiItem component = go.transform.parent.parent.GetComponent<TayNaiItem>();
		UserInfo.VatPhamTieuThuData data = component.m_Data;
		if (data == null)
		{
			return;
		}
		if (data.Name.StartsWith("VP_HOP"))
		{
			int num = checkHasKey(data, 1);
			if (num > 0)
			{
				OpenHopRequest openHopRequest = new OpenHopRequest();
				openHopRequest.HopID = data.ID;
				openHopRequest.KeyID = num;
				GameManager.instance.m_GameClient.RequestOpenHop(openHopRequest);
			}
		}
		else if (data.Name.StartsWith("VP_TUITHAN"))
		{
			PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
			for (int i = 0; i < ConfigManager.instance.OtherConfig.TuiThanList.Count; i++)
			{
				if (ConfigManager.instance.OtherConfig.TuiThanList[i].CodeName == data.Name)
				{
					phanThuongResponse.PhanThuongList = ConfigManager.instance.OtherConfig.TuiThanList[i].PhanThuongList;
				}
			}
			PopupSelectPhanThuong.Create(ConfigManager.instance.m_dicVatPhamTieuThu[data.Name].TenHienThi, Localization.instance.Get("HopThanMoiMo"), phanThuongResponse, data);
		}
		else if (data.Name == "VP_GA_QUAY" || data.Name == "VP_LUAN_KIEM_LENH" || data.Name.StartsWith("VP_CUSTOM_") || data.Name == "VP_BANH_CHUNG")
		{
			UseCustomItemRequest useCustomItemRequest = new UseCustomItemRequest();
			useCustomItemRequest.ID = data.ID;
			GameManager.instance.m_GameClient.RequestUseCustomItem(useCustomItemRequest);
		}
		else if (data.Name == "VP_DOI_TEN")
		{
			PopupDatTenMonPhai.Create();
		}
		else if (data.Name == "VP_BOI_DUONG_DAN" || data.Name == "VP_TAY_TUY_DAN")
		{
			MessagePopup.Create("Vui lòng vào giao diện Đệ Tử -> Bồi Dưỡng để sử dụng!");
		}
		else if (data.Name == "VP_BAT_QUAI_TRAN_DO")
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenDoiHinh);
			ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
			screenDoiHinh.displayBatQuaiTranDo();
		}
		else if (data.Name == "VP_RUONG_THAN_BI")
		{
			if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.RuongThanBiConfig != null)
			{
				if (GameManager.instance.m_GameClient.ServerTime < GameManager.instance.m_GameClient.UserInfo.ServerInfo.RuongThanBiConfig.ThoiGianBatDau || GameManager.instance.m_GameClient.ServerTime > GameManager.instance.m_GameClient.UserInfo.ServerInfo.RuongThanBiConfig.ThoiGianKetThuc)
				{
					MessagePopup.Create(Localization.instance.Get("ThongBaoEventDaKetThuc"));
				}
				else
				{
					PopupRuongThanBiDetail.Create(data, GameManager.instance.m_GameClient.UserInfo.ServerInfo.RuongThanBiConfig, true);
				}
			}
		}
		else if (data.Name.StartsWith("VP_NGOC_"))
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenTrangBi);
		}
		else if (data.Name == "VP_NGUYEN_KHI_DAN")
		{
			string value = "NguyenKhi";
			if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains(value))
			{
				MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
			}
			else if (GameManager.instance.m_GameClient.UserInfo.Gamer.Level >= 28)
			{
				GUIManager.instance.SetScreen(GAME_SCREEN.ScreenNguyenKhi);
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("ChuaDuDieuKienDungDuocVien"));
			}
		}
		else if (data.Name.StartsWith("VP_SEED_"))
		{
			string value2 = "NguyenKhi";
			if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains(value2))
			{
				MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
			}
			else if (GameManager.instance.m_GameClient.UserInfo.Gamer.Level >= 28)
			{
				GUIManager.instance.SetScreen(GAME_SCREEN.ScreenLienMinhTrongCay);
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("ChuaDuDieuKienDungDuocVien"));
			}
		}
		else if (data.Name.StartsWith("VP_RUONGTHAN_"))
		{
			List<string> list = ConfigManager.instance.OtherConfig.HopThanList[data.Name];
			PhanThuongResponse phanThuongResponse2 = new PhanThuongResponse();
			foreach (string item in list)
			{
				PhanThuongResponse.PhanThuong phanThuong = new PhanThuongResponse.PhanThuong();
				phanThuong.Level = 1;
				phanThuong.Count = 1;
				phanThuong.Name = item;
				if (item.StartsWith("VC_"))
				{
					phanThuong.Loai = PhanThuongResponse.LoaiPhanThuong.VO_CONG;
				}
				else if (item.StartsWith("MVC_"))
				{
					phanThuong.Loai = PhanThuongResponse.LoaiPhanThuong.MANH_VO_CONG;
				}
				else if (item.StartsWith("VK_") || item.StartsWith("MU_") || item.StartsWith("AG_") || item.StartsWith("TS_"))
				{
					phanThuong.Loai = PhanThuongResponse.LoaiPhanThuong.TRANG_BI;
				}
				else if (item.StartsWith("MVK_") || item.StartsWith("MMU_") || item.StartsWith("MAG_") || item.StartsWith("MTS_"))
				{
					phanThuong.Loai = PhanThuongResponse.LoaiPhanThuong.MANH_TRANG_BI;
				}
				else if (item.StartsWith("NGUA_"))
				{
					phanThuong.Loai = PhanThuongResponse.LoaiPhanThuong.THU_CUOI;
				}
				else if (item.StartsWith("NV_"))
				{
					phanThuong.Loai = PhanThuongResponse.LoaiPhanThuong.HON_NHAN_VAT;
				}
				else if (item.StartsWith("PET_"))
				{
					phanThuong.Loai = PhanThuongResponse.LoaiPhanThuong.THAN_THU;
				}
				else if (item.StartsWith("NK_"))
				{
					phanThuong.Loai = PhanThuongResponse.LoaiPhanThuong.NGUYEN_KHI;
				}
				phanThuongResponse2.PhanThuongList.Add(phanThuong);
			}
			PopupSelectPhanThuong.Create(ConfigManager.instance.m_dicVatPhamTieuThu[data.Name].TenHienThi, Localization.instance.Get("HopThanMoiMo"), phanThuongResponse2, data);
		}
		else if (data.Name == "VP_TRUYEN_CONG_1" || data.Name == "VP_TRUYEN_CONG_2")
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenDeTu);
			ScreenDeTu screenDeTu = GUIManager.getScreen(GAME_SCREEN.ScreenDeTu) as ScreenDeTu;
			screenDeTu.onClick_DetuTab(true);
		}
		else if (data.Name == "VP_VONG_QUAY")
		{
			ScreenVongQuay.id = data.ID;
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenVongQuay);
		}
		else if (data.Name == "VP_HUA_NGUYEN")
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenKyNgoHuaNguyen);
		}
		else if (data.Name == "VP_TAY_TUY_DAN" || data.Name == "VP_BOI_DUONG_DAN")
		{
			MessagePopup.Create("Vui lòng vào giao diện Đệ Tử -> Bồi Dưỡng để sử dụng!");
		}
		else if (data.Name == "VP_DOI_TEN_BANG")
		{
			PopupDoiTenBang.Create();
		}
		else if (data.Name == "VP_LINH_DAN_1" || data.Name == "VP_HOA_THAN_DAN")
		{
			lastVatPhamId = data.ID;
			PopupSelectNhanVat.Create(OnLinhDanUsed);
		}
		else if (data.Name == "VP_LINH_DAN_2")
		{
			UseCustomItemRequest useCustomItemRequest2 = new UseCustomItemRequest();
			useCustomItemRequest2.ID = data.ID;
			GameManager.instance.m_GameClient.RequestUseCustomItem(useCustomItemRequest2);
		}
		else
		{
			UseCustomItemRequest useCustomItemRequest3 = new UseCustomItemRequest();
			useCustomItemRequest3.ID = data.ID;
			GameManager.instance.m_GameClient.RequestUseCustomItem(useCustomItemRequest3);
		}
	}

	public bool OnLinhDanUsed(int hero)
	{
		UseCustomItemRequest useCustomItemRequest = new UseCustomItemRequest();
		useCustomItemRequest.ID = lastVatPhamId;
		useCustomItemRequest.Count = 1;
		useCustomItemRequest.parameter = hero.ToString();
		GameManager.instance.m_GameClient.RequestUseCustomItem(useCustomItemRequest);
		return true;
	}

	public bool OnLinhDanUsed10(int hero)
	{
		UseCustomItemRequest useCustomItemRequest = new UseCustomItemRequest();
		useCustomItemRequest.ID = lastVatPhamId;
		useCustomItemRequest.Count = 10;
		useCustomItemRequest.parameter = hero.ToString();
		GameManager.instance.m_GameClient.RequestUseCustomItem(useCustomItemRequest);
		return true;
	}

	public bool OnLinhDanUsed100(int hero)
	{
		UseCustomItemRequest useCustomItemRequest = new UseCustomItemRequest();
		useCustomItemRequest.ID = lastVatPhamId;
		useCustomItemRequest.Count = 100;
		useCustomItemRequest.parameter = hero.ToString();
		GameManager.instance.m_GameClient.RequestUseCustomItem(useCustomItemRequest);
		return true;
	}

	public void vpAvatar_OnClick(GameObject go)
	{
	}

	public void dung10VatPham_OnClick(GameObject go)
	{
		TayNaiItem component = go.transform.parent.parent.GetComponent<TayNaiItem>();
		UserInfo.VatPhamTieuThuData data = component.m_Data;
		if (data == null)
		{
			return;
		}
		if (data.Name.StartsWith("VP_CUSTOM_") || data.Name == "VP_BANH_CHUNG")
		{
			if (checBoxCount(data, 10))
			{
				UseCustomItemRequest useCustomItemRequest = new UseCustomItemRequest();
				useCustomItemRequest.ID = data.ID;
				useCustomItemRequest.Count = 10;
				GameManager.instance.m_GameClient.RequestUseCustomItem(useCustomItemRequest);
			}
		}
		else if (data.Name.StartsWith("VP_LINH_DAN_1"))
		{
			if (checBoxCount(data, 10))
			{
				lastVatPhamId = data.ID;
				PopupSelectNhanVat.Create(OnLinhDanUsed10);
			}
		}
		else if (data.Name.StartsWith("VP_LINH_DAN_2"))
		{
			if (checBoxCount(data, 10))
			{
				UseCustomItemRequest useCustomItemRequest2 = new UseCustomItemRequest();
				useCustomItemRequest2.ID = data.ID;
				useCustomItemRequest2.Count = 10;
				GameManager.instance.m_GameClient.RequestUseCustomItem(useCustomItemRequest2);
			}
		}
		else if (data.Name.StartsWith("VP_BOI_DUONG_DAN") || data.Name.StartsWith("VP_TAY_TUY_DAN"))
		{
			MessagePopup.Create("Vui lòng vào giao diện Đệ Tử -> Bồi Dưỡng để sử dụng!");
		}
		else if (data.Name.StartsWith("VP_HOA_THAN_DAN"))
		{
			if (checBoxCount(data, 10))
			{
				lastVatPhamId = data.ID;
				PopupSelectNhanVat.Create(OnLinhDanUsed10);
			}
		}
		else if (data.Name.StartsWith("VP_HOP"))
		{
			if (checBoxCount(data, 10))
			{
				int num = checkHasKey(data, 10);
				if (num > 0)
				{
					OpenHopRequest openHopRequest = new OpenHopRequest();
					openHopRequest.HopID = data.ID;
					openHopRequest.KeyID = num;
					openHopRequest.Count = 10;
					GameManager.instance.m_GameClient.RequestOpenHop(openHopRequest);
				}
			}
		}
		else if (checBoxCount(data, 10))
		{
			UseCustomItemRequest useCustomItemRequest3 = new UseCustomItemRequest();
			useCustomItemRequest3.ID = data.ID;
			useCustomItemRequest3.Count = 10;
			GameManager.instance.m_GameClient.RequestUseCustomItem(useCustomItemRequest3);
		}
	}

	public void dung100VatPham_OnClick(GameObject go)
	{
		TayNaiItem component = go.transform.parent.parent.GetComponent<TayNaiItem>();
		UserInfo.VatPhamTieuThuData data = component.m_Data;
		if (data == null)
		{
			return;
		}
		if (data.Name.StartsWith("VP_BOI_DUONG_DAN") || data.Name.StartsWith("VP_TAY_TUY_DAN"))
		{
			MessagePopup.Create("Vui lòng vào giao diện Đệ Tử -> Bồi Dưỡng để sử dụng!");
		}
		else if (data.Name.StartsWith("VP_HOA_THAN_DAN") || data.Name.StartsWith("VP_LINH_DAN_1"))
		{
			if (checBoxCount(data, 100))
			{
				lastVatPhamId = data.ID;
				PopupSelectNhanVat.Create(OnLinhDanUsed100);
			}
		}
		else if (checBoxCount(data, 100))
		{
			int num = checkHasKey(data, 10);
			if (num > 0)
			{
				OpenHopRequest openHopRequest = new OpenHopRequest();
				openHopRequest.HopID = data.ID;
				openHopRequest.KeyID = num;
				openHopRequest.Count = 10;
				GameManager.instance.m_GameClient.RequestOpenHop(openHopRequest);
			}
		}
	}

	public bool checBoxCount(UserInfo.VatPhamTieuThuData vpData, int countBox)
	{
		if (vpData != null && vpData.Quantity >= countBox)
		{
			return true;
		}
		if (vpData.Name.StartsWith("VP_CUSTOM_") || vpData.Name == "VP_BANH_CHUNG")
		{
			MessagePopup.Create(Localization.instance.Get("SoLuongVatPhamKhongDuDung"));
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("KhongDuBoxMess"));
		}
		return false;
	}

	public int checkHasKey(UserInfo.VatPhamTieuThuData vpData, int countKey)
	{
		string text = vpData.Name + "_KEY";
		if (ListVPData != null && ListVPData.Count > 0)
		{
			for (int i = 0; i < ListVPData.Count; i++)
			{
				if (ListVPData[i].Name == text)
				{
					if (ListVPData[i].Quantity >= countKey)
					{
						return ListVPData[i].ID;
					}
					MessagePopup.Create(Localization.instance.Get("KhongDuKeyMess"));
					return 0;
				}
			}
		}
		MessagePopup.Create(Localization.instance.Get("KhongCoKeyMess"));
		return 0;
	}

	public void onClick_VatPhamItem(GameObject go)
	{
		TayNaiItem component = go.GetComponent<TayNaiItem>();
	}

	public override void OnActive()
	{
		base.OnActive();
		SyncWithNetworkData();
		dangGhepManh = false;
		if (!(ItemParticle != null))
		{
			return;
		}
		foreach (Transform item in ItemParticle.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
	}

	private void onClick_ManhTrangBiBtn(bool isActive)
	{
		if (isActive && m_Tab != ScreenTayNaiTab.TabManhTrangBi)
		{
			m_Tab = ScreenTayNaiTab.TabManhTrangBi;
			startItemGUI_Idx = 0;
			SyncWithNetworkData(true);
		}
	}

	private void onClick_TanChuongBtn(bool isActive)
	{
		if (isActive && m_Tab != ScreenTayNaiTab.TabTanChuong)
		{
			m_Tab = ScreenTayNaiTab.TabTanChuong;
			startItemGUI_Idx = 0;
			SyncWithNetworkData(true);
		}
	}

	private void onClick_VatPhamBtn(bool isActive)
	{
		if (isActive && m_Tab != ScreenTayNaiTab.TabVatPham)
		{
			m_Tab = ScreenTayNaiTab.TabVatPham;
			startItemGUI_Idx = 0;
			SyncWithNetworkData(true);
		}
	}

	public void openListPhanThuong(PhanThuongResponse response)
	{
		PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongNhanDuoc"), response);
	}

	public void displayMessResponse(UseCustomItemResponse response)
	{
		if (response.VatPhamName == "VP_GA_QUAY")
		{
			MessagePopup.Create(Localization.instance.Get("HoiTheLucThanhCongMess"));
		}
		else if (response.VatPhamName == "VP_LUAN_KIEM_LENH")
		{
			MessagePopup.Create(Localization.instance.Get("HoiLuanKiemThanhCongMess"));
		}
		else if (response.PhanThuong.PhanThuongList != null && response.PhanThuong.PhanThuongList.Count > 0)
		{
			openListPhanThuong(response.PhanThuong);
		}
		else
		{
			MessagePopup.Create(response.ErrorMessage);
		}
		if ((response.VatPhamName == "VP_LINH_DAN_1" || response.VatPhamName == "VP_LINH_DAN_2") && TutorialPopup.instance != null)
		{
			TutorialPopup.instance.ShowNextTutorial();
		}
	}

	public void startPlayAnim(string trangBiName)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(mAnimGhep);
		if (!(gameObject != null))
		{
			return;
		}
		gameObject.transform.parent = ItemParticle.transform;
		gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
		gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		UISprite component = GameObject.Find("spTanChuong").GetComponent<UISprite>();
		if (component != null)
		{
			if (trangBiName.StartsWith("VC_") && (trangBiName.EndsWith("_S") || trangBiName.EndsWith("_A") || trangBiName.EndsWith("_B")))
			{
				component.spriteName = trangBiName.Substring(0, trangBiName.Length - 2);
			}
			else
			{
				component.spriteName = trangBiName;
			}
		}
		gameObject.GetComponent<ParticleSystem>().Play();
		StartCoroutine(destroyAnim(3f, gameObject));
	}

	public IEnumerator destroyAnim(float waitTime, GameObject m_anim)
	{
		yield return new WaitForSeconds(waitTime);
		Object.Destroy(m_anim.gameObject);
		dangGhepManh = false;
	}
}
