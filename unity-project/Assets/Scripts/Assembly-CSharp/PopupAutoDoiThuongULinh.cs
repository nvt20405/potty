using System;
using UnityEngine;

public class PopupAutoDoiThuongULinh : MonoBehaviour
{
	public static PopupAutoDoiThuongULinh instance;

	public UICheckbox cb;

	private ULinhInfoResponse response;

	private bool autoEnable;

	private float nextSecond = 1f;

	public static PopupAutoDoiThuongULinh Create(ULinhInfoResponse response)
	{
		DestroyPopup();
		UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("popup/PopupAutoDoiThuongULinh"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		instance = gameObject.GetComponent<PopupAutoDoiThuongULinh>();
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		instance.response = response;
		return instance;
	}

	public void OnDisableScreen()
	{
		cb.gameObject.SetActive(false);
	}

	public void OnEnableScreen()
	{
		cb.gameObject.SetActive(true);
	}

	private void OnActivateAuto(bool isActive)
	{
		autoEnable = isActive;
		if (autoEnable)
		{
			EGDebug.Log("Enable Auto U Linh");
		}
		else
		{
			EGDebug.Log("Disable Auto U Linh");
		}
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			UnityEngine.Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public void callRequestDoiThuong(int index, int ID)
	{
		DoiItemULinhRequest doiItemULinhRequest = new DoiItemULinhRequest();
		doiItemULinhRequest.SlotIdx = index;
		if (ID > 0)
		{
			doiItemULinhRequest.VatPhamID = ID;
		}
		EGDebug.Log("RequestDoiItemULinh --- index: " + index + " - vat pham ID: " + ID);
		GameManager.instance.m_GameClient.RequestDoiItemULinh(doiItemULinhRequest);
	}

	public void SetInfo(ULinhInfoResponse response)
	{
		this.response = response;
	}

	private void Update()
	{
		nextSecond += Time.deltaTime;
		if (!(nextSecond >= 1f))
		{
			return;
		}
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		if (userInfo.ServerInfo != null && userInfo.ServerInfo.ULinhCfg != null && response != null && response.ListDoiDo != null && response.ListDoiDo.Count > 0)
		{
			DateTime thoiGianBatDau = userInfo.ServerInfo.ULinhCfg.ThoiGianBatDau;
			DateTime thoiGianKetThuc = userInfo.ServerInfo.ULinhCfg.ThoiGianKetThuc;
			DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
			if (autoEnable && serverTime > thoiGianBatDau && serverTime < thoiGianKetThuc)
			{
				int num = 0;
				foreach (ULinhInfoResponse.ULinhDoiDoItem item in response.ListDoiDo)
				{
					if (item.Diem > 0)
					{
						PhanThuongResponse.PhanThuong vpDoi = item.VatPhamDoi;
						if (vpDoi.Loai == PhanThuongResponse.LoaiPhanThuong.BAC)
						{
							if (vpDoi.Count <= userInfo.Gamer.Bac)
							{
								callRequestDoiThuong(num, 0);
							}
						}
						else if (vpDoi.Loai == PhanThuongResponse.LoaiPhanThuong.HON_NHAN_VAT)
						{
							UserInfo.HonNhanVatData honNhanVatData = GameManager.instance.m_GameClient.UserInfo.HonNhanVatList.Find((UserInfo.HonNhanVatData e) => e.Name == vpDoi.Name);
							if (honNhanVatData != null && honNhanVatData.Quantity >= vpDoi.Count)
							{
								callRequestDoiThuong(num, honNhanVatData.ID);
							}
						}
						else if (vpDoi.Loai == PhanThuongResponse.LoaiPhanThuong.MANH_TRANG_BI)
						{
							UserInfo.ManhTrangBiData manhTrangBiData = GameManager.instance.m_GameClient.UserInfo.ManhTrangBiList.Find((UserInfo.ManhTrangBiData e) => e.Name == vpDoi.Name);
							if (manhTrangBiData != null && manhTrangBiData.Quantity >= vpDoi.Count)
							{
								callRequestDoiThuong(num, manhTrangBiData.ID);
							}
						}
						else if (vpDoi.Loai == PhanThuongResponse.LoaiPhanThuong.MANH_VO_CONG)
						{
							UserInfo.ManhVoCongData manhVoCongData = GameManager.instance.m_GameClient.UserInfo.ManhVoCongList.Find((UserInfo.ManhVoCongData e) => e.Name == vpDoi.Name);
							if (manhVoCongData != null && manhVoCongData.Quantity >= vpDoi.Count)
							{
								callRequestDoiThuong(num, manhVoCongData.ID);
							}
						}
						else if (vpDoi.Loai == PhanThuongResponse.LoaiPhanThuong.TRANG_BI)
						{
							UserInfo.TrangBiData trangBiData = GameManager.instance.m_GameClient.UserInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.Name == vpDoi.Name && e.Level == 1 && e.HID <= 0);
							if (trangBiData != null)
							{
								callRequestDoiThuong(num, trangBiData.ID);
							}
						}
						else if (vpDoi.Loai == PhanThuongResponse.LoaiPhanThuong.VO_CONG)
						{
							UserInfo.VoCongData voCongData = GameManager.instance.m_GameClient.UserInfo.VoCongList.Find((UserInfo.VoCongData e) => e.Name == vpDoi.Name && e.Level == 1 && e.HID <= 0);
							if (voCongData != null)
							{
								callRequestDoiThuong(num, voCongData.ID);
							}
						}
						else if (vpDoi.Loai == PhanThuongResponse.LoaiPhanThuong.VANG)
						{
							if (userInfo.Gamer.Vang >= vpDoi.Count)
							{
								callRequestDoiThuong(num, 0);
							}
						}
						else if (vpDoi.Loai == PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU)
						{
							UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == vpDoi.Name && e.Quantity >= 1);
							if (vatPhamTieuThuData != null)
							{
								callRequestDoiThuong(num, vatPhamTieuThuData.ID);
							}
						}
					}
					num++;
				}
				if (autoEnable && response.ThoiGianReset < serverTime)
				{
					GameManager.instance.m_GameClient.RequestULinhInfo();
				}
			}
		}
		nextSecond = 0f;
	}
}
