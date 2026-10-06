using System;
using System.Collections.Generic;

public class BattleGamerInfo
{
	public class DuLieuHBase : ChiSoCoSo
	{
		private string _Name = string.Empty;

		public int BeQuan;

		public int ChuyenSinh;

		public int HID { get; set; }

		public string Name
		{
			get
			{
				return _Name;
			}
			set
			{
				_Name = value;
			}
		}

		public List<BattleVoCong> ListVoCong { get; set; }

		public UserInfo.ChienHon ChienHonData { get; set; }

		public List<UserInfo.HuyenKhi> ListHuyenKhi { get; set; }

		public List<UserInfo.ThienMaLenhInfo> ListThienMaLenh { get; set; }

		public List<BattleTrangBi> ListTrangBi { get; set; }

		public List<string> DuyenHeroList { get; set; }

		public ChiSoNhanVat ChienHonChiSoTangThem()
		{
			ChiSoNhanVat chiSoNhanVat = new ChiSoNhanVat();
			if (ChienHonData != null)
			{
				chiSoNhanVat = ChienHonData.GetChiSoCuoi(ListHuyenKhi);
				if (ChienHonData.LoaiBuff == 0)
				{
					chiSoNhanVat.Ngoai = 0f;
					chiSoNhanVat.ThanPhap = 0f;
					chiSoNhanVat.Noi = 0f;
				}
				else if (ChienHonData.LoaiBuff == 1)
				{
					chiSoNhanVat.Menh = 0f;
					chiSoNhanVat.ThanPhap = 0f;
					chiSoNhanVat.Noi = 0f;
				}
				else if (ChienHonData.LoaiBuff == 2)
				{
					chiSoNhanVat.Ngoai = 0f;
					chiSoNhanVat.Menh = 0f;
					chiSoNhanVat.Noi = 0f;
				}
				else
				{
					chiSoNhanVat.Ngoai = 0f;
					chiSoNhanVat.Menh = 0f;
					chiSoNhanVat.ThanPhap = 0f;
				}
			}
			return chiSoNhanVat;
		}

		public ChiSoNhanVat TrangBiDoChiSoTangThem(bool isBatQuai)
		{
			ChiSoNhanVat chiSoNhanVat = new ChiSoNhanVat();
			if (ListTrangBi != null)
			{
				foreach (BattleTrangBi item in ListTrangBi)
				{
					chiSoNhanVat.Add(item.ChiSoTangThem(isBatQuai));
				}
			}
			return chiSoNhanVat;
		}

		public ChiSoNhanVat TrangBiDoEffectTangThem()
		{
			ChiSoNhanVat chiSoNhanVat = new ChiSoNhanVat();
			if (ListTrangBi != null)
			{
				foreach (BattleTrangBi item in ListTrangBi)
				{
					chiSoNhanVat.Add(item.ChiSoEffect());
				}
			}
			return chiSoNhanVat;
		}

		private bool CheckActiveDuyenHero(List<string> doi_tuong_list)
		{
			if (doi_tuong_list == null || DuyenHeroList == null)
			{
				return false;
			}
			string e;
			foreach (string item in doi_tuong_list)
			{
				e = item;
				string value = DuyenHeroList.Find((string F) => F == e);
				if (string.IsNullOrEmpty(value))
				{
					return false;
				}
			}
			return true;
		}

		private bool CheckActiveDuyenTrangBiDo(List<string> doi_tuong_list)
		{
			if (doi_tuong_list == null || ListTrangBi == null)
			{
				return false;
			}
			string e;
			foreach (string item in doi_tuong_list)
			{
				e = item;
				BattleTrangBi battleTrangBi = ListTrangBi.Find((BattleTrangBi F) => F.Data.Name == e);
				if (battleTrangBi != null)
				{
					return true;
				}
			}
			return false;
		}

		private ChiSoNhanVat GetChiSoDuyen(NhanVatCfg.DuyenPhanCfg e, int duyenNum)
		{
			ChiSoNhanVat chiSoNhanVat = new ChiSoNhanVat();
			float num = 1f;
			if (ChuyenSinh == 1 && duyenNum != 6)
			{
				num = 2.5f;
			}
			switch (e.ChiSoDuyen)
			{
			case ChiSoDuyenPhan.Menh:
				chiSoNhanVat.Menh = base.Menh * num * e.HeSo / 100f;
				break;
			case ChiSoDuyenPhan.Ngoai:
				chiSoNhanVat.Ngoai = base.Ngoai * num * e.HeSo / 100f;
				break;
			case ChiSoDuyenPhan.Khi:
				chiSoNhanVat.Noi = base.Noi * num * e.HeSo / 100f;
				break;
			case ChiSoDuyenPhan.ThanPhap:
				chiSoNhanVat.ThanPhap = base.ThanPhap * num * e.HeSo / 100f;
				break;
			case ChiSoDuyenPhan.Ne:
				chiSoNhanVat.TyleNe = e.HeSo;
				break;
			case ChiSoDuyenPhan.Bao:
				chiSoNhanVat.TyleCrit = e.HeSo;
				chiSoNhanVat.HesoCrit = 2f;
				break;
			case ChiSoDuyenPhan.DoDon:
				chiSoNhanVat.DuyenTyleDoDon = 50f;
				chiSoNhanVat.DuyenHesoDoDon = e.HeSo;
				break;
			}
			return chiSoNhanVat;
		}

		public bool CheckActiveDuyen(NhanVatCfg.DuyenPhanCfg e, int duyen_num)
		{
			if (duyen_num == 6 && BeQuan == 0)
			{
				return false;
			}
			switch (e.LoaiDuyenPhan)
			{
			case LoaiDuyenPhan.CungDoi:
				if (CheckActiveDuyenHero(e.DoiTuong))
				{
					return true;
				}
				break;
			case LoaiDuyenPhan.TrangBiDo:
				if (CheckActiveDuyenTrangBiDo(e.DoiTuong))
				{
					return true;
				}
				break;
			}
			return false;
		}

