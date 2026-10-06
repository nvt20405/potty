using System;
using System.Collections.Generic;

public class GiangHoCfg
{
	public class NhiemVu
	{
		public class NhanVatGH
		{
			public class VoCong
			{
				private VCType codeName { get; set; }

				public VCType CodeName
				{
					get
					{
						return codeName;
					}
					set
					{
						codeName = value;
					}
				}

				public string CodeNameStr
				{
					set
					{
						try
						{
							codeName = (VCType)(int)Enum.Parse(typeof(VCType), value, true);
						}
						catch (Exception ex)
						{
							EGDebug.LogWarning("GiangHoCfg->NhanVatGH->VoCong : Error parse CodeNameStr : " + value + " " + ex.ToString());
						}
					}
				}

				public int Level { get; set; }

				public bool IsValid()
				{
					return ConfigManager.instance.m_dicVCs.ContainsKey(CodeName.ToString());
				}
			}

			public class TrangBi
			{
				public string CodeName { get; set; }

				public int Level { get; set; }

				public bool IsValid()
				{
					return ConfigManager.instance.m_dicTrangBi.ContainsKey(CodeName);
				}
			}

			public class NhanVatAI
			{
				private UserInfo.HeroData.AI.ENUM_CHIEN_THUAT m_chienThuat;

				public UserInfo.HeroData.AI.ENUM_CHIEN_THUAT ChienThuat
				{
					get
					{
						return m_chienThuat;
					}
					set
					{
						m_chienThuat = value;
					}
				}

				public int BaoVeDongDoi { get; set; }

				public string ChienThuatStr
				{
					set
					{
						try
						{
							ChienThuat = (UserInfo.HeroData.AI.ENUM_CHIEN_THUAT)(int)Enum.Parse(typeof(UserInfo.HeroData.AI.ENUM_CHIEN_THUAT), value, true);
						}
						catch (Exception ex)
						{
							EGDebug.LogWarning("NhanVatCfg : Error parse ChienThuatStr : " + value + " " + ex.ToString());
						}
					}
				}
			}

			private float _scale = Common.TyleModel;

			public string NhanVatCode { get; set; }

			public float Menh { get; set; }

			public float Ngoai { get; set; }

			public float ThanPhap { get; set; }

			public float Noi { get; set; }

			public int LevelVoCongMacDinh { get; set; }

			public List<VoCong> VoCongList { get; set; }

			public List<TrangBi> TrangBiList { get; set; }

			public int PosX { get; set; }

			public int PosY { get; set; }

			public float Scale
			{
				get
				{
					return _scale;
				}
				set
				{
					_scale = value;
				}
			}

			public NhanVatAI AI { get; set; }

			public bool IsValid(out string message)
			{
				if (string.IsNullOrEmpty(NhanVatCode))
				{
					message = "Nhập lỗi nhân vật code";
					return false;
				}
				if (!ConfigManager.instance.m_dicNhanVats.ContainsKey(NhanVatCode))
				{
					message = "Không tìm thấy config nhân vật " + NhanVatCode;
					return false;
				}
				if (VoCongList == null)
				{
					message = "Nhập lỗi võ công của nhân vật " + NhanVatCode;
					return false;
				}
				foreach (VoCong voCong in VoCongList)
				{
					if (!voCong.IsValid())
					{
						message = string.Format("Nhập lỗi võ công {0} của nhân vật {1}", voCong.CodeName, NhanVatCode);
						return false;
					}
				}
				if (TrangBiList == null)
				{
					message = "Nhập lỗi trang bị của nhân vật " + NhanVatCode;
					return false;
				}
				foreach (TrangBi trangBi in TrangBiList)
				{
					if (!trangBi.IsValid())
					{
						message = "Nhập lỗi trang bị " + trangBi.CodeName + " của nhân vật " + NhanVatCode;
						return false;
					}
				}
				if ((double)Scale < 0.01)
				{
					message = "Nhập lỗi tỷ lệ scale của nhân vật " + NhanVatCode + ", scale < 0.01";
					return false;
				}
				message = "OK";
				return true;
			}
		}

		public class PhanThuongNV
		{
			private int _count = 1;

			public string PhanThuongCode { get; set; }

			public float TiLe { get; set; }

			public int Count
			{
				get
				{
					return _count;
				}
				set
				{
					_count = value;
				}
			}
		}

		public string NhanVatDaiDienCode { get; set; }

		public string TenHienThi { get; set; }

		public int CapDoDeNghi { get; set; }

		public string MoTa { get; set; }

		public int ExpThuong { get; set; }

		public int BacThuong { get; set; }

		public int SoLuot1Ngay { get; set; }

		public List<NhanVatGH> DoiHinh { get; set; }

		public List<PhanThuongNV> PhanThuong { get; set; }

		public NhanVatGH NPCHoTro { get; set; }

