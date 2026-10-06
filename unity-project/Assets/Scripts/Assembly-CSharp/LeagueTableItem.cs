using UnityEngine;

public class LeagueTableItem : MonoBehaviour
{
	public UILabel displayName;

	public UISprite avatar;

	public UILabel level;

	public UILabel rank;

	public UILabel point;

	public UILabel star;

	public UISprite hang;

	public UILabel lenHangLabel;

	public GameObject lenHangGrid;

	public UILabel xuongHangLabel;

	public GameObject xuongHangGrid;

	private int rankID;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Init(LeagueGamer gamer, int rank)
	{
		rankID = rank;
		avatar.spriteName = gamer.Avatar;
		displayName.text = "S" + gamer.SID + ". " + gamer.DisplayName;
		if (rank > GameManager.instance.m_GameClient.UserInfo.LeagueInfo.promotedCount)
		{
			this.rank.text = string.Format(Localization.instance.Get("Hang"), rank);
			if (rank < 9 - GameManager.instance.m_GameClient.UserInfo.LeagueInfo.relegatedCount)
			{
				hang.spriteName = "Daihoi_Top3";
			}
			else
			{
				hang.spriteName = "Daihoi_Top4";
			}
		}
		else
		{
			this.rank.text = string.Format(Localization.instance.Get("Hang"), rank);
			hang.spriteName = "Daihoi_Top1";
		}
		if (rank == GameManager.instance.m_GameClient.UserInfo.LeagueInfo.promotedCount && gamer.Rank < 3)
		{
			lenHangLabel.text = Localization.instance.Get("LenHang");
			lenHangGrid.gameObject.SetActive(true);
		}
		else
		{
			lenHangLabel.text = string.Empty;
			lenHangGrid.gameObject.SetActive(false);
		}
		if (rank == 9 - GameManager.instance.m_GameClient.UserInfo.LeagueInfo.relegatedCount && gamer.Rank > 1)
		{
			xuongHangLabel.text = Localization.instance.Get("XuongHang");
			xuongHangGrid.gameObject.SetActive(true);
		}
		else
		{
			xuongHangLabel.text = string.Empty;
			xuongHangGrid.gameObject.SetActive(false);
		}
		level.text = gamer.Level.ToString();
		point.text = gamer.Point.ToString();
		star.text = gamer.Star.ToString();
	}

	public void OnShowPhanThuong()
	{
		PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTitle"), Localization.instance.Get("PhanThuongSeNhanDuoc"), GameManager.instance.m_GameClient.UserInfo.LeagueInfo.PhanThuongList[rankID - 1]);
	}
}
