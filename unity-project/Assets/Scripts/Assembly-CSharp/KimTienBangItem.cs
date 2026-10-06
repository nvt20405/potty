using UnityEngine;

public class KimTienBangItem : MonoBehaviour
{
	public UILabel lbInfo;

	public UISprite spChuaDuDk;

	public GameObject daNhanGrp;

	public UserInfo.ServerData.MocTichLuyTieu mThuongTieuData;

	public int IdxMocThuongTieu = -1;

	public UIButton btnDoi;

	public void setData(UserInfo.ServerData.MocTichLuyTieu mocThuongTieu, int indexMocTieu, int soKNBDaTieu)
	{
		mThuongTieuData = mocThuongTieu;
		IdxMocThuongTieu = indexMocTieu;
		lbInfo.text = ((mocThuongTieu == null) ? string.Empty : mocThuongTieu.GiaTri.ToString());
		if (soKNBDaTieu >= mocThuongTieu.GiaTri)
		{
			spChuaDuDk.gameObject.SetActive(false);
		}
		else
		{
			spChuaDuDk.gameObject.SetActive(true);
		}
		if (CommonHelper.CheckFlag(GameManager.instance.m_GameClient.UserInfo.Gamer.TichLuyTieuFlag, indexMocTieu))
		{
			daNhanGrp.gameObject.SetActive(true);
		}
		else
		{
			daNhanGrp.gameObject.SetActive(false);
		}
		bool flag = CommonHelper.CheckFlag(GameManager.instance.m_GameClient.UserInfo.Gamer.TichLuyTieuFlag, IdxMocThuongTieu);
		btnDoi.gameObject.SetActive(!flag);
	}
}
