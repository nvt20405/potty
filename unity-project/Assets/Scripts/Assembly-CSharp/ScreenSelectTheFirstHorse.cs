using UnityEngine;

public class ScreenSelectTheFirstHorse : ScreenBase
{
	public GameObject horse1;

	public GameObject horse2;

	public GameObject horse3;

	public GameObject Horse1_3D;

	public GameObject Horse2_3D;

	public GameObject Horse3_3D;

	private GameObject avatar1;

	private GameObject avatar2;

	private GameObject avatar3;

	public UILabel lbName1;

	public UILabel lbDescription1;

	public UILabel lbName2;

	public UILabel lbDescription2;

	public UILabel lbName3;

	public UILabel lbDescription3;

	private string horse1CodeName = "NGUA_DOAN_VIEN";

	private string horse2CodeName = "NGUA_TAN_XUAN";

	private string horse3CodeName = "NGUA_DAI_CAT";

	private void Start()
	{
		UIEventListener.Get(horse1.gameObject).onClick = onClick_Horse1;
		UIEventListener.Get(horse2.gameObject).onClick = onClick_Horse2;
		UIEventListener.Get(horse3.gameObject).onClick = onClick_Horse3;
	}

	public override void OnActive()
	{
		base.OnActive();
		displayInfo();
	}

	public void displayInfo()
	{
		OtherCfg.ThuCuoiCfg thuCuoiCfg = ConfigManager.instance.OtherConfig.ThuCuoiConfig[horse1CodeName];
		OtherCfg.ThuCuoiCfg thuCuoiCfg2 = ConfigManager.instance.OtherConfig.ThuCuoiConfig[horse2CodeName];
		OtherCfg.ThuCuoiCfg thuCuoiCfg3 = ConfigManager.instance.OtherConfig.ThuCuoiConfig[horse3CodeName];
		lbName1.text = ((thuCuoiCfg.DisplayName == null) ? string.Empty : thuCuoiCfg.DisplayName);
		lbName2.text = ((thuCuoiCfg2.DisplayName == null) ? string.Empty : thuCuoiCfg2.DisplayName);
		lbName3.text = ((thuCuoiCfg3.DisplayName == null) ? string.Empty : thuCuoiCfg3.DisplayName);
		lbDescription1.text = ((thuCuoiCfg.MoTa == null) ? string.Empty : thuCuoiCfg.MoTa);
		lbDescription2.text = ((thuCuoiCfg2.MoTa == null) ? string.Empty : thuCuoiCfg2.MoTa);
		lbDescription3.text = ((thuCuoiCfg3.MoTa == null) ? string.Empty : thuCuoiCfg3.MoTa);
	}

	public void onClick_Horse1(GameObject go)
	{
		SelectStartNguaRequest selectStartNguaRequest = new SelectStartNguaRequest();
		selectStartNguaRequest.Name = horse1CodeName;
		GameManager.instance.m_GameClient.RequestSelectStartNgua(selectStartNguaRequest);
	}

	public void onClick_Horse2(GameObject go)
	{
		SelectStartNguaRequest selectStartNguaRequest = new SelectStartNguaRequest();
		selectStartNguaRequest.Name = horse2CodeName;
		GameManager.instance.m_GameClient.RequestSelectStartNgua(selectStartNguaRequest);
	}

	public void onClick_Horse3(GameObject go)
	{
		SelectStartNguaRequest selectStartNguaRequest = new SelectStartNguaRequest();
		selectStartNguaRequest.Name = horse3CodeName;
		GameManager.instance.m_GameClient.RequestSelectStartNgua(selectStartNguaRequest);
	}
}
