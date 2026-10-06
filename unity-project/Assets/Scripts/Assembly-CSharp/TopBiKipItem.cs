using UnityEngine;

public class TopBiKipItem : MonoBehaviour
{
	public UILabel lbSTT;

	public UILabel lbName;

	public UILabel lbBiKip;

	public int ID;

	public void setData(CacLoaiTopResponse.TopBiKipData data, int stt)
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
			lbBiKip.text = data.SoBiKipMaxLevel.ToString();
			return;
		}
		UILabel uILabel = lbName;
		string empty = string.Empty;
		lbBiKip.text = empty;
		uILabel.text = empty;
	}
}
