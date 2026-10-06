using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ScreenSelectFirstDeTu : ScreenBase
{
	public GameObject SelectDeTuGroup;

	public GameObject[] scenes;

	public float[] scenesTime;

	public int CurrentIntroIdx;

	public float CurrentSceneTime;

	public GameObject prefabNhanVat3D;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<string> listDetu = new List<string>();

	public UILabel lbName1;

	public UILabel lbName2;

	public UILabel lbName3;

	public UILabel lbName4;

	public UILabel lbName5;

	public GameObject Nv1_3D;

	public GameObject Nv2_3D;

	public GameObject Nv3_3D;

	public GameObject Nv4_3D;

	public GameObject Nv5_3D;

	public GameObject Nv1_Particle;

	public GameObject Nv2_Particle;

	public GameObject Nv3_Particle;

	public GameObject Nv4_Particle;

	public GameObject Nv5_Particle;

	private NhanVatCfg cfg1;

	private NhanVatCfg cfg2;

	private NhanVatCfg cfg3;

	private NhanVatCfg cfg4;

	private NhanVatCfg cfg5;

	private Avatar3D avatar1;

	private Avatar3D avatar2;

	private Avatar3D avatar3;

	private Avatar3D avatar4;

	private Avatar3D avatar5;

	private List<string> listCodeNameNV = new List<string>();

	private void Start()
	{
	}

	public override void OnActive()
	{
		base.OnActive();
		listCodeNameNV.Clear();
		listCodeNameNV.Add("NV_DOC_CO_CAU_BAI");
		listCodeNameNV.Add("NV_DIEP_CO_THANH");
		listCodeNameNV.Add("NV_TRUONG_TAM_PHONG");
		listCodeNameNV.Add("NV_LY_TAM_HOAN");
		listCodeNameNV.Add("NV_VUONG_NGU_YEN");
		GameManager.instance.isStartTutorial = true;
		TurnOnSelectDeTu();
		if (avatar1 != null)
		{
			avatar1.PlayAnimBattle("idle", true);
		}
		if (avatar2 != null)
		{
			avatar2.PlayAnimBattle("idle", true);
		}
		if (avatar3 != null)
		{
			avatar3.PlayAnimBattle("idle", true);
		}
		if (avatar4 != null)
		{
			avatar4.PlayAnimBattle("idle", true);
		}
		if (avatar5 != null)
		{
			avatar5.PlayAnimBattle("idle", true);
		}
	}

	public void BackFromBattle()
	{
		CurrentSceneTime = Time.time + scenesTime[CurrentIntroIdx];
	}

	public void TurnOnSelectDeTu()
	{
		if (listCodeNameNV == null || (listCodeNameNV != null && listCodeNameNV.Count == 0))
		{
			return;
		}
		if (listCodeNameNV[0] != null)
		{
			cfg1 = ConfigManager.instance.m_dicNhanVats[listCodeNameNV[0]];
			lbName1.text = cfg1.TenHienThi.Replace(" ", "\n");
			if (avatar1 != null)
			{
				Object.Destroy(avatar1.gameObject);
				avatar1 = null;
			}
			avatar1 = GUIManager.instance.InstantiateAvatar3DWithBattleAnim(cfg1.Name, cfg1.VuKhiMacDinh, string.Empty, string.Empty, string.Empty);
			if (avatar1 != null)
			{
				avatar1.transform.parent = Nv1_3D.transform;
				avatar1.transform.localPosition = Vector3.zero;
				avatar1.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
				avatar1.transform.localScale = Vector3.one;
			}
			Nv1_Particle.gameObject.SetActive(true);
		}
		if (listCodeNameNV[1] != null)
		{
			cfg2 = ConfigManager.instance.m_dicNhanVats[listCodeNameNV[1]];
			lbName2.text = cfg2.TenHienThi.Replace(" ", "\n");
			if (avatar2 != null)
			{
				Object.Destroy(avatar2.gameObject);
				avatar2 = null;
			}
			avatar2 = GUIManager.instance.InstantiateAvatar3DWithBattleAnim(cfg2.Name, cfg2.VuKhiMacDinh, string.Empty, string.Empty, string.Empty);
			if (avatar2 != null)
			{
				avatar2.transform.parent = Nv2_3D.transform;
				avatar2.transform.localPosition = Vector3.zero;
				avatar2.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
				avatar2.transform.localScale = Vector3.one;
			}
			Nv2_Particle.gameObject.SetActive(true);
		}
		if (listCodeNameNV[2] != null)
		{
			cfg3 = ConfigManager.instance.m_dicNhanVats[listCodeNameNV[2]];
			lbName3.text = cfg3.TenHienThi.Replace(" ", "\n");
			if (avatar3 != null)
			{
				Object.Destroy(avatar3.gameObject);
				avatar3 = null;
			}
			avatar3 = GUIManager.instance.InstantiateAvatar3DWithBattleAnim(cfg3.Name, cfg3.VuKhiMacDinh, string.Empty, string.Empty, string.Empty);
			if (avatar3 != null)
			{
				avatar3.transform.parent = Nv3_3D.transform;
				avatar3.transform.localPosition = Vector3.zero;
				avatar3.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
				avatar3.transform.localScale = Vector3.one;
			}
			Nv3_Particle.gameObject.SetActive(true);
		}
		if (listCodeNameNV[3] != null)
		{
			cfg4 = ConfigManager.instance.m_dicNhanVats[listCodeNameNV[3]];
			lbName4.text = cfg4.TenHienThi.Replace(" ", "\n");
			if (avatar4 != null)
			{
				Object.Destroy(avatar4.gameObject);
				avatar4 = null;
			}
			avatar4 = GUIManager.instance.InstantiateAvatar3DWithBattleAnim(cfg4.Name, cfg4.VuKhiMacDinh, string.Empty, string.Empty, string.Empty);
			if (avatar4 != null)
			{
				avatar4.transform.parent = Nv4_3D.transform;
				avatar4.transform.localPosition = Vector3.zero;
				avatar4.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
				avatar4.transform.localScale = Vector3.one;
			}
			Nv4_Particle.gameObject.SetActive(true);
		}
		if (listCodeNameNV[4] != null)
		{
			cfg5 = ConfigManager.instance.m_dicNhanVats[listCodeNameNV[4]];
			lbName5.text = cfg5.TenHienThi.Replace(" ", "\n");
			if (avatar5 != null)
			{
				Object.Destroy(avatar5.gameObject);
				avatar5 = null;
			}
			avatar5 = GUIManager.instance.InstantiateAvatar3DWithBattleAnim(cfg5.Name, cfg5.VuKhiMacDinh, string.Empty, string.Empty, string.Empty);
			if (avatar5 != null)
			{
				avatar5.transform.parent = Nv5_3D.transform;
				avatar5.transform.localPosition = Vector3.zero;
				avatar5.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
				avatar5.transform.localScale = Vector3.one;
			}
			Nv5_Particle.gameObject.SetActive(true);
		}
		SelectDeTuGroup.gameObject.SetActive(true);
		StartCoroutine(DeLayPlayAnim(0.1f));
	}

	private IEnumerator DeLayPlayAnim(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		if (avatar1 != null)
		{
			avatar1.PlayAnimBattle("idle", true);
		}
		if (avatar2 != null)
		{
			avatar2.PlayAnimBattle("idle", true);
		}
		if (avatar3 != null)
		{
			avatar3.PlayAnimBattle("idle", true);
		}
		if (avatar4 != null)
		{
			avatar4.PlayAnimBattle("idle", true);
		}
		if (avatar5 != null)
		{
			avatar5.PlayAnimBattle("idle", true);
		}
	}

	public void onClick_NV1()
	{
		displayFullNV(0);
	}

	public void onClick_NV2()
	{
		displayFullNV(1);
	}

	public void onClick_NV3()
	{
		displayFullNV(2);
	}

	public void onClick_NV4()
	{
		displayFullNV(3);
	}

	public void onClick_NV5()
	{
		displayFullNV(4);
	}

	private void displayFullNV(int index)
	{
		if (listCodeNameNV != null)
		{
			ScreenSelectNhanVatFullItem screenSelectNhanVatFullItem = GUIManager.getScreen(GAME_SCREEN.ScreenSelectNhanVatFullItem) as ScreenSelectNhanVatFullItem;
			screenSelectNhanVatFullItem.setData(listCodeNameNV, index);
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenSelectNhanVatFullItem);
		}
	}

	public override void OnDeactive()
	{
		base.OnDeactive();
		if (PlayerPrefs.GetInt("sounds_value", 1) == 1)
		{
			AudioListener.pause = false;
		}
	}
}
