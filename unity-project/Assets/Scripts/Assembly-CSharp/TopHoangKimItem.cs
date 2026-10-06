using UnityEngine;

public class TopHoangKimItem : MonoBehaviour
{
	public UILabel lbSTT;

	public UILabel lbName;

	public UILabel lbSoLuong;

	public void setData(CacLoaiTopResponse.TopHoangKimData data, int stt)
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
			lbSoLuong.text = data.SoLuong.ToString();
			return;
		}
		UILabel uILabel = lbName;
		string empty = string.Empty;
		lbSoLuong.text = empty;
		uILabel.text = empty;
	}
}
