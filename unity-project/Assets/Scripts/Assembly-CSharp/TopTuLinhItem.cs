using UnityEngine;

public class TopTuLinhItem : MonoBehaviour
{
	public UILabel lbSTT;

	public UILabel lbName;

	public UILabel lbPhamChat;

	public UILabel lbChiSo;

	public void setData(CacLoaiTopResponse.TopTuLinhData data, int stt)
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
			lbPhamChat.text = data.PhamChat.ToString();
			lbChiSo.text = data.ChiSo.ToString();
			return;
		}
		UILabel uILabel = lbName;
		string empty = string.Empty;
		lbChiSo.text = empty;
		empty = empty;
		lbPhamChat.text = empty;
		uILabel.text = empty;
	}
}
