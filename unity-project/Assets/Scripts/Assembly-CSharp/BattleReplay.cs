using System.Collections.Generic;

public class BattleReplay : ExDataBase
{
	public class HeroInfo
	{
		public int HID { get; set; }

		public int RID { get; set; }

		public float HPMax { get; set; }

		public float MauConLai { get; set; }
	}

	public class TeamInfo
	{
		public string Name { get; set; }

		public bool Winner { get; set; }

		public int SoSao { get; set; }

		public float TongDamage { get; set; }

		public List<HeroInfo> HeroDataList { get; set; }

		public int KhiThe { get; set; }

		public int GID { get; set; }
	}

	public class TTNhanDam
	{
		public bool Ne { get; set; }

		public bool Cr { get; set; }

		public bool Do { get; set; }

		public bool Phan { get; set; }

		public float HpMatDi { get; set; }

		public TTNhanDam()
		{
			Ne = false;
			Cr = false;
			Do = false;
			Phan = false;
		}
	}

	public class SpawnInfo
	{
		private float _TyleModel = Common.TyleModel;

		private bool _DHChinhThuc = true;

		private bool _ChoXemChiSo = true;

		private List<VCType> _VCList = new List<VCType>();

		public int HID { get; set; }

		public string Name { get; set; }

		public float PosX { get; set; }

		public float PosZ { get; set; }

		public string Costume { get; set; }

		public float TyLeModel
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

		public float ORI { get; set; }

		public float HP { get; set; }

		public float HPMax { get; set; }

		public float HPReg { get; set; }

		public float MPMax { get; set; }

		public float MP { get; set; }

		public float MPReg { get; set; }

		public float AS { get; set; }

		public float MS { get; set; }

		public float Cong { get; set; }

		public float Thu { get; set; }

		public string VK { get; set; }

		public int VKLevel { get; set; }

		public string AnimVK { get; set; }

		public byte TeamID { get; set; }

		public HeroState State { get; set; }

		public HeroType Type { get; set; }

		public bool DHPri
		{
			get
			{
				return _DHChinhThuc;
			}
			set
			{
				_DHChinhThuc = value;
			}
		}

		public bool ChoXemChiSo
		{
			get
			{
				return _ChoXemChiSo;
			}
			set
			{
				_ChoXemChiSo = value;
			}
		}

		public List<VCType> VCList
		{
			get
			{
				return _VCList;
			}
			set
			{
				_VCList = value;
			}
		}

		public SpawnInfo()
		{
			Type = HeroType.NhanVat;
		}
	}

	public class TrangThai
	{
		public class ImpactDaThe
		{
			public float X { get; set; }

			public float Z { get; set; }
		}

		public class ImpactDonThe
		{
		}

		public class ImpactDonTheExtra
		{
			public int HID { get; set; }
		}

		public class ImpactPhuTro
		{
			private List<int> _DongDoiHoTroList = new List<int>();

			public List<int> DongDoiHoTroList
			{
				get
				{
					return _DongDoiHoTroList;
				}
				set
				{
					_DongDoiHoTroList = value;
				}
			}
		}

		public class TeleportToPos
		{
			public float X { get; set; }

			public float Z { get; set; }
		}

		public class HStateInfo
		{
			public class HAttackThuongInfo
			{
				public int TID { get; set; }

				public float AS { get; set; }

				public int CNbr { get; set; }
			}

			public class HSkillInfo
			{
				public int TID { get; set; }

				public VCType VC { get; set; }

				public ImpactDaThe IDaThe { get; set; }

				public ImpactDonThe IDonThe { get; set; }

				public ImpactDonTheExtra IDonThe_1 { get; set; }

				public ImpactPhuTro IPhuTro { get; set; }

				public TeleportToPos TeleportToPosData { get; set; }
			}

			public class DeadInfo
			{
				public float X { get; set; }

				public float Z { get; set; }
			}

			private bool _pause;

			public int HID { get; set; }

			public HeroState S { get; set; }

			public float X { get; set; }

			public float Z { get; set; }

			public bool P
			{
				get
				{
					return _pause;
				}
				set
				{
					_pause = value;
				}
			}

			public HAttackThuongInfo AT { get; set; }

			public HSkillInfo VCD { get; set; }

