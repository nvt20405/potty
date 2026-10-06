using System.Collections;
using UnityEngine;

public class ScreenTrangBiHoangKim : ScreenBase
{
	public OtherAvatar ava1;

	public OtherAvatar ava2;

	public UILabel lbName1;

	public UILabel lbName2;

	public UILabel lbChiSo1;

	public UILabel lbChiSo2;

	public OtherAvatar avaVP;

	public UILabel lbSoLuongCan;

	private UserInfo.TrangBiData m_TrangBiData;

	private int soLuongVPCan = 100;

	public GameObject grpNormal;

	public GameObject grpResult;

	public OtherAvatar avaResult;

	public UILabel lbNameResult;

	public UILabel lbChiSoResult;

	private UserInfo.VatPhamTieuThuData vpNeed;

	public GameObject goAnimUpgrade;

	private void Start()
	{
	}

	public void Set(UserInfo.TrangBiData data)
	{
		if (data != null)
		{
			m_TrangBiData = data;
		}
	}

	public override void OnActive()
	{
		if (m_TrangBiData != null)
		{
			displayInfoTrangBi();
		}
	}

	private void displayInfoTrangBi()
	{
		goAnimUpgrade.gameObject.SetActive(false);
		grpNormal.gameObject.SetActive(true);
		grpResult.gameObject.SetActive(false);
		ava1.Set(m_TrangBiData, false, m_TrangBiData.HoangKim);
		ava2.Set(m_TrangBiData, false, m_TrangBiData.HoangKim + 1);
		TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[m_TrangBiData.Name];
		lbName1.text = trangBiCfg.TenHienThi;
		lbName2.text = trangBiCfg.TenHienThi;
		vpNeed = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_TINH_KIM");
		if (vpNeed == null)
		{
			vpNeed = new UserInfo.VatPhamTieuThuData();
			vpNeed.Name = "VP_TINH_KIM";
			vpNeed.Quantity = 0;
		}
		avaVP.Set(vpNeed);
		soLuongVPCan = 100 * (m_TrangBiData.HoangKim + 1);
		lbSoLuongCan.text = vpNeed.Quantity + "/" + soLuongVPCan;
		UserInfo.TrangBiData trangBiData = new UserInfo.TrangBiData();
		trangBiData.HoangKim = m_TrangBiData.HoangKim + 1;
		trangBiData.Level = m_TrangBiData.Level;
		trangBiData.Name = m_TrangBiData.Name;
		trangBiData.TinhLuyenLevel = m_TrangBiData.TinhLuyenLevel;
		trangBiData.TinhLuyenExp = m_TrangBiData.TinhLuyenExp;
		trangBiData.GID = m_TrangBiData.GID;
		trangBiData.Ngoc1Name = m_TrangBiData.Ngoc1Name;
		trangBiData.Ngoc2Name = m_TrangBiData.Ngoc2Name;
		trangBiData.Ngoc3Name = m_TrangBiData.Ngoc3Name;
		trangBiData.Ngoc1Lvl = m_TrangBiData.Ngoc1Lvl;
		trangBiData.Ngoc2Lvl = m_TrangBiData.Ngoc2Lvl;
		trangBiData.Ngoc3Lvl = m_TrangBiData.Ngoc3Lvl;
		trangBiData.Effect = m_TrangBiData.Effect;
		trangBiData.EffecTichLuy = m_TrangBiData.EffecTichLuy;
		trangBiData.MenhBoiDuong = m_TrangBiData.MenhBoiDuong;
		trangBiData.NgoaiBoiDuong = m_TrangBiData.NgoaiBoiDuong;
		trangBiData.ThanBoiDuong = m_TrangBiData.ThanBoiDuong;
		trangBiData.KhiBoiDuong = m_TrangBiData.KhiBoiDuong;
		switch (TrangBiCfg.GetLoaiTrangBi(m_TrangBiData.Name))
		{
		case LoaiTrangBi.Mu:
			lbChiSo1.text = Localization.instance.Get("KhiLabel") + ": " + trangBiCfg.GetChiSo(m_TrangBiData)[3];
			lbChiSo2.text = Localization.instance.Get("KhiLabel") + ": " + trangBiCfg.GetChiSo(trangBiData)[3];
			break;
		case LoaiTrangBi.VuKhi:
			lbChiSo1.text = Localization.instance.Get("NgoaiLabel") + ": " + trangBiCfg.GetChiSo(m_TrangBiData)[1];
			lbChiSo2.text = Localization.instance.Get("NgoaiLabel") + ": " + trangBiCfg.GetChiSo(trangBiData)[1];
			break;
		case LoaiTrangBi.AoGiap:
			lbChiSo1.text = Localization.instance.Get("ThanLabel") + ": " + trangBiCfg.GetChiSo(m_TrangBiData)[2];
			lbChiSo2.text = Localization.instance.Get("ThanLabel") + ": " + trangBiCfg.GetChiSo(trangBiData)[2];
			break;
		case LoaiTrangBi.TrangSuc:
			lbChiSo1.text = Localization.instance.Get("MenhLabel") + ": " + trangBiCfg.GetChiSo(m_TrangBiData)[0];
			lbChiSo2.text = Localization.instance.Get("MenhLabel") + ": " + trangBiCfg.GetChiSo(trangBiData)[0];
			break;
		}
	}