		public ChiSoNhanVat DuyenPhanChiSoTangThem()
		{
			ChiSoNhanVat chiSoNhanVat = new ChiSoNhanVat();
			NhanVatCfg value = null;
			ConfigManager.instance.m_dicNhanVats.TryGetValue(Name, out value);
			if (value != null)
			{
				int num = 1;
				foreach (NhanVatCfg.DuyenPhanCfg item in value.DuyenPhan)
				{
					if (CheckActiveDuyen(item, num))
					{
						ChiSoNhanVat chiSoDuyen = GetChiSoDuyen(item, num);
						chiSoNhanVat.Add(chiSoDuyen);
					}
					num++;
				}
			}
			return chiSoNhanVat;
		}
	}

	public class DuLieuHHoTro : DuLieuHBase
	{
		public class Costume
		{
			public string CodeName { get; set; }

			public int TinhLuyen { get; set; }

			public int HongNgoc { get; set; }

			public int LamNgoc { get; set; }

			public int HoangNgoc { get; set; }

			public int TuNgoc { get; set; }
		}

		public int Slot { get; set; }

		public float MenhCostumeBuff { get; set; }

		public float NgoaiCostumeBuff { get; set; }

		public float ThanCostumeBuff { get; set; }

		public float KhiCostumeBuff { get; set; }

		public Costume CosData { get; set; }

		public ChiSoNhanVat GetCostumeChiSoTangThem()
		{
			ChiSoNhanVat chiSoNhanVat = new ChiSoNhanVat();
			if (CosData == null)
			{
				return chiSoNhanVat;
			}
			chiSoNhanVat.Cong = CostumeCfg.GetBuffFromNgoc(UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_DO, CosData.HongNgoc);
			chiSoNhanVat.Thu = CostumeCfg.GetBuffFromNgoc(UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_XANH, CosData.LamNgoc);
			chiSoNhanVat.HP = CostumeCfg.GetBuffFromNgoc(UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_VANG, CosData.HoangNgoc);
			chiSoNhanVat.MP = CostumeCfg.GetBuffFromNgoc(UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_TIM, CosData.TuNgoc);
			return chiSoNhanVat;
		}
	}

	public class DuLieuHero : DuLieuHBase
	{
		public class NguyenKhi
		{
			public OtherCfg.NguyenKhiType Type { get; set; }

			public float ChiSo { get; set; }
		}

		public class Costume
		{
			public string CodeName { get; set; }

			public int TinhLuyen { get; set; }

			public int HongNgoc { get; set; }

			public int LamNgoc { get; set; }

			public int HoangNgoc { get; set; }

			public int TuNgoc { get; set; }
		}

		private float _TyleModel = Common.TyleModel;

		private float _HpHeSoBienDoi = 1f;

		private float _HpMaxHeSoBienDoi = 1f;

		public float PosX { get; set; }

		public float PosY { get; set; }

		public float TyleModel
		{
			get
			{
				return _TyleModel;
			}
			set
			{
				_TyleModel = value;
			}
		}

		public UserInfo.HeroData.AI AI { get; set; }

		public float HpHeSoBienDoi
		{
			get
			{
				return _HpHeSoBienDoi;
			}
			set
			{
				_HpHeSoBienDoi = value;
			}
		}

		public float HpMaxHeSoBienDoi
		{
			get
			{
				return _HpMaxHeSoBienDoi;
			}
			set
			{
				_HpMaxHeSoBienDoi = value;
			}
		}

		public List<NguyenKhi> NguyenKhiList { get; set; }

		public float MenhCostumeBuff { get; set; }

		public float NgoaiCostumeBuff { get; set; }

		public float ThanCostumeBuff { get; set; }

		public float KhiCostumeBuff { get; set; }

		public Costume CosData { get; set; }

		public TrangBiCfg GetVKCfg()
		{
			if (base.ListTrangBi != null)
			{
				foreach (BattleTrangBi item in base.ListTrangBi)
				{
					if (item.Class == LoaiTrangBi.VuKhi)
					{
						return item.Config;
					}
				}
			}
			return null;
		}

		public string GetVKName()
		{
			if (base.ListTrangBi != null)
			{
				foreach (BattleTrangBi item in base.ListTrangBi)
				{
					if (item.Class == LoaiTrangBi.VuKhi)
					{
						return item.Data.Name;
					}
				}
			}
			return string.Empty;
		}

		public ChiSoNhanVat VoCongChiSoTangThem(ChiSoNhanVat chi_so_tham_chieu)
		{
			ChiSoNhanVat chiSoNhanVat = new ChiSoNhanVat();
			if (base.ListVoCong != null)
			{
				foreach (BattleVoCong item in base.ListVoCong)
				{
					chiSoNhanVat.Add(item.ChiSoTangThem(chi_so_tham_chieu));
				}
			}
			return chiSoNhanVat;
		}

		public ChiSoNhanVat NguyenKhiChiSoTangThem()
		{
			ChiSoNhanVat chiSoNhanVat = new ChiSoNhanVat();
			if (NguyenKhiList != null)
			{
				foreach (NguyenKhi nguyenKhi in NguyenKhiList)
				{
					switch (nguyenKhi.Type)
					{
					case OtherCfg.NguyenKhiType.NGOAI:
						chiSoNhanVat.Ngoai += nguyenKhi.ChiSo;
						break;
					case OtherCfg.NguyenKhiType.THAN:
						chiSoNhanVat.ThanPhap += nguyenKhi.ChiSo;
						break;
					case OtherCfg.NguyenKhiType.KHI:
						chiSoNhanVat.Noi += nguyenKhi.ChiSo;
						break;
					case OtherCfg.NguyenKhiType.DO_DON:
						chiSoNhanVat.NguyenKhiHesoDoDon += nguyenKhi.ChiSo;
						break;
					case OtherCfg.NguyenKhiType.KHANG_BAO:
						chiSoNhanVat.NguyenKhiHesoKhangBao += nguyenKhi.ChiSo;
						break;
					}
				}
			}
			return chiSoNhanVat;
		}

