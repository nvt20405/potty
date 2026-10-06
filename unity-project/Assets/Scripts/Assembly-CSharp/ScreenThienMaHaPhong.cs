using System.Collections.Generic;
using UnityEngine;

public class ScreenThienMaHaPhong : ScreenBase
{
	public List<GameObject> FormationSlots;

	public GameObject CenterObject;

	public GameObject PinPoint;

	public bool isSpinning;

	public float speed;

	public float Round;

	public int SelectedSlot;

	public int ThienMaBuffVal;

	private int HeroID;

	private int SelectedHero;

	public UILabel ThienMaBuffLabel;

	public void Sync(int heroID, int val)
	{
		speed = 240f;
		Round = Random.Range(0.2f, 0.5f);
		isSpinning = true;
		SelectedSlot = GameManager.instance.m_GameClient.UserInfo.DoiHinh.ListRaTran.IndexOf(heroID);
		ThienMaBuffVal = val;
		HeroID = heroID;
		ThienMaBuffLabel.text = "+ ????";
	}

	private void OnEnable()
	{
		int i = 0;
		ThienMaBuffLabel.text = "+ ????";
		foreach (GameObject formationSlot in FormationSlots)
		{
			foreach (Transform item in formationSlot.transform)
			{
				Transform transform2 = item;
				Object.Destroy(transform2.gameObject);
			}
			if (GameManager.instance.m_GameClient.UserInfo.DoiHinh.ListRaTran.Count > i)
			{
				UserInfo.HeroData hero = GameManager.instance.m_GameClient.UserInfo.HeroList.Find((UserInfo.HeroData h) => h.HID == GameManager.instance.m_GameClient.UserInfo.DoiHinh.ListRaTran[i]);
				Avatar3D avatar3D = GUIManager.instance.InstantiateAvatar3D(hero.Name, string.Empty, string.Empty, string.Empty, null, null, null, (hero.CostumeID <= 0) ? string.Empty : GameManager.instance.m_GameClient.UserInfo.CostumeList.Find((UserInfo.CostumeData c) => c.ID == hero.CostumeID).CodeName, string.Empty);
				avatar3D.transform.parent = formationSlot.transform;
				avatar3D.transform.localPosition = Vector3.zero;
				avatar3D.transform.localScale = Vector3.one;
				avatar3D.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
				avatar3D.PlayAnimIdle();
			}
			i++;
		}
	}

	private void Update()
	{
		if (!isSpinning)
		{
			return;
		}
		if (Round > 0f)
		{
			CenterObject.transform.Rotate(Time.deltaTime * speed * Vector3.up);
			Round -= Time.deltaTime * speed / 360f;
			return;
		}
		float num = Vector3.Distance(FormationSlots[SelectedSlot].transform.position, PinPoint.transform.position);
		if (num < 1f && num > 0.2f)
		{
			CenterObject.transform.Rotate(Time.deltaTime * speed * Vector3.up * num);
			return;
		}
		if (num > 1f)
		{
			CenterObject.transform.Rotate(Time.deltaTime * speed * Vector3.up);
			return;
		}
		isSpinning = false;
		UILabel thienMaBuffLabel = ThienMaBuffLabel;
		string text = ThienMaBuffVal.ToString();
		Localization instance = Localization.instance;
		ChiSoCoBan chiSoThienMaBuf = (ChiSoCoBan)ConfigManager.instance.m_dicNhanVats[GameManager.instance.m_GameClient.UserInfo.HeroList.Find((UserInfo.HeroData h) => h.HID == HeroID).Name].ChiSoThienMaBuf;
		thienMaBuffLabel.text = "+ " + text + " " + instance.Get(chiSoThienMaBuf.ToString() + "Label");
	}

	public void OnSpinBtn()
	{
		if (GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_THAN_HUYET_THACH"))
		{
			GameManager.instance.m_GameClient.RequestQuayThienMaHaPhong(-1, GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_THAN_HUYET_THACH").ID, false, 0);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("ThienMaHaPhongKhongDu"));
		}
	}

	public void OnSpecialSpinBtn()
	{
		List<int> list = new List<int>();
		foreach (UserInfo.HeroData hero in GameManager.instance.m_GameClient.UserInfo.HeroList)
		{
			if (!GameManager.instance.m_GameClient.UserInfo.DoiHinh.ListRaTran.Contains(hero.HID))
			{
				list.Add(hero.HID);
			}
		}
		PopupSelectNhanVat.Create(FinishSelectHero, list);
	}

	public bool FinishSelectHero(int heroID)
	{
		if (GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_THAN_HUYET_THACH") && GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_THAN_VUONG_TINH_HUYET"))
		{
			SelectedHero = heroID;
			PopupYesNo.Create(Localization.instance.Get("ThienMaSpecialConfirm"), Localization.instance.Get("DongYLabelBtn"), Localization.instance.Get("TuChoiLabelBtn"), ConfirmSpecialSpin, null);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("ThienMaHaPhongSpecialKhongDu"));
		}
		return true;
	}

	public void ConfirmSpecialSpin()
	{
		GameManager.instance.m_GameClient.RequestQuayThienMaHaPhong(SelectedHero, GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_THAN_HUYET_THACH").ID, false, GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_THAN_VUONG_TINH_HUYET").ID);
	}

	public void OnSpecialSpinCaoCapBtn()
	{
		List<int> list = new List<int>();
		foreach (UserInfo.HeroData hero in GameManager.instance.m_GameClient.UserInfo.HeroList)
		{
			if (!GameManager.instance.m_GameClient.UserInfo.DoiHinh.ListRaTran.Contains(hero.HID))
			{
				list.Add(hero.HID);
			}
		}
		PopupSelectNhanVat.Create(FinishSelectHeroCaoCap, list);
	}

	public bool FinishSelectHeroCaoCap(int heroID)
	{
		if (GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_THAN_HUYET_THACH") && GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_THAN_VUONG_TINH_HUYET"))
		{
			SelectedHero = heroID;
			PopupYesNo.Create(Localization.instance.Get("ThienMaCaoCapSpecialConfirm"), Localization.instance.Get("DongYLabelBtn"), Localization.instance.Get("TuChoiLabelBtn"), ConfirmCaoCapSpecialSpin, null);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("ThienMaHaPhongCaoCapKhongDu"));
		}
		return true;
	}

	public void ConfirmCaoCapSpecialSpin()
	{
		GameManager.instance.m_GameClient.RequestQuayThienMaHaPhong(SelectedHero, GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_THAN_HUYET_THACH").ID, true, GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_THAN_VUONG_TINH_HUYET").ID);
	}

	public void OnHelpBtn()
	{
		ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
		screenHelpInfo.setByLevel(0, 11);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
		EGDebug.Log("btnHelp_OnClick");
	}
}
