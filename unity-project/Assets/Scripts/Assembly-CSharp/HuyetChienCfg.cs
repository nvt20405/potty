using System.Collections.Generic;

public class HuyetChienCfg
{
	public class NPCTeam
	{
		public class Hero
		{
			public string CodeName { get; set; }

			public List<GiangHoCfg.NhiemVu.NhanVatGH.VoCong> VoCongList { get; set; }

			public bool IsValid()
			{
				if (!ConfigManager.instance.m_dicNhanVats.ContainsKey(CodeName))
				{
					EGDebug.LogWarning("Khong co config cho nhan vat " + CodeName);
					return false;
				}
				if (VoCongList != null)
				{
					foreach (GiangHoCfg.NhiemVu.NhanVatGH.VoCong voCong in VoCongList)
					{
						if (!voCong.IsValid())
						{
							EGDebug.LogWarning("Nhap loi vo cong " + voCong.CodeName.ToString() + " cua hero " + CodeName);
							return false;
						}
					}
				}
				return true;
			}
		}

		private int _tinhAnh;

		public List<Hero> DoiHinh { get; set; }

		public float BonusMenh { get; set; }

		public float BonusNgoai { get; set; }

		public float BonusThanPhap { get; set; }

		public float BonusKhi { get; set; }

		public int TinhAnh
		{
			get
			{
				return _tinhAnh;
			}
			set
			{
				_tinhAnh = value;
			}
		}
	}

	public List<NPCTeam> NPCTeams { get; set; }

	public int SaoChiSo1 { get; set; }

	public int PhanTramBuff1 { get; set; }

	public int SaoChiSo2 { get; set; }

	public int PhanTramBuff2 { get; set; }

	public int SaoChiSo3 { get; set; }

	public int PhanTramBuff3 { get; set; }

	public int SaoRewardDe { get; set; }

	public int SaoRewardKho { get; set; }

	public int SaoRewardBt { get; set; }

	public float HeSoCanBangNPCKho { get; set; }

	public float HeSoCanBangNPCThuong { get; set; }

	public float HeSoCanBangNPCDe { get; set; }

	public int AiBatDauTinhAnh { get; set; }

	public int XacSuatTinhAnhKho { get; set; }

	public int XacSuatTinhAnhDe { get; set; }

	public int XacSuatTinhAnhBt { get; set; }

	public int NumTopList { get; set; }

	public bool IsValid()
	{
		for (int i = 0; i < NPCTeams.Count; i++)
		{
			foreach (NPCTeam.Hero item in NPCTeams[i].DoiHinh)
			{
				if (!item.IsValid())
				{
					EGDebug.LogWarning(string.Format("Nhap loi nhan vat {0} trong NPCTeams {1}", item.CodeName, i + 1));
					return false;
				}
			}
		}
		if (SaoChiSo1 <= 0 || SaoChiSo2 <= 0 || SaoChiSo3 <= 0)
		{
			EGDebug.LogWarning("Số sao cần tăng chỉ số phải lớn hơn 0");
			return false;
		}
		if (PhanTramBuff1 <= 0 || PhanTramBuff2 <= 0 || PhanTramBuff3 <= 0)
		{
			EGDebug.LogWarning("Phần trăm chỉ số tăng phải lớn hơn 0");
			return false;
		}
		return true;
	}
}
