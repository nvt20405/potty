using System;
using UnityEngine;

public class DiHoaCungItem : MonoBehaviour
{
	public UISprite spBkg;

	public UISprite spBkgDe;

	public UIButton btnHomQua;

	public GameObject animNhanThuong;

	private UserInfo.ServerData.DiHoaCungTang mDiHoaCungTangData;

	private bool isNhanThuong;

	private bool isDuDieuKien;

	private int indexTang = -1;

	private UserInfo.ServerData.DiHoaCungCfg cfgDiHoaCung;

	public void setNocDiHoaCung()
	{
		mDiHoaCungTangData = null;
		displayNocDiHoaCung();
	}

	public void setData(UserInfo.ServerData.DiHoaCungTang tangData, int tongSoGachDaGop, int index, bool isTang1 = false)
	{
		cfgDiHoaCung = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DiHoaCungConfig;
		if (tangData == null || cfgDiHoaCung == null)
		{
			return;
		}
		mDiHoaCungTangData = tangData;
		indexTang = index;
		if (isTang1)
		{
			spBkgDe.gameObject.SetActive(true);
		}
		else
		{
			spBkgDe.gameObject.SetActive(false);
			if (cfgDiHoaCung != null)
			{
				double num = (double)spBkg.transform.localScale.x * Math.Pow(0.9, cfgDiHoaCung.ListTang.Count - indexTang - 1);
				spBkg.transform.localScale = new Vector3((float)num, spBkg.transform.localScale.y, spBkg.transform.localScale.z);
			}
		}
		displayTangDiHoaCung(tongSoGachDaGop);
	}

	private void displayTangDiHoaCung(int soGachDaGop)
	{
		UserInfo.ServerData.DiHoaCungCfg diHoaCungConfig = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DiHoaCungConfig;
		if (diHoaCungConfig == null)
		{
			return;
		}
		if (indexTang > 0)
		{
			int num = diHoaCungConfig.ListTang.Count - indexTang - 1;
		}
		btnHomQua.gameObject.SetActive(true);
		animNhanThuong.gameObject.SetActive(false);
		isNhanThuong = CommonHelper.CheckFlag(GameManager.instance.m_GameClient.UserInfo.Gamer.ThuongTieuFlag, diHoaCungConfig.ListTang.Count - indexTang - 1);
		Debug.Log("index : " + indexTang + " - " + diHoaCungConfig.ListTang[diHoaCungConfig.ListTang.Count - indexTang - 1].GiaTri + "- isNhanThuong: " + isNhanThuong);
		if (soGachDaGop >= mDiHoaCungTangData.GiaTri)
		{
			isDuDieuKien = true;
			if (isNhanThuong)
			{
				btnHomQua.gameObject.SetActive(false);
				animNhanThuong.gameObject.SetActive(false);
			}
			else
			{
				btnHomQua.gameObject.SetActive(true);
				animNhanThuong.gameObject.SetActive(true);
				animNhanThuong.GetComponent<ParticleSystem>().Play();
			}
		}
		else
		{
			isDuDieuKien = false;
			btnHomQua.gameObject.SetActive(true);
			animNhanThuong.gameObject.SetActive(false);
		}
	}

	private void displayNocDiHoaCung()
	{
		spBkg.spriteName = "cung_mai";
		spBkgDe.gameObject.SetActive(false);
		btnHomQua.gameObject.SetActive(false);
		UserInfo.ServerData.DiHoaCungCfg diHoaCungConfig = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DiHoaCungConfig;
		double num = (double)spBkg.transform.localScale.x * Math.Pow(0.95, diHoaCungConfig.ListTang.Count);
		double num2 = (double)spBkg.transform.localScale.y * (0.1 * (double)(diHoaCungConfig.ListTang.Count - indexTang));
		spBkg.transform.localScale = new Vector3((float)num, (float)num2, spBkg.transform.localScale.z);
		spBkg.depth = diHoaCungConfig.ListTang.Count;
		animNhanThuong.gameObject.SetActive(true);
	}

	public void btnHomQua_OnClick()
	{
		UserInfo.ServerData.DiHoaCungCfg diHoaCungConfig = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DiHoaCungConfig;
		if (mDiHoaCungTangData != null && !isNhanThuong && isDuDieuKien && diHoaCungConfig.ListTang != null)
		{
			NhanThuongDiHoaCungAllRequest nhanThuongDiHoaCungAllRequest = new NhanThuongDiHoaCungAllRequest();
			nhanThuongDiHoaCungAllRequest.Tang = diHoaCungConfig.ListTang.Count - indexTang - 1;
			GameManager.instance.m_GameClient.RequestNhanThuongDiHoaCungAll(nhanThuongDiHoaCungAllRequest);
		}
		else
		{
			PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
			phanThuongResponse.PhanThuongList = mDiHoaCungTangData.ListPhanThuong;
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongSeNhanDuoc"), phanThuongResponse);
		}
	}
}
