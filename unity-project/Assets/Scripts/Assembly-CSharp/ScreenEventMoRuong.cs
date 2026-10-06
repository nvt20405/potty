using UnityEngine;

public class ScreenEventMoRuong : ScreenBase
{
	public string RuongCodeName = string.Empty;

	public UILabel lbRuongHT;

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
	}

	public override void OnActive()
	{
		base.OnActive();
		lbRuongHT.text = string.Format(Localization.instance.Get("SoRuongDaMoLabel"), 0);
		GameManager.instance.m_GameClient.RequestGetDiemMoRuong();
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.TopBaoRuongConfig != null)
		{
			RuongCodeName = GameManager.instance.m_GameClient.UserInfo.ServerInfo.TopBaoRuongConfig.RuongCodeName;
		}
	}

	public void btnXepHang_OnClick(GameObject go)
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenTopMoRuong);
	}

	public void btnDung_OnClick(GameObject go)
	{
		if (!RuongCodeName.StartsWith("VP_HOP"))
		{
			return;
		}
		UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == RuongCodeName);
		if (checBoxCount(vatPhamTieuThuData, 1))
		{
			int num = checkHasKey(vatPhamTieuThuData, 1);
			if (num > 0)
			{
				OpenHopRequest openHopRequest = new OpenHopRequest();
				openHopRequest.HopID = vatPhamTieuThuData.ID;
				openHopRequest.KeyID = num;
				GameManager.instance.m_GameClient.RequestOpenHop(openHopRequest);
			}
		}
	}

	public void btnDung10_OnClick(GameObject go)
	{
		if (!RuongCodeName.StartsWith("VP_HOP"))
		{
			return;
		}
		UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == RuongCodeName);
		if (checBoxCount(vatPhamTieuThuData, 10))
		{
			int num = checkHasKey(vatPhamTieuThuData, 10);
			if (num > 0)
			{
				OpenHopRequest openHopRequest = new OpenHopRequest();
				openHopRequest.HopID = vatPhamTieuThuData.ID;
				openHopRequest.KeyID = num;
				openHopRequest.Count = 10;
				GameManager.instance.m_GameClient.RequestOpenHop(openHopRequest);
			}
		}
	}

	public int checkHasKey(UserInfo.VatPhamTieuThuData vpData, int countKey)
	{
		if (vpData == null)
		{
			return 0;
		}
		string text = vpData.Name + "_KEY";
		if (GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList != null && GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Count > 0)
		{
			for (int i = 0; i < GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Count; i++)
			{
				if (GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList[i].Name == text)
				{
					if (GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList[i].Quantity >= countKey)
					{
						return GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList[i].ID;
					}
					MessagePopup.Create(Localization.instance.Get("KhongDuKeyMess"));
					return 0;
				}
			}
		}
		MessagePopup.Create(Localization.instance.Get("KhongCoKeyMess"));
		return 0;
	}

	public bool checBoxCount(UserInfo.VatPhamTieuThuData vpData, int countBox)
	{
		if (vpData != null && vpData.Quantity >= countBox)
		{
			return true;
		}
		MessagePopup.Create(Localization.instance.Get("KhongDuBoxMess"));
		return false;
	}
}
