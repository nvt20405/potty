using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenLienMinh3D : MonoBehaviour
{
	public Camera cam;

	public GameObject TuNghiaDuong;

	public GameObject ThienHaLau;

	public GameObject TangKiemCac;

	public GameObject LuaTraiObject;

	private bool isDisable;

	public List<GameObject> MemberList;

	private float progress;

	public Transform PosGroup;

	private void OnEnable()
	{
	}

	private void Update()
	{
	}

	public void RefreshThanhVien()
	{
		foreach (GameObject member in MemberList)
		{
			Object.Destroy(member.gameObject);
		}
		MemberList = new List<GameObject>();
		List<int> list = new List<int>();
		for (int i = 0; i < PosGroup.childCount; i++)
		{
			list.Add(i);
		}
		if (GameManager.instance.m_GameClient.UserInfo.LienMinh == null)
		{
			return;
		}
		foreach (LienMinhThanhVienData thanhVien in GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList)
		{
			if (list.Count == 0)
			{
				break;
			}
			if (thanhVien.Online)
			{
				int index = Random.Range(0, list.Count);
				Object obj = Object.Instantiate(Resources.Load<GameObject>("Prefabs/LienMinhAvatar3D"));
				GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
				gameObject.transform.parent = base.transform;
				gameObject.transform.localScale = Vector3.one;
				Transform transform = gameObject.transform.Find("Panel");
				ScreenLienMinhMain screenLienMinhMain = GUIManager.getScreen(GAME_SCREEN.ScreenLienMinhMain) as ScreenLienMinhMain;
				screenLienMinhMain.CreateGUIPanel(thanhVien.ID, transform);
				UILabel component = transform.Find("Label").GetComponent<UILabel>();
				Debug.Log("DISPLAY NAME: " + thanhVien.DisplayName);
				Debug.Log("KHI THE: " + thanhVien.KhiThe);
				string empty = string.Empty;
				empty = ((thanhVien.ID == GameManager.instance.m_GameClient.UserInfo.Gamer.ID) ? (Utils.getStringNameByKhiThe(thanhVien.KhiThe) + " " + thanhVien.DisplayName) : ((thanhVien.ID == GameManager.instance.m_GameClient.UserInfo.LienMinh.MinhChuID) ? (Utils.getStringNameByKhiThe(thanhVien.KhiThe) + " " + Localization.instance.Get("MinhChu")) : ((thanhVien.ID != GameManager.instance.m_GameClient.UserInfo.LienMinh.PhoMinhChuID) ? string.Empty : (Utils.getStringNameByKhiThe(thanhVien.KhiThe) + " " + Localization.instance.Get("PhoMinhChu")))));
				component.text = empty;
				Avatar3D avatar3D = GUIManager.instance.InstantiateAvatar3D(thanhVien.NhanVatDaiDien.Replace("_TT", string.Empty), thanhVien.VuKhi, string.Empty, string.Empty, null, null, string.Empty, thanhVien.NhanVatDaiDien, string.Empty);
				avatar3D.transform.parent = gameObject.transform;
				avatar3D.transform.localScale = Vector3.one;
				gameObject.transform.position = PosGroup.GetChild(list[index]).position;
				gameObject.transform.rotation = Quaternion.Euler(0f, Random.Range(-90, 90), 0f);
				component.transform.LookAt(cam.transform.position);
				CapsuleCollider capsuleCollider = avatar3D.gameObject.AddComponent<CapsuleCollider>();
				capsuleCollider.center = new Vector3(0f, 1f, 0f);
				capsuleCollider.height = 2f;
				LienMinhPlayerMovement lienMinhPlayerMovement = avatar3D.gameObject.AddComponent<LienMinhPlayerMovement>();
				lienMinhPlayerMovement.gid = thanhVien.ID;
				lienMinhPlayerMovement.codeName = thanhVien.NhanVatDaiDien;
				lienMinhPlayerMovement.displayName = thanhVien.DisplayName;
				list.RemoveAt(index);
				avatar3D.PlayAnimIdle();
				MemberList.Add(gameObject);
			}
		}
	}

	public void RefreshCongTrinh(bool isDisable)
	{
		if (PopupLoading.instance == null)
		{
			PopupLoading.Create();
		}
		this.isDisable = isDisable;
		if (TuNghiaDuong != null)
		{
			Object.Destroy(TuNghiaDuong);
		}
		if (ThienHaLau != null)
		{
			Object.Destroy(ThienHaLau);
		}
		if (TangKiemCac != null)
		{
			Object.Destroy(TangKiemCac);
		}
		progress = 0f;
		base.gameObject.SetActive(true);
		StartCoroutine(LoadSceneTuNghiaDuong3D());
	}

	private void OnLoadingScene3D(float prog)
	{
		if (PopupLoading.instance != null)
		{
			PopupLoading.instance.SetAmmount(progress + 0.33f * prog);
		}
	}

	private void OnFinishLoadingTuNghiaDuong3D(Object obj)
	{
		Object obj2 = Object.Instantiate(obj);
		GameObject gameObject = (GameObject)((obj2 is GameObject) ? obj2 : null);
		gameObject.transform.parent = base.transform;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localScale = Vector3.one;
		TuNghiaDuong = gameObject;
		progress += 0.33f;
		StartCoroutine(LoadSceneThienHaLau3D());
	}

	private void OnFinishLoadingThienHaLau3D(Object obj)
	{
		Object obj2 = Object.Instantiate(obj);
		GameObject gameObject = (GameObject)((obj2 is GameObject) ? obj2 : null);
		gameObject.transform.parent = base.transform;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localScale = Vector3.one;
		ThienHaLau = gameObject;
		progress += 0.33f;
		StartCoroutine(LoadSceneTangKiemCac3D());
	}

	private void OnFinishLoadingTangKiemCac3D(Object obj)
	{
		Object obj2 = Object.Instantiate(obj);
		GameObject gameObject = (GameObject)((obj2 is GameObject) ? obj2 : null);
		gameObject.transform.parent = base.transform;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localScale = Vector3.one;
		TangKiemCac = gameObject;
		progress += 0.33f;
		PopupLoading.DestroyPopup();
		if (!isDisable)
		{
			base.gameObject.SetActive(false);
		}
	}

	private IEnumerator LoadSceneTuNghiaDuong3D()
	{
		int tunghiaLvl = ((GameManager.instance.m_GameClient.UserInfo.LienMinh.TuNghiaLevel <= 1) ? 1 : (GameManager.instance.m_GameClient.UserInfo.LienMinh.TuNghiaLevel / 2));
		if (tunghiaLvl > 10)
		{
			tunghiaLvl = 10;
		}
		EGResourceAsyncLoader TuNghiaDuongRes = EGResourceAsyncLoader.Load("Prefabs/LienMinh/Tu_Nghia_Sanh_" + tunghiaLvl, true, OnFinishLoadingTuNghiaDuong3D, OnLoadingScene3D);
		yield return null;
		while (!TuNghiaDuongRes.IsDone)
		{
			yield return null;
		}
	}

	private IEnumerator LoadSceneThienHaLau3D()
	{
		EGResourceAsyncLoader ThienHaLauRes = EGResourceAsyncLoader.Load("Prefabs/LienMinh/Thien_Ha_Lau_lv" + ((GameManager.instance.m_GameClient.UserInfo.LienMinh.ThienHaLauLevel <= 1) ? 1 : (GameManager.instance.m_GameClient.UserInfo.LienMinh.ThienHaLauLevel / 2)), true, OnFinishLoadingThienHaLau3D, OnLoadingScene3D);
		yield return null;
		while (!ThienHaLauRes.IsDone)
		{
			yield return null;
		}
	}

	private IEnumerator LoadSceneTangKiemCac3D()
	{
		EGResourceAsyncLoader TangKiemCacRes = EGResourceAsyncLoader.Load("Prefabs/LienMinh/Tang_Kiem_Cac_" + GameManager.instance.m_GameClient.UserInfo.LienMinh.TangKiemCacLevel, true, OnFinishLoadingTangKiemCac3D, OnLoadingScene3D);
		yield return null;
		while (!TangKiemCacRes.IsDone)
		{
			yield return null;
		}
	}

	public void LoadLuaTrai()
	{
		if (LuaTraiObject != null)
		{
			LuaTraiObject.gameObject.SetActive(true);
			return;
		}
		Object obj = Object.Instantiate(Resources.Load("Prefabs/LienMinh/LuaTrai"));
		LuaTraiObject = (GameObject)((obj is GameObject) ? obj : null);
		LuaTraiObject.transform.parent = base.transform;
		LuaTraiObject.transform.localScale = Vector3.one;
	}

	public void TatLuaTrai()
	{
		if (LuaTraiObject != null)
		{
			LuaTraiObject.gameObject.SetActive(false);
		}
	}
}
