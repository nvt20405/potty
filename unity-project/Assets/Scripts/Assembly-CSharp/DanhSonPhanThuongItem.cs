using UnityEngine;

public class DanhSonPhanThuongItem : MonoBehaviour
{
	public UISprite back;

	public UISprite front;

	public OtherAvatar avatar;

	private bool sang = true;

	private bool havePt = true;

	private DanhSonCfg.PhanThuong PhanThuong;

	private void Awake()
	{
		avatar.OnEventClick = OnAvatarClick;
	}

	private void OnAvatarClick(OtherAvatar ava)
	{
		if (PhanThuong != null)
		{
			PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
			PhanThuongResponse.PhanThuong phanThuong = new PhanThuongResponse.PhanThuong();
			phanThuong.Name = PhanThuongResponse.GetPhanThuongCodeName(PhanThuong.CodeName);
			phanThuong.Loai = PhanThuongResponse.GetLoaiPhanThuongFromCode(PhanThuong.CodeName);
			phanThuong.Count = PhanThuong.Count;
			phanThuong.Level = PhanThuong.Level;
			phanThuongResponse.PhanThuongList.Add(phanThuong);
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongDanhSonPreviewTitle"), Localization.instance.Get("PhanThuongDanhSonPreviewDesc"), phanThuongResponse);
		}
	}

	public void SetPhanThuongOff()
	{
		SetSangToi(false);
		havePt = false;
		avatar.gameObject.SetActive(false);
	}

	public void SetInfo(DanhSonCfg.PhanThuong pt, bool isSang, bool havePhanThuong)
	{
		if (havePhanThuong)
		{
			PhanThuong = pt;
			avatar.gameObject.SetActive(true);
			if (pt.CodeName == "BAC")
			{
				avatar.SetBac(pt.Count);
			}
			else if (pt.CodeName == "VANG")
			{
				avatar.SetVang(pt.Count);
			}
			else
			{
				avatar.Set(pt.CodeName, 0, -1, pt.Count);
			}
		}
		else
		{
			avatar.gameObject.SetActive(false);
		}
		SetSangToi(isSang);
		havePt = havePhanThuong;
	}

	public void SetSangToi(bool isSang)
	{
		if (sang != isSang)
		{
			front.color = ((!isSang) ? Color.gray : Color.white);
			sang = isSang;
		}
	}
}
