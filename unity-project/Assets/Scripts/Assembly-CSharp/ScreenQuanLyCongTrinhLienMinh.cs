using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class ScreenQuanLyCongTrinhLienMinh : ScreenBase
{
	public List<GameObject> ListLevelItem;

	public GameObject ThienHaLauGroup;

	private LienMinhCongTrinh.CONGTRINH curCongTrinh;

	private int curLevel;

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(6);
	}

	public void OnEnable()
	{
		if (curCongTrinh == LienMinhCongTrinh.CONGTRINH.TU_NGHIA_DUONG)
		{
			OnTuNghiaDuong(true);
			OnThienHaLau(false);
			OnTangKiemCac(false);
		}
		else if (curCongTrinh == LienMinhCongTrinh.CONGTRINH.THIEN_HA_LAU)
		{
			OnTuNghiaDuong(false);
			OnThienHaLau(true);
			OnTangKiemCac(false);
		}
		else if (curCongTrinh == LienMinhCongTrinh.CONGTRINH.TANG_KIEM_CAC)
		{
			OnTuNghiaDuong(false);
			OnThienHaLau(false);
			OnTangKiemCac(true);
		}
	}

	public void OnTuNghiaDuong(bool onSelected)
	{
		if (!onSelected)
		{
			return;
		}
		curCongTrinh = LienMinhCongTrinh.CONGTRINH.TU_NGHIA_DUONG;
		curLevel = GameManager.instance.m_GameClient.UserInfo.LienMinh.TuNghiaLevel;
		base.transform.Find("ThienHaLauLabel").GetComponent<UILabel>().color = new Color(1f, 1f, 1f, 0f);
		ThienHaLauGroup.SetActive(false);
		if (GameManager.instance.m_GameClient.UserInfo.LienMinh == null)
		{
			return;
		}
		int num = 0;
		base.transform.Find("LienMinhStatus").GetComponent<UILabel>().text = string.Format(Localization.instance.Get("LienMinhStatus"), GameManager.instance.m_GameClient.UserInfo.LienMinh.TuNghiaLevel, GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList.Count, GameManager.instance.m_GameClient.UserInfo.LienMinh.DiemCongHien, ConfigManager.instance.CongTrinhLienMinhConfig["TuNghiaDuong"].Value[GameManager.instance.m_GameClient.UserInfo.LienMinh.TuNghiaLevel]);
		base.transform.Find("LienMinhName").GetComponent<UILabel>().text = GameManager.instance.m_GameClient.UserInfo.LienMinh.DisplayName;
		if (GameManager.instance.m_GameClient.UserInfo.LienMinh.TuNghiaLevel < ConfigManager.instance.CongTrinhLienMinhConfig["TuNghiaDuong"].CongHien.Count - 1)
		{
			base.transform.Find("CongHienRequire").GetComponent<UILabel>().text = string.Format(Localization.instance.Get("CongTrinhTieuTon"), ConfigManager.instance.CongTrinhLienMinhConfig["TuNghiaDuong"].CongHien[GameManager.instance.m_GameClient.UserInfo.LienMinh.TuNghiaLevel + 1].ToString());
		}
		else
		{
			base.transform.Find("CongHienRequire").GetComponent<UILabel>().text = string.Empty;
		}
		int num2 = 0;
		num2 = ((GameManager.instance.m_GameClient.UserInfo.LienMinh.TuNghiaLevel < 3) ? GameManager.instance.m_GameClient.UserInfo.LienMinh.TuNghiaLevel : ((GameManager.instance.m_GameClient.UserInfo.LienMinh.TuNghiaLevel <= ConfigManager.instance.CongTrinhLienMinhConfig["TuNghiaDuong"].CongHien.Count - 3) ? 2 : (GameManager.instance.m_GameClient.UserInfo.LienMinh.TuNghiaLevel - (ConfigManager.instance.CongTrinhLienMinhConfig["TuNghiaDuong"].CongHien.Count - 5))));
		for (int i = 0; i < ListLevelItem.Count; i++)
		{
			ListLevelItem[i].transform.Find("DescTHL1").GetComponent<UILabel>().text = string.Empty;
			ListLevelItem[i].transform.Find("DescTHL2").GetComponent<UILabel>().text = string.Empty;
			ListLevelItem[i].transform.Find("DescTHL3").GetComponent<UILabel>().text = string.Empty;
			ListLevelItem[i].transform.Find("DescTHL4").GetComponent<UILabel>().text = string.Empty;
			if (i < num2)
			{
				ListLevelItem[i].transform.Find("Bg").GetComponent<UISprite>().spriteName = "lien_minh_unlock";
				ListLevelItem[i].transform.Find("Bg").GetComponent<UISprite>().color = new Color(0.8f, 0.8f, 0.8f);
			}
			else if (i == num2)
			{
				ListLevelItem[i].transform.Find("Bg").GetComponent<UISprite>().spriteName = "lien_minh_user";
			}
			else
			{
				ListLevelItem[i].transform.Find("Bg").GetComponent<UISprite>().spriteName = "lien_minh_lock";
			}
			if (i < num2)
			{
				ListLevelItem[i].transform.Find("QuangSang").GetComponent<UISprite>().alpha = 0f;
			}
			else if (i == num2)
			{
				ListLevelItem[i].transform.Find("QuangSang").GetComponent<UISprite>().alpha = 1f;
			}
			else
			{
				ListLevelItem[i].transform.Find("QuangSang").GetComponent<UISprite>().alpha = 0f;
			}
			ListLevelItem[i].transform.Find("Level").GetComponent<UILabel>().text = string.Format(Localization.instance.Get("CapCongTrinh"), GameManager.instance.m_GameClient.UserInfo.LienMinh.TuNghiaLevel - num2 + i);
			ListLevelItem[i].transform.Find("Desc").GetComponent<UILabel>().text = string.Format(Localization.instance.Get("DescTuNghiaDuong"), ConfigManager.instance.CongTrinhLienMinhConfig["TuNghiaDuong"].Value[GameManager.instance.m_GameClient.UserInfo.LienMinh.TuNghiaLevel - num2 + i]);
		}
	}

	public void OnThienHaLau(bool onSelected)
	{
		if (!onSelected)
		{
			return;
		}
		base.transform.Find("ThienHaLauLabel").GetComponent<UILabel>().color = new Color(1f, 1f, 1f, 1f);
		ThienHaLauGroup.SetActive(true);
		curCongTrinh = LienMinhCongTrinh.CONGTRINH.THIEN_HA_LAU;
		curLevel = GameManager.instance.m_GameClient.UserInfo.LienMinh.ThienHaLauLevel;
		if (GameManager.instance.m_GameClient.UserInfo.LienMinh == null)
		{
			return;
		}
		int num = 0;
		base.transform.Find("LienMinhStatus").GetComponent<UILabel>().text = string.Format(Localization.instance.Get("LienMinhStatus"), GameManager.instance.m_GameClient.UserInfo.LienMinh.TuNghiaLevel, GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList.Count, GameManager.instance.m_GameClient.UserInfo.LienMinh.DiemCongHien, ConfigManager.instance.CongTrinhLienMinhConfig["TuNghiaDuong"].Value[GameManager.instance.m_GameClient.UserInfo.LienMinh.TuNghiaLevel]);
		base.transform.Find("LienMinhName").GetComponent<UILabel>().text = GameManager.instance.m_GameClient.UserInfo.LienMinh.DisplayName;
		if (GameManager.instance.m_GameClient.UserInfo.LienMinh.ThienHaLauLevel < ConfigManager.instance.CongTrinhLienMinhConfig["ThienHaLau"].CongHien.Count - 1)
		{
			base.transform.Find("CongHienRequire").GetComponent<UILabel>().text = string.Format(Localization.instance.Get("CongTrinhTieuTon"), ConfigManager.instance.CongTrinhLienMinhConfig["ThienHaLau"].CongHien[GameManager.instance.m_GameClient.UserInfo.LienMinh.ThienHaLauLevel + 1].ToString());
		}
		else
		{
			base.transform.Find("CongHienRequire").GetComponent<UILabel>().text = string.Empty;
		}
		int num2 = 0;
		num2 = ((GameManager.instance.m_GameClient.UserInfo.LienMinh.ThienHaLauLevel < 3) ? GameManager.instance.m_GameClient.UserInfo.LienMinh.ThienHaLauLevel : ((GameManager.instance.m_GameClient.UserInfo.LienMinh.ThienHaLauLevel <= 18) ? 2 : (GameManager.instance.m_GameClient.UserInfo.LienMinh.ThienHaLauLevel - 16)));
		for (int i = 0; i < ListLevelItem.Count; i++)
		{
			string json = ConfigManager.instance.CongTrinhLienMinhConfig["ThienHaLau"].Value[GameManager.instance.m_GameClient.UserInfo.LienMinh.ThienHaLauLevel - num2 + i];
			if (i < num2)
			{
				ListLevelItem[i].transform.Find("Bg").GetComponent<UISprite>().spriteName = "lien_minh_unlock";
				ListLevelItem[i].transform.Find("Bg").GetComponent<UISprite>().color = new Color(0.8f, 0.8f, 0.8f);
			}
			else if (i == num2)
			{
				ListLevelItem[i].transform.Find("Bg").GetComponent<UISprite>().spriteName = "lien_minh_user";
			}
			else
			{
				ListLevelItem[i].transform.Find("Bg").GetComponent<UISprite>().spriteName = "lien_minh_lock";
			}
			if (i < num2)
			{
				ListLevelItem[i].transform.Find("QuangSang").GetComponent<UISprite>().alpha = 0f;
			}
			else if (i == num2)
			{
				ListLevelItem[i].transform.Find("QuangSang").GetComponent<UISprite>().alpha = 1f;
			}
			else
			{
				ListLevelItem[i].transform.Find("QuangSang").GetComponent<UISprite>().alpha = 0f;
			}
			List<int> list = JsonMapper.ToObject<List<int>>(json);
			ListLevelItem[i].transform.Find("DescTHL1").GetComponent<UILabel>().text = list[0] + "%";
			ListLevelItem[i].transform.Find("DescTHL2").GetComponent<UILabel>().text = list[1] + "%";
			ListLevelItem[i].transform.Find("DescTHL3").GetComponent<UILabel>().text = list[2] + "%";
			ListLevelItem[i].transform.Find("DescTHL4").GetComponent<UILabel>().text = list[3] + "%";
			ListLevelItem[i].transform.Find("Level").GetComponent<UILabel>().text = string.Format(Localization.instance.Get("CapCongTrinh"), GameManager.instance.m_GameClient.UserInfo.LienMinh.ThienHaLauLevel - num2 + i);
			ListLevelItem[i].transform.Find("Desc").GetComponent<UILabel>().text = string.Empty;
		}
	}

	public void OnTangKiemCac(bool onSelected)
	{
		if (!onSelected)
		{
			return;
		}
		base.transform.Find("ThienHaLauLabel").GetComponent<UILabel>().color = new Color(1f, 1f, 1f, 0f);
		ThienHaLauGroup.SetActive(false);
		curCongTrinh = LienMinhCongTrinh.CONGTRINH.TANG_KIEM_CAC;
		curLevel = GameManager.instance.m_GameClient.UserInfo.LienMinh.TangKiemCacLevel;
		if (GameManager.instance.m_GameClient.UserInfo.LienMinh == null)
		{
			return;
		}
		int num = 0;
		base.transform.Find("LienMinhStatus").GetComponent<UILabel>().text = string.Format(Localization.instance.Get("LienMinhStatus"), GameManager.instance.m_GameClient.UserInfo.LienMinh.TuNghiaLevel, GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList.Count, GameManager.instance.m_GameClient.UserInfo.LienMinh.DiemCongHien, ConfigManager.instance.CongTrinhLienMinhConfig["TuNghiaDuong"].Value[GameManager.instance.m_GameClient.UserInfo.LienMinh.TuNghiaLevel]);
		base.transform.Find("LienMinhName").GetComponent<UILabel>().text = GameManager.instance.m_GameClient.UserInfo.LienMinh.DisplayName;
		if (GameManager.instance.m_GameClient.UserInfo.LienMinh.TangKiemCacLevel < ConfigManager.instance.CongTrinhLienMinhConfig["TangKiemCac"].CongHien.Count - 1)
		{
			base.transform.Find("CongHienRequire").GetComponent<UILabel>().text = string.Format(Localization.instance.Get("CongTrinhTieuTon"), ConfigManager.instance.CongTrinhLienMinhConfig["TangKiemCac"].CongHien[GameManager.instance.m_GameClient.UserInfo.LienMinh.TangKiemCacLevel + 1].ToString());
		}
		else
		{
			base.transform.Find("CongHienRequire").GetComponent<UILabel>().text = string.Empty;
		}
		int num2 = 0;
		num2 = ((GameManager.instance.m_GameClient.UserInfo.LienMinh.TangKiemCacLevel < 3) ? GameManager.instance.m_GameClient.UserInfo.LienMinh.TangKiemCacLevel : ((GameManager.instance.m_GameClient.UserInfo.LienMinh.TangKiemCacLevel <= 8) ? 2 : (GameManager.instance.m_GameClient.UserInfo.LienMinh.TangKiemCacLevel - 6)));
		for (int i = 0; i < ListLevelItem.Count; i++)
		{
			if (i < num2)
			{
				ListLevelItem[i].transform.Find("Bg").GetComponent<UISprite>().spriteName = "lien_minh_unlock";
				ListLevelItem[i].transform.Find("Bg").GetComponent<UISprite>().color = new Color(0.8f, 0.8f, 0.8f);
			}
			else if (i == num2)
			{
				ListLevelItem[i].transform.Find("Bg").GetComponent<UISprite>().spriteName = "lien_minh_user";
			}
			else
			{
				ListLevelItem[i].transform.Find("Bg").GetComponent<UISprite>().spriteName = "lien_minh_lock";
			}
			if (i < num2)
			{
				ListLevelItem[i].transform.Find("QuangSang").GetComponent<UISprite>().alpha = 0f;
			}
			else if (i == num2)
			{
				ListLevelItem[i].transform.Find("QuangSang").GetComponent<UISprite>().alpha = 1f;
			}
			else
			{
				ListLevelItem[i].transform.Find("QuangSang").GetComponent<UISprite>().alpha = 0f;
			}
			ListLevelItem[i].transform.Find("DescTHL1").GetComponent<UILabel>().text = string.Empty;
			ListLevelItem[i].transform.Find("DescTHL2").GetComponent<UILabel>().text = string.Empty;
			ListLevelItem[i].transform.Find("DescTHL3").GetComponent<UILabel>().text = string.Empty;
			ListLevelItem[i].transform.Find("DescTHL4").GetComponent<UILabel>().text = string.Empty;
			ListLevelItem[i].transform.Find("Level").GetComponent<UILabel>().text = string.Format(Localization.instance.Get("CapCongTrinh"), GameManager.instance.m_GameClient.UserInfo.LienMinh.TangKiemCacLevel - num2 + i);
			ListLevelItem[i].transform.Find("Desc").GetComponent<UILabel>().text = ConfigManager.instance.CongTrinhLienMinhConfig["TangKiemCac"].Value[GameManager.instance.m_GameClient.UserInfo.LienMinh.TangKiemCacLevel - num2 + i];
		}
	}

	private void OnNangCap()
	{
		NangCapCongTrinhRequest nangCapCongTrinhRequest = new NangCapCongTrinhRequest();
		nangCapCongTrinhRequest.CongTrinh = new LienMinhCongTrinh();
		nangCapCongTrinhRequest.CongTrinh.CongTrinhType = curCongTrinh;
		nangCapCongTrinhRequest.CongTrinh.CongTrinhLevel = curLevel;
		if (curCongTrinh == LienMinhCongTrinh.CONGTRINH.THIEN_HA_LAU)
		{
			if (curLevel >= 20)
			{
				return;
			}
		}
		else if (curCongTrinh == LienMinhCongTrinh.CONGTRINH.TU_NGHIA_DUONG)
		{
			if (curLevel >= 30)
			{
				return;
			}
		}
		else if (curLevel >= 10)
		{
			return;
		}
		GameManager.instance.m_GameClient.RequestNangCapCongTrinh(nangCapCongTrinhRequest);
	}
}
