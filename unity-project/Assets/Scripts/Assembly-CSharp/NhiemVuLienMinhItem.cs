using UnityEngine;

public class NhiemVuLienMinhItem : MonoBehaviour
{
	public int NhiemVuNo;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void OnFinishNhiemVu()
	{
		FinishNhiemVuLienMinhRequest finishNhiemVuLienMinhRequest = new FinishNhiemVuLienMinhRequest();
		finishNhiemVuLienMinhRequest.BoQua = false;
		finishNhiemVuLienMinhRequest.NhiemVuNo = NhiemVuNo;
		GameManager.instance.m_GameClient.RequestFinishNhiemVuLienMinh(finishNhiemVuLienMinhRequest);
	}

	public void OnBoQuaNhiemVu()
	{
		FinishNhiemVuLienMinhRequest finishNhiemVuLienMinhRequest = new FinishNhiemVuLienMinhRequest();
		finishNhiemVuLienMinhRequest.BoQua = true;
		finishNhiemVuLienMinhRequest.NhiemVuNo = NhiemVuNo;
		GameManager.instance.m_GameClient.RequestFinishNhiemVuLienMinh(finishNhiemVuLienMinhRequest);
	}
}
