using UnityEngine;

public class LanhDiaTopItem : MonoBehaviour
{
	public UILabel DisplayName;

	public UILabel TopLabel;

	public UILabel ScoreLabel;

	private PhanThuongResponse Phanthuong;

	public void Set(string displayName, int top, PhanThuongResponse phanthuong)
	{
		DisplayName.text = displayName;
		TopLabel.text = top.ToString();
		Phanthuong = phanthuong;
		if (ScoreLabel != null)
		{
			ScoreLabel.text = string.Empty;
		}
	}

	public void Set(string displayName, int top, PhanThuongResponse phanthuong, int Score)
	{
		DisplayName.text = displayName;
		TopLabel.text = top.ToString();
		Phanthuong = phanthuong;
		ScoreLabel.text = Score.ToString();
	}

	public void OnShowPhanThuong()
	{
		PopupDanhSachPhanThuong.Create(string.Empty, string.Empty, Phanthuong);
	}
}
