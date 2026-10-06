using System.Collections.Generic;
using UnityEngine;

public class ScreenQuangMinhDinh : ScreenBase
{
	public GameObject SelectGroup;

	public GameObject CongPhaGroup;

	public GameObject BeQuanGroup;

	public GameObject TranHinhGroup;

	public NhanVatAvatar NV1;

	public NhanVatAvatar NV2;

	public NhanVatAvatar NV3;

	public NhanVatAvatar NV4;

	public QMDSkillNVGroup NV1SKill;

	public QMDSkillNVGroup NV2SKill;

	public QMDSkillNVGroup NV3SKill;

	public QMDSkillNVGroup NV4SKill;

	public GameObject NV3DRoot;

	public Avatar3D NV3DAvatar;

	public UILabel npcLabel;

	public QMDInfo m_Current_Info;

	public GameObject BeQuanItemRoot;

	public GameObject BeQuanItemPrefab;

	public UILabel BeQuanTTDLabel;

	public bool BackToXongPha;

	public void OnTranHinhClick()
	{
		PopupQMDTranHinh.Create(false, m_Current_Info);
	}

	public void OnChienThuatClick()
	{
		PopupQMDChienThuat.Create(m_Current_Info);
	}

	public void OnXongPhaSelect(int select)
	{
		string choice = m_Current_Info.NextChoice1;
		switch (select)
		{
		case 2:
			choice = m_Current_Info.NextChoice2;
			break;
		case 3:
			choice = m_Current_Info.NextChoice3;
			break;
		}
		QMDSelectRequest qMDSelectRequest = new QMDSelectRequest();
		qMDSelectRequest.Choice = choice;
		GameManager.instance.m_GameClient.RequestQMDSelect(qMDSelectRequest);
	}

