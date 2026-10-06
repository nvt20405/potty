using System;
using System.Collections.Generic;
using UnityEngine;

public class ThuCuoiInfo : MonoBehaviour
{
	public GameObject ItemRoot;

	public GameObject ItemPrefab;

	public Vector3 Offset;

	public UILabel CurThuCuoiName;

	public GameObject EquippedSprite;

	public GameObject InfoGroup;

	private int viewThuCuoi;

	public UILabel UseBtnName;

	public UISprite ClassSprite;

	public GameObject ThuCuoiModel;

	public GameObject ThuCuoi3DRoot;

	public List<GameObject> ReadOnlyHidden;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Create(string thuCuoiName)
	{
		if (ThuCuoiModel != null)
		{
			UnityEngine.Object.Destroy(ThuCuoiModel);
		}
		foreach (GameObject item in ReadOnlyHidden)
		{
			item.SetActive(false);
		}
		OtherCfg.ThuCuoiCfg thuCuoiCfg = ConfigManager.instance.OtherConfig.ThuCuoiConfig[thuCuoiName];
		UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("thucuoi/" + thuCuoiCfg.CodeName, typeof(GameObject)));
		ThuCuoiModel = (GameObject)((obj is GameObject) ? obj : null);
		ThuCuoiModel.transform.parent = ThuCuoi3DRoot.transform;
		ThuCuoiModel.transform.localScale = 0.5f * Vector3.one;
		ThuCuoiModel.transform.localPosition = Vector3.zero;
		ThuCuoiModel.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
		ThuCuoi3DRoot.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
		EquippedSprite.SetActive(false);
		if (thuCuoiCfg.Loai == 1)
		{
			ClassSprite.gameObject.SetActive(true);
			ClassSprite.spriteName = "binh";
		}
		else if (thuCuoiCfg.Loai == 2)
		{
			ClassSprite.gameObject.SetActive(true);
			ClassSprite.spriteName = "at";
		}
		else if (thuCuoiCfg.Loai == 3)
		{
			ClassSprite.gameObject.SetActive(true);
			ClassSprite.spriteName = "giap";
		}
		else
		{
			ClassSprite.gameObject.SetActive(false);
		}
		CurThuCuoiName.text = thuCuoiCfg.DisplayName;
		InfoGroup.SetActive(true);
		string text = Localization.instance.Get("ThuCuoiHieuUng");
		if (thuCuoiCfg.MenhBuff > 0)
		{
			text += string.Format("\n+ {0} {1}", thuCuoiCfg.MenhBuff, Localization.instance.Get("MenhLabel"));
		}
		if (thuCuoiCfg.MenhBuffRate > 0)
		{
			text += string.Format("\n+ {0}% {1}", thuCuoiCfg.MenhBuffRate, Localization.instance.Get("MenhLabel"));
		}
		if (thuCuoiCfg.NgoaiBuff > 0)
		{
			text += string.Format("\n+ {0} {1}", thuCuoiCfg.NgoaiBuff, Localization.instance.Get("NgoaiLabel"));
		}
		if (thuCuoiCfg.NgoaiBuffRate > 0)
		{
			text += string.Format("\n+ {0}% {1}", thuCuoiCfg.NgoaiBuffRate, Localization.instance.Get("NgoaiLabel"));
		}
		if (thuCuoiCfg.ThanBuff > 0)
		{
			text += string.Format("\n+ {0} {1}", thuCuoiCfg.ThanBuff, Localization.instance.Get("ThanLabel"));
		}
		if (thuCuoiCfg.ThanBuffRate > 0)
		{
			text += string.Format("\n+ {0}% {1}", thuCuoiCfg.ThanBuffRate, Localization.instance.Get("ThanLabel"));
		}
		if (thuCuoiCfg.KhiBuff > 0)
		{
			text += string.Format("\n+ {0} {1}", thuCuoiCfg.KhiBuff, Localization.instance.Get("KhiLabel"));
		}
		if (thuCuoiCfg.KhiBuffRate > 0)
		{
			text += string.Format("\n+ {0}% {1}", thuCuoiCfg.KhiBuffRate, Localization.instance.Get("KhiLabel"));
		}
		if (thuCuoiCfg.BaoBuff > 0)
		{
			text += string.Format("\n+ {0}% {1}", thuCuoiCfg.BaoBuff, Localization.instance.Get("BaoLabel"));
		}
		if (thuCuoiCfg.NeBuff > 0)
		{
			text += string.Format("\n+ {0}% {1}", thuCuoiCfg.NeBuff, Localization.instance.Get("NeLabel"));
		}
		if (thuCuoiCfg.DoDonBuff > 0)
		{
			text += string.Format("\n+ {0}% {1}", thuCuoiCfg.DoDonBuff, Localization.instance.Get("DoDonLabel"));
		}
		InfoGroup.transform.Find("Info").GetComponent<UILabel>().text = text;
	}

	public void Create()
	{
		int num = 0;
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 0f, 0f);
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			if (transform2.name != ItemPrefab.name)
			{
				UnityEngine.Object.Destroy(transform2.gameObject);
			}
		}
		if (GameManager.instance.m_GameClient.UserInfo.ThuCuoi == null || GameManager.instance.m_GameClient.UserInfo.ThuCuoi.ThuCuoiList.Count <= 0)
		{
			return;
		}
		ItemPrefab.SetActive(true);
		foreach (UserInfo.ThuCuoiData thuCuoi in GameManager.instance.m_GameClient.UserInfo.ThuCuoi.ThuCuoiList)
		{
			num++;
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(ItemPrefab);
			gameObject.transform.parent = ItemRoot.transform;
			gameObject.transform.localPosition = vector;
			gameObject.transform.localScale = 1.2f * Vector3.one;
			gameObject.name = "ThuCuoi" + num;
			vector = ((num % 4 != 0) ? new Vector3(vector.x + Offset.x, vector.y, 0f) : new Vector3(0f, vector.y - Offset.y, 0f));
			gameObject.GetComponent<ThuCuoiItem>().Create(thuCuoi);
			UIEventListener.Get(gameObject).onClick = SelectThuCuoi;
			if (thuCuoi.ID == GameManager.instance.m_GameClient.UserInfo.Gamer.curThuCuoi)
			{
				gameObject.GetComponent<UICheckbox>().isChecked = true;
			}
		}
		ItemPrefab.SetActive(false);
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.curThuCuoi > 0)
		{
			UserInfo.ThuCuoiData thuCuoiData = GameManager.instance.m_GameClient.UserInfo.ThuCuoi.ThuCuoiList.Find((UserInfo.ThuCuoiData e) => e.ID == GameManager.instance.m_GameClient.UserInfo.Gamer.curThuCuoi);
			if (thuCuoiData == null)
			{
				SetupThuCuoi(GameManager.instance.m_GameClient.UserInfo.ThuCuoi.ThuCuoiList[0].ID);
				ItemRoot.transform.GetChild(1).GetComponent<UICheckbox>().isChecked = true;
			}
			else
			{
				SetupThuCuoi(GameManager.instance.m_GameClient.UserInfo.Gamer.curThuCuoi);
			}
		}
		else
		{
			SetupThuCuoi(GameManager.instance.m_GameClient.UserInfo.ThuCuoi.ThuCuoiList[0].ID);
			ItemRoot.transform.GetChild(0).GetComponent<UICheckbox>().isChecked = true;
		}
	}

	public void SelectThuCuoi(GameObject go)
	{
		SetupThuCuoi(go.GetComponent<ThuCuoiItem>().id);
	}

	public void UseThuCuoi()
	{
		if (TutorialPopup.instance != null)
		{
			TutorialPopup.instance.ShowNextTutorial();
		}
		if (viewThuCuoi == GameManager.instance.m_GameClient.UserInfo.Gamer.curThuCuoi)
		{
			if (GameManager.instance.m_GameClient.UserInfo.ThuCuoi.ThuCuoiList.Exists((UserInfo.ThuCuoiData e) => e.ID == GameManager.instance.m_GameClient.UserInfo.Gamer.curThuCuoi && e.ExpiredTime > GameManager.instance.m_GameClient.ServerTime))
			{
				ThaoThuCuoi();
			}
			else if (ConfigManager.instance.OtherConfig.ThuCuoiConfig[GameManager.instance.m_GameClient.UserInfo.ThuCuoi.ThuCuoiList.Find((UserInfo.ThuCuoiData e) => e.ID == viewThuCuoi).CodeName].ReActiveTime > 0)
			{
				PopupYesNo.Create(string.Format(Localization.instance.Get("ThuCuoiReActive"), ConfigManager.instance.OtherConfig.ThuCuoiConfig[GameManager.instance.m_GameClient.UserInfo.ThuCuoi.ThuCuoiList.Find((UserInfo.ThuCuoiData e) => e.ID == viewThuCuoi).CodeName].KNB, ConfigManager.instance.OtherConfig.ThuCuoiConfig[GameManager.instance.m_GameClient.UserInfo.ThuCuoi.ThuCuoiList.Find((UserInfo.ThuCuoiData e) => e.ID == viewThuCuoi).CodeName].ReActiveTime), Localization.instance.Get("DongY"), Localization.instance.Get("TuChoi"), ActiveThuCuoi, null);
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("ThuCuoi_KhongTheGiaHan"));
			}
		}
		else if (GameManager.instance.m_GameClient.UserInfo.ThuCuoi.ThuCuoiList.Find((UserInfo.ThuCuoiData e) => e.ID == viewThuCuoi).isActive)
		{
			if (GameManager.instance.m_GameClient.UserInfo.ThuCuoi.ThuCuoiList.Exists((UserInfo.ThuCuoiData e) => e.ID == viewThuCuoi && e.ExpiredTime > GameManager.instance.m_GameClient.ServerTime))
			{
				DungThuCuoi();
			}
			else if (ConfigManager.instance.OtherConfig.ThuCuoiConfig[GameManager.instance.m_GameClient.UserInfo.ThuCuoi.ThuCuoiList.Find((UserInfo.ThuCuoiData e) => e.ID == viewThuCuoi).CodeName].ReActiveTime > 0)
			{
				PopupYesNo.Create(string.Format(Localization.instance.Get("ThuCuoiReActive"), ConfigManager.instance.OtherConfig.ThuCuoiConfig[GameManager.instance.m_GameClient.UserInfo.ThuCuoi.ThuCuoiList.Find((UserInfo.ThuCuoiData e) => e.ID == viewThuCuoi).CodeName].KNB, ConfigManager.instance.OtherConfig.ThuCuoiConfig[GameManager.instance.m_GameClient.UserInfo.ThuCuoi.ThuCuoiList.Find((UserInfo.ThuCuoiData e) => e.ID == viewThuCuoi).CodeName].ReActiveTime), Localization.instance.Get("DongY"), Localization.instance.Get("TuChoi"), ActiveThuCuoi, null);
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("ThuCuoi_KhongTheGiaHan"));
			}
		}
		else if (!GameManager.instance.m_GameClient.UserInfo.ThuCuoi.ThuCuoiList.Find((UserInfo.ThuCuoiData e) => e.ID == viewThuCuoi).isActive)
		{
			PopupYesNo.Create(Localization.instance.Get("ActiveThuCuoiConfirm"), Localization.instance.Get("DongY"), Localization.instance.Get("TuChoi"), ActiveThuCuoi, null);
		}
	}

	private void ThaoThuCuoi()
	{
		ThaoNguaRequest thaoNguaRequest = new ThaoNguaRequest();
		thaoNguaRequest.id = viewThuCuoi;
		GameManager.instance.m_GameClient.RequestThaoNgua(thaoNguaRequest);
	}

	private void DungThuCuoi()
	{
		LapNguaRequest lapNguaRequest = new LapNguaRequest();
		lapNguaRequest.id = viewThuCuoi;
		GameManager.instance.m_GameClient.RequestDungNgua(lapNguaRequest);
	}

	private void ActiveThuCuoi()
	{
		ActiveNguaRequest activeNguaRequest = new ActiveNguaRequest();
		activeNguaRequest.id = viewThuCuoi;
		GameManager.instance.m_GameClient.RequestActiveNgua(activeNguaRequest);
	}

	public void SetupThuCuoi(int id)
	{
		if (ThuCuoiModel != null)
		{
			UnityEngine.Object.Destroy(ThuCuoiModel);
		}
		UserInfo.ThuCuoiData thuCuoiData = GameManager.instance.m_GameClient.UserInfo.ThuCuoi.ThuCuoiList.Find((UserInfo.ThuCuoiData e) => e.ID == id);
		OtherCfg.ThuCuoiCfg thuCuoiCfg = ConfigManager.instance.OtherConfig.ThuCuoiConfig[thuCuoiData.CodeName];
		UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("thucuoi/" + thuCuoiCfg.CodeName, typeof(GameObject)));
		ThuCuoiModel = (GameObject)((obj is GameObject) ? obj : null);
		ThuCuoiModel.transform.parent = ThuCuoi3DRoot.transform;
		ThuCuoiModel.transform.localScale = 0.5f * Vector3.one;
		ThuCuoiModel.transform.localPosition = Vector3.zero;
		ThuCuoiModel.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
		ThuCuoi3DRoot.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
		viewThuCuoi = id;
		foreach (GameObject item in ReadOnlyHidden)
		{
			item.SetActive(true);
		}
		if (id == GameManager.instance.m_GameClient.UserInfo.Gamer.curThuCuoi)
		{
			EquippedSprite.SetActive(true);
		}
		else
		{
			EquippedSprite.SetActive(false);
		}
		if (id == GameManager.instance.m_GameClient.UserInfo.Gamer.curThuCuoi && thuCuoiData.ExpiredTime > GameManager.instance.m_GameClient.ServerTime)
		{
			UseBtnName.text = Localization.instance.Get("Unequip");
		}
		else if (thuCuoiData.isActive && thuCuoiData.ExpiredTime > GameManager.instance.m_GameClient.ServerTime)
		{
			UseBtnName.text = Localization.instance.Get("Equip");
		}
		else
		{
			UseBtnName.text = Localization.instance.Get("Active");
		}
		if (thuCuoiCfg.Loai == 1)
		{
			ClassSprite.gameObject.SetActive(true);
			ClassSprite.spriteName = "binh";
		}
		else if (thuCuoiCfg.Loai == 2)
		{
			ClassSprite.gameObject.SetActive(true);
			ClassSprite.spriteName = "at";
		}
		else if (thuCuoiCfg.Loai == 3)
		{
			ClassSprite.gameObject.SetActive(true);
			ClassSprite.spriteName = "giap";
		}
		else
		{
			ClassSprite.gameObject.SetActive(false);
		}
		CurThuCuoiName.text = thuCuoiCfg.DisplayName;
		InfoGroup.SetActive(true);
		string text = Localization.instance.Get("ThuCuoiHieuUng");
		if (thuCuoiCfg.MenhBuff > 0)
		{
			text += string.Format("\n+ {0} {1}", thuCuoiCfg.MenhBuff, Localization.instance.Get("MenhLabel"));
		}
		if (thuCuoiCfg.MenhBuffRate > 0)
		{
			text += string.Format("\n+ {0}% {1}", thuCuoiCfg.MenhBuffRate, Localization.instance.Get("MenhLabel"));
		}
		if (thuCuoiCfg.NgoaiBuff > 0)
		{
			text += string.Format("\n+ {0} {1}", thuCuoiCfg.NgoaiBuff, Localization.instance.Get("NgoaiLabel"));
		}
		if (thuCuoiCfg.NgoaiBuffRate > 0)
		{
			text += string.Format("\n+ {0}% {1}", thuCuoiCfg.NgoaiBuffRate, Localization.instance.Get("NgoaiLabel"));
		}
		if (thuCuoiCfg.ThanBuff > 0)
		{
			text += string.Format("\n+ {0} {1}", thuCuoiCfg.ThanBuff, Localization.instance.Get("ThanLabel"));
		}
		if (thuCuoiCfg.ThanBuffRate > 0)
		{
			text += string.Format("\n+ {0}% {1}", thuCuoiCfg.ThanBuffRate, Localization.instance.Get("ThanLabel"));
		}
		if (thuCuoiCfg.KhiBuff > 0)
		{
			text += string.Format("\n+ {0} {1}", thuCuoiCfg.KhiBuff, Localization.instance.Get("KhiLabel"));
		}
		if (thuCuoiCfg.KhiBuffRate > 0)
		{
			text += string.Format("\n+ {0}% {1}", thuCuoiCfg.KhiBuffRate, Localization.instance.Get("KhiLabel"));
		}
		if (thuCuoiCfg.BaoBuff > 0)
		{
			text += string.Format("\n+ {0}% {1}", thuCuoiCfg.BaoBuff, Localization.instance.Get("BaoLabel"));
		}
		if (thuCuoiCfg.NeBuff > 0)
		{
			text += string.Format("\n+ {0}% {1}", thuCuoiCfg.NeBuff, Localization.instance.Get("NeLabel"));
		}
		if (thuCuoiCfg.DoDonBuff > 0)
		{
			text += string.Format("\n+ {0}% {1}", thuCuoiCfg.DoDonBuff, Localization.instance.Get("DoDonLabel"));
		}
		InfoGroup.transform.Find("Info").GetComponent<UILabel>().text = text;
		if (thuCuoiData.isActive)
		{
			if (thuCuoiData.ExpiredTime > GameManager.instance.m_GameClient.ServerTime)
			{
				TimeSpan span = thuCuoiData.ExpiredTime - GameManager.instance.m_GameClient.ServerTime;
				InfoGroup.transform.Find("Time").GetComponent<LinhDuocCountDown>().prefix = Localization.instance.Get("ThoiGianConLai") + " ";
				InfoGroup.transform.Find("Time").GetComponent<LinhDuocCountDown>().StartCountDown(span);
			}
			else
			{
				TimeSpan span2 = thuCuoiData.ExpiredTime + new TimeSpan(30, 0, 0, 0) - GameManager.instance.m_GameClient.ServerTime;
				InfoGroup.transform.Find("Time").GetComponent<LinhDuocCountDown>().prefix = Localization.instance.Get("ThoiGianXoa") + " ";
				InfoGroup.transform.Find("Time").GetComponent<LinhDuocCountDown>().StartCountDown(span2);
			}
		}
		else
		{
			InfoGroup.transform.Find("Time").GetComponent<UILabel>().text = string.Format(Localization.instance.Get("ThoiGianSuDung"), thuCuoiData.Duration);
			InfoGroup.transform.Find("Time").GetComponent<LinhDuocCountDown>().StopAllCoroutines();
		}
	}
}
