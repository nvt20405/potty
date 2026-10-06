using System.Collections.Generic;
using UnityEngine;

public class PopupDuyenNhanVat : MonoBehaviour
{
	public NhanVatAvatar nhanvatAvatar;

	public UILabel nhanVatName;

	public UILabel MenhLabel;

	public UILabel NgoaiLabel;

	public UILabel ThanPhapLabel;

	public UILabel KhiLabel;

	public GameObject duyenNVPrefab;

	public GameObject ItemRoot;

	private UserInfo RefUserInfo;

	private UserInfo.HeroData mHeroData;

	private NhanVatCfg mHeroConfig;

	public UISprite spPham;

	public static PopupDuyenNhanVat instance;

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

	public static void Create(UserInfo ref_user_info, UserInfo.HeroData data)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupDuyenNhanVat"))).GetComponent<PopupDuyenNhanVat>();
		PopupManager.instance.Add(instance.gameObject);
		instance.RefUserInfo = ref_user_info;
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.Set(data);
	}

	public static void Create(NhanVatCfg cfgData)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupDuyenNhanVat"))).GetComponent<PopupDuyenNhanVat>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.Set(cfgData);
	}

	private void Set(NhanVatCfg cfgData)
	{
		mHeroConfig = cfgData;
		nhanvatAvatar.Set(cfgData.Name);
		nhanVatName.text = cfgData.TenHienThi;
		if (cfgData.Hang == 3)
		{
			spPham.spriteName = "giap";
		}
		else if (cfgData.Hang == 2)
		{
			spPham.spriteName = "at";
		}
		else if (cfgData.Hang == 1)
		{
			spPham.spriteName = "binh";
		}
		List<NhanVatCfg.DuyenPhanCfg> duyenPhan = cfgData.DuyenPhan;
		MenhLabel.text = cfgData.Menh.ToString();
		NgoaiLabel.text = cfgData.Ngoai.ToString();
		ThanPhapLabel.text = cfgData.ThanPhap.ToString();
		KhiLabel.text = cfgData.Noi.ToString();
		if (duyenPhan == null || duyenPhan.Count <= 0)
		{
			return;
		}
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 200f, -3f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, -180f, 0f);
		for (int i = 0; i < duyenPhan.Count; i++)
		{
			if (duyenPhan[i] != null)
			{
				bool isActiveDuyen = false;
				bool isLock = false;
				DuyenNhanVatItem component = ((GameObject)Object.Instantiate(duyenNVPrefab)).GetComponent<DuyenNhanVatItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = vector;
				vector += vector2;
				component.setDuyenData(duyenPhan[i], isActiveDuyen, isLock);
			}
		}
	}

	private void Set(UserInfo.HeroData data)
	{
		mHeroData = data;
		nhanvatAvatar.Set(data);
		NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[data.Name];
		nhanVatName.text = nhanVatCfg.TenHienThi;
		if (nhanVatCfg.Hang == 3)
		{
			spPham.spriteName = "giap";
		}
		else if (nhanVatCfg.Hang == 2)
		{
			spPham.spriteName = "at";
		}
		else if (nhanVatCfg.Hang == 1)
		{
			spPham.spriteName = "binh";
		}
		List<NhanVatCfg.DuyenPhanCfg> duyenPhan = nhanVatCfg.DuyenPhan;
		BattleGamerInfo battleGamerData = BattleGamerInfo.GetBattleGamerData(RefUserInfo);
		if (data.HID > 0)
		{
			if (RefUserInfo.DoiHinh.ListRaTran.Contains(data.HID))
			{
				ChiSoCoSo chiSoRaTran = BattleChiSoHero.GetChiSoRaTran(data.HID, battleGamerData);
				MenhLabel.text = Mathf.RoundToInt(chiSoRaTran.Menh).ToString();
				NgoaiLabel.text = Mathf.RoundToInt(chiSoRaTran.Ngoai).ToString();
				ThanPhapLabel.text = Mathf.RoundToInt(chiSoRaTran.ThanPhap).ToString();
				KhiLabel.text = Mathf.RoundToInt(chiSoRaTran.Noi).ToString();
			}
			else
			{
				ChiSoCoSo chiSoBatQuaiTran = BattleChiSoHero.GetChiSoBatQuaiTran(data.HID, battleGamerData);
				MenhLabel.text = Mathf.RoundToInt(chiSoBatQuaiTran.Menh).ToString();
				NgoaiLabel.text = Mathf.RoundToInt(chiSoBatQuaiTran.Ngoai).ToString();
				ThanPhapLabel.text = Mathf.RoundToInt(chiSoBatQuaiTran.ThanPhap).ToString();
				KhiLabel.text = Mathf.RoundToInt(chiSoBatQuaiTran.Noi).ToString();
			}
		}
		else if (data.ChiSoGoc != null)
		{
			MenhLabel.text = data.ChiSoGoc.Menh.ToString();
			NgoaiLabel.text = data.ChiSoGoc.Ngoai.ToString();
			ThanPhapLabel.text = data.ChiSoGoc.ThanPhap.ToString();
			KhiLabel.text = data.ChiSoGoc.Noi.ToString();
		}
		else
		{
			MenhLabel.text = nhanVatCfg.Menh.ToString();
			NgoaiLabel.text = nhanVatCfg.Ngoai.ToString();
			ThanPhapLabel.text = nhanVatCfg.ThanPhap.ToString();
			KhiLabel.text = nhanVatCfg.Noi.ToString();
		}
		if (duyenPhan == null || duyenPhan.Count <= 0)
		{
			return;
		}
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 200f, -3f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, -180f, 0f);
		bool isChuyenSinh = data.ChuyenSinh > 0;
		for (int i = 0; i < duyenPhan.Count; i++)
		{
			if (duyenPhan[i] != null)
			{
				bool flag = BattleChiSoHero.CheckActiveDuyen(data.HID, battleGamerData, duyenPhan[i], i + 1);
				bool isLock = false;
				if (data.BeQuan == 0 && i == 5 && !flag)
				{
					isLock = true;
				}
				DuyenNhanVatItem component = ((GameObject)Object.Instantiate(duyenNVPrefab)).GetComponent<DuyenNhanVatItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = vector;
				vector += vector2;
				component.setDuyenData(duyenPhan[i], flag, isLock, isChuyenSinh);
			}
		}
	}
}