		public ChiSoNhanVat GetCostumeChiSoTangThem()
		{
			ChiSoNhanVat chiSoNhanVat = new ChiSoNhanVat();
			if (CosData == null)
			{
				return chiSoNhanVat;
			}
			chiSoNhanVat.Cong = CostumeCfg.GetBuffFromNgoc(UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_DO, CosData.HongNgoc);
			chiSoNhanVat.Thu = CostumeCfg.GetBuffFromNgoc(UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_XANH, CosData.LamNgoc);
			chiSoNhanVat.HP = CostumeCfg.GetBuffFromNgoc(UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_VANG, CosData.HoangNgoc);
			chiSoNhanVat.MP = CostumeCfg.GetBuffFromNgoc(UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_TIM, CosData.TuNgoc);
			return chiSoNhanVat;
		}
	}

	private float _MenhHeSoTangThem;

	private float _NgoaiHeSoTangThem;

	private float _ThanHeSoTangThem;

	private float _KhiHeSoTangThem;

	private float _MenhLienMinhHeSoTangThem;

	private float _NgoaiLienMinhHeSoTangThem;

	private float _ThanLienMinhHeSoTangThem;

	private float _KhiLienMinhHeSoTangThem;

	public DateTime ThuCuoiExpired;

	public UserInfo.DoiHinhData RawDoiHinhData;

	public int GID { get; set; }

	public string Name { get; set; }

	public List<DuLieuHero> DoiHinhRaTran { get; set; }

	public List<DuLieuHHoTro> DoiHinhTranDo { get; set; }

	public List<UserInfo.VCThietLapData> ListVCSetting { get; set; }

	public float MenhHeSoTangThem
	{
		get
		{
			return _MenhHeSoTangThem;
		}
		set
		{
			_MenhHeSoTangThem = value;
		}
	}

	public float NgoaiHeSoTangThem
	{
		get
		{
			return _NgoaiHeSoTangThem;
		}
		set
		{
			_NgoaiHeSoTangThem = value;
		}
	}

	public float ThanHeSoTangThem
	{
		get
		{
			return _ThanHeSoTangThem;
		}
		set
		{
			_ThanHeSoTangThem = value;
		}
	}

	public float KhiHeSoTangThem
	{
		get
		{
			return _KhiHeSoTangThem;
		}
		set
		{
			_KhiHeSoTangThem = value;
		}
	}

	public float MenhLienMinhHeSoTangThem
	{
		get
		{
			return _MenhLienMinhHeSoTangThem;
		}
		set
		{
			_MenhLienMinhHeSoTangThem = value;
		}
	}

	public float NgoaiLienMinhHeSoTangThem
	{
		get
		{
			return _NgoaiLienMinhHeSoTangThem;
		}
		set
		{
			_NgoaiLienMinhHeSoTangThem = value;
		}
	}

	public float ThanLienMinhHeSoTangThem
	{
		get
		{
			return _ThanLienMinhHeSoTangThem;
		}
		set
		{
			_ThanLienMinhHeSoTangThem = value;
		}
	}

	public float KhiLienMinhHeSoTangThem
	{
		get
		{
			return _KhiLienMinhHeSoTangThem;
		}
		set
		{
			_KhiLienMinhHeSoTangThem = value;
		}
	}

	public int KhiThe { get; set; }

	public OtherCfg.ThuCuoiCfg ThuCuoiData { get; set; }

	public UserInfo.PetInfo ThanThuData { get; set; }

	private static DuLieuHero BuildBattleHero(int HID, List<HuyetChienNPC> NPCList)
	{
		HuyetChienNPC huyetChienNPC = NPCList.Find((HuyetChienNPC e) => e.HID == HID);
		DuLieuHero duLieuHero = new DuLieuHero();
		duLieuHero.DuyenHeroList = new List<string>();
		HuyetChienNPC HCCFG;
		foreach (HuyetChienNPC NPC in NPCList)
		{
			HCCFG = NPC;
			if (HCCFG.HID != HID)
			{
				HuyetChienNPC huyetChienNPC2 = NPCList.Find((HuyetChienNPC element) => element.HID == HCCFG.HID);
				if (huyetChienNPC2 != null)
				{
					duLieuHero.DuyenHeroList.Add(huyetChienNPC2.CodeName);
				}
			}
		}
		if (huyetChienNPC == null)
		{
			return duLieuHero;
		}
		NhanVatCfg value = null;
		ConfigManager.instance.m_dicNhanVats.TryGetValue(huyetChienNPC.CodeName, out value);
		if (value == null)
		{
			duLieuHero.HID = HID;
			duLieuHero.Name = huyetChienNPC.CodeName;
			return duLieuHero;
		}
		duLieuHero.HID = HID;
		duLieuHero.Name = value.Name;
		duLieuHero.Menh = CommonHero.GetMenhCoSo(value, huyetChienNPC.Level);
		duLieuHero.Ngoai = CommonHero.GetNgoaiCoSo(value, huyetChienNPC.Level);
		duLieuHero.Noi = CommonHero.GetNoiCoSo(value, huyetChienNPC.Level);
		duLieuHero.ThanPhap = CommonHero.GetThanPhapCoSo(value, huyetChienNPC.Level);
		duLieuHero.BAS = value.BAS;
		duLieuHero.BMS = value.BMS;
		duLieuHero.Range = value.Range;
		duLieuHero.ListVoCong = new List<BattleVoCong>();
		foreach (GiangHoCfg.NhiemVu.NhanVatGH.VoCong voCong in huyetChienNPC.VoCongList)
		{
			duLieuHero.ListVoCong.Add(new BattleVoCong(voCong.CodeName, voCong.Level));
		}
		duLieuHero.ListTrangBi = new List<BattleTrangBi>();
		duLieuHero.ListTrangBi.Add(new BattleTrangBi(new UserInfo.TrangBiData(huyetChienNPC.VuKhi, 1, 1, string.Empty, 0, string.Empty, 0, string.Empty)));
		duLieuHero.AI = new UserInfo.HeroData.AI();
		duLieuHero.PosX = huyetChienNPC.PosX;
		duLieuHero.PosY = huyetChienNPC.PosY;
		return duLieuHero;
	}

