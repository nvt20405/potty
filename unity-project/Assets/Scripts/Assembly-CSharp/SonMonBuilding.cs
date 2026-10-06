using UnityEngine;

public class SonMonBuilding : MonoBehaviour
{
	public ScreenSonMon3D SonMonMain;

	public UserInfo.SonMonBuildingInfo info;

	public GameObject GUIPanel;

	public UILabel NameLabel;

	public UILabel BuildTimeLabel;

	public GameObject ThuHoachReady;

	public GameObject RaidReady;

	public GameObject BuildReady;

	public GameObject BuildingEff;

	public float RaidTimer;

	public void ShowGUIPanel()
	{
		if (SonMonMain.isReadOnly)
		{
			return;
		}
		if (info != null && info.LoaiCongTrinh == UserInfo.SonMonBuildingInfo.SonMonBuildingType.NULL_CT)
		{
			PopupSonMonXayDung.CreateUtility(info.ID);
			return;
		}
		if (SonMonHelper.CheckThuHoachValid(info, GameManager.instance.m_GameClient.UserInfo) || (info.NextLevel > info.Level && info.BuildTime < GameManager.instance.m_GameClient.ServerTime))
		{
			GameManager.instance.m_GameClient.RequestThuHoachSonMon(info.ID);
		}
		GUIPanel.SetActive(true);
		GUIPanel.transform.position = base.transform.position;
		SonMonMain.CurBuilding = info;
		SonMonMain.CurSlot = base.gameObject.name;
		if (info != null && info.LoaiCongTrinh != UserInfo.SonMonBuildingInfo.SonMonBuildingType.NULL_CT)
		{
			NameLabel.text = ConfigManager.instance.SonMonConfig.CongTrinhCfg[info.LoaiCongTrinh.ToString()][0].DisplayName;
		}
		else
		{
			NameLabel.text = string.Empty;
		}
	}

	public void Set(UserInfo.SonMonBuildingInfo congTrinhInfo)
	{
		foreach (Transform item in base.transform)
		{
			Transform transform2 = item;
			if (transform2 != ThuHoachReady.transform && transform2 != BuildReady.transform && transform2 != RaidReady.transform)
			{
				Object.Destroy(transform2.gameObject);
			}
		}
		info = congTrinhInfo;
		if (congTrinhInfo == null && base.gameObject.name != "Slot10")
		{
			GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load("prefabs/sonmon/Terrain_nho_" + ((Random.Range(0, 2) != 0) ? "B" : "A")), base.transform.position, Quaternion.identity);
			gameObject.transform.parent = base.transform;
			gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
		}
		else if (congTrinhInfo == null)
		{
			Debug.LogWarning("chinh sanh info null");
			GameObject gameObject2 = (GameObject)Object.Instantiate(Resources.Load("prefabs/sonmon/Terrain_to"), base.transform.position, Quaternion.identity);
			gameObject2.transform.parent = base.transform;
			gameObject2.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
		}
		else if (congTrinhInfo.LoaiCongTrinh != UserInfo.SonMonBuildingInfo.SonMonBuildingType.NULL_CT)
		{
			GameObject gameObject3 = (GameObject)Object.Instantiate(Resources.Load("prefabs/sonmon/" + ConfigManager.instance.SonMonConfig.CongTrinhCfg[congTrinhInfo.LoaiCongTrinh.ToString()][congTrinhInfo.Level - 1].Model), base.transform.position, Quaternion.identity);
			gameObject3.transform.parent = base.transform;
			gameObject3.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
		}
		else if (congTrinhInfo.Slot == 10)
		{
			GameObject gameObject4 = (GameObject)Object.Instantiate(Resources.Load("prefabs/sonmon/Terrain_to"), base.transform.position, Quaternion.identity);
			gameObject4.transform.parent = base.transform;
			gameObject4.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
		}
		else
		{
			GameObject gameObject5 = (GameObject)Object.Instantiate(Resources.Load("prefabs/sonmon/Terrain_nho_" + ((Random.Range(0, 2) != 0) ? "B" : "A")), base.transform.position, Quaternion.identity);
			gameObject5.transform.parent = base.transform;
			gameObject5.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
		}
		BuildTimeLabel.gameObject.SetActive(true);
		ThuHoachReady.SetActive(false);
		RaidTimer = 10f;
		if (info != null)
		{
			RaidReady.SetActive(info.Raid);
			info.Raid = false;
		}
	}

	private void ThuHoach()
	{
		GameManager.instance.m_GameClient.RequestThuHoachSonMon(info.ID);
	}

	private void Update()
	{
		if (SonMonMain.CurSlot == base.gameObject.name)
		{
			if (info != null && info.NextLevel > info.Level && info.BuildTime > GameManager.instance.m_GameClient.ServerTime)
			{
				BuildTimeLabel.text = Localization.instance.Get("ThoiGianXayDung:") + SonMonHelper.GenTimeString(info.BuildTime - GameManager.instance.m_GameClient.ServerTime);
				BuildingEff.SetActive(true);
			}
			else
			{
				BuildTimeLabel.text = string.Empty;
				BuildingEff.SetActive(false);
			}
		}
		if (info != null && info.NextLevel > info.Level && info.BuildTime < GameManager.instance.m_GameClient.ServerTime)
		{
			BuildReady.SetActive(true);
		}
		else
		{
			BuildReady.SetActive(false);
		}
		if (RaidTimer > 0f)
		{
			RaidTimer -= Time.deltaTime;
		}
		else
		{
			RaidReady.SetActive(false);
		}
		if (SonMonHelper.CheckThuHoachValid(info, GameManager.instance.m_GameClient.UserInfo) || (info.NextLevel > info.Level && info.BuildTime < GameManager.instance.m_GameClient.ServerTime))
		{
			ThuHoachReady.SetActive(true);
		}
		else
		{
			ThuHoachReady.SetActive(false);
		}
	}
}
