using System;
using UnityEngine;

public class NhanVatAvatar : MonoBehaviour
{
	public UISprite avatar;

	public UISprite avatarBkg;

	public UISprite bkg;

	public UISprite lvlBkg;

	public UILabel lvlLabel;

	public UISprite countBkg;

	public UILabel countLabel;

	public UISprite tanHon;

	public Action<NhanVatAvatar> OnEventClick;

	public GameObject groupLevelDotPha;

	public GameObject goChuyenSinh;

	private int _currentQuantity;

	public string strCodeName;

	private bool _isSelected;

	public int currentQuantity
	{
		get
		{
			return _currentQuantity;
		}
		set
		{
			displayCount(value);
		}
	}

	public bool IsSelected
	{
		get
		{
			return _isSelected;
		}
		set
		{
			Select(value);
		}
	}

	public void displayCount(int value)
	{
		_currentQuantity = value;
		countBkg.gameObject.SetActive(true);
		countLabel.gameObject.SetActive(true);
		if (value < 0)
		{
			countLabel.text = string.Empty;
		}
		else
		{
			countLabel.text = value.ToString();
		}
	}

	public bool IsEmpty()
	{
		return avatar.spriteName == "empty";
	}

	public void OnAvatarClick()
	{
		EGDebug.Log("NhanVatAvatar click");
		if (OnEventClick != null)
		{
			OnEventClick(this);
		}
		else if (ConfigManager.instance.m_dicNhanVats.ContainsKey(strCodeName))
		{
			NhanVatCfg cfgData = ConfigManager.instance.m_dicNhanVats[strCodeName];
			PopupNhanVat.CreateByNhanVatAvatar(cfgData);
		}
	}

	public void Select(bool isSelected)
	{
		if (isSelected)
		{
			bkg.transform.localScale = new Vector3(120f, 120f, 1f);
		}
		else
		{
			bkg.transform.localScale = new Vector3(110f, 110f, 1f);
		}
		_isSelected = isSelected;
	}