	public static BattleGamerInfo GetBattleGamerData(List<HuyetChienNPC> NPCList)
	{
		BattleGamerInfo battleGamerInfo = new BattleGamerInfo();
		battleGamerInfo.DoiHinhRaTran = new List<DuLieuHero>();
		foreach (HuyetChienNPC NPC in NPCList)
		{
			DuLieuHero item = BuildBattleHero(NPC.HID, NPCList);
			battleGamerInfo.DoiHinhRaTran.Add(item);
		}
		return battleGamerInfo;
	}

	public static DuLieuHHoTro BuildHeroHoTro(int HID, UserInfo userInfo)
	{
		DuLieuHHoTro duLieuHHoTro = new DuLieuHHoTro();
		duLieuHHoTro.DuyenHeroList = new List<string>();
		if (userInfo.DoiHinh == null || userInfo.HeroList == null)
		{
			return duLieuHHoTro;
		}
		int e2;
		foreach (int item in userInfo.DoiHinh.ListRaTran)
		{
			e2 = item;
			if (e2 > 0)
			{
				UserInfo.HeroData heroData = userInfo.HeroList.Find((UserInfo.HeroData element) => element.HID == e2);
				if (heroData != null)
				{
					duLieuHHoTro.DuyenHeroList.Add(heroData.Name);
				}
			}
		}
		for (int num = 0; num < userInfo.DoiHinh.ListHoTro.Count; num++)
		{
			int TID = userInfo.DoiHinh.ListHoTro[num];
			UserInfo.HeroData heroData2 = userInfo.HeroList.Find((UserInfo.HeroData element) => element.HID == TID);
			if (heroData2 != null)
			{
				duLieuHHoTro.DuyenHeroList.Add(heroData2.Name);
				if (HID == TID)
				{
					duLieuHHoTro.Slot = num + 1;
				}
			}
		}
		for (int num2 = 0; num2 < userInfo.DoiHinh.ListThienCang.Count; num2++)
		{
			int TID2 = userInfo.DoiHinh.ListThienCang[num2];
			UserInfo.HeroData heroData3 = userInfo.HeroList.Find((UserInfo.HeroData element) => element.HID == TID2);
			if (heroData3 != null)
			{
				duLieuHHoTro.DuyenHeroList.Add(heroData3.Name);
			}
		}
		UserInfo.HeroData propHero = userInfo.HeroList.Find((UserInfo.HeroData element) => element.HID == HID);
		if (propHero != null)
		{
			duLieuHHoTro.HID = propHero.HID;
			duLieuHHoTro.Name = propHero.Name;
			duLieuHHoTro.Menh = propHero.ChiSoGoc.Menh;
			duLieuHHoTro.Ngoai = propHero.ChiSoGoc.Ngoai;
			duLieuHHoTro.Noi = propHero.ChiSoGoc.Noi;
			duLieuHHoTro.ThanPhap = propHero.ChiSoGoc.ThanPhap;
			float num3 = propHero.ThienMaBuf;
			if (num3 > 0f)
			{
				NhanVatCfg value = null;
				ConfigManager.instance.m_dicNhanVats.TryGetValue(propHero.Name, out value);
				if (value != null)
				{
					if (value.ChiSoThienMaBuf == 0)
					{
						float num4 = Math.Min(duLieuHHoTro.Menh, num3);
						duLieuHHoTro.Menh -= num4;
						num3 -= num4;
					}
					else if (value.ChiSoThienMaBuf == 1)
					{
						float num5 = Math.Min(duLieuHHoTro.Ngoai, num3);
						duLieuHHoTro.Ngoai -= num5;
						num3 -= num5;
					}
					else if (value.ChiSoThienMaBuf == 2)
					{
						float num6 = Math.Min(duLieuHHoTro.ThanPhap, num3);
						duLieuHHoTro.ThanPhap -= num6;
						num3 -= num6;
					}
					else if (value.ChiSoThienMaBuf == 3)
					{
						float num7 = Math.Min(duLieuHHoTro.Noi, num3);
						duLieuHHoTro.Noi -= num7;
						num3 -= num7;
					}
				}
			}
			if (num3 > 0f)
			{
				if (propHero.ChiSoGoc.Menh >= Math.Max(propHero.ChiSoGoc.Ngoai, propHero.ChiSoGoc.ThanPhap) && propHero.ChiSoGoc.Menh >= propHero.ChiSoGoc.Noi)
				{
					float num8 = Math.Min(duLieuHHoTro.Ngoai, num3);
					duLieuHHoTro.Ngoai -= num8;
					num3 -= num8;
					if (num3 > 0f)
					{
						num8 = Math.Min(duLieuHHoTro.ThanPhap, num3);
						duLieuHHoTro.ThanPhap -= num8;
						num3 -= num8;
					}
					if (num3 > 0f)
					{
						num8 = Math.Min(duLieuHHoTro.Noi, num3);
						duLieuHHoTro.Noi -= num8;
						num3 -= num8;
					}
					if (num3 > 0f)
					{
						duLieuHHoTro.Menh -= num3;
					}
				}
				else if (propHero.ChiSoGoc.Ngoai >= Math.Max(propHero.ChiSoGoc.Menh, propHero.ChiSoGoc.ThanPhap) && propHero.ChiSoGoc.Ngoai >= propHero.ChiSoGoc.Noi)
				{
					float num9 = Math.Min(duLieuHHoTro.Menh, num3);
					duLieuHHoTro.Menh -= num9;
					num3 -= num9;
					if (num3 > 0f)
					{
						num9 = Math.Min(duLieuHHoTro.ThanPhap, num3);
						duLieuHHoTro.ThanPhap -= num9;
						num3 -= num9;
					}
					if (num3 > 0f)
					{
						num9 = Math.Min(duLieuHHoTro.Noi, num3);
						duLieuHHoTro.Noi -= num9;
						num3 -= num9;
					}
					if (num3 > 0f)
					{
						duLieuHHoTro.Ngoai -= num3;
					}
				}
				else if (propHero.ChiSoGoc.ThanPhap >= Math.Max(propHero.ChiSoGoc.Ngoai, propHero.ChiSoGoc.Menh) && propHero.ChiSoGoc.ThanPhap >= propHero.ChiSoGoc.Noi)
				{
					float num10 = Math.Min(duLieuHHoTro.Menh, num3);
					duLieuHHoTro.Menh -= num10;
					num3 -= num10;
					if (num3 > 0f)
					{
						num10 = Math.Min(duLieuHHoTro.Ngoai, num3);
						duLieuHHoTro.Ngoai -= num10;
						num3 -= num10;
					}
					if (num3 > 0f)
					{
						num10 = Math.Min(duLieuHHoTro.Noi, num3);
						duLieuHHoTro.Noi -= num10;
						num3 -= num10;
					}
					if (num3 > 0f)
					{
						duLieuHHoTro.ThanPhap -= num3;
					}
				}
				else if (propHero.ChiSoGoc.Noi >= Math.Max(propHero.ChiSoGoc.Ngoai, propHero.ChiSoGoc.ThanPhap) && propHero.ChiSoGoc.Noi >= propHero.ChiSoGoc.Menh)
				{
					float num11 = Math.Min(duLieuHHoTro.Menh, num3);
					duLieuHHoTro.Menh -= num11;
					num3 -= num11;
					if (num3 > 0f)
					{
						num11 = Math.Min(duLieuHHoTro.Ngoai, num3);
						duLieuHHoTro.Ngoai -= num11;
						num3 -= num11;
					}
					if (num3 > 0f)
					{
						num11 = Math.Min(duLieuHHoTro.ThanPhap, num3);
						duLieuHHoTro.ThanPhap -= num11;
						num3 -= num11;
					}
					if (num3 > 0f)
					{
						duLieuHHoTro.Noi -= num3;
					}
				}
			}
			if (duLieuHHoTro.Menh < 0f)
			{
				duLieuHHoTro.Menh = 0f;
			}
			if (duLieuHHoTro.Ngoai < 0f)
			{
				duLieuHHoTro.Ngoai = 0f;
			}
			if (duLieuHHoTro.ThanPhap < 0f)
			{
				duLieuHHoTro.ThanPhap = 0f;
			}
			if (duLieuHHoTro.Noi < 0f)
			{
				duLieuHHoTro.Noi = 0f;
			}
			duLieuHHoTro.BAS = propHero.ChiSoGoc.BAS;
			duLieuHHoTro.BMS = propHero.ChiSoGoc.BMS;
			duLieuHHoTro.Range = propHero.ChiSoGoc.Range;
			duLieuHHoTro.CapDotPha = propHero.CapDotPha;
			if (userInfo.TrangBiList != null)
			{
				duLieuHHoTro.ListTrangBi = new List<BattleTrangBi>();
				UserInfo.TrangBiData trangBiData = userInfo.TrangBiList.Find((UserInfo.TrangBiData F) => F.ID == propHero.VuKhiID && F.HID == HID);
				if (trangBiData != null)
				{
					duLieuHHoTro.ListTrangBi.Add(new BattleTrangBi(trangBiData));
				}
				trangBiData = userInfo.TrangBiList.Find((UserInfo.TrangBiData F) => F.ID == propHero.MuID && F.HID == HID);
				if (trangBiData != null)
				{
					duLieuHHoTro.ListTrangBi.Add(new BattleTrangBi(trangBiData));
				}
				trangBiData = userInfo.TrangBiList.Find((UserInfo.TrangBiData F) => F.ID == propHero.AoGiapID && F.HID == HID);
				if (trangBiData != null)
				{
					duLieuHHoTro.ListTrangBi.Add(new BattleTrangBi(trangBiData));
				}
				trangBiData = userInfo.TrangBiList.Find((UserInfo.TrangBiData F) => F.ID == propHero.TrangSucID && F.HID == HID);
				if (trangBiData != null)
				{
					duLieuHHoTro.ListTrangBi.Add(new BattleTrangBi(trangBiData));
				}
			}
			duLieuHHoTro.ListVoCong = new List<BattleVoCong>();
			if (userInfo.VoCongList != null)
			{
				UserInfo.VoCongData voCongData = userInfo.VoCongList.Find((UserInfo.VoCongData voCongData2) => voCongData2.ID == propHero.VoCong3ID);
				if (voCongData != null)
				{
					duLieuHHoTro.ListVoCong.Add(new BattleVoCong(voCongData.Type, voCongData.Level));
				}
				voCongData = userInfo.VoCongList.Find((UserInfo.VoCongData voCongData2) => voCongData2.ID == propHero.VoCong4ID);
				if (voCongData != null)
				{
					duLieuHHoTro.ListVoCong.Add(new BattleVoCong(voCongData.Type, voCongData.Level));
				}
			}
			duLieuHHoTro.BeQuan = propHero.BeQuan;
			duLieuHHoTro.ChuyenSinh = propHero.ChuyenSinh;
			duLieuHHoTro.CosData = null;
			if (propHero.CostumeID > 0)
			{
				UserInfo.CostumeData costumeData = null;
				if (userInfo.CostumeList != null)
				{
					costumeData = userInfo.CostumeList.Find((UserInfo.CostumeData costumeData2) => costumeData2.ID == propHero.CostumeID);
				}
				if (costumeData != null)
				{
					CostumeCfg value2 = null;
					if (ConfigManager.instance.m_dicCostumeCfg.TryGetValue(costumeData.CodeName, out value2))
					{
						CostumeCfg.CostumeStat statFromTinhLuyen = value2.GetStatFromTinhLuyen(costumeData.TinhLuyen);
						duLieuHHoTro.MenhCostumeBuff = statFromTinhLuyen.EffMenh / 100f;
						duLieuHHoTro.NgoaiCostumeBuff = statFromTinhLuyen.EffNgoai / 100f;
						duLieuHHoTro.ThanCostumeBuff = statFromTinhLuyen.EffThan / 100f;
						duLieuHHoTro.KhiCostumeBuff = statFromTinhLuyen.EffKhi / 100f;
						duLieuHHoTro.CosData = new DuLieuHHoTro.Costume();
						duLieuHHoTro.CosData.HongNgoc = costumeData.HongNgoc;
						duLieuHHoTro.CosData.LamNgoc = costumeData.LamNgoc;
						duLieuHHoTro.CosData.HoangNgoc = costumeData.HoangNgoc;
						duLieuHHoTro.CosData.TuNgoc = costumeData.TuNgoc;
						duLieuHHoTro.CosData.TinhLuyen = costumeData.TinhLuyen;
						duLieuHHoTro.CosData.CodeName = costumeData.CodeName;
					}
				}
			}
		}
		return duLieuHHoTro;
	}

