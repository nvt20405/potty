using UnityEngine;

public class TonHieuDetailItem : MonoBehaviour
{
	public UISprite spBall;

	public UILabel lbTonHieuName;

	public UILabel lbTonHieuDes;

	public UICheckbox cbUse;

	private UserInfo.GamerData.TonHieuType curType;

	private int curTOP;

	public void setData(TonHieuData detail, UserInfo.GamerData.TonHieuType type)
	{
		if (detail == null)
		{
			return;
		}
		curType = type;
		curTOP = detail.TOP;
		string text = detail.Name;
		lbTonHieuDes.text = detail.Details;
		spBall.spriteName = "tonhieu_ball_top" + detail.TOP;
		cbUse.gameObject.SetActive(false);
		switch (type)
		{
		case UserInfo.GamerData.TonHieuType.LEVEL:
		{
			int level = GameManager.instance.m_GameClient.UserInfo.Gamer.Level;
			if ((detail.TOP == 5 && level < 35) || (detail.TOP == 4 && level >= 35 && level <= 50) || (detail.TOP == 3 && level >= 51 && level <= 70) || (detail.TOP == 2 && level >= 71 && level <= 90) || (detail.TOP == 1 && level >= 91))
			{
				cbUse.gameObject.SetActive(true);
				if (GameManager.instance.m_GameClient.UserInfo.Gamer.CurTonHieu == UserInfo.GamerData.TonHieuType.LEVEL)
				{
					cbUse.isChecked = true;
					text = "[074007]" + detail.Name + "[-]";
				}
				else
				{
					cbUse.isChecked = false;
				}
			}
			break;
		}
		case UserInfo.GamerData.TonHieuType.HANH_TAU:
		{
			int count = GameManager.instance.m_GameClient.UserInfo.GiangHo.Count;
			if ((detail.TOP == 5 && count <= 20) || (detail.TOP == 4 && count >= 21 && count <= 40) || (detail.TOP == 3 && count >= 41 && count <= 60) || (detail.TOP == 2 && count >= 61 && count <= 70) || (detail.TOP == 1 && count >= 71))
			{
				cbUse.gameObject.SetActive(true);
				if (GameManager.instance.m_GameClient.UserInfo.Gamer.CurTonHieu == UserInfo.GamerData.TonHieuType.HANH_TAU)
				{
					cbUse.isChecked = true;
					text = "[074007]" + detail.Name + "[-]";
				}
				else
				{
					cbUse.isChecked = false;
				}
			}
			break;
		}
		default:
			if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(TonHieuInfo.getStringCheck(type, detail.TOP)))
			{
				cbUse.gameObject.SetActive(true);
				if (GameManager.instance.m_GameClient.UserInfo.Gamer.CurTonHieu == type)
				{
					cbUse.isChecked = true;
					text = "[074007]" + detail.Name + "[-]";
				}
				else
				{
					cbUse.isChecked = false;
				}
			}
			break;
		}
		UIEventListener.Get(cbUse.gameObject).onClick = OnCheckBoxActive;
		lbTonHieuName.text = text;
	}

	public void OnCheckBoxActive(GameObject go)
	{
		if (cbUse.isChecked)
		{
			SetTonHieuRequest setTonHieuRequest = new SetTonHieuRequest();
			setTonHieuRequest.tonHieu = curType;
			setTonHieuRequest.TOP = curTOP;
			GameManager.instance.m_GameClient.RequestSetTonHieu(setTonHieuRequest);
		}
	}
}