	public void Set(string code, int dotPhaLvl = 0, int lvl = -1, bool isSelected = false, int count = -1, bool isTanHon = false, bool haveCostume = false, int chuyensinh = 0)
	{
		if (string.IsNullOrEmpty(code))
		{
			code = "empty";
		}
		if (code.StartsWith("TH_"))
		{
			if (tanHon != null)
			{
				tanHon.gameObject.SetActive(true);
			}
			code = "NV_" + code.Substring(3);
			EGDebug.Log("code after substring: " + code);
		}
		else if (tanHon != null)
		{
			tanHon.gameObject.SetActive(false);
		}
		if (isTanHon && tanHon != null)
		{
			tanHon.gameObject.SetActive(true);
		}
		strCodeName = code;
		avatar.spriteName = code;
		int num = 0;
		if (ConfigManager.instance.m_dicNhanVats.ContainsKey(code))
		{
			NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[code];
			num = nhanVatCfg.Hang;
			avatarBkg.spriteName = string.Format("hang{0}_bkgnho", num);
			if (haveCostume)
			{
				bkg.spriteName = string.Format("bkg_avatar{0}", 5);
			}
			else
			{
				bkg.spriteName = string.Format("bkg_avatar{0}", num);
			}
		}
		else if (code.StartsWith("PET_"))
		{
			num = int.Parse(code[code.Length - 1].ToString()) - 1;
			if (num == 4)
			{
				bkg.spriteName = string.Format("bkg_avatar{0}", 5);
			}
			else
			{
				bkg.spriteName = string.Format("bkg_avatar{0}", num);
			}
		}
		else if (code.StartsWith("DH_"))
		{
			bkg.spriteName = "nen_danh_hieu";
			bkg.MakePixelPerfect();
		}
		if (lvlBkg != null)
		{
			lvlBkg.gameObject.SetActive(lvl > 0);
		}
		if (lvlLabel != null)
		{
			lvlLabel.gameObject.SetActive(lvl > 0);
		}
		if (lvl > 0 && lvlBkg != null && lvlLabel != null)
		{
			if (haveCostume)
			{
				lvlBkg.spriteName = string.Format("hang{0}_tl{1}_lvl_bkg", 4, 1);
			}
			else
			{
				lvlBkg.spriteName = string.Format("hang{0}_tl{1}_lvl_bkg", num, 1);
			}
			lvlBkg.transform.localScale = new Vector3(60f, 60f, 60f);
			lvlBkg.transform.localPosition = new Vector3(-55f, 0f, lvlBkg.transform.localPosition.z);
			lvlLabel.text = lvl.ToString();
			lvlLabel.transform.localPosition = new Vector3(-70f, 1f, lvlLabel.transform.localPosition.z);
		}
		if (countBkg != null)
		{
			countBkg.gameObject.SetActive(count >= 0);
		}
		if (countLabel != null)
		{
			countLabel.gameObject.SetActive(count >= 0);
		}
		if (count >= 0 && countBkg != null && countLabel != null)
		{
			countLabel.text = count.ToString();
			float num2 = countLabel.relativeSize.x * countLabel.transform.localScale.x + 10f;
			countBkg.transform.localScale = new Vector3((!(num2 < 24f)) ? num2 : 24f, countBkg.transform.localScale.y, 1f);
		}
		if (groupLevelDotPha != null)
		{
			foreach (Transform item in groupLevelDotPha.transform)
			{
				Transform transform2 = item;
				UnityEngine.Object.Destroy(transform2.gameObject);
			}
		}
		else
		{
			groupLevelDotPha = new GameObject();
			groupLevelDotPha.transform.parent = base.transform;
			groupLevelDotPha.transform.localPosition = new Vector3(0f, -35f, 0f);
			groupLevelDotPha.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		if (dotPhaLvl > 0 && dotPhaLvl < 4)
		{
			int num3 = 30;
			int num4 = (dotPhaLvl - 1) * num3;
			for (int i = 0; i < dotPhaLvl; i++)
			{
				GameObject gameObject = new GameObject("star" + i);
				UISprite uISprite = gameObject.AddComponent<UISprite>();
				uISprite.atlas = GUIManager.instance.nhanvatAtlas;
				uISprite.spriteName = "sao";
				uISprite.depth = 10;
				uISprite.transform.parent = groupLevelDotPha.transform;
				uISprite.transform.localScale = new Vector3(22f, 22f, 1f);
				uISprite.transform.localPosition = new Vector3(i * num3 - num4 / 2, 0f, 0f);
			}
		}
		if (goChuyenSinh != null)
		{
			foreach (Transform item2 in goChuyenSinh.transform)
			{
				Transform transform4 = item2;
				UnityEngine.Object.Destroy(transform4.gameObject);
			}
		}
		else
		{
			goChuyenSinh = new GameObject("chuyensinh");
			goChuyenSinh.transform.parent = base.transform;
			goChuyenSinh.transform.localPosition = new Vector3(0f, 0f, 0f);
			goChuyenSinh.transform.localScale = base.transform.localScale;
		}
		if (chuyensinh > 0)
		{
			GameObject gameObject2 = null;
			gameObject2 = ((!(base.transform.localScale.x < 1f)) ? (UnityEngine.Object.Instantiate(Resources.Load("FX/Prefabs/GUI_CHUYENSINHAVATAR")) as GameObject) : (UnityEngine.Object.Instantiate(Resources.Load("FX/Prefabs/GUI_CHUYENSINHAVATAR_SMALL")) as GameObject));
			if (gameObject2 != null)
			{
				gameObject2.transform.parent = goChuyenSinh.transform;
				gameObject2.transform.position = goChuyenSinh.transform.position;
				gameObject2.transform.localScale = base.transform.localScale;
				gameObject2.transform.localRotation = Quaternion.identity;
			}
		}
		IsSelected = isSelected;
	}

	public void Set(UserInfo.HeroData data, bool isSelected = false)
	{
		if (data != null)
		{
			Set(data.Name, data.CapDotPha, data.Level, isSelected, -1, false, data.CostumeID > 0, data.ChuyenSinh);
		}
		else
		{
			Set("empty");
		}
	}

	public void Set(UserInfo.HonNhanVatData data, bool isSelected = false, int count = -1)
	{
		string text = data.Name;
		EGDebug.Log("SET CODE NAME TAN HON 1: " + text);
		if (!text.StartsWith("TH_"))
		{
			text = text.Substring(3);
			text = "TH_" + text;
		}
		EGDebug.Log("SET CODE NAME TAN HON 2: " + text);
		Set(text, 0, -1, isSelected, count);
	}
}
