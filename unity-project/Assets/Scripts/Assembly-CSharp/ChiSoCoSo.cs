using System;

public class ChiSoCoSo
{
	private float m_heSoThuModifier = 1f;

	private float _BMS;

	private float _Menh;

	private float _Ngoai;

	private float _ThanPhap;

	private float _Noi;

	private int _CapDotPha;

	public float HeSoThuModifier
	{
		get
		{
			return m_heSoThuModifier;
		}
		set
		{
			m_heSoThuModifier = value;
		}
	}

	public float BAS { get; set; }

	public float BMS
	{
		get
		{
			return _BMS;
		}
		set
		{
			_BMS = value;
			MS = _BMS / 100f;
		}
	}

	public float Menh
	{
		get
		{
			return _Menh;
		}
		set
		{
			_Menh = value;
			HP = _Menh * 8f;
			RegHp = _Menh / 13.5f;
			IAS = GetIAS();
		}
	}

	public float Ngoai
	{
		get
		{
			return _Ngoai;
		}
		set
		{
			_Ngoai = value;
			Cong = _Ngoai * 1.5f;
			IAS = GetIAS();
		}
	}

	public float ThanPhap
	{
		get
		{
			return _ThanPhap;
		}
		set
		{
			_ThanPhap = value;
			Thu = _ThanPhap;
			IAS = GetIAS();
		}
	}

	public float Noi
	{
		get
		{
			return _Noi;
		}
		set
		{
			_Noi = value;
			RegMp = _Noi / 10f;
			MP = _Noi * 8f;
			IAS = GetIAS();
		}
	}

	public float MS { get; set; }

	public double Range { get; set; }

	public float HP { get; set; }

	public float RegHp { get; set; }

	public float Cong { get; set; }

	public float Thu { get; set; }

	public float RegMp { get; set; }

	public float MP { get; set; }

	public float IAS { get; set; }

	public int CapDotPha
	{
		get
		{
			return _CapDotPha;
		}
		set
		{
			_CapDotPha = value;
		}
	}

	public float MenhDotPha
	{
		get
		{
			return (float)((double)(_Menh * (float)_CapDotPha) * 0.1);
		}
	}

	public float NgoaiDotPha
	{
		get
		{
			return (float)((double)(_Ngoai * (float)_CapDotPha) * 0.1);
		}
	}

	public float ThanDotPha
	{
		get
		{
			return (float)((double)(_ThanPhap * (float)_CapDotPha) * 0.1);
		}
	}

	public float NoiDotPha
	{
		get
		{
			return (float)((double)(_Noi * (float)_CapDotPha) * 0.1);
		}
	}

	private float GetIAS()
	{
		double num = (double)(0.4f * _ThanPhap + 0.3f * _Menh) + 0.2 * (double)_Noi + (double)(0.1f * _Ngoai);
		return (float)(num / 20.0 + Math.Sqrt(num));
	}

	public float AS()
	{
		double num = Math.Max(0.1f, BAS / (1f + IAS / 120f));
		return (float)num;
	}

	public void Add(ChiSoCoSo chisonv)
	{
		if (chisonv.Menh > 0f)
		{
			Menh += chisonv.Menh;
		}
		if (chisonv.Ngoai > 0f)
		{
			Ngoai += chisonv.Ngoai;
		}
		if (chisonv.Noi > 0f)
		{
			Noi += chisonv.Noi;
		}
		if (chisonv.ThanPhap > 0f)
		{
			ThanPhap += chisonv.ThanPhap;
		}
		IAS += chisonv.IAS;
		HP += chisonv.HP;
		RegHp += chisonv.RegHp;
		Cong += chisonv.Cong;
		MP += chisonv.MP;
		RegMp += chisonv.RegMp;
		Thu += chisonv.Thu;
		MS += chisonv.MS;
		Range += chisonv.Range;
	}
}
