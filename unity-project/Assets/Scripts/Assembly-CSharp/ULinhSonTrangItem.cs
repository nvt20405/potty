using UnityEngine;

public class ULinhSonTrangItem : MonoBehaviour
{
	public PhanThuongItem fromAvatar;

	public PhanThuongItem toAvatar;

	public UIButton btnDoi;

	public UILabel lbDiemThuong;

	public UILabel lbDaDoi;

	public int mIndex;

	public ULinhInfoResponse.ULinhDoiDoItem m_Data;

	public void setData(ULinhInfoResponse.ULinhDoiDoItem dataItem, int index)
	{
		if (dataItem != null)
		{
			m_Data = dataItem;
			mIndex = index;
			fromAvatar.Set(dataItem.VatPhamDoi, false);
			toAvatar.Set(dataItem.VatPhamNhan, false);
			fromAvatar.displayInfo(fromAvatar.nameLabel.text + " x" + dataItem.VatPhamDoi.Count, string.Empty);
			toAvatar.displayInfo(toAvatar.nameLabel.text + " x" + dataItem.VatPhamNhan.Count, string.Empty);
			if (dataItem.Diem > 0)
			{
				lbDiemThuong.text = "+" + dataItem.Diem + " " + Localization.instance.Get("DiemLabel");
				btnDoi.gameObject.SetActive(true);
				lbDaDoi.gameObject.SetActive(false);
			}
			else
			{
				btnDoi.gameObject.SetActive(false);
				lbDaDoi.gameObject.SetActive(true);
			}
		}
	}
}
