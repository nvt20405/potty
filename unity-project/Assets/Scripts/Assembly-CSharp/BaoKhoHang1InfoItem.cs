using UnityEngine;

public class BaoKhoHang1InfoItem : MonoBehaviour
{
	public UISprite spBkg;

	public UILabel lbInfo;

	public UserInfo.BaoKhoInfo baokhoData;

	public void setData(UserInfo.BaoKhoInfo baoKhoInfo)
	{
		if (baoKhoInfo != null)
		{
			baokhoData = baoKhoInfo;
			OtherCfg.BaoKhoCfg baoKhoCfg = null;
			switch (baokhoData.BaoKhoType)
			{
			case UserInfo.BaoKhoInfo.LoaiBaoKho.DONG:
				baoKhoCfg = ConfigManager.instance.OtherConfig.BaoKhoConfig["BAO_KHO_DONG"];
				spBkg.spriteName = "bao_kho_4";
				break;
			case UserInfo.BaoKhoInfo.LoaiBaoKho.NGAN:
				baoKhoCfg = ConfigManager.instance.OtherConfig.BaoKhoConfig["BAO_KHO_NGAN"];
				spBkg.spriteName = "bao_kho_3";
				break;
			case UserInfo.BaoKhoInfo.LoaiBaoKho.KIM:
				baoKhoCfg = ConfigManager.instance.OtherConfig.BaoKhoConfig["BAO_KHO_KIM"];
				spBkg.spriteName = "bao_kho_2";
				spBkg.transform.localScale = new Vector3(101f, 100f, 1f);
				break;
			case UserInfo.BaoKhoInfo.LoaiBaoKho.NGOC:
				baoKhoCfg = ConfigManager.instance.OtherConfig.BaoKhoConfig["BAO_KHO_NGOC"];
				spBkg.spriteName = "bao_kho_1";
				spBkg.transform.localScale = new Vector3(100f, 105f, 1f);
				break;
			}
			if (baokhoData.GID > 0 && baokhoData.SID > 0 && baokhoData.DisplayName != null)
			{
				lbInfo.text = "S" + baokhoData.SID + ". " + baokhoData.DisplayName;
			}
			else
			{
				lbInfo.text = Localization.instance.Get("BaoKhoVoChuLabel");
			}
		}
	}

	public void onClick()
	{
		if (baokhoData != null)
		{
			if (baokhoData.GID <= 0)
			{
				PopupBaoKhoEmpty.Create(baokhoData);
			}
			else if (baokhoData.GID == GameManager.instance.m_GameClient.UserInfo.Gamer.ID && baokhoData.SID == GameManager.instance.m_GameClient.UserInfo.ServerInfo.ID)
			{
				PopupMyBaoKho.Create(baokhoData);
			}
			else if (baokhoData.GID != GameManager.instance.m_GameClient.UserInfo.Gamer.ID)
			{
				PopupOtherBaoKho.Create(baokhoData);
			}
		}
	}
}