			public DeadInfo D { get; set; }
		}

		public class ToaSatThuong
		{
			public int HID { get; set; }

			public VCType VC { get; set; }
		}

		public class HUPhanDamage
		{
			public int HID { get; set; }
		}

		public class HUThanThu
		{
			public int HID { get; set; }

			public string VC { get; set; }
		}

		public class HieuUng
		{
			public class HitInfo
			{
				public float HP { get; set; }

				public TTNhanDam TTData { get; set; }
			}

			public class VCHU
			{
				public int ID { get; set; }

				public VCEffect.VCEffectType Type { get; set; }

				public VCHU(int _ID)
				{
					ID = _ID;
				}

				public VCHU()
				{
				}
			}

			public int HID { get; set; }

			public float LT { get; set; }

			public HitInfo TD { get; set; }

			public VCHU VCHD { get; set; }

			public string ByVC { get; set; }
		}

		public class RemoveHU
		{
			public int HID { get; set; }

			public int HUID { get; set; }
		}

		public class HUTrungDoc
		{
			public int HID { get; set; }

			public float HpLost { get; set; }

			public float Hp { get; set; }
		}

		public class ThongKeHP
		{
			public int HID { get; set; }

			public float HP { get; set; }
		}

		public class ThongKeHPMax
		{
			public int HID { get; set; }

			public float HPMax { get; set; }
		}

		public class ThongKeMP
		{
			public int HID { get; set; }

			public float MP { get; set; }
		}

		public class ThongKeMPMax
		{
			public int HID { get; set; }

			public float MPMax { get; set; }
		}

		public class ThongKeCong
		{
			public int HID { get; set; }

			public float Cong { get; set; }
		}

		public class ThongKeThu
		{
			public int HID { get; set; }

			public float Thu { get; set; }
		}

		public class ThongKeAS
		{
			public int HID { get; set; }

			public float AS { get; set; }
		}

		public class ThongKeMS
		{
			public int HID { get; set; }

			public float MS { get; set; }
		}

		public class ThongKeRegHp
		{
			public int HID { get; set; }

			public float RegHp { get; set; }
		}

		public class ThongKeRegMp
		{
			public int HID { get; set; }

			public float RegMp { get; set; }
		}

		public class DuBiVaoTran
		{
			public int HID { get; set; }
		}

		public class DauNoiCong
		{
			public class HeroData
			{
				public int HID { get; set; }

				public float HP { get; set; }
			}

			public class TeamData
			{
				private List<HeroData> _HeroDataList = new List<HeroData>();

				public List<HeroData> HeroDataList
				{
					get
					{
						return _HeroDataList;
					}
					set
					{
						_HeroDataList = value;
					}
				}

				public float TongHPConLai { get; set; }

				public float TongMPConLai { get; set; }
			}

			private TeamData _Team1Data = new TeamData();

			private TeamData _Team2Data = new TeamData();

			public TeamData Team1Data
			{
				get
				{
					return _Team1Data;
				}
				set
				{
					_Team1Data = value;
				}
			}

			public TeamData Team2Data
			{
				get
				{
					return _Team2Data;
				}
				set
				{
					_Team2Data = value;
				}
			}
		}

		public class RemoveHero
		{
			public int HID { get; set; }
		}

		public class HoiSinh
		{
			public int HID { get; set; }

			public float HP { get; set; }

			public float MP { get; set; }
		}

		public float T { get; set; }

		public HStateInfo SD { get; set; }

		public ToaSatThuong ToaSatThuongData { get; set; }

		public HUPhanDamage HUPD { get; set; }

		public HUThanThu _HUThanThu { get; set; }

		public List<HieuUng> HUAList { get; set; }

		public List<RemoveHU> HURList { get; set; }

		public HUTrungDoc HUTrD { get; set; }

		public ThongKeHP TKHP { get; set; }

		public ThongKeHPMax TKHPMax { get; set; }

		public ThongKeMP TKMP { get; set; }

		public ThongKeMPMax TKMPMax { get; set; }

		public ThongKeCong TKCong { get; set; }

		public ThongKeThu TKThu { get; set; }

		public ThongKeAS TKAS { get; set; }

		public ThongKeMS TKMS { get; set; }

		public ThongKeRegHp TKRegHP { get; set; }

