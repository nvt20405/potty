using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class TutorialPopup : MonoBehaviour
{
	private static string GIANG_HO_BTN = "Camera/InGame/MenuGroup/BottomA/SliderMenu/GiangHoBtn";

	public static bool createCutScene;

	public GameObject totalGroup;

	public Transform circleGroup;

	public GameObject NhanVatGroup;

	public Transform circle;

	public Transform arrow;

	public UILabel text;

	public GameObject NV3DGroup;

	public Avatar3D Nv3DAvatar;

	private Vector3 OffsetPos = new Vector3(0f, 1477f, -500f);

	private Vector3 OffsetPosSameLayer = new Vector3(0f, 0f, -500f);

	public BoxCollider[] boxColliders;

	public UIAnchor anchor;

	private float screenWidth;

	private float screenHeight;

	private float screenLeft = -200f;

	private float screenRight;

	private float screenBottom = -200f;

	private float screenTop;

	private int index = -1;

	private Dictionary<string, List<TutorialCfgItem>> m_dicTutorial;

	private UIRoot root;

	public Transform targetTransform;

	private Vector3 size;

	private string targetPath;

	private float time;

	private string CurrentTut = string.Empty;

	public AudioClip sound;

	public static TutorialPopup instance;

	private void Start()
	{
	}

	private void OnDestroy()
	{
	}

	public void ShowNextTutorial()
	{
		EGDebug.Log("-------------> " + time);
		time = 0.3f;
		index++;
		totalGroup.SetActive(true);
		circleGroup.gameObject.SetActive(false);
		StartCoroutine(ShowTutorial(index));
	}

	public void Hide()
	{
		if (!(time > 0f))
		{
			time = 0.3f;
			totalGroup.SetActive(false);
		}
	}

	private IEnumerator ShowTutorialDelay()
	{
		yield return new WaitForSeconds(0.1f);
	}

	private void OnPress()
	{
		Release();
	}

	private IEnumerator ShowTutorial(int index)
	{
		List<TutorialCfgItem> tutorials = m_dicTutorial[CurrentTut];
		if (index == tutorials.Count - 1)
		{
			GetComponent<BoxCollider>().enabled = true;
			circle.gameObject.SetActive(false);
		}
		if (index == tutorials.Count)
		{
			Release();
			GameManager.instance.isStartTutorial = false;
			yield break;
		}
		TutorialCfgItem tutorial = tutorials[index];
		string contentStr = tutorial.text;
		EGDebug.Log(index + "/" + tutorials.Count + " " + contentStr);
		text.text = contentStr;
		Vector3 size = ParseVector3FromString(tutorial.size);
		this.size = size;
		targetPath = tutorial.position;
		if (tutorial.NhanVatVisible)
		{
			NGUITools.SetActive(NhanVatGroup, true);
			Nv3DAvatar.PlayAnimIdle();
		}
		else
		{
			NGUITools.SetActive(NhanVatGroup, false);
		}
		targetTransform = root.transform.Find(targetPath);
		float delay = tutorial.delay;
		if (delay > 0f)
		{
			yield return new WaitForSeconds(delay);
		}
		circleGroup.gameObject.SetActive(true);
		AudioClip audioClip = Resources.Load<AudioClip>(tutorial.sourceAudio);
		if (GetComponent<AudioSource>().isPlaying)
		{
			GetComponent<AudioSource>().Stop();
		}
		GetComponent<AudioSource>().clip = audioClip;
		GetComponent<AudioSource>().loop = false;
		GetComponent<AudioSource>().playOnAwake = true;
		GetComponent<AudioSource>().volume = 1f;
		GetComponent<AudioSource>().Play();
	}

	private void Update()
	{
		if (targetTransform == null)
		{
			targetTransform = root.transform.Find(targetPath);
			EGDebug.Log(string.Concat(targetTransform, " ", targetPath));
		}
		if (targetTransform != null)
		{
			SetCirclePosition();
		}
		if (Nv3DAvatar != null)
		{
			Utils.SetLayer(Nv3DAvatar.transform, "GUIPopUp", true);
		}
		if (time > 0f)
		{
			time -= Time.deltaTime;
		}
	}

	public static void Release()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	private void OnCloseClick()
	{
		Release();
	}

	public static void Create(string tutName)
	{
		Release();
		instance = ((GameObject)Object.Instantiate(Resources.Load("popup/TutorialPopup"))).GetComponent<TutorialPopup>();
		instance.root = Object.FindObjectOfType(typeof(UIRoot)) as UIRoot;
		instance.transform.parent = GUIManager.instance.popUpContainer.transform;
		instance.transform.localPosition = new Vector3(0f, 0f, -600f);
		instance.transform.localScale = Vector3.one;
		Object obj = Resources.Load("config/Tutorial");
		string json = ((TextAsset)((obj is TextAsset) ? obj : null)).ToString();
		instance.m_dicTutorial = JsonMapper.ToObject<Dictionary<string, List<TutorialCfgItem>>>(json);
		instance.CurrentTut = tutName;
		instance.index = -1;
		instance.screenHeight = instance.root.manualHeight;
		instance.screenWidth = (float)Screen.width * 1f / (float)Screen.height * instance.screenHeight;
		instance.screenRight = instance.screenWidth + 100f;
		instance.screenTop = instance.screenHeight + 100f;
		string text = "NV_LAM_TRIEU_ANH";
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.MonPhaiTuongTro == LucDaiMonPhai.NguDoc || GameManager.instance.m_GameClient.UserInfo.Gamer.MonPhaiTuongTro == LucDaiMonPhai.NhatNguyetThanGiao || GameManager.instance.m_GameClient.UserInfo.Gamer.MonPhaiTuongTro == LucDaiMonPhai.TieuDao)
		{
			text = "NV_BACH_PHAT_MA_NU";
		}
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.MonPhaiTuongTro == LucDaiMonPhai.LangKhach || GameManager.instance.m_GameClient.UserInfo.Gamer.MonPhaiTuongTro == LucDaiMonPhai.HiepKhachDao || GameManager.instance.m_GameClient.UserInfo.Gamer.MonPhaiTuongTro == LucDaiMonPhai.DoanThi)
		{
			text = "NV_HOANG_SAM_NU_TU";
		}
		text = "NV_DE_NHI_MONG";
		Avatar3D avatar3D = GUIManager.instance.InstantiateAvatar3D(text, string.Empty, string.Empty, string.Empty, null, null, string.Empty, "NV_DE_NHI_MONG_TT", string.Empty);
		instance.Nv3DAvatar = avatar3D;
		avatar3D.gameObject.transform.parent = instance.NV3DGroup.transform;
		avatar3D.PlayAnimIdle();
		avatar3D.transform.localPosition = Vector3.zero;
		avatar3D.transform.localScale = Vector3.one;
		avatar3D.transform.localEulerAngles = Vector3.zero;
	}

	public void SetCirclePosition()
	{
		float num = ((!(size.x < 100f)) ? size.x : 100f);
		circle.transform.localScale = Vector3.one * num;
		circleGroup.transform.position = targetTransform.position;
		if (targetTransform.gameObject.layer == circleGroup.gameObject.layer)
		{
			if (!(targetTransform.gameObject.name == "TextGroup"))
			{
				circleGroup.transform.localPosition = circleGroup.transform.localPosition + OffsetPosSameLayer;
			}
		}
		else
		{
			circleGroup.transform.localPosition = circleGroup.transform.localPosition + OffsetPos;
		}
		Vector3 localPosition = circleGroup.transform.localPosition;
		boxColliders[0].size = new Vector3(localPosition.x - size.x / 2f - screenLeft, screenHeight, 1f);
		boxColliders[0].center = new Vector3(boxColliders[0].size.x / 2f + screenLeft, screenHeight / 2f, 0f);
		boxColliders[1].size = new Vector3(screenRight - localPosition.x - size.x / 2f, screenHeight, 1f);
		boxColliders[1].center = new Vector3(screenRight - boxColliders[1].size.x / 2f, screenHeight / 2f, 0f);
		boxColliders[2].size = new Vector3(size.x, localPosition.y - size.y / 2f - screenBottom, 1f);
		boxColliders[2].center = new Vector3(localPosition.x, boxColliders[2].size.y / 2f + screenBottom, 0f);
		boxColliders[3].size = new Vector3(size.x, screenTop - localPosition.y - size.y / 2f, 1f);
		boxColliders[3].center = new Vector3(localPosition.x, screenTop - boxColliders[3].size.y / 2f, 0f);
		UIPanel component = GetComponent<UIPanel>();
		component.Refresh();
	}

	private Vector3 ParseVector3FromString(string posStr)
	{
		string[] array = posStr.Split(' ');
		return new Vector3(int.Parse(array[0]), int.Parse(array[1]), -1f);
	}
}
