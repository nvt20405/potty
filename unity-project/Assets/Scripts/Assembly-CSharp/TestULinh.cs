using UnityEngine;

public class TestULinh : MonoBehaviour
{
	private void Start()
	{
		int num = 0;
		int num2 = 0;
		ULinhInfoResponse uLinhInfoResponse = new ULinhInfoResponse();
		int num3 = 0;
		int num4 = 0;
		for (int i = 0; i < 100; i++)
		{
			for (int j = 0; j < 100; j++)
			{
				for (int k = 0; k < 100; k++)
				{
					num2 += 100;
					num += 100;
					RandomULinhDoiDo(uLinhInfoResponse);
					num += uLinhInfoResponse.ListDoiDo[0].Diem + uLinhInfoResponse.ListDoiDo[1].Diem + uLinhInfoResponse.ListDoiDo[2].Diem;
					num3 += uLinhInfoResponse.ListDoiDo[0].Diem + uLinhInfoResponse.ListDoiDo[1].Diem + uLinhInfoResponse.ListDoiDo[2].Diem;
					if (uLinhInfoResponse.ListDoiDo[0].VatPhamDoi.Loai == PhanThuongResponse.LoaiPhanThuong.VANG)
					{
						num2 += uLinhInfoResponse.ListDoiDo[0].VatPhamDoi.Count;
						num4 += uLinhInfoResponse.ListDoiDo[0].VatPhamDoi.Count;
					}
					if (uLinhInfoResponse.ListDoiDo[1].VatPhamDoi.Loai == PhanThuongResponse.LoaiPhanThuong.VANG)
					{
						num2 += uLinhInfoResponse.ListDoiDo[1].VatPhamDoi.Count;
						num4 += uLinhInfoResponse.ListDoiDo[0].VatPhamDoi.Count;
					}
					if (uLinhInfoResponse.ListDoiDo[2].VatPhamDoi.Loai == PhanThuongResponse.LoaiPhanThuong.VANG)
					{
						num2 += uLinhInfoResponse.ListDoiDo[2].VatPhamDoi.Count;
						num4 += uLinhInfoResponse.ListDoiDo[0].VatPhamDoi.Count;
					}
					if (uLinhInfoResponse.ListDoiDo[0].VatPhamNhan.Loai == PhanThuongResponse.LoaiPhanThuong.VANG)
					{
						num2 -= uLinhInfoResponse.ListDoiDo[0].VatPhamNhan.Count;
						num4 -= uLinhInfoResponse.ListDoiDo[0].VatPhamDoi.Count;
					}
					if (uLinhInfoResponse.ListDoiDo[1].VatPhamNhan.Loai == PhanThuongResponse.LoaiPhanThuong.VANG)
					{
						num2 -= uLinhInfoResponse.ListDoiDo[1].VatPhamNhan.Count;
						num4 -= uLinhInfoResponse.ListDoiDo[0].VatPhamDoi.Count;
					}
					if (uLinhInfoResponse.ListDoiDo[2].VatPhamNhan.Loai == PhanThuongResponse.LoaiPhanThuong.VANG)
					{
						num2 -= uLinhInfoResponse.ListDoiDo[2].VatPhamNhan.Count;
						num4 -= uLinhInfoResponse.ListDoiDo[0].VatPhamDoi.Count;
					}
				}
			}
			Debug.Log("KNB: " + num2 + "  Diem: " + num + "  HeSo: " + (float)num / (float)num2 + " DiemTB: " + (float)num3 / 1000000f);
		}
	}

	private void Update()
	{
	}

