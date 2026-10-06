using System.Collections.Generic;
using UnityEngine;

public class ScreenBanPhaoHoaEvent : ScreenBase
{
	public UILabel DiemHienTai;

	public UILabel SoLuongNL1;

	public UILabel SoLuongNL2;

	public UILabel SoLuongNL3;

	public GameObject TopGrp;

	public BanPhaoHoaTopItem baseTopItem;

	public GameObject PhanThuongGrp;

	public BanPhaoHoaPhanThuongItem basePhanThuongItem;

	public UILabel tongDiem;

	public UILabel desc;

	public override void OnActive()
	{
		if (child3Dscreen == null && GUIManager.instance.homeCity != null)
		{
			child3Dscreen = GUIManager.instance.homeCity.gameObject;
		}
		base.OnActive();
		AudioListener component = GUIManager.instance.cam2D.GetComponent<AudioListener>();
		if (component != null)
		{
			component.enabled = false;
		}
		GUIManager.ShowGadgets(6);
		Utils.SetLightMaps("Lightmap/HomeCity/", 2);
	}

	public void OnEnable()
	{
		DiemHienTai.text = Localization.instance.Get("DiemLabel") + ": " + GameManager.instance.m_GameClient.UserInfo.Gamer.PhaoHoaCount;
		SoLuongNL1.text = (GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData p) => p.Name == "VP_VO_PHAO") ? GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData p) => p.Name == "VP_VO_PHAO").Quantity.ToString() : "0");
		SoLuongNL2.text = (GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData p) => p.Name == "VP_NGOI_PHAO") ? GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData p) => p.Name == "VP_NGOI_PHAO").Quantity.ToString() : "0");
		SoLuongNL3.text = (GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData p) => p.Name == "VP_THUOC_SUNG") ? GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData p) => p.Name == "VP_THUOC_SUNG").Quantity.ToString() : "0");
	}

	private void Update()
	{
	}

	public void OnBanPhaoHoa()
	{
		GameManager.instance.m_GameClient.RequestBanPhaoHoaEvent();
	}

	public void OnTopGrpActive()
	{
		TopGrp.SetActive(true);
		foreach (Transform item in baseTopItem.transform.parent)
		{
			Transform transform2 = item;
			if (transform2.gameObject != baseTopItem.gameObject)
			{
				Object.Destroy(transform2.gameObject);
			}
		}
		for (int i = 0; i < GameManager.instance.m_GameClient.UserInfo.ServerInfo.PhaoHoaInfo.TopInfo.Count; i++)
		{
			baseTopItem.Create(GameManager.instance.m_GameClient.UserInfo.ServerInfo.PhaoHoaInfo.TopInfo[i].Key, i + 1, GameManager.instance.m_GameClient.UserInfo.ServerInfo.PhaoHoaInfo.TopInfo[i].Value, GameManager.instance.m_GameClient.UserInfo.ServerInfo.PhaoHoaInfo.PhanThuongTop[i]);
		}
	}

	public void ShowTop()
	{
		GameManager.instance.m_GameClient.RequestGetTopBanPhaoHoaEvent();
	}

	public void CloseTop()
	{
		TopGrp.SetActive(false);
	}

	public void OnPhanThuongGrpActive()
	{
		PhanThuongGrp.SetActive(true);
		tongDiem.text = Localization.instance.Get("TongDiemLabel") + ": " + GameManager.instance.m_GameClient.UserInfo.ServerInfo.PhaoHoaInfo.PhaoHoaCount;
		foreach (Transform item in basePhanThuongItem.transform.parent)
		{
			Transform transform2 = item;
			if (transform2.gameObject != basePhanThuongItem.gameObject)
			{
				Object.Destroy(transform2.gameObject);
			}
		}
		int num = 0;
		foreach (KeyValuePair<string, PhanThuongResponse> item2 in GameManager.instance.m_GameClient.UserInfo.ServerInfo.PhaoHoaInfo.PhanThuongAll)
		{
			num++;
			basePhanThuongItem.Create(num, int.Parse(item2.Key), item2.Value);
		}
	}

	public void ShowPhanThuong()
	{
		GameManager.instance.m_GameClient.RequestGetPhanThuongBanPhaoHoaEvent();
	}

	public void ClosePhanThuong()
	{
		PhanThuongGrp.SetActive(false);
	}
}
