using UnityEngine;

public class PopupBaoKhoEmpty : MonoBehaviour
{
	public UISprite spBaoKho;

	public UILabel lbPham;

	public UILabel lbBacNhanMoiGio;

	public UILabel lbNKDNhanMoiGio;

	public UILabel lbVang;

	public GameObject grpPrice;

	public UISprite spBkg;

	private UserInfo.BaoKhoInfo baokhoData;

	public static PopupBaoKhoEmpty instance;

	public static void Create(UserInfo.BaoKhoInfo info)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupBaoKhoEmpty"))).GetComponent<PopupBaoKhoEmpty>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.setInfo(info);
	}

	private void setInfo(UserInfo.BaoKhoInfo info)
	{
		if (info != null)
		{
			baokhoData = info;
			OtherCfg.BaoKhoCfg baoKhoCfg = null;
			switch (baokhoData.BaoKhoType)
			{
			case UserInfo.BaoKhoInfo.LoaiBaoKho.DONG:
				baoKhoCfg = ConfigManager.instance.OtherConfig.BaoKhoConfig["BAO_KHO_DONG"];
				lbPham.text = Localization.instance.Get("PhamDONGLabel");
				spBkg.spriteName = "bao_kho_4";
				break;
			case UserInfo.BaoKhoInfo.LoaiBaoKho.NGAN:
				baoKhoCfg = ConfigManager.instance.OtherConfig.BaoKhoConfig["BAO_KHO_NGAN"];
				lbPham.text = Localization.instance.Get("PhamNGANLabel");
				spBkg.spriteName = "bao_kho_3";
				break;
			case UserInfo.BaoKhoInfo.LoaiBaoKho.KIM:
				baoKhoCfg = ConfigManager.instance.OtherConfig.BaoKhoConfig["BAO_KHO_KIM"];
				lbPham.text = Localization.instance.Get("PhamKIMLabel");
				spBkg.spriteName = "bao_kho_2";
				break;
			case UserInfo.BaoKhoInfo.LoaiBaoKho.NGOC:
				baoKhoCfg = ConfigManager.instance.OtherConfig.BaoKhoConfig["BAO_KHO_NGOC"];
				lbPham.text = Localization.instance.Get("PhamNGOCLabel");
				spBkg.spriteName = "bao_kho_1";
				break;
			}
			spBkg.MakePixelPerfect();
			if (baoKhoCfg != null)
			{
				lbBacNhanMoiGio.text = baoKhoCfg.BacNhanDuoc.ToString();
				lbNKDNhanMoiGio.text = baoKhoCfg.NguyenKhiDanNhanDuoc.ToString();
			}
			int luotCuopBaoKhoTrongNgay = GameManager.instance.m_GameClient.UserInfo.Gamer.LuotCuopBaoKhoTrongNgay;
			int num = 0;
			int num2 = luotCuopBaoKhoTrongNgay + 1;
			if (num2 >= 7)
			{
				num = ((num2 > 12) ? (10 * (num2 - 12)) : 5);
			}
			if (num > 0)
			{
				grpPrice.gameObject.SetActive(true);
				lbVang.text = num.ToString();
			}
			else
			{
				grpPrice.gameObject.SetActive(false);
			}
		}
	}

	public void btnCuopOnClick()
	{
		CuopBaoKhoRequest cuopBaoKhoRequest = new CuopBaoKhoRequest();
		cuopBaoKhoRequest.BaoKhoID = baokhoData.ID;
		GameManager.instance.m_GameClient.RequestCuopBaoKho(cuopBaoKhoRequest);
		DestroyPopup();
	}

	public void OnCloseClick()
	{
		DestroyPopup();
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}
}
