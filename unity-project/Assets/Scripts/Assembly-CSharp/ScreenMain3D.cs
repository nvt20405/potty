using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class ScreenMain3D : MonoBehaviour
{
	public Camera cam;

	public GameObject thanhChinh;

	public GameObject[] KyVienPos;

	public GameObject[] TuuLauPos;

	private HomeResponse lastSyncNetworkData;

	public Dictionary<int, GameObject> otherPlayers = new Dictionary<int, GameObject>();

	public PlayerController mainAvatar;

	public GameObject dongNhanAvatar3D;

	public BunnyController bunny;

	public GameObject KyNuBanGa;

	public GameObject TieuPhongUongRuou;

	public GameObject ParticleKiemThan;

	public Transform WorshipPos;

	public WorshipStatue ChampionStatue;

	public bool IsDirty;

	private int numOtherAvatar;

	public bool IsFightingNienThu;

	public GameObject NienThuAvatar3D;

	public List<GameObject> NienThuPosList;

	public GameObject FightNienThuSpawnPoint;

	public NienThuData CurNienThu;

	private Dictionary<int, GameObject> noLeList = new Dictionary<int, GameObject>();

	private Dictionary<int, int> occupiedPos = new Dictionary<int, int>();

	private float timeCheckOfflineUser;

	private float timeSpawnBunny;

	private static readonly List<Vector3> listArround = new List<Vector3>
	{
		new Vector3(3.999961f, 0f, 0.017702792f),
		new Vector3(-3.9996474f, 0f, -0.10698889f),
		new Vector3(3.9990206f, 0f, 0.08850703f),
		new Vector3(-3.9980807f, 0f, -0.12390013f),
		new Vector3(3.9968274f, 0f, 0.15928352f),
		new Vector3(-3.995261f, 0f, -0.19465443f),
		new Vector3(3.9933815f, 0f, 0.2300101f),
		new Vector3(-3.991189f, 0f, -0.26534775f),
		new Vector3(3.9886842f, 0f, 0.3006646f),
		new Vector3(-3.9858665f, 0f, -0.33595788f),
		new Vector3(3.9827368f, 0f, 0.37122488f),
		new Vector3(-3.979295f, 0f, -0.40646276f),
		new Vector3(3.9755414f, 0f, 0.4416688f),
		new Vector3(-3.9714763f, 0f, -0.47684026f),
		new Vector3(3.9671f, 0f, 0.51197433f),
		new Vector3(-3.9624128f, 0f, -0.5470683f),
		new Vector3(0.017702619f, 0f, -1.9999217f),
		new Vector3(1.7110399f, 0f, -1.0355396f),
		new Vector3(1.7799911f, 0f, 0.9119382f),
		new Vector3(0.15909709f, 0f, 1.993662f),
		new Vector3(-1.6128368f, 0f, 1.1826907f),
		new Vector3(-1.8536143f, 0f, -0.7510751f),
		new Vector3(-0.33465198f, 0f, -1.9718033f),
		new Vector3(1.5020143f, 0f, -1.3205881f),
		new Vector3(-3.2f, 0f, 3.2f),
		new Vector3(3.2f, 0f, 3.2f),
		new Vector3(-3.2f, 0f, -3.2f),
		new Vector3(3.2f, 0f, -3.2f),
		new Vector3(-1.6f, 0f, 1.6f),
		new Vector3(1.6f, 0f, 1.6f),
		new Vector3(-1.6f, 0f, -1.6f),
		new Vector3(1.6f, 0f, -1.6f)
	};

	public void InstantiateNienThuAvatar(NienThuData nienthu)
	{
		if (NienThuAvatar3D != null)
		{
			Object.Destroy(NienThuAvatar3D);
		}
		IsFightingNienThu = true;
		Object obj = Object.Instantiate(Resources.Load("Prefabs/Home/NienThu"));
		NienThuAvatar3D = (GameObject)((obj is GameObject) ? obj : null);
		if (NienThuAvatar3D != null)
		{
			NienThuAvatar3D.transform.parent = NienThuPosList[nienthu.PosID].transform;
			NienThuAvatar3D.transform.position = NienThuPosList[nienthu.PosID].transform.position;
			NienThuAvatar3D.transform.localScale = new Vector3(1f, 1f, 1f);
			NienThuAvatar3D.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			NienThuAvatar3D.GetComponent<NienThuAvatar3D>().Set(nienthu);
		}
		CurNienThu = nienthu;
		NienThuAvatar3D.GetComponent<NienThuAvatar3D>().OnPlayerEnterTrigger = DanhNienThu;
	}

	public void ThoatNienThu()
	{
		IsFightingNienThu = false;
		if (NienThuAvatar3D != null)
		{
			Object.Destroy(NienThuAvatar3D);
		}
	}

	public void DanhNienThu(PlayerController player)
	{
		GameManager.instance.m_GameClient.RequestDanhNienThu();
	}

	public void SyncWithSieuCupData(SieuCupChampion champion)
	{
		if (champion != null && champion.GID > 0 && champion.SID > 0)
		{
			ChampionStatue.gameObject.SetActive(true);
			ChampionStatue.LoadAvatar3D(champion.Ava);
			ChampionStatue.name.text = string.Format("s{0}\n{1}", champion.SID, champion.Ten);
		}
		else
		{
			ChampionStatue.name.text = string.Empty;
			ChampionStatue.gameObject.SetActive(false);
		}
	}

	public void InstantiateDongNhanAvatar()
	{
		if (dongNhanAvatar3D != null)
		{
			Object.Destroy(dongNhanAvatar3D);
		}
		Object obj = Object.Instantiate(Resources.Load("Prefabs/Home/TayDocAvatar"));
		dongNhanAvatar3D = (GameObject)((obj is GameObject) ? obj : null);
		if (dongNhanAvatar3D != null)
		{
			dongNhanAvatar3D.transform.parent = base.transform;
			dongNhanAvatar3D.transform.localPosition = new Vector3(20f, 3f, 20f);
			dongNhanAvatar3D.transform.localScale = new Vector3(1f, 1f, 1f);
			dongNhanAvatar3D.transform.localRotation = Quaternion.Euler(0f, -135f, 0f);
		}
		ScreenDongNhan screenDongNhan = GUIManager.getScreen(GAME_SCREEN.ScreenDongNhan) as ScreenDongNhan;
		dongNhanAvatar3D.GetComponent<DongNhanAvatar3D>().OnPlayerEnterTrigger = screenDongNhan.OnPlayerEnterDongNhanTrigger;
	}

	public void DestroyDongNhan()
	{
		if (dongNhanAvatar3D != null)
		{
			Object.Destroy(dongNhanAvatar3D);
			dongNhanAvatar3D = null;
		}
	}

	private void OnEnable()
	{
		if (lastSyncNetworkData != null)
		{
			SyncWithNoLeList();
		}
		PlayKyNuBanGaAnim();
		PlayTieuPhongUongRuouAnim();
	}

	private void PlayKyNuBanGaAnim()
	{
		if (KyNuBanGa != null)
		{
			KyNuBanGa.GetComponent<Animation>().wrapMode = WrapMode.Loop;
			KyNuBanGa.GetComponent<Animation>().Play();
		}
	}

	private void PlayTieuPhongUongRuouAnim()
	{
		if (TieuPhongUongRuou != null)
		{
			TieuPhongUongRuou.GetComponent<Animation>().wrapMode = WrapMode.Loop;
			TieuPhongUongRuou.GetComponent<Animation>().Play();
		}
	}

	private void SyncWithNoLeList()
	{
		if (lastSyncNetworkData == null)
		{
			return;
		}
		List<int> list = new List<int>();
		foreach (int key in noLeList.Keys)
		{
			bool flag = true;
			foreach (HomeResponse.Gamer3DInfo noLe in lastSyncNetworkData.NoLeList)
			{
				if (key == noLe.ID)
				{
					flag = false;
				}
			}
			if (flag)
			{
				list.Add(key);
			}
		}
		foreach (int item in list)
		{
			noLeList[item].SetActive(false);
			Object.Destroy(noLeList[item]);
			noLeList.Remove(item);
			occupiedPos.Remove(item);
		}
		StartCoroutine(SpawnNoLeList());
	}

	private IEnumerator SpawnNoLeList()
	{
		List<int> freeTuuLauPos = new List<int>();
		List<int> freeKyVienPos = new List<int>();
		for (int i = 0; i < TuuLauPos.Length; i++)
		{
			if (!occupiedPos.ContainsValue(i))
			{
				freeTuuLauPos.Add(i);
			}
		}
		for (int j = 0; j < KyVienPos.Length; j++)
		{
			if (!occupiedPos.ContainsValue(j + TuuLauPos.Length))
			{
				freeKyVienPos.Add(j);
			}
		}
		foreach (HomeResponse.Gamer3DInfo g in lastSyncNetworkData.NoLeList)
		{
			NhanVatCfg cfg = null;
			if (!ConfigManager.instance.m_dicNhanVats.TryGetValue(g.CodeNameAvatar, out cfg))
			{
				continue;
			}
			if (!noLeList.ContainsKey(g.ID))
			{
				GameObject pos;
				int p3;
				if (cfg.Sex == NhanVatCfg.GioiTinh.Nam)
				{
					if (freeTuuLauPos.Count == 0)
					{
						continue;
					}
					int p2 = Random.Range(0, freeTuuLauPos.Count - 1);
					p3 = freeTuuLauPos[p2];
					freeTuuLauPos.RemoveAt(p2);
					pos = TuuLauPos[p3];
				}
				else
				{
					if (freeKyVienPos.Count == 0)
					{
						continue;
					}
					int p4 = Random.Range(0, freeKyVienPos.Count - 1);
					p3 = freeKyVienPos[p4];
					freeKyVienPos.RemoveAt(p4);
					pos = KyVienPos[p3];
					p3 = p4 + TuuLauPos.Length;
				}
				string noLeName = ":-&" + g.UserName;
				ScreenMain3D screenMain3D = this;
				int iD = g.ID;
				string codeNameAvatar = g.CodeNameAvatar;
				string empty = string.Empty;
				Vector3 position = pos.transform.position;
				NPCAvatar avatar = screenMain3D.SpawnNPCAvatar(iD, noLeName, codeNameAvatar, empty, position, pos.transform.rotation.eulerAngles.y);
				avatar.avatar.PlayAnimNoLe();
				noLeList.Add(g.ID, avatar.gameObject);
				occupiedPos.Add(g.ID, p3);
				yield return null;
			}
			else
			{
				noLeList[g.ID].GetComponent<NPCAvatar>().avatar.PlayAnimNoLe();
			}
		}
	}

	public void SyncWithOnlineData(HomeResponse response)
	{
		List<int> list = new List<int>();
		foreach (int key in otherPlayers.Keys)
		{
			if (key == response.MyInfo.ID)
			{
				continue;
			}
			bool flag = false;
			for (int i = 0; i < response.PosOtherGamers.Count; i++)
			{
				if (response.PosOtherGamers[i].ID == key)
				{
					flag = true;
					break;
				}
			}
			if (otherPlayers[key] == null)
			{
				list.Add(key);
				continue;
			}
			PlayerMovement component = otherPlayers[key].GetComponent<PlayerMovement>();
			if (!(component == null) && !(component.avatar == null))
			{
				if (flag)
				{
					component.IsOnline = true;
					component.label.color = Color.white;
				}
				else
				{
					list.Add(key);
				}
			}
		}
		foreach (int item in list)
		{
			otherPlayers[item].gameObject.SetActive(false);
			Object.Destroy(otherPlayers[item]);
			otherPlayers.Remove(item);
		}
		StartCoroutine(SpawnPlayers(response));
	}

	public void SyncWithNetworkData()
	{
		if (ParticleKiemThan != null)
		{
			if (GameManager.instance.m_GameClient.UserInfo.Gamer != null && !GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("KyNgoThamBai;"))
			{
				ParticleKiemThan.SetActive(true);
			}
			else
			{
				ParticleKiemThan.SetActive(false);
			}
		}
		HomeResponse homeResponse = GameManager.instance.m_GameClient.HomeResponse;
		List<int> list = new List<int>();
		foreach (int key in otherPlayers.Keys)
		{
			if (key == homeResponse.MyInfo.ID)
			{
				continue;
			}
			if (otherPlayers[key] == null)
			{
				list.Add(key);
				continue;
			}
			PlayerMovement component = otherPlayers[key].GetComponent<PlayerMovement>();
			if (component == null || component.avatar == null)
			{
				continue;
			}
			bool flag = false;
			for (int i = 0; i < homeResponse.PosOtherGamers.Count; i++)
			{
				if (homeResponse.PosOtherGamers[i].ID == key)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				component.IsOnline = true;
				component.label.color = Color.white;
				continue;
			}
			flag = false;
			for (int j = 0; j < homeResponse.OfflineGamers.Count; j++)
			{
				if (homeResponse.OfflineGamers[j].ID == key)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				component.IsOnline = false;
				component.label.color = Color.gray;
			}
			else
			{
				list.Add(key);
			}
		}
		foreach (int item in list)
		{
			otherPlayers[item].gameObject.SetActive(false);
			Object.Destroy(otherPlayers[item]);
			otherPlayers.Remove(item);
		}
		StartCoroutine(SpawnPlayers(homeResponse));
		lastSyncNetworkData = homeResponse;
		SyncWithNoLeList();
	}

	private void Update()
	{
		if (IsDirty)
		{
			SyncWithNetworkData();
			IsDirty = false;
		}
		timeCheckOfflineUser += Time.deltaTime;
		if (timeCheckOfflineUser > 10f && lastSyncNetworkData != null && lastSyncNetworkData.OfflineGamers.Count > 0 && mainAvatar != null)
		{
			SpawnRandomOfflineUser();
			timeCheckOfflineUser = 0f;
		}
		timeSpawnBunny += Time.deltaTime;
		if (timeSpawnBunny > 12f && GameManager.instance != null && GameManager.instance.m_GameClient != null && GameManager.instance.m_GameClient.UserInfo != null && GameManager.instance.m_GameClient.UserInfo.GiaTriThoiGian != null && GameManager.instance.m_GameClient.UserInfo.GiaTriThoiGian.BatThoCount > 0)
		{
			SpawnBunny();
			timeSpawnBunny = 0f;
		}
	}

	private void CleanUpOutRangeOffline()
	{
		if (mainAvatar == null)
		{
			return;
		}
		List<int> list = new List<int>();
		if (bunny != null && Vector3.Distance(bunny.transform.position, mainAvatar.transform.position) > 16f)
		{
			bunny.gameObject.SetActive(false);
			Object.Destroy(bunny.gameObject, 0.05f);
			bunny = null;
		}
		foreach (int key in otherPlayers.Keys)
		{
			GameObject gameObject = otherPlayers[key];
			if (gameObject != null && !gameObject.GetComponent<PlayerMovement>().IsOnline)
			{
				float num = Vector3.Distance(gameObject.transform.position, mainAvatar.transform.position);
				if (num > 100f)
				{
					list.Add(key);
				}
			}
		}
		foreach (int item in list)
		{
			GameObject value = null;
			if (otherPlayers.TryGetValue(item, out value) && value != null)
			{
				value.SetActive(false);
				Object.Destroy(value);
				otherPlayers.Remove(item);
			}
		}
	}

	private Vector3 GetReflectVectorInPath(Vector3 a, Vector3 b)
	{
		return a * (Vector3.Dot(a, b) / Vector3.Dot(a, a));
	}

	private Vector3 GetMovePoint(Vector3 p1, Vector3 p2, Vector3 p, float distance)
	{
		Vector3 reflectVectorInPath = GetReflectVectorInPath(p2 - p1, p - p1);
		Vector3 vector = (reflectVectorInPath - p + p1).normalized * distance;
		return p1 + (p + vector - p1).normalized * Vector3.Distance(p2, p1);
	}

	private void SpawnBunny()
	{
		if (mainAvatar == null)
		{
			return;
		}
		Vector3 randomPosArround = GetRandomPosArround(mainAvatar.transform.localPosition);
		Vector3 vector = mainAvatar.transform.localPosition + (randomPosArround - mainAvatar.transform.localPosition) * 1.6f;
		NavMeshHit hit = default(NavMeshHit);
		if (NavMesh.SamplePosition(randomPosArround, out hit, 12f, 1))
		{
			vector = hit.position;
		}
		if (bunny != null)
		{
			if (!bunny.IsInSightOfPlayer)
			{
				Vector3 movePoint = GetMovePoint(bunny.transform.localPosition, vector, mainAvatar.transform.localPosition, 4f);
				bunny.MoveTo(movePoint);
			}
			return;
		}
		Vector3 lhs = vector - mainAvatar.transform.localPosition;
		Vector3 vector2 = vector;
		vector2 = ((!(Vector3.Dot(lhs, mainAvatar.transform.right) > 0f)) ? (vector + 16f * mainAvatar.transform.right) : (vector - 16f * mainAvatar.transform.right));
		NavMeshHit hit2 = default(NavMeshHit);
		if (NavMesh.SamplePosition(vector2, out hit2, 12f, 1))
		{
			vector2 = hit2.position;
		}
		Vector3 movePoint2 = GetMovePoint(vector2, vector, mainAvatar.transform.localPosition, 4f);
		if (!IsPointInCamera(vector2))
		{
			Object obj = Object.Instantiate(Resources.Load("Prefabs/Home/DeVang"));
			GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
			gameObject.transform.parent = base.transform;
			gameObject.transform.position = vector2;
			bunny = gameObject.GetComponent<BunnyController>();
			bunny.MoveTo(movePoint2);
		}
	}

	public void RemoveOfflinetUser()
	{
		foreach (HomeResponse.Gamer3DInfo offlineGamer in lastSyncNetworkData.OfflineGamers)
		{
			if (otherPlayers.ContainsKey(offlineGamer.ID))
			{
				Object.Destroy(otherPlayers[offlineGamer.ID]);
				otherPlayers.Remove(offlineGamer.ID);
			}
		}
		lastSyncNetworkData.OfflineGamers = new List<HomeResponse.Gamer3DInfo>();
	}

	public void RemoveOfflinetUser(int gid)
	{
		int num = lastSyncNetworkData.OfflineGamers.FindIndex((HomeResponse.Gamer3DInfo e) => e.ID == gid);
		if (num >= 0)
		{
			lastSyncNetworkData.OfflineGamers.RemoveAt(num);
		}
		if (otherPlayers.ContainsKey(gid))
		{
			Object.Destroy(otherPlayers[gid]);
			otherPlayers.Remove(gid);
		}
	}

	public void SpawnRandomOfflineUser()
	{
		if (mainAvatar == null || lastSyncNetworkData == null || lastSyncNetworkData.OfflineGamers.Count == 0)
		{
			return;
		}
		int num = 0;
		foreach (GameObject value in otherPlayers.Values)
		{
			if (value != null && !value.GetComponent<PlayerMovement>().IsOnline)
			{
				num++;
			}
		}
		if (num >= 5 || otherPlayers.Count >= ConfigManager.instance.OtherConfig.MaxAvatar3DInHome)
		{
			return;
		}
		int index = Random.Range(0, lastSyncNetworkData.OfflineGamers.Count);
		HomeResponse.Gamer3DInfo gamer3DInfo = lastSyncNetworkData.OfflineGamers[index];
		Vector3 point;
		if (!TryCityWalkPoint(out point))
		{
			return;
		}
		Vector3 vector = point + 0.25f * (point - mainAvatar.transform.localPosition);
		CleanUpOutRangeOffline();
		numOtherAvatar = otherPlayers.Count;
		NavMeshHit hit = default(NavMeshHit);
		if (NavMesh.SamplePosition(point, out hit, 12f, 1))
		{
			vector = hit.position;
		}
		if (!otherPlayers.ContainsKey(gamer3DInfo.ID) && numOtherAvatar < ConfigManager.instance.OtherConfig.MaxAvatar3DInHome)
		{
			Vector3 lhs = vector - mainAvatar.transform.localPosition;
			Vector3 vector2 = vector;
			vector2 = ((!(Vector3.Dot(lhs, mainAvatar.transform.right) > 0f)) ? (vector + 16f * mainAvatar.transform.right) : (vector - 16f * mainAvatar.transform.right));
			NavMeshHit hit2 = default(NavMeshHit);
			if (NavMesh.SamplePosition(vector2, out hit2, 12f, 1))
			{
				vector2 = hit2.position;
			}
			if (NavMesh.SamplePosition(point, out hit2, 2f, 1))
			{
				GameObject gameObject = SpawnOtherAvatar(gamer3DInfo.ID, gamer3DInfo.UserName, gamer3DInfo.CodeNameAvatar, string.Empty, string.Empty, string.Empty, hit2.position, false, string.Empty, string.Empty, string.Empty, UserInfo.PetInfo.PetQuality.PHO_THONG, string.Empty);
				gameObject.GetComponent<PlayerMovement>().MoveRandomTarget();
				otherPlayers.Add(gamer3DInfo.ID, gameObject);
				numOtherAvatar = otherPlayers.Count;
			}
		}
	}

	private bool IsPointInCamera(Vector3 point)
	{
		Vector3 position = base.transform.TransformPoint(point);
		Vector3 point2 = cam.WorldToViewportPoint(position);
		int num;
		if (point2.z > 0f)
		{
			Rect rect = default(Rect);
			num = (new Rect(0f, 0f, 1f, 1f).Contains(point2) ? 1 : 0);
		}
		else
		{
			num = 0;
		}
		return (byte)num != 0;
	}

	public bool TryCityWalkPoint(out Vector3 point)
	{
		List<Vector3> list = new List<Vector3>();
		if (KyVienPos != null)
		{
			GameObject[] kyVienPos = KyVienPos;
			foreach (GameObject gameObject in kyVienPos)
			{
				if (gameObject != null)
				{
					list.Add(gameObject.transform.localPosition);
				}
			}
		}
		if (TuuLauPos != null)
		{
			GameObject[] tuuLauPos = TuuLauPos;
			foreach (GameObject gameObject2 in tuuLauPos)
			{
				if (gameObject2 != null)
				{
					list.Add(gameObject2.transform.localPosition);
				}
			}
		}
		if (WorshipPos != null)
		{
			list.Add(WorshipPos.localPosition);
		}
		if (NienThuPosList != null)
		{
			foreach (GameObject nienThuPos in NienThuPosList)
			{
				if (nienThuPos != null)
				{
					list.Add(nienThuPos.transform.localPosition);
				}
			}
		}
		point = Vector3.zero;
		if (list.Count == 0)
		{
			return false;
		}
		for (int k = 0; k < 12; k++)
		{
			Vector3 sourcePosition = Vector3.Lerp(list[Random.Range(0, list.Count)], list[Random.Range(0, list.Count)], Random.value);
			sourcePosition += new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
			NavMeshHit hit;
			if (!NavMesh.SamplePosition(sourcePosition, out hit, 3f, 1))
			{
				continue;
			}
			bool flag = false;
			foreach (GameObject value in otherPlayers.Values)
			{
				if (value != null && Vector3.Distance(value.transform.localPosition, hit.position) < 2f)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				point = hit.position;
				return true;
			}
		}
		return false;
	}

	private IEnumerator DelayDestroyAvatar3D(int gid, GameObject go)
	{
		yield return new WaitForSeconds(21f);
		if (go != null)
		{
			go.SetActive(false);
			Object.Destroy(go, 0.2f);
		}
		otherPlayers.Remove(gid);
		numOtherAvatar = otherPlayers.Count;
	}

	private Vector3 GetRandomPosArround(Vector3 center)
	{
		List<Vector3> list = new List<Vector3>(listArround);
		int index = Random.Range(0, listArround.Count - 1);
		Vector3 vector = listArround[index];
		return vector + center + new Vector3(Random.Range(-1f, 1f), 10f, Random.Range(-1f, 1f));
	}

	private List<Vector3> GetRandomPosArround(Vector3 center, int num)
	{
		List<Vector3> list = new List<Vector3>(listArround);
		List<Vector3> list2 = new List<Vector3>();
		for (int i = 0; i < num; i++)
		{
			int index = Random.Range(0, list.Count);
			Vector3 vector = list[index];
			list.RemoveAt(index);
			list2.Add(vector + center + new Vector3(Random.Range(-0.3f, 0.3f), 10f, Random.Range(-0.3f, 0.3f)));
		}
		return list2;
	}

	private IEnumerator SpawnPlayers(HomeResponse response)
	{
		if (response != null)
		{
			Vector3 mPos = new Vector3(response.MyInfo.PosInHome.X, response.MyInfo.PosInHome.Y, response.MyInfo.PosInHome.Z);
			numOtherAvatar = otherPlayers.Count;
			NavMeshHit hit2 = default(NavMeshHit);
			for (int i = 0; i < response.PosOtherGamers.Count; i++)
			{
				HomeResponse.Gamer3DInfo otherInfo = response.PosOtherGamers[i];
				Vector3 pos = new Vector3(otherInfo.PosInHome.X, otherInfo.PosInHome.Y, otherInfo.PosInHome.Z);
				GameObject other;
				if (otherPlayers.TryGetValue(otherInfo.ID, out other))
				{
					if (other != null)
					{
						PlayerMovement p = other.GetComponent<PlayerMovement>();
						if (p == null || p.avatar == null)
						{
							continue;
						}
						p.IsOnline = otherInfo.IsOnline;
						p.label.color = ((!p.IsOnline) ? Color.gray : Color.white);
						otherInfo.ThuCuoi = ((!p.IsOnline) ? string.Empty : otherInfo.ThuCuoi);
						if (p.CodeName != otherInfo.CodeNameAvatar || p.VuKhiName != otherInfo.CodeNameVuKhi || p.avatar.ThuCuoiName != otherInfo.ThuCuoi || p.avatar.CostumeName != otherInfo.CostumeName || p.avatar.ThanThuName != otherInfo.ThanThuName || p.avatar.ThanThuQuality != otherInfo.ThanThuQuality)
						{
							p.SetCodeName(otherInfo.CodeNameAvatar, otherInfo.CodeNameVuKhi, otherInfo.ThuCuoi, otherInfo.CostumeName, otherInfo.ThanThuName, otherInfo.ThanThuQuality);
						}
						if (p.avatar != null)
						{
							p.avatar.LoadBoPhap(otherInfo.BoPhap);
							p.avatar.LoadNoiCong(otherInfo.NoiCong);
						}
						p.MoveTo(pos);
					}
				}
				else
				{
					Vector3 t = pos + 10f * Vector3.up;
					if (NavMesh.SamplePosition(t, out hit2, 12f, 1))
					{
						pos = hit2.position;
					}
					string userName = otherInfo.UserName;
					string displayName = Utils.getStringNameByKhiThe(otherInfo.KhiThe) + " " + otherInfo.UserName;
					EGDebug.Log("SpawnPlayers - " + otherInfo.UserName + "-" + otherInfo.ID + "-" + otherInfo.GiangHoCount + "-" + otherInfo.Level);
					otherPlayers[otherInfo.ID] = SpawnOtherAvatar(otherInfo.ID, displayName, otherInfo.CodeNameAvatar, otherInfo.CodeNameVuKhi, otherInfo.BoPhap, otherInfo.NoiCong, pos, otherInfo.IsOnline, otherInfo.ThuCuoi, otherInfo.CostumeName, string.Empty, UserInfo.PetInfo.PetQuality.PHO_THONG, otherInfo.ghiChuTrongNgay, otherInfo.DanhHieuType, otherInfo.Level, otherInfo.GiangHoCount);
				}
				yield return null;
				other = null;
			}
			if (mainAvatar != null)
			{
				if (mainAvatar.avatar == null)
				{
					mainAvatar.SetCodeName(response.MyInfo.CodeNameAvatar, response.MyInfo.CodeNameVuKhi, response.MyInfo.ThuCuoi, response.MyInfo.CostumeName, response.MyInfo.ThanThuName, response.MyInfo.ThanThuQuality);
				}
				else if (mainAvatar.CodeName != response.MyInfo.CodeNameAvatar || mainAvatar.VuKhiName != response.MyInfo.CodeNameVuKhi || mainAvatar.avatar.CostumeName != response.MyInfo.CostumeName || mainAvatar.avatar.ThanThuName != response.MyInfo.ThanThuName || mainAvatar.avatar.ThanThuQuality != response.MyInfo.ThanThuQuality)
				{
					mainAvatar.SetCodeName(response.MyInfo.CodeNameAvatar, response.MyInfo.CodeNameVuKhi, response.MyInfo.ThuCuoi, response.MyInfo.CostumeName, response.MyInfo.ThanThuName, response.MyInfo.ThanThuQuality);
				}
				if (mainAvatar.avatar != null)
				{
					mainAvatar.avatar.LoadBoPhap(response.MyInfo.BoPhap);
					mainAvatar.avatar.LoadNoiCong(response.MyInfo.NoiCong);
				}
			}
			else
			{
				string userName2 = response.MyInfo.UserName;
				string displayName2 = Utils.getStringNameByKhiThe(response.MyInfo.KhiThe) + " " + response.MyInfo.UserName;
				GameObject go = SpawnPlayerAvatar(displayName2, response.MyInfo.CodeNameAvatar, response.MyInfo.CodeNameVuKhi, response.MyInfo.BoPhap, response.MyInfo.NoiCong, mPos, response.MyInfo.ThuCuoi, response.MyInfo.CostumeName, response.MyInfo.ThanThuName, response.MyInfo.ThanThuQuality, response.MyInfo.ghiChuTrongNgay, response.MyInfo.DanhHieuType, response.MyInfo.Level, response.MyInfo.GiangHoCount);
				mainAvatar = go.GetComponent<PlayerController>();
				HomeResponse.Position3D pos2 = new HomeResponse.Position3D(mainAvatar.transform.position.x, mainAvatar.transform.position.y, mainAvatar.transform.position.z);
				GameManager.instance.m_GameClient.SendRequest(GameManager.instance.m_GameClient.C2SProxy.RequestChangeNextPos, JsonMapper.ToJson(pos2, false), false);
			}
		}
		yield return null;
	}

	public GameObject SpawnPlayerAvatar(string userName, string codeName, string vukhi, string bophap, string noicong, Vector3 pos, string thuCuoi, string costume, string thanthu, UserInfo.PetInfo.PetQuality thanthuQuality, string ghiChuTrongNgay, UserInfo.GamerData.TonHieuType tonHieuType, int level, int countGH)
	{
		Object obj = Object.Instantiate(Resources.Load("Prefabs/Home/MainAvatar"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		if (gameObject != null)
		{
			gameObject.transform.parent = base.transform;
			gameObject.transform.position = pos;
			if (pos.sqrMagnitude > 10000f)
			{
				gameObject.transform.position = new Vector3(0f, 0.1f, 0f);
			}
			PlayerController component = gameObject.GetComponent<PlayerController>();
			if (component != null)
			{
				component.SetCodeName(codeName, vukhi, thuCuoi, costume, thanthu, thanthuQuality);
				component.SetInfoUser(userName, ghiChuTrongNgay, tonHieuType, level, countGH);
				component.IsOnline = true;
				if (component.avatar != null)
				{
					component.avatar.LoadBoPhap(bophap);
					component.avatar.LoadNoiCong(noicong);
				}
			}
			else
			{
				EGDebug.Log("Can not spawn avatar 3d");
			}
			if (cam != null)
			{
				CameraMovement component2 = cam.GetComponent<CameraMovement>();
				if (component2 != null)
				{
					component2.Init(gameObject.transform);
				}
			}
		}
		else
		{
			EGDebug.Log("Can not isntantiate main avatar");
		}
		return gameObject;
	}

	public NPCAvatar SpawnNPCAvatar(int gid, string userName, string codeName, string vukhi, Vector3 pos, float rotation)
	{
		Object obj = Object.Instantiate(Resources.Load("Prefabs/Home/NPCAvatar"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		NPCAvatar nPCAvatar = null;
		if (gameObject != null)
		{
			gameObject.transform.parent = base.transform;
			gameObject.transform.position = pos;
			gameObject.transform.rotation = Quaternion.Euler(0f, rotation, 0f);
			nPCAvatar = gameObject.GetComponent<NPCAvatar>();
			if (nPCAvatar != null)
			{
				nPCAvatar.SetCodeName(codeName, vukhi, string.Empty);
				nPCAvatar.SetUserName(userName);
				nPCAvatar.GID = gid;
			}
			else
			{
				EGDebug.Log("Can not spawn avatar 3d");
			}
		}
		else
		{
			EGDebug.Log("Can not isntantiate npc avatar prefab");
		}
		return nPCAvatar;
	}

	public GameObject SpawnOtherAvatar(int gid, string userName, string codeName, string vukhi, string bophap, string noicong, Vector3 pos, bool isOnline, string thuCuoi = "", string costume = "", string thanthu = "", UserInfo.PetInfo.PetQuality thanthuQuality = UserInfo.PetInfo.PetQuality.PHO_THONG, string ghiChuTrongNgay = "", UserInfo.GamerData.TonHieuType typeDanhHieu = UserInfo.GamerData.TonHieuType.LEVEL, int level = 1, int countGH = 1)
	{
		Object obj = Object.Instantiate(Resources.Load("Prefabs/Home/PlayerAvatar"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		if (gameObject != null)
		{
			gameObject.transform.parent = base.transform;
			gameObject.transform.position = pos;
			PlayerMovement component = gameObject.GetComponent<PlayerMovement>();
			if (component != null)
			{
				component.IsOnline = isOnline;
				component.SetCodeName(codeName, vukhi, thuCuoi, costume, thanthu, thanthuQuality);
				if (isOnline)
				{
					component.SetInfoUser(userName, ghiChuTrongNgay, typeDanhHieu, level, countGH);
				}
				else
				{
					component.SetUserName(userName);
					component.grpTonHieu.gameObject.SetActive(false);
				}
				component.label.color = ((!isOnline) ? Color.gray : Color.white);
				component.GID = gid;
				if (component.avatar != null)
				{
					component.avatar.LoadBoPhap(bophap);
					component.avatar.LoadNoiCong(noicong);
				}
			}
			else
			{
				EGDebug.Log("Can not spawn avatar 3d");
			}
		}
		else
		{
			EGDebug.Log("Can not isntantiate main avatar");
		}
		return gameObject;
	}
}
