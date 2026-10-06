using System;
using System.Collections.Generic;
using UnityEngine;

public class PopupChonHatGiong : MonoBehaviour
{
	public static PopupChonHatGiong instance;

	public GameObject group;

	public List<GameObject> hatGiongList;

	public GameObject checkBox;

	public UILabel PickCount;

	public UILabel CurHatGiongLabel;

	public static PopupChonHatGiong Create()
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("Popup/ChonHatGiongPopup"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		instance = gameObject.GetComponent<PopupChonHatGiong>();
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		try
		{
			instance.Set();
		}
		catch (Exception ex)
		{
			EGDebug.LogError("[PopupChonHatGiong] Set error: " + ((ex != null) ? ex.ToString() : null));
		}
		return instance;
	}

	private void SetHatGiong()
	{
		if (GameManager.instance.m_GameClient.UserInfo != null && GameManager.instance.m_GameClient.UserInfo.Gamer != null)
		{
			if (GameManager.instance.m_GameClient.UserInfo.LinhDuocInfo == null)
			{
				GameManager.instance.m_GameClient.UserInfo.LinhDuocInfo = new UserInfo.LinhDuocUserData
				{
					DailySeedCount = 10
				};
			}
			if (GameManager.instance.m_GameClient.UserInfo.Gamer.CurSeed < UserInfo.LinhDuocType.THONG_KINH_THAO || GameManager.instance.m_GameClient.UserInfo.Gamer.CurSeed > UserInfo.LinhDuocType.THIEN_SON_TUYET_LIEN)
			{
				GameManager.instance.m_GameClient.UserInfo.Gamer.CurSeed = UserInfo.LinhDuocType.THONG_KINH_THAO;
			}
			if (GameManager.instance.m_GameClient.UserInfo.Gamer.CurSeed == UserInfo.LinhDuocType.THONG_KINH_THAO)
			{
				hatGiongList[0].transform.localPosition = new Vector3(-175f, 124f, -19f);
				hatGiongList[1].transform.localPosition = new Vector3(-47f, 124f, -19f);
				hatGiongList[2].transform.localPosition = new Vector3(69f, 124f, -19f);
				hatGiongList[3].transform.localPosition = new Vector3(186f, 124f, -19f);
				checkBox.transform.localPosition = new Vector3(-175f, 124f, -19f);
				hatGiongList[0].transform.localScale = new Vector3(1.2f, 1.2f, 1f);
				hatGiongList[1].transform.localScale = new Vector3(1f, 1f, 1f);
				hatGiongList[2].transform.localScale = new Vector3(1f, 1f, 1f);
				hatGiongList[3].transform.localScale = new Vector3(1f, 1f, 1f);
			}
			else if (GameManager.instance.m_GameClient.UserInfo.Gamer.CurSeed == UserInfo.LinhDuocType.HUYET_BO_DE)
			{
				hatGiongList[0].transform.localPosition = new Vector3(-179f, 124f, -19f);
				hatGiongList[1].transform.localPosition = new Vector3(-54f, 124f, -19f);
				hatGiongList[2].transform.localPosition = new Vector3(69f, 124f, -19f);
				hatGiongList[3].transform.localPosition = new Vector3(186f, 124f, -19f);
				checkBox.transform.localPosition = new Vector3(-54f, 124f, -19f);
				hatGiongList[0].transform.localScale = new Vector3(1f, 1f, 1f);
				hatGiongList[1].transform.localScale = new Vector3(1.2f, 1.2f, 1f);
				hatGiongList[2].transform.localScale = new Vector3(1f, 1f, 1f);
				hatGiongList[3].transform.localScale = new Vector3(1f, 1f, 1f);
			}
			else if (GameManager.instance.m_GameClient.UserInfo.Gamer.CurSeed == UserInfo.LinhDuocType.HAC_LINH_CHI)
			{
				hatGiongList[0].transform.localPosition = new Vector3(-179f, 124f, -19f);
				hatGiongList[1].transform.localPosition = new Vector3(-64f, 124f, -19f);
				hatGiongList[2].transform.localPosition = new Vector3(61f, 124f, -19f);
				hatGiongList[3].transform.localPosition = new Vector3(186f, 124f, -19f);
				checkBox.transform.localPosition = new Vector3(61f, 124f, -19f);
				hatGiongList[0].transform.localScale = new Vector3(1f, 1f, 1f);
				hatGiongList[1].transform.localScale = new Vector3(1f, 1f, 1f);
				hatGiongList[2].transform.localScale = new Vector3(1.2f, 1.2f, 1f);
				hatGiongList[3].transform.localScale = new Vector3(1f, 1f, 1f);
			}
			else if (GameManager.instance.m_GameClient.UserInfo.Gamer.CurSeed == UserInfo.LinhDuocType.THIEN_SON_TUYET_LIEN)
			{
				hatGiongList[0].transform.localPosition = new Vector3(-175f, 124f, -19f);
				hatGiongList[1].transform.localPosition = new Vector3(-59f, 124f, -19f);
				hatGiongList[2].transform.localPosition = new Vector3(55f, 124f, -19f);
				hatGiongList[3].transform.localPosition = new Vector3(180f, 124f, -19f);
				checkBox.transform.localPosition = new Vector3(180f, 124f, -19f);
				hatGiongList[0].transform.localScale = new Vector3(1f, 1f, 1f);
				hatGiongList[1].transform.localScale = new Vector3(1f, 1f, 1f);
				hatGiongList[2].transform.localScale = new Vector3(1f, 1f, 1f);
				hatGiongList[3].transform.localScale = new Vector3(1.2f, 1.2f, 1f);
			}
			string key = "VP_SEED_" + GameManager.instance.m_GameClient.UserInfo.Gamer.CurSeed;
			if (ConfigManager.instance.m_dicVatPhamTieuThu.ContainsKey(key))
			{
				CurHatGiongLabel.text = ConfigManager.instance.m_dicVatPhamTieuThu[key].TenHienThi;
			}
			else
			{
				CurHatGiongLabel.text = "Thông Kinh Thảo";
			}
			PickCount.text = GameManager.instance.m_GameClient.UserInfo.LinhDuocInfo.DailySeedCount + "/10";
		}
	}

