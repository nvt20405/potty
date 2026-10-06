using System.Collections.Generic;
using UnityEngine;

public class PopupSonMonXayDungItem : MonoBehaviour
{
	public UISprite Avatar;

	public UILabel CTNameLabel;

	public UILabel CostLabel;

	private UserInfo.SonMonBuildingInfo.SonMonBuildingType LoaiCT;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Set(UserInfo.SonMonBuildingInfo.SonMonBuildingType codename)
	{
		Avatar.spriteName = codename.ToString();
		CTNameLabel.text = ConfigManager.instance.SonMonConfig.CongTrinhCfg[codename.ToString()][0].DisplayName;
		CostLabel.text = string.Empty;
		foreach (KeyValuePair<string, int> item in ConfigManager.instance.SonMonConfig.CongTrinhCfg[codename.ToString()][0].BuildCost)
		{
			if (item.Key == "BAC")
			{
				UILabel costLabel = CostLabel;
				string text = costLabel.text;
				costLabel.text = text + item.Value + " " + Localization.instance.Get("Bac");
			}
			else if (item.Key != "CHINH_SANH")
			{
				UILabel costLabel2 = CostLabel;
				string text2 = costLabel2.text;
				costLabel2.text = text2 + item.Value + " " + ConfigManager.instance.m_dicVatPhamTieuThu[item.Key].TenHienThi + " ";
			}
		}
		LoaiCT = codename;
	}

	public void OnActivate(bool isSelected)
	{
		if (isSelected)
		{
			base.transform.parent.parent.GetComponent<PopupSonMonXayDung>().SelectedCT = LoaiCT;
		}
	}
}
