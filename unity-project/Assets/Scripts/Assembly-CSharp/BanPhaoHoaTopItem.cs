using UnityEngine;

public class BanPhaoHoaTopItem : MonoBehaviour
{
	public UILabel displayName;

	public UILabel rank;

	public UILabel score;

	public PhanThuongResponse pt;

	public void Create(string displayName, int rank, int score, PhanThuongResponse pt)
	{
		base.gameObject.SetActive(false);
		BanPhaoHoaTopItem banPhaoHoaTopItem = (BanPhaoHoaTopItem)Object.Instantiate(this);
		banPhaoHoaTopItem.transform.parent = base.transform.parent;
		banPhaoHoaTopItem.transform.localPosition = base.transform.localPosition + (GetComponent<BoxCollider>().size.y + 5f) * (float)(rank - 1) * Vector3.down;
		banPhaoHoaTopItem.transform.localScale = Vector3.one;
		banPhaoHoaTopItem.gameObject.SetActive(true);
		banPhaoHoaTopItem.displayName.text = displayName;
		banPhaoHoaTopItem.rank.text = rank.ToString();
		banPhaoHoaTopItem.score.text = score.ToString();
		banPhaoHoaTopItem.pt = pt;
		base.gameObject.SetActive(false);
	}

	public void ShowPhanThuong()
	{
		PopupDanhSachPhanThuong.Create(string.Empty, string.Empty, pt);
	}
}