	public static void RandomULinhDoiDo(ULinhInfoResponse response)
	{
		response.ListDoiDo.Clear();
		for (int i = 0; i < 3; i++)
		{
			ULinhInfoResponse.ULinhDoiDoItem uLinhDoiDoItem = new ULinhInfoResponse.ULinhDoiDoItem();
			if (Random.Range(0, 2) == 0)
			{
				int num = Random.Range(0, 100);
				if (num < 25)
				{
					int num2 = Random.Range(0, 100);
					if (num2 < 50)
					{
						uLinhDoiDoItem.VatPhamDoi.Loai = PhanThuongResponse.LoaiPhanThuong.BAC;
						uLinhDoiDoItem.VatPhamDoi.Count = 10000;
						uLinhDoiDoItem.Diem = 10;
					}
					else
					{
						uLinhDoiDoItem.VatPhamDoi.Loai = PhanThuongResponse.LoaiPhanThuong.VANG;
						uLinhDoiDoItem.VatPhamDoi.Count = 10;
						uLinhDoiDoItem.Diem = 20;
					}
					int num3 = Random.Range(0, 100);
					if (num3 < 20)
					{
						uLinhDoiDoItem.VatPhamNhan.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
						uLinhDoiDoItem.VatPhamNhan.Name = "VP_BOI_DUONG_DAN";
						uLinhDoiDoItem.VatPhamNhan.Count = 5;
					}
					else if (num3 < 40)
					{
						uLinhDoiDoItem.VatPhamNhan.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
						uLinhDoiDoItem.VatPhamNhan.Name = "VP_HOP_BAC";
						uLinhDoiDoItem.VatPhamNhan.Count = 1;
					}
					else if (num3 < 60)
					{
						uLinhDoiDoItem.VatPhamNhan.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
						uLinhDoiDoItem.VatPhamNhan.Name = "VP_HOP_SAT";
						uLinhDoiDoItem.VatPhamNhan.Count = 2;
					}
					else if (num3 < 80)
					{
						uLinhDoiDoItem.VatPhamNhan.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
						uLinhDoiDoItem.VatPhamNhan.Name = "VP_HOP_SAT_KEY";
						uLinhDoiDoItem.VatPhamNhan.Count = 1;
					}
					else
					{
						uLinhDoiDoItem.VatPhamNhan.Loai = PhanThuongResponse.LoaiPhanThuong.HON_NHAN_VAT;
						uLinhDoiDoItem.VatPhamNhan.Count = 1;
					}
				}
				else if (num < 50)
				{
					int num4 = Random.Range(0, 100);
					if (num4 < 50)
					{
						uLinhDoiDoItem.VatPhamDoi.Loai = PhanThuongResponse.LoaiPhanThuong.BAC;
						uLinhDoiDoItem.VatPhamDoi.Count = 20000;
						uLinhDoiDoItem.Diem = 15;
					}
					else
					{
						uLinhDoiDoItem.VatPhamDoi.Loai = PhanThuongResponse.LoaiPhanThuong.VANG;
						uLinhDoiDoItem.VatPhamDoi.Count = 20;
						uLinhDoiDoItem.Diem = 35;
					}
					int num5 = Random.Range(0, 100);
					if (num5 < 15)
					{
						uLinhDoiDoItem.VatPhamNhan.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
						uLinhDoiDoItem.VatPhamNhan.Name = "VP_BOI_DUONG_DAN";
						uLinhDoiDoItem.VatPhamNhan.Count = 10;
					}
					else if (num5 < 30)
					{
						uLinhDoiDoItem.VatPhamNhan.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
						uLinhDoiDoItem.VatPhamNhan.Name = "VP_HOP_VANG";
						uLinhDoiDoItem.VatPhamNhan.Count = 1;
					}
					else if (num5 < 45)
					{
						uLinhDoiDoItem.VatPhamNhan.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
						uLinhDoiDoItem.VatPhamNhan.Name = "VP_HOP_BAC";
						uLinhDoiDoItem.VatPhamNhan.Count = 2;
					}
					else if (num5 < 60)
					{
						uLinhDoiDoItem.VatPhamNhan.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
						uLinhDoiDoItem.VatPhamNhan.Name = "VP_HOP_BAC_KEY";
						uLinhDoiDoItem.VatPhamNhan.Count = 1;
					}
					else if (num5 < 75)
					{
						uLinhDoiDoItem.VatPhamNhan.Loai = PhanThuongResponse.LoaiPhanThuong.MANH_VO_CONG;
						uLinhDoiDoItem.VatPhamNhan.Count = 2;
					}
					else
					{
						uLinhDoiDoItem.VatPhamNhan.Loai = PhanThuongResponse.LoaiPhanThuong.HON_NHAN_VAT;
						uLinhDoiDoItem.VatPhamNhan.Count = 2;
					}
				}
				else if (num < 75)
				{
					int num6 = Random.Range(0, 100);
					if (num6 < 50)
					{
						uLinhDoiDoItem.VatPhamDoi.Loai = PhanThuongResponse.LoaiPhanThuong.BAC;
						uLinhDoiDoItem.VatPhamDoi.Count = 50000;
						uLinhDoiDoItem.Diem = 20;
					}
					else
					{
						uLinhDoiDoItem.VatPhamDoi.Loai = PhanThuongResponse.LoaiPhanThuong.VANG;
						uLinhDoiDoItem.VatPhamDoi.Count = 30;
						uLinhDoiDoItem.Diem = 50;
					}
					int num7 = Random.Range(0, 100);
					if (num7 < 15)
					{
						uLinhDoiDoItem.VatPhamNhan.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
						uLinhDoiDoItem.VatPhamNhan.Name = "VP_BOI_DUONG_DAN";
						uLinhDoiDoItem.VatPhamNhan.Count = 15;
					}
					else if (num7 < 30)
					{
						uLinhDoiDoItem.VatPhamNhan.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
						uLinhDoiDoItem.VatPhamNhan.Name = "VP_HOP_VANG";
						uLinhDoiDoItem.VatPhamNhan.Count = 1;
					}
					else if (num7 < 40)
					{
						uLinhDoiDoItem.VatPhamNhan.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
						uLinhDoiDoItem.VatPhamNhan.Name = "VP_HOP_BAC";
						uLinhDoiDoItem.VatPhamNhan.Count = 3;
					}
					else if (num7 < 50)
					{
						uLinhDoiDoItem.VatPhamNhan.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
						uLinhDoiDoItem.VatPhamNhan.Name = "VP_HOP_BAC_KEY";
						uLinhDoiDoItem.VatPhamNhan.Count = 2;
					}
					else if (num7 < 60)
					{
						uLinhDoiDoItem.VatPhamNhan.Loai = PhanThuongResponse.LoaiPhanThuong.MANH_VO_CONG;
						uLinhDoiDoItem.VatPhamNhan.Count = 3;
					}
					else if (num7 < 70)
					{
						uLinhDoiDoItem.VatPhamNhan.Loai = PhanThuongResponse.LoaiPhanThuong.MANH_TRANG_BI;
						uLinhDoiDoItem.VatPhamNhan.Count = 2;
					}
					else
					{
						uLinhDoiDoItem.VatPhamNhan.Loai = PhanThuongResponse.LoaiPhanThuong.HON_NHAN_VAT;
						uLinhDoiDoItem.VatPhamNhan.Count = 1;
					}
				}
				else if (num < 95)
				{
					int num8 = Random.Range(0, 100);
					if (num8 < 50)
					{
						uLinhDoiDoItem.VatPhamDoi.Loai = PhanThuongResponse.LoaiPhanThuong.BAC;
						uLinhDoiDoItem.VatPhamDoi.Count = 80000;
						uLinhDoiDoItem.Diem = 25;
					}
					else
					{
						uLinhDoiDoItem.VatPhamDoi.Loai = PhanThuongResponse.LoaiPhanThuong.VANG;
						uLinhDoiDoItem.VatPhamDoi.Count = 40;
						uLinhDoiDoItem.Diem = 65;
					}
					int num9 = Random.Range(0, 100);
					if (num9 < 15)
					{
						uLinhDoiDoItem.VatPhamNhan.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
						uLinhDoiDoItem.VatPhamNhan.Name = "VP_BOI_DUONG_DAN";
						uLinhDoiDoItem.VatPhamNhan.Count = 20;
					}
					else if (num9 < 30)
					{
						uLinhDoiDoItem.VatPhamNhan.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
						uLinhDoiDoItem.VatPhamNhan.Name = "VP_HOP_VANG";
						uLinhDoiDoItem.VatPhamNhan.Count = 2;
					}
					else if (num9 < 45)
					{
						uLinhDoiDoItem.VatPhamNhan.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
						uLinhDoiDoItem.VatPhamNhan.Name = "VP_HOP_BAC";
						uLinhDoiDoItem.VatPhamNhan.Count = 4;
					}
					else if (num9 < 60)
					{
						uLinhDoiDoItem.VatPhamNhan.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
						uLinhDoiDoItem.VatPhamNhan.Name = "VP_HOP_BAC_KEY";
						uLinhDoiDoItem.VatPhamNhan.Count = 3;
					}
					else if (num9 < 80)
					{
						uLinhDoiDoItem.VatPhamNhan.Loai = PhanThuongResponse.LoaiPhanThuong.MANH_VO_CONG;
						uLinhDoiDoItem.VatPhamNhan.Count = 4;
					}
					else if (num9 < 90)
					{
						uLinhDoiDoItem.VatPhamNhan.Loai = PhanThuongResponse.LoaiPhanThuong.MANH_TRANG_BI;
						uLinhDoiDoItem.VatPhamNhan.Count = 3;
					}
					else
					{
						uLinhDoiDoItem.VatPhamNhan.Loai = PhanThuongResponse.LoaiPhanThuong.HON_NHAN_VAT;
						uLinhDoiDoItem.VatPhamNhan.Count = 2;
					}
				}
				else
				{
					int num10 = Random.Range(0, 100);
					uLinhDoiDoItem.VatPhamDoi.Loai = PhanThuongResponse.LoaiPhanThuong.VANG;
					uLinhDoiDoItem.VatPhamDoi.Count = 250;
					uLinhDoiDoItem.Diem = 250;
					uLinhDoiDoItem.VatPhamNhan.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
					uLinhDoiDoItem.VatPhamNhan.Name = "VP_HUA_NGUYEN";
					uLinhDoiDoItem.VatPhamNhan.Count = 1;
				}
			}
			else
			{
				int num11 = Random.Range(0, 100);
				if (num11 < 30)
				{
					int num12 = Random.Range(0, 100);
					uLinhDoiDoItem.Diem = 10;
					if (num12 < 50)
					{
						uLinhDoiDoItem.VatPhamNhan.Loai = PhanThuongResponse.LoaiPhanThuong.BAC;
						uLinhDoiDoItem.VatPhamNhan.Count = 80000;
					}
					else
					{
						uLinhDoiDoItem.VatPhamNhan.Loai = PhanThuongResponse.LoaiPhanThuong.VANG;
						uLinhDoiDoItem.VatPhamNhan.Count = 8;
					}
					int num13 = Random.Range(0, 100);
					if (num13 < 35)
					{
						uLinhDoiDoItem.VatPhamDoi.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
						uLinhDoiDoItem.VatPhamDoi.Name = "VP_BOI_DUONG_DAN";
						uLinhDoiDoItem.VatPhamDoi.Count = 5;
					}
					else if (num13 < 70)
					{
						uLinhDoiDoItem.VatPhamDoi.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
						uLinhDoiDoItem.VatPhamDoi.Name = "VP_HOP_BAC";
						uLinhDoiDoItem.VatPhamDoi.Count = 1;
					}
					else
					{
						uLinhDoiDoItem.VatPhamDoi.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
						uLinhDoiDoItem.VatPhamDoi.Name = "VP_HOP_SAT_KEY";
						uLinhDoiDoItem.VatPhamDoi.Count = 1;
					}
				}
				else if (num11 < 65)
				{
					int num14 = Random.Range(0, 100);
					uLinhDoiDoItem.Diem = 15;
					if (num14 < 50)
					{
						uLinhDoiDoItem.VatPhamNhan.Loai = PhanThuongResponse.LoaiPhanThuong.BAC;
						uLinhDoiDoItem.VatPhamNhan.Count = 120000;
					}
					else
					{
						uLinhDoiDoItem.VatPhamNhan.Loai = PhanThuongResponse.LoaiPhanThuong.VANG;
						uLinhDoiDoItem.VatPhamNhan.Count = 12;
					}
					int num15 = Random.Range(0, 100);
					if (num15 < 25)
					{
						uLinhDoiDoItem.VatPhamDoi.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
						uLinhDoiDoItem.VatPhamDoi.Name = "VP_BOI_DUONG_DAN";
						uLinhDoiDoItem.VatPhamDoi.Count = 10;
					}
					else if (num15 < 50)
					{
						uLinhDoiDoItem.VatPhamDoi.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
						uLinhDoiDoItem.VatPhamDoi.Name = "VP_HOP_VANG";
						uLinhDoiDoItem.VatPhamDoi.Count = 1;
					}
					else if (num15 < 75)
					{
						uLinhDoiDoItem.VatPhamDoi.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
						uLinhDoiDoItem.VatPhamDoi.Name = "VP_HOP_BAC_KEY";
						uLinhDoiDoItem.VatPhamDoi.Count = 1;
					}
					else
					{
						uLinhDoiDoItem.VatPhamDoi.Loai = PhanThuongResponse.LoaiPhanThuong.MANH_VO_CONG;
						uLinhDoiDoItem.VatPhamDoi.Count = 1;
					}
				}
				else
				{
					int num16 = Random.Range(0, 100);
					uLinhDoiDoItem.Diem = 20;
					uLinhDoiDoItem.VatPhamNhan.Loai = PhanThuongResponse.LoaiPhanThuong.BAC;
					uLinhDoiDoItem.VatPhamNhan.Count = 200000;
					int num17 = Random.Range(0, 100);
					if (num17 < 25)
					{
						uLinhDoiDoItem.VatPhamDoi.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
						uLinhDoiDoItem.VatPhamDoi.Name = "VP_BOI_DUONG_DAN";
						uLinhDoiDoItem.VatPhamDoi.Count = 15;
					}
					else if (num17 < 50)
					{
						uLinhDoiDoItem.VatPhamDoi.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
						uLinhDoiDoItem.VatPhamDoi.Name = "VP_HOP_VANG_KEY";
						uLinhDoiDoItem.VatPhamDoi.Count = 1;
					}
					else if (num17 < 75)
					{
						uLinhDoiDoItem.VatPhamDoi.Loai = PhanThuongResponse.LoaiPhanThuong.MANH_VO_CONG;
						uLinhDoiDoItem.VatPhamDoi.Count = 1;
					}
					else
					{
						uLinhDoiDoItem.VatPhamDoi.Loai = PhanThuongResponse.LoaiPhanThuong.MANH_TRANG_BI;
						uLinhDoiDoItem.VatPhamDoi.Count = 2;
					}
				}
			}
			response.ListDoiDo.Add(uLinhDoiDoItem);
		}
	}
}
