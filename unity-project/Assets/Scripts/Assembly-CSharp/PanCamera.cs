using System.Collections.Generic;
using UnityEngine;

public class PanCamera
{
	public float ZoomTime = 1.5f;

	public float ZoomTimeCurrent;

	public float ZoomMaxFOV = 70f;

	public float ZoomMinFOV = 50f;

	private bool m_bPanCam_Z;

	private Vector3 m_vTarPanCam_Z;

	private bool m_bPanCam_X;

	private Vector3 m_vTarPanCam_X;

	private float PanNgangSpeed = 1f;

	private float PanDocSpeed = 1f;

	private Vector3 VCamO = new Vector3(0f, 9f, 15f);

	private Vector3 VCamMax = new Vector3(0f, 12f, 15f);

	private Vector3 VCamRot = new Vector3(35f, 180f, 0f);

	private Vector3 VDauNoiCong = new Vector3(-1.87f, 8.73f, 10.9f);

	private Vector3 ODauNoiCong = new Vector3(42f, 173.7351f, 0f);

	public PanCamMode Mode { get; set; }

	public Vector3 VNhanVatHoiThoai { get; set; }

	public bool DauNoiCong { get; set; }

	public PanCamera()
	{
		Mode = PanCamMode.BinhThuong;
		Camera.main.transform.position = VCamO;
		Camera.main.transform.eulerAngles = VCamRot;
		DauNoiCong = false;
	}

	public void Update()
	{
		if (Mode == PanCamMode.BatDauTran)
		{
			float num = (ZoomMaxFOV - ZoomMinFOV) / ZoomTime;
			float num2 = (VCamMax.y - VCamO.y) / ZoomTime;
			Camera.main.fieldOfView -= num * Time.deltaTime;
			Vector3 vector = default(Vector3);
			vector = new Vector3(0f, num2 * Time.deltaTime, 0f);
			Camera.main.transform.position -= vector;
			if (Camera.main.fieldOfView <= ZoomMinFOV)
			{
				Mode = PanCamMode.BinhThuong;
			}
		}
		else if (DauNoiCong)
		{
			Camera.main.transform.position = VDauNoiCong;
			Camera.main.transform.eulerAngles = ODauNoiCong;
		}
		else
		{
			OnPanCam_X();
			OnPanCam_Z();
		}
	}

	public void SetPanZ(Vector3 tar)
	{
		m_bPanCam_Z = true;
		m_vTarPanCam_Z = VCamO + tar;
		m_vTarPanCam_Z.x = (m_vTarPanCam_Z.y = 0f);
	}

	private void CheckIfMoveCam_Z()
	{
		m_vTarPanCam_Z = GetVNextCenter_Z();
		Vector3 vector = m_vTarPanCam_Z - Camera.main.transform.position;
		vector.x = (vector.y = 0f);
		m_bPanCam_Z = vector.magnitude > 1f;
	}

	private void OnPanCam_Z()
	{
		CheckIfMoveCam_Z();
		if (m_bPanCam_Z)
		{
			Vector3 vector = m_vTarPanCam_Z - Camera.main.transform.position;
			vector.y = (vector.x = 0f);
			Camera.main.transform.position += vector.normalized * PanDocSpeed * Time.deltaTime;
			if (vector.magnitude < 2f * Time.deltaTime * PanDocSpeed)
			{
				m_bPanCam_Z = false;
			}
		}
	}

	private void OnPanCam_X()
	{
		if (!m_bPanCam_X)
		{
			Vector3 vector = Vector3.zero;
			switch (Mode)
			{
			case PanCamMode.BinhThuong:
				PanNgangSpeed = 1f;
				vector = GetVNextPanCam_X();
				break;
			case PanCamMode.HoiThoai:
				PanNgangSpeed = 2f;
				vector = VCamO + VNhanVatHoiThoai;
				break;
			}
			Vector3 vector2 = vector - Camera.main.transform.position;
			vector2.y = (vector2.z = 0f);
			if (vector2.magnitude > 0.4f)
			{
				m_bPanCam_X = true;
				m_vTarPanCam_X = vector;
			}
		}
		if (m_bPanCam_X)
		{
			Vector3 vector3 = m_vTarPanCam_X - Camera.main.transform.position;
			vector3.y = (vector3.z = 0f);
			Camera.main.transform.position += vector3.normalized * PanNgangSpeed * Time.deltaTime;
			if (vector3.magnitude < 2f * Time.deltaTime * PanNgangSpeed)
			{
				m_bPanCam_X = false;
			}
		}
	}

	private Vector3 GetVNextPanCam_X()
	{
		ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
		Vector3 zero = Vector3.zero;
		Vector3 vector = default(Vector3);
		vector = new Vector3(float.MinValue, 0f, 0f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(float.MaxValue, 0f, 0f);
		int num = 0;
		foreach (KeyValuePair<int, BattleHero> dicHero in screenBattle.m_dicHeros)
		{
			if (dicHero.Value.State != HeroState.DEAD && NGUITools.GetActive(dicHero.Value.gameObject))
			{
				zero.x += dicHero.Value.transform.position.x;
				num++;
				if (vector.x < dicHero.Value.transform.position.x)
				{
					vector = dicHero.Value.transform.position;
				}
				if (vector2.x > dicHero.Value.transform.position.x)
				{
					vector2 = dicHero.Value.transform.position;
				}
			}
		}
		if (num > 0)
		{
			zero.x /= num;
		}
		Vector3 vector3 = (vector + vector2) / 2f;
		vector3.z = (vector3.y = 0f);
		return VCamO + vector3;
	}

	private Vector3 GetVNextCenter_Z()
	{
		ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 0f, float.MinValue);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, 0f, float.MaxValue);
		Vector3 zero = Vector3.zero;
		int num = 0;
		foreach (KeyValuePair<int, BattleHero> dicHero in screenBattle.m_dicHeros)
		{
			if (dicHero.Value.State != HeroState.DEAD && NGUITools.GetActive(dicHero.Value.gameObject))
			{
				if (vector.z < dicHero.Value.transform.position.z)
				{
					vector = dicHero.Value.transform.position;
				}
				if (vector2.z > dicHero.Value.transform.position.z)
				{
					vector2 = dicHero.Value.transform.position;
				}
				zero.z += dicHero.Value.transform.position.z;
				num++;
			}
		}
		if (num > 0)
		{
			zero.z /= num;
		}
		Vector3 vector3 = zero;
		if (Mode == PanCamMode.HoiThoai)
		{
			vector3 = VNhanVatHoiThoai;
		}
		vector3.x = (vector3.y = 0f);
		Vector3 result = VCamO + vector3;
		result.z = Mathf.Min(result.z, 18.5f);
		return result;
	}

	public void BatDauTran()
	{
		Mode = PanCamMode.BatDauTran;
		Camera.main.fieldOfView = 70f;
		Camera.main.transform.position = VCamMax;
	}
}
