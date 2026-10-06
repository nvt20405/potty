using UnityEngine;

public class TanHonItem : MonoBehaviour
{
	public enum TanHonSelectedType
	{
		None = 0,
		TuLuyen = 1,
		TrieuHoi = 2
	}

	public NhanVatAvatar avatar;

	public UILabel nhanVatName;

	public UILabel countLabel;

	public UIButton btnGoi;

	public UILabel statusLabel;

	public UILabel buttonLabel;

	public UISprite spPham;

	public UserInfo.HonNhanVatData m_Data;

	public int TanHonID;

	public TanHonSelectedType tanHonType;

	public void SetDataTanHonTab(UserInfo.HonNhanVatData data)
	{
		if (data == null)
		{
			return;
		}
		m_Data = data;
		avatar.Set(m_Data);
		string text = data.Name;
		if (text.StartsWith("TH_"))
		{
			text = "NV_" + text.Substring(3);
		}
		NhanVatCfg value = null;
		if (!ConfigManager.instance.m_dicNhanVats.TryGetValue(text, out value))
		{
			ConfigManager.instance.m_dicNhanVats.TryGetValue(data.Name, out value);
		}
		if (value != null)
		{
			nhanVatName.text = value.TenHienThi;
			if (value.Hang == 3)
			{
				spPham.spriteName = "giap";
			}
			else if (value.Hang == 2)
			{
				spPham.spriteName = "at";
			}
			else if (value.Hang == 1)
			{
				spPham.spriteName = "binh";
			}
		}
		else
		{
			nhanVatName.text = data.Name;
		}
		UserInfo.HeroData heroData = GameManager.instance.m_GameClient.UserInfo.HeroList.Find((UserInfo.HeroData e) => e.Name == data.Name);
		int num;
		if (heroData != null)
		{
			int capDotPha = heroData.CapDotPha;
			num = ConfigManager.instance.GetTanHonCanDotPha(heroData.Name, capDotPha);
			if (num > data.Quantity)
			{
				statusLabel.text = Localization.instance.Get("TanHonChuaDuDotPha");
				btnGoi.gameObject.SetActive(false);
			}
			else
			{
				countLabel.text = Localization.instance.Get("HonLabel") + " : " + data.Quantity + "/ " + num;
				statusLabel.text = string.Empty;
				btnGoi.gameObject.SetActive(true);
				buttonLabel.text = Localization.instance.Get("TuLuyenBtnLabel");
				tanHonType = TanHonSelectedType.TuLuyen;
			}
		}
		else
		{
			num = ConfigManager.instance.GetTanHonCanTrieuHoi(data.Name);
			if (num > data.Quantity)
			{
				btnGoi.gameObject.SetActive(false);
				statusLabel.text = Localization.instance.Get("TanHonChuaDuTrieuHoi");
			}
			else
			{
				btnGoi.gameObject.SetActive(true);
				buttonLabel.text = Localization.instance.Get("TrieuHoiLabel");
				statusLabel.text = string.Empty;
				tanHonType = TanHonSelectedType.TrieuHoi;
			}
		}
		countLabel.text = Localization.instance.Get("HonLabel") + " : " + data.Quantity + "/ " + num;
	}

	public void TanHonAvatar_OnClick()
	{
		EGDebug.Log("TanHonAvatar_OnClick");
		if (m_Data != null)
		{
			UserInfo.HeroData heroData = new UserInfo.HeroData();
			heroData.HID = 0;
			heroData.Name = m_Data.Name;
			PopupNhanVat.CreateByNormalScreens(GameManager.instance.m_GameClient.UserInfo, heroData);
		}
	}
}
