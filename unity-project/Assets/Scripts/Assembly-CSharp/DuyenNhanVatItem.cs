using UnityEngine;

public class DuyenNhanVatItem : MonoBehaviour
{
	public UILabel lbDuyenInfo;

	public GameObject DuyenItemRoot;

	public GameObject nhanvatAvatar;

	public GameObject otherAvatar;

	private NhanVatCfg.DuyenPhanCfg duyenData;

	public UISprite spLine;

	public void setDuyenData(NhanVatCfg.DuyenPhanCfg duyenCfg, bool isActiveDuyen, bool isLock, bool isChuyenSinh = false)
	{
		if (duyenCfg == null)
		{
			return;
		}
		duyenData = duyenCfg;
		string text = string.Empty;
		if (isLock)
		{
			text += "[515151]";
		}
		if (isActiveDuyen)
		{
			text += "[FFEE00]";
		}
		text = text + duyenCfg.TenHienThi + ": ";
		if (duyenCfg.LoaiDuyenPhan == LoaiDuyenPhan.CungDoi)
		{
			text = text + Localization.instance.Get("CungDoiDuyenPhanLabel") + " ";
			if (duyenCfg.DoiTuong != null && duyenCfg.DoiTuong.Count > 0)
			{
				for (int i = 0; i < duyenCfg.DoiTuong.Count; i++)
				{
					NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[duyenCfg.DoiTuong[i]];
					if (nhanVatCfg != null)
					{
						text = ((i == duyenCfg.DoiTuong.Count - 1) ? ((isActiveDuyen | isLock) ? (text + nhanVatCfg.TenHienThi + " ") : (text + "[FF0000]" + nhanVatCfg.TenHienThi + "[-] ")) : ((isActiveDuyen | isLock) ? (text + nhanVatCfg.TenHienThi + ", ") : (text + "[FF0000]" + nhanVatCfg.TenHienThi + "[-], ")));
					}
				}
			}
		}
		else if (duyenCfg.LoaiDuyenPhan == LoaiDuyenPhan.TrangBiDo)
		{
			text = text + Localization.instance.Get("TrangBiDoDuyenPhanLabel") + " ";
			if (duyenCfg.DoiTuong != null && duyenCfg.DoiTuong.Count > 0)
			{
				for (int j = 0; j < duyenCfg.DoiTuong.Count; j++)
				{
					TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[duyenCfg.DoiTuong[j]];
					if (trangBiCfg != null)
					{
						if (j == duyenCfg.DoiTuong.Count - 1)
						{
							text = ((isActiveDuyen | isLock) ? (text + trangBiCfg.TenHienThi + " ") : (text + "[FF0000]" + trangBiCfg.TenHienThi + "[-] "));
						}
						else if (!isActiveDuyen && !isLock)
						{
							string text2 = text;
							text = text2 + "[FF0000]" + trangBiCfg.TenHienThi + " [-]" + Localization.instance.Get("OrMess") + "[-] ";
						}
						else
						{
							string text3 = text;
							text = text3 + trangBiCfg.TenHienThi + " " + Localization.instance.Get("OrMess") + " ";
						}
					}
				}
			}
		}
		string text4 = "[FF0000]";
		float num = 1f;
		if (isChuyenSinh)
		{
			num = (float)ConfigManager.instance.OtherConfig.ChuyenSinhConfig.HeSoDuyen / 100f;
			text4 = "[21DD49]";
		}
		if (!isActiveDuyen && !isLock)
		{
			string text5 = text;
			text = text5 + Localization.instance.Get("DuocNhanMess") + " + " + text4 + num * duyenCfg.HeSo + "% [-]";
		}
		else if (isActiveDuyen & isChuyenSinh)
		{
			string text6 = text;
			text = text6 + Localization.instance.Get("DuocNhanMess") + " + " + text4 + num * duyenCfg.HeSo + "% [-]";
		}
		else
		{
			string text7 = text;
			text = text7 + Localization.instance.Get("DuocNhanMess") + " + " + num * duyenCfg.HeSo + "% ";
		}
		switch (duyenCfg.ChiSoDuyen)
		{
		case ChiSoDuyenPhan.Khi:
			text += Localization.instance.Get("NoiTitle");
			break;
		case ChiSoDuyenPhan.Menh:
			text += Localization.instance.Get("MenhTitle");
			break;
		case ChiSoDuyenPhan.Ngoai:
			text += Localization.instance.Get("NgoaiTitle");
			break;
		case ChiSoDuyenPhan.ThanPhap:
			text += Localization.instance.Get("ThanPhapTitle");
			break;
		case ChiSoDuyenPhan.DoDon:
			text += Localization.instance.Get("DoDonTitle");
			break;
		case ChiSoDuyenPhan.Ne:
			text += Localization.instance.Get("NeTitle");
			break;
		case ChiSoDuyenPhan.Bao:
			text += Localization.instance.Get("BaoTitle");
			break;
		}
		lbDuyenInfo.text = text + ".";
		displayDuyenAvatar();
	}

