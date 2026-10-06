using UnityEngine;

public class TopTrangBiHKItem : MonoBehaviour
{
	public UILabel lbSTT;

	public UILabel lbName;

	public UILabel lbChiSo3;

	public UILabel lbChiSo2;

	public UILabel lbChiSo1;

	public void setData(CacLoaiTopResponse.TopTrangBiHoangKimData data, int stt)
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
			lbName.text = data.DisplayName;
			lbChiSo3.text = data.TopTrangBiHKCap3.ToString();
			lbChiSo2.text = data.TopTrangBiHKCap2.ToString();
			lbChiSo1.text = data.TopTrangBiHKCap1.ToString();
			return;
		}
		UILabel uILabel = lbName;
		string empty = string.Empty;
		lbChiSo1.text = empty;
		empty = empty;
		lbChiSo2.text = empty;
		empty = empty;
		lbChiSo3.text = empty;
		uILabel.text = empty;
	}
}
