using UnityEngine;

public class TopTinhLuyenItem : MonoBehaviour
{
	public UILabel lbSTT;

	public UILabel lbName;

	public UILabel lbCap;

	public void setData(CacLoaiTopResponse.TopTinhLuyenData data, int stt)
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
			lbCap.text = data.TinhLuyenCap5.ToString();
			return;
		}
		UILabel uILabel = lbName;
		string empty = string.Empty;
		lbCap.text = empty;
		uILabel.text = empty;
	}
}
