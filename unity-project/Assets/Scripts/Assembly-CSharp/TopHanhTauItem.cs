using UnityEngine;

public class TopHanhTauItem : MonoBehaviour
{
	public UILabel lbSTT;

	public UILabel lbName;

	public UILabel lbGiangHo;

	public UILabel lbAiGiangHo;

	public void setData(CacLoaiTopResponse.TopHanhTauData data, int stt)
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
			lbGiangHo.text = data.TenGiangHo;
			lbAiGiangHo.text = data.TenAiGiangHo;
			return;
		}
		UILabel uILabel = lbName;
		string empty = string.Empty;
		lbAiGiangHo.text = empty;
		empty = empty;
		lbGiangHo.text = empty;
		uILabel.text = empty;
	}
}
