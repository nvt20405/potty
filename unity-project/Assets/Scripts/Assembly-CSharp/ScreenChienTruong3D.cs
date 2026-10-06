using System.Collections.Generic;
using UnityEngine;

public class ScreenChienTruong3D : MonoBehaviour
{
	public GameObject m_container;

	private List<CT2Rune3D> _listRune = new List<CT2Rune3D>();

	private List<CT2BasePlayer> _PlayerList = new List<CT2BasePlayer>();

	public GameObject m_map;

	public Camera m_camera;

	public GameObject m_doiNoiCongPar;

	public List<JoinCT2Response.DetuInfo> DoiHinhList { get; set; }

	private ChienTruongChinhTa.NguoiChoi MainPlayerInfo { get; set; }

	public List<CT2BasePlayer> PlayerList
	{
		get
		{
			return _PlayerList;
		}
		set
		{
			_PlayerList = value;
		}
	}

	public ChienTruongChinhTa RoomInfo { get; set; }

	private void Start()
	{
	}

	private void Update()
	{
	}

	public bool GetMainPlayerChinhPhai()
	{
		if (MainPlayerInfo != null)
		{
			return MainPlayerInfo.ChinhPhai;
		}
		return true;
	}

	public CT2BasePlayer SearchPlayer(int GID, int SID)
	{
		return _PlayerList.Find((CT2BasePlayer e) => e.Info.GID == GID && e.Info.SID == SID);
	}