		public bool IsValid(out string message)
		{
			if (!ConfigManager.instance.m_dicNhanVats.ContainsKey(NhanVatDaiDienCode))
			{
				message = "Nhập lỗi nhân vật đại diện " + NhanVatDaiDienCode + " của nhiệm vụ " + TenHienThi;
				return false;
			}
			if (ExpThuong < 0 || BacThuong < 0 || SoLuot1Ngay < 1)
			{
				message = "Nhập lỗi nhiệm vụ " + TenHienThi;
				return false;
			}
			if (DoiHinh == null || DoiHinh.Count < 1)
			{
				message = "Nhập lỗi nhiệm vụ " + TenHienThi + ", phải có ít nhất 1 nhân vật trong đội hình";
				return false;
			}
			foreach (NhanVatGH item in DoiHinh)
			{
				if (!item.IsValid(out message))
				{
					message = string.Format("Nhập lỗi đội hình nhiệm vụ {1} :\n\t- {0}", message, TenHienThi);
					return false;
				}
			}
			foreach (PhanThuongNV item2 in PhanThuong)
			{
				PhanThuongResponse.PhanThuong phanThuong = new PhanThuongResponse.PhanThuong();
				phanThuong.Name = PhanThuongResponse.GetPhanThuongCodeName(item2.PhanThuongCode);
				phanThuong.Loai = PhanThuongResponse.GetLoaiPhanThuongFromCode(item2.PhanThuongCode);
				phanThuong.Count = item2.Count;
				if (phanThuong.Count < 1)
				{
					message = "Nhập lỗi phần thưởng " + item2.PhanThuongCode + " - Nhiem vu " + TenHienThi + ". Count < 1 ";
					return false;
				}
				if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.BAC || phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.VANG)
				{
					continue;
				}
				if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.VO_CONG || phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.MANH_VO_CONG)
				{
					if (ConfigManager.instance.m_dicVCs.ContainsKey(phanThuong.Name))
					{
						continue;
					}
					message = "Nhập lỗi phần thưởng " + item2.PhanThuongCode;
					return false;
				}
				if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.MANH_TRANG_BI || phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.TRANG_BI)
				{
					if (ConfigManager.instance.m_dicTrangBi.ContainsKey(phanThuong.Name))
					{
						continue;
					}
					message = "Nhập lỗi phần thưởng " + item2.PhanThuongCode;
					return false;
				}
				if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.HON_NHAN_VAT)
				{
					if (ConfigManager.instance.m_dicNhanVats.ContainsKey(phanThuong.Name))
					{
						continue;
					}
					message = "Nhập lỗi phần thưởng " + item2.PhanThuongCode;
					return false;
				}
				message = "Nhập lỗi phần thưởng " + item2.PhanThuongCode;
				return false;
			}
			message = "OK";
			return true;
		}
	}

	public class HoiThoai
	{
		public string NhanVatCode { get; set; }

		public string NoiDung { get; set; }
	}

	public class PhanThuongGH
	{
		private int _count = 1;

		public int LuotNhanThuong { get; set; }

		public string PhanThuong { get; set; }

		public int Count
		{
			get
			{
				return _count;
			}
			set
			{
				_count = value;
			}
		}
	}

	public static float TimeHoiThoaiCuoiTran = 1000f;

	private string _map = string.Empty;

	private float _hesoSucManh = 1f;

	private float _HoiThoaiTime = TimeHoiThoaiCuoiTran;

	public string Map
	{
		get
		{
			return _map;
		}
		set
		{
			_map = value;
		}
	}

	public string TenHienThi { get; set; }

	public string MoTa { get; set; }

	public List<PhanThuongGH> PhanThuongList { get; set; }

	public float UVTextureX { get; set; }

	public float UVTextureY { get; set; }

	public float HeSoSucManh
	{
		get
		{
			return _hesoSucManh;
		}
		set
		{
			_hesoSucManh = value;
		}
	}

	public float HoiThoaiTime
	{
		get
		{
			return _HoiThoaiTime;
		}
		set
		{
			_HoiThoaiTime = value;
		}
	}

	public List<HoiThoai> HoiThoaiList { get; set; }

	public List<NhiemVu> NhiemVuList { get; set; }

	public bool IsValid(out string message)
	{
		foreach (NhiemVu nhiemVu in NhiemVuList)
		{
			if (!nhiemVu.IsValid(out message))
			{
				message = string.Format("GiangHoCfg: Nhập lỗi giang hồ {1} :\n\t- {0}", message, TenHienThi);
				return false;
			}
		}
		foreach (PhanThuongGH phanThuong2 in PhanThuongList)
		{
			PhanThuongResponse.PhanThuong phanThuong = new PhanThuongResponse.PhanThuong();
			phanThuong.Name = PhanThuongResponse.GetPhanThuongCodeName(phanThuong2.PhanThuong);
			phanThuong.Loai = PhanThuongResponse.GetLoaiPhanThuongFromCode(phanThuong2.PhanThuong);
			if (phanThuong2.Count < 1)
			{
				message = "Nhập lỗi phần thưởng " + phanThuong2.PhanThuong + ". Count < 1";
				return false;
			}
			if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.BAC || phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.VANG)
			{
				continue;
			}
			if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.VO_CONG || phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.MANH_VO_CONG)
			{
				if (ConfigManager.instance.m_dicVCs.ContainsKey(phanThuong.Name))
				{
					continue;
				}
				message = "Nhập lỗi phần thưởng " + phanThuong2.PhanThuong;
				return false;
			}
			if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.MANH_TRANG_BI || phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.TRANG_BI)
			{
				if (ConfigManager.instance.m_dicTrangBi.ContainsKey(phanThuong.Name))
				{
					continue;
				}
				message = "Nhập lỗi phần thưởng " + phanThuong2.PhanThuong;
				return false;
			}
			if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.HON_NHAN_VAT)
			{
				if (ConfigManager.instance.m_dicNhanVats.ContainsKey(phanThuong.Name))
				{
					continue;
				}
				message = "Nhập lỗi phần thưởng " + phanThuong2.PhanThuong;
				return false;
			}
			if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU)
			{
				if (ConfigManager.instance.m_dicVatPhamTieuThu.ContainsKey(phanThuong.Name))
				{
					continue;
				}
				message = "Nhập lỗi phần thưởng " + phanThuong2.PhanThuong;
				return false;
			}
			message = "Nhập lỗi phần thưởng " + phanThuong2.PhanThuong;
			return false;
		}
		message = "OK";
		return true;
	}
}
