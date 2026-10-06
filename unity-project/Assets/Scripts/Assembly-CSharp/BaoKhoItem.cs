using UnityEngine;

public class BaoKhoItem : MonoBehaviour
{
	public UISprite spBkg;

	public UILabel lbInfo;

	public UserInfo.BaoKhoInfo baoKhoData;

	public void setData(UserInfo.BaoKhoInfo bkInfo)
	{
		if (bkInfo != null)
		{
			baoKhoData = bkInfo;
			if (baoKhoData.GID > 0)
			{
				string text = "S" + baoKhoData.SID + ". " + baoKhoData.DisplayName;
				lbInfo.text = text;
			}
			else
			{
				lbInfo.text = Localization.instance.Get("BaoKhoVoChuLabel");
			}
			switch (baoKhoData.BaoKhoType)
			{
			case UserInfo.BaoKhoInfo.LoaiBaoKho.DONG:
				spBkg.spriteName = "bao_kho_4";
				break;
			case UserInfo.BaoKhoInfo.LoaiBaoKho.NGAN:
				spBkg.spriteName = "bao_kho_3";
				break;
			case UserInfo.BaoKhoInfo.LoaiBaoKho.KIM:
				spBkg.spriteName = "bao_kho_2";
				break;
			case UserInfo.BaoKhoInfo.LoaiBaoKho.NGOC:
				spBkg.spriteName = "bao_kho_1";
				break;
			}
			spBkg.MakePixelPerfect();
		}
	}
}