	public void Set()
	{
		SetHatGiong();
		UserInfo.VatPhamTieuThuData vatPhamTieuThuData = new UserInfo.VatPhamTieuThuData();
		vatPhamTieuThuData.Name = "VP_SEED_THONG_KINH_THAO";
		vatPhamTieuThuData.Quantity = -1;
		hatGiongList[0].GetComponent<OtherAvatar>().Set(vatPhamTieuThuData);
		vatPhamTieuThuData.Name = "VP_SEED_HUYET_BO_DE";
		hatGiongList[1].GetComponent<OtherAvatar>().Set(vatPhamTieuThuData);
		vatPhamTieuThuData.Name = "VP_SEED_HAC_LINH_CHI";
		hatGiongList[2].GetComponent<OtherAvatar>().Set(vatPhamTieuThuData);
		vatPhamTieuThuData.Name = "VP_SEED_THIEN_SON_TUYET_LIEN";
		hatGiongList[3].GetComponent<OtherAvatar>().Set(vatPhamTieuThuData);
	}

	public void Update()
	{
		if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBattle)
		{
			NGUITools.SetActive(group, false);
		}
		else
		{
			NGUITools.SetActive(group, true);
		}
	}

	public void OnChonHatGiong()
	{
		ChonHatGiongRequest request = new ChonHatGiongRequest();
		GameManager.instance.m_GameClient.RequestChonHatGiong(request);
	}

	public void OnLayHatGiong()
	{
		LayHatGiongRequest layHatGiongRequest = new LayHatGiongRequest();
		layHatGiongRequest.isCaoCap = false;
		GameManager.instance.m_GameClient.RequestLayHatGiong(layHatGiongRequest);
	}

	public void OnLayHatGiongCaoCap()
	{
		LayHatGiongRequest layHatGiongRequest = new LayHatGiongRequest();
		layHatGiongRequest.isCaoCap = true;
		GameManager.instance.m_GameClient.RequestLayHatGiong(layHatGiongRequest);
	}

	public void OnCloseClick()
	{
		DestroyPopup();
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			instance.gameObject.SetActive(false);
			UnityEngine.Object.Destroy(instance.gameObject);
		}
		instance = null;
	}
}
