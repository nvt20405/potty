using System.Collections.Generic;
using UnityEngine;

public class ScreenCuongHoa : ScreenBase
{
	private const int maxItemCount = 20;

	public OtherAvatar trangBiAvatar;

	public UILabel lbTrangBiName;

	public UILabel lbSoBacCan;

	public UILabel lbPercentValue;

	public UISlider progressBar;

	public UserInfo.TrangBiData m_TrangBiData;

	public GameObject groupCuongHoaSuccess;

	public UILabel lbLevelOld;

	public UILabel lbLevelNew;

	public UILabel lbChiSoOld;

	public UILabel lbChiSoNew;

	public UISprite iconChiSoOld;

	public UISprite iconChiSoNew;

	public UILabel lbCuongHoaFail;

	public GameObject ItemRoot;

	public GameObject TrangBiPerfab;

	private List<TrangBiCuongHoaItem> ItemList = new List<TrangBiCuongHoaItem>();

	private List<UserInfo.HonNhanVatData> ListData = new List<UserInfo.HonNhanVatData>();

	private int startItemGUI_Idx;

	private int colCount = 5;

	private int rowCount;

	private float hSpace = 10f;

	private int itemCount;

	public bool isScroll;

	private Vector3 itemPos;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	public GameObject m_AnimTinhLuyen;

	private CuongHoaTrangBiRequest request = new CuongHoaTrangBiRequest();

	private float currentPoint;

	private float tyLeThanhCong;

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

	public void Set(UserInfo.TrangBiData data)
	{
		if (data != null)
		{
			m_TrangBiData = data;
		}
		request.TrangBiID = m_TrangBiData.ID;
	}

	public override void OnActive()
	{
		dragPanel = ItemRoot.GetComponent<UIDraggablePanel>();
		panel = ItemRoot.GetComponent<UIPanel>();
		m_AnimTinhLuyen.SetActive(false);
		if (m_TrangBiData != null)
		{
			displayInfo();
		}
		if (request.HonIDList != null)
		{
			request.HonIDList.Clear();
		}
		else
		{
			request.HonIDList = new List<int>();
		}
		if (request.CountList != null)
		{
			request.CountList.Clear();
		}
		else
		{
			request.CountList = new List<int>();
		}
		groupCuongHoaSuccess.gameObject.SetActive(false);
		lbCuongHoaFail.gameObject.SetActive(false);
	}

	public void displayInfo()
	{
		trangBiAvatar.Set(m_TrangBiData);
		TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[m_TrangBiData.Name];
		lbTrangBiName.text = trangBiCfg.TenHienThi;
		currentPoint = ConfigManager.instance.GetTileCuongHoa(m_TrangBiData.Level, GameManager.instance.m_GameClient.UserInfo.Gamer.Level);
		tyLeThanhCong = currentPoint;
		lbSoBacCan.text = ConfigManager.instance.GetGiaCuongHoaTrangBi(m_TrangBiData).ToString();
		progressBar.sliderValue = currentPoint / 100f;
		lbPercentValue.text = currentPoint + "%";
		clearList();
		if (currentPoint < 100f)
		{
			getListTanHon();
		}
	}