		public ThongKeRegMp TKRegMP { get; set; }

		public DuBiVaoTran SDuBi { get; set; }

		public DauNoiCong DauNoiCongData { get; set; }

		public RemoveHero RHData { get; set; }

		public HoiSinh HSData { get; set; }
	}

	private bool _DauNoiLuc;

	private TeamInfo m_team1Data = new TeamInfo();

	private TeamInfo m_team2Data = new TeamInfo();

	private List<SpawnInfo> _spawn = new List<SpawnInfo>();

	private List<TrangThai> m_heroStates = new List<TrangThai>();

	public BattleType Type { get; set; }

	public int Winner { get; set; }

	public bool isNull { get; set; }

	public bool DauNoiLuc
	{
		get
		{
			return _DauNoiLuc;
		}
		set
		{
			_DauNoiLuc = value;
		}
	}

	public TeamInfo Team1Data
	{
		get
		{
			return m_team1Data;
		}
		set
		{
			m_team1Data = value;
		}
	}

	public TeamInfo Team2Data
	{
		get
		{
			return m_team2Data;
		}
		set
		{
			m_team2Data = value;
		}
	}

	public List<SpawnInfo> SpawnInfoList
	{
		get
		{
			return _spawn;
		}
		set
		{
			_spawn = value;
		}
	}

	public List<TrangThai> States
	{
		get
		{
			return m_heroStates;
		}
		set
		{
			m_heroStates = value;
		}
	}

	public void RepHoiSinh(float time, int HID, float HP, float MP)
	{
		TrangThai trangThai = new TrangThai();
		trangThai.T = time;
		trangThai.HSData = new TrangThai.HoiSinh();
		trangThai.HSData.HID = HID;
		trangThai.HSData.HP = HP;
		trangThai.HSData.MP = MP;
		States.Add(trangThai);
	}

	public void RepTrungDoc(float time, int HID, float HpLost, float HP)
	{
		TrangThai trangThai = new TrangThai();
		trangThai.T = time;
		trangThai.HUTrD = new TrangThai.HUTrungDoc();
		trangThai.HUTrD.HID = HID;
		trangThai.HUTrD.Hp = HP;
		trangThai.HUTrD.HpLost = HpLost;
		States.Add(trangThai);
	}

	public void RepToaSatThuong(float time, int HID, VCType VC)
	{
		TrangThai trangThai = new TrangThai();
		trangThai.T = time;
		trangThai.ToaSatThuongData = new TrangThai.ToaSatThuong();
		trangThai.ToaSatThuongData.HID = HID;
		trangThai.ToaSatThuongData.VC = VC;
		States.Add(trangThai);
	}

	public void RepThanThuEff(float time, int HID, string vc)
	{
		if (!string.IsNullOrEmpty(vc))
		{
			TrangThai trangThai = new TrangThai();
			trangThai.T = time;
			trangThai._HUThanThu = new TrangThai.HUThanThu();
			trangThai._HUThanThu.HID = HID;
			trangThai._HUThanThu.VC = vc;
			States.Add(trangThai);
		}
	}

	public void RepPhanDamage(float time, int HID)
	{
		TrangThai trangThai = new TrangThai();
		trangThai.T = time;
		trangThai.HUPD = new TrangThai.HUPhanDamage();
		trangThai.HUPD.HID = HID;
		States.Add(trangThai);
	}

	public void RepDuBiVaoTran(float time, int HID)
	{
		TrangThai trangThai = new TrangThai();
		trangThai.T = time;
		trangThai.SDuBi = new TrangThai.DuBiVaoTran();
		trangThai.SDuBi.HID = HID;
		States.Add(trangThai);
	}

	public void RepRemoveHero(float time, int HID)
	{
		TrangThai trangThai = new TrangThai();
		trangThai.T = time;
		trangThai.RHData = new TrangThai.RemoveHero();
		trangThai.RHData.HID = HID;
		States.Add(trangThai);
	}

