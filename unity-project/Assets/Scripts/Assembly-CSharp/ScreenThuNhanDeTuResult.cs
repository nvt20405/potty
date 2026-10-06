using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenThuNhanDeTuResult : ScreenBase
{
	public UILabel lbNhanVatName;

	public GameObject nhanVatAvatar3D;

	private GameObject NhanVat3D;

	public GameObject eventGroup;

	public GameObject tanHonGroup;

	public GameObject btnClose;

	public GameObject goFocusAvatar;

	public bool isShowEvent;

	public UILabel lbTanHon;

	public List<NhanVatAvatar> listNhanVat;

	private float nextSecond;

	private int currentFocusAvatar;

	private int countPlayFocus;

	private float timeGap;

	private float maxTimeGap;

	private float icrTimeGap;

	private bool isPlayFocus;

	private int targetIndex;

	public GameObject phanThuongGroup;

	public NhanVatAvatar phanThuongAvatar;

	public UILabel lbPhanThuong;

	private LayDeTuResponse getDeTuResponse;

	public GameObject mAnimNhanVatAt;

	public GameObject mAnimNhanVatGiap;

	public GameObject mAnimNhanVatBinh;

	private void Awake()
	{
		UIAnchor[] componentsInChildren = GetComponentsInChildren<UIAnchor>();
		UIAnchor[] array = componentsInChildren;
		UIAnchor[] array2 = array;
		foreach (UIAnchor uIAnchor in array2)
		{
			if (GUIManager.instance != null)
			{
				uIAnchor.widgetContainer = GUIManager.instance.GameFrame;
			}
		}
		btnClose.gameObject.SetActive(false);
	}

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(-1);
		mAnimNhanVatAt.SetActive(false);
		mAnimNhanVatGiap.SetActive(false);
		mAnimNhanVatBinh.SetActive(false);
	}

	private void Update()
	{
		if (NhanVat3D != null)
		{
			Avatar3D component = NhanVat3D.GetComponent<Avatar3D>();
			if (component.AvatarGO != null && component.AvatarGO.GetComponent<Animation>() != null && component.AvatarGO.GetComponent<Animation>().isPlaying)
			{
				if (component.AvatarGO.GetComponent<Animation>().cullingType != AnimationCullingType.AlwaysAnimate)
				{
					component.AvatarGO.GetComponent<Animation>().cullingType = AnimationCullingType.AlwaysAnimate;
				}
				if (component.AvatarGO.GetComponent<Animation>().IsPlaying("idle"))
				{
					component.AvatarGO.GetComponent<Animation>().wrapMode = WrapMode.Loop;
				}
				else
				{
					component.AvatarGO.GetComponent<Animation>().wrapMode = WrapMode.Default;
				}
			}
		}
		nextSecond += Time.deltaTime;
		if (nextSecond >= 3f)
		{
			if (!isShowEvent && !NGUITools.GetActive(btnClose.gameObject))
			{
				btnClose.gameObject.SetActive(true);
			}
			else if (isShowEvent && !NGUITools.GetActive(eventGroup.gameObject))
			{
				displayEventGroup(true);
				isPlayFocus = true;
			}
		}
		if (!isPlayFocus || !NGUITools.GetActive(eventGroup.gameObject))
		{
			return;
		}
		if (timeGap <= 0f)
		{
			if (currentFocusAvatar == 5)
			{
				currentFocusAvatar = 0;
				if (countPlayFocus < 3)
				{
					countPlayFocus++;
				}
			}
			else
			{
				currentFocusAvatar++;
			}
			timeGap = maxTimeGap + icrTimeGap * (float)countPlayFocus;
			maxTimeGap = timeGap;
			goFocusAvatar.transform.localPosition = listNhanVat[currentFocusAvatar].transform.localPosition;
			if (currentFocusAvatar == targetIndex && countPlayFocus >= 3)
			{
				StartCoroutine(openPopUpPhanThuong(1.5f));
				return;
			}
		}
		else
		{
			timeGap -= Time.deltaTime;
		}
		if (timeGap > 80f)
		{
			resetVar();
		}
	}

	private void resetVar()
	{
		currentFocusAvatar = 0;
		countPlayFocus = 0;
		timeGap = 0.001f;
		maxTimeGap = 0.01f;
		icrTimeGap = 0.0001f;
		isPlayFocus = false;
		isShowEvent = false;
		targetIndex = 0;
	}

	public void displayEventGroup(bool isDisplay)
	{
		eventGroup.gameObject.SetActive(isDisplay);
		goFocusAvatar.gameObject.SetActive(isDisplay);
		btnClose.gameObject.SetActive(!isDisplay);
		phanThuongGroup.gameObject.SetActive(!isDisplay);
	}

	public void setData(LayDeTuResponse response)
	{
		resetVar();
		nextSecond = 0f;
		goFocusAvatar.transform.localPosition = listNhanVat[0].transform.localPosition;
		eventGroup.gameObject.SetActive(false);
		goFocusAvatar.gameObject.SetActive(false);
		btnClose.gameObject.SetActive(false);
		tanHonGroup.gameObject.SetActive(false);
		if (response != null)
		{
			getDeTuResponse = response;
			lbNhanVatName.text = response.NhanVatName;
			if (response.TanHonCount > 0)
			{
				tanHonGroup.gameObject.SetActive(true);
				lbTanHon.text = string.Format(Localization.instance.Get("ThongBaoTanHonThuNhanDeTu"), response.TanHonCount);
			}
			if (response.BonusTanHonName != null && response.BonusTanHonName.Count > 0)
			{
				for (int i = 0; i < listNhanVat.Count; i++)
				{
					if (response.BonusTanHonName[i] != null)
					{
						listNhanVat[i].Set(response.BonusTanHonName[i]);
					}
					else
					{
						listNhanVat[i].Set("empty");
					}
				}
				isShowEvent = true;
				countPlayFocus = 0;
				targetIndex = response.BonusTanHonNhanDuocIdx;
			}
			else
			{
				eventGroup.gameObject.SetActive(false);
				goFocusAvatar.gameObject.SetActive(false);
				isShowEvent = false;
			}
			NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[response.NhanVatName];
			if (nhanVatCfg != null)
			{
				lbNhanVatName.text = nhanVatCfg.TenHienThi;
				displayNhanVat3D(response.NhanVatName, nhanVatCfg);
			}
		}
		phanThuongGroup.gameObject.SetActive(false);
	}

	public IEnumerator openPopUpPhanThuong(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		eventGroup.gameObject.SetActive(false);
		phanThuongGroup.gameObject.SetActive(true);
		string tanHonName = getDeTuResponse.BonusTanHonName[getDeTuResponse.BonusTanHonNhanDuocIdx];
		if (tanHonName != null)
		{
			phanThuongAvatar.Set(tanHonName);
			NhanVatCfg cfg = ConfigManager.instance.m_dicNhanVats[tanHonName];
			if (cfg != null)
			{
				lbPhanThuong.text = string.Format(Localization.instance.Get("ThongBaoThuNhanMess"), getDeTuResponse.BonusTanHonNhanDuocCount, cfg.TenHienThi);
			}
			else
			{
				lbPhanThuong.text = string.Format(Localization.instance.Get("ThongBaoThuNhanMess"), getDeTuResponse.BonusTanHonNhanDuocCount, cfg.TenHienThi);
			}
		}
		btnClose.gameObject.SetActive(true);
		resetVar();
	}

	public void displayNhanVat3D(string nvName, NhanVatCfg cfgData)
	{
		if (NhanVat3D != null)
		{
			Object.Destroy(NhanVat3D);
			NhanVat3D = null;
		}
		EGDebug.Log("NHAN VAT: " + nvName + " VU KHI MAC DINH: " + cfgData.VuKhiMacDinh);
		if (cfgData.VuKhiMacDinh.StartsWith("VK_") && !string.IsNullOrEmpty(cfgData.VuKhiMacDinh))
		{
			NhanVat3D = GUIManager.instance.InstantiateAvatar3DWithBattleAnim(nvName, cfgData.VuKhiMacDinh, string.Empty, string.Empty, string.Empty).gameObject;
		}
		else
		{
			NhanVat3D = GUIManager.instance.InstantiateAvatar3DWithBattleAnim(nvName, string.Empty, string.Empty, string.Empty, string.Empty).gameObject;
		}
		if (NhanVat3D != null)
		{
			NhanVat3D.transform.parent = nhanVatAvatar3D.transform;
			NhanVat3D.transform.localPosition = Vector3.zero;
			NhanVat3D.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			NhanVat3D.transform.localScale = Vector3.one;
			NhanVat3D.GetComponent<Avatar3D>().PlayAnimBattle("change", false);
			NhanVat3D.GetComponent<Avatar3D>().FadeQueuedAnimBattle("market", false, 0.3f);
			NhanVat3D.GetComponent<Avatar3D>().FadeQueuedAnimBattle("idle", false, 0.3f);
		}
		if (cfgData.Hang == 1)
		{
			startPlayAnim(mAnimNhanVatBinh);
		}
		else if (cfgData.Hang == 2)
		{
			startPlayAnim(mAnimNhanVatAt);
		}
		else if (cfgData.Hang == 3)
		{
			startPlayAnim(mAnimNhanVatGiap);
		}
	}

	public void btnClose_OnClick()
	{
		if (TutorialPopup.instance != null)
		{
			TutorialPopup.instance.ShowNextTutorial();
		}
		GUIManager.GoBackLastScreen();
	}

	private void startPlayAnim(GameObject m_anim)
	{
		m_anim.SetActive(true);
		m_anim.GetComponent<ParticleSystem>().Simulate(0f, true, true);
		m_anim.GetComponent<ParticleSystem>().Play();
	}
}
