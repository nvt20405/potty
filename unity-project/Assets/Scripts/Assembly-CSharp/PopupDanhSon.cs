using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupDanhSon : MonoBehaviour
{
	private const int maxTurnFree = 3;

	private const int maxTurnOpenReward = 9;

	public UILabel tenDanhSon;

	public GameObject nhanThuongGrp;

	public GameObject xongPhaGrp;

	public UILabel khongCoBanDesc;

	public UILabel vangLabel;

	public UISprite vangSprite;

	public UILabel danhLaiVangLabel;

	public UISprite danhLaiVangSprite;

	public UILabel moHetVangLabel;

	public UISprite moHetVangSprite;

	public UILabel luotMienPhiLabel;

	public UILabel daVuotAiLabel;

	public GameObject btnDanhLai;

	public GameObject btnMoHet;

	public GameObject btnQuayThuong;

	public GameObject btnGotoMoThuong;

	public UISprite blackCoverPt;

	public DanhSonPhanThuongItem[] phanThuongList;

	public UIPanel FriendsPanel;

	public static PopupDanhSon instance;

	private UserInfo.DanhSonData danhSon;

	public int friendId;

	public static PopupDanhSon Create(UserInfo.DanhSonData ds)
	{
		DestroyPopup();
		Object obj = Object.Instantiate(Resources.Load("Popup/PopupDanhSon"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		instance = gameObject.GetComponent<PopupDanhSon>();
		instance.SetInfo(ds);
		PopupManager.instance.Add(instance.gameObject);
		gameObject.transform.localScale = Vector3.one;
		return instance;
	}

	public void SetFriends(FriensInfoResponse response)
	{
		DanhSonFriendItem[] componentsInChildren = FriendsPanel.GetComponentsInChildren<DanhSonFriendItem>(true);
		DanhSonFriendItem[] array = componentsInChildren;
		DanhSonFriendItem[] array2 = array;
		foreach (DanhSonFriendItem danhSonFriendItem in array2)
		{
			for (int j = 0; j < response.ListHeros.Count; j++)
			{
				if (response.ListHeros[j].GID == danhSonFriendItem.FriendID)
				{
					if (response.ListHeros[j].HeroCodeNames.Count > 0 && response.ListHeros[j].HeroLevels.Count > 0)
					{
						danhSonFriendItem.nv1.Set(response.ListHeros[j].HeroCodeNames[0], 0, response.ListHeros[j].HeroLevels[0]);
					}
					if (response.ListHeros[j].HeroCodeNames.Count > 1 && response.ListHeros[j].HeroLevels.Count > 1)
					{
						danhSonFriendItem.nv2.Set(response.ListHeros[j].HeroCodeNames[1], 0, response.ListHeros[j].HeroLevels[1]);
					}
					break;
				}
			}
		}
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	private void OnCloseBtnClick()
	{
		if (instance == base.gameObject)
		{
			instance = null;
		}
		Object.Destroy(base.gameObject);
	}

	private void SetInfoPhanThuong(UserInfo.DanhSonData ds)
	{
		if (ds == null)
		{
			return;
		}
		DanhSonCfg danhSonCfg = ConfigManager.instance.m_listDanhSon[ds.DanhSonIdx];
		blackCoverPt.gameObject.SetActive(false);
		for (int i = 0; i < phanThuongList.Length; i++)
		{
			switch (i)
			{
			case 0:
				phanThuongList[i].SetInfo(danhSonCfg.PhanThuong1, !DanhSonCfg.IsMoThuong(ds.LuotMoThuong, i), !DanhSonCfg.IsMoThuong(ds.LuotMoThuong, i));
				break;
			case 1:
				phanThuongList[i].SetInfo(danhSonCfg.PhanThuong2, !DanhSonCfg.IsMoThuong(ds.LuotMoThuong, i), !DanhSonCfg.IsMoThuong(ds.LuotMoThuong, i));
				break;
			case 2:
				phanThuongList[i].SetInfo(danhSonCfg.PhanThuong3, !DanhSonCfg.IsMoThuong(ds.LuotMoThuong, i), !DanhSonCfg.IsMoThuong(ds.LuotMoThuong, i));
				break;
			case 3:
				phanThuongList[i].SetInfo(danhSonCfg.PhanThuong4, !DanhSonCfg.IsMoThuong(ds.LuotMoThuong, i), !DanhSonCfg.IsMoThuong(ds.LuotMoThuong, i));
				break;
			case 4:
				phanThuongList[i].SetInfo(danhSonCfg.PhanThuong5, !DanhSonCfg.IsMoThuong(ds.LuotMoThuong, i), !DanhSonCfg.IsMoThuong(ds.LuotMoThuong, i));
				break;
			case 5:
				phanThuongList[i].SetInfo(danhSonCfg.PhanThuong6, !DanhSonCfg.IsMoThuong(ds.LuotMoThuong, i), !DanhSonCfg.IsMoThuong(ds.LuotMoThuong, i));
				break;
			case 6:
				phanThuongList[i].SetInfo(danhSonCfg.PhanThuong7, !DanhSonCfg.IsMoThuong(ds.LuotMoThuong, i), !DanhSonCfg.IsMoThuong(ds.LuotMoThuong, i));
				break;
			case 7:
				phanThuongList[i].SetInfo(danhSonCfg.PhanThuong8, !DanhSonCfg.IsMoThuong(ds.LuotMoThuong, i), !DanhSonCfg.IsMoThuong(ds.LuotMoThuong, i));
				break;
			case 8:
				phanThuongList[i].SetInfo(danhSonCfg.PhanThuong9, !DanhSonCfg.IsMoThuong(ds.LuotMoThuong, i), !DanhSonCfg.IsMoThuong(ds.LuotMoThuong, i));
				break;
			}
		}
	}

	public IEnumerator VisualQuayThuong(MoThuongDanhSonResponse response, UserInfo.DanhSonData dsInfo, int oMo)
	{
		blackCoverPt.gameObject.SetActive(true);
		int turnMo = 1;
		if (response.OpSlot2 >= 0)
		{
			turnMo = 2;
		}
		if (response.OpSlot3 >= 0)
		{
			turnMo = 3;
		}
		for (int t = 0; t < turnMo; t++)
		{
			switch (t)
			{
			case 1:
				oMo = response.OpSlot2;
				break;
			case 2:
				oMo = response.OpSlot3;
				break;
			}
			DanhSonPhanThuongItem[] array = phanThuongList;
			DanhSonPhanThuongItem[] array2 = array;
			foreach (DanhSonPhanThuongItem it in array2)
			{
				it.SetSangToi(false);
			}
			List<int> oMoList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			int selectedO = 0;
			for (int j = 0; j < oMoList.Count; j++)
			{
				if (oMoList[j] != oMo && DanhSonCfg.IsMoThuong(dsInfo.LuotMoThuong, oMoList[j] - 1))
				{
					oMoList.RemoveAt(j);
					j--;
				}
			}
			int turn = Random.Range(oMoList.Count - 1, oMoList.Count + 2);
			if (turn > 0 && oMoList.Count > 1)
			{
				for (int k = 0; k < turn; k++)
				{
					List<int> randomList = new List<int>(oMoList);
					if (selectedO > 0)
					{
						randomList.Remove(selectedO);
						phanThuongList[selectedO - 1].SetSangToi(false);
					}
					int t2 = Random.Range(0, randomList.Count - 1);
					selectedO = randomList[t2];
					phanThuongList[selectedO - 1].SetSangToi(true);
					yield return new WaitForSeconds(0.06f);
				}
			}
			else
			{
				selectedO = oMoList[0];
				phanThuongList[selectedO - 1].SetSangToi(true);
			}
			if (selectedO != oMo)
			{
				phanThuongList[selectedO - 1].SetSangToi(false);
				phanThuongList[oMo - 1].SetSangToi(true);
			}
			yield return new WaitForSeconds(0.7f);
			phanThuongList[oMo - 1].SetPhanThuongOff();
		}
		if (response.PhanThuongList != null && response.PhanThuongList.Count > 0)
		{
			PhanThuongResponse res = new PhanThuongResponse
			{
				PhanThuongList = response.PhanThuongList
			};
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTitle"), Localization.instance.Get("PhanThuongDesc"), res);
		}
		UserInfo uInfo = GameManager.instance.m_GameClient.UserInfo;
		if (response.UpdateUserInfo != null && response.UpdateUserInfo.ErrorCode == ERROR_CODE.OK)
		{
			uInfo.UpdateInfo(response.UpdateUserInfo);
		}
		blackCoverPt.gameObject.SetActive(false);
		if (instance != null)
		{
			instance.SetInfo(uInfo.GetDanhSonByIdx(response.DanhSonIdx));
		}
	}

	private void OnMoThuongBtnClick()
	{
		if (danhSon != null)
		{
			int luotMoThuong = DanhSonCfg.GetLuotMoThuong(danhSon.LuotMoThuong);
			int costMoThuongDanhSon = ConfigManager.GetCostMoThuongDanhSon(luotMoThuong - 3 + 1);
			if (GameManager.instance.m_GameClient.checkKNB(costMoThuongDanhSon))
			{
				GameManager.instance.m_GameClient.MoThuongDanhSon(danhSon.DanhSonIdx);
			}
		}
	}

	private void OnResetLuotDanhClick()
	{
		nhanThuongGrp.SetActive(false);
		xongPhaGrp.SetActive(true);
		SetXongPhaInfo(danhSon);
	}

	private void OnMoThuongClick()
	{
		nhanThuongGrp.SetActive(true);
		xongPhaGrp.SetActive(false);
		SetMoThuongInfo(danhSon);
	}

	private void SetMoThuongInfo(UserInfo.DanhSonData ds)
	{
		if (ds == null)
		{
			return;
		}
		daVuotAiLabel.text = string.Format(Localization.instance.Get("DanhSonDaVuotAiLabel"), ds.VuotAi, 3);
		SetInfoPhanThuong(ds);
		int luotMoThuong = DanhSonCfg.GetLuotMoThuong(ds.LuotMoThuong);
		if (ds.VuotAi == 3)
		{
			if (luotMoThuong >= 9)
			{
				nhanThuongGrp.gameObject.SetActive(false);
				xongPhaGrp.gameObject.SetActive(true);
				SetXongPhaInfo(ds);
				return;
			}
			btnQuayThuong.gameObject.SetActive(true);
			btnMoHet.gameObject.SetActive(true);
			moHetVangSprite.gameObject.SetActive(true);
			moHetVangLabel.gameObject.SetActive(true);
			int costMoThuongDanhSon = ConfigManager.GetCostMoThuongDanhSon(luotMoThuong - 3 + 1);
			vangLabel.gameObject.SetActive(costMoThuongDanhSon > 0);
			luotMienPhiLabel.gameObject.SetActive(costMoThuongDanhSon <= 0);
			if (costMoThuongDanhSon > 0)
			{
				vangLabel.text = costMoThuongDanhSon.ToString();
			}
			else
			{
				luotMienPhiLabel.text = string.Format(Localization.instance.Get("DanhSonLuotMienPhi"), 3 - luotMoThuong);
			}
			vangSprite.gameObject.SetActive(costMoThuongDanhSon > 0);
			moHetVangLabel.text = ConfigManager.instance.OtherConfig.GetCostMoTatCa(ds.LuotMoThuong).ToString();
		}
		else if (luotMoThuong < ds.VuotAi)
		{
			vangLabel.gameObject.SetActive(false);
			vangSprite.gameObject.SetActive(false);
			btnQuayThuong.gameObject.SetActive(true);
			btnMoHet.gameObject.SetActive(false);
			moHetVangSprite.gameObject.SetActive(false);
			moHetVangLabel.gameObject.SetActive(false);
			luotMienPhiLabel.gameObject.SetActive(true);
			luotMienPhiLabel.text = string.Format(Localization.instance.Get("DanhSonLuotMienPhi"), ds.VuotAi - luotMoThuong);
		}
		else
		{
			vangLabel.gameObject.SetActive(false);
			vangSprite.gameObject.SetActive(false);
			btnQuayThuong.gameObject.SetActive(false);
			btnMoHet.gameObject.SetActive(false);
			moHetVangSprite.gameObject.SetActive(false);
			moHetVangLabel.gameObject.SetActive(false);
			luotMienPhiLabel.gameObject.SetActive(false);
		}
	}

	private int CompareFriendDanhSon(UserInfo.BanBeData f1, UserInfo.BanBeData f2)
	{
		if (f1.DanhSonCount != f2.DanhSonCount)
		{
			return f1.DanhSonCount - f2.DanhSonCount;
		}
		if (f2.Level != f1.Level)
		{
			return f2.Level - f1.Level;
		}
		return f2.Vip - f1.Vip;
	}

	private void SetXongPhaInfo(UserInfo.DanhSonData ds)
	{
		if (ds == null)
		{
			return;
		}
		if (ds.VuotAi <= 0)
		{
			danhLaiVangLabel.gameObject.SetActive(false);
			danhLaiVangSprite.gameObject.SetActive(false);
			btnGotoMoThuong.gameObject.SetActive(false);
		}
		else
		{
			danhLaiVangLabel.gameObject.SetActive(true);
			danhLaiVangSprite.gameObject.SetActive(true);
			if (ds.VuotAi == 3)
			{
				if (DanhSonCfg.GetLuotMoThuong(ds.LuotMoThuong) < 9)
				{
					btnGotoMoThuong.gameObject.SetActive(true);
				}
				else
				{
					btnGotoMoThuong.gameObject.SetActive(false);
				}
			}
			else if (DanhSonCfg.GetLuotMoThuong(ds.LuotMoThuong) < ds.VuotAi)
			{
				btnGotoMoThuong.gameObject.SetActive(true);
			}
			else
			{
				btnGotoMoThuong.gameObject.SetActive(false);
			}
			danhLaiVangLabel.text = ConfigManager.instance.OtherConfig.GetCostDanhLaiDanhSon(ds.LuotChoi).ToString();
		}
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		if (userInfo == null || userInfo.BanBeList == null || userInfo.BanBeList.Count < 1)
		{
			khongCoBanDesc.gameObject.SetActive(true);
		}
		else
		{
			bool flag = false;
			foreach (UserInfo.BanBeData banBe in userInfo.BanBeList)
			{
				if (banBe.Status == UserInfo.BanBeData.BanBeStatus.CONFIRMED)
				{
					flag = true;
					break;
				}
			}
			khongCoBanDesc.gameObject.SetActive(!flag);
		}
		DanhSonFriendItem[] componentsInChildren = FriendsPanel.GetComponentsInChildren<DanhSonFriendItem>();
		DanhSonFriendItem[] array = componentsInChildren;
		DanhSonFriendItem[] array2 = array;
		foreach (DanhSonFriendItem danhSonFriendItem in array2)
		{
			danhSonFriendItem.gameObject.SetActive(false);
			Object.Destroy(danhSonFriendItem.gameObject, 1f);
		}
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 210f, 0f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, -120f, 0f);
		List<int> list = new List<int>();
		List<UserInfo.BanBeData> list2 = new List<UserInfo.BanBeData>(userInfo.BanBeList);
		list2.Sort(CompareFriendDanhSon);
		for (int j = 0; j < list2.Count; j++)
		{
			UserInfo.BanBeData banBeData = list2[j];
			if (banBeData.Status == UserInfo.BanBeData.BanBeStatus.CONFIRMED)
			{
				Object obj = Object.Instantiate(Resources.Load("Prefabs/DanhSon/DanhSonFriendItem"));
				GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
				gameObject.transform.parent = FriendsPanel.transform;
				gameObject.transform.localPosition = vector;
				gameObject.transform.localScale = Vector3.one;
				DanhSonFriendItem component = gameObject.GetComponent<DanhSonFriendItem>();
				component.SetInfo(banBeData.GID, banBeData.DisplayName, "empty", "empty", 0, 0, banBeData.DanhSonCount > 0);
				component.cb.radioButtonRoot = FriendsPanel.transform;
				component.OnSelectFriend = OnSelectBangHuu;
				vector += vector2;
				list.Add(banBeData.GID);
			}
		}
		GameManager.instance.m_GameClient.RequestGetFriendsDanhSon(list);
	}

	private void OnSelectBangHuu(bool isActivate, DanhSonFriendItem friendItem)
	{
		if (isActivate)
		{
			friendId = friendItem.FriendID;
		}
		else
		{
			friendId = 0;
		}
	}

	private void OnMoHetBtnClick()
	{
		if (danhSon != null && GameManager.instance.m_GameClient.checkKNB(ConfigManager.instance.OtherConfig.GiaMoTatCaDanhSon))
		{
			GameManager.instance.m_GameClient.RequestMoHetDanhSon(danhSon.DanhSonIdx);
		}
	}

	private void OnBtnXongPhaClick()
	{
		if (danhSon == null)
		{
			return;
		}
		if (danhSon.VuotAi > 0)
		{
			if (GameManager.instance.m_GameClient.checkKNB(ConfigManager.instance.OtherConfig.GiaResetDanhSon))
			{
				GameManager.instance.m_GameClient.RequestVuotAiDanhSon(danhSon.DanhSonIdx, friendId);
			}
		}
		else
		{
			GameManager.instance.m_GameClient.RequestDanhDanhSon(danhSon.DanhSonIdx, friendId);
		}
	}

	public void SetInfo(UserInfo.DanhSonData ds)
	{
		if (ds != null)
		{
			danhSon = ds;
			DanhSonCfg danhSonCfg = ConfigManager.instance.m_listDanhSon[ds.DanhSonIdx];
			tenDanhSon.text = danhSonCfg.TenHienThi;
			if (ds.VuotAi > 0)
			{
				nhanThuongGrp.gameObject.SetActive(true);
				xongPhaGrp.gameObject.SetActive(false);
				SetMoThuongInfo(ds);
			}
			else
			{
				xongPhaGrp.gameObject.SetActive(true);
				nhanThuongGrp.gameObject.SetActive(false);
				SetXongPhaInfo(ds);
			}
		}
	}
}