	public void RepNhanPhanDamage(float time, int HID, float dmg, float HP)
	{
		TrangThai trangThai = new TrangThai();
		trangThai.T = time;
		TrangThai.HieuUng hieuUng = new TrangThai.HieuUng();
		hieuUng.HID = HID;
		hieuUng.LT = 0.1f;
		hieuUng.TD = new TrangThai.HieuUng.HitInfo();
		hieuUng.TD.TTData = new TTNhanDam();
		hieuUng.TD.TTData.Phan = true;
		hieuUng.TD.TTData.HpMatDi = dmg;
		hieuUng.TD.HP = HP;
		if (trangThai.HUAList == null)
		{
			trangThai.HUAList = new List<TrangThai.HieuUng>();
		}
		trangThai.HUAList.Add(hieuUng);
		States.Add(trangThai);
	}

	public void RepHURemove(float time, int HID, List<int> ListVCHURemove)
	{
		TrangThai trangThai = new TrangThai();
		trangThai.T = time;
		foreach (int item in ListVCHURemove)
		{
			TrangThai.RemoveHU removeHU = new TrangThai.RemoveHU();
			removeHU.HID = HID;
			removeHU.HUID = item;
			if (trangThai.HURList == null)
			{
				trangThai.HURList = new List<TrangThai.RemoveHU>();
			}
			trangThai.HURList.Add(removeHU);
		}
		States.Add(trangThai);
	}

	public void RepThongKeHP(float time, int HID, float hp)
	{
		TrangThai trangThai = new TrangThai();
		trangThai.T = time;
		trangThai.TKHP = new TrangThai.ThongKeHP();
		trangThai.TKHP.HID = HID;
		trangThai.TKHP.HP = hp;
		States.Add(trangThai);
	}

	public void RepThongKeMP(float time, int HID, float mp)
	{
		TrangThai trangThai = new TrangThai();
		trangThai.T = time;
		trangThai.TKMP = new TrangThai.ThongKeMP();
		trangThai.TKMP.HID = HID;
		trangThai.TKMP.MP = mp;
		States.Add(trangThai);
	}

	public void RepThongKeMPMax(float time, int HID, float mp)
	{
		TrangThai trangThai = new TrangThai();
		trangThai.T = time;
		trangThai.TKMPMax = new TrangThai.ThongKeMPMax();
		trangThai.TKMPMax.HID = HID;
		trangThai.TKMPMax.MPMax = mp;
		States.Add(trangThai);
	}

	public void RepThongKeCong(float time, int HID, float cong)
	{
		TrangThai trangThai = new TrangThai();
		trangThai.T = time;
		trangThai.TKCong = new TrangThai.ThongKeCong();
		trangThai.TKCong.HID = HID;
		trangThai.TKCong.Cong = cong;
		States.Add(trangThai);
	}

	public void RepThongKeThu(float time, int HID, float thu)
	{
		TrangThai trangThai = new TrangThai();
		trangThai.T = time;
		trangThai.TKThu = new TrangThai.ThongKeThu();
		trangThai.TKThu.HID = HID;
		trangThai.TKThu.Thu = thu;
		States.Add(trangThai);
	}

	public void RepThongKeRegHp(float time, int HID, float reg)
	{
		TrangThai trangThai = new TrangThai();
		trangThai.T = time;
		trangThai.TKRegHP = new TrangThai.ThongKeRegHp();
		trangThai.TKRegHP.HID = HID;
		trangThai.TKRegHP.RegHp = reg;
		States.Add(trangThai);
	}

	public void RepThongKeRegMp(float time, int HID, float reg)
	{
		TrangThai trangThai = new TrangThai();
		trangThai.T = time;
		trangThai.TKRegMP = new TrangThai.ThongKeRegMp();
		trangThai.TKRegMP.HID = HID;
		trangThai.TKRegMP.RegMp = reg;
		States.Add(trangThai);
	}

	public void RepThongKeAS(float time, int HID, float AS)
	{
		TrangThai trangThai = new TrangThai();
		trangThai.T = time;
		trangThai.TKAS = new TrangThai.ThongKeAS();
		trangThai.TKAS.HID = HID;
		trangThai.TKAS.AS = AS;
		States.Add(trangThai);
	}

	public void RepThongKeMS(float time, int HID, float MS)
	{
		TrangThai trangThai = new TrangThai();
		trangThai.T = time;
		trangThai.TKMS = new TrangThai.ThongKeMS();
		trangThai.TKMS.HID = HID;
		trangThai.TKMS.MS = MS;
		States.Add(trangThai);
	}
}
