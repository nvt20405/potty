using UnityEngine;

public class TopCongLucItem : MonoBehaviour
{
	public UILabel lbSTT;

	public UILabel lbName;

	public UILabel lbChiSo;

	public int ID;

	public void setData(CacLoaiTopResponse.TopCongLucData data, int stt)
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
			lbName.text = data.UserName + "\n[8E0707]" + data.NhanVatName;
			lbChiSo.text = data.ChiSo.ToString();
			return;
		}
		UILabel uILabel = lbName;
		string empty = string.Empty;
		lbChiSo.text = empty;
		uILabel.text = empty;
	}
}
