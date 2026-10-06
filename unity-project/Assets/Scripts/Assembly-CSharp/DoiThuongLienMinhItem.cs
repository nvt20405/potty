using UnityEngine;

public class DoiThuongLienMinhItem : MonoBehaviour
{
	public OtherAvatar item;

	public NhanVatAvatar nhanvat;

	public string DoiThuongCfg;

	public static string DoiThuongSelected;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnDoiThuong()
	{
		DoiThuongSelected = DoiThuongCfg;
		PopupYesNo.Create(Localization.instance.Get("DoiThuongLienMinhConfirmDesc"), Localization.instance.Get("DongYLabelBtn"), Localization.instance.Get("TuChoiLabelBtn"), OnDoiThuongConfirm, null);
	}

	public void OnDoiThuongConfirm()
	{
		DoiThuongLienMinhRequest doiThuongLienMinhRequest = new DoiThuongLienMinhRequest();
		doiThuongLienMinhRequest.DoiThuongCodename = DoiThuongSelected;
		GameManager.instance.m_GameClient.RequestDoiThuongLienMinh(doiThuongLienMinhRequest);
	}
}