	private void clearList()
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
		ListData.Clear();
	}

	public void getListTanHon()
	{
		startItemGUI_Idx = 0;
		int num = 0;
		GameManager.instance.m_GameClient.UserInfo.HonNhanVatList.Sort((UserInfo.HonNhanVatData honNhanVatData2, UserInfo.HonNhanVatData y) => ConfigManager.instance.CompareTanHon(y.Name, y.Quantity, honNhanVatData2.Name, honNhanVatData2.Quantity));
		for (int num2 = 0; num2 < GameManager.instance.m_GameClient.UserInfo.HonNhanVatList.Count; num2++)
		{
			if (GameManager.instance.m_GameClient.UserInfo.HonNhanVatList[num2].Quantity > 0)
			{
				ListData.Add(GameManager.instance.m_GameClient.UserInfo.HonNhanVatList[num2]);
			}
		}
		if (ListData.Count > 0)
		{
			for (int num3 = startItemGUI_Idx; num3 < ListData.Count; num3++)
			{
				UserInfo.HonNhanVatData honNhanVatData = ListData[num3];
				TrangBiCuongHoaItem component = ((GameObject)Object.Instantiate(TrangBiPerfab)).GetComponent<TrangBiCuongHoaItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
				float x = (float)(num3 % colCount) * (component.tanHonAvatar.bkg.transform.localScale.x * 8f / 10f + hSpace) - panel.clipRange.z / 2f + 65f;
				float num4 = Mathf.Floor(num3 / colCount) * (component.tanHonAvatar.bkg.transform.localScale.x * 8f / 10f + hSpace) + panel.clipRange.w / 2f;
				component.transform.localPosition = new Vector3(x, 0f - num4, 0f);
				component.setData(honNhanVatData, honNhanVatData.Quantity);
				UIEventListener.Get(component.gameObject).onClick = manhTH_OnClick;
				ItemList.Add(component);
				num++;
				if (num >= 20)
				{
					break;
				}
			}
		}
		if (20 % colCount == 0)
		{
			rowCount = 20 / colCount;
		}
		else
		{
			rowCount = 20 / colCount + 1;
		}
		UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
		component2.ResetPosition();
	}

	public void manhTH_OnClick(GameObject go)
	{
		if (tyLeThanhCong >= 100f)
		{
			MessagePopup.Create(Localization.instance.Get("ThongBaoTyLeThanhCongDaDuMess"));
			return;
		}
		TrangBiCuongHoaItem component = go.transform.GetComponent<TrangBiCuongHoaItem>();
		if (!(component != null) || component.m_honNhanVatData == null)
		{
			return;
		}
		component.tanHonAvatar.currentQuantity--;
		if (component.tanHonAvatar.currentQuantity < 0)
		{
			component.tanHonAvatar.currentQuantity = 0;
			return;
		}
		int num = -1;
		if (request.HonIDList != null)
		{
			if (request.HonIDList.Count > 0)
			{
				for (int i = 0; i < request.HonIDList.Count; i++)
				{
					if (request.HonIDList[i] == component.m_honNhanVatData.ID)
					{
						num = i;
					}
				}
				if (num < 0)
				{
					request.HonIDList.Add(component.m_honNhanVatData.ID);
				}
			}
			else
			{
				request.HonIDList.Add(component.m_honNhanVatData.ID);
			}
		}
		if (request.CountList != null)
		{
			if (num >= 0 && num < request.CountList.Count)
			{
				List<int> countList;
				List<int> list = (countList = request.CountList);
				int index2;
				int index = (index2 = num);
				index2 = countList[index2];
				list[index] = index2 + 1;
			}
			else
			{
				request.CountList.Add(1);
			}
		}
		bool NotEnough;
		tyLeThanhCong = currentPoint + (float)ConfigManager.instance.GetTileCuongHoaTangCuong(request, GameManager.instance.m_GameClient.UserInfo, out NotEnough);
		if (tyLeThanhCong > 100f)
		{
			tyLeThanhCong = 100f;
		}
		progressBar.sliderValue = tyLeThanhCong / 100f;
		lbPercentValue.text = tyLeThanhCong + "/" + 100;
	}

	private void Update()
	{
		if (ItemList.Count > 0)
		{
			Vector4 clipRange = panel.clipRange;
			TrangBiCuongHoaItem trangBiCuongHoaItem = ItemList[ItemList.Count - 1];
			float y = trangBiCuongHoaItem.transform.localPosition.y;
			TrangBiCuongHoaItem trangBiCuongHoaItem2 = ItemList[0];
			float y2 = trangBiCuongHoaItem2.transform.localPosition.y;
			if (y - clipRange.y > -70f && ListData.Count > 20)
			{
				isScroll = true;
				SwapDragListDown();
			}
			else if (y2 - clipRange.y < 70f && ListData.Count > 20)
			{
				isScroll = true;
				SwapDragListUp();
			}
		}
	}

	public void SwapDragListDown()
	{
		if ((!isScroll && rowCount >= ListData.Count / colCount) || ItemList.Count - colCount < 0)
		{
			return;
		}
		float y = ItemList[ItemList.Count - colCount].transform.localPosition.y;
		for (int i = 0; i < colCount; i++)
		{
			TrangBiCuongHoaItem trangBiCuongHoaItem = ItemList[0];
			TrangBiCuongHoaItem trangBiCuongHoaItem2 = ItemList[ItemList.Count - colCount];
			ItemList.RemoveAt(0);
			ItemList.Add(trangBiCuongHoaItem);
			float y2 = y - 100f;
			trangBiCuongHoaItem.transform.localPosition = new Vector3(trangBiCuongHoaItem2.transform.localPosition.x, y2, trangBiCuongHoaItem2.transform.localPosition.z);
			int num = startItemGUI_Idx + 20;
			if (num >= ListData.Count)
			{
				trangBiCuongHoaItem.gameObject.SetActive(false);
			}
			else
			{
				trangBiCuongHoaItem.setData(ListData[num], getRemainCount(ListData[num]));
			}
			startItemGUI_Idx++;
		}
		rowCount++;
		isScroll = false;
	}

	public int getRemainCount(UserInfo.HonNhanVatData honNVData)
	{
		if (honNVData != null)
		{
			int result = honNVData.Quantity;
			if (request.HonIDList != null && request.CountList != null && request.HonIDList.Count == request.CountList.Count)
			{
				for (int i = 0; i < request.HonIDList.Count; i++)
				{
					if (honNVData.ID == request.HonIDList[i])
					{
						result = honNVData.Quantity - request.CountList[i];
					}
				}
			}
			return result;
		}
		return 0;
	}

	public void SwapDragListUp()
	{
		if (!isScroll || rowCount == 0)
		{
			return;
		}
		float y = ItemList[0].transform.localPosition.y;
		float z = ItemList[0].transform.localPosition.z;
		for (int i = 0; i < colCount; i++)
		{
			if (startItemGUI_Idx <= 0)
			{
				return;
			}
			startItemGUI_Idx--;
			int num = startItemGUI_Idx;
			if (num < 0 || num >= ListData.Count)
			{
				return;
			}
			TrangBiCuongHoaItem trangBiCuongHoaItem = ItemList[ItemList.Count - 1];
			ItemList.RemoveAt(ItemList.Count - 1);
			ItemList.Insert(0, trangBiCuongHoaItem);
			trangBiCuongHoaItem.transform.localPosition = new Vector3(trangBiCuongHoaItem.transform.localPosition.x, y + 100f, z);
			trangBiCuongHoaItem.setData(ListData[num], getRemainCount(ListData[num]));
			trangBiCuongHoaItem.gameObject.SetActive(true);
		}
		isScroll = false;
		rowCount--;
	}

	public void btnBack_OnClick()
	{
		GUIManager.instance.SetScreen(GUIManager.instance.LastScreen);
	}

	public void startPlayAnim()
	{
		m_AnimTinhLuyen.SetActive(true);
		m_AnimTinhLuyen.GetComponent<ParticleSystem>().Simulate(0f, true, true);
		m_AnimTinhLuyen.GetComponent<ParticleSystem>().Play();
	}

	public void btnHelp_OnClick()
	{
		ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
		screenHelpInfo.setByLevel(2, 2);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
	}

	public void btnTinhLuyen_OnClick()
	{
		if (m_TrangBiData != null && request != null && request.HonIDList != null)
		{
			GameClient gameClient = GameManager.instance.m_GameClient;
			gameClient.RequestCuongHoaTrangBi(request);
		}
	}

	public void updateInfo(int trangBiID, int thayDoiLevel)
	{
		startPlayAnim();
		if (request.HonIDList != null)
		{
			request.HonIDList.Clear();
		}
		else
		{
			request.HonIDList = new List<int>();
		}
		if (request.CountList != null)
		{
			request.CountList.Clear();
		}
		else
		{
			request.CountList = new List<int>();
		}
		UserInfo.TrangBiData trangBiData = GameManager.instance.m_GameClient.UserInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.ID == trangBiID);
		if (trangBiData == null)
		{
			return;
		}
		if (thayDoiLevel > 0)
		{
			groupCuongHoaSuccess.SetActive(true);
			lbCuongHoaFail.gameObject.SetActive(false);
			TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[m_TrangBiData.Name];
			ChiSoCoBan chiSoCoBan = ChiSoCoBan.None;
			switch (TrangBiCfg.GetLoaiTrangBi(m_TrangBiData.Name))
			{
			case LoaiTrangBi.Mu:
				iconChiSoOld.spriteName = "icon_noi_goc";
				iconChiSoNew.spriteName = "icon_noi_goc";
				chiSoCoBan = ChiSoCoBan.Noi;
				break;
			case LoaiTrangBi.VuKhi:
				iconChiSoOld.spriteName = "icon_ngoai";
				iconChiSoNew.spriteName = "icon_ngoai";
				chiSoCoBan = ChiSoCoBan.Ngoai;
				break;
			case LoaiTrangBi.AoGiap:
				iconChiSoOld.spriteName = "icon_than";
				iconChiSoNew.spriteName = "icon_than";
				chiSoCoBan = ChiSoCoBan.ThanPhap;
				break;
			case LoaiTrangBi.TrangSuc:
				iconChiSoOld.spriteName = "icon_menh";
				iconChiSoNew.spriteName = "icon_menh";
				chiSoCoBan = ChiSoCoBan.Menh;
				break;
			}
			lbLevelOld.text = m_TrangBiData.Level.ToString();
			List<int> chiSo = trangBiCfg.GetChiSo(m_TrangBiData);
			switch (chiSoCoBan)
			{
			case ChiSoCoBan.Menh:
				lbChiSoOld.text = chiSo[0].ToString();
				break;
			case ChiSoCoBan.Ngoai:
				lbChiSoOld.text = chiSo[1].ToString();
				break;
			case ChiSoCoBan.ThanPhap:
				lbChiSoOld.text = chiSo[2].ToString();
				break;
			case ChiSoCoBan.Noi:
				lbChiSoOld.text = chiSo[3].ToString();
				break;
			case ChiSoCoBan.None:
				lbChiSoOld.text = "0";
				break;
			}
			m_TrangBiData = trangBiData;
			List<int> chiSo2 = trangBiCfg.GetChiSo(m_TrangBiData);
			switch (chiSoCoBan)
			{
			case ChiSoCoBan.Menh:
				lbChiSoNew.text = chiSo2[0].ToString();
				break;
			case ChiSoCoBan.Ngoai:
				lbChiSoNew.text = chiSo2[1].ToString();
				break;
			case ChiSoCoBan.ThanPhap:
				lbChiSoNew.text = chiSo2[2].ToString();
				break;
			case ChiSoCoBan.Noi:
				lbChiSoNew.text = chiSo2[3].ToString();
				break;
			case ChiSoCoBan.None:
				lbChiSoNew.text = "0";
				break;
			}
		}
		else
		{
			groupCuongHoaSuccess.SetActive(false);
			lbCuongHoaFail.gameObject.SetActive(true);
		}
		displayInfo();
	}
}
