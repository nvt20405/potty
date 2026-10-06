using System.Collections.Generic;
using UnityEngine;

public class ScreenThanhChien3D : MonoBehaviour
{
	public Camera camThu;

	public Camera camCong;

	public Transform ThanhChienTransform;

	public GameObject Cong1Collider;

	public GameObject Cong2Collider;

	public GameObject Cong3Collider;

	public GameObject Cong1DefCollider;

	public GameObject Cong2DefCollider;

	public GameObject Cong3DefCollider;

	private CameraMovement activeCam;

	public int ThanhIdx;

	public BangChienMainPlayer mainPlayer;

	private List<BangChienPlayer> listOtherAvatars = new List<BangChienPlayer>();

	private void Awake()
	{
		if (activeCam == null)
		{
			SetActiveCam(true);
		}
	}

	public void SetActiveCam(bool isCamCong)
	{
		if (isCamCong)
		{
			camCong.gameObject.SetActive(true);
			camThu.gameObject.SetActive(false);
			activeCam = camCong.GetComponent<CameraMovement>();
		}
		else
		{
			camThu.gameObject.SetActive(true);
			camCong.gameObject.SetActive(false);
			activeCam = camThu.GetComponent<CameraMovement>();
		}
	}

	private void Start()
	{
		camThu.GetComponent<CameraMovement>().relCameraPos = new Vector3(-20f, 28f, 20f);
		camCong.GetComponent<CameraMovement>().relCameraPos = new Vector3(-13f, 28f, 13f);
	}

	public void SpawnPlayers(List<NguoiChoiBangChien> listOthers)
	{
		List<BangChienPlayer> list = new List<BangChienPlayer>();
		for (int i = 0; i < listOtherAvatars.Count; i++)
		{
			BangChienPlayer p = listOtherAvatars[i];
			if (!listOthers.Exists((NguoiChoiBangChien e) => e.GID == p.GID && e.SID == p.SID))
			{
				p.gameObject.SetActive(false);
				Object.Destroy(p.gameObject);
				listOtherAvatars.RemoveAt(i);
				i--;
			}
		}
		NguoiChoiBangChien nguoiChoi;
		foreach (NguoiChoiBangChien listOther in listOthers)
		{
			nguoiChoi = listOther;
			BangChienPlayer bangChienPlayer = listOtherAvatars.Find((BangChienPlayer e) => e.GID == nguoiChoi.GID && e.SID == nguoiChoi.SID);
			if (bangChienPlayer != null)
			{
				bangChienPlayer.playerInfo = nguoiChoi;
				bangChienPlayer.MoveTo(bangChienPlayer.transform.parent.TransformPoint(new Vector3(nguoiChoi.X, nguoiChoi.Y, nguoiChoi.Z)));
			}
			else
			{
				GameObject gameObject = SpawnOtherAvatar(nguoiChoi);
				bangChienPlayer = gameObject.GetComponent<BangChienPlayer>();
				listOtherAvatars.Add(bangChienPlayer);
			}
		}
	}

	public BangChienPlayer GetEnemyPlayer(NguoiChoiBangChien enemy)
	{
		BangChienPlayer bangChienPlayer = listOtherAvatars.Find((BangChienPlayer e) => e.SID == enemy.SID && e.GID == enemy.GID);
		if (bangChienPlayer == null)
		{
			bangChienPlayer = SpawnOtherAvatar(enemy).GetComponent<BangChienPlayer>();
		}
		return bangChienPlayer;
	}

	public GameObject SpawnOtherAvatar(NguoiChoiBangChien nguoiChoi)
	{
		Object obj = Object.Instantiate(Resources.Load("Prefabs/LienMinh/BangChienPlayer"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		if (gameObject != null)
		{
			gameObject.transform.parent = ThanhChienTransform;
			gameObject.transform.localPosition = new Vector3(nguoiChoi.X, nguoiChoi.Y, nguoiChoi.Z);
			BangChienPlayer component = gameObject.GetComponent<BangChienPlayer>();
			if (component != null)
			{
				component.SetCodeName(nguoiChoi.Ava, nguoiChoi.VuKhi, nguoiChoi.Costume);
				component.SetUserName(string.Format("s{0}.{1}", nguoiChoi.SID, nguoiChoi.Ten));
				component.GID = nguoiChoi.GID;
				component.SID = nguoiChoi.SID;
				component.LID = nguoiChoi.LID;
				component.LMName = nguoiChoi.BTen;
				component.playerInfo = nguoiChoi;
				component.thanhChien = GUIManager.getScreen(GAME_SCREEN.ScreenThanhChien) as ScreenThanhChien;
				component.avatar.LoadBoPhap(nguoiChoi.BoPhap);
				component.avatar.LoadNoiCong(nguoiChoi.NoiCong);
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

	public GameObject SpawnPlayerAvatar(NguoiChoiBangChien nguoiChoi)
	{
		Object obj = Object.Instantiate(Resources.Load("Prefabs/LienMinh/BangChienMainPlayer"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		if (gameObject != null)
		{
			gameObject.transform.parent = ThanhChienTransform;
			gameObject.transform.localPosition = new Vector3(nguoiChoi.X, nguoiChoi.Y, nguoiChoi.Z);
			BangChienMainPlayer component = gameObject.GetComponent<BangChienMainPlayer>();
			if (component != null)
			{
				component.SetCodeName(nguoiChoi.Ava, nguoiChoi.VuKhi, nguoiChoi.Costume);
				component.SetUserName(nguoiChoi.Ten);
				component.GID = nguoiChoi.GID;
				component.SID = nguoiChoi.SID;
				component.LID = nguoiChoi.LID;
				component.LMName = nguoiChoi.BTen;
				mainPlayer = component;
				mainPlayer.playerInfo = nguoiChoi;
				component.avatar.LoadBoPhap(nguoiChoi.BoPhap);
				component.avatar.LoadNoiCong(nguoiChoi.NoiCong);
			}
			else
			{
				EGDebug.Log("Can not spawn avatar 3d");
			}
			if (activeCam != null)
			{
				activeCam.GetComponent<CameraMovement>().Init(gameObject.transform);
			}
		}
		else
		{
			EGDebug.Log("Can not isntantiate main avatar");
		}
		return gameObject;
	}
}
