using UnityEngine;

public class TopThienMaItem : MonoBehaviour
{
	public UILabel lbSTT;

	public UILabel lbName;

	public UILabel lbChiSo;

	public void setData(CacLoaiTopResponse.TopThienMaData data, int stt)
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
			lbChiSo.text = data.TotalDiem.ToString();
			return;
		}
		UILabel uILabel = lbName;
		string empty = string.Empty;
		lbChiSo.text = empty;
		uILabel.text = empty;
	}
}
