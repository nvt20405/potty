using System.Collections.Generic;
using UnityEngine;

public class PopupSelectTrangBiItem : MonoBehaviour
{
	public OtherAvatar avatar;

	public UILabel trangBiName;

	public UILabel heroName;

	public UISprite spChiSo;

	public UILabel lbChiSo;

	public UISprite spPham;

	public UISprite spChiSo2;

	public UILabel lbChiSo2;

	public GameObject grpChiSo1;

	public GameObject grpChiSo2;

	public UserInfo.TrangBiData m_Data;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Set(UserInfo.TrangBiData data)
	{
		m_Data = data;
		avatar.SetTrangBiAvatar(data);
		Utils.SetLayer(avatar.gameObject.transform, "GUIPopUp", true);
		TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[data.Name];
		trangBiName.text = trangBiCfg.TenHienThi;
		if (trangBiCfg.Hang == ItemClass.Giap)
		{
			spPham.spriteName = "giap";
		}
		else if (trangBiCfg.Hang == ItemClass.At)
		{
			spPham.spriteName = "at";
		}
		else if (trangBiCfg.Hang == ItemClass.Binh)
		{
			spPham.spriteName = "binh";
		}
		if (data.HID > 0)
		{
			UserInfo.HeroData heroData = GameManager.instance.m_GameClient.UserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == data.HID);
			NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[heroData.Name];
			heroName.text = nhanVatCfg.TenHienThi + " " + Localization.instance.Get("UsedLabel");
		}
		else
		{
			heroName.text = string.Empty;
		}
		UICheckbox componentInChildren = GetComponentInChildren<UICheckbox>();
		componentInChildren.radioButtonRoot = base.transform.parent;
		componentInChildren.optionCanBeNone = true;
		List<int> chiSo = trangBiCfg.GetChiSo(data);
		grpChiSo1.SetActive(false);
		grpChiSo2.SetActive(false);
		if (chiSo == null || chiSo.Count != 4)
		{
			return;
		}
		bool flag = false;
		for (int num = 0; num < chiSo.Count; num++)
		{
			if (num == 0 && chiSo[0] > 0)
			{
				spChiSo.spriteName = "icon_menh";
				lbChiSo.text = chiSo[0].ToString();
				grpChiSo1.SetActive(true);
				flag = true;
			}
			if (num == 1 && chiSo[1] > 0)
			{
				if (flag)
				{
					spChiSo2.spriteName = "icon_ngoai";
					lbChiSo2.text = chiSo[1].ToString();
					grpChiSo2.SetActive(true);
				}
				else
				{
					spChiSo.spriteName = "icon_ngoai";
					lbChiSo.text = chiSo[1].ToString();
					grpChiSo1.SetActive(true);
					flag = true;
				}
			}
			if (num == 2 && chiSo[2] > 0)
			{
				if (flag)
				{
					spChiSo2.spriteName = "icon_than";
					lbChiSo2.text = chiSo[2].ToString();
					grpChiSo2.SetActive(true);
				}
				else
				{
					spChiSo.spriteName = "icon_than";
					lbChiSo.text = chiSo[2].ToString();
					grpChiSo1.SetActive(true);
					flag = true;
				}
			}
			if (num == 3 && chiSo[3] > 0)
			{
				if (flag)
				{
					spChiSo2.spriteName = "icon_noi_goc";
					lbChiSo2.text = chiSo[3].ToString();
					grpChiSo2.SetActive(true);
				}
				else
				{
					spChiSo.spriteName = "icon_noi_goc";
					lbChiSo.text = chiSo[3].ToString();
					grpChiSo1.SetActive(true);
					flag = true;
				}
			}
		}
	}
}
