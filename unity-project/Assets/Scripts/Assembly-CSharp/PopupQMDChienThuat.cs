using UnityEngine;

public class PopupQMDChienThuat : MonoBehaviour
{
	public NhanVatAvatar[] AvatarList;

	public UICheckbox ChuDongBtn;

	public UICheckbox BiDongBtn;

	public GameObject MucTieuGroup;

	public UICheckbox optionGanNhat;

	public UICheckbox optionMauNhieuNhat;

	public UICheckbox optionMauItNhat;

	public UICheckbox optionThanPhapCaoNhat;

	public UICheckbox optionThanPhapThapNhat;

	public UICheckbox optionCongToNhat;

	public UICheckbox optionCongNhoNhat;

	public UICheckbox optionKhiLonNhat;

	public UICheckbox optionKhiNhoNhat;

	public static PopupQMDChienThuat instance;

	private QMDInfo.QMDNhanVat TargetNhatVat;

	private QMDInfo m_CurrInfo;

	private bool isChuDong = true;

	public void OnSetChuDong(bool active)
	{
		if (active)
		{
			isChuDong = true;
		}
	}

	public void OnSetBiDong(bool active)
	{
		if (active)
		{
			isChuDong = false;
		}
	}

	public void OnSelectGanNhat(bool active)
	{
		if (active && TargetNhatVat != null)
		{
			if (isChuDong)
			{
				TargetNhatVat.ChienThuat.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_GAN_NHAT;
			}
			else
			{
				TargetNhatVat.ChienThuat.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_GAN_NHAT;
			}
		}
	}

	public void OnSelectMauNhieuNhat(bool active)
	{
		if (active && TargetNhatVat != null)
		{
			if (isChuDong)
			{
				TargetNhatVat.ChienThuat.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_SINH_LUC_MAX;
			}
			else
			{
				TargetNhatVat.ChienThuat.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_SINH_LUC_MAX;
			}
		}
	}

	public void OnSelectMauItNhat(bool active)
	{
		if (active && TargetNhatVat != null)
		{
			if (isChuDong)
			{
				TargetNhatVat.ChienThuat.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_SINH_LUC_MIN;
			}
			else
			{
				TargetNhatVat.ChienThuat.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_SINH_LUC_MIN;
			}
		}
	}

	public void OnSelectCongNhieuNhat(bool active)
	{
		if (active && TargetNhatVat != null)
		{
			if (isChuDong)
			{
				TargetNhatVat.ChienThuat.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_CONG_MAX;
			}
			else
			{
				TargetNhatVat.ChienThuat.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_CONG_MAX;
			}
		}
	}

	public void OnSelectCongItNhat(bool active)
	{
		if (active && TargetNhatVat != null)
		{
			if (isChuDong)
			{
				TargetNhatVat.ChienThuat.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_CONG_MIN;
			}
			else
			{
				TargetNhatVat.ChienThuat.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_CONG_MIN;
			}
		}
	}

	public void OnSelectThanNhieuNhat(bool active)
	{
		if (active && TargetNhatVat != null)
		{
			if (isChuDong)
			{
				TargetNhatVat.ChienThuat.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_THAN_PHAP_MAX;
			}
			else
			{
				TargetNhatVat.ChienThuat.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_THAN_PHAP_MAX;
			}
		}
	}

	public void OnSelectThanItNhat(bool active)
	{
		if (active && TargetNhatVat != null)
		{
			if (isChuDong)
			{
				TargetNhatVat.ChienThuat.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_THAN_PHAP_MIN;
			}
			else
			{
				TargetNhatVat.ChienThuat.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_THAN_PHAP_MIN;
			}
		}
	}

	public void OnSelectKhiNhieuNhat(bool active)
	{
		if (active && TargetNhatVat != null)
		{
			if (isChuDong)
			{
				TargetNhatVat.ChienThuat.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_NOI_MAX;
			}
			else
			{
				TargetNhatVat.ChienThuat.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_NOI_MAX;
			}
		}
	}

	public void OnSelectKhiItNhat(bool active)
	{
		if (active && TargetNhatVat != null)
		{
			if (isChuDong)
			{
				TargetNhatVat.ChienThuat.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_NOI_MIN;
			}
			else
			{
				TargetNhatVat.ChienThuat.ChienThuat = UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_NOI_MIN;
			}
		}
	}

	public void OnSelectNV1()
	{
		SetSelectNVTo(0);
	}

	public void OnSelectNV2()
	{
		SetSelectNVTo(1);
	}

	public void OnSelectNV3()
	{
		SetSelectNVTo(2);
	}

	public void OnSelectNV4()
	{
		SetSelectNVTo(3);
	}

