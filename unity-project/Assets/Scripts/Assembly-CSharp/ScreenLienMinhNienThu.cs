using UnityEngine;

public class ScreenLienMinhNienThu : ScreenBase
{
	public GameObject GeneralView;

	public LienMinhNienThuScreenItem BaseItem;

	public GameObject ItemRoot;

	public float ItemSize;

	public UILabel ThanVuongLenhLabel;

	public GameObject BtnSummonNienThu;

	public GameObject BtnJoinNienThu;

	public UISlider Slider;

	public void OnEnable()
	{
		GUIManager.ShowGadgets(2);
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			if (transform2.gameObject != BaseItem.gameObject && transform2.gameObject != GeneralView)
			{
				Object.Destroy(transform2.gameObject);
			}
		}
		ThanVuongLenhLabel.text = string.Format(Localization.instance.Get("ThanVuongLenhCount"), GameManager.instance.m_GameClient.UserInfo.LienMinh.NienThuItem);
		int num = 0;
		foreach (LienMinhThanhVienData thanhVien in GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList)
		{
			BaseItem.gameObject.SetActive(true);
			LienMinhNienThuScreenItem lienMinhNienThuScreenItem = Object.Instantiate(BaseItem) as LienMinhNienThuScreenItem;
			lienMinhNienThuScreenItem.transform.parent = ItemRoot.transform;
			lienMinhNienThuScreenItem.transform.localPosition = (float)num * ItemSize * Vector3.down + BaseItem.transform.localPosition;
			lienMinhNienThuScreenItem.transform.localScale = Vector3.one;
			lienMinhNienThuScreenItem.Set(thanhVien.DisplayName, thanhVien.NienThuItem);
			BaseItem.gameObject.SetActive(false);
			num++;
		}
		Slider.sliderValue = (float)GameManager.instance.m_GameClient.UserInfo.LienMinh.NienThuItem / 3500f;
	}

	public void OnGetTopLienMinh()
	{
		GameManager.instance.m_GameClient.RequestGetTopNienThu();
	}

	public void OnSummonNienThu()
	{
		if (GameManager.instance.m_GameClient.UserInfo.LienMinh.MinhChuID == GameManager.instance.m_GameClient.UserInfo.Gamer.ID)
		{
			GameManager.instance.m_GameClient.RequestSummonNienThu();
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("LienMinhKhongDuQuyenHan"));
		}
	}

	public void OnJoinNienThu()
	{
		GameManager.instance.m_GameClient.RequestThamGiaNienThu();
	}
}
