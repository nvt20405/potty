using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCamCungBiBao : ScreenBase
{
	public List<PhanThuongItem> listPhanThuong = new List<PhanThuongItem>();

	public UISprite spFocusItem;

	private bool Spinning;

	public OtherAvatar PhiThienLenhAva;

	public UICheckbox checkBox1;

	public UICheckbox checkBox5;

	public UICheckbox checkBox25;

	public QuayCamCungBiBaoResquest request = new QuayCamCungBiBaoResquest();

	private QuayCamCungBiBaoResponse CamCungResponse;

	private QuayCamCungType curType;

	private int oldIndex = -1;

	private UserInfo.ServerData.EventCamCungBiBao cfgCamCung;

	private void Start()
	{
		displayInfo();
	}

	public void displayInfo()
	{
		if (listPhanThuong.Count > 0)
		{
			for (int i = 0; i < listPhanThuong.Count; i++)
			{
				listPhanThuong[i].gameObject.SetActive(false);
			}
		}
		updateVatPhamCan();
		checkBox1.isChecked = true;
		checkBox5.isChecked = false;
		checkBox25.isChecked = false;
		cfgCamCung = GameManager.instance.m_GameClient.UserInfo.ServerInfo.CamCungBiBaoConfig;
		if (cfgCamCung == null || cfgCamCung.ListPhanThuong == null || cfgCamCung.ListPhanThuong.Count == 0)
		{
			cfgCamCung = new UserInfo.ServerData.EventCamCungBiBao();
			cfgCamCung.ListPhanThuong = new List<UserInfo.ServerData.PhanThuongCamCungBiBao>();
			string[] array = new string[8] { "VANG", "BAC", "VP_BOI_DUONG_DAN", "VP_TAY_TUY_DAN", "VP_GA_QUAY", "VP_HOP_VANG", "VP_LUAN_KIEM_LENH", "VANG" };
			int[] array2 = new int[8] { 100, 500000, 50, 10, 5, 2, 3, 300 };
			PhanThuongResponse.LoaiPhanThuong[] array3 = new PhanThuongResponse.LoaiPhanThuong[8]
			{
				PhanThuongResponse.LoaiPhanThuong.VANG,
				PhanThuongResponse.LoaiPhanThuong.BAC,
				PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU,
				PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU,
				PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU,
				PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU,
				PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU,
				PhanThuongResponse.LoaiPhanThuong.VANG
			};
			for (int j = 0; j < 8; j++)
			{
				UserInfo.ServerData.PhanThuongCamCungBiBao phanThuongCamCungBiBao = new UserInfo.ServerData.PhanThuongCamCungBiBao();
				phanThuongCamCungBiBao.PhanThuong = new PhanThuongResponse.PhanThuong
				{
					Name = array[j],
					Count = array2[j],
					Level = 1,
					Loai = array3[j]
				};
				cfgCamCung.ListPhanThuong.Add(phanThuongCamCungBiBao);
			}
		}
		for (int k = 0; k < listPhanThuong.Count; k++)
		{
			if (k < cfgCamCung.ListPhanThuong.Count && cfgCamCung.ListPhanThuong[k].PhanThuong != null)
			{
				if (cfgCamCung.ListPhanThuong[k].PhanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.BAC)
				{
					listPhanThuong[k].setBac(cfgCamCung.ListPhanThuong[k].PhanThuong.Count, true);
				}
				else if (cfgCamCung.ListPhanThuong[k].PhanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.VANG)
				{
					listPhanThuong[k].setVang(cfgCamCung.ListPhanThuong[k].PhanThuong.Count, true);
				}
				else
				{
					listPhanThuong[k].Set(cfgCamCung.ListPhanThuong[k].PhanThuong, false, true);
				}
				listPhanThuong[k].gameObject.SetActive(true);
			}
		}
	}

	private void updateVatPhamCan()
	{
		UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_PHI_THIEN_LENH");
		if (vatPhamTieuThuData != null)
		{
			PhiThienLenhAva.Set(vatPhamTieuThuData);
			return;
		}
		vatPhamTieuThuData = new UserInfo.VatPhamTieuThuData();
		vatPhamTieuThuData.Name = "VP_PHI_THIEN_LENH";
		vatPhamTieuThuData.Quantity = 0;
		PhiThienLenhAva.Set(vatPhamTieuThuData);
	}

	private IEnumerator Spin(int pos, PhanThuongResponse pt)
	{
		Spinning = true;
		int maxCount = ((cfgCamCung != null && cfgCamCung.ListPhanThuong != null && cfgCamCung.ListPhanThuong.Count > 0) ? cfgCamCung.ListPhanThuong.Count : listPhanThuong.Count);
		if (maxCount <= 0)
		{
			maxCount = 1;
		}
		pos = Mathf.Clamp(pos, 0, maxCount - 1);
		int count = 0;
		int round = UnityEngine.Random.Range(1, 3);
		for (int i = 1; i <= round * 8 + pos; i++)
		{
			int random = UnityEngine.Random.Range(0, maxCount);
			if (random < listPhanThuong.Count && listPhanThuong[random] != null)
			{
				spFocusItem.transform.position = listPhanThuong[random].transform.position;
			}
			count++;
			yield return new WaitForSeconds(0.1f);
		}
		if (pos < listPhanThuong.Count && listPhanThuong[pos] != null)
		{
			spFocusItem.transform.position = listPhanThuong[pos].transform.position;
		}
		yield return new WaitForSeconds(0.3f);
		try
		{
			if (CamCungResponse == null)
			{
				yield break;
			}
			PhanThuongResponse.PhanThuong ptNhanDuoc = null;
			if (cfgCamCung != null && cfgCamCung.ListPhanThuong != null && CamCungResponse.indexPT >= 0 && CamCungResponse.indexPT < cfgCamCung.ListPhanThuong.Count)
			{
				ptNhanDuoc = cfgCamCung.ListPhanThuong[CamCungResponse.indexPT].PhanThuong;
			}
			else if (pt != null && pt.PhanThuongList != null && pt.PhanThuongList.Count > 0)
			{
				ptNhanDuoc = pt.PhanThuongList[0];
			}
			if (CamCungResponse.isNhanThuongNow)
			{
				if (pt != null && pt.PhanThuongList != null && pt.PhanThuongList.Count > 0)
				{
					PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTitle"), Localization.instance.Get("PhanThuongNhanDuoc"), pt);
				}
				else if (ptNhanDuoc != null)
				{
					PopupCamCungPhanThuongDB.Create(ptNhanDuoc);
				}
				oldIndex = -1;
			}
			else if (oldIndex == -1 || CamCungResponse.indexPT == oldIndex)
			{
				if (ptNhanDuoc != null)
				{
					PopupCamCungBiBaoInfo.Create(ptNhanDuoc, curType, CamCungResponse.LuotQuay);
				}
			}
			else
			{
				oldIndex = -1;
				CamCungResponse = null;
				if (pt != null)
				{
					PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTitle"), Localization.instance.Get("PhanThuongCamCungThatBaiDesc"), pt);
				}
			}
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			EGDebug.LogError("[ScreenCamCungBiBao] Spin end error: " + ((ex2 != null) ? ex2.ToString() : null));
		}
		finally
		{
			Spinning = false;
		}
	}

	public void displayResponse(QuayCamCungBiBaoResponse response)
	{
		if (response != null)
		{
			if (oldIndex == -1)
			{
				oldIndex = response.indexPT;
			}
			CamCungResponse = response;
			updateVatPhamCan();
			StartCoroutine(Spin(response.indexPT, response.ptResponse));
		}
		else
		{
			Spinning = false;
		}
	}

	public void BtnQuayOnClick()
	{
		if (!Spinning)
		{
			GameManager.instance.m_GameClient.RequestQuayCamCungBiBao(request);
		}
	}

	public void checkBox1Selected()
	{
		if (checkBox1.isChecked)
		{
			curType = QuayCamCungType.USE_1_PHITHIENLENH;
			request.typeQuayCamCung = QuayCamCungType.USE_1_PHITHIENLENH;
		}
	}

	public void checkBox5Selected()
	{
		if (checkBox5.isChecked)
		{
			curType = QuayCamCungType.USE_5_PHITHIENLENH;
			request.typeQuayCamCung = QuayCamCungType.USE_5_PHITHIENLENH;
		}
	}

	public void checkBox25Selected()
	{
		if (checkBox25.isChecked)
		{
			curType = QuayCamCungType.USE_25_PHITHIENLENH;
			request.typeQuayCamCung = QuayCamCungType.USE_25_PHITHIENLENH;
		}
	}

	public void displayPhanthuong(NhanThuongCamCungResponse response)
	{
		oldIndex = -1;
		CamCungResponse = null;
		if (response.ptResponse != null)
		{
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTitle"), Localization.instance.Get("PhanThuongDesc"), response.ptResponse);
		}
	}
}