	public void SpawnPlayer1(ChienTruongChinhTa.NguoiChoi info)
	{
		MainPlayerInfo = info;
		Object obj = Object.Instantiate(Resources.Load("prefabs/chientruong/CT2Player1Avatar"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		if (gameObject != null)
		{
			gameObject.transform.parent = m_container.transform;
			gameObject.transform.localPosition = new Vector3(info.PosX, 0f, info.PosZ);
			CT2Player1 component = gameObject.GetComponent<CT2Player1>();
			if (component != null)
			{
				component.InitGUI();
				PlayerList.Add(component);
				component.Info = info;
				component.SetTyleHp(info.TyleHp);
				component.SetAvatar(info.NVAvt, info.VKAvt, info.BPAvt, info.NCAvt, info.ThuCuoiAvt, info.Costume, info.ThanThuName, info.ThanThuQuality);
				string ten = info.Ten;
				ten = Utils.getStringNameByKhiThe(info.KhiThe) + " " + info.Ten;
				component.GUIPanel.displayDanhHieu(info);
				component.GUIPanel.NameLabel.text = ten;
				if (component.GUIPanel.HPBarSpr != null)
				{
					component.GUIPanel.HPBarSpr.color = Color.green;
				}
			}
			else
			{
				EGDebug.Log("Can not spawn avatar 3d");
			}
			m_camera.GetComponent<CameraMovement>().Init(gameObject.transform);
		}
		else
		{
			EGDebug.Log("Can not isntantiate main avatar");
		}
	}

	public void SpawnRune(int runeType, int runeIndex, float posx, float posz)
	{
		for (int i = 0; i < _listRune.Count; i++)
		{
			if (_listRune[i] != null && _listRune[i].runeIndex == runeIndex)
			{
				_listRune[i].transform.localPosition = new Vector3(posx, 0f, posz);
				_listRune[i].SetRuneInfo(runeType, runeIndex);
				return;
			}
		}
		Object obj = Object.Instantiate(Resources.Load("prefabs/chientruong/CT2Rune"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		if (gameObject != null)
		{
			gameObject.transform.parent = m_container.transform;
			gameObject.transform.localPosition = new Vector3(posx, 0f, posz);
			CT2Rune3D component = gameObject.GetComponent<CT2Rune3D>();
			component.SetRuneInfo(runeType, runeIndex);
			_listRune.Add(component);
		}
	}

	public void SpawnPlayer3(ChienTruongChinhTa.NguoiChoi info)
	{
		Object obj = Object.Instantiate(Resources.Load("prefabs/chientruong/CT2Player3Avatar"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		if (gameObject != null)
		{
			gameObject.transform.parent = m_container.transform;
			gameObject.transform.localPosition = new Vector3(info.PosX, 0f, info.PosZ);
			CT2Player3 component = gameObject.GetComponent<CT2Player3>();
			if (component != null)
			{
				component.InitGUI();
				PlayerList.Add(component);
				component.Info = info;
				component.SetTyleHp(info.TyleHp);
				component.GUIPanel.grpDanhHieu.SetActive(false);
				if ((double)info.TyleModel > 1.5)
				{
					component.SetAvatarBoss(info.NVAvt, string.Empty);
				}
				else
				{
					component.SetAvatar(info.NVAvt, info.VKAvt, info.BPAvt, info.NCAvt, info.ThuCuoiAvt, info.Costume, info.ThanThuName, info.ThanThuQuality);
					if (info.GID > 0 && info.SID > 0)
					{
						component.GUIPanel.displayDanhHieu(info);
					}
				}
				component.ShowName(info.LifeVal, MainPlayerInfo.ChinhPhai, !string.IsNullOrEmpty(info.ThuCuoiAvt));
				component.PosFSetTarget(info.PosX, info.PosZ, 0f, 0f);
				if (component.GUIPanel.HPBarSpr != null)
				{
					component.GUIPanel.HPBarSpr.color = ((MainPlayerInfo.ChinhPhai != component.Info.ChinhPhai) ? Color.red : Color.green);
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
	}

	public void OnJoinCT2Response(JoinCT2Response r)
	{
		ScreenCT2 screenCT = GUIManager.getScreen(GAME_SCREEN.ScreenCT2) as ScreenCT2;
		DoiHinhList = r.DoiHinhList;
		SpawnPlayer1(r.ThongTinNguoiChoi);
		RoomInfo = r.RoomInfo;
		if (RoomInfo != null && RoomInfo.NguoiChoiList != null)
		{
			foreach (ChienTruongChinhTa.NguoiChoi nguoiChoi in RoomInfo.NguoiChoiList)
			{
				SpawnPlayer3(nguoiChoi);
			}
		}
		if (RoomInfo != null && RoomInfo.ListRune != null)
		{
			foreach (ChienTruongChinhTa.Rune item in RoomInfo.ListRune)
			{
				SpawnRune((int)item.Type, item.RuneIndex, item.PosX, item.PosZ);
			}
		}
		if (r.DoiHinhList != null)
		{
			GameObject[] array = new GameObject[8] { screenCT.m_detu1Gr, screenCT.m_detu2Gr, screenCT.m_detu3Gr, screenCT.m_detu4Gr, screenCT.m_detu5Gr, screenCT.m_detu6Gr, screenCT.m_detu7Gr, screenCT.m_detu8Gr };
			UISprite[] array2 = new UISprite[8] { screenCT.m_detu1AvatarSpr, screenCT.m_detu2AvatarSpr, screenCT.m_detu3AvatarSpr, screenCT.m_detu4AvatarSpr, screenCT.m_detu5AvatarSpr, screenCT.m_detu6AvatarSpr, screenCT.m_detu7AvatarSpr, screenCT.m_detu8AvatarSpr };
			UISprite[] array3 = new UISprite[8] { screenCT.m_detu1HpBarSpr, screenCT.m_detu2HpBarSpr, screenCT.m_detu3HpBarSpr, screenCT.m_detu4HpBarSpr, screenCT.m_detu5HpBarSpr, screenCT.m_detu6HpBarSpr, screenCT.m_detu7HpBarSpr, screenCT.m_detu8HpBarSpr };
			for (int i = 0; i < r.DoiHinhList.Count; i++)
			{
				array2[i].spriteName = r.DoiHinhList[i].Name;
				array3[i].fillAmount = r.DoiHinhList[i].Hp / r.DoiHinhList[i].HpMax;
				NGUITools.SetActive(array[i].gameObject, true);
			}
			for (int j = r.DoiHinhList.Count; j < array2.Length; j++)
			{
				NGUITools.SetActive(array[j].gameObject, false);
			}
		}
	}

	public void SetTyleHp(int HID, float hp)
	{
		ScreenCT2 screenCT = GUIManager.getScreen(GAME_SCREEN.ScreenCT2) as ScreenCT2;
		UISprite[] array = new UISprite[8] { screenCT.m_detu1HpBarSpr, screenCT.m_detu2HpBarSpr, screenCT.m_detu3HpBarSpr, screenCT.m_detu4HpBarSpr, screenCT.m_detu5HpBarSpr, screenCT.m_detu6HpBarSpr, screenCT.m_detu7HpBarSpr, screenCT.m_detu8HpBarSpr };
		for (int i = 0; i < DoiHinhList.Count; i++)
		{
			if (DoiHinhList[i].HID == HID)
			{
				array[i].fillAmount = hp / DoiHinhList[i].HpMax;
				break;
			}
		}
	}

	public void ResetTyleHp()
	{
		ScreenCT2 screenCT = GUIManager.getScreen(GAME_SCREEN.ScreenCT2) as ScreenCT2;
		UISprite[] array = new UISprite[8] { screenCT.m_detu1HpBarSpr, screenCT.m_detu2HpBarSpr, screenCT.m_detu3HpBarSpr, screenCT.m_detu4HpBarSpr, screenCT.m_detu5HpBarSpr, screenCT.m_detu6HpBarSpr, screenCT.m_detu7HpBarSpr, screenCT.m_detu8HpBarSpr };
		for (int i = 0; i < DoiHinhList.Count; i++)
		{
			array[i].fillAmount = 1f;
		}
	}

	public void UpdateHpDoiHinh(BattleReplay.TeamInfo I)
	{
		if (!I.Winner)
		{
			ResetTyleHp();
		}
	}

	public void UpdateHpDoiHinh(float[] tyLeHp)
	{
		int num = 0;
		foreach (JoinCT2Response.DetuInfo doiHinh in DoiHinhList)
		{
			if (num < tyLeHp.Length)
			{
				SetTyleHp(doiHinh.HID, tyLeHp[num]);
			}
			else
			{
				SetTyleHp(doiHinh.HID, 0f);
			}
			num++;
		}
	}

	public GameObject PlayParticle(string prefab, Vector3 localPos)
	{
		Object obj = Object.Instantiate(Resources.Load(prefab));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		if (gameObject != null)
		{
			gameObject.transform.parent = m_container.transform;
			gameObject.transform.localPosition = localPos;
			gameObject.transform.localScale = Vector3.one;
		}
		return gameObject;
	}

	public void PlayParticle_BossDauNC()
	{
		Vector3 zero = Vector3.zero;
		if (!(m_doiNoiCongPar == null))
		{
			return;
		}
		foreach (CT2BasePlayer player in PlayerList)
		{
			if (player.Info.TyleModel > 1.8f)
			{
				zero += player.transform.localPosition;
			}
		}
		m_doiNoiCongPar = PlayParticle("FX/Prefabs/MIS_DAU_NOI_CONG", zero / 2f);
		m_doiNoiCongPar.transform.localScale = Vector3.one * 2f;
	}
}
