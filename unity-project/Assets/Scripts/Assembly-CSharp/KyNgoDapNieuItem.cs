using UnityEngine;

public class KyNgoDapNieuItem : MonoBehaviour
{
	public UISprite bkg;

	public UISprite bgNieu;

	public PhanThuongItem phanThuong;

	public GameObject goPartile;

	public GameObject grpPhanThuong;

	public GameObject grpNieu;

	public bool isOpen;

	public void setData(PhanThuongResponse.PhanThuong pt)
	{
		if (pt != null)
		{
			if (pt.Loai == PhanThuongResponse.LoaiPhanThuong.BAC)
			{
				phanThuong.setBac(pt.Count, true);
			}
			else if (pt.Loai == PhanThuongResponse.LoaiPhanThuong.VANG)
			{
				phanThuong.setVang(pt.Count, true);
			}
			else
			{
				phanThuong.Set(pt, false);
			}
		}
		else
		{
			phanThuong.setNullValue();
		}
		grpPhanThuong.gameObject.SetActive(false);
		grpNieu.gameObject.SetActive(true);
		goPartile.gameObject.SetActive(false);
		bkg.spriteName = "dap_nieu_19";
		bgNieu.spriteName = "dap_nieu_05";
	}

	public void setEmptyData()
	{
		isOpen = false;
		phanThuong.setNullValue();
		grpPhanThuong.gameObject.SetActive(false);
		grpNieu.gameObject.SetActive(true);
		goPartile.gameObject.SetActive(false);
		bkg.spriteName = "dap_nieu_19";
		bgNieu.spriteName = "dap_nieu_05";
	}

	public void setNullValue()
	{
		isOpen = false;
		phanThuong.setNullValue();
		grpPhanThuong.gameObject.SetActive(false);
		grpNieu.gameObject.SetActive(true);
		goPartile.gameObject.SetActive(false);
		bkg.spriteName = "dap_nieu_19";
		bgNieu.spriteName = "dap_nieu_05";
	}

	public void Hit()
	{
		isOpen = true;
		grpPhanThuong.gameObject.SetActive(true);
		grpPhanThuong.transform.localPosition = new Vector3(0f, 0f, -1f);
		grpNieu.gameObject.SetActive(false);
		bkg.spriteName = "dap_nieu_21";
		startPlayAnim();
	}

	public void Open()
	{
		isOpen = true;
		grpPhanThuong.gameObject.SetActive(true);
		grpPhanThuong.transform.localPosition = new Vector3(0f, 0f, -1f);
		grpNieu.gameObject.SetActive(false);
		bkg.spriteName = "dap_nieu_21";
		isOpen = true;
	}

	public void notHit(PhanThuongResponse.PhanThuong pt)
	{
		if (pt != null)
		{
			if (pt.Loai == PhanThuongResponse.LoaiPhanThuong.BAC)
			{
				phanThuong.setBac(pt.Count, true);
			}
			else if (pt.Loai == PhanThuongResponse.LoaiPhanThuong.VANG)
			{
				phanThuong.setVang(pt.Count, true);
			}
			else
			{
				phanThuong.Set(pt, false);
			}
		}
		else
		{
			phanThuong.setNullValue();
		}
		grpPhanThuong.gameObject.SetActive(true);
		grpPhanThuong.transform.localPosition = new Vector3(0f, -10f, -1f);
		grpNieu.gameObject.SetActive(true);
		bgNieu.spriteName = "dap_nieu_03";
	}

	public void startPlayAnim()
	{
		goPartile.SetActive(true);
		goPartile.GetComponent<ParticleSystem>().Simulate(0f, true, true);
		goPartile.GetComponent<ParticleSystem>().Play();
	}
}
