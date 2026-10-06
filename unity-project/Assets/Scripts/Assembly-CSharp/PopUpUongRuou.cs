using UnityEngine;

public class PopUpUongRuou : MonoBehaviour
{
	public NhanVatAvatar deTuAvatar;

	public UILabel lbDescription;

	public UILabel lbTiemLuc;

	private UserInfo.HeroData m_DeTuData;

	public static PopUpUongRuou instance;

	public void OnCloseClick()
	{
		DestroyPopup();
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public static void Create(UserInfo.HeroData data, int timeLuc)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupUongRuou"))).GetComponent<PopUpUongRuou>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.Set(data, timeLuc);
	}

	public void Set(UserInfo.HeroData data, int timeLuc)
	{
		m_DeTuData = data;
		deTuAvatar.Set(data);
		NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[data.Name];
		lbDescription.text = string.Format(Localization.instance.Get("UongRuouThanhCongMess"), nhanVatCfg.TenHienThi);
		lbTiemLuc.text = Localization.instance.Get("TiemLucLabel") + " + " + timeLuc;
	}
}
