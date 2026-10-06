using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSoDoBatQuai : MonoBehaviour
{
	public static PopupSoDoBatQuai instance;

	public Func<OtherCfg.BatQuaiTranDoType, bool> OnFinish;

	public UILabel lbTitle;

	public GameObject ThanhLongGrp;

	public GameObject BachHoGrp;

	public GameObject HuyenVuGrp;

	public GameObject ChuTuocGrp;

	public CuongHoaBatQuaiItem[] listItemThanhLongMap;

	public CuongHoaBatQuaiItem[] listItemBachHoMap;

	public CuongHoaBatQuaiItem[] listItemChuTuocMap;

	public CuongHoaBatQuaiItem[] listItemHuyenVuMap;

	private UserInfo.DoiHinhData doiHinhData;

	public GameObject ChonLinhDaoGrp;

	public GameObject btnDoiLinhDao;

	public GameObject AnotherViewButtonGroup;

	private bool isOpenByAnotherUser;

	private int currentSlot;

	private CuongHoaBatQuaiItem currentItemSelected;

	private OtherCfg.BatQuaiTranDoType curSoDoBatQuai;

	private void Start()
	{
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			UnityEngine.Object.Destroy(instance.gameObject);
		}
		instance = null;
	}

	public static void Create(OtherCfg.BatQuaiTranDoType curMap, UserInfo userInfo)
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("popup/PopupBatQuaiTran"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		instance = gameObject.GetComponent<PopupSoDoBatQuai>();
		instance.ThanhLongGrp.gameObject.SetActive(false);
		instance.BachHoGrp.gameObject.SetActive(false);
		instance.HuyenVuGrp.gameObject.SetActive(false);
		instance.ChuTuocGrp.gameObject.SetActive(false);
		instance.curSoDoBatQuai = curMap;
		if (userInfo.DoiHinh != null)
		{
			instance.doiHinhData = userInfo.DoiHinh;
		}
		instance.displayInfo();
	}

	public static void CreateToAnotherView(OtherCfg.BatQuaiTranDoType batQuaiTranDoType, UserInfo userInfo)
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("popup/PopupBatQuaiTran"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		instance = gameObject.GetComponent<PopupSoDoBatQuai>();
		instance.ThanhLongGrp.gameObject.SetActive(false);
		instance.BachHoGrp.gameObject.SetActive(false);
		instance.HuyenVuGrp.gameObject.SetActive(false);
		instance.ChuTuocGrp.gameObject.SetActive(false);
		instance.curSoDoBatQuai = batQuaiTranDoType;
		if (userInfo.DoiHinh != null)
		{
			instance.doiHinhData = userInfo.DoiHinh;
		}
		instance.isOpenByAnotherUser = true;
		instance.displayInfo();
	}

	public void updateView(SetSoDoBatQuaiTranResponse response)
	{
		curSoDoBatQuai = response.newSoDoType;
		UserInfo.DoiHinhData doiHinh = GameManager.instance.m_GameClient.UserInfo.DoiHinh;
		if (doiHinh == null)
		{
			return;
		}
		doiHinhData = doiHinh;
		CuongHoaBatQuaiItem[] listItem = getListItem();
		if (listItem != null)
		{
			if (doiHinhData.CuongHoa_Slot1 > 1)
			{
				listItem[0].startCuongHoa();
			}
			if (doiHinhData.CuongHoa_Slot2 > 1)
			{
				listItem[1].startCuongHoa();
			}
			if (doiHinhData.CuongHoa_Slot3 > 1)
			{
				listItem[2].startCuongHoa();
			}
			if (doiHinhData.CuongHoa_Slot4 > 1)
			{
				listItem[3].startCuongHoa();
			}
			if (doiHinhData.CuongHoa_Slot5 > 1)
			{
				listItem[4].startCuongHoa();
			}
			if (doiHinhData.CuongHoa_Slot6 > 1)
			{
				listItem[5].startCuongHoa();
			}
			if (doiHinhData.CuongHoa_Slot7 > 1)
			{
				listItem[6].startCuongHoa();
			}
			if (doiHinhData.CuongHoa_Slot8 > 1)
			{
				listItem[7].startCuongHoa();
			}
		}
		StartCoroutine(updateInfoAfterChangeMap(1f));
	}

	private CuongHoaBatQuaiItem[] getListItem()
	{
		switch (curSoDoBatQuai)
		{
		case OtherCfg.BatQuaiTranDoType.THANH_LONG:
			return listItemThanhLongMap;
		case OtherCfg.BatQuaiTranDoType.BACH_HO:
			return listItemBachHoMap;
		case OtherCfg.BatQuaiTranDoType.HUYEN_VU:
			return listItemHuyenVuMap;
		case OtherCfg.BatQuaiTranDoType.CHU_TUOC:
			return listItemChuTuocMap;
		default:
			return null;
		}
	}

	public IEnumerator updateInfoAfterChangeMap(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		displayInfo();
	}

	public void displayInfo()
	{
		string empty = string.Empty;
		string soDoBQTCodeName = OtherCfg.GetSoDoBQTCodeName(curSoDoBatQuai);
		lbTitle.text = ConfigManager.instance.OtherConfig.BatQuaiConfig[soDoBQTCodeName].DisplayName;
		switch (curSoDoBatQuai)
		{
		case OtherCfg.BatQuaiTranDoType.THANH_LONG:
			if (!NGUITools.GetActive(ThanhLongGrp.gameObject))
			{
				ThanhLongGrp.gameObject.SetActive(true);
			}
			OnLoadThanhLongMap();
			break;
		case OtherCfg.BatQuaiTranDoType.BACH_HO:
			if (!NGUITools.GetActive(BachHoGrp.gameObject))
			{
				BachHoGrp.gameObject.SetActive(true);
			}
			OnLoadBachHoMap();
			break;
		case OtherCfg.BatQuaiTranDoType.HUYEN_VU:
			if (!NGUITools.GetActive(HuyenVuGrp.gameObject))
			{
				HuyenVuGrp.gameObject.SetActive(true);
			}
			OnLoadHuyenVuMap();
			break;
		case OtherCfg.BatQuaiTranDoType.CHU_TUOC:
			if (!NGUITools.GetActive(ChuTuocGrp.gameObject))
			{
				ChuTuocGrp.gameObject.SetActive(true);
			}
			OnLoadChuTuocMap();
			break;
		}
		if (!isOpenByAnotherUser)
		{
			if (curSoDoBatQuai != doiHinhData.BatQuaiTranType)
			{
				ChonLinhDaoGrp.gameObject.SetActive(true);
				btnDoiLinhDao.gameObject.SetActive(false);
			}
			else
			{
				ChonLinhDaoGrp.gameObject.SetActive(false);
				btnDoiLinhDao.gameObject.SetActive(true);
			}
			AnotherViewButtonGroup.gameObject.SetActive(false);
		}
		else
		{
			ChonLinhDaoGrp.gameObject.SetActive(false);
			btnDoiLinhDao.gameObject.SetActive(false);
			AnotherViewButtonGroup.gameObject.SetActive(true);
		}
	}

	public void OnLoadThanhLongMap()
	{
		string soDoBQTCodeName = OtherCfg.GetSoDoBQTCodeName(curSoDoBatQuai);
		List<string> listSlot = ConfigManager.instance.OtherConfig.BatQuaiConfig[soDoBQTCodeName].ListSlot;
		if (doiHinhData.BatQuaiTranType != OtherCfg.BatQuaiTranDoType.NONE)
		{
			if (listSlot != null)
			{
				if (listSlot[0] != null && listItemThanhLongMap[0] != null)
				{
					listItemThanhLongMap[0].SetData(GetSpriteNameThuocTinh(listSlot[0]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot1 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(1, doiHinhData) + "%");
				}
				if (listSlot[1] != null && listItemThanhLongMap[1] != null)
				{
					listItemThanhLongMap[1].SetData(GetSpriteNameThuocTinh(listSlot[1]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot2 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(2, doiHinhData) + "%");
				}
				if (listSlot[2] != null && listItemThanhLongMap[2] != null)
				{
					listItemThanhLongMap[2].SetData(GetSpriteNameThuocTinh(listSlot[2]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot3 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(3, doiHinhData) + "%");
				}
				if (listSlot[3] != null && listItemThanhLongMap[3] != null)
				{
					listItemThanhLongMap[3].SetData(GetSpriteNameThuocTinh(listSlot[3]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot4 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(4, doiHinhData) + "%");
				}
				if (listSlot[4] != null && listItemThanhLongMap[4] != null)
				{
					listItemThanhLongMap[4].SetData(GetSpriteNameThuocTinh(listSlot[4]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot5 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(5, doiHinhData) + "%");
				}
				if (listSlot[5] != null && listItemThanhLongMap[5] != null)
				{
					listItemThanhLongMap[5].SetData(GetSpriteNameThuocTinh(listSlot[5]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot6 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(6, doiHinhData) + "%");
				}
				if (listSlot[6] != null && listItemThanhLongMap[6] != null)
				{
					listItemThanhLongMap[6].SetData(GetSpriteNameThuocTinh(listSlot[6]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot7 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(7, doiHinhData) + "%");
				}
				if (listSlot[7] != null && listItemThanhLongMap[7] != null)
				{
					listItemThanhLongMap[7].SetData(GetSpriteNameThuocTinh(listSlot[7]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot8 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(8, doiHinhData) + "%");
				}
			}
			displayItemStatus(doiHinhData.CuongHoa_Slot1, 0, 0, listItemThanhLongMap[0]);
			displayItemStatus(doiHinhData.CuongHoa_Slot2, doiHinhData.CuongHoa_Slot1, 1, listItemThanhLongMap[1]);
			displayItemStatus(doiHinhData.CuongHoa_Slot3, doiHinhData.CuongHoa_Slot2, 2, listItemThanhLongMap[2]);
			displayItemStatus(doiHinhData.CuongHoa_Slot4, doiHinhData.CuongHoa_Slot3, 3, listItemThanhLongMap[3]);
			displayItemStatus(doiHinhData.CuongHoa_Slot5, doiHinhData.CuongHoa_Slot4, 4, listItemThanhLongMap[4]);
			displayItemStatus(doiHinhData.CuongHoa_Slot6, doiHinhData.CuongHoa_Slot5, 5, listItemThanhLongMap[5]);
			displayItemStatus(doiHinhData.CuongHoa_Slot7, doiHinhData.CuongHoa_Slot6, 6, listItemThanhLongMap[6]);
			displayItemStatus(doiHinhData.CuongHoa_Slot8, doiHinhData.CuongHoa_Slot7, 7, listItemThanhLongMap[7]);
		}
		else if (doiHinhData != null && listSlot != null)
		{
			if (listSlot[0] != null && listItemThanhLongMap[0] != null)
			{
				listItemThanhLongMap[0].SetData(GetSpriteNameThuocTinh(listSlot[0]), string.Empty);
			}
			if (listSlot[1] != null && listItemThanhLongMap[1] != null)
			{
				listItemThanhLongMap[1].SetData(GetSpriteNameThuocTinh(listSlot[1]), string.Empty);
			}
			if (listSlot[2] != null && listItemThanhLongMap[2] != null)
			{
				listItemThanhLongMap[2].SetData(GetSpriteNameThuocTinh(listSlot[2]), string.Empty);
			}
			if (listSlot[3] != null && listItemThanhLongMap[3] != null)
			{
				listItemThanhLongMap[3].SetData(GetSpriteNameThuocTinh(listSlot[3]), string.Empty);
			}
			if (listSlot[4] != null && listItemThanhLongMap[4] != null)
			{
				listItemThanhLongMap[4].SetData(GetSpriteNameThuocTinh(listSlot[4]), string.Empty);
			}
			if (listSlot[5] != null && listItemThanhLongMap[5] != null)
			{
				listItemThanhLongMap[5].SetData(GetSpriteNameThuocTinh(listSlot[5]), string.Empty);
			}
			if (listSlot[6] != null && listItemThanhLongMap[6] != null)
			{
				listItemThanhLongMap[6].SetData(GetSpriteNameThuocTinh(listSlot[6]), string.Empty);
			}
			if (listSlot[7] != null && listItemThanhLongMap[7] != null)
			{
				listItemThanhLongMap[7].SetData(GetSpriteNameThuocTinh(listSlot[7]), string.Empty);
			}
		}
	}

	public void OnLoadBachHoMap()
	{
		string soDoBQTCodeName = OtherCfg.GetSoDoBQTCodeName(curSoDoBatQuai);
		List<string> listSlot = ConfigManager.instance.OtherConfig.BatQuaiConfig[soDoBQTCodeName].ListSlot;
		if (doiHinhData.BatQuaiTranType != OtherCfg.BatQuaiTranDoType.NONE)
		{
			if (listSlot != null)
			{
				if (listSlot[0] != null && listItemBachHoMap[0] != null)
				{
					listItemBachHoMap[0].SetData(GetSpriteNameThuocTinh(listSlot[0]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot1 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(1, doiHinhData) + "%");
				}
				if (listSlot[1] != null && listItemBachHoMap[1] != null)
				{
					listItemBachHoMap[1].SetData(GetSpriteNameThuocTinh(listSlot[1]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot2 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(2, doiHinhData) + "%");
				}
				if (listSlot[2] != null && listItemBachHoMap[2] != null)
				{
					listItemBachHoMap[2].SetData(GetSpriteNameThuocTinh(listSlot[2]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot3 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(3, doiHinhData) + "%");
				}
				if (listSlot[3] != null && listItemBachHoMap[3] != null)
				{
					listItemBachHoMap[3].SetData(GetSpriteNameThuocTinh(listSlot[3]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot4 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(4, doiHinhData) + "%");
				}
				if (listSlot[4] != null && listItemBachHoMap[4] != null)
				{
					listItemBachHoMap[4].SetData(GetSpriteNameThuocTinh(listSlot[4]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot5 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(5, doiHinhData) + "%");
				}
				if (listSlot[5] != null && listItemBachHoMap[5] != null)
				{
					listItemBachHoMap[5].SetData(GetSpriteNameThuocTinh(listSlot[5]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot6 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(6, doiHinhData) + "%");
				}
				if (listSlot[6] != null && listItemBachHoMap[6] != null)
				{
					listItemBachHoMap[6].SetData(GetSpriteNameThuocTinh(listSlot[6]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot7 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(7, doiHinhData) + "%");
				}
				if (listSlot[7] != null && listItemBachHoMap[7] != null)
				{
					listItemBachHoMap[7].SetData(GetSpriteNameThuocTinh(listSlot[7]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot8 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(8, doiHinhData) + "%");
				}
			}
			displayItemStatus(doiHinhData.CuongHoa_Slot1, 0, 0, listItemBachHoMap[0]);
			displayItemStatus(doiHinhData.CuongHoa_Slot2, doiHinhData.CuongHoa_Slot1, 1, listItemBachHoMap[1]);
			displayItemStatus(doiHinhData.CuongHoa_Slot3, doiHinhData.CuongHoa_Slot2, 2, listItemBachHoMap[2]);
			displayItemStatus(doiHinhData.CuongHoa_Slot4, doiHinhData.CuongHoa_Slot3, 3, listItemBachHoMap[3]);
			displayItemStatus(doiHinhData.CuongHoa_Slot5, doiHinhData.CuongHoa_Slot4, 4, listItemBachHoMap[4]);
			displayItemStatus(doiHinhData.CuongHoa_Slot6, doiHinhData.CuongHoa_Slot5, 5, listItemBachHoMap[5]);
			displayItemStatus(doiHinhData.CuongHoa_Slot7, doiHinhData.CuongHoa_Slot6, 6, listItemBachHoMap[6]);
			displayItemStatus(doiHinhData.CuongHoa_Slot8, doiHinhData.CuongHoa_Slot7, 7, listItemBachHoMap[7]);
		}
		else if (listSlot != null)
		{
			if (listSlot[0] != null && listItemBachHoMap[0] != null)
			{
				listItemBachHoMap[0].SetData(GetSpriteNameThuocTinh(listSlot[0]), string.Empty);
			}
			if (listSlot[1] != null && listItemBachHoMap[1] != null)
			{
				listItemBachHoMap[1].SetData(GetSpriteNameThuocTinh(listSlot[1]), string.Empty);
			}
			if (listSlot[2] != null && listItemBachHoMap[2] != null)
			{
				listItemBachHoMap[2].SetData(GetSpriteNameThuocTinh(listSlot[2]), string.Empty);
			}
			if (listSlot[3] != null && listItemBachHoMap[3] != null)
			{
				listItemBachHoMap[3].SetData(GetSpriteNameThuocTinh(listSlot[3]), string.Empty);
			}
			if (listSlot[4] != null && listItemBachHoMap[4] != null)
			{
				listItemBachHoMap[4].SetData(GetSpriteNameThuocTinh(listSlot[4]), string.Empty);
			}
			if (listSlot[5] != null && listItemBachHoMap[5] != null)
			{
				listItemBachHoMap[5].SetData(GetSpriteNameThuocTinh(listSlot[5]), string.Empty);
			}
			if (listSlot[6] != null && listItemBachHoMap[6] != null)
			{
				listItemBachHoMap[6].SetData(GetSpriteNameThuocTinh(listSlot[6]), string.Empty);
			}
			if (listSlot[7] != null && listItemBachHoMap[7] != null)
			{
				listItemBachHoMap[7].SetData(GetSpriteNameThuocTinh(listSlot[7]), string.Empty);
			}
		}
	}

	public void OnLoadHuyenVuMap()
	{
		string soDoBQTCodeName = OtherCfg.GetSoDoBQTCodeName(curSoDoBatQuai);
		List<string> listSlot = ConfigManager.instance.OtherConfig.BatQuaiConfig[soDoBQTCodeName].ListSlot;
		if (doiHinhData.BatQuaiTranType != OtherCfg.BatQuaiTranDoType.NONE)
		{
			if (listSlot != null)
			{
				if (listSlot[0] != null && listItemHuyenVuMap[0] != null)
				{
					listItemHuyenVuMap[0].SetData(GetSpriteNameThuocTinh(listSlot[0]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot1 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(1, doiHinhData) + "%");
				}
				if (listSlot[1] != null && listItemHuyenVuMap[1] != null)
				{
					listItemHuyenVuMap[1].SetData(GetSpriteNameThuocTinh(listSlot[1]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot2 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(2, doiHinhData) + "%");
				}
				if (listSlot[2] != null && listItemHuyenVuMap[2] != null)
				{
					listItemHuyenVuMap[2].SetData(GetSpriteNameThuocTinh(listSlot[2]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot3 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(3, doiHinhData) + "%");
				}
				if (listSlot[3] != null && listItemHuyenVuMap[3] != null)
				{
					listItemHuyenVuMap[3].SetData(GetSpriteNameThuocTinh(listSlot[3]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot4 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(4, doiHinhData) + "%");
				}
				if (listSlot[4] != null && listItemHuyenVuMap[4] != null)
				{
					listItemHuyenVuMap[4].SetData(GetSpriteNameThuocTinh(listSlot[4]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot5 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(5, doiHinhData) + "%");
				}
				if (listSlot[5] != null && listItemHuyenVuMap[5] != null)
				{
					listItemHuyenVuMap[5].SetData(GetSpriteNameThuocTinh(listSlot[5]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot6 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(6, doiHinhData) + "%");
				}
				if (listSlot[6] != null && listItemHuyenVuMap[6] != null)
				{
					listItemHuyenVuMap[6].SetData(GetSpriteNameThuocTinh(listSlot[6]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot7 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(7, doiHinhData) + "%");
				}
				if (listSlot[7] != null && listItemHuyenVuMap[7] != null)
				{
					listItemHuyenVuMap[7].SetData(GetSpriteNameThuocTinh(listSlot[7]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot8 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(8, doiHinhData) + "%");
				}
			}
			displayItemStatus(doiHinhData.CuongHoa_Slot1, 0, 0, listItemHuyenVuMap[0]);
			displayItemStatus(doiHinhData.CuongHoa_Slot2, doiHinhData.CuongHoa_Slot1, 1, listItemHuyenVuMap[1]);
			displayItemStatus(doiHinhData.CuongHoa_Slot3, doiHinhData.CuongHoa_Slot2, 2, listItemHuyenVuMap[2]);
			displayItemStatus(doiHinhData.CuongHoa_Slot4, doiHinhData.CuongHoa_Slot3, 3, listItemHuyenVuMap[3]);
			displayItemStatus(doiHinhData.CuongHoa_Slot5, doiHinhData.CuongHoa_Slot4, 4, listItemHuyenVuMap[4]);
			displayItemStatus(doiHinhData.CuongHoa_Slot6, doiHinhData.CuongHoa_Slot5, 5, listItemHuyenVuMap[5]);
			displayItemStatus(doiHinhData.CuongHoa_Slot7, doiHinhData.CuongHoa_Slot6, 6, listItemHuyenVuMap[6]);
			displayItemStatus(doiHinhData.CuongHoa_Slot8, doiHinhData.CuongHoa_Slot7, 7, listItemHuyenVuMap[7]);
		}
		else if (listSlot != null)
		{
			if (listSlot[0] != null && listItemHuyenVuMap[0] != null)
			{
				listItemHuyenVuMap[0].SetData(GetSpriteNameThuocTinh(listSlot[0]), string.Empty);
			}
			if (listSlot[1] != null && listItemHuyenVuMap[1] != null)
			{
				listItemHuyenVuMap[1].SetData(GetSpriteNameThuocTinh(listSlot[1]), string.Empty);
			}
			if (listSlot[2] != null && listItemHuyenVuMap[2] != null)
			{
				listItemHuyenVuMap[2].SetData(GetSpriteNameThuocTinh(listSlot[2]), string.Empty);
			}
			if (listSlot[3] != null && listItemHuyenVuMap[3] != null)
			{
				listItemHuyenVuMap[3].SetData(GetSpriteNameThuocTinh(listSlot[3]), string.Empty);
			}
			if (listSlot[4] != null && listItemHuyenVuMap[4] != null)
			{
				listItemHuyenVuMap[4].SetData(GetSpriteNameThuocTinh(listSlot[4]), string.Empty);
			}
			if (listSlot[5] != null && listItemHuyenVuMap[5] != null)
			{
				listItemHuyenVuMap[5].SetData(GetSpriteNameThuocTinh(listSlot[5]), string.Empty);
			}
			if (listSlot[6] != null && listItemHuyenVuMap[6] != null)
			{
				listItemHuyenVuMap[6].SetData(GetSpriteNameThuocTinh(listSlot[6]), string.Empty);
			}
			if (listSlot[7] != null && listItemHuyenVuMap[7] != null)
			{
				listItemHuyenVuMap[7].SetData(GetSpriteNameThuocTinh(listSlot[7]), string.Empty);
			}
		}
	}

	public void OnLoadChuTuocMap()
	{
		string soDoBQTCodeName = OtherCfg.GetSoDoBQTCodeName(curSoDoBatQuai);
		List<string> listSlot = ConfigManager.instance.OtherConfig.BatQuaiConfig[soDoBQTCodeName].ListSlot;
		if (doiHinhData.BatQuaiTranType != OtherCfg.BatQuaiTranDoType.NONE)
		{
			if (listSlot != null)
			{
				if (listSlot[0] != null && listItemChuTuocMap[0] != null)
				{
					listItemChuTuocMap[0].SetData(GetSpriteNameThuocTinh(listSlot[0]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot1 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(1, doiHinhData) + "%");
				}
				if (listSlot[1] != null && listItemChuTuocMap[1] != null)
				{
					listItemChuTuocMap[1].SetData(GetSpriteNameThuocTinh(listSlot[1]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot2 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(2, doiHinhData) + "%");
				}
				if (listSlot[2] != null && listItemChuTuocMap[2] != null)
				{
					listItemChuTuocMap[2].SetData(GetSpriteNameThuocTinh(listSlot[2]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot3 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(3, doiHinhData) + "%");
				}
				if (listSlot[3] != null && listItemChuTuocMap[3] != null)
				{
					listItemChuTuocMap[3].SetData(GetSpriteNameThuocTinh(listSlot[3]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot4 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(4, doiHinhData) + "%");
				}
				if (listSlot[4] != null && listItemChuTuocMap[4] != null)
				{
					listItemChuTuocMap[4].SetData(GetSpriteNameThuocTinh(listSlot[4]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot5 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(5, doiHinhData) + "%");
				}
				if (listSlot[5] != null && listItemChuTuocMap[5] != null)
				{
					listItemChuTuocMap[5].SetData(GetSpriteNameThuocTinh(listSlot[5]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot6 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(6, doiHinhData) + "%");
				}
				if (listSlot[6] != null && listItemChuTuocMap[6] != null)
				{
					listItemChuTuocMap[6].SetData(GetSpriteNameThuocTinh(listSlot[6]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot7 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(7, doiHinhData) + "%");
				}
				if (listSlot[7] != null && listItemChuTuocMap[7] != null)
				{
					listItemChuTuocMap[7].SetData(GetSpriteNameThuocTinh(listSlot[7]), Localization.instance.Get("CapLabel") + " " + doiHinhData.CuongHoa_Slot8 + " - " + ConfigManager.instance.GetBatQuaiBuffHeSo(8, doiHinhData) + "%");
				}
			}
			if (doiHinhData.ListHoTro != null && doiHinhData.ListHoTro.Count > 0)
			{
				for (int i = 0; i < doiHinhData.ListHoTro.Count; i++)
				{
					if (doiHinhData.ListHoTro[i] == -1)
					{
						listItemChuTuocMap[i].setLockStatus();
					}
					else if (doiHinhData.ListHoTro[i] == 0)
					{
						listItemChuTuocMap[i].SetInviStatus();
					}
				}
			}
			displayItemStatus(doiHinhData.CuongHoa_Slot1, 0, 0, listItemChuTuocMap[0]);
			displayItemStatus(doiHinhData.CuongHoa_Slot2, doiHinhData.CuongHoa_Slot1, 1, listItemChuTuocMap[1]);
			displayItemStatus(doiHinhData.CuongHoa_Slot3, doiHinhData.CuongHoa_Slot2, 2, listItemChuTuocMap[2]);
			displayItemStatus(doiHinhData.CuongHoa_Slot4, doiHinhData.CuongHoa_Slot3, 3, listItemChuTuocMap[3]);
			displayItemStatus(doiHinhData.CuongHoa_Slot5, doiHinhData.CuongHoa_Slot4, 4, listItemChuTuocMap[4]);
			displayItemStatus(doiHinhData.CuongHoa_Slot6, doiHinhData.CuongHoa_Slot5, 5, listItemChuTuocMap[5]);
			displayItemStatus(doiHinhData.CuongHoa_Slot7, doiHinhData.CuongHoa_Slot6, 6, listItemChuTuocMap[6]);
			displayItemStatus(doiHinhData.CuongHoa_Slot8, doiHinhData.CuongHoa_Slot7, 7, listItemChuTuocMap[7]);
		}
		else if (listSlot != null)
		{
			if (listSlot[0] != null && listItemChuTuocMap[0] != null)
			{
				listItemChuTuocMap[0].SetData(GetSpriteNameThuocTinh(listSlot[0]), string.Empty);
			}
			if (listSlot[1] != null && listItemChuTuocMap[1] != null)
			{
				listItemChuTuocMap[1].SetData(GetSpriteNameThuocTinh(listSlot[1]), string.Empty);
			}
			if (listSlot[2] != null && listItemChuTuocMap[2] != null)
			{
				listItemChuTuocMap[2].SetData(GetSpriteNameThuocTinh(listSlot[2]), string.Empty);
			}
			if (listSlot[3] != null && listItemChuTuocMap[3] != null)
			{
				listItemChuTuocMap[3].SetData(GetSpriteNameThuocTinh(listSlot[3]), string.Empty);
			}
			if (listSlot[4] != null && listItemChuTuocMap[4] != null)
			{
				listItemChuTuocMap[4].SetData(GetSpriteNameThuocTinh(listSlot[4]), string.Empty);
			}
			if (listSlot[5] != null && listItemChuTuocMap[5] != null)
			{
				listItemChuTuocMap[5].SetData(GetSpriteNameThuocTinh(listSlot[5]), string.Empty);
			}
			if (listSlot[6] != null && listItemChuTuocMap[6] != null)
			{
				listItemChuTuocMap[6].SetData(GetSpriteNameThuocTinh(listSlot[6]), string.Empty);
			}
			if (listSlot[7] != null && listItemChuTuocMap[7] != null)
			{
				listItemChuTuocMap[7].SetData(GetSpriteNameThuocTinh(listSlot[7]), string.Empty);
			}
		}
	}

	public void displayItemStatus(int currentLevelSlot, int prevLevelSlot, int index, CuongHoaBatQuaiItem curItem)
	{
		if (curItem != null)
		{
			if (currentLevelSlot >= ConfigManager.instance.OtherConfig.GetMaxBatQuaiCuongHoa())
			{
				curItem.SetMaxLevel();
			}
			else if (prevLevelSlot > currentLevelSlot)
			{
				curItem.SetCurrentLevel();
			}
			else if (currentLevelSlot == 1 && currentLevelSlot == prevLevelSlot)
			{
				curItem.SetInviStatus();
			}
			else if (currentLevelSlot >= 1 && currentLevelSlot < ConfigManager.instance.OtherConfig.GetMaxBatQuaiCuongHoa() && prevLevelSlot == 0 && index == 0)
			{
				curItem.SetCurrentLevel();
			}
			if (currentLevelSlot <= prevLevelSlot && currentLevelSlot < ConfigManager.instance.OtherConfig.GetMaxBatQuaiCuongHoa() && prevLevelSlot > 1)
			{
				curItem.SetCurrentLevel();
			}
		}
	}

	public void OnSelectItemCuongHoa1()
	{
		OnSelectItemCuongHoa(1, doiHinhData.CuongHoa_Slot1);
	}

	public void OnSelectItemCuongHoa2()
	{
		OnSelectItemCuongHoa(2, doiHinhData.CuongHoa_Slot2);
	}

	public void OnSelectItemCuongHoa3()
	{
		OnSelectItemCuongHoa(3, doiHinhData.CuongHoa_Slot3);
	}

	public void OnSelectItemCuongHoa4()
	{
		OnSelectItemCuongHoa(4, doiHinhData.CuongHoa_Slot4);
	}

	public void OnSelectItemCuongHoa5()
	{
		OnSelectItemCuongHoa(5, doiHinhData.CuongHoa_Slot5);
	}

	public void OnSelectItemCuongHoa6()
	{
		OnSelectItemCuongHoa(6, doiHinhData.CuongHoa_Slot6);
	}

	public void OnSelectItemCuongHoa7()
	{
		OnSelectItemCuongHoa(7, doiHinhData.CuongHoa_Slot7);
	}

	public void OnSelectItemCuongHoa8()
	{
		OnSelectItemCuongHoa(8, doiHinhData.CuongHoa_Slot8);
	}

	public void OnSelectItemCuongHoa(int index, int curLevel)
	{
		if (isOpenByAnotherUser)
		{
			return;
		}
		currentSlot = index;
		EGDebug.Log("MAX LEVEL CUONG HOA: " + ConfigManager.instance.OtherConfig.GetMaxBatQuaiCuongHoa());
		if (curLevel >= ConfigManager.instance.OtherConfig.GetMaxBatQuaiCuongHoa())
		{
			MessagePopup.Create(Localization.instance.Get("ThongBaoMaxCuongHoaSlot"));
			return;
		}
		switch (curSoDoBatQuai)
		{
		case OtherCfg.BatQuaiTranDoType.NONE:
			currentItemSelected = null;
			break;
		case OtherCfg.BatQuaiTranDoType.THANH_LONG:
			currentItemSelected = listItemThanhLongMap[index - 1];
			break;
		case OtherCfg.BatQuaiTranDoType.BACH_HO:
			currentItemSelected = listItemBachHoMap[index - 1];
			break;
		case OtherCfg.BatQuaiTranDoType.HUYEN_VU:
			currentItemSelected = listItemHuyenVuMap[index - 1];
			break;
		case OtherCfg.BatQuaiTranDoType.CHU_TUOC:
			currentItemSelected = listItemChuTuocMap[index - 1];
			break;
		}
		UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_BAT_QUAI_TRAN_DO");
		if (vatPhamTieuThuData != null && vatPhamTieuThuData.Quantity > 0)
		{
			VatPhamTieuThuCfg vatPhamTieuThuCfg = ConfigManager.instance.m_dicVatPhamTieuThu["VP_BAT_QUAI_TRAN_DO"];
			string text = string.Empty;
			if (vatPhamTieuThuCfg != null)
			{
				text = vatPhamTieuThuCfg.TenHienThi;
			}
			PopupYesNo.Create(string.Format(Localization.instance.Get("ConfirmMessNangCapSlotSoDoBatQuai"), "1 " + text), Localization.instance.Get("Confirm"), Localization.instance.Get("Cancel"), OnNangCapSoDoBatQuai, null);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("ThongBaoKhongCoBatQuaiTranDo"));
		}
	}

	public void OnNangCapSoDoBatQuai()
	{
		UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_BAT_QUAI_TRAN_DO");
		if (vatPhamTieuThuData != null && vatPhamTieuThuData.Quantity > 0)
		{
			CuongHoaBatQuaiTranRequest cuongHoaBatQuaiTranRequest = new CuongHoaBatQuaiTranRequest();
			cuongHoaBatQuaiTranRequest.Slot = currentSlot;
			cuongHoaBatQuaiTranRequest.VatPhamID = vatPhamTieuThuData.ID;
			GameManager.instance.m_GameClient.RequestCuongHoaBatQuaiTran(cuongHoaBatQuaiTranRequest);
		}
	}

	private string GetSpriteNameThuocTinh(string strType)
	{
		switch (strType)
		{
		case "M1":
		case "M2":
			return "ngoc1";
		case "N1":
		case "N2":
			return "ngoc2";
		case "T1":
		case "T2":
			return "ngoc3";
		case "K1":
		case "K2":
			return "ngoc4";
		default:
			return string.Empty;
		}
	}

	public void btnDoiLinhDao_OnClick()
	{
		DestroyPopup();
		PopupSelectSoDoBatQuai.Create(curSoDoBatQuai);
	}

	public void OnSelectMapClick()
	{
		if (curSoDoBatQuai == OtherCfg.BatQuaiTranDoType.NONE)
		{
			return;
		}
		if (GameManager.instance.m_GameClient.UserInfo.DoiHinh.BatQuaiTranType != OtherCfg.BatQuaiTranDoType.NONE)
		{
			string soDoBQTCodeName = OtherCfg.GetSoDoBQTCodeName(curSoDoBatQuai);
			int kNBCanDoi = ConfigManager.instance.OtherConfig.BatQuaiConfig[soDoBQTCodeName].KNBCanDoi;
			if (kNBCanDoi > 0)
			{
				if (kNBCanDoi > GameManager.instance.m_GameClient.UserInfo.Gamer.Vang)
				{
					PopUpCheckKNB.Create();
				}
				else
				{
					PopupYesNo.Create(string.Format(Localization.instance.Get("ConfirmMessThayDoiSoDoBatQuaiTran"), kNBCanDoi), Localization.instance.Get("Confirm"), Localization.instance.Get("Cancel"), OnFinishSelectSoDo, null);
				}
			}
		}
		else
		{
			PopupYesNo.Create(Localization.instance.Get("ConfirmMessChonSoDoBatQuaiTran"), Localization.instance.Get("Confirm"), Localization.instance.Get("Cancel"), OnFinishSelectSoDo, null);
		}
	}

	public void OnFinishSelectSoDo()
	{
		SetSoDoBatQuaiTranRequest setSoDoBatQuaiTranRequest = new SetSoDoBatQuaiTranRequest();
		setSoDoBatQuaiTranRequest.SoDoType = curSoDoBatQuai;
		GameManager.instance.m_GameClient.RequestSetSoDoBatQuaiTran(setSoDoBatQuaiTranRequest);
	}

	public void OnCancelClick()
	{
		DestroyPopup();
	}

	public void UpdateView(CuongHoaBatQuaiTranResponse response)
	{
		if (response != null)
		{
			currentSlot = response.Slot;
			doiHinhData = GameManager.instance.m_GameClient.UserInfo.DoiHinh;
			if (currentItemSelected != null)
			{
				currentItemSelected.startCuongHoa();
			}
			StartCoroutine(updateInfoAfterCuongHoa(1f));
		}
	}

	public IEnumerator updateInfoAfterCuongHoa(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		displayInfo();
	}
}