	public static DuLieuHero BuildBattleHero(int HID, UserInfo userInfo)
	{
		DuLieuHero dataHero = new DuLieuHero();
		dataHero.DuyenHeroList = new List<string>();
		int e2;
		foreach (int item in userInfo.DoiHinh.ListRaTran)
		{
			e2 = item;
			if (e2 != HID)
			{
				UserInfo.HeroData heroData = userInfo.HeroList.Find((UserInfo.HeroData element) => element.HID == e2);
				if (heroData != null)
				{
					dataHero.DuyenHeroList.Add(heroData.Name);
				}
			}
		}
		int e3;
		foreach (int item2 in userInfo.DoiHinh.ListHoTro)
		{
			e3 = item2;
			UserInfo.HeroData heroData2 = userInfo.HeroList.Find((UserInfo.HeroData element) => element.HID == e3);
			if (heroData2 != null)
			{
				dataHero.DuyenHeroList.Add(heroData2.Name);
			}
		}
		for (int num = 0; num < userInfo.DoiHinh.ListThienCang.Count; num++)
		{
			int TID = userInfo.DoiHinh.ListThienCang[num];
			UserInfo.HeroData heroData3 = userInfo.HeroList.Find((UserInfo.HeroData element) => element.HID == TID);
			if (heroData3 != null)
			{
				dataHero.DuyenHeroList.Add(heroData3.Name);
			}
		}
		UserInfo.HeroData propHero = userInfo.HeroList.Find((UserInfo.HeroData element) => element.HID == HID);
		if (propHero != null)
		{
			dataHero.HID = propHero.HID;
			dataHero.Name = propHero.Name;
			dataHero.Menh = propHero.ChiSoGoc.Menh;
			dataHero.Ngoai = propHero.ChiSoGoc.Ngoai;
			dataHero.Noi = propHero.ChiSoGoc.Noi;
			dataHero.ThanPhap = propHero.ChiSoGoc.ThanPhap;
			dataHero.BAS = propHero.ChiSoGoc.BAS;
			dataHero.BMS = propHero.ChiSoGoc.BMS;
			dataHero.Range = propHero.ChiSoGoc.Range;
			dataHero.CapDotPha = propHero.CapDotPha;
			dataHero.ListVoCong = new List<BattleVoCong>();
			dataHero.ListVoCong.Add(new BattleVoCong(propHero.VoCong1Name, propHero.VoCong1Level));
			if (userInfo.VoCongList != null)
			{
				UserInfo.VoCongData voCongData = userInfo.VoCongList.Find((UserInfo.VoCongData voCongData2) => voCongData2.ID == propHero.VoCong2ID);
				if (voCongData != null)
				{
					dataHero.ListVoCong.Add(new BattleVoCong(voCongData.Type, voCongData.Level));
				}
				voCongData = userInfo.VoCongList.Find((UserInfo.VoCongData voCongData2) => voCongData2.ID == propHero.VoCong3ID);
				if (voCongData != null)
				{
					dataHero.ListVoCong.Add(new BattleVoCong(voCongData.Type, voCongData.Level));
				}
				voCongData = userInfo.VoCongList.Find((UserInfo.VoCongData voCongData2) => voCongData2.ID == propHero.VoCong4ID);
				if (voCongData != null)
				{
					dataHero.ListVoCong.Add(new BattleVoCong(voCongData.Type, voCongData.Level));
				}
			}
			if (userInfo.ChienHonList != null)
			{
				dataHero.ChienHonData = userInfo.ChienHonList.Find((UserInfo.ChienHon chienHon) => chienHon.HID == dataHero.HID);
				if (dataHero.ChienHonData != null)
				{
					dataHero.ListHuyenKhi = new List<UserInfo.HuyenKhi>();
					if (userInfo.HuyenKhiList != null)
					{
						UserInfo.HuyenKhi huyenKhi = userInfo.HuyenKhiList.Find((UserInfo.HuyenKhi huyenKhi3) => huyenKhi3.ID == dataHero.ChienHonData.HuyenKhi1ID);
						if (huyenKhi != null)
						{
							dataHero.ListHuyenKhi.Add(huyenKhi);
						}
						UserInfo.HuyenKhi huyenKhi2 = userInfo.HuyenKhiList.Find((UserInfo.HuyenKhi huyenKhi3) => huyenKhi3.ID == dataHero.ChienHonData.HuyenKhi2ID);
						if (huyenKhi2 != null)
						{
							dataHero.ListHuyenKhi.Add(huyenKhi2);
						}
					}
				}
			}
			dataHero.ListThienMaLenh = new List<UserInfo.ThienMaLenhInfo>();
			if (userInfo.ListThienMaLenh != null)
			{
				UserInfo.ThienMaLenhInfo thienMaLenhInfo = userInfo.ListThienMaLenh.Find((UserInfo.ThienMaLenhInfo tm) => tm.ID == propHero.ThienMaLenhID1);
				if (thienMaLenhInfo != null)
				{
					dataHero.ListThienMaLenh.Add(thienMaLenhInfo);
				}
				thienMaLenhInfo = userInfo.ListThienMaLenh.Find((UserInfo.ThienMaLenhInfo tm) => tm.ID == propHero.ThienMaLenhID2);
				if (thienMaLenhInfo != null)
				{
					dataHero.ListThienMaLenh.Add(thienMaLenhInfo);
				}
			}
			if (userInfo.TrangBiList != null)
			{
				dataHero.ListTrangBi = new List<BattleTrangBi>();
				UserInfo.TrangBiData trangBiData = userInfo.TrangBiList.Find((UserInfo.TrangBiData F) => F.ID == propHero.VuKhiID && F.HID == HID);
				if (trangBiData != null)
				{
					dataHero.ListTrangBi.Add(new BattleTrangBi(trangBiData));
				}
				trangBiData = userInfo.TrangBiList.Find((UserInfo.TrangBiData F) => F.ID == propHero.MuID && F.HID == HID);
				if (trangBiData != null)
				{
					dataHero.ListTrangBi.Add(new BattleTrangBi(trangBiData));
				}
				trangBiData = userInfo.TrangBiList.Find((UserInfo.TrangBiData F) => F.ID == propHero.AoGiapID && F.HID == HID);
				if (trangBiData != null)
				{
					dataHero.ListTrangBi.Add(new BattleTrangBi(trangBiData));
				}
				trangBiData = userInfo.TrangBiList.Find((UserInfo.TrangBiData F) => F.ID == propHero.TrangSucID && F.HID == HID);
				if (trangBiData != null)
				{
					dataHero.ListTrangBi.Add(new BattleTrangBi(trangBiData));
				}
			}
			if (userInfo.NguyenKhiList != null)
			{
				dataHero.NguyenKhiList = new List<DuLieuHero.NguyenKhi>();
				int[] array = new int[6] { propHero.NguyenKhi1ID, propHero.NguyenKhi2ID, propHero.NguyenKhi3ID, propHero.NguyenKhi4ID, propHero.NguyenKhi5ID, propHero.NguyenKhi6ID };
				int[] array2 = array;
				int[] array3 = array2;
				foreach (int nid in array3)
				{
					UserInfo.NguyenKhiData nguyenKhiData = userInfo.NguyenKhiList.Find((UserInfo.NguyenKhiData nguyenKhiData2) => nguyenKhiData2.HID == HID && nguyenKhiData2.ID == nid);
					if (nguyenKhiData != null)
					{
						DuLieuHero.NguyenKhi nguyenKhi = new DuLieuHero.NguyenKhi();
						nguyenKhi.Type = nguyenKhiData.GetNguyenKhiLoai();
						nguyenKhi.ChiSo = nguyenKhiData.GetNguyenKhiStats();
						dataHero.NguyenKhiList.Add(nguyenKhi);
					}
				}
			}
			dataHero.BeQuan = propHero.BeQuan;
			dataHero.ChuyenSinh = propHero.ChuyenSinh;
			dataHero.CosData = null;
			if (propHero.CostumeID > 0)
			{
				UserInfo.CostumeData costumeData = null;
				if (userInfo.CostumeList != null)
				{
					costumeData = userInfo.CostumeList.Find((UserInfo.CostumeData costumeData2) => costumeData2.ID == propHero.CostumeID);
				}
				if (costumeData != null)
				{
					CostumeCfg value = null;
					if (ConfigManager.instance.m_dicCostumeCfg.TryGetValue(costumeData.CodeName, out value))
					{
						CostumeCfg.CostumeStat statFromTinhLuyen = value.GetStatFromTinhLuyen(costumeData.TinhLuyen);
						dataHero.MenhCostumeBuff = statFromTinhLuyen.EffMenh / 100f;
						dataHero.NgoaiCostumeBuff = statFromTinhLuyen.EffNgoai / 100f;
						dataHero.ThanCostumeBuff = statFromTinhLuyen.EffThan / 100f;
						dataHero.KhiCostumeBuff = statFromTinhLuyen.EffKhi / 100f;
						dataHero.CosData = new DuLieuHero.Costume();
						dataHero.CosData.HongNgoc = costumeData.HongNgoc;
						dataHero.CosData.LamNgoc = costumeData.LamNgoc;
						dataHero.CosData.HoangNgoc = costumeData.HoangNgoc;
						dataHero.CosData.TuNgoc = costumeData.TuNgoc;
						dataHero.CosData.TinhLuyen = costumeData.TinhLuyen;
						dataHero.CosData.CodeName = costumeData.CodeName;
					}
				}
			}
			dataHero.PosX = propHero.DHPosX;
			dataHero.PosY = propHero.DHPosY;
			dataHero.AI = propHero.TrangThai;
		}
		return dataHero;
	}

