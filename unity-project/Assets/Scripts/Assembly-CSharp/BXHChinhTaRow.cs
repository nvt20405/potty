using UnityEngine;

public class BXHChinhTaRow : MonoBehaviour
{
	public UILabel xepHangLbl;

	public UILabel vipLbl;

	public UILabel tenLbl;

	public UILabel diemLbl;

	public UILabel dietLbl;

	public void SetInfo(int hang, int vip, string name, int serverid, int diem, int kill)
	{
		xepHangLbl.text = string.Format(Localization.instance.Get("CT2BXHHangLabel"), hang);
		vipLbl.text = vip.ToString();
		tenLbl.text = string.Format("s{0}.{1}", serverid, name);
		diemLbl.text = diem.ToString();
		dietLbl.text = kill.ToString();
	}
}
