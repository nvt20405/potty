using UnityEngine;

public class LuanKiemListItem : MonoBehaviour
{
	public enum FeatureButton
	{
		None = 0,
		Danh = 1,
		DoiHinh = 2,
		LamMoi = 3
	}

	public UILabel hangLabel;

	public UILabel nameLabel;

	public UILabel descLabel;

	public UILabel levelLabel;

	public UISprite bg;

	public NhanVatAvatar avatar;

	public GameObject fightBtn;

	public GameObject doiHinhBtn;

	public GameObject refreshBtn;

	private int id = -1;

	private int botId = -1;

	public void SetInfo(string name, string codename, int level, int hang, string desc, int id, int botId, FeatureButton buttonshown)
	{
		nameLabel.text = name;
		levelLabel.text = level.ToString();
		hangLabel.text = hang.ToString();
		descLabel.text = desc;
		this.id = id;
		this.botId = botId;
		avatar.Set(codename);
		if (buttonshown == FeatureButton.LamMoi)
		{
			bg.spriteName = "tab7";
		}
		else if (hang <= 10)
		{
			bg.spriteName = "bgr_tab4";
		}
		else
		{
			bg.spriteName = "bgr_tab9";
		}
		fightBtn.SetActive(buttonshown == FeatureButton.Danh);
		doiHinhBtn.SetActive(buttonshown == FeatureButton.DoiHinh);
		refreshBtn.SetActive(buttonshown == FeatureButton.LamMoi);
		if (id <= 0 && botId > 0)
		{
			doiHinhBtn.SetActive(false);
		}
	}

	private void OnFightBtnClick()
	{
		int vip = GameManager.instance.m_GameClient.UserInfo.Gamer.Vip;
		int luotLuanKiem = GameManager.instance.m_GameClient.UserInfo.LuanKiem.LuotLuanKiem;
		if (luotLuanKiem >= ConfigManager.GetLuotLuanKiemByVip(vip))
		{
			PopUpCheckLuanKiem.Create();
		}
		else
		{
			GameManager.instance.m_GameClient.DauLuanKiem(id, botId);
		}
	}

	private void OnRefreshBtnClick()
	{
		GameManager.instance.m_GameClient.GetLuanKiemInfo();
	}

	private void OnDoiHinhBtnClick()
	{
		if (id > 0)
		{
			XemThongTinMonPhaiRequest xemThongTinMonPhaiRequest = new XemThongTinMonPhaiRequest();
			xemThongTinMonPhaiRequest.TargetGID = id;
			GameManager.instance.m_GameClient.RequestXemThongTinMonPhai(xemThongTinMonPhaiRequest);
		}
	}
}
