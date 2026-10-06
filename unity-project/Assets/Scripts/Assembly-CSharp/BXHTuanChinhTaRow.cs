using UnityEngine;

public class BXHTuanChinhTaRow : MonoBehaviour
{
	public UILabel xepHangLbl;

	public UILabel vipLbl;

	public UILabel tenLbl;

	public UILabel diemLbl;

	private PhanThuongResponse phanThuong;

	public void SetInfo(string hang, int vip, string name, int diemChienTich, PhanThuongResponse phanThuong)
	{
		xepHangLbl.text = hang;
		vipLbl.text = vip.ToString();
		tenLbl.text = name;
		diemLbl.text = diemChienTich.ToString();
		this.phanThuong = phanThuong;
	}

	private void OnPhanThuongBtnClick()
	{
		if (phanThuong != null)
		{
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("CT2BXHTuanPhanThuongTitle"), Localization.instance.Get("CT2BXHTuanPhanThuongDesc"), phanThuong);
		}
	}
}
