using UnityEngine;

public class TopNgocItem : MonoBehaviour
{
	public UILabel lbSTT;

	public UILabel lbName;

	public UILabel lbInfo;

	public int ID;

	public void setData(CacLoaiTopResponse.TopNgocData data, int stt)
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
			lbInfo.text = data.SoNgoc.ToString();
			return;
		}
		UILabel uILabel = lbName;
		string empty = string.Empty;
		lbInfo.text = empty;
		uILabel.text = empty;
	}
}
