using System.Collections.Generic;
using UnityEngine;

public class PopupTrangBi : MonoBehaviour
{
	public OtherAvatar otherAvatar;

	public UILabel TrangBiName;

	public UILabel ThuCuoiName;

	public GameObject DoiHinhBtnGroup;

	public GameObject TrangBiBtnGroup;

	public UILabel lbChiSo;

	public UILabel oldLevelLabel;

	public UISprite spChiSo;

	public UILabel lbChiSo2;

	public UISprite spChiSo2;

	public UILabel lbDescription;

	public UILabel lbDuyenInfo;

	public UIButton btnClose;

	public UILabel lbType;

	public UISprite spPham;

	public GameObject groupNgoc;

	public UISprite spNgoc1;

	public UISprite spNgoc2;

	public UISprite spNgoc3;

	public UILabel lbNgoc1;

	public UILabel lbNgoc2;

	public UILabel lbNgoc3;

	public GameObject groupDuyen;

	public GameObject groupThanBinh;

	public UILabel lbThanBinhInfo1;

	public UILabel lbThanBinhInfo2;

	public UILabel lbThanBinhInfo3;

	public UISprite iconThanBinh1;

	public UISprite iconThanBinh2;

	public UISprite iconThanBinh3;

	private UserInfo.TrangBiData m_TrangBiData;

	public GameObject m_AnimCuongHoa;

	public static PopupTrangBi instance;

