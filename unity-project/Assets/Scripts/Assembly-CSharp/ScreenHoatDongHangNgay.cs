using UnityEngine;

public class ScreenHoatDongHangNgay : ScreenBase
{
	public HoatDongHangNgayItem BaseItem;

	public Transform ItemRoot;

	public float Size;

	public override void OnActive()
	{
		GUIManager.ShowGadgets(6);
		foreach (Transform item in ItemRoot)
		{
			Transform transform2 = item;
			if (transform2.gameObject != BaseItem.gameObject)
			{
				Object.Destroy(transform2.gameObject);
			}
		}
		BaseItem.gameObject.SetActive(true);
		int num = 0;
		foreach (OtherCfg.HoatDongDailyCfg.HoatDongDaily item2 in ConfigManager.instance.OtherConfig.HoatDongDailyConfig.ListHoatDong)
		{
			HoatDongHangNgayItem hoatDongHangNgayItem = Object.Instantiate(BaseItem) as HoatDongHangNgayItem;
			hoatDongHangNgayItem.transform.parent = ItemRoot;
			hoatDongHangNgayItem.transform.localPosition = BaseItem.transform.localPosition + (float)num * Size * Vector3.down;
			hoatDongHangNgayItem.transform.localScale = Vector3.one;
			hoatDongHangNgayItem.Set(item2);
			num++;
		}
		BaseItem.gameObject.SetActive(false);
	}
}
