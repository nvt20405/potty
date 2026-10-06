using UnityEngine;

public class TopChuyenSinhItem : MonoBehaviour
{
	public UILabel lbSTT;

	public UILabel lbName;

	public UILabel lbSoLuongNVChuyenSinh;

	public void setData(CacLoaiTopResponse.TopChuyenSinhData data, int stt)
	{
		if (stt > 0)
		{
			lbSTT.text = stt.ToString();
		}
		else
		{
			lbSTT.text = string.Empty;
		}
		if (data != null)
		{
			lbName.text = data.UserName;
			lbSoLuongNVChuyenSinh.text = data.SoLuong.ToString();
			return;
		}
		UILabel uILabel = lbName;
		string empty = string.Empty;
		lbSoLuongNVChuyenSinh.text = empty;
		uILabel.text = empty;
	}
}