	private void Awake()
	{
		UIAnchor[] componentsInChildren = GetComponentsInChildren<UIAnchor>();
		UIAnchor[] array = componentsInChildren;
		UIAnchor[] array2 = array;
		foreach (UIAnchor uIAnchor in array2)
		{
			uIAnchor.widgetContainer = GUIManager.instance.GameFrame;
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void OnNVClick()
	{
		if (m_Current_Info.NextChoice1.StartsWith("NV_"))
		{
			PopupSelectNVQMD.Create(m_Current_Info);
		}
	}

	public void OnDanh()
	{
		ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
		if (screenBattle != null)
		{
			screenBattle.screenBackAfterBattle = GAME_SCREEN.ScreenQuangMinhDinh;
		}
		GameManager.instance.m_GameClient.RequestQMDXongPha();
	}

	public void OnBXH()
	{
		GameManager.instance.m_GameClient.RequestQMDGetBXH();
	}

	public void OnChiTiet()
	{
		GameManager.instance.m_GameClient.RequestQMDGetChiTietNPC();
	}

	public void OnVoCongClick()
	{
		if (!m_Current_Info.NextChoice1.StartsWith("NV_"))
		{
			PopupSelectVoCongQMD.Create(m_Current_Info);
		}
	}

	public void OnHelpCongPha()
	{
		ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
		screenHelpInfo.setByLevel(6, 5);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
	}

	public void OnHelpBeQuan()
	{
		ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
		screenHelpInfo.setByLevel(0, 7);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
	}

	public void SyncBequanWithNetworkData()
	{
		int num = 0;
		UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_TAY_TUY_DAN");
		if (vatPhamTieuThuData != null)
		{
			num = vatPhamTieuThuData.Quantity;
		}
		BeQuanTTDLabel.text = string.Format(Localization.instance.Get("QMDBeQuanTTDHienCo"), num);
		NGUITools.SetActive(SelectGroup, false);
		NGUITools.SetActive(CongPhaGroup, false);
		NGUITools.SetActive(BeQuanGroup, true);
		foreach (Transform item in BeQuanItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 280f, 0f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, -165f, 0f);
		List<UserInfo.HeroData> list = new List<UserInfo.HeroData>();
		int num2 = 0;
		foreach (UserInfo.HeroData hero in GameManager.instance.m_GameClient.UserInfo.HeroList)
		{
			if (hero.BeQuan == 0 && hero.Level > 100)
			{
				NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[hero.Name];
				if (nhanVatCfg.Hang >= 3)
				{
					list.Add(hero);
				}
			}
		}
		list.Sort((UserInfo.HeroData x, UserInfo.HeroData y) => ConfigManager.instance.CompareNhanVat(x.Name, x.Level, y.Name, y.Level));
		foreach (UserInfo.HeroData item2 in list)
		{
			DeTuBeQuanItem component = ((GameObject)Object.Instantiate(BeQuanItemPrefab)).GetComponent<DeTuBeQuanItem>();
			component.transform.parent = BeQuanItemRoot.transform;
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			component.transform.localPosition = vector;
			vector += vector2;
			component.Set(item2);
		}
	}

	public void SyncCongPhaWithNetworkData(QMDInfoResponse response)
	{
		PopupQMDTranHinh.DestroyPopup();
		PopupSelectNVQMD.DestroyPopup();
		PopupSelectVoCongQMD.DestroyPopup();
		PopupQMDChienThuat.DestroyPopup();
		m_Current_Info = response.info;
		NGUITools.SetActive(SelectGroup, false);
		NGUITools.SetActive(CongPhaGroup, true);
		NGUITools.SetActive(BeQuanGroup, false);
		NGUITools.SetActive(TranHinhGroup, false);
		NGUITools.SetActive(NV1.gameObject, false);
		NGUITools.SetActive(NV2.gameObject, false);
		NGUITools.SetActive(NV3.gameObject, false);
		NGUITools.SetActive(NV4.gameObject, false);
		NGUITools.SetActive(NV1SKill.gameObject, false);
		NGUITools.SetActive(NV2SKill.gameObject, false);
		NGUITools.SetActive(NV3SKill.gameObject, false);
		NGUITools.SetActive(NV4SKill.gameObject, false);
		QMDInfo.QMDXongPhaState state = response.info.State;
		npcLabel.text = string.Format(Localization.instance.Get("QMDNPCLabel"), m_Current_Info.NPCAi);
		if (NV3DAvatar != null && NV3DAvatar.CodeName != m_Current_Info.NPCName)
		{
			Object.Destroy(NV3DAvatar);
			NV3DAvatar = null;
		}
		if (NV3DAvatar == null)
		{
			Avatar3D nV3DAvatar = GUIManager.instance.InstantiateAvatar3DWithBattleAnim(m_Current_Info.NPCName, string.Empty, string.Empty, string.Empty, string.Empty);
			NV3DAvatar = nV3DAvatar;
		}
		if (NV3DAvatar != null)
		{
			NV3DAvatar.transform.parent = NV3DRoot.transform;
			NV3DAvatar.transform.localPosition = Vector3.zero;
			NV3DAvatar.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			NV3DAvatar.transform.localScale = Vector3.one;
			NV3DAvatar.PlayAnimBattle("idle", true);
		}
		if (state >= QMDInfo.QMDXongPhaState.SELECT_NV1)
		{
			NGUITools.SetActive(NV1.gameObject, true);
			SetNhanVatAvatar(NV1, response.info.NV1.Name);
		}
		if (state >= QMDInfo.QMDXongPhaState.SELECT_NV1_SKILL1)
		{
			NGUITools.SetActive(NV1SKill.gameObject, true);
			NGUITools.SetActive(NV1SKill.Skill1.gameObject, true);
			NGUITools.SetActive(NV1SKill.Skill2.gameObject, false);
			NGUITools.SetActive(NV1SKill.Skill3.gameObject, false);
			SetVoCongAvatar(NV1SKill.Skill1, response.info.NV1.Skill1);
		}
		if (state >= QMDInfo.QMDXongPhaState.SELECT_NV1_SKILL2)
		{
			NGUITools.SetActive(NV1SKill.Skill2.gameObject, true);
			SetVoCongAvatar(NV1SKill.Skill2, response.info.NV1.Skill2);
		}
		if (state >= QMDInfo.QMDXongPhaState.SELECT_NV1_SKILL3)
		{
			NGUITools.SetActive(NV1SKill.Skill3.gameObject, true);
			SetVoCongAvatar(NV1SKill.Skill3, response.info.NV1.Skill3);
		}
		if (state >= QMDInfo.QMDXongPhaState.SELECT_NV2)
		{
			NGUITools.SetActive(NV2.gameObject, true);
			SetNhanVatAvatar(NV2, response.info.NV2.Name);
		}
		if (state >= QMDInfo.QMDXongPhaState.SELECT_NV2_SKILL1)
		{
			NGUITools.SetActive(NV2SKill.gameObject, true);
			NGUITools.SetActive(NV2SKill.Skill1.gameObject, true);
			NGUITools.SetActive(NV2SKill.Skill2.gameObject, false);
			NGUITools.SetActive(NV2SKill.Skill3.gameObject, false);
			SetVoCongAvatar(NV2SKill.Skill1, response.info.NV2.Skill1);
		}
		if (state >= QMDInfo.QMDXongPhaState.SELECT_NV2_SKILL2)
		{
			NGUITools.SetActive(NV2SKill.Skill2.gameObject, true);
			SetVoCongAvatar(NV2SKill.Skill2, response.info.NV2.Skill2);
		}
		if (state >= QMDInfo.QMDXongPhaState.SELECT_NV2_SKILL3)
		{
			NGUITools.SetActive(NV2SKill.Skill3.gameObject, true);
			SetVoCongAvatar(NV2SKill.Skill3, response.info.NV2.Skill3);
		}
		if (state >= QMDInfo.QMDXongPhaState.SELECT_NV3)
		{
			NGUITools.SetActive(NV3.gameObject, true);
			SetNhanVatAvatar(NV3, response.info.NV3.Name);
		}
		if (state >= QMDInfo.QMDXongPhaState.SELECT_NV3_SKILL1)
		{
			NGUITools.SetActive(NV3SKill.gameObject, true);
			NGUITools.SetActive(NV3SKill.Skill1.gameObject, true);
			NGUITools.SetActive(NV3SKill.Skill2.gameObject, false);
			NGUITools.SetActive(NV3SKill.Skill3.gameObject, false);
			SetVoCongAvatar(NV3SKill.Skill1, response.info.NV3.Skill1);
		}
		if (state >= QMDInfo.QMDXongPhaState.SELECT_NV3_SKILL2)
		{
			NGUITools.SetActive(NV3SKill.Skill2.gameObject, true);
			SetVoCongAvatar(NV3SKill.Skill2, response.info.NV3.Skill2);
		}
		if (state >= QMDInfo.QMDXongPhaState.SELECT_NV3_SKILL3)
		{
			NGUITools.SetActive(NV3SKill.Skill3.gameObject, true);
			SetVoCongAvatar(NV3SKill.Skill3, response.info.NV3.Skill3);
		}
		if (state >= QMDInfo.QMDXongPhaState.SELECT_NV4)
		{
			NGUITools.SetActive(NV4.gameObject, true);
			SetNhanVatAvatar(NV4, response.info.NV4.Name);
		}
		if (state >= QMDInfo.QMDXongPhaState.SELECT_NV4_SKILL1)
		{
			NGUITools.SetActive(NV4SKill.gameObject, true);
			NGUITools.SetActive(NV4SKill.Skill1.gameObject, true);
			NGUITools.SetActive(NV4SKill.Skill2.gameObject, false);
			NGUITools.SetActive(NV4SKill.Skill3.gameObject, false);
			SetVoCongAvatar(NV4SKill.Skill1, response.info.NV4.Skill1);
		}
		if (state >= QMDInfo.QMDXongPhaState.SELECT_NV4_SKILL2)
		{
			NGUITools.SetActive(NV4SKill.Skill2.gameObject, true);
			SetVoCongAvatar(NV4SKill.Skill2, response.info.NV4.Skill2);
		}
		if (state >= QMDInfo.QMDXongPhaState.SELECT_NV4_SKILL3)
		{
			NGUITools.SetActive(NV4SKill.Skill3.gameObject, true);
			SetVoCongAvatar(NV4SKill.Skill3, response.info.NV4.Skill3);
		}
		if (state == QMDInfo.QMDXongPhaState.FINISH)
		{
			NGUITools.SetActive(TranHinhGroup, true);
		}
	}

	public void SetVoCongAvatar(OtherAvatar avatar, string name)
	{
		if (name == string.Empty)
		{
			avatar.Set("plus");
		}
		else
		{
			avatar.Set(name);
		}
	}

	public void SetNhanVatAvatar(NhanVatAvatar avatar, string name)
	{
		if (name == string.Empty)
		{
			avatar.Set("plus");
		}
		else
		{
			avatar.Set(name);
		}
	}

	public void OnSelectCongPha()
	{
		EGDebug.Log("Xong pha");
		GameManager.instance.m_GameClient.RequestGetQMDInfo();
	}

	public void OnSelectBeQuan()
	{
		SyncBequanWithNetworkData();
	}

	public override void OnDeactive()
	{
		base.OnDeactive();
	}

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(2);
		if (BackToXongPha)
		{
			BackToXongPha = false;
			GameManager.instance.m_GameClient.RequestGetQMDInfo();
		}
		else
		{
			NGUITools.SetActive(SelectGroup, true);
			NGUITools.SetActive(CongPhaGroup, false);
			NGUITools.SetActive(BeQuanGroup, false);
		}
	}

	public void OnCloseBattleResult()
	{
		if (PopupDanhSachPhanThuong.instance != null)
		{
			PopupDanhSachPhanThuong.instance.gameObject.SetActive(true);
		}
		GUIManager.setScreen(GAME_SCREEN.ScreenQuangMinhDinh);
	}
}
