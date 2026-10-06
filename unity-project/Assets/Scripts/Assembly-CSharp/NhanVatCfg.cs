using System;
using System.Collections.Generic;

public class NhanVatCfg
{
	public enum GioiTinh
	{
		Nam = 0,
		Nu = 1
	}

	public class DuyenPhanCfg
	{
		private LoaiDuyenPhan loaiDuyenPhan;

		private List<string> doiTuong = new List<string>();

		private float heSo = 20f;

		private string tenHienThi;

		private string vuKhiMacDinh = "VK_QUAN_TU_KIEM";

		public ChiSoDuyenPhan ChiSoDuyen { get; set; }

		public string VuKhiMacDinh
		{
			get
			{
				return vuKhiMacDinh;
			}
			set
			{
				vuKhiMacDinh = value;
			}
		}

		public LoaiDuyenPhan LoaiDuyenPhan
		{
			get
			{
				return loaiDuyenPhan;
			}
			set
			{
				loaiDuyenPhan = value;
			}
		}

		public List<string> DoiTuong
		{
			get
			{
				return doiTuong;
			}
			set
			{
				doiTuong = value;
			}
		}

		public float HeSo
		{
			get
			{
				return heSo;
			}
			set
			{
				heSo = value;
			}
		}

		public string TenHienThi
		{
			get
			{
				return tenHienThi;
			}
			set
			{
				tenHienThi = value;
			}
		}

		public string LoaiDuyenPhanStr
		{
			set
			{
				try
				{
					loaiDuyenPhan = (LoaiDuyenPhan)(int)Enum.Parse(typeof(LoaiDuyenPhan), value, true);
				}
				catch (Exception ex)
				{
					EGDebug.LogWarning("NhanVatCfg : Error parse loaiDuyenPhan : " + value + " " + ex.ToString());
				}
			}
		}

		public string ChiSoDuyenStr
		{
			set
			{
				try
				{
					ChiSoDuyen = (ChiSoDuyenPhan)(int)Enum.Parse(typeof(ChiSoDuyenPhan), value, true);
				}
				catch (Exception ex)
				{
					EGDebug.LogWarning("NhanVatCfg : Error parse ChiSoDuyenStr : " + value + " " + ex.ToString());
				}
			}
		}

		public bool IsValid()
		{
			if (loaiDuyenPhan == LoaiDuyenPhan.CungDoi)
			{
				foreach (string item in doiTuong)
				{
					if (!ConfigManager.instance.m_dicNhanVats.ContainsKey(item))
					{
						EGDebug.LogError("Nhập lỗi duyên đối tương: " + item);
						return false;
					}
				}
			}
			if (loaiDuyenPhan == LoaiDuyenPhan.TrangBiDo)
			{
				foreach (string item2 in doiTuong)
				{
					if (!ConfigManager.instance.m_dicTrangBi.ContainsKey(item2))
					{
						EGDebug.LogError("Nhập lỗi duyên đối tương: " + item2);
						return false;
					}
				}
			}
			if (heSo <= 0f)
			{
				EGDebug.LogError("Nhập lỗi heso: " + tenHienThi);
				return false;
			}
			return true;
		}
	}

	private List<DuyenPhanCfg> duyenPhan = new List<DuyenPhanCfg>();

	private GioiTinh sex;

	private string huaNguyen = string.Empty;

	private int heSoRandom = 100;

	public int ChiSoThienMaBuf;

	private int _expTanHon = 10000;

	public List<DuyenPhanCfg> DuyenPhan
	{
		get
		{
			return duyenPhan;
		}
		set
		{
			duyenPhan = value;
		}
	}

	public GioiTinh Sex
	{
		get
		{
			return sex;
		}
		set
		{
			sex = value;
		}
	}

	public string HuaNguyen
	{
		get
		{
			return huaNguyen;
		}
		set
		{
			huaNguyen = value;
		}
	}

	public string Name { get; set; }

	public int HeSoRandom
	{
		get
		{
			return heSoRandom;
		}
		set
		{
			heSoRandom = value;
		}
	}

	public string TenHienThi { get; set; }

	public float Menh { get; set; }

	public float MenhTT { get; set; }

	public float Ngoai { get; set; }

	public float NgoaiTT { get; set; }

	public float ThanPhap { get; set; }

	public float ThanPhapTT { get; set; }

	public float Noi { get; set; }

	public float NoiTT { get; set; }

	public float BAS { get; set; }

	public float BMS { get; set; }

	public float Range { get; set; }

	public int Hang { get; set; }

	public float HeSoExp { get; set; }

	public VCType VoCongMacDinh { get; set; }

	public string Mota { get; set; }

	public string VoCongMacDinhStr
	{
		set
		{
			try
			{
				VoCongMacDinh = (VCType)(int)Enum.Parse(typeof(VCType), value, true);
			}
			catch (Exception ex)
			{
				EGDebug.LogWarning("NhanVatCfg : Error parse VoCongMacDinh : " + value + " " + ex.ToString());
			}
		}
	}

	public double TocDoAnimAttack { get; set; }

	public string VuKhiMacDinh { get; set; }

	public int ExpTanHon
	{
		get
		{
			return _expTanHon;
		}
		private set
		{
			_expTanHon = value;
		}
	}

	public bool IsValid()
	{
		if (HeSoExp <= 1f)
		{
			EGDebug.LogError("Nhập lỗi nhân vật " + TenHienThi + " - HeSoExp");
			return false;
		}
		return true;
	}
}
