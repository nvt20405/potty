using System;

public class NguoiChoiBangChien
{
	private DateTime _lastTimeDenCongThanh = DateTime.Now;

	private DateTime _lastTimeGetPhanThuong = DateTime.Now;

	private DateTime _timeHoiSinh = DateTime.Now;

	public int GID { get; set; }

	public int LID { get; set; }

	public int SID { get; set; }

	public int Level { get; set; }

	public string Ava { get; set; }

	public string Ten { get; set; }

	public string BTen { get; set; }

	public string BoPhap { get; set; }

	public string NoiCong { get; set; }

	public string VuKhi { get; set; }

	public float X { get; set; }

	public float Y { get; set; }

	public float Z { get; set; }

	public int Damage { get; set; }

	public int Def { get; set; }

	public string Costume { get; set; }

	public DateTime LastTimeDenCongThanh
	{
		get
		{
			return _lastTimeDenCongThanh;
		}
		set
		{
			_lastTimeDenCongThanh = value;
		}
	}

	public DateTime LastTimeGetPhanThuong
	{
		get
		{
			return _lastTimeGetPhanThuong;
		}
		set
		{
			_lastTimeGetPhanThuong = value;
		}
	}

	public DateTime TimeHoiSinh
	{
		get
		{
			return _timeHoiSinh;
		}
		set
		{
			_timeHoiSinh = value;
		}
	}
}
