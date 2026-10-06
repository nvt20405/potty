using System.Collections.Generic;

public class HomeResponse : ExDataBase
{
	public class Position3D
	{
		public float X { get; set; }

		public float Y { get; set; }

		public float Z { get; set; }

		public bool isNienThu { get; set; }

		public Position3D(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public Position3D()
		{
			X = (Y = (Z = 0f));
		}
	}

	public class Gamer3DInfo
	{
		private Position3D _posInHome = new Position3D();

		public int ID { get; set; }

		public Position3D PosInHome
		{
			get
			{
				return _posInHome;
			}
			set
			{
				_posInHome = value;
			}
		}

		public string UserName { get; set; }

		public int Level { get; set; }

		public string CodeNameVuKhi { get; set; }

		public string CodeNameAvatar { get; set; }

		public string CostumeName { get; set; }

		public string ThanThuName { get; set; }

		public UserInfo.PetInfo.PetQuality ThanThuQuality { get; set; }

		public string NoiCong { get; set; }

		public string BoPhap { get; set; }

		public bool IsOnline { get; set; }

		public int SID { get; set; }

		public int KhiThe { get; set; }

		public string ThuCuoi { get; set; }

		public int Vip { get; set; }

		public int LienMinhID { get; set; }

		public string ghiChuTrongNgay { get; set; }

		public UserInfo.GamerData.TonHieuType DanhHieuType { get; set; }

		public int GiangHoCount { get; set; }
	}

	private Gamer3DInfo _myInfo = new Gamer3DInfo();

	private List<Gamer3DInfo> _posOtherGamers = new List<Gamer3DInfo>();

	private List<Gamer3DInfo> _offlineGamers = new List<Gamer3DInfo>();

	private List<Gamer3DInfo> _noleList = new List<Gamer3DInfo>();

	public Gamer3DInfo MyInfo
	{
		get
		{
			return _myInfo;
		}
		set
		{
			_myInfo = value;
		}
	}

	public List<Gamer3DInfo> PosOtherGamers
	{
		get
		{
			return _posOtherGamers;
		}
		set
		{
			_posOtherGamers = value;
		}
	}

	public List<Gamer3DInfo> OfflineGamers
	{
		get
		{
			return _offlineGamers;
		}
		set
		{
			_offlineGamers = value;
		}
	}

	public List<Gamer3DInfo> NoLeList
	{
		get
		{
			return _noleList;
		}
		set
		{
			_noleList = value;
		}
	}
}
