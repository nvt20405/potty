using System.Collections.Generic;
using UnityEngine;

public class ScreenDiHoaCungDetail : ScreenBase
{
	public GameObject DiHoaCungPrefab;

	public GameObject ItemRoot;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<DiHoaCungItem> ListDiHoaCungItem = new List<DiHoaCungItem>();

	private Vector3 itemPos;

	private Vector3 itemOffset = new Vector3(0f, -160f, 0f);

	public UILabel lbDaGop;

	public UILabel lbNangCapCan;

	public int tongSoLuongGachDaGop;

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

	public override void OnActive()
	{
		base.OnActive();
		lbDaGop.text = string.Format(Localization.instance.Get("DongGopHienTaiLabel"), GameManager.instance.m_GameClient.UserInfo.Gamer.DiemTieuKNB);
		displayListDiHoaCung();
	}

	public void clearList()
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		ListDiHoaCungItem.Clear();
	}

	public void displayListDiHoaCung()
	{
		clearList();
		itemPos = new Vector3(0f, 230f, 0f);
		int num = -1;
		int num2 = -1;
		UserInfo.ServerData.DiHoaCungCfg diHoaCungConfig = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DiHoaCungConfig;
		if (diHoaCungConfig.ListTang != null)
		{
			diHoaCungConfig.ListTang.Sort((UserInfo.ServerData.DiHoaCungTang x, UserInfo.ServerData.DiHoaCungTang y) => CompareTangDiHoaCung(x, y));
			for (int num3 = 0; num3 < diHoaCungConfig.ListTang.Count; num3++)
			{
				if (tongSoLuongGachDaGop < diHoaCungConfig.ListTang[num3].GiaTri)
				{
					continue;
				}
				num = num3;
				for (int num4 = 0; num4 < diHoaCungConfig.ListTang.Count; num4++)
				{
					if (tongSoLuongGachDaGop < diHoaCungConfig.ListTang[num4].GiaTri)
					{
						num2 = num4;
					}
				}
			}
		}
		Debug.Log("current index: " + num + " - nextIndex: " + num2);
		if (num2 >= 0)
		{
			int giaTri = diHoaCungConfig.ListTang[num2].GiaTri;
			lbNangCapCan.text = string.Format(Localization.instance.Get("NapCapCanMess"), giaTri - tongSoLuongGachDaGop);
		}
		else
		{
			lbNangCapCan.text = string.Empty;
			if (num == diHoaCungConfig.ListTang.Count - 1)
			{
				lbNangCapCan.text = Localization.instance.Get("HoanThanhXayDiHoaCungMess");
			}
			if (num < 0 && num2 < 0)
			{
				int giaTri2 = diHoaCungConfig.ListTang[diHoaCungConfig.ListTang.Count - 1].GiaTri;
				lbNangCapCan.text = string.Format(Localization.instance.Get("NapCapCanMess"), giaTri2 - tongSoLuongGachDaGop);
			}
		}
		if (diHoaCungConfig.ListTang != null && diHoaCungConfig.ListTang.Count > 0)
		{
			for (int num5 = 0; num5 < diHoaCungConfig.ListTang.Count + 1; num5++)
			{
				DiHoaCungItem component = ((GameObject)Object.Instantiate(DiHoaCungPrefab)).GetComponent<DiHoaCungItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPos;
				if (num5 == 0)
				{
					component.setNocDiHoaCung();
				}
				else if (num5 == diHoaCungConfig.ListTang.Count)
				{
					component.setData(diHoaCungConfig.ListTang[num5 - 1], tongSoLuongGachDaGop, num5 - 1, true);
				}
				else
				{
					component.setData(diHoaCungConfig.ListTang[num5 - 1], tongSoLuongGachDaGop, num5 - 1);
				}
				itemPos += itemOffset;
				ListDiHoaCungItem.Add(component);
			}
		}
		UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
		component2.ResetPosition();
	}

	public int CompareTangDiHoaCung(UserInfo.ServerData.DiHoaCungTang tangData1, UserInfo.ServerData.DiHoaCungTang tangData2)
	{
		if (tangData1.GiaTri > tangData2.GiaTri)
		{
			return -1;
		}
		if (tangData1.GiaTri < tangData2.GiaTri)
		{
			return 1;
		}
		return 0;
	}

	public void onClick_btnBack()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenKyNgoDiHoaCung);
	}

	public void onClick_btnXepHang()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenTopDiHoaCung);
	}

	public void updateInfo(PhanThuongResponse response)
	{
		if (response != null)
		{
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongNhanDuoc"), response);
			displayListDiHoaCung();
		}
	}
}