	private void Start()
	{
		m_AnimCuongHoa.SetActive(false);
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

	private static void Create(UserInfo.TrangBiData data, bool isDisplayHieuUng = false)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("popup/PopupTrangBi"))).GetComponent<PopupTrangBi>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.Set(data, isDisplayHieuUng);
		NGUITools.SetActive(instance.DoiHinhBtnGroup, false);
		NGUITools.SetActive(instance.TrangBiBtnGroup, false);
		NGUITools.SetActive(instance.btnClose.gameObject, false);
	}

	public void Set(UserInfo.TrangBiData data, bool isDisplayHieuUng = false)
	{
		ScreenVoLamPho.SetVoLamPhoStatus(data.Name, 1);
		m_TrangBiData = data;
		otherAvatar.Set(data, false, data.HoangKim, true);
		TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[data.Name];
		TrangBiName.text = trangBiCfg.TenHienThi;
		ThuCuoiName.text = string.Empty;
		lbDescription.text = trangBiCfg.MoTa;
		oldLevelLabel.text = Localization.instance.Get("CapLabel") + ": " + data.Level;
		switch (TrangBiCfg.GetLoaiTrangBi(data.Name))
		{
		case LoaiTrangBi.VuKhi:
			lbType.text = Localization.instance.Get("VuKhiBtnLabel");
			break;
		case LoaiTrangBi.Mu:
			lbType.text = Localization.instance.Get("MuBtnLabel");
			break;
		case LoaiTrangBi.AoGiap:
			lbType.text = Localization.instance.Get("GiapBtnLabel");
			break;
		case LoaiTrangBi.TrangSuc:
			lbType.text = Localization.instance.Get("TrangSucBtnLabel");
			break;
		default:
			lbType.text = string.Empty;
			break;
		}
		if (isDisplayHieuUng)
		{
			displayKhaiQuangInfo();
		}
		else
		{
			displayDuyenTrangBi(data.Name);
		}
		spNgoc1.gameObject.SetActive(false);
		spNgoc2.gameObject.SetActive(false);
		spNgoc3.gameObject.SetActive(false);
		lbNgoc1.text = string.Empty;
		lbNgoc2.text = string.Empty;
		lbNgoc3.text = string.Empty;
		if (trangBiCfg.Hang == ItemClass.Giap)
		{
			spPham.spriteName = "giap";
		}
		else if (trangBiCfg.Hang == ItemClass.At)
		{
			spPham.spriteName = "at";
		}
		else if (trangBiCfg.Hang == ItemClass.Binh)
		{
			spPham.spriteName = "binh";
		}
		if (trangBiCfg.Hang >= ItemClass.Binh)
		{
			spNgoc1.gameObject.SetActive(true);
			setIconNgoc(m_TrangBiData.Ngoc1Name, m_TrangBiData.Ngoc1Lvl, spNgoc1);
			UserInfo.TrangBiData.LoaiBuff buff;
			float ChiSo;
			UserInfo.TrangBiData.GetLoaiBuff(m_TrangBiData, 0, out buff, out ChiSo);
			lbNgoc1.text = displayChiSoNgoc(buff, ChiSo);
		}
		if (trangBiCfg.Hang >= ItemClass.At)
		{
			spNgoc2.gameObject.SetActive(true);
			setIconNgoc(m_TrangBiData.Ngoc2Name, m_TrangBiData.Ngoc2Lvl, spNgoc2);
			UserInfo.TrangBiData.LoaiBuff buff2;
			float ChiSo2;
			UserInfo.TrangBiData.GetLoaiBuff(m_TrangBiData, 1, out buff2, out ChiSo2);
			lbNgoc2.text = displayChiSoNgoc(buff2, ChiSo2);
		}
		if (trangBiCfg.Hang >= ItemClass.Giap)
		{
			spNgoc3.gameObject.SetActive(true);
			setIconNgoc(m_TrangBiData.Ngoc3Name, m_TrangBiData.Ngoc3Lvl, spNgoc3);
			UserInfo.TrangBiData.LoaiBuff buff3;
			float ChiSo3;
			UserInfo.TrangBiData.GetLoaiBuff(m_TrangBiData, 2, out buff3, out ChiSo3);
			lbNgoc3.text = displayChiSoNgoc(buff3, ChiSo3);
		}
		spChiSo.gameObject.SetActive(false);
		spChiSo2.gameObject.SetActive(false);
		UILabel uILabel = lbChiSo;
		string empty = string.Empty;
		lbChiSo2.text = empty;
		uILabel.text = empty;
		List<int> chiSo = trangBiCfg.GetChiSo(data);
		if (chiSo == null || chiSo.Count != 4)
		{
			return;
		}
		bool flag = false;
		for (int i = 0; i < chiSo.Count; i++)
		{
			if (i == 0 && chiSo[0] > 0)
			{
				spChiSo.gameObject.SetActive(true);
				spChiSo.spriteName = "icon_menh";
				lbChiSo.text = chiSo[0].ToString();
				flag = true;
			}
			if (i == 1 && chiSo[1] > 0)
			{
				if (flag)
				{
					spChiSo2.gameObject.SetActive(true);
					spChiSo2.spriteName = "icon_ngoai";
					lbChiSo2.text = chiSo[1].ToString();
				}
				else
				{
					spChiSo.gameObject.SetActive(true);
					spChiSo.spriteName = "icon_ngoai";
					lbChiSo.text = chiSo[1].ToString();
					flag = true;
				}
			}
			if (i == 2 && chiSo[2] > 0)
			{
				if (flag)
				{
					spChiSo2.gameObject.SetActive(true);
					spChiSo2.spriteName = "icon_than";
					lbChiSo2.text = chiSo[2].ToString();
				}
				else
				{
					spChiSo.gameObject.SetActive(true);
					spChiSo.spriteName = "icon_than";
					lbChiSo.text = chiSo[2].ToString();
					flag = true;
				}
			}
			if (i == 3 && chiSo[3] > 0)
			{
				if (flag)
				{
					spChiSo2.gameObject.SetActive(true);
					spChiSo2.spriteName = "icon_noi_goc";
					lbChiSo2.text = chiSo[3].ToString();
				}
				else
				{
					spChiSo.gameObject.SetActive(true);
					spChiSo.spriteName = "icon_noi_goc";
					lbChiSo.text = chiSo[3].ToString();
					flag = true;
				}
			}
		}
	}

	public void Set(UserInfo.ThuCuoiData data)
	{
		otherAvatar.Set(data.CodeName);
		otherAvatar.transform.localPosition = new Vector3(0f, otherAvatar.transform.localPosition.y, otherAvatar.transform.localPosition.z);
		OtherCfg.ThuCuoiCfg thuCuoiCfg = ConfigManager.instance.OtherConfig.ThuCuoiConfig[data.CodeName];
		TrangBiName.text = string.Empty;
		ThuCuoiName.text = thuCuoiCfg.DisplayName;
		lbDuyenInfo.text = thuCuoiCfg.MoTa;
		oldLevelLabel.text = string.Empty;
		lbType.text = string.Empty;
		lbDescription.text = displayChiSoThuCuoi(thuCuoiCfg) + "\n" + string.Format(Localization.instance.Get("TimeSuDungNguaMess"), data.Duration);
		spNgoc1.gameObject.SetActive(false);
		spNgoc2.gameObject.SetActive(false);
		spNgoc3.gameObject.SetActive(false);
		lbNgoc1.text = string.Empty;
		lbNgoc2.text = string.Empty;
		lbNgoc3.text = string.Empty;
		groupThanBinh.gameObject.SetActive(false);
		groupDuyen.gameObject.SetActive(false);
		spPham.gameObject.SetActive(false);
		UILabel uILabel = lbChiSo;
		string empty = string.Empty;
		lbChiSo2.text = empty;
		uILabel.text = empty;
		spChiSo.gameObject.SetActive(false);
		spChiSo2.gameObject.SetActive(false);
	}

	private string displayChiSoThuCuoi(OtherCfg.ThuCuoiCfg thuCuoiCfg)
	{
		string text = Localization.instance.Get("ThuCuoiHieuUng");
		if (thuCuoiCfg.MenhBuff > 0)
		{
			text = text + string.Format(" {0} {1}", thuCuoiCfg.MenhBuff, Localization.instance.Get("MenhLabel")) + ",";
		}
		if (thuCuoiCfg.MenhBuffRate > 0)
		{
			text = text + string.Format(" {0}% {1}", thuCuoiCfg.MenhBuffRate, Localization.instance.Get("MenhLabel")) + ",";
		}
		if (thuCuoiCfg.NgoaiBuff > 0)
		{
			text = text + string.Format(" {0} {1}", thuCuoiCfg.NgoaiBuff, Localization.instance.Get("NgoaiLabel")) + ",";
		}
		if (thuCuoiCfg.NgoaiBuffRate > 0)
		{
			text = text + string.Format(" {0}% {1}", thuCuoiCfg.NgoaiBuffRate, Localization.instance.Get("NgoaiLabel")) + ",";
		}
		if (thuCuoiCfg.ThanBuff > 0)
		{
			text = text + string.Format(" {0} {1}", thuCuoiCfg.ThanBuff, Localization.instance.Get("ThanLabel")) + ",";
		}
		if (thuCuoiCfg.ThanBuffRate > 0)
		{
			text = text + string.Format(" {0}% {1}", thuCuoiCfg.ThanBuffRate, Localization.instance.Get("ThanLabel")) + ",";
		}
		if (thuCuoiCfg.KhiBuff > 0)
		{
			text = text + string.Format(" {0} {1}", thuCuoiCfg.KhiBuff, Localization.instance.Get("KhiLabel")) + ",";
		}
		if (thuCuoiCfg.KhiBuffRate > 0)
		{
			text = text + string.Format(" {0}% {1}", thuCuoiCfg.KhiBuffRate, Localization.instance.Get("KhiLabel")) + ",";
		}
		if (thuCuoiCfg.BaoBuff > 0)
		{
			text = text + string.Format(" {0}% {1}", thuCuoiCfg.BaoBuff, Localization.instance.Get("BaoLabel")) + ",";
		}
		if (thuCuoiCfg.NeBuff > 0)
		{
			text = text + string.Format(" {0}% {1}", thuCuoiCfg.NeBuff, Localization.instance.Get("NeLabel")) + ",";
		}
		if (thuCuoiCfg.DoDonBuff > 0)
		{
			text = text + string.Format(" {0}% {1}", thuCuoiCfg.DoDonBuff, Localization.instance.Get("DoDonLabel")) + ",";
		}
		return text.Substring(0, text.Length - 1) + ".";
	}

	private string displayChiSoNgoc(UserInfo.TrangBiData.LoaiBuff loaiBuff, float chiSo)
	{
		string empty = string.Empty;
		switch (loaiBuff)
		{
		case UserInfo.TrangBiData.LoaiBuff.NONE:
			return "---";
		case UserInfo.TrangBiData.LoaiBuff.TOC_DANH:
			return string.Format(Localization.instance.Get("TangChiSoTocDoDanh"), chiSo);
		case UserInfo.TrangBiData.LoaiBuff.KHANG_CHUONG:
			return string.Format(Localization.instance.Get("TangChiSoKhangChuong"), chiSo);
		case UserInfo.TrangBiData.LoaiBuff.HOA_GIAI:
			return string.Format(Localization.instance.Get("TangChiSoHoaGiai"), chiSo);
		case UserInfo.TrangBiData.LoaiBuff.BAO_KICH:
			return string.Format(Localization.instance.Get("TangChiSoBaoKich"), chiSo);
		case UserInfo.TrangBiData.LoaiBuff.TANG_CHI_SO:
			return string.Format(Localization.instance.Get("TangChiSoNgoc"), chiSo);
		default:
			return "---";
		}
	}

	public void displayKhaiQuangInfo()
	{
		groupThanBinh.gameObject.SetActive(true);
		groupDuyen.gameObject.SetActive(false);
		if (m_TrangBiData != null)
		{
			TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[m_TrangBiData.Name];
			if (m_TrangBiData.Effect != null && m_TrangBiData.Effect.Count > 0)
			{
				if (m_TrangBiData.Effect[0] != null)
				{
					lbThanBinhInfo1.text = m_TrangBiData.Effect[0].GetEffectColor() + getStringChiSo(m_TrangBiData.Effect[0].LoaiEff, m_TrangBiData.Effect[0].EffVal);
					iconThanBinh1.spriteName = "icon_active_than_binh_list";
				}
				else
				{
					lbThanBinhInfo1.text = Localization.instance.Get("ChuaMoHieuUngTrangBiLabel");
					iconThanBinh1.spriteName = "icon_than_binh_list";
				}
				if (trangBiCfg.Hang == ItemClass.At || trangBiCfg.Hang == ItemClass.Giap)
				{
					if (m_TrangBiData.Effect.Count > 1 && m_TrangBiData.Effect[1] != null)
					{
						lbThanBinhInfo2.text = m_TrangBiData.Effect[1].GetEffectColor() + getStringChiSo(m_TrangBiData.Effect[1].LoaiEff, m_TrangBiData.Effect[1].EffVal);
						iconThanBinh2.spriteName = "icon_active_than_binh_list";
					}
					else
					{
						lbThanBinhInfo2.text = Localization.instance.Get("ChuaMoHieuUngTrangBiLabel");
						iconThanBinh2.spriteName = "icon_than_binh_list";
					}
				}
				if (trangBiCfg.Hang == ItemClass.Giap)
				{
					if (m_TrangBiData.Effect.Count > 2 && m_TrangBiData.Effect[2] != null)
					{
						lbThanBinhInfo3.text = m_TrangBiData.Effect[2].GetEffectColor() + getStringChiSo(m_TrangBiData.Effect[2].LoaiEff, m_TrangBiData.Effect[2].EffVal);
						iconThanBinh3.spriteName = "icon_active_than_binh_list";
					}
					else
					{
						lbThanBinhInfo3.text = Localization.instance.Get("ChuaMoHieuUngTrangBiLabel");
						iconThanBinh3.spriteName = "icon_than_binh_list";
					}
				}
			}
			else
			{
				lbThanBinhInfo1.text = Localization.instance.Get("ChuaMoHieuUngTrangBiLabel");
				lbThanBinhInfo2.text = Localization.instance.Get("ChuaMoHieuUngTrangBiLabel");
				lbThanBinhInfo3.text = Localization.instance.Get("ChuaMoHieuUngTrangBiLabel");
				iconThanBinh1.spriteName = "icon_than_binh_list";
				iconThanBinh2.spriteName = "icon_than_binh_list";
				iconThanBinh3.spriteName = "icon_than_binh_list";
			}
			iconThanBinh1.MakePixelPerfect();
			iconThanBinh2.MakePixelPerfect();
			iconThanBinh3.MakePixelPerfect();
			if (trangBiCfg.Hang == ItemClass.Binh)
			{
				lbThanBinhInfo1.gameObject.SetActive(true);
				iconThanBinh1.gameObject.SetActive(true);
				lbThanBinhInfo2.gameObject.SetActive(false);
				iconThanBinh2.gameObject.SetActive(false);
				lbThanBinhInfo3.gameObject.SetActive(false);
				iconThanBinh3.gameObject.SetActive(false);
			}
			else if (trangBiCfg.Hang == ItemClass.At)
			{
				lbThanBinhInfo1.gameObject.SetActive(true);
				iconThanBinh1.gameObject.SetActive(true);
				lbThanBinhInfo2.gameObject.SetActive(true);
				iconThanBinh2.gameObject.SetActive(true);
				lbThanBinhInfo3.gameObject.SetActive(false);
				iconThanBinh3.gameObject.SetActive(false);
			}
			else if (trangBiCfg.Hang == ItemClass.Giap)
			{
				lbThanBinhInfo1.gameObject.SetActive(true);
				iconThanBinh1.gameObject.SetActive(true);
				lbThanBinhInfo2.gameObject.SetActive(true);
				iconThanBinh2.gameObject.SetActive(true);
				lbThanBinhInfo3.gameObject.SetActive(true);
				iconThanBinh3.gameObject.SetActive(true);
			}
		}
		else
		{
			groupThanBinh.gameObject.SetActive(false);
		}
	}

	public void displayDuyenTrangBi(string trangBiName)
	{
		groupThanBinh.gameObject.SetActive(false);
		groupDuyen.gameObject.SetActive(true);
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, NhanVatCfg> dicNhanVat in ConfigManager.instance.m_dicNhanVats)
		{
			if (dicNhanVat.Value.DuyenPhan == null || dicNhanVat.Value.DuyenPhan.Count <= 0)
			{
				continue;
			}
			for (int i = 0; i < dicNhanVat.Value.DuyenPhan.Count; i++)
			{
				if (dicNhanVat.Value.DuyenPhan[i].LoaiDuyenPhan != LoaiDuyenPhan.TrangBiDo || dicNhanVat.Value.DuyenPhan[i].DoiTuong == null || dicNhanVat.Value.DuyenPhan[i].DoiTuong.Count <= 0)
				{
					continue;
				}
				for (int j = 0; j < dicNhanVat.Value.DuyenPhan[i].DoiTuong.Count; j++)
				{
					if (dicNhanVat.Value.DuyenPhan[i].DoiTuong[j] == trangBiName)
					{
						list.Add(getStringDuyenTrangBi(dicNhanVat.Value, dicNhanVat.Value.DuyenPhan[i].HeSo, dicNhanVat.Value.DuyenPhan[i].ChiSoDuyen));
					}
				}
			}
		}
		string text = string.Empty;
		if (list != null && list.Count > 0)
		{
			for (int k = 0; k < list.Count; k++)
			{
				text = text + list[k] + ". \n";
			}
		}
		lbDuyenInfo.text = text;
	}

	public string getStringDuyenTrangBi(NhanVatCfg nhanVatConfig, float heso, ChiSoDuyenPhan loaiDuyen)
	{
		string empty = string.Empty;
		string text = empty;
		empty = text + Localization.instance.Get("DuyenTrangBiChoNhanVatMess") + " : " + nhanVatConfig.TenHienThi + " ";
		text = empty;
		empty = text + Localization.instance.Get("DuocNhanMess") + " " + heso + "% [-]";
		switch (loaiDuyen)
		{
		case ChiSoDuyenPhan.Khi:
			empty += Localization.instance.Get("NoiTitle");
			break;
		case ChiSoDuyenPhan.Menh:
			empty += Localization.instance.Get("MenhTitle");
			break;
		case ChiSoDuyenPhan.Ngoai:
			empty += Localization.instance.Get("NgoaiTitle");
			break;
		case ChiSoDuyenPhan.ThanPhap:
			empty += Localization.instance.Get("ThanPhapTitle");
			break;
		case ChiSoDuyenPhan.DoDon:
			empty += Localization.instance.Get("DoDonTitle");
			break;
		case ChiSoDuyenPhan.Ne:
			empty += Localization.instance.Get("NeTitle");
			break;
		case ChiSoDuyenPhan.Bao:
			empty += Localization.instance.Get("BaoTitle");
			break;
		}
		return empty;
	}

	public static void CreateByScreenDoiHinh(UserInfo.TrangBiData data)
	{
		Create(data, true);
		if (data.ID > 0)
		{
			NGUITools.SetActive(instance.DoiHinhBtnGroup, true);
		}
	}

	public static void CreateToCuongHoaTrangBi(UserInfo.TrangBiData data)
	{
		Create(data);
		if (data.ID > 0)
		{
			NGUITools.SetActive(instance.TrangBiBtnGroup, true);
		}
	}

	public static void CreateByScreenTrangBi(UserInfo.TrangBiData data)
	{
		Create(data, true);
		NGUITools.SetActive(instance.btnClose.gameObject, true);
	}

	public static void CreateByNormalScreen(TrangBiCfg cfgData)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("popup/PopupTrangBi"))).GetComponent<PopupTrangBi>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.Set(cfgData);
		NGUITools.SetActive(instance.DoiHinhBtnGroup, false);
		NGUITools.SetActive(instance.TrangBiBtnGroup, false);
		NGUITools.SetActive(instance.groupNgoc, false);
		NGUITools.SetActive(instance.btnClose.gameObject, true);
		NGUITools.SetActive(instance.m_AnimCuongHoa.gameObject, false);
	}

	public static void CreateByNormalScreen(UserInfo.TrangBiData data, bool isDisplayHieuUng = false)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("popup/PopupTrangBi"))).GetComponent<PopupTrangBi>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.Set(data, isDisplayHieuUng);
		NGUITools.SetActive(instance.DoiHinhBtnGroup, false);
		NGUITools.SetActive(instance.TrangBiBtnGroup, false);
		NGUITools.SetActive(instance.btnClose.gameObject, true);
	}

	public static void CreateByThuCuoiInfo(UserInfo.ThuCuoiData data)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("popup/PopupTrangBi"))).GetComponent<PopupTrangBi>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.Set(data);
		NGUITools.SetActive(instance.DoiHinhBtnGroup, false);
		NGUITools.SetActive(instance.TrangBiBtnGroup, false);
		NGUITools.SetActive(instance.btnClose.gameObject, true);
	}

	public void Set(TrangBiCfg cfgData)
	{
		ScreenVoLamPho.SetVoLamPhoStatus(cfgData.Name, 1);
		otherAvatar.Set(cfgData.Name);
		TrangBiName.text = cfgData.TenHienThi;
		ThuCuoiName.text = string.Empty;
		lbDescription.text = cfgData.MoTa;
		if (cfgData.Hang == ItemClass.Giap)
		{
			spPham.spriteName = "giap";
		}
		else if (cfgData.Hang == ItemClass.At)
		{
			spPham.spriteName = "at";
		}
		else if (cfgData.Hang == ItemClass.Binh)
		{
			spPham.spriteName = "binh";
		}
		displayDuyenTrangBi(cfgData.Name);
		oldLevelLabel.text = string.Empty;
		UserInfo.TrangBiData trangBiData = new UserInfo.TrangBiData();
		trangBiData.Name = cfgData.Name;
		trangBiData.Level = 1;
		trangBiData.TinhLuyenLevel = 0;
		spChiSo2.gameObject.SetActive(false);
		lbChiSo2.text = string.Empty;
		List<int> chiSo = cfgData.GetChiSo(trangBiData);
		switch (TrangBiCfg.GetLoaiTrangBi(cfgData.Name))
		{
		case LoaiTrangBi.Mu:
			spChiSo.spriteName = "icon_noi_goc";
			lbType.text = Localization.instance.Get("MuBtnLabel");
			lbChiSo.text = chiSo[3].ToString();
			break;
		case LoaiTrangBi.VuKhi:
			spChiSo.spriteName = "icon_ngoai";
			lbType.text = Localization.instance.Get("VuKhiBtnLabel");
			lbChiSo.text = chiSo[1].ToString();
			break;
		case LoaiTrangBi.AoGiap:
			spChiSo.spriteName = "icon_than";
			lbType.text = Localization.instance.Get("GiapBtnLabel");
			lbChiSo.text = chiSo[2].ToString();
			break;
		case LoaiTrangBi.TrangSuc:
			spChiSo.spriteName = "icon_menh";
			lbType.text = Localization.instance.Get("TrangSucBtnLabel");
			lbChiSo.text = chiSo[0].ToString();
			break;
		}
	}

	public void OnDoiClick()
	{
		ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
		List<int> list = new List<int>();
		UserInfo.HeroData heroData = GameManager.instance.m_GameClient.UserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == m_TrangBiData.HID);
		if (heroData != null)
		{
			list.Add(heroData.VuKhiID);
			list.Add(heroData.MuID);
			list.Add(heroData.AoGiapID);
			list.Add(heroData.TrangSucID);
		}
		LoaiTrangBi loaiTrangBi = TrangBiCfg.GetLoaiTrangBi(m_TrangBiData.Name);
		PopupSelectTrangBi.Create(screenDoiHinh.NhanVatInfo.OnChangeTrangBi, list, loaiTrangBi);
		DestroyPopup();
	}

	public void btnCuongHoaTrangBi_OnClick(GameObject go)
	{
		if (m_TrangBiData != null)
		{
			DestroyPopup();
			ScreenCuongHoa screenCuongHoa = GUIManager.getScreen(GAME_SCREEN.ScreenCuongHoa) as ScreenCuongHoa;
			screenCuongHoa.Set(m_TrangBiData);
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenCuongHoa);
		}
	}

	public void btnKhamNam_OnClick()
	{
		if (m_TrangBiData != null)
		{
			ScreenKhamNam screenKhamNam = GUIManager.getScreen(GAME_SCREEN.ScreenKhamNam) as ScreenKhamNam;
			screenKhamNam.Set(m_TrangBiData);
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenKhamNam);
		}
		DestroyPopup();
	}

	public void startPlayAnim()
	{
		m_AnimCuongHoa.SetActive(true);
		m_AnimCuongHoa.GetComponent<ParticleSystem>().Simulate(0f, true, true);
		m_AnimCuongHoa.GetComponent<ParticleSystem>().Play();
	}

	public void setIconNgoc(string ngocName, int ngocLevel, UISprite currentSprite)
	{
		UserInfo.TrangBiData.LoaiNgoc loaiNgoc = UserInfo.TrangBiData.GetLoaiNgoc(ngocName);
		setNgocData(loaiNgoc, ngocLevel, currentSprite);
	}

	private void setNgocData(UserInfo.TrangBiData.LoaiNgoc loaiNgoc, int ngocLevel, UISprite currentSprite)
	{
		string text = "icon_";
		if (ngocLevel > 0 && ngocLevel <= 10)
		{
			switch (loaiNgoc)
			{
			case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_DO:
				text = text + "do_hang" + ngocLevel;
				break;
			case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_TIM:
				text = text + "tim_hang" + ngocLevel;
				break;
			case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_VANG:
				text = text + "vang_hang" + ngocLevel;
				break;
			case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_XANH:
				text = text + "xanh_hang" + ngocLevel;
				break;
			}
		}
		else
		{
			text = "icon_ngoc_empty";
		}
		currentSprite.spriteName = text;
	}

	private string getStringChiSo(UserInfo.TrangBiData.TRANG_BI_EFF tRANG_BI_EFF, float p)
	{
		string result = string.Empty;
		switch (tRANG_BI_EFF)
		{
		case UserInfo.TrangBiData.TRANG_BI_EFF.TANG_CONG:
			result = string.Format(Localization.instance.Get("TRANG_BI_EFF_TANG_CONG_DES"), p);
			break;
		case UserInfo.TrangBiData.TRANG_BI_EFF.TANG_MAU:
			result = string.Format(Localization.instance.Get("TRANG_BI_EFF_TANG_MAU_DES"), p);
			break;
		case UserInfo.TrangBiData.TRANG_BI_EFF.TANG_THU:
			result = string.Format(Localization.instance.Get("TRANG_BI_EFF_TANG_THU_DES"), p);
			break;
		case UserInfo.TrangBiData.TRANG_BI_EFF.TANG_NOI:
			result = string.Format(Localization.instance.Get("TRANG_BI_EFF_TANG_NOI_DES"), p);
			break;
		case UserInfo.TrangBiData.TRANG_BI_EFF.CHINH_XAC:
			result = string.Format(Localization.instance.Get("TRANG_BI_EFF_CHINH_XAC_DES"), p);
			break;
		case UserInfo.TrangBiData.TRANG_BI_EFF.KHANG_BAO:
			result = string.Format(Localization.instance.Get("TRANG_BI_EFF_KHANG_BAO_DES"), p);
			break;
		case UserInfo.TrangBiData.TRANG_BI_EFF.STUN:
			result = string.Format(Localization.instance.Get("TRANG_BI_EFF_STUN_DES"), p);
			break;
		case UserInfo.TrangBiData.TRANG_BI_EFF.PHONG_CHIEU:
			result = string.Format(Localization.instance.Get("TRANG_BI_EFF_PHONG_CHIEU_DES"), p);
			break;
		case UserInfo.TrangBiData.TRANG_BI_EFF.DINH_THAN:
			result = string.Format(Localization.instance.Get("TRANG_BI_EFF_DINH_THAN_DES"), p);
			break;
		case UserInfo.TrangBiData.TRANG_BI_EFF.BA_THE:
			result = string.Format(Localization.instance.Get("TRANG_BI_EFF_BA_THE_DES"), p);
			break;
		case UserInfo.TrangBiData.TRANG_BI_EFF.HO_THE:
			result = string.Format(Localization.instance.Get("TRANG_BI_EFF_HO_THE_DES"), p);
			break;
		case UserInfo.TrangBiData.TRANG_BI_EFF.HUT_MAU:
			result = string.Format(Localization.instance.Get("TRANG_BI_EFF_HUT_MAU_DES"), p);
			break;
		case UserInfo.TrangBiData.TRANG_BI_EFF.HOI_SINH:
			result = string.Format(Localization.instance.Get("TRANG_BI_EFF_HOI_SINH_DES"), p);
			break;
		case UserInfo.TrangBiData.TRANG_BI_EFF.BURN_MANA:
			result = string.Format(Localization.instance.Get("TRANG_BI_EFF_BURN_MANA_DES"), p);
			break;
		}
		return result;
	}
}
