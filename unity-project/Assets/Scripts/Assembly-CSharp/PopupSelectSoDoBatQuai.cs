using UnityEngine;

public class PopupSelectSoDoBatQuai : MonoBehaviour
{
	public static PopupSelectSoDoBatQuai instance;

	public UILabel lbThanhLongTitle;

	public UILabel lbBachHoTitle;

	public UILabel lbHuyenVuTitle;

	public UILabel lbChuTuocTitle;

	public UISprite focusThanhLong;

	public UISprite focusBachHo;

	public UISprite focusChuTuoc;

	public UISprite focusHuyenVu;

	public OtherCfg.BatQuaiTranDoType LinhDaoType;

	private void Start()
	{
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
		}
		instance = null;
	}

	public static void Create(OtherCfg.BatQuaiTranDoType typeDefault)
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		Object obj = Object.Instantiate(Resources.Load("Popup/PopupSelectSoDoBatQuaiTran"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		instance = gameObject.GetComponent<PopupSelectSoDoBatQuai>();
		instance.LinhDaoType = typeDefault;
		instance.displayInfo();
	}

	public void displayInfo()
	{
		lbThanhLongTitle.text = ConfigManager.instance.OtherConfig.BatQuaiConfig[OtherCfg.GetSoDoBQTCodeName(OtherCfg.BatQuaiTranDoType.THANH_LONG)].DisplayName;
		lbBachHoTitle.text = ConfigManager.instance.OtherConfig.BatQuaiConfig[OtherCfg.GetSoDoBQTCodeName(OtherCfg.BatQuaiTranDoType.BACH_HO)].DisplayName;
		lbHuyenVuTitle.text = ConfigManager.instance.OtherConfig.BatQuaiConfig[OtherCfg.GetSoDoBQTCodeName(OtherCfg.BatQuaiTranDoType.HUYEN_VU)].DisplayName;
		lbChuTuocTitle.text = ConfigManager.instance.OtherConfig.BatQuaiConfig[OtherCfg.GetSoDoBQTCodeName(OtherCfg.BatQuaiTranDoType.CHU_TUOC)].DisplayName;
		focusBachHo.gameObject.SetActive(false);
		focusChuTuoc.gameObject.SetActive(false);
		focusHuyenVu.gameObject.SetActive(false);
		focusThanhLong.gameObject.SetActive(false);
		switch (LinhDaoType)
		{
		case OtherCfg.BatQuaiTranDoType.THANH_LONG:
			focusThanhLong.gameObject.SetActive(true);
			break;
		case OtherCfg.BatQuaiTranDoType.BACH_HO:
			focusBachHo.gameObject.SetActive(true);
			break;
		case OtherCfg.BatQuaiTranDoType.HUYEN_VU:
			focusHuyenVu.gameObject.SetActive(true);
			break;
		case OtherCfg.BatQuaiTranDoType.CHU_TUOC:
			focusChuTuoc.gameObject.SetActive(true);
			break;
		}
	}

	public void ThanhLongItem_onClick(GameObject go)
	{
		if (GameManager.instance.m_GameClient.UserInfo.DoiHinh.BatQuaiTranType == OtherCfg.BatQuaiTranDoType.THANH_LONG)
		{
			MessagePopup.Create(Localization.instance.Get("ThongBaoDangSuDungLinhDao"));
			return;
		}
		focusBachHo.gameObject.SetActive(false);
		focusChuTuoc.gameObject.SetActive(false);
		focusHuyenVu.gameObject.SetActive(false);
		focusThanhLong.gameObject.SetActive(true);
		PopupSoDoBatQuai.Create(OtherCfg.BatQuaiTranDoType.THANH_LONG, GameManager.instance.m_GameClient.UserInfo);
	}

	public void BachHoItem_onClick(GameObject go)
	{
		if (GameManager.instance.m_GameClient.UserInfo.DoiHinh.BatQuaiTranType == OtherCfg.BatQuaiTranDoType.BACH_HO)
		{
			MessagePopup.Create(Localization.instance.Get("ThongBaoDangSuDungLinhDao"));
			return;
		}
		focusBachHo.gameObject.SetActive(true);
		focusChuTuoc.gameObject.SetActive(false);
		focusHuyenVu.gameObject.SetActive(false);
		focusThanhLong.gameObject.SetActive(false);
		PopupSoDoBatQuai.Create(OtherCfg.BatQuaiTranDoType.BACH_HO, GameManager.instance.m_GameClient.UserInfo);
	}

	public void HuyenVuItem_onClick(GameObject go)
	{
		if (GameManager.instance.m_GameClient.UserInfo.DoiHinh.BatQuaiTranType == OtherCfg.BatQuaiTranDoType.HUYEN_VU)
		{
			MessagePopup.Create(Localization.instance.Get("ThongBaoDangSuDungLinhDao"));
			return;
		}
		focusBachHo.gameObject.SetActive(false);
		focusChuTuoc.gameObject.SetActive(false);
		focusHuyenVu.gameObject.SetActive(true);
		focusThanhLong.gameObject.SetActive(false);
		PopupSoDoBatQuai.Create(OtherCfg.BatQuaiTranDoType.HUYEN_VU, GameManager.instance.m_GameClient.UserInfo);
	}

	public void ChuTuocItem_onClick(GameObject go)
	{
		if (GameManager.instance.m_GameClient.UserInfo.DoiHinh.BatQuaiTranType == OtherCfg.BatQuaiTranDoType.CHU_TUOC)
		{
			MessagePopup.Create(Localization.instance.Get("ThongBaoDangSuDungLinhDao"));
			return;
		}
		focusBachHo.gameObject.SetActive(false);
		focusChuTuoc.gameObject.SetActive(true);
		focusHuyenVu.gameObject.SetActive(false);
		focusThanhLong.gameObject.SetActive(false);
		PopupSoDoBatQuai.Create(OtherCfg.BatQuaiTranDoType.CHU_TUOC, GameManager.instance.m_GameClient.UserInfo);
	}

	public void OnCancelClick()
	{
		DestroyPopup();
	}
}
