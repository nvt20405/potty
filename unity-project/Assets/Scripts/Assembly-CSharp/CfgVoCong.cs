using System;

public class CfgVoCong
{
	private bool _AutoCast;

	private float _CastTime = 1f;

	private int heSoRandom = 100;

	private int _TanAnh;

	private bool _pause;

	public OtherCfg.NguyenKhiType m_BatQuaiType = OtherCfg.NguyenKhiType.DO_DON;

	public float BatQuaiBase;

	public float BatQuaiTangTruong;

	public int HeSoThienMa;

	public int ReqThienMa;

	public string Name { get; set; }

	public VCType Type { get; set; }

	public string TypeStr
	{
		set
		{
			try
			{
				Type = (VCType)(int)Enum.Parse(typeof(VCType), value, true);
			}
			catch (Exception ex)
			{
				EGDebug.LogWarning("CfgVoCong : Error parse TypeStr : " + value + " " + ex.ToString());
			}
		}
	}

	public VCClass m_Class { get; set; }

	public int Hang { get; set; }

	public float TangTruong { get; set; }

	public float TangTruong_1 { get; set; }

	public float TangTruong_2 { get; set; }

	public float TangTruong_3 { get; set; }

	public int SoTanChuong { get; set; }

	public VCCongKich CongKichType { get; set; }

	public string CongKichStr
	{
		set
		{
			try
			{
				CongKichType = (VCCongKich)(int)Enum.Parse(typeof(VCCongKich), value, true);
			}
			catch (Exception ex)
			{
				EGDebug.LogWarning("CfgVoCong : Error parse CongKichType : " + value + " " + ex.ToString());
			}
		}
	}

	public string TenHienThi { get; set; }

	public float SatThuong { get; set; }

	public int SoHit { get; set; }

	public float Cooldown { get; set; }

	public float TieuHao { get; set; }

	public float TieuHaoTangTruong { get; set; }

	public bool AutoCast
	{
		get
		{
			return _AutoCast;
		}
		set
		{
			_AutoCast = value;
		}
	}

	public float ChiSo_1 { get; set; }

	public float ChiSo_2 { get; set; }

	public float ChiSo_3 { get; set; }

	public float TamSuDung { get; set; }

	public int SoMucTieuThem { get; set; }

	public float Parameter_1 { get; set; }

	public float Parameter_2 { get; set; }

	public float Parameter_3 { get; set; }

	public float Parameter_4 { get; set; }

	public float Parameter_5 { get; set; }

	public float Parameter_6 { get; set; }

	public float CastTime
	{
		get
		{
			return _CastTime;
		}
		set
		{
			_CastTime = value;
		}
	}

	public string Mota1 { get; set; }

	public string Mota2 { get; set; }

	public int TanAnh
	{
		get
		{
			return _TanAnh;
		}
		set
		{
			_TanAnh = value;
		}
	}

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

	public VCTargetType TargetType { get; set; }

	public string Loai
	{
		set
		{
			try
			{
				TargetType = (VCTargetType)(int)Enum.Parse(typeof(VCTargetType), value, true);
			}
			catch (Exception ex)
			{
				EGDebug.LogWarning("CfgVoCong : Error parse Loai : " + value + " " + ex.ToString());
			}
		}
	}

	public string LoaiSach
	{
		set
		{
			try
			{
				m_Class = (VCClass)(int)Enum.Parse(typeof(VCClass), value, true);
			}
			catch (Exception ex)
			{
				EGDebug.LogWarning("CfgVoCong : Error parse LoaiSach : " + value + " " + ex.ToString());
			}
		}
	}

	public string LoaiSetting
	{
		set
		{
			try
			{
				m_Setting = (VCSetting)(int)Enum.Parse(typeof(VCSetting), value, false);
			}
			catch (Exception ex)
			{
				EGDebug.LogWarning("CfgVoCong : Error parse LoaiSetting : " + value + " " + ex.ToString());
			}
		}
	}

	public VCSetting m_Setting { get; private set; }

	public bool Pause
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

	public string YeuCauVuKhi { get; set; }

	public string BatQuaiBuff
	{
		set
		{
			try
			{
				m_BatQuaiType = (OtherCfg.NguyenKhiType)(int)Enum.Parse(typeof(OtherCfg.NguyenKhiType), value, true);
			}
			catch (Exception ex)
			{
				EGDebug.LogWarning("CfgVoCong : Error parse LoaiSach : " + value + " " + ex.ToString());
			}
		}
	}

	public bool IsValid()
	{
		return true;
	}

	public float GetSatThuong(int level)
	{
		return SatThuong + (float)(level - 1) * TangTruong;
	}

	public float GetTieuHao(int lv)
	{
		if (lv <= 0)
		{
			return 0f;
		}
		if (lv == 1)
		{
			return TieuHao;
		}
		return (float)(lv * lv) + GetTieuHao(lv - 1) * 1.6f;
	}

	public float ChiSo1CoSo(int level)
	{
		return ChiSo_1 + (float)(level - 1) * TangTruong_1;
	}

	public float ChiSo2CoSo(int level)
	{
		return ChiSo_2 + (float)(level - 1) * TangTruong_2;
	}

	public float ChiSo3CoSo(int level)
	{
		return ChiSo_3 + (float)(level - 1) * TangTruong_3;
	}

	public float GetBatQuaiChiSo(int level)
	{
		return BatQuaiBase + (float)(level - 1) * BatQuaiTangTruong;
	}
}
