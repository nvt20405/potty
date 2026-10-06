using UnityEngine;

public class PopupNangCapThienMaLenhResult : MonoBehaviour
{
	public OtherAvatar oldVoCong;

	public OtherAvatar newVoCong;

	public UILabel lbInfoOld;

	public UILabel lbInfoNew;

	public GameObject btnNhan;

	public GameObject btnCuongHoa;

	public UILabel lbTitle;

	public UILabel lbButtonCancel;

	public UILabel lbCurLevel;

	public UILabel lbNextLevel;

	public UILabel lbInfoCuongHoa;

	private ThienMaAction ThienMaType;

	public GameObject grpVoCongMax;

	public GameObject grpVoCongNormal;

	public UILabel lbInfoMax;

	public OtherAvatar avaMax;

	public static PopupNangCapThienMaLenhResult instance;

	public UserInfo.ThienMaLenhInfo resultData;

	public int currentSlot = -1;

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public static void Create(UserInfo.ThienMaLenhInfo currentLvlData, UserInfo.ThienMaLenhInfo nextLevelData, int curSlot, ThienMaAction type)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("popup/PopupNangCapThienMaLenhResult"))).GetComponent<PopupNangCapThienMaLenhResult>();
		instance.ThienMaType = type;
		instance.btnCuongHoa.SetActive(false);
		instance.btnNhan.SetActive(false);
		instance.resultData = currentLvlData;
		instance.currentSlot = curSlot;
		instance.grpVoCongMax.gameObject.SetActive(false);
		instance.grpVoCongNormal.gameObject.SetActive(true);
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.displayInfoResult(currentLvlData, nextLevelData, curSlot);
	}

	public void displayInfoResult(UserInfo.ThienMaLenhInfo curLvlData, UserInfo.ThienMaLenhInfo nextLvlData, int curSlot)
	{
		int num = 0;
		switch (curSlot)
		{
		case 1:
			displayInfo(curLvlData.Slot1, nextLvlData.Slot1, curLvlData.Value1, nextLvlData.Value1);
			num = curLvlData.TichLuy1;
			break;
		case 2:
			displayInfo(curLvlData.Slot2, nextLvlData.Slot2, curLvlData.Value2, nextLvlData.Value2);
			num = curLvlData.TichLuy2;
			break;
		case 3:
			displayInfo(curLvlData.Slot3, nextLvlData.Slot3, curLvlData.Value3, nextLvlData.Value3);
			num = curLvlData.TichLuy3;
			break;
		}
		if (ThienMaType == ThienMaAction.KHAM)
		{
			btnNhan.gameObject.SetActive(true);
			lbTitle.text = Localization.instance.Get("CheTacResultTitle");
			lbButtonCancel.text = Localization.instance.Get("HuyLabelBtn");
			UILabel uILabel = lbCurLevel;
			string empty = string.Empty;
			lbInfoCuongHoa.text = empty;
			empty = empty;
			lbNextLevel.text = empty;
			uILabel.text = empty;
		}
		else if (ThienMaType == ThienMaAction.CUONG_HOA)
		{
			btnCuongHoa.gameObject.SetActive(true);
			lbTitle.text = Localization.instance.Get("CuongHoaTitlePopup");
			lbButtonCancel.text = Localization.instance.Get("QuayLaiBtn");
			lbCurLevel.text = Localization.instance.Get("CapHienTaiLabel");
			lbNextLevel.text = Localization.instance.Get("CapKeTiepLabel");
			UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_HUYET_NGOC");
			int num2 = 1;
			if (ConfigManager.instance.OtherConfig.ThienMaExpReq != null)
			{
				if (ConfigManager.instance.OtherConfig.ThienMaExpReq.Count >= num + 1)
				{
					num2 = ConfigManager.instance.OtherConfig.ThienMaExpReq[num];
					if (vatPhamTieuThuData != null)
					{
						lbInfoCuongHoa.text = string.Format(Localization.instance.Get("CuongHoaTMLInfoLabel"), vatPhamTieuThuData.Quantity, string.Format(Localization.instance.Get("CuongHoaThienMaDescription"), num2));
					}
					else
					{
						lbInfoCuongHoa.text = string.Format(Localization.instance.Get("CuongHoaTMLInfoLabel"), 0, string.Format(Localization.instance.Get("CuongHoaThienMaDescription"), num2));
					}
				}
				else
				{
					lbInfoCuongHoa.text = Localization.instance.Get("ThienMaMaxLevel");
					grpVoCongMax.gameObject.SetActive(true);
					grpVoCongNormal.gameObject.SetActive(false);
				}
			}
			else
			{
				lbInfoCuongHoa.text = string.Empty;
			}
		}
		resultData = curLvlData;
	}

	private void displayInfo(string oldVC, string newVC, float oldValue, float newValue)
	{
		oldVoCong.SetBaoKhiAvatar(oldVC);
		newVoCong.SetBaoKhiAvatar(newVC);
		CfgVoCong cfgVoCong = ConfigManager.instance.m_dicVCs[oldVC];
		lbInfoOld.text = string.Format(Localization.instance.Get("BaoKhiInfo2"), cfgVoCong.TenHienThi, oldValue);
		CfgVoCong cfgVoCong2 = ConfigManager.instance.m_dicVCs[newVC];
		lbInfoNew.text = string.Format(Localization.instance.Get("BaoKhiInfo2"), cfgVoCong2.TenHienThi, newValue);
		avaMax.SetBaoKhiAvatar(oldVC);
		lbInfoMax.text = string.Format(Localization.instance.Get("BaoKhiInfo2"), cfgVoCong.TenHienThi, oldValue);
	}

	public void btnNhan_OnClick(GameObject go)
	{
		GameManager.instance.m_GameClient.RequestThienMaQuaySlotConfirm(true);
		DestroyPopup();
	}

	public void btnCuongHoa_OnClick(GameObject go)
	{
		UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_HUYET_NGOC");
		if (vatPhamTieuThuData != null && vatPhamTieuThuData.Quantity > 0)
		{
			SetThienMaLenhRequest setThienMaLenhRequest = new SetThienMaLenhRequest();
			setThienMaLenhRequest.ID = resultData.ID;
			setThienMaLenhRequest.VatPhamID = vatPhamTieuThuData.ID;
			setThienMaLenhRequest.SlotID = currentSlot;
			GameManager.instance.m_GameClient.RequestThienMaUpgradeSlot(setThienMaLenhRequest);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("ThongBaoKhongDuHuyetNgoc"));
		}
	}

	public void btnHuy_OnClick(GameObject go)
	{
		if (ThienMaType == ThienMaAction.KHAM)
		{
			GameManager.instance.m_GameClient.RequestThienMaQuaySlotConfirm(false);
		}
		DestroyPopup();
	}
}