	public void SetGUIChienThuatFromData(UserInfo.HeroData.AI ai)
	{
		if (ai.IsChuDong())
		{
			ChuDongBtn.isChecked = true;
			BiDongBtn.isChecked = false;
		}
		else
		{
			ChuDongBtn.isChecked = false;
			BiDongBtn.isChecked = true;
		}
		optionGanNhat.isChecked = false;
		optionMauNhieuNhat.isChecked = false;
		optionMauItNhat.isChecked = false;
		optionThanPhapCaoNhat.isChecked = false;
		optionThanPhapThapNhat.isChecked = false;
		optionCongToNhat.isChecked = false;
		optionCongNhoNhat.isChecked = false;
		optionKhiLonNhat.isChecked = false;
		optionKhiNhoNhat.isChecked = false;
		switch (ai.ChienThuat)
		{
		case UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_GAN_NHAT:
		case UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_GAN_NHAT:
			optionGanNhat.isChecked = true;
			break;
		case UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_SINH_LUC_MAX:
		case UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_SINH_LUC_MAX:
			optionMauNhieuNhat.isChecked = true;
			break;
		case UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_SINH_LUC_MIN:
		case UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_SINH_LUC_MIN:
			optionMauItNhat.isChecked = true;
			break;
		case UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_THAN_PHAP_MAX:
		case UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_THAN_PHAP_MAX:
			optionThanPhapCaoNhat.isChecked = true;
			break;
		case UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_THAN_PHAP_MIN:
		case UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_THAN_PHAP_MIN:
			optionThanPhapThapNhat.isChecked = true;
			break;
		case UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_CONG_MAX:
		case UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_CONG_MAX:
			optionCongToNhat.isChecked = true;
			break;
		case UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_CONG_MIN:
		case UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_CONG_MIN:
			optionCongNhoNhat.isChecked = true;
			break;
		case UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_NOI_MAX:
		case UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_NOI_MAX:
			optionKhiLonNhat.isChecked = true;
			break;
		case UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_NOI_MIN:
		case UserInfo.HeroData.AI.ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_NOI_MIN:
			optionKhiNhoNhat.isChecked = true;
			break;
		}
	}

	public static PopupQMDChienThuat Create(QMDInfo info)
	{
		DestroyPopup();
		Object obj = Object.Instantiate(Resources.Load("Popup/PopupQMDChienThuat"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		instance = gameObject.GetComponent<PopupQMDChienThuat>();
		instance.Set(info);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		return instance;
	}

	private void Set(QMDInfo info)
	{
		m_CurrInfo = info;
		SetSelectNVTo(0);
	}

	private void SetSelectNVTo(int idx)
	{
		if (idx == 0)
		{
			AvatarList[0].Set(m_CurrInfo.NV1.Name, 0, -1, true);
			AvatarList[1].Set(m_CurrInfo.NV2.Name);
			AvatarList[2].Set(m_CurrInfo.NV3.Name);
			AvatarList[3].Set(m_CurrInfo.NV4.Name);
			TargetNhatVat = m_CurrInfo.NV1;
		}
		if (idx == 1)
		{
			AvatarList[0].Set(m_CurrInfo.NV1.Name);
			AvatarList[1].Set(m_CurrInfo.NV2.Name, 0, -1, true);
			AvatarList[2].Set(m_CurrInfo.NV3.Name);
			AvatarList[3].Set(m_CurrInfo.NV4.Name);
			TargetNhatVat = m_CurrInfo.NV2;
		}
		if (idx == 2)
		{
			AvatarList[0].Set(m_CurrInfo.NV1.Name);
			AvatarList[1].Set(m_CurrInfo.NV2.Name);
			AvatarList[2].Set(m_CurrInfo.NV3.Name, 0, -1, true);
			AvatarList[3].Set(m_CurrInfo.NV4.Name);
			TargetNhatVat = m_CurrInfo.NV3;
		}
		if (idx == 3)
		{
			AvatarList[0].Set(m_CurrInfo.NV1.Name);
			AvatarList[1].Set(m_CurrInfo.NV2.Name);
			AvatarList[2].Set(m_CurrInfo.NV3.Name);
			AvatarList[3].Set(m_CurrInfo.NV4.Name, 0, -1, true);
			TargetNhatVat = m_CurrInfo.NV4;
		}
		SetGUIChienThuatFromData(TargetNhatVat.ChienThuat);
	}

	private void OnSaveBtnClick()
	{
		QMDTranHinhRequest qMDTranHinhRequest = new QMDTranHinhRequest();
		qMDTranHinhRequest.info = m_CurrInfo;
		GameManager.instance.m_GameClient.RequestQMDSelect(qMDTranHinhRequest);
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}
}
