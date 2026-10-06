using System;
using UnityEngine;

public class NguyenKhiAvatar : MonoBehaviour
{
	public GameObject mAnim;

	public UILabel lbLevel;

	public UISprite avatarBkg;

	public GameObject nkPhaQuan;

	public GameObject nkThamLang;

	public GameObject nkThatSat;

	public GameObject nkThienCo;

	public GameObject nkThienTuong;

	public GameObject nkVuKhuc;

	private bool _isLocked;

	public Action<NguyenKhiAvatar> OnEventClick;

	public string strCodeName = string.Empty;

	public bool IsLocked
	{
		get
		{
			return _isLocked;
		}
		set
		{
			Lock(value);
		}
	}

	public void Set(UserInfo.NguyenKhiData nkData)
	{
		if (nkData != null)
		{
			Set(nkData.Codename, nkData.Level);
		}
	}

	public void Set(string name, int level)
	{
		strCodeName = name;
		if (name.StartsWith("NK_"))
		{
			foreach (Transform item in mAnim.transform)
			{
				Transform transform2 = item;
				UnityEngine.Object.Destroy(transform2.gameObject);
			}
			GameObject gameObject = null;
			if (name == "NK_PHA_QUAN")
			{
				gameObject = (GameObject)UnityEngine.Object.Instantiate(nkPhaQuan);
			}
			if (name == "NK_THAM_LANG")
			{
				gameObject = (GameObject)UnityEngine.Object.Instantiate(nkThamLang);
			}
			if (name == "NK_THAT_SAT")
			{
				gameObject = (GameObject)UnityEngine.Object.Instantiate(nkThatSat);
			}
			if (name == "NK_THIEN_CO")
			{
				gameObject = (GameObject)UnityEngine.Object.Instantiate(nkThienCo);
			}
			if (name == "NK_THIEN_TUONG")
			{
				gameObject = (GameObject)UnityEngine.Object.Instantiate(nkThienTuong);
			}
			if (name == "NK_VU_KHUC")
			{
				gameObject = (GameObject)UnityEngine.Object.Instantiate(nkVuKhuc);
			}
			if (gameObject != null)
			{
				gameObject.transform.parent = mAnim.transform;
				gameObject.transform.localPosition = new Vector3(0f, -3f, 0f);
				gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				gameObject.GetComponent<ParticleSystem>().Play();
			}
		}
		if (level > 0 && lbLevel != null)
		{
			lbLevel.text = level.ToString();
		}
	}

	public void Lock(bool isLocked)
	{
		if (isLocked)
		{
			avatarBkg.spriteName = "nguyen_khi1";
			lbLevel.text = string.Empty;
		}
		else
		{
			avatarBkg.spriteName = "nguyen_khi2";
			lbLevel.text = string.Empty;
			foreach (Transform item in mAnim.transform)
			{
				Transform transform2 = item;
				UnityEngine.Object.Destroy(transform2.gameObject);
			}
		}
		_isLocked = isLocked;
	}

	public void OnAvatarClick()
	{
		EGDebug.Log("NhanVatAvatar click");
		if (OnEventClick != null)
		{
			OnEventClick(this);
		}
		else if (ConfigManager.instance.OtherConfig.NguyenKhiConfig.ContainsKey(strCodeName))
		{
			OtherCfg.NguyenKhiCfg nkConfig = ConfigManager.instance.OtherConfig.NguyenKhiConfig[strCodeName];
			PopUpNguyenKhi.CreateByConfig(nkConfig);
		}
	}
}