	public static BattleGamerInfo GetBattleGamerData(UserInfo userInfo)
	{
		BattleGamerInfo battleGamerInfo = new BattleGamerInfo();
		if (userInfo.Gamer != null)
		{
			battleGamerInfo.GID = userInfo.Gamer.ID;
			battleGamerInfo.Name = userInfo.Gamer.DisplayName;
		}
		List<int> lienMinhBoostChiSo = LienMinhData.GetLienMinhBoostChiSo(userInfo.LienMinh);
		if (lienMinhBoostChiSo != null && lienMinhBoostChiSo.Count >= 4)
		{
			battleGamerInfo.MenhLienMinhHeSoTangThem = (float)lienMinhBoostChiSo[0] / 100f;
			battleGamerInfo.NgoaiLienMinhHeSoTangThem = (float)lienMinhBoostChiSo[1] / 100f;
			battleGamerInfo.ThanLienMinhHeSoTangThem = (float)lienMinhBoostChiSo[2] / 100f;
			battleGamerInfo.KhiLienMinhHeSoTangThem = (float)lienMinhBoostChiSo[3] / 100f;
		}
		battleGamerInfo.ListVCSetting = userInfo.ListVCSetting;
		battleGamerInfo.DoiHinhRaTran = new List<DuLieuHero>();
		battleGamerInfo.DoiHinhTranDo = new List<DuLieuHHoTro>();
		battleGamerInfo.RawDoiHinhData = userInfo.DoiHinh;
		if (userInfo.DoiHinh != null)
		{
			if (userInfo.DoiHinh.ListRaTran != null)
			{
				for (int i = 0; i < userInfo.DoiHinh.ListRaTran.Count; i++)
				{
					int num = userInfo.DoiHinh.ListRaTran[i];
					if (num > 0)
					{
						DuLieuHero item = BuildBattleHero(num, userInfo);
						battleGamerInfo.DoiHinhRaTran.Add(item);
					}
				}
			}
			if (userInfo.DoiHinh.ListHoTro != null)
			{
				foreach (int item3 in userInfo.DoiHinh.ListHoTro)
				{
					if (item3 > 0)
					{
						DuLieuHHoTro item2 = BuildHeroHoTro(item3, userInfo);
						battleGamerInfo.DoiHinhTranDo.Add(item2);
					}
				}
			}
		}
		if (userInfo.DanhHieu != null)
		{
			battleGamerInfo.KhiThe = ConfigManager.GetKhiTheByDanhHieuAtTime(userInfo.DanhHieu, GameManager.instance.m_GameClient.ServerTime);
		}
		battleGamerInfo.ThuCuoiData = userInfo.GetThuCuoiData();
		if (userInfo.Gamer != null && userInfo.ThuCuoi != null && userInfo.ThuCuoi.ThuCuoiList != null && userInfo.ThuCuoi.ThuCuoiList.Exists((UserInfo.ThuCuoiData e) => e.ID == userInfo.Gamer.curThuCuoi))
		{
			battleGamerInfo.ThuCuoiExpired = userInfo.ThuCuoi.ThuCuoiList.Find((UserInfo.ThuCuoiData e) => e.ID == userInfo.Gamer.curThuCuoi).ExpiredTime;
		}
		if (userInfo.Gamer != null && userInfo.Gamer.curThanThu > 0 && userInfo.ListThanThu != null)
		{
			battleGamerInfo.ThanThuData = ((userInfo.ListThanThu.Count != 0) ? userInfo.ListThanThu.Find((UserInfo.PetInfo p) => p.ID == userInfo.Gamer.curThanThu) : null);
		}
		return battleGamerInfo;
	}

