using UnityEngine;

public class CostumeItem : MonoBehaviour
{
	public CostumeAvatar avatar;

	public UILabel costumeName;

	public UILabel noteLabel;

	public UserInfo.CostumeData m_Data;

	private void Start()
	{
		avatar.onAvatarClick = OnClickCostumeAvatar;
	}

	public void SetForPopupSelectCostume(UserInfo.CostumeData data)
	{
		if (data == null)
		{
			return;
		}
		m_Data = data;
		avatar.SetInfo(data.CodeName, data.TinhLuyen, data.HoangNgoc, data.HongNgoc, data.LamNgoc, data.TuNgoc);
		CostumeCfg cfg = ConfigManager.instance.m_dicCostumeCfg[data.CodeName];
		costumeName.text = cfg.TenHienThi;
		UICheckbox uICheckbox = GetComponentsInChildren<UICheckbox>(true)[0];
		uICheckbox.radioButtonRoot = base.transform.parent;
		uICheckbox.optionCanBeNone = true;
		if (noteLabel != null)
		{
			UserInfo.HeroData heroData = GameManager.instance.m_GameClient.UserInfo.HeroList.Find((UserInfo.HeroData e) => e.Name == cfg.NhanVat);
			NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[heroData.Name];
			if (heroData.CostumeID == data.ID)
			{
				noteLabel.text = nhanVatCfg.TenHienThi + " " + Localization.instance.Get("UsedLabel");
			}
			else
			{
				noteLabel.text = string.Empty;
			}
		}
	}

	public void OnClickCostumeAvatar(CostumeAvatar ava)
	{
		if (m_Data != null)
		{
			PopupCostume.Create(m_Data, false, false);
		}
	}
}
