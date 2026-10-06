using System.Collections.Generic;
using UnityEngine;

public class PopupNhanVat : MonoBehaviour
{
	public NhanVatAvatar nhanvatAvatar;

	public UILabel nhanVatName;

	public UILabel MenhLabel;

	public UILabel NgoaiLabel;

	public UILabel ThanPhapLabel;

	public UILabel KhiLabel;

	public UILabel NhanVatDesc;

	public UILabel NhanVatDuyen;

	public GameObject DoiHinhBtnGroup;

	public GameObject CloseBtn;

	public OtherAvatar voCongDefault;

	public UIButton btnDuyenDetail;

	private UserInfo RefUserInfo;

	private UserInfo.HeroData mHeroData;

	private NhanVatCfg mHeroConfig;

	public UISprite spPham;

	private bool isOpenByVoLamPho;

	public GameObject groupBatQuaiInfo;

	public GameObject groupNormalInfo;

	public OtherAvatar boPhapAvaBatQuai;

	public OtherAvatar noiCongAvaBatQuai;

	public OtherAvatar trangBiAvaBatQuai;

	public UILabel chisoBoPhapBQ;

	public UILabel chisoNoiCongBQ;

	public UILabel chisoTrangBiBQ;

	public UISprite iconBoPhapBQ;

	public UISprite iconNoiCongBQ;

	public UISprite iconTrangBiBQ;

	private int currentIndexBatQuai = -1;

	public UISprite spBGPopUp;

	public UISprite spBGDuyenGrp;

	public UISprite spBGInfo;

	public UISprite spBGInfoBatQuai;

	public UILabel lbThienPhuTitle;

	public UILabel lbBoPhapTitle;

	public UILabel lbVoCongTitle;

	public UILabel lbTrangBiTitle;

	private int CUONG_HOA_BQ_TRANG_BI_LEVEL = 7;

	private int CUONG_HOA_BQ_BO_PHAP_LEVEL = 8;

	private int CUONG_HOA_BQ_NOI_CONG_LEVEL = 9;

	private int cuongHoaCurSlot = -1;

	private int TrangBiID;

	private int BoPhapID;

	private int NoiCongID;

	private static ChiSoCoBan chisoHoTroBatQuai = ChiSoCoBan.None;

	public static PopupNhanVat instance;

	private void Start()
	{
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

	private static void Create(UserInfo ref_user_info, UserInfo.HeroData data, bool isOpenByScreenDoiHinh, bool isActiveDuyen = false, bool isShowAnother = false, bool isOpenByBatQuai = false, int indexInDoiHinhBQ = -1)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupNhanVat"))).GetComponent<PopupNhanVat>();
		PopupManager.instance.Add(instance.gameObject);
		instance.RefUserInfo = ref_user_info;
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.Set(data, isOpenByScreenDoiHinh, isActiveDuyen, isShowAnother, isOpenByBatQuai, indexInDoiHinhBQ);
		NGUITools.SetActive(instance.DoiHinhBtnGroup, false);
	}

