public class LienMinhCongThanh
{
	public int LienMinhID { get; set; }

	public int LienMinhSID { get; set; }

	public string LienMinhName { get; set; }

	public int DoBen { get; set; }

	public LienMinhCongThanh(int lid, int sid, string name, int damage)
	{
		LienMinhID = lid;
		LienMinhSID = sid;
		LienMinhName = ((!string.IsNullOrEmpty(name)) ? string.Format("s{0}.{1}", sid, name) : string.Empty);
		DoBen = damage;
	}

	public LienMinhCongThanh(LienMinhCongThanh other)
	{
		LienMinhID = other.LienMinhID;
		LienMinhName = other.LienMinhName;
		LienMinhSID = other.LienMinhSID;
		DoBen = other.DoBen;
	}

	public LienMinhCongThanh()
	{
		LienMinhID = 0;
		LienMinhName = string.Empty;
		LienMinhSID = 0;
		DoBen = 0;
	}

	public bool IsThisLienMinh(int sid, int lid)
	{
		return sid == LienMinhSID && lid == LienMinhID;
	}

	public bool BelongToLienMinh(NguoiChoiBangChien nguoichoi)
	{
		return IsThisLienMinh(nguoichoi.SID, nguoichoi.LID);
	}
}
