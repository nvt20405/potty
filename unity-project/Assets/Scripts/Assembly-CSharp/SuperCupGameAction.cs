using UnityEngine;

public class SuperCupGameAction : MonoBehaviour
{
	public GameObject btnDatCuoc1;

	public GameObject btnDatCuoc2;

	public GameObject btnTranDau;

	public GameObject grpCuoc1;

	public GameObject grpCuoc2;

	public GameObject btnSubmit1;

	public GameObject btnSubmit2;

	public int matchID;

	public int idx;

	public string name1;

	public string name2;

	private void OnBtnSubmitClick()
	{
		GameManager.instance.m_GameClient.RequestSubmitDoiHinhSieuCup();
	}

	private void OnBtnDatCuoc1Click()
	{
		PopupYesNo.Create(string.Format(Localization.instance.Get("DatCuocConfirmMsg"), ConfigManager.instance.OtherConfig.CostDatCuocLienDau, name1), Localization.instance.Get("DanhNhanhConfirmBtn"), Localization.instance.Get("DanhNhanhDeniedBtn"), OnDatCuoc1, null);
	}

	private void OnBtnDatCuoc2Click()
	{
		PopupYesNo.Create(string.Format(Localization.instance.Get("DatCuocConfirmMsg"), ConfigManager.instance.OtherConfig.CostDatCuocLienDau, name2), Localization.instance.Get("DanhNhanhConfirmBtn"), Localization.instance.Get("DanhNhanhDeniedBtn"), OnDatCuoc2, null);
	}

	private void OnDatCuoc2()
	{
		GameManager.instance.m_GameClient.RequestSieuCupDatCuoc(matchID, 2);
	}

	private void OnDatCuoc1()
	{
		GameManager.instance.m_GameClient.RequestSieuCupDatCuoc(matchID, 1);
	}

	private void OnBtnTranDauClick()
	{
		GameManager.instance.m_GameClient.RequestGetSieuCupBattle(matchID);
	}
}
