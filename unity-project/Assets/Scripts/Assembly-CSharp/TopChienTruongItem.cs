using UnityEngine;

public class TopChienTruongItem : MonoBehaviour
{
	public UILabel lbSoThuTu;

	public UILabel lbName;

	public UILabel lbInfo;

	public void setData(CacLoaiTopResponse.TopChienTruongData data, int soThuTu)
	{
		if (data != null)
		{
			lbName.text = data.DisplayName;
			lbInfo.text = data.pkChienTruong.ToString();
		}
		else
		{
			UILabel uILabel = lbName;
			string empty = string.Empty;
			lbInfo.text = empty;
			uILabel.text = empty;
		}
		if (soThuTu > 0)
		{
			lbSoThuTu.text = soThuTu.ToString();
		}
		else
		{
			lbSoThuTu.text = string.Empty;
		}
	}
}
