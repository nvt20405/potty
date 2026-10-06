using System;
using System.Collections.Generic;

public class QuangMinhDinhCfg
{
	public class NPC
	{
		public int Menh;

		public int Ngoai;

		public int Than;

		public int Khi;

		public string VoCong2;

		public string NoiCong;

		public string BoPhap;

		private UserInfo.HeroData.AI.ENUM_CHIEN_THUAT _AIChienThuat;

		public string AIChienThuatStr
		{
			set
			{
				try
				{
					_AIChienThuat = (UserInfo.HeroData.AI.ENUM_CHIEN_THUAT)(int)Enum.Parse(typeof(UserInfo.HeroData.AI.ENUM_CHIEN_THUAT), value, true);
				}
				catch (Exception ex)
				{
					EGDebug.LogWarning("QuangMinhDinhCfg : Error parse ChienThuatStr : " + value + " " + ex.ToString());
				}
			}
		}

		public UserInfo.HeroData.AI.ENUM_CHIEN_THUAT GetAI()
		{
			return _AIChienThuat;
		}

		public bool IsValid(out string message)
		{
			if (Menh < 0 || Ngoai < 0 || Than < 0 || Khi < 0 || Menh + Ngoai + Than + Khi <= 0)
			{
				message = "NPC: Chỉ số không hợp lệ";
				return false;
			}
			if (VoCong2.Length > 0 && !ConfigManager.instance.m_dicVCs.ContainsKey(VoCong2))
			{
				message = "NPC: VoCong2 không hợp lệ : " + VoCong2;
				return false;
			}
			if (NoiCong.Length > 0 && !ConfigManager.instance.m_dicVCs.ContainsKey(NoiCong))
			{
				message = "NPC: NoiCong không hợp lệ :" + NoiCong;
				return false;
			}
			if (BoPhap.Length > 0 && !ConfigManager.instance.m_dicVCs.ContainsKey(BoPhap))
			{
				message = "NPC: BoPhap không hợp lệ :" + BoPhap;
				return false;
			}
			message = string.Empty;
			return true;
		}
	}

	public class NPCDoiHinh
	{
		public string Mota;

		public List<NPC> NPCs;
	}

	public List<string> DeTu1Sao;

	public List<string> DeTu2Sao;

	public List<string> DeTu3Sao;

	public List<string> ChieuThuc;

	public List<string> NoiCong;

	public List<string> BoPhap;

	public int DeTuLevel = 200;

	public int VoCongLevel = 9;

	public float NPCHeSoTangChiSo = 0.2f;

	public List<NPCDoiHinh> DanhSachNPC;

	public bool IsValid(out string message)
	{
		foreach (NPCDoiHinh item in DanhSachNPC)
		{
			foreach (NPC nPC in item.NPCs)
			{
				if (!nPC.IsValid(out message))
				{
					return false;
				}
			}
		}
		foreach (string item2 in DeTu1Sao)
		{
			if (!ConfigManager.instance.m_dicNhanVats.ContainsKey(item2))
			{
				message = "DeTu1Sao :" + item2 + " không hợp lệ";
				return false;
			}
		}
		foreach (string item3 in DeTu2Sao)
		{
			if (!ConfigManager.instance.m_dicNhanVats.ContainsKey(item3))
			{
				message = "DeTu2Sao :" + item3 + " không hợp lệ";
				return false;
			}
		}
		foreach (string item4 in DeTu3Sao)
		{
			if (!ConfigManager.instance.m_dicNhanVats.ContainsKey(item4))
			{
				message = "DeTu3Sao :" + item4 + " không hợp lệ";
				return false;
			}
		}
		foreach (string item5 in ChieuThuc)
		{
			if (!ConfigManager.instance.m_dicVCs.ContainsKey(item5))
			{
				message = "ChieuThuc :" + item5 + " không hợp lệ";
				return false;
			}
		}
		foreach (string item6 in NoiCong)
		{
			if (!ConfigManager.instance.m_dicVCs.ContainsKey(item6))
			{
				message = "NoiCong :" + item6 + " không hợp lệ";
				return false;
			}
		}
		foreach (string item7 in BoPhap)
		{
			if (!ConfigManager.instance.m_dicVCs.ContainsKey(item7))
			{
				message = "BoPhap :" + item7 + " không hợp lệ";
				return false;
			}
		}
		message = string.Empty;
		return true;
	}
}
