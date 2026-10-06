using System;
using System.Collections.Generic;

public class ChienTruongChinhTa
{
	public enum RuneType
	{
		None = 0,
		Menh = 1,
		Ngoai = 2,
		Than = 3,
		Khi = 4,
		RuneMax = 5
	}

	public class Rune
	{
		public RuneType Type { get; set; }

		public float PosX { get; set; }

		public float PosY { get; set; }

		public float PosZ { get; set; }

		public int RuneIndex { get; set; }
	}

	public class NguoiChoi
	{
		private float _TyleModel = 1f;

		private float _TyleHp = 1f;

		public int GID { get; set; }

		public int SID { get; set; }

		public int LifeVal { get; set; }

		public string Ten { get; set; }

		public int Level { get; set; }

		public int Vip { get; set; }

		public int Kill { get; set; }

		public string NVAvt { get; set; }

		public string VKAvt { get; set; }

		public string BPAvt { get; set; }

		public string NCAvt { get; set; }

		public int KhiThe { get; set; }

		public string ThuCuoiAvt { get; set; }

		public string Costume { get; set; }

		public string ThanThuName { get; set; }

		public UserInfo.PetInfo.PetQuality ThanThuQuality { get; set; }

		public string GhiChuTrongNgay { get; set; }

		public UserInfo.GamerData.TonHieuType DanhHieuType { get; set; }

		public int GiangHoCount { get; set; }

		public int Diem { get; set; }

		public int DLife { get; set; }

		public bool ChinhPhai { get; set; }

		public float PosX { get; set; }

		public float PosZ { get; set; }

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

		public float TyleHp
		{
			get
			{
				return _TyleHp;
			}
			set
			{
				_TyleHp = value;
			}
		}
	}

	public class Boss
	{
		public int Mau { get; set; }

		public int MauMax { get; set; }
	}

	private List<Rune> _listRune = new List<Rune>();

	private List<NguoiChoi> _NguoiChoiList = new List<NguoiChoi>();

	public int ID { get; set; }

	public DateTime StartTime { get; set; }

	public DateTime TimeOut { get; set; }

	public List<Rune> ListRune
	{
		get
		{
			return _listRune;
		}
		set
		{
			_listRune = value;
		}
	}

	public List<NguoiChoi> NguoiChoiList
	{
		get
		{
			return _NguoiChoiList;
		}
		set
		{
			_NguoiChoiList = value;
		}
	}

	public Boss BossChinhPhai { get; set; }

	public Boss BossTaPhai { get; set; }
}
