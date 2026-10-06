using UnityEngine;

public class PopupSonMonEventItem : MonoBehaviour
{
	private PhanThuongResponse phanthuong;

	public UISprite AvatarSprite;

	public UILabel NameLabel;

	public UILabel RankLabel;

	public UILabel ScoreLabel;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Set(int rank, string name, string avatar, int score, PhanThuongResponse phanthuong)
	{
		this.phanthuong = phanthuong;
		AvatarSprite.spriteName = avatar;
		NameLabel.text = name;
		RankLabel.text = rank.ToString();
		ScoreLabel.text = Localization.instance.Get("DanhVong") + ": " + score;
	}

	public void ShowPhanThuong()
	{
		PopupDanhSachPhanThuong.Create(string.Empty, string.Empty, phanthuong, PopupSonMonEvent.Resume);
		base.transform.parent.parent.parent.GetComponent<PopupSonMonEvent>().Close();
	}
}
