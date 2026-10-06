using UnityEngine;

public class BXHBanhChungItem : MonoBehaviour
{
	public UILabel labelHang;

	public NhanVatAvatar avatar;

	public UILabel labelSoBanh;

	public UILabel labelName;

	public PhanThuongResponse phanThuong;

	public void Set(BanhChungBXHResponse.BXHItem it, int hang, PhanThuongResponse pt)
	{
		labelHang.text = string.Format(Localization.instance.Get("DongNhanHangLabel"), hang);
		phanThuong = pt;
		if (it != null)
		{
			labelName.gameObject.SetActive(true);
			labelSoBanh.gameObject.SetActive(true);
			avatar.gameObject.SetActive(true);
			labelSoBanh.text = string.Format(Localization.instance.Get("LabelSoBanhTop"), it.SoBanh);
			labelName.text = string.Format("s{0}.{1}", it.SID, it.UName);
			avatar.Set(it.UAva);
		}
		else
		{
			labelName.gameObject.SetActive(false);
			labelSoBanh.gameObject.SetActive(false);
			avatar.gameObject.SetActive(false);
		}
	}

	private void OnPhanThuongBtnClick()
	{
		PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTopBanhChungTitle"), Localization.instance.Get("PhanThuongTopBanhChungDesc"), phanThuong);
	}
}
