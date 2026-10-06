using UnityEngine;

public class PopupSonMonChienSuItem : MonoBehaviour
{
	public UILabel Description;

	public UILabel Title;

	public UISprite Avatar;

	public UserInfo.SonMonLog chienBao;

	public GameObject PhanCongBtn;

	public int LuotDoTham;

	public GameObject KNBDoThamGrp;

	public UILabel KNBDoThamLabel;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Set(UserInfo.SonMonLog chienBao, bool isReadOnly)
	{
		int num = 5;
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.Vip >= 12)
		{
			num = 15;
		}
		else if (GameManager.instance.m_GameClient.UserInfo.Gamer.Vip >= 9)
		{
			num = 13;
		}
		else if (GameManager.instance.m_GameClient.UserInfo.Gamer.Vip >= 5)
		{
			num = 11;
		}
		else if (GameManager.instance.m_GameClient.UserInfo.Gamer.Vip >= 3)
		{
			num = 9;
		}
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("DoThamSM"))
		{
			for (int i = 1; i < num; i++)
			{
				if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("DoThamSM" + i + ";"))
				{
					LuotDoTham = i + 1;
					break;
				}
			}
		}
		else
		{
			LuotDoTham = 1;
		}
		if (SonMonHelper.GetGiaDoThamSonMon(LuotDoTham) > 0)
		{
			KNBDoThamGrp.SetActive(true);
			KNBDoThamLabel.text = SonMonHelper.GetGiaDoThamSonMon(LuotDoTham).ToString();
		}
		else
		{
			KNBDoThamGrp.SetActive(false);
		}
		this.chienBao = chienBao;
		if (!isReadOnly)
		{
			Title.text = string.Format(Localization.instance.Get("ChienBaoTitle"), chienBao.DisplayName1);
		}
		else if (chienBao.Score1 > chienBao.Score2)
		{
			Title.text = string.Format(Localization.instance.Get("ChienBaoTitleThang"), chienBao.DisplayName1, chienBao.DisplayName2);
		}
		else
		{
			Title.text = string.Format(Localization.instance.Get("ChienBaoTitleThua"), chienBao.DisplayName1, chienBao.DisplayName2);
		}
		if (isReadOnly)
		{
			Description.text = Localization.instance.Get("ChienBaoReward") + chienBao.Reward;
		}
		else
		{
			Description.text = Localization.instance.Get("ChienBaoCost") + chienBao.Reward;
		}
		Avatar.spriteName = chienBao.Team1[0];
		PhanCongBtn.gameObject.SetActive(!isReadOnly);
	}

	public void OnPhanCongBtn()
	{
		GameManager.instance.m_GameClient.RequestDoThamSonMonThuDich(chienBao.GID1, chienBao.SID1);
		if (PopupSonMonChienSu.instance != null)
		{
			Object.Destroy(PopupSonMonChienSu.instance.gameObject);
		}
	}
}