	public static BattleGamerInfo GetBattleGamerData(UserInfo uInfo, HuyetChienDoc doc)
	{
		BattleGamerInfo battleGamerData = GetBattleGamerData(uInfo);
		battleGamerData.MenhHeSoTangThem = (float)doc.TangMenh / 100f;
		battleGamerData.NgoaiHeSoTangThem = (float)doc.TangNgoai / 100f;
		battleGamerData.KhiHeSoTangThem = (float)doc.TangKhi / 100f;
		battleGamerData.ThanHeSoTangThem = (float)doc.TangThanPhap / 100f;
		return battleGamerData;
	}

	public static BattleGamerInfo GetBattleGamerData(UserInfo userInfo, UserInfo friendInfo)
	{
		BattleGamerInfo battleGamerData = GetBattleGamerData(userInfo);
		if (friendInfo != null && friendInfo.DoiHinh != null)
		{
			int num = ((friendInfo.DoiHinh.ListRaTran.Count > 0) ? friendInfo.DoiHinh.ListRaTran[0] : 0);
			if (num > 0)
			{
				DuLieuHero item = BuildBattleHero(num, friendInfo);
				battleGamerData.DoiHinhRaTran.Add(item);
			}
			num = ((friendInfo.DoiHinh.ListRaTran.Count > 1) ? friendInfo.DoiHinh.ListRaTran[1] : 0);
			if (num > 0)
			{
				DuLieuHero item2 = BuildBattleHero(num, friendInfo);
				battleGamerData.DoiHinhRaTran.Add(item2);
			}
		}
		return battleGamerData;
	}
}