	private void displayDuyenAvatar()
	{
		foreach (Transform item in DuyenItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		DuyenItemRoot.transform.localPosition = new Vector3(0f, 0f - (lbDuyenInfo.relativeSize.y * lbDuyenInfo.transform.localScale.y + 50f), 0f);
		spLine.transform.localPosition = new Vector3(0f, DuyenItemRoot.transform.localPosition.y - 55f, 0f);
		if (duyenData.DoiTuong == null || duyenData.DoiTuong.Count <= 0)
		{
			return;
		}
		int num = 135;
		for (int i = 0; i < duyenData.DoiTuong.Count; i++)
		{
			if (duyenData.LoaiDuyenPhan == LoaiDuyenPhan.TrangBiDo)
			{
				TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[duyenData.DoiTuong[i]];
				if (trangBiCfg == null)
				{
					continue;
				}
				OtherAvatar component = ((GameObject)Object.Instantiate(otherAvatar)).GetComponent<OtherAvatar>();
				component.transform.parent = DuyenItemRoot.transform;
				component.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
				component.gameObject.AddComponent<UIDragPanelContents>();
				component.Set(trangBiCfg.Name);
				Utils.SetLayer(component.transform, "GUIPopUp", true);
				if (duyenData.DoiTuong.Count % 2 == 0)
				{
					if (i % 2 == 0)
					{
						component.transform.localPosition = new Vector3(-(i + 1) * (num / 2), 0f, -5f);
					}
					else
					{
						component.transform.localPosition = new Vector3(i * (num / 2), 0f, -5f);
					}
				}
				else if (i % 2 == 0)
				{
					if (i == 0)
					{
						component.transform.localPosition = new Vector3(0f, 0f, -5f);
					}
					else
					{
						component.transform.localPosition = new Vector3((i - 1) * num, 0f, -5f);
					}
				}
				else
				{
					component.transform.localPosition = new Vector3(-i * num, 0f, -5f);
				}
			}
			else
			{
				if (duyenData.LoaiDuyenPhan != LoaiDuyenPhan.CungDoi)
				{
					continue;
				}
				NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[duyenData.DoiTuong[i]];
				if (nhanVatCfg == null)
				{
					continue;
				}
				NhanVatAvatar component2 = ((GameObject)Object.Instantiate(nhanvatAvatar)).GetComponent<NhanVatAvatar>();
				component2.transform.parent = DuyenItemRoot.transform;
				component2.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
				component2.gameObject.AddComponent<UIDragPanelContents>();
				component2.Set(nhanVatCfg.Name);
				Utils.SetLayer(component2.transform, "GUIPopUp", true);
				if (duyenData.DoiTuong.Count % 2 == 0)
				{
					if (i % 2 == 0)
					{
						component2.transform.localPosition = new Vector3(-(i + 1) * (num / 2), 0f, -5f);
					}
					else
					{
						component2.transform.localPosition = new Vector3(i * (num / 2), 0f, -5f);
					}
				}
				else if (i % 2 == 0)
				{
					if (i == 0)
					{
						component2.transform.localPosition = new Vector3(0f, 0f, -5f);
					}
					else
					{
						component2.transform.localPosition = new Vector3((i - 1) * num, 0f, -5f);
					}
				}
				else
				{
					component2.transform.localPosition = new Vector3(-i * num, 0f, -5f);
				}
			}
		}
	}
}
