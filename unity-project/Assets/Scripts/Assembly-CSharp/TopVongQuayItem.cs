using UnityEngine;

public class TopVongQuayItem : MonoBehaviour
{
	public UILabel rankLabel;

	public UILabel nameLabel;

	public UILabel scoreLabel;

	public PhanThuongResponse pt;

	public Vector3 offset;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Create(int rank, string name, int score, PhanThuongResponse pt)
	{
		base.gameObject.SetActive(true);
		TopVongQuayItem topVongQuayItem = (TopVongQuayItem)Object.Instantiate(this);
		topVongQuayItem.transform.parent = base.transform.parent;
		topVongQuayItem.transform.localScale = Vector3.one;
		topVongQuayItem.transform.localPosition = base.transform.localPosition + offset * (rank - 1);
		topVongQuayItem.rankLabel.text = rank + ".";
		topVongQuayItem.nameLabel.text = name;
		topVongQuayItem.scoreLabel.text = score.ToString();
		topVongQuayItem.pt = pt;
		base.gameObject.SetActive(false);
	}

	public void ShowPhanThuong()
	{
		PopupDanhSachPhanThuong.Create(string.Empty, string.Empty, pt);
	}
}
