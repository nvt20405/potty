using System.Collections.Generic;
using UnityEngine;

public class PopupDangNhapHangNgay : MonoBehaviour
{
	public static PopupDangNhapHangNgay instance;

	public GameObject group1Day;

	public GameObject group2Day;

	public GameObject group3Day;

	public UIButton btnQuay1;

	public UIButton btnQuay2;

	public UIButton btnQuay3;

	public UILabel lbResult1;

	public UILabel lbResult2;

	public UILabel lbResult3;

	public GameObject mAnimSpining1Day;

	public GameObject mAnimSpining2Day;

	public GameObject mAnimSpining3Day;

	public GameObject mAnimResult1Day;

	public GameObject mAnimResult2Day;

	public GameObject mAnimResult3Day;

	public List<PopUpDangNhapSlotMachineItem> listItem;

	private void Start()
	{
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public void Update()
	{
	}

	public static void Create()
	{
		DestroyPopup();
		if (!(TutorialPopup.instance != null))
		{
			instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupDangNhapHangNgay"))).GetComponent<PopupDangNhapHangNgay>();
			PopupManager.instance.Add(instance.gameObject);
			instance.transform.localScale = new Vector3(1f, 1f, 1f);
			instance.setView();
		}
	}

	public void setView()
	{
		for (int i = 0; i < listItem.Count; i++)
		{
			if (i == 0 && listItem[i] != null)
			{
				listItem[i].OnFinishPlay = onStopAnim1;
			}
			else if (i == 1 && listItem[i] != null)
			{
				listItem[i].OnFinishPlay = onStopAnim2;
			}
			else if (i == 2 && listItem[i] != null)
			{
				listItem[i].OnFinishPlay = onStopAnim3;
			}
		}
		mAnimSpining1Day.SetActive(false);
		mAnimSpining2Day.SetActive(false);
		mAnimSpining3Day.SetActive(false);
		mAnimResult1Day.SetActive(false);
		mAnimResult2Day.SetActive(false);
		mAnimResult3Day.SetActive(false);
		btnQuay1.isEnabled = false;
		btnQuay2.isEnabled = false;
		btnQuay3.isEnabled = false;
		int quayXoSo = GameManager.instance.m_GameClient.UserInfo.Gamer.QuayXoSo;
		if (quayXoSo >= 1)
		{
			btnQuay1.isEnabled = true;
			if (quayXoSo >= 2)
			{
				btnQuay2.isEnabled = true;
				if (quayXoSo >= 3)
				{
					btnQuay3.isEnabled = true;
				}
			}
		}
		checkLuotDaQuay();
	}

	public static bool checkConLuotQuay()
	{
		int quayXoSo = GameManager.instance.m_GameClient.UserInfo.Gamer.QuayXoSo;
		if (quayXoSo >= 1 && !GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("QuayXoSo0;"))
		{
			return true;
		}
		if (quayXoSo >= 2 && (!GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("QuayXoSo0;") || !GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("QuayXoSo1;")))
		{
			return true;
		}
		if (quayXoSo >= 3 && (!GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("QuayXoSo0;") || !GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("QuayXoSo1;") || !GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("QuayXoSo2;")))
		{
			return true;
		}
		return false;
	}

	public void checkLuotDaQuay()
	{
		int quayXoSo = GameManager.instance.m_GameClient.UserInfo.Gamer.QuayXoSo;
		string value = string.Format("QuayXoSo{0};", 0);
		if (!GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(value) && quayXoSo >= 1)
		{
			btnQuay1.isEnabled = true;
		}
		else
		{
			btnQuay1.isEnabled = false;
		}
		string value2 = string.Format("QuayXoSo{0};", 1);
		if (!GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(value2) && quayXoSo >= 2)
		{
			btnQuay2.isEnabled = true;
		}
		else
		{
			btnQuay2.isEnabled = false;
		}
		string value3 = string.Format("QuayXoSo{0};", 2);
		if (!GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(value3) && quayXoSo >= 3)
		{
			btnQuay3.isEnabled = true;
		}
		else
		{
			btnQuay3.isEnabled = false;
		}
	}

	public void getDangNhapHangNgayData(int sttNum)
	{
		DangNhapQuayXoSoRequest dangNhapQuayXoSoRequest = new DangNhapQuayXoSoRequest();
		dangNhapQuayXoSoRequest.Num = sttNum;
		GameManager.instance.m_GameClient.RequestDangNhapQuayXoSo(dangNhapQuayXoSoRequest);
	}

	public void startQuay(DangNhapQuayXoSoResponse response)
	{
		List<string> visualList = response.VisualList;
		PhanThuongResponse phanThuong = response.PhanThuong;
		if (visualList != null && phanThuong != null)
		{
			if (response.Num == 0)
			{
				listItem[0].Play(visualList, phanThuong);
				startPlayAnim(mAnimSpining1Day);
			}
			else if (response.Num == 1)
			{
				listItem[1].Play(visualList, phanThuong);
				startPlayAnim(mAnimSpining2Day);
			}
			else if (response.Num == 2)
			{
				listItem[2].Play(visualList, phanThuong);
				startPlayAnim(mAnimSpining3Day);
			}
		}
		checkLuotDaQuay();
	}

	public void btnQuay1_OnClick()
	{
		getDangNhapHangNgayData(0);
	}

	public void btnQuay2_OnClick()
	{
		getDangNhapHangNgayData(1);
	}

	public void btnQuay3_OnClick()
	{
		getDangNhapHangNgayData(2);
	}

	private void onStopAnim1(PhanThuongResponse phanThuong)
	{
		if (phanThuong != null && phanThuong.PhanThuongList[0] != null)
		{
			lbResult1.text = getNamePhanThuong(phanThuong.PhanThuongList[0]) + " x " + phanThuong.PhanThuongList[0].Count;
		}
		stopPlayAnim(mAnimSpining1Day);
		startPlayAnim(mAnimResult1Day);
	}

	private void onStopAnim2(PhanThuongResponse phanThuong)
	{
		if (phanThuong != null && phanThuong.PhanThuongList[0] != null)
		{
			lbResult2.text = getNamePhanThuong(phanThuong.PhanThuongList[0]) + " x " + phanThuong.PhanThuongList[0].Count;
		}
		stopPlayAnim(mAnimSpining2Day);
		startPlayAnim(mAnimResult2Day);
	}

	private void onStopAnim3(PhanThuongResponse phanThuong)
	{
		if (phanThuong != null && phanThuong.PhanThuongList[0] != null)
		{
			lbResult3.text = getNamePhanThuong(phanThuong.PhanThuongList[0]) + " x " + phanThuong.PhanThuongList[0].Count;
		}
		stopPlayAnim(mAnimSpining3Day);
		startPlayAnim(mAnimResult3Day);
	}

	private void startPlayAnim(GameObject m_anim)
	{
		m_anim.SetActive(true);
		m_anim.GetComponent<ParticleSystem>().Simulate(0f, true, true);
		m_anim.GetComponent<ParticleSystem>().Play();
	}

	private void stopPlayAnim(GameObject m_anim)
	{
		m_anim.SetActive(false);
		m_anim.GetComponent<ParticleSystem>().Stop();
	}

	private string getNamePhanThuong(PhanThuongResponse.PhanThuong phanThuong)
	{
		string result = string.Empty;
		if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.VANG)
		{
			result = Localization.instance.Get("VangLabel");
		}
		else if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.BAC)
		{
			result = Localization.instance.Get("BacLabel");
		}
		else if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.TRANG_BI)
		{
			if (ConfigManager.instance.m_dicTrangBi.ContainsKey(phanThuong.Name))
			{
				result = ConfigManager.instance.m_dicTrangBi[phanThuong.Name].TenHienThi;
			}
		}
		else if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.VO_CONG)
		{
			if (ConfigManager.instance.m_dicVCs.ContainsKey(phanThuong.Name))
			{
				result = ConfigManager.instance.m_dicVCs[phanThuong.Name].TenHienThi;
			}
		}
		else if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU)
		{
			if (ConfigManager.instance.m_dicVatPhamTieuThu.ContainsKey(phanThuong.Name))
			{
				result = ConfigManager.instance.m_dicVatPhamTieuThu[phanThuong.Name].TenHienThi;
			}
		}
		else if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.MANH_VO_CONG)
		{
			if (ConfigManager.instance.m_dicVCs.ContainsKey(phanThuong.Name))
			{
				result = ConfigManager.instance.m_dicVCs[phanThuong.Name].TenHienThi;
			}
		}
		else if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.MANH_TRANG_BI && ConfigManager.instance.m_dicTrangBi.ContainsKey(phanThuong.Name))
		{
			result = ConfigManager.instance.m_dicTrangBi[phanThuong.Name].TenHienThi;
		}
		return result;
	}

	public void OnCloseClick()
	{
		DestroyPopup();
	}
}
