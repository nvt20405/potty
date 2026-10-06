using System.Collections.Generic;
using UnityEngine;

public class ScreenBanhChung3D : MonoBehaviour
{
	public Transform MeshParrent;

	public Transform SpawnPos;

	public CameraMovement Cam;

	public BanhChungMainPlayer mainPlayer;

	public GameObject[] ListNoiBanh;

	public BoxCollider NoiBanhCollider;

	public LonSuaController lon_sua;

	public GameObject baoGao;

	public GameObject buiDong;

	public Transform[] listLonPos;

	public Transform[] listThocPos;

	public List<BanhChungPlayer> listOthers = new List<BanhChungPlayer>();

	public Vector3 GetRandomLonSuaPos()
	{
		int num = Random.Range(0, listLonPos.Length - 1);
		return listLonPos[num].position;
	}

	public Vector3 GetRandomBaoGaoPos()
	{
		int num = Random.Range(0, listThocPos.Length - 1);
		return listThocPos[num].position;
	}

	private void Start()
	{
		Cam.GetComponent<CameraMovement>().relCameraPos = new Vector3(0f, 16f, 16f);
	}

	public void SpawnOthers(List<HomeResponse.Gamer3DInfo> others)
	{
		List<BanhChungPlayer> list = new List<BanhChungPlayer>();
		BanhChungPlayer p;
		foreach (BanhChungPlayer listOther in listOthers)
		{
			p = listOther;
			if (!others.Exists((HomeResponse.Gamer3DInfo e) => e.ID == p.GID && e.SID == p.SID))
			{
				list.Add(p);
			}
		}
		foreach (BanhChungPlayer item in list)
		{
			Object.Destroy(item.gameObject);
			listOthers.Remove(item);
		}
		list.Clear();
		Vector3 vector = default(Vector3);
		HomeResponse.Gamer3DInfo ava;
		foreach (HomeResponse.Gamer3DInfo other in others)
		{
			ava = other;
			BanhChungPlayer banhChungPlayer = listOthers.Find((BanhChungPlayer e) => e.GID == ava.ID && e.SID == ava.SID);
			if (banhChungPlayer != null)
			{
				vector = new Vector3(ava.PosInHome.X, ava.PosInHome.Y, ava.PosInHome.Z);
				banhChungPlayer.MoveTo(vector);
			}
			else
			{
				GameObject gameObject = SpawnOtherAvatar(ava);
				banhChungPlayer = gameObject.GetComponent<BanhChungPlayer>();
				listOthers.Add(banhChungPlayer);
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

	public void SpawnBaoGao()
	{
		if (!(mainPlayer == null))
		{
			Vector3 randomBaoGaoPos = GetRandomBaoGaoPos();
			if (baoGao == null)
			{
				Object obj = Object.Instantiate(Resources.Load("prefabs/banhchung/Bao_Thoc"));
				GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
				gameObject.transform.parent = base.transform;
				gameObject.transform.position = randomBaoGaoPos;
				baoGao = gameObject;
			}
		}
	}

	public void SpawnBuiDong()
	{
		if (!(mainPlayer == null))
		{
			Vector3 randomLonSuaPos = GetRandomLonSuaPos();
			if (buiDong == null)
			{
				Object obj = Object.Instantiate(Resources.Load("prefabs/banhchung/Bui_La_Dong"));
				GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
				gameObject.transform.parent = base.transform;
				gameObject.transform.position = randomLonSuaPos;
				buiDong = gameObject;
			}
		}
	}

	public void SpawnLonSua()
	{
		if (!(mainPlayer == null))
		{
			Vector3 randomLonSuaPos = GetRandomLonSuaPos();
			Vector3 vector = randomLonSuaPos;
			if (lon_sua == null)
			{
				Object obj = Object.Instantiate(Resources.Load("prefabs/banhchung/LonSua"));
				GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
				gameObject.transform.parent = base.transform;
				gameObject.transform.position = vector;
				lon_sua = gameObject.GetComponent<LonSuaController>();
				lon_sua.spawnPos = vector;
			}
		}
	}

	public GameObject SpawnPlayerAvatar(NguoiNauBanh nguoiChoi, HomeResponse.Gamer3DInfo playerInfo)
	{
		Object obj = Object.Instantiate(Resources.Load("prefabs/home/BanhChungMainPlayer"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		if (gameObject != null)
		{
			gameObject.transform.parent = SpawnPos.parent;
			Vector3 position = GUIManager.instance.homeCity.mainAvatar.transform.position;
			gameObject.transform.position = position;
			Vector3 vector = SpawnPos.position - gameObject.transform.position;
			BanhChungMainPlayer component = gameObject.GetComponent<BanhChungMainPlayer>();
			if (vector.sqrMagnitude > 50f)
			{
				component.Teleport(SpawnPos.position);
				GameManager.instance.m_GameClient.RequestBanhChungPlayerMove(SpawnPos.position.x, SpawnPos.position.y, SpawnPos.position.z);
			}
			if (component != null)
			{
				component.SetCodeName(playerInfo.CodeNameAvatar, playerInfo.CodeNameVuKhi, playerInfo.CostumeName);
				component.SetUserName(playerInfo.UserName);
				component.GID = nguoiChoi.GID;
				component.SID = nguoiChoi.ServerID;
				mainPlayer = component;
				EGDebug.Log(playerInfo.BoPhap);
				EGDebug.Log(component.avatar.BoPhapName);
				component.avatar.LoadBoPhap(playerInfo.BoPhap);
				component.avatar.LoadNoiCong(playerInfo.NoiCong);
			}
			else
			{
				EGDebug.Log("Can not spawn avatar 3d");
			}
			if (Cam != null)
			{
				Cam.GetComponent<CameraMovement>().Init(gameObject.transform);
			}
		}
		else
		{
			EGDebug.Log("Can not isntantiate main avatar");
		}
		return gameObject;
	}

	public GameObject SpawnOtherAvatar(HomeResponse.Gamer3DInfo avatar)
	{
		Object obj = Object.Instantiate(Resources.Load("prefabs/home/BanhChungPlayer"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		if (gameObject != null)
		{
			gameObject.transform.parent = SpawnPos.parent;
			gameObject.transform.localPosition = new Vector3(avatar.PosInHome.X, avatar.PosInHome.Y, avatar.PosInHome.Z);
			BanhChungPlayer component = gameObject.GetComponent<BanhChungPlayer>();
			if (component != null)
			{
				component.SetCodeName(avatar.CodeNameAvatar, avatar.CodeNameVuKhi, avatar.CostumeName);
				component.SetUserName(string.Format("{0}.{1}", avatar.SID, avatar.UserName));
				component.GID = avatar.ID;
				component.SID = avatar.SID;
				component.avatar.LoadBoPhap(avatar.BoPhap);
				component.avatar.LoadNoiCong(avatar.NoiCong);
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

	public void SetNoiBanh(int level)
	{
		for (int i = 0; i < ListNoiBanh.Length; i++)
		{
			if (ListNoiBanh[i] != null)
			{
				ListNoiBanh[i].SetActive(i == level);
			}
		}
	}

	public Vector3 GetRandomPosNauBanh()
	{
		float num = (float)((Random.Range(0, 1) <= 0) ? 1 : (-1)) * Random.Range(3.2f, 4.4f);
		float num2 = (float)((Random.Range(0, 1) <= 0) ? 1 : (-1)) * Random.Range(3.2f, 4.4f);
		Vector3 position = ListNoiBanh[0].transform.position;
		Vector3 vector = default(Vector3);
		return new Vector3(position.x + num, 0.1f, position.z + num2);
	}
}
