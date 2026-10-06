using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBaoKhi : ScreenBase
{
	public QuayVoCongItem ava1;

	public QuayVoCongItem ava2;

	public QuayVoCongItem ava3;

	public OtherAvatar avaCurSelected;

	public UISprite spFocus;

	public UILabel lbCurInfo;

	public UILabel lbSelectedInfo;

	public CardIndex curCarIdx;

	public int slotSelectedIdx;

	public UserInfo.ThienMaLenhInfo curBaoKhiData;

	private UserInfo.ThienMaLenhInfo resultBaoKhiData;

	private bool isOpenPopUpResult = true;

	private bool isPlayParticle;

	private bool[] m_animFinish = new bool[2];

	public GameObject mAnimSpining1;

	public GameObject mAnimSpining2;

	public GameObject mAnimSpining3;

	public GameObject mAnimResult1;

	public GameObject mAnimResult2;

	public GameObject mAnimResult3;

	private void Start()
	{
		ava1.OnFinishPlay = onStopAnim;
		ava2.OnFinishPlay = onStopAnim;
		ava3.OnFinishPlay = onStopAnim;
	}

	public void Set(UserInfo.ThienMaLenhInfo data)
	{
		if (data != null)
		{
			curBaoKhiData = data;
		}
	}

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(6);
		curCarIdx = CardIndex.ECI_1;
		displayInfo();
	}

	public void displayInfo()
	{
		displayItemSelected(curCarIdx);
		mAnimSpining1.SetActive(false);
		mAnimSpining2.SetActive(false);
		mAnimSpining3.SetActive(false);
		mAnimResult1.SetActive(false);
		mAnimResult2.SetActive(false);
		mAnimResult3.SetActive(false);
		if (curBaoKhiData != null)
		{
			ava1.listItem[0].avaVoCong.SetBaoKhiAvatar(curBaoKhiData.Slot1);
			ava1.listItem[1].avaVoCong.SetBaoKhiAvatar(curBaoKhiData.Slot1);
			ava2.listItem[0].avaVoCong.SetBaoKhiAvatar(curBaoKhiData.Slot2);
			ava2.listItem[1].avaVoCong.SetBaoKhiAvatar(curBaoKhiData.Slot2);
			ava3.listItem[0].avaVoCong.SetBaoKhiAvatar(curBaoKhiData.Slot3);
			ava3.listItem[1].avaVoCong.SetBaoKhiAvatar(curBaoKhiData.Slot3);
		}
		else
		{
			ava1.listItem[0].avaVoCong.Set("lock");
			ava1.listItem[1].avaVoCong.Set("lock");
			ava2.listItem[0].avaVoCong.Set("lock");
			ava2.listItem[1].avaVoCong.Set("lock");
			ava3.listItem[0].avaVoCong.Set("lock");
			ava3.listItem[1].avaVoCong.Set("lock");
		}
		if (curBaoKhiData != null && !string.IsNullOrEmpty(curBaoKhiData.Slot1))
		{
			CfgVoCong cfgVoCong = ConfigManager.instance.m_dicVCs[curBaoKhiData.Slot1];
			lbCurInfo.text = string.Format(Localization.instance.Get("BaoKhiInfo2"), cfgVoCong.TenHienThi, curBaoKhiData.GetTotalValue());
		}
		else
		{
			lbCurInfo.text = string.Empty;
		}
	}

	private void displayItemSelected(CardIndex index)
	{
		if (curBaoKhiData == null)
		{
			avaCurSelected.Set("lock");
			lbSelectedInfo.text = string.Empty;
			spFocus.transform.localPosition = new Vector3(ava1.gameObject.transform.localPosition.x - 2f, ava1.gameObject.transform.localPosition.y + 5f, ava1.gameObject.transform.localPosition.z);
			return;
		}
		CfgVoCong cfgVoCong = ConfigManager.instance.m_dicVCs[curBaoKhiData.Slot1];
		switch (index)
		{
		case CardIndex.ECI_1:
			curCarIdx = CardIndex.ECI_1;
			slotSelectedIdx = 1;
			spFocus.transform.localPosition = new Vector3(ava1.gameObject.transform.localPosition.x - 2f, ava1.gameObject.transform.localPosition.y + 5f, ava1.gameObject.transform.localPosition.z);
			if (!string.IsNullOrEmpty(curBaoKhiData.Slot1))
			{
				EGDebug.Log("SLOT 1: " + curBaoKhiData.Slot1);
				lbSelectedInfo.text = string.Format(Localization.instance.Get("BaoKhiVoCongInfo"), cfgVoCong.TenHienThi, curBaoKhiData.Value1);
				avaCurSelected.SetBaoKhiAvatar(curBaoKhiData.Slot1);
			}
			else
			{
				avaCurSelected.Set("lock");
				lbSelectedInfo.text = Localization.instance.Get("BaiKhiVoCongDangKhoa");
				PopupThienMaThuongPhong.Create(curBaoKhiData, 1, ThienMaAction.MO_KHOA, Localization.instance.Get("MoKhoaThienMaDescription"), "VP_HOANG_KIM_TIEU_DAO");
			}
			break;
		case CardIndex.ECI_2:
			curCarIdx = CardIndex.ECI_2;
			slotSelectedIdx = 2;
			spFocus.transform.localPosition = new Vector3(ava2.gameObject.transform.localPosition.x - 2f, ava2.gameObject.transform.localPosition.y + 5f, ava2.gameObject.transform.localPosition.z);
			if (!string.IsNullOrEmpty(curBaoKhiData.Slot2))
			{
				EGDebug.Log("SLOT 2: " + curBaoKhiData.Slot2);
				CfgVoCong cfgVoCong3 = ConfigManager.instance.m_dicVCs[curBaoKhiData.Slot2];
				lbSelectedInfo.text = string.Format(Localization.instance.Get("BaoKhiVoCong23Info"), cfgVoCong3.TenHienThi, cfgVoCong.TenHienThi, curBaoKhiData.Value2);
				avaCurSelected.SetBaoKhiAvatar(curBaoKhiData.Slot2);
			}
			else
			{
				avaCurSelected.Set("lock");
				lbSelectedInfo.text = Localization.instance.Get("BaiKhiVoCongDangKhoa");
				PopupThienMaThuongPhong.Create(curBaoKhiData, 2, ThienMaAction.MO_KHOA, Localization.instance.Get("MoKhoaThienMaDescription"), "VP_HOANG_KIM_TIEU_DAO");
			}
			break;
		case CardIndex.ECI_3:
			curCarIdx = CardIndex.ECI_3;
			slotSelectedIdx = 3;
			spFocus.transform.localPosition = new Vector3(ava3.gameObject.transform.localPosition.x - 2f, ava3.gameObject.transform.localPosition.y + 5f, ava3.gameObject.transform.localPosition.z);
			if (!string.IsNullOrEmpty(curBaoKhiData.Slot3))
			{
				EGDebug.Log("SLOT 3: " + curBaoKhiData.Slot3);
				CfgVoCong cfgVoCong2 = ConfigManager.instance.m_dicVCs[curBaoKhiData.Slot3];
				lbSelectedInfo.text = string.Format(Localization.instance.Get("BaoKhiVoCong23Info"), cfgVoCong2.TenHienThi, cfgVoCong.TenHienThi, curBaoKhiData.Value3);
				avaCurSelected.SetBaoKhiAvatar(curBaoKhiData.Slot3);
			}
			else
			{
				avaCurSelected.Set("lock");
				lbSelectedInfo.text = Localization.instance.Get("BaiKhiVoCongDangKhoa");
				PopupThienMaThuongPhong.Create(curBaoKhiData, 3, ThienMaAction.MO_KHOA, Localization.instance.Get("MoKhoaThienMaDescription"), "VP_HOANG_KIM_TIEU_DAO");
			}
			break;
		}
	}

	public void startQuay(UserInfo.ThienMaLenhInfo thienMaLenhData, bool isOpenPopUp, CardIndex cardIdx = CardIndex.ECI_NONE)
	{
		isPlayParticle = true;
		resultBaoKhiData = thienMaLenhData;
		isOpenPopUpResult = isOpenPopUp;
		if (cardIdx != CardIndex.ECI_NONE)
		{
			curCarIdx = cardIdx;
			displayItemSelected(curCarIdx);
		}
		switch (curCarIdx)
		{
		case CardIndex.ECI_1:
			startPlayAnim(mAnimSpining1);
			ava1.Play(thienMaLenhData.Slot1, getListVCRandom(VCClass.CHIEU_THUC), CardIndex.ECI_1, 120f, 0.5f, 5, 100f);
			break;
		case CardIndex.ECI_2:
			startPlayAnim(mAnimSpining2);
			ava2.Play(thienMaLenhData.Slot2, getListVCRandom(VCClass.BO_PHAP), CardIndex.ECI_2, 120f, 0.5f, 5, 100f);
			break;
		case CardIndex.ECI_3:
			startPlayAnim(mAnimSpining3);
			ava3.Play(thienMaLenhData.Slot3, getListVCRandom(VCClass.NOI_CONG), CardIndex.ECI_3, 120f, 0.5f, 5, 100f);
			break;
		}
	}

	public List<string> getListVCRandom(VCClass typeVoCong)
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, CfgVoCong> dicVC in ConfigManager.instance.m_dicVCs)
		{
			if (dicVC.Value.m_Class == typeVoCong)
			{
				list.Add(dicVC.Value.Name);
			}
		}
		return list;
	}

	private void onStopAnim1()
	{
		stopPlayAnim(mAnimSpining1);
		startPlayAnim(mAnimResult1);
	}

	private void onStopAnim2()
	{
		stopPlayAnim(mAnimSpining2);
		startPlayAnim(mAnimResult2);
	}

	private void onStopAnim3()
	{
		stopPlayAnim(mAnimSpining3);
		startPlayAnim(mAnimResult3);
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

	private void onStopAnim(CardIndex cardNumber)
	{
		m_animFinish[0] = true;
		m_animFinish[1] = true;
		switch (cardNumber)
		{
		case CardIndex.ECI_1:
			onStopAnim1();
			break;
		case CardIndex.ECI_2:
			onStopAnim2();
			break;
		case CardIndex.ECI_3:
			onStopAnim3();
			break;
		}
		if (isOpenPopUpResult)
		{
			StartCoroutine(delayOpenPopupSuccess(1f));
			return;
		}
		Set(resultBaoKhiData);
		displayInfo();
		isPlayParticle = false;
	}

	public IEnumerator delayOpenPopupSuccess(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		if (resultBaoKhiData != null)
		{
			PopupNangCapThienMaLenhResult.Create(curBaoKhiData, resultBaoKhiData, slotSelectedIdx, ThienMaAction.KHAM);
		}
		isPlayParticle = false;
	}

	public void btnKham_OnClick(GameObject go)
	{
		if ((curCarIdx != CardIndex.ECI_1 || !string.IsNullOrEmpty(curBaoKhiData.Slot1)) && (curCarIdx != CardIndex.ECI_2 || !string.IsNullOrEmpty(curBaoKhiData.Slot2)) && (curCarIdx != CardIndex.ECI_3 || !string.IsNullOrEmpty(curBaoKhiData.Slot3)) && !isPlayParticle)
		{
			if ((curCarIdx == CardIndex.ECI_2 && (string.IsNullOrEmpty(curBaoKhiData.Slot1) || curBaoKhiData.Slot2 == null)) || (curCarIdx == CardIndex.ECI_3 && (string.IsNullOrEmpty(curBaoKhiData.Slot2) || curBaoKhiData.Slot3 == null)))
			{
				MessagePopup.Create(Localization.instance.Get("ThongBaoKhamError"));
				return;
			}
			resultBaoKhiData = null;
			PopupThienMaThuongPhong.Create(curBaoKhiData, slotSelectedIdx, ThienMaAction.KHAM, Localization.instance.Get("KhamThienMaDescription"), "VP_HOANG_KIM_TIEU_DAO");
		}
	}

	public void btnCuongHoa_OnClick(GameObject go)
	{
		if ((curCarIdx != CardIndex.ECI_1 || !string.IsNullOrEmpty(curBaoKhiData.Slot1)) && (curCarIdx != CardIndex.ECI_2 || !string.IsNullOrEmpty(curBaoKhiData.Slot2)) && (curCarIdx != CardIndex.ECI_3 || !string.IsNullOrEmpty(curBaoKhiData.Slot3)) && !isPlayParticle)
		{
			displayPopupResult();
		}
	}

	public void VoCong1_OnClick(GameObject go)
	{
		if (!isPlayParticle)
		{
			displayItemSelected(CardIndex.ECI_1);
		}
	}

	public void VoCong2_OnClick(GameObject go)
	{
		if (!isPlayParticle)
		{
			displayItemSelected(CardIndex.ECI_2);
		}
	}

	public void VoCong3_OnClick(GameObject go)
	{
		if (!isPlayParticle)
		{
			displayItemSelected(CardIndex.ECI_3);
		}
	}

	public void displayPopupResult()
	{
		UserInfo.ThienMaLenhInfo thienMaLenhInfo = new UserInfo.ThienMaLenhInfo();
		thienMaLenhInfo.GID = curBaoKhiData.GID;
		thienMaLenhInfo.HID = curBaoKhiData.HID;
		thienMaLenhInfo.ID = curBaoKhiData.ID;
		thienMaLenhInfo.Slot1 = curBaoKhiData.Slot1;
		thienMaLenhInfo.Slot2 = curBaoKhiData.Slot2;
		thienMaLenhInfo.Slot3 = curBaoKhiData.Slot3;
		thienMaLenhInfo.TichLuy1 = curBaoKhiData.TichLuy1;
		thienMaLenhInfo.TichLuy2 = curBaoKhiData.TichLuy2;
		thienMaLenhInfo.TichLuy3 = curBaoKhiData.TichLuy3;
		thienMaLenhInfo.Value1 = curBaoKhiData.Value1;
		thienMaLenhInfo.Value2 = curBaoKhiData.Value2;
		thienMaLenhInfo.Value3 = curBaoKhiData.Value3;
		switch (slotSelectedIdx)
		{
		case 1:
			thienMaLenhInfo.TichLuy1++;
			thienMaLenhInfo.Value1 = CommonHelper.GetValueVoCongSlot(thienMaLenhInfo.Slot1, thienMaLenhInfo.TichLuy1);
			break;
		case 2:
			thienMaLenhInfo.TichLuy2++;
			thienMaLenhInfo.Value2 = CommonHelper.GetValueVoCongSlot(thienMaLenhInfo.Slot2, thienMaLenhInfo.TichLuy2);
			break;
		case 3:
			thienMaLenhInfo.TichLuy3++;
			thienMaLenhInfo.Value3 = CommonHelper.GetValueVoCongSlot(thienMaLenhInfo.Slot3, thienMaLenhInfo.TichLuy3);
			break;
		}
		if (PopupNangCapThienMaLenhResult.instance == null)
		{
			PopupNangCapThienMaLenhResult.Create(curBaoKhiData, thienMaLenhInfo, slotSelectedIdx, ThienMaAction.CUONG_HOA);
		}
		else
		{
			PopupNangCapThienMaLenhResult.instance.displayInfoResult(curBaoKhiData, thienMaLenhInfo, PopupNangCapThienMaLenhResult.instance.currentSlot);
		}
	}

	public void btnBack_OnClick(GameObject go)
	{
		GUIManager.instance.SetScreen(GUIManager.instance.LastScreen);
	}
}