	public void btnHoangKim_OnClick()
	{
		SetTrangBiHoangKimRequest setTrangBiHoangKimRequest = new SetTrangBiHoangKimRequest();
		setTrangBiHoangKimRequest.TrangBiID = m_TrangBiData.ID;
		setTrangBiHoangKimRequest.VatPhamID = vpNeed.ID;
		GameManager.instance.m_GameClient.RequestSetTrangBiHoangKim(setTrangBiHoangKimRequest);
	}

	public void btnBack_OnClick()
	{
		GUIManager.instance.SetScreen(GUIManager.instance.LastScreen);
	}

	public void startPlayAnim(SetTrangBiHoangKimReponse response)
	{
		goAnimUpgrade.SetActive(true);
		goAnimUpgrade.GetComponent<ParticleSystem>().Simulate(0f, true, true);
		goAnimUpgrade.GetComponent<ParticleSystem>().Play();
		StartCoroutine(updateView(2f, response));
	}

	public IEnumerator updateView(float waitTime, SetTrangBiHoangKimReponse response)
	{
		yield return new WaitForSeconds(waitTime);
		grpNormal.gameObject.SetActive(false);
		grpResult.gameObject.SetActive(true);
		SetTrangBiHoangKimReponse response2 = null;
		m_TrangBiData = GameManager.instance.m_GameClient.UserInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.ID == response2.TrangBiID);
		if (m_TrangBiData != null)
		{
			avaResult.Set(m_TrangBiData, false, m_TrangBiData.HoangKim);
			TrangBiCfg cfg = ConfigManager.instance.m_dicTrangBi[m_TrangBiData.Name];
			lbNameResult.text = cfg.TenHienThi;
			switch (TrangBiCfg.GetLoaiTrangBi(m_TrangBiData.Name))
			{
			case LoaiTrangBi.Mu:
				lbChiSoResult.text = Localization.instance.Get("KhiLabel") + ": " + cfg.GetChiSo(m_TrangBiData)[3];
				break;
			case LoaiTrangBi.VuKhi:
				lbChiSoResult.text = Localization.instance.Get("NgoaiLabel") + ": " + cfg.GetChiSo(m_TrangBiData)[1];
				break;
			case LoaiTrangBi.AoGiap:
				lbChiSoResult.text = Localization.instance.Get("ThanLabel") + ": " + cfg.GetChiSo(m_TrangBiData)[2];
				break;
			case LoaiTrangBi.TrangSuc:
				lbChiSoResult.text = Localization.instance.Get("MenhLabel") + ": " + cfg.GetChiSo(m_TrangBiData)[0];
				break;
			}
		}
	}
}
