using UnityEngine;

public class BanPhaoHoaPhanThuongItem : MonoBehaviour
{
	public UILabel MocLabel;

	public UILabel DiemLabel;

	public GameObject onGrp;

	public GameObject offGrp;

	public GameObject NhanThuongGrp;

	public GameObject DaNhanThuongGrp;

	public PhanThuongResponse pt;

	public void Create(int moc, int diem, PhanThuongResponse pt)
	{
		base.gameObject.SetActive(false);
		BanPhaoHoaPhanThuongItem banPhaoHoaPhanThuongItem = (BanPhaoHoaPhanThuongItem)Object.Instantiate(this);
		banPhaoHoaPhanThuongItem.transform.parent = base.transform.parent;
		banPhaoHoaPhanThuongItem.transform.localPosition = base.transform.localPosition + (GetComponent<BoxCollider>().size.y + 5f) * (float)(moc - 1) * Vector3.down;
		banPhaoHoaPhanThuongItem.transform.localScale = Vector3.one;
		banPhaoHoaPhanThuongItem.gameObject.SetActive(true);
		banPhaoHoaPhanThuongItem.DiemLabel.text = diem.ToString();
		banPhaoHoaPhanThuongItem.MocLabel.text = Localization.instance.Get("MocLabel") + " " + moc;
		banPhaoHoaPhanThuongItem.pt = pt;
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.PhaoHoaInfo.PhaoHoaCount > diem)
		{
			onGrp.SetActive(true);
			offGrp.SetActive(false);
			if (diem > GameManager.instance.m_GameClient.UserInfo.Gamer.PhaoHoaDaNhanThuong)
			{
				banPhaoHoaPhanThuongItem.DaNhanThuongGrp.SetActive(false);
				banPhaoHoaPhanThuongItem.NhanThuongGrp.SetActive(true);
			}
			else
			{
				banPhaoHoaPhanThuongItem.DaNhanThuongGrp.SetActive(true);
				banPhaoHoaPhanThuongItem.NhanThuongGrp.SetActive(false);
			}
		}
		else
		{
			banPhaoHoaPhanThuongItem.onGrp.SetActive(false);
			banPhaoHoaPhanThuongItem.offGrp.SetActive(true);
			banPhaoHoaPhanThuongItem.DaNhanThuongGrp.SetActive(true);
			banPhaoHoaPhanThuongItem.NhanThuongGrp.SetActive(false);
		}
		base.gameObject.SetActive(false);
	}

	public void ShowPhanThuong()
	{
		PopupDanhSachPhanThuong.Create(string.Empty, string.Empty, pt);
	}

	public void NhanThuong()
	{
		GameManager.instance.m_GameClient.RequestLinhThuongPhaoHoaEvent(int.Parse(DiemLabel.text));
	}
}
