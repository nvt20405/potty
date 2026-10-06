using System.Collections.Generic;
using UnityEngine;

public class ScreenSelectNhanVatFullItem : ScreenBase
{
	public UILabel lbNhanVatName;

	public UILabel lbDescription;

	public GameObject nhanVatAvatar3D;

	private GameObject NhanVat3D;

	public GameObject mAnimNhanVatGiap;

	private string CodeNameNV = string.Empty;

	private List<string> listCodeNameNV = new List<string>();

	private int CurIndex = -1;

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
	}

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(-1);
		mAnimNhanVatGiap.SetActive(false);
		displayInfo();
	}

	private void Update()
	{
		if (!(NhanVat3D != null))
		{
			return;
		}
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

	public void displayInfo()
	{
		if (listCodeNameNV != null && listCodeNameNV.Count > 0 && CurIndex >= 0 && CurIndex < listCodeNameNV.Count)
		{
			CodeNameNV = listCodeNameNV[CurIndex];
			NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[CodeNameNV];
			if (nhanVatCfg != null)
			{
				lbNhanVatName.text = nhanVatCfg.TenHienThi;
				lbDescription.text = Localization.instance.Get("SelectFirstDeTuDesc" + (CurIndex + 1));
				displayNhanVat3D(CodeNameNV, nhanVatCfg);
			}
		}
	}

	public void setData(List<string> listStrCodeName, int index)
	{
		listCodeNameNV = listStrCodeName;
		CurIndex = index;
	}

	public void displayNhanVat3D(string nvName, NhanVatCfg cfgData)
	{
		if (NhanVat3D != null)
		{
			Object.Destroy(NhanVat3D);
			NhanVat3D = null;
		}
		NhanVat3D = GUIManager.instance.InstantiateAvatar3DWithBattleAnim(nvName, cfgData.VuKhiMacDinh, string.Empty, string.Empty, string.Empty, true).gameObject;
		if (NhanVat3D != null)
		{
			NhanVat3D.transform.parent = nhanVatAvatar3D.transform;
			NhanVat3D.transform.localPosition = Vector3.zero;
			NhanVat3D.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			NhanVat3D.transform.localScale = Vector3.one;
			NhanVat3D.GetComponent<Avatar3D>().FadeQueuedAnimBattle(cfgData.VoCongMacDinh.ToString(), false, 0.3f);
			NhanVat3D.GetComponent<Avatar3D>().FadeQueuedAnimBattle("idle", true, 0.3f);
		}
		startPlayAnim(mAnimNhanVatGiap);
	}

	public void btnClose_OnClick()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenSelectFirstDeTu);
	}

	public void btnChon_OnClick()
	{
		SelectStartDeTuRequest selectStartDeTuRequest = new SelectStartDeTuRequest();
		selectStartDeTuRequest.Name = CodeNameNV;
		GameManager.instance.m_GameClient.RequestSelectStartDeTu(selectStartDeTuRequest);
	}

	private void startPlayAnim(GameObject m_anim)
	{
		m_anim.SetActive(true);
		m_anim.GetComponent<ParticleSystem>().Simulate(0f, true, true);
		m_anim.GetComponent<ParticleSystem>().Play();
	}

	public void NextButton_OnClick()
	{
		if (CurIndex < listCodeNameNV.Count - 1)
		{
			CurIndex++;
		}
		else if (CurIndex == listCodeNameNV.Count - 1)
		{
			CurIndex = 0;
		}
		CodeNameNV = listCodeNameNV[CurIndex];
		displayInfo();
	}

	public void PrevButton_OnClick()
	{
		if (CurIndex > 0)
		{
			CurIndex--;
		}
		else if (CurIndex == 0)
		{
			CurIndex = listCodeNameNV.Count - 1;
		}
		CodeNameNV = listCodeNameNV[CurIndex];
		displayInfo();
	}
}
