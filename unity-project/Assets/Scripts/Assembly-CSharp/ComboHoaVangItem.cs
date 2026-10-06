using UnityEngine;

public class ComboHoaVangItem : MonoBehaviour
{
	public PhanThuongItem avatar;

	public UISprite yeuCau1;

	public UISprite yeuCau2;

	public UISprite yeuCau3;

	public void setDataItem(HoaVangCfg.ComboItem data)
	{
		if (data != null)
		{
			avatar.Set(data.PhanThuongItem, false, true);
			if (data.ListYeuCau.Count > 0)
			{
				setYeuCau(data.ListYeuCau[0], yeuCau1);
			}
			else
			{
				setYeuCau(string.Empty, yeuCau1);
			}
			if (data.ListYeuCau.Count > 1)
			{
				setYeuCau(data.ListYeuCau[1], yeuCau2);
			}
			else
			{
				setYeuCau(string.Empty, yeuCau2);
			}
			if (data.ListYeuCau.Count > 2)
			{
				setYeuCau(data.ListYeuCau[2], yeuCau3);
			}
			else
			{
				setYeuCau(string.Empty, yeuCau3);
			}
		}
	}

	private void setYeuCau(string name, UISprite spYeuCau)
	{
		switch (name)
		{
		case "DAO":
			spYeuCau.spriteName = "icon_dao";
			break;
		case "KIEM":
			spYeuCau.spriteName = "icon_kiem";
			break;
		case "THUONG":
			spYeuCau.spriteName = "icon_thuong";
			break;
		case "CUNG":
			spYeuCau.spriteName = "icon_cung";
			break;
		case "RIU":
			spYeuCau.spriteName = "icon_riu";
			break;
		case "AMKHI":
			spYeuCau.spriteName = "icon_amkhi";
			break;
		case "":
			spYeuCau.spriteName = "icon_dauhoi";
			break;
		}
		spYeuCau.MakePixelPerfect();
	}
}