	public void Set(UserInfo.HeroData data, bool isOpenByScreenDoiHinh, bool isDisplayActiveDuyen = false, bool isShowAnother = false, bool isOpenByBatQuai = false, int indexInDoiHinhBQ = -1)
	{
		currentIndexBatQuai = -1;
		mHeroData = data;
		nhanvatAvatar.Set(data);
		groupBatQuaiInfo.gameObject.SetActive(false);
		groupNormalInfo.gameObject.SetActive(true);
		if (!isOpenByScreenDoiHinh)
		{
			btnDuyenDetail.gameObject.SetActive(false);
		}
		if (data.ChuyenSinh > 0)
		{
			spBGDuyenGrp.spriteName = "khung_trong";
			spBGInfo.spriteName = "khung_trong";
			spBGInfoBatQuai.spriteName = "khung_trong";
			spBGPopUp.spriteName = "khung_giay_2";
			lbThienPhuTitle.color = Utils.MakeColor(255, 255, 255);
			lbBoPhapTitle.color = Utils.MakeColor(255, 255, 255);
			lbVoCongTitle.color = Utils.MakeColor(255, 255, 255);
			lbTrangBiTitle.color = Utils.MakeColor(255, 255, 255);
		}
		else
		{
			spBGDuyenGrp.spriteName = "bgr_popup";
			spBGInfo.spriteName = "bgr_popup";
			spBGInfoBatQuai.spriteName = "bgr_popup";
			spBGPopUp.spriteName = "khung_giay";
			lbThienPhuTitle.color = Utils.MakeColor(142, 7, 7);
			lbBoPhapTitle.color = Utils.MakeColor(142, 7, 7);
			lbVoCongTitle.color = Utils.MakeColor(142, 7, 7);
			lbTrangBiTitle.color = Utils.MakeColor(142, 7, 7);
		}
		NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[data.Name];
		nhanVatName.text = nhanVatCfg.TenHienThi;
		if (nhanVatCfg.Hang == 3)
		{
			spPham.spriteName = "giap";
		}
		else if (nhanVatCfg.Hang == 2)
		{
			spPham.spriteName = "at";
		}
		else if (nhanVatCfg.Hang == 1)
		{
			spPham.spriteName = "binh";
		}
		if (isOpenByBatQuai)
		{
			groupBatQuaiInfo.gameObject.SetActive(true);
			groupNormalInfo.gameObject.SetActive(false);
			chisoBoPhapBQ.text = string.Empty;
			chisoNoiCongBQ.text = string.Empty;
			chisoTrangBiBQ.text = string.Empty;
			iconBoPhapBQ.gameObject.SetActive(false);
			iconNoiCongBQ.gameObject.SetActive(false);
			iconTrangBiBQ.gameObject.SetActive(false);
			currentIndexBatQuai = indexInDoiHinhBQ;
			cuongHoaCurSlot = ConfigManager.instance.GetBatQuaiCuongHoaLevel(indexInDoiHinhBQ, RefUserInfo.DoiHinh);
			boPhapAvaBatQuai.Set("lock");
			noiCongAvaBatQuai.Set("lock");
			trangBiAvaBatQuai.Set("lock");
			int HeroID = RefUserInfo.DoiHinh.ListHoTro[indexInDoiHinhBQ];
			UserInfo.HeroData heroData = RefUserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == HeroID);
			if (cuongHoaCurSlot >= CUONG_HOA_BQ_TRANG_BI_LEVEL)
			{
				if (heroData != null)
				{
					if (heroData.VuKhiID > 0)
					{
						TrangBiID = heroData.VuKhiID;
					}
					else if (heroData.MuID > 0)
					{
						TrangBiID = heroData.MuID;
					}
					else if (heroData.AoGiapID > 0)
					{
						TrangBiID = heroData.AoGiapID;
					}
					else if (heroData.TrangSucID > 0)
					{
						TrangBiID = heroData.TrangSucID;
					}
				}
				if (TrangBiID > 0)
				{
					UserInfo.TrangBiData trangBiData = RefUserInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.ID == TrangBiID);
					if (trangBiData != null)
					{
						trangBiAvaBatQuai.Set(trangBiData);
						TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[trangBiData.Name];
						switch (chisoHoTroBatQuai)
						{
						case ChiSoCoBan.Menh:
							chisoTrangBiBQ.text = "+" + trangBiCfg.GetFullChiSo(trangBiData)[0];
							break;
						case ChiSoCoBan.Ngoai:
							chisoTrangBiBQ.text = "+" + trangBiCfg.GetFullChiSo(trangBiData)[1];
							break;
						case ChiSoCoBan.ThanPhap:
							chisoTrangBiBQ.text = "+" + trangBiCfg.GetFullChiSo(trangBiData)[2];
							break;
						case ChiSoCoBan.Noi:
							chisoTrangBiBQ.text = "+" + trangBiCfg.GetFullChiSo(trangBiData)[3];
							break;
						}
						iconTrangBiBQ.spriteName = GetSpriteNameChiSoTrangBi(chisoHoTroBatQuai);
						iconTrangBiBQ.gameObject.SetActive(true);
					}
				}
				else
				{
					trangBiAvaBatQuai.Set("plus");
					chisoTrangBiBQ.text = string.Empty;
				}
			}
			if (cuongHoaCurSlot >= CUONG_HOA_BQ_BO_PHAP_LEVEL)
			{
				BoPhapID = ((heroData != null) ? heroData.VoCong3ID : 0);
				if (BoPhapID > 0)
				{
					UserInfo.VoCongData voCongData = RefUserInfo.VoCongList.Find((UserInfo.VoCongData e) => e.ID == BoPhapID);
					if (voCongData != null)
					{
						CfgVoCong cfgVoCong = ConfigManager.instance.m_dicVCs[voCongData.Name];
						boPhapAvaBatQuai.Set(voCongData);
						chisoBoPhapBQ.text = "+" + cfgVoCong.GetBatQuaiChiSo(voCongData.Level) + "%";
						iconBoPhapBQ.spriteName = GetSpriteNameThuocTinh(cfgVoCong.m_BatQuaiType);
						iconBoPhapBQ.gameObject.SetActive(true);
					}
				}
				else
				{
					boPhapAvaBatQuai.Set("plus");
					chisoBoPhapBQ.text = string.Empty;
				}
			}
			if (cuongHoaCurSlot >= CUONG_HOA_BQ_NOI_CONG_LEVEL)
			{
				NoiCongID = ((heroData != null) ? heroData.VoCong4ID : 0);
				if (NoiCongID > 0)
				{
					UserInfo.VoCongData voCongData2 = RefUserInfo.VoCongList.Find((UserInfo.VoCongData e) => e.ID == NoiCongID);
					if (voCongData2 != null)
					{
						noiCongAvaBatQuai.Set(voCongData2);
						CfgVoCong cfgVoCong2 = ConfigManager.instance.m_dicVCs[voCongData2.Name];
						chisoNoiCongBQ.text = "+" + cfgVoCong2.GetBatQuaiChiSo(voCongData2.Level) + "%";
						iconNoiCongBQ.spriteName = GetSpriteNameThuocTinh(cfgVoCong2.m_BatQuaiType);
						iconNoiCongBQ.gameObject.SetActive(true);
					}
				}
				else
				{
					noiCongAvaBatQuai.Set("plus");
					chisoNoiCongBQ.text = string.Empty;
				}
			}
		}
		else
		{
			groupBatQuaiInfo.gameObject.SetActive(false);
			groupNormalInfo.gameObject.SetActive(true);
			NhanVatDesc.text = nhanVatCfg.Mota;
			voCongDefault.Set(nhanVatCfg.VoCongMacDinh.ToString());
		}
		List<NhanVatCfg.DuyenPhanCfg> duyenPhan = nhanVatCfg.DuyenPhan;
		BattleGamerInfo battleGamerData = BattleGamerInfo.GetBattleGamerData(RefUserInfo);
		if ((isOpenByScreenDoiHinh | isShowAnother) && data.HID > 0)
		{
			if (RefUserInfo.DoiHinh.ListRaTran.Contains(data.HID))
			{
				ChiSoCoSo chiSoRaTran = BattleChiSoHero.GetChiSoRaTran(data.HID, battleGamerData);
				MenhLabel.text = Mathf.RoundToInt(chiSoRaTran.Menh).ToString();
				NgoaiLabel.text = Mathf.RoundToInt(chiSoRaTran.Ngoai).ToString();
				ThanPhapLabel.text = Mathf.RoundToInt(chiSoRaTran.ThanPhap).ToString();
				KhiLabel.text = Mathf.RoundToInt(chiSoRaTran.Noi).ToString();
			}
			else
			{
				ChiSoCoSo chiSoBatQuaiTran = BattleChiSoHero.GetChiSoBatQuaiTran(data.HID, battleGamerData);
				MenhLabel.text = Mathf.RoundToInt(chiSoBatQuaiTran.Menh).ToString();
				NgoaiLabel.text = Mathf.RoundToInt(chiSoBatQuaiTran.Ngoai).ToString();
				ThanPhapLabel.text = Mathf.RoundToInt(chiSoBatQuaiTran.ThanPhap).ToString();
				KhiLabel.text = Mathf.RoundToInt(chiSoBatQuaiTran.Noi).ToString();
			}
		}
		else if (data.ChiSoGoc != null)
		{
			MenhLabel.text = data.ChiSoGoc.Menh.ToString();
			NgoaiLabel.text = data.ChiSoGoc.Ngoai.ToString();
			ThanPhapLabel.text = data.ChiSoGoc.ThanPhap.ToString();
			KhiLabel.text = data.ChiSoGoc.Noi.ToString();
		}
		else
		{
			MenhLabel.text = nhanVatCfg.Menh.ToString();
			NgoaiLabel.text = nhanVatCfg.Ngoai.ToString();
			ThanPhapLabel.text = nhanVatCfg.ThanPhap.ToString();
			KhiLabel.text = nhanVatCfg.Noi.ToString();
		}
		if (duyenPhan == null || duyenPhan.Count <= 0)
		{
			return;
		}
		string text = string.Empty;
		for (int num = 0; num < duyenPhan.Count; num++)
		{
			bool flag = data.ChuyenSinh > 0 && num != duyenPhan.Count - 1;
			bool flag2 = BattleChiSoHero.CheckActiveDuyen(data.HID, battleGamerData, duyenPhan[num], num + 1);
			bool flag3 = false;
			if (data.BeQuan == 0 && num == 5 && !flag2)
			{
				flag3 = true;
				text = ((data.ChuyenSinh <= 0) ? (text + "[515151]") : (text + "[757575]"));
			}
			if ((isOpenByScreenDoiHinh | isDisplayActiveDuyen) & flag2)
			{
				text += "[FFEE00]";
			}
			text = text + duyenPhan[num].TenHienThi + ": ";
			if (duyenPhan[num].LoaiDuyenPhan == LoaiDuyenPhan.CungDoi)
			{
				text = text + Localization.instance.Get("CungDoiDuyenPhanLabel") + " ";
				if (duyenPhan[num].DoiTuong != null && duyenPhan[num].DoiTuong.Count > 0)
				{
					for (int num2 = 0; num2 < duyenPhan[num].DoiTuong.Count; num2++)
					{
						NhanVatCfg nhanVatCfg2 = ConfigManager.instance.m_dicNhanVats[duyenPhan[num].DoiTuong[num2]];
						if (nhanVatCfg2 != null)
						{
							text = ((num2 == duyenPhan[num].DoiTuong.Count - 1) ? (((flag2 & isOpenByScreenDoiHinh) | isDisplayActiveDuyen | flag3) ? (text + nhanVatCfg2.TenHienThi + " ") : (text + "[FF0000]" + nhanVatCfg2.TenHienThi + "[-] ")) : (((flag2 & isOpenByScreenDoiHinh) | isDisplayActiveDuyen | flag3) ? (text + nhanVatCfg2.TenHienThi + ", ") : (text + "[FF0000]" + nhanVatCfg2.TenHienThi + "[-], ")));
						}
					}
				}
			}
			else if (duyenPhan[num].LoaiDuyenPhan == LoaiDuyenPhan.TrangBiDo)
			{
				text = text + Localization.instance.Get("TrangBiDoDuyenPhanLabel") + " ";
				if (duyenPhan[num].DoiTuong != null && duyenPhan[num].DoiTuong.Count > 0)
				{
					for (int num3 = 0; num3 < duyenPhan[num].DoiTuong.Count; num3++)
					{
						TrangBiCfg trangBiCfg2 = ConfigManager.instance.m_dicTrangBi[duyenPhan[num].DoiTuong[num3]];
						if (trangBiCfg2 != null)
						{
							if (num3 == duyenPhan[num].DoiTuong.Count - 1)
							{
								text = (((flag2 & isOpenByScreenDoiHinh) | isDisplayActiveDuyen | flag3) ? (text + trangBiCfg2.TenHienThi + " ") : (text + "[FF0000]" + trangBiCfg2.TenHienThi + "[-] "));
							}
							else if ((!flag2 || !isOpenByScreenDoiHinh) && !isDisplayActiveDuyen && !flag3)
							{
								string text2 = text;
								text = text2 + "[FF0000]" + trangBiCfg2.TenHienThi + " [-]" + Localization.instance.Get("OrMess") + "[-] ";
							}
							else
							{
								string text3 = text;
								text = text3 + trangBiCfg2.TenHienThi + " " + Localization.instance.Get("OrMess") + " ";
							}
						}
					}
				}
			}
			string text4 = "[FF0000]";
			float num4 = 1f;
			if (flag)
			{
				num4 = (float)ConfigManager.instance.OtherConfig.ChuyenSinhConfig.HeSoDuyen / 100f;
				text4 = "[21DD49]";
			}
			if ((!flag2 || !isOpenByScreenDoiHinh) && !isDisplayActiveDuyen && !flag3)
			{
				string text5 = text;
				text = text5 + Localization.instance.Get("DuocNhanMess") + " + " + text4 + num4 * duyenPhan[num].HeSo + "% [-]";
			}
			else if (flag2 & flag)
			{
				string text6 = text;
				text = text6 + Localization.instance.Get("DuocNhanMess") + " + " + text4 + num4 * duyenPhan[num].HeSo + "% [-]";
			}
			else
			{
				string text7 = text;
				text = text7 + Localization.instance.Get("DuocNhanMess") + " + " + num4 * duyenPhan[num].HeSo + "% ";
			}
			switch (duyenPhan[num].ChiSoDuyen)
			{
			case ChiSoDuyenPhan.Khi:
				text += Localization.instance.Get("NoiTitle");
				break;
			case ChiSoDuyenPhan.Menh:
				text += Localization.instance.Get("MenhTitle");
				break;
			case ChiSoDuyenPhan.Ngoai:
				text += Localization.instance.Get("NgoaiTitle");
				break;
			case ChiSoDuyenPhan.ThanPhap:
				text += Localization.instance.Get("ThanPhapTitle");
				break;
			case ChiSoDuyenPhan.DoDon:
				text += Localization.instance.Get("DoDonTitle");
				break;
			case ChiSoDuyenPhan.Ne:
				text += Localization.instance.Get("NeTitle");
				break;
			case ChiSoDuyenPhan.Bao:
				text += Localization.instance.Get("BaoTitle");
				break;
			}
			text += ".[-]\n";
		}
		NhanVatDuyen.text = text;
	}

	private string GetSpriteNameThuocTinh(OtherCfg.NguyenKhiType type)
	{
		switch (type)
		{
		case OtherCfg.NguyenKhiType.MENH:
			return "icon_menh";
		case OtherCfg.NguyenKhiType.NGOAI:
			return "icon_ngoai";
		case OtherCfg.NguyenKhiType.THAN:
			return "icon_than";
		case OtherCfg.NguyenKhiType.KHI:
			return "icon_noi_goc";
		default:
			return string.Empty;
		}
	}

	private string GetSpriteNameThuocTinhTrangBi(string trangBiName)
	{
		switch (TrangBiCfg.GetLoaiTrangBi(trangBiName))
		{
		case LoaiTrangBi.TrangSuc:
			return "icon_menh";
		case LoaiTrangBi.VuKhi:
			return "icon_ngoai";
		case LoaiTrangBi.AoGiap:
			return "icon_than";
		case LoaiTrangBi.Mu:
			return "icon_noi_goc";
		default:
			return string.Empty;
		}
	}

	private string GetSpriteNameChiSoTrangBi(ChiSoCoBan chiso)
	{
		switch (chiso)
		{
		case ChiSoCoBan.Menh:
			return "icon_menh";
		case ChiSoCoBan.Ngoai:
			return "icon_ngoai";
		case ChiSoCoBan.ThanPhap:
			return "icon_than";
		case ChiSoCoBan.Noi:
			return "icon_noi_goc";
		default:
			return string.Empty;
		}
	}

	public void updateNhanVatBatQuaiView()
	{
		RefUserInfo = GameManager.instance.m_GameClient.UserInfo;
		boPhapAvaBatQuai.Set("lock");
		noiCongAvaBatQuai.Set("lock");
		trangBiAvaBatQuai.Set("lock");
		chisoBoPhapBQ.text = string.Empty;
		chisoNoiCongBQ.text = string.Empty;
		chisoTrangBiBQ.text = string.Empty;
		iconBoPhapBQ.gameObject.SetActive(false);
		iconNoiCongBQ.gameObject.SetActive(false);
		iconTrangBiBQ.gameObject.SetActive(false);
		int HeroID = RefUserInfo.DoiHinh.ListHoTro[currentIndexBatQuai];
		UserInfo.HeroData heroData = RefUserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == HeroID);
		BattleGamerInfo battleGamerData = BattleGamerInfo.GetBattleGamerData(RefUserInfo);
		ChiSoCoSo chiSoBatQuaiTran = BattleChiSoHero.GetChiSoBatQuaiTran(HeroID, battleGamerData);
		MenhLabel.text = Mathf.RoundToInt(chiSoBatQuaiTran.Menh).ToString();
		NgoaiLabel.text = Mathf.RoundToInt(chiSoBatQuaiTran.Ngoai).ToString();
		ThanPhapLabel.text = Mathf.RoundToInt(chiSoBatQuaiTran.ThanPhap).ToString();
		KhiLabel.text = Mathf.RoundToInt(chiSoBatQuaiTran.Noi).ToString();
		if (cuongHoaCurSlot >= CUONG_HOA_BQ_TRANG_BI_LEVEL)
		{
			if (heroData != null)
			{
				if (heroData.VuKhiID > 0)
				{
					TrangBiID = heroData.VuKhiID;
				}
				else if (heroData.MuID > 0)
				{
					TrangBiID = heroData.MuID;
				}
				else if (heroData.AoGiapID > 0)
				{
					TrangBiID = heroData.AoGiapID;
				}
				else if (heroData.TrangSucID > 0)
				{
					TrangBiID = heroData.TrangSucID;
				}
			}
			if (TrangBiID > 0)
			{
				UserInfo.TrangBiData trangBiData = RefUserInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.ID == TrangBiID);
				if (trangBiData != null)
				{
					trangBiAvaBatQuai.Set(trangBiData);
					TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[trangBiData.Name];
					switch (chisoHoTroBatQuai)
					{
					case ChiSoCoBan.Menh:
						chisoTrangBiBQ.text = "+" + trangBiCfg.GetFullChiSo(trangBiData)[0];
						break;
					case ChiSoCoBan.Ngoai:
						chisoTrangBiBQ.text = "+" + trangBiCfg.GetFullChiSo(trangBiData)[1];
						break;
					case ChiSoCoBan.ThanPhap:
						chisoTrangBiBQ.text = "+" + trangBiCfg.GetFullChiSo(trangBiData)[2];
						break;
					case ChiSoCoBan.Noi:
						chisoTrangBiBQ.text = "+" + trangBiCfg.GetFullChiSo(trangBiData)[3];
						break;
					}
					iconTrangBiBQ.spriteName = GetSpriteNameChiSoTrangBi(chisoHoTroBatQuai);
					iconTrangBiBQ.gameObject.SetActive(true);
				}
			}
			else
			{
				trangBiAvaBatQuai.Set("plus");
				chisoTrangBiBQ.text = string.Empty;
			}
		}
		if (cuongHoaCurSlot >= CUONG_HOA_BQ_BO_PHAP_LEVEL)
		{
			BoPhapID = ((heroData != null) ? heroData.VoCong3ID : 0);
			if (BoPhapID > 0)
			{
				UserInfo.VoCongData voCongData = RefUserInfo.VoCongList.Find((UserInfo.VoCongData e) => e.ID == BoPhapID);
				if (voCongData != null)
				{
					boPhapAvaBatQuai.Set(voCongData);
					CfgVoCong cfgVoCong = ConfigManager.instance.m_dicVCs[voCongData.Name];
					chisoBoPhapBQ.text = "+" + cfgVoCong.GetBatQuaiChiSo(voCongData.Level) + "%";
					iconBoPhapBQ.spriteName = GetSpriteNameThuocTinh(cfgVoCong.m_BatQuaiType);
					iconBoPhapBQ.gameObject.SetActive(true);
				}
			}
			else
			{
				boPhapAvaBatQuai.Set("plus");
				chisoBoPhapBQ.text = string.Empty;
			}
		}
		if (cuongHoaCurSlot < CUONG_HOA_BQ_NOI_CONG_LEVEL)
		{
			return;
		}
		NoiCongID = ((heroData != null) ? heroData.VoCong4ID : 0);
		if (NoiCongID > 0)
		{
			UserInfo.VoCongData voCongData2 = RefUserInfo.VoCongList.Find((UserInfo.VoCongData e) => e.ID == NoiCongID);
			if (voCongData2 != null)
			{
				noiCongAvaBatQuai.Set(voCongData2);
				CfgVoCong cfgVoCong2 = ConfigManager.instance.m_dicVCs[voCongData2.Name];
				chisoNoiCongBQ.text = "+" + cfgVoCong2.GetBatQuaiChiSo(voCongData2.Level) + "%";
				iconNoiCongBQ.spriteName = GetSpriteNameThuocTinh(cfgVoCong2.m_BatQuaiType);
				iconNoiCongBQ.gameObject.SetActive(true);
			}
		}
		else
		{
			noiCongAvaBatQuai.Set("plus");
			chisoNoiCongBQ.text = string.Empty;
		}
	}

	public bool IsReadOnlyMode()
	{
		if (RefUserInfo == null || RefUserInfo.Gamer.ID == GameManager.instance.m_GameClient.UserInfo.Gamer.ID)
		{
			return false;
		}
		return true;
	}

	public void OnBoPhapBatQuaiClick()
	{
		if (IsReadOnlyMode())
		{
			if (BoPhapID > 0)
			{
				UserInfo.VoCongData voCongData = RefUserInfo.VoCongList.Find((UserInfo.VoCongData e) => e.ID == BoPhapID);
				if (voCongData != null)
				{
					PopupVoCong.CreateByNormalScreen(RefUserInfo, voCongData, voCongData.Level);
				}
			}
		}
		else if (cuongHoaCurSlot < CUONG_HOA_BQ_BO_PHAP_LEVEL)
		{
			MessagePopup.Create(Localization.instance.Get("ThongBaoMoBoPhapBatQuai"));
		}
		else
		{
			if (cuongHoaCurSlot > ConfigManager.instance.OtherConfig.GetMaxBatQuaiCuongHoa())
			{
				return;
			}
			List<int> list = new List<int>();
			if (mHeroData != null)
			{
				list.Add(mHeroData.VoCong2ID);
				list.Add(mHeroData.VoCong3ID);
				list.Add(mHeroData.VoCong4ID);
			}
			for (int num = 0; num < GameManager.instance.m_GameClient.UserInfo.VoCongList.Count; num++)
			{
				CfgVoCong cfgVoCong = ConfigManager.instance.m_dicVCs[GameManager.instance.m_GameClient.UserInfo.VoCongList[num].Name];
				if (cfgVoCong.Hang < 3)
				{
					list.Add(GameManager.instance.m_GameClient.UserInfo.VoCongList[num].ID);
				}
			}
			PopupSelectVoCong.Create(OnSelectBoPhap, list, VCClass.BO_PHAP, false, Localization.instance.Get("ChonBoPhapTitle"), true);
		}
	}

	public void OnNoiCongBatQuaiClick()
	{
		if (IsReadOnlyMode())
		{
			if (NoiCongID > 0)
			{
				UserInfo.VoCongData voCongData = RefUserInfo.VoCongList.Find((UserInfo.VoCongData e) => e.ID == NoiCongID);
				if (voCongData != null)
				{
					PopupVoCong.CreateByNormalScreen(RefUserInfo, voCongData, voCongData.Level);
				}
			}
		}
		else if (cuongHoaCurSlot < CUONG_HOA_BQ_NOI_CONG_LEVEL)
		{
			MessagePopup.Create(Localization.instance.Get("ThongBaoMoNoiCongBatQuai"));
		}
		else
		{
			if (cuongHoaCurSlot > ConfigManager.instance.OtherConfig.GetMaxBatQuaiCuongHoa())
			{
				return;
			}
			List<int> list = new List<int>();
			if (mHeroData != null)
			{
				list.Add(mHeroData.VoCong2ID);
				list.Add(mHeroData.VoCong3ID);
				list.Add(mHeroData.VoCong4ID);
			}
			for (int num = 0; num < GameManager.instance.m_GameClient.UserInfo.VoCongList.Count; num++)
			{
				CfgVoCong cfgVoCong = ConfigManager.instance.m_dicVCs[GameManager.instance.m_GameClient.UserInfo.VoCongList[num].Name];
				if (cfgVoCong.Hang < 3)
				{
					list.Add(GameManager.instance.m_GameClient.UserInfo.VoCongList[num].ID);
				}
			}
			PopupSelectVoCong.Create(OnSelectNoiCong, list, VCClass.NOI_CONG, false, Localization.instance.Get("ChonNoiCongTitle"), true);
		}
	}

	public void OnTrangBiBatQuaiClick()
	{
		if (IsReadOnlyMode())
		{
			if (TrangBiID > 0)
			{
				UserInfo.TrangBiData trangBiData = RefUserInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.ID == TrangBiID);
				if (trangBiData != null)
				{
					PopupTrangBi.CreateByNormalScreen(trangBiData);
				}
			}
		}
		else if (cuongHoaCurSlot < CUONG_HOA_BQ_TRANG_BI_LEVEL)
		{
			MessagePopup.Create(Localization.instance.Get("ThongBaoMoTrangBiBatQuai"));
		}
		else if (cuongHoaCurSlot <= ConfigManager.instance.OtherConfig.GetMaxBatQuaiCuongHoa())
		{
			List<int> list = new List<int>();
			if (mHeroData != null)
			{
				list.Add(mHeroData.VuKhiID);
				list.Add(mHeroData.MuID);
				list.Add(mHeroData.AoGiapID);
				list.Add(mHeroData.TrangSucID);
			}
			PopupSelectTrangBi.Create(OnSelectTrangBi, list, LoaiTrangBi.None, Localization.instance.Get("PopupSelectTrangBiTitle"), chisoHoTroBatQuai);
		}
	}

	public bool OnSelectBoPhap(int id)
	{
		if (id > 0 && mHeroData != null && currentIndexBatQuai > -1)
		{
			GameManager.instance.m_GameClient.RequestSetVoCong(mHeroData.HID, id);
		}
		return true;
	}

	public bool OnSelectNoiCong(int id)
	{
		if (id > 0 && mHeroData != null && currentIndexBatQuai > -1)
		{
			GameManager.instance.m_GameClient.RequestSetVoCong(mHeroData.HID, id);
		}
		return true;
	}

	public bool OnSelectTrangBi(int id)
	{
		if (id > 0 && mHeroData != null && currentIndexBatQuai > -1)
		{
			GameManager.instance.m_GameClient.RequestSetTrangBi(mHeroData.HID, id);
		}
		return true;
	}

	public static void CreateByScreenDoiHinh(UserInfo ref_user, UserInfo.HeroData data)
	{
		Create(ref_user, data, true);
		NGUITools.SetActive(instance.CloseBtn, false);
		NGUITools.SetActive(instance.DoiHinhBtnGroup, true);
	}

	public static void CreateInBatQuaiTranView(UserInfo ref_user, UserInfo.HeroData data, int indexDoiHinhHT, ChiSoCoBan curChiSo)
	{
		chisoHoTroBatQuai = curChiSo;
		Create(ref_user, data, true, false, false, true, indexDoiHinhHT);
		NGUITools.SetActive(instance.CloseBtn, false);
		NGUITools.SetActive(instance.DoiHinhBtnGroup, true);
	}

	public static void CreateToViewAnotherUser(UserInfo ref_user, UserInfo.HeroData data, bool isActiveDuyen = false, bool isOpenByBatQuai = false, int indexDoiHinhHT = -1, ChiSoCoBan curChiSo = ChiSoCoBan.None)
	{
		chisoHoTroBatQuai = curChiSo;
		Create(ref_user, data, false, true, true, isOpenByBatQuai, indexDoiHinhHT);
		NGUITools.SetActive(instance.DoiHinhBtnGroup, false);
		NGUITools.SetActive(instance.CloseBtn, true);
	}

	public static void CreateByNormalScreens(UserInfo ref_user, UserInfo.HeroData data, bool isActiveDuyen = false)
	{
		Create(ref_user, data, false, isActiveDuyen);
		NGUITools.SetActive(instance.DoiHinhBtnGroup, false);
		NGUITools.SetActive(instance.CloseBtn, true);
	}

	public static void CreateByNhanVatAvatar(NhanVatCfg cfgData)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupNhanVat"))).GetComponent<PopupNhanVat>();
		PopupManager.instance.Add(instance.gameObject);
		instance.RefUserInfo = GameManager.instance.m_GameClient.UserInfo;
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.Set(cfgData);
		NGUITools.SetActive(instance.DoiHinhBtnGroup, false);
		NGUITools.SetActive(instance.CloseBtn, true);
		NGUITools.SetActive(instance.btnDuyenDetail.gameObject, false);
	}

	public static void CreateByVoLamPhoScreen(NhanVatCfg cfgData)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupNhanVat"))).GetComponent<PopupNhanVat>();
		PopupManager.instance.Add(instance.gameObject);
		instance.RefUserInfo = GameManager.instance.m_GameClient.UserInfo;
		instance.isOpenByVoLamPho = true;
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.Set(cfgData);
		NGUITools.SetActive(instance.DoiHinhBtnGroup, false);
		NGUITools.SetActive(instance.CloseBtn, true);
		NGUITools.SetActive(instance.btnDuyenDetail.gameObject, true);
	}

	public void OnBoiDuongClick()
	{
		if (mHeroData != null)
		{
			ScreenBoiDuong screenBoiDuong = GUIManager.getScreen(GAME_SCREEN.ScreenBoiDuong) as ScreenBoiDuong;
			screenBoiDuong.Set(mHeroData);
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenBoiDuong);
		}
		DestroyPopup();
	}

	public void OnDoiClick()
	{
		ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
		List<int> ignoreList = screenDoiHinh.getIgnoreList();
		if (NGUITools.GetActive(screenDoiHinh.BatQuaiGroup.gameObject))
		{
			PopupSelectNhanVat.Create(screenDoiHinh.OnChangeDoiHinhSupport, ignoreList);
		}
		else if (NGUITools.GetActive(screenDoiHinh.DetuGroup.gameObject))
		{
			PopupSelectNhanVat.Create(screenDoiHinh.OnChangeDoiHinh, ignoreList);
		}
		else if (NGUITools.GetActive(screenDoiHinh.ThienCangGroup.gameObject))
		{
			PopupSelectNhanVat.Create(screenDoiHinh.OnChange_ThienCangSupport, ignoreList);
		}
		DestroyPopup();
	}

	private void Set(NhanVatCfg cfgData)
	{
		mHeroConfig = cfgData;
		nhanvatAvatar.Set(cfgData.Name);
		nhanVatName.text = cfgData.TenHienThi;
		spBGDuyenGrp.spriteName = "bgr_popup";
		spBGInfo.spriteName = "bgr_popup";
		spBGInfoBatQuai.spriteName = "bgr_popup";
		spBGPopUp.spriteName = "khung_giay";
		lbThienPhuTitle.color = Utils.MakeColor(142, 7, 7);
		lbBoPhapTitle.color = Utils.MakeColor(142, 7, 7);
		lbVoCongTitle.color = Utils.MakeColor(142, 7, 7);
		lbTrangBiTitle.color = Utils.MakeColor(142, 7, 7);
		groupBatQuaiInfo.gameObject.SetActive(false);
		groupNormalInfo.gameObject.SetActive(true);
		if (cfgData.Hang == 3)
		{
			spPham.spriteName = "giap";
		}
		else if (cfgData.Hang == 2)
		{
			spPham.spriteName = "at";
		}
		else if (cfgData.Hang == 1)
		{
			spPham.spriteName = "binh";
		}
		NhanVatDesc.text = cfgData.Mota;
		voCongDefault.Set(cfgData.VoCongMacDinh.ToString());
		List<NhanVatCfg.DuyenPhanCfg> duyenPhan = cfgData.DuyenPhan;
		BattleGamerInfo battleGamerData = BattleGamerInfo.GetBattleGamerData(RefUserInfo);
		MenhLabel.text = cfgData.Menh.ToString();
		NgoaiLabel.text = cfgData.Ngoai.ToString();
		ThanPhapLabel.text = cfgData.ThanPhap.ToString();
		KhiLabel.text = cfgData.Noi.ToString();
		if (duyenPhan == null || duyenPhan.Count <= 0)
		{
			return;
		}
		string text = string.Empty;
		for (int i = 0; i < duyenPhan.Count; i++)
		{
			text = text + duyenPhan[i].TenHienThi + ": ";
			string text2;
			if (duyenPhan[i].LoaiDuyenPhan == LoaiDuyenPhan.CungDoi)
			{
				text = text + Localization.instance.Get("CungDoiDuyenPhanLabel") + " ";
				if (duyenPhan[i].DoiTuong != null && duyenPhan[i].DoiTuong.Count > 0)
				{
					for (int j = 0; j < duyenPhan[i].DoiTuong.Count; j++)
					{
						NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[duyenPhan[i].DoiTuong[j]];
						if (nhanVatCfg != null)
						{
							text = ((j != duyenPhan[i].DoiTuong.Count - 1) ? (text + nhanVatCfg.TenHienThi + ", ") : (text + nhanVatCfg.TenHienThi + " "));
						}
					}
				}
			}
			else if (duyenPhan[i].LoaiDuyenPhan == LoaiDuyenPhan.TrangBiDo)
			{
				text = text + Localization.instance.Get("TrangBiDoDuyenPhanLabel") + " ";
				if (duyenPhan[i].DoiTuong != null && duyenPhan[i].DoiTuong.Count > 0)
				{
					for (int k = 0; k < duyenPhan[i].DoiTuong.Count; k++)
					{
						TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[duyenPhan[i].DoiTuong[k]];
						if (trangBiCfg != null)
						{
							if (k == duyenPhan[i].DoiTuong.Count - 1)
							{
								text = text + trangBiCfg.TenHienThi + " ";
								continue;
							}
							text2 = text;
							text = text2 + trangBiCfg.TenHienThi + " " + Localization.instance.Get("OrMess") + " ";
						}
					}
				}
			}
			text2 = text;
			text = text2 + Localization.instance.Get("DuocNhanMess") + " + " + duyenPhan[i].HeSo + "% ";
			switch (duyenPhan[i].ChiSoDuyen)
			{
			case ChiSoDuyenPhan.Khi:
				text += Localization.instance.Get("NoiTitle");
				break;
			case ChiSoDuyenPhan.Menh:
				text += Localization.instance.Get("MenhTitle");
				break;
			case ChiSoDuyenPhan.Ngoai:
				text += Localization.instance.Get("NgoaiTitle");
				break;
			case ChiSoDuyenPhan.ThanPhap:
				text += Localization.instance.Get("ThanPhapTitle");
				break;
			case ChiSoDuyenPhan.DoDon:
				text += Localization.instance.Get("DoDonTitle");
				break;
			case ChiSoDuyenPhan.Ne:
				text += Localization.instance.Get("NeTitle");
				break;
			case ChiSoDuyenPhan.Bao:
				text += Localization.instance.Get("BaoTitle");
				break;
			}
			text += ".[-]\n";
		}
		NhanVatDuyen.text = text;
	}

	public void OnVCDefaultClick()
	{
		if (mHeroData != null)
		{
			CfgVoCong cfgVoCong = ConfigManager.instance.m_dicVCs[mHeroData.VoCong1Name.ToString()];
			if (cfgVoCong != null)
			{
				PopupVoCong.CreateByVoLamPhoScreen(cfgVoCong);
			}
		}
		else if (mHeroConfig != null)
		{
			CfgVoCong cfgVoCong2 = ConfigManager.instance.m_dicVCs[mHeroConfig.VoCongMacDinh.ToString()];
			if (cfgVoCong2 != null)
			{
				PopupVoCong.CreateByVoLamPhoScreen(cfgVoCong2);
			}
		}
	}

	public void btnDetail_OnClick()
	{
		DestroyPopup();
		if (isOpenByVoLamPho)
		{
			PopupDuyenNhanVat.Create(mHeroConfig);
		}
		else
		{
			PopupDuyenNhanVat.Create(RefUserInfo, mHeroData);
		}
	}
}
