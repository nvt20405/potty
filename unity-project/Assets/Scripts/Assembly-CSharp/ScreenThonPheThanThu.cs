using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenThonPheThanThu : ScreenBase
{
	private UserInfo.PetInfo thanthu;

	public ThanThuAvatar thanthuAvatar;

	public UILabel thanThuName;

	public UILabel thanThuThonPheName;

	public UISprite ChiSo1IconTruocThuong;

	public UISprite ChiSo2IconTruocThuong;

	public UILabel ChiSo1TruocThuong;

	public UILabel ChiSo2TruocThuong;

	public UILabel ChiSo1TangThuong;

	public UILabel ChiSo2TangThuong;

	public UISprite ChiSo1IconTruocCaoCap;

	public UISprite ChiSo2IconTruocCaoCap;

	public UILabel ChiSo1TruocCaoCap;

	public UILabel ChiSo2TruocCaoCap;

	public UILabel ChiSo1TangCaoCap;

	public UILabel ChiSo2TangCaoCap;

	public UILabel TieuHaoThuongLabel;

	public UILabel TieuHaoCaoCapLabel;

	public UILabel TieuHaoNKDThuong;

	public UILabel TieuHaoNKDCaoCap;

	public ThanThuAvatar thanthuHiSinh1;

	public int selectedThanthu;

	public UISprite bg1;

	public UISprite bg2;

	public UISprite ButtonThuong;

	public UISprite ButtonCaoCap;

	private bool isCaoCap;

	private bool effectTime;

	private void Awake()
	{
		UIAnchor[] componentsInChildren = GetComponentsInChildren<UIAnchor>();
		UIAnchor[] array = componentsInChildren;
		UIAnchor[] array2 = array;
		foreach (UIAnchor uIAnchor in array2)
		{
			uIAnchor.widgetContainer = GUIManager.instance.GameFrame;
		}
	}

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(6);
		AudioListener component = GUIManager.instance.cam2D.GetComponent<AudioListener>();
		if (component != null)
		{
			component.enabled = false;
		}
	}

	public override void OnDeactive()
	{
		AudioListener component = GUIManager.instance.cam2D.GetComponent<AudioListener>();
		if (component != null)
		{
			component.enabled = true;
		}
		base.OnDeactive();
	}

	public void BeginThonPhe()
	{
		UserInfo.PetInfo petInfo = GameManager.instance.m_GameClient.UserInfo.ListThanThu.Find((UserInfo.PetInfo p) => p.ID == thanthu.ID);
		int newChiSo = 0;
		int newChiSo2 = 0;
		if (petInfo.GetChiSo1() == ChiSoCoBan.Menh)
		{
			newChiSo = petInfo.GetHP();
		}
		else if (petInfo.GetChiSo1() == ChiSoCoBan.Ngoai)
		{
			newChiSo = petInfo.GetCong();
		}
		else if (petInfo.GetChiSo1() == ChiSoCoBan.ThanPhap)
		{
			newChiSo = petInfo.GetThu();
		}
		else if (petInfo.GetChiSo1() == ChiSoCoBan.Noi)
		{
			newChiSo = petInfo.GetMP();
		}
		if (petInfo.GetChiSo2() == ChiSoCoBan.Menh)
		{
			newChiSo2 = petInfo.GetHP();
		}
		else if (petInfo.GetChiSo2() == ChiSoCoBan.Ngoai)
		{
			newChiSo2 = petInfo.GetCong();
		}
		else if (petInfo.GetChiSo2() == ChiSoCoBan.ThanPhap)
		{
			newChiSo2 = petInfo.GetThu();
		}
		else if (petInfo.GetChiSo2() == ChiSoCoBan.Noi)
		{
			newChiSo2 = petInfo.GetMP();
		}
		if (isCaoCap)
		{
			bg1.color = new Color(0.2f, 0.2f, 0.2f);
			ButtonThuong.color = new Color(0.2f, 0.2f, 0.2f);
		}
		else
		{
			bg2.color = new Color(0.2f, 0.2f, 0.2f);
			ButtonCaoCap.color = new Color(0.2f, 0.2f, 0.2f);
		}
		int oldChiSo = int.Parse(ChiSo1TruocCaoCap.text);
		int oldChiSo2 = int.Parse(ChiSo2TruocCaoCap.text);
		if (isCaoCap)
		{
			StartCoroutine(BeginThonPheEffect(oldChiSo, oldChiSo2, newChiSo, newChiSo2, ChiSo1TruocCaoCap, ChiSo2TruocCaoCap, ChiSo1TangCaoCap, ChiSo2TangCaoCap));
		}
		else
		{
			StartCoroutine(BeginThonPheEffect(oldChiSo, oldChiSo2, newChiSo, newChiSo2, ChiSo1TruocThuong, ChiSo2TruocThuong, ChiSo1TangThuong, ChiSo2TangThuong));
		}
	}

	private IEnumerator BeginThonPheEffect(int oldChiSo1, int oldChiSo2, int newChiSo1, int newChiSo2, UILabel chiso1, UILabel chiso2, UILabel hieuso1, UILabel hieuso2)
	{
		effectTime = true;
		chiso1.text = oldChiSo1.ToString();
		chiso2.text = oldChiSo2.ToString();
		float delta1Predict;
		float delta2Predict;
		if (isCaoCap)
		{
			delta1Predict = int.Parse(ChiSo1TangCaoCap.text.Replace("[00FF00]~", string.Empty));
			delta2Predict = int.Parse(ChiSo2TangCaoCap.text.Replace("[00FF00]~", string.Empty));
		}
		else
		{
			delta1Predict = int.Parse(ChiSo1TangThuong.text.Replace("[00FF00]~", string.Empty));
			delta2Predict = int.Parse(ChiSo2TangThuong.text.Replace("[00FF00]~", string.Empty));
		}
		float count = 80f;
		float delta1 = newChiSo1 - oldChiSo1;
		float delta2 = newChiSo2 - oldChiSo2;
		count = Mathf.Min(delta1 + delta2, count);
		delta1 /= count;
		delta2 /= count;
		string mamau1 = "[FF0000]";
		string mamau2 = "[FF0000]";
		for (int i = 0; (float)i < 0.8f * count; i++)
		{
			yield return new WaitForSeconds(0.02f);
			chiso1.text = ((int)((float)oldChiSo1 + (float)i * delta1)).ToString();
			chiso2.text = ((int)((float)oldChiSo2 + (float)i * delta2)).ToString();
			hieuso1.text = mamau1 + (int)((float)i * delta1);
			hieuso2.text = mamau2 + (int)((float)i * delta2);
		}
		for (int j = (int)(0.8f * count); (float)j < count; j++)
		{
			yield return new WaitForSeconds(0.04f);
			chiso1.text = ((int)((float)oldChiSo1 + (float)j * delta1)).ToString();
			chiso2.text = ((int)((float)oldChiSo2 + (float)j * delta2)).ToString();
			if (delta1Predict <= (float)(j + 1) * delta1)
			{
				mamau1 = "[00FF00]";
			}
			if (delta2Predict <= (float)(j + 1) * delta2)
			{
				mamau2 = "[00FF00]";
			}
			hieuso1.text = mamau1 + (int)((float)j * delta1);
			hieuso2.text = mamau2 + (int)((float)j * delta2);
		}
		chiso1.text = newChiSo1.ToString();
		chiso2.text = newChiSo2.ToString();
		hieuso1.text = mamau1 + (int)(count * delta1);
		hieuso2.text = mamau2 + (int)(count * delta2);
		yield return new WaitForSeconds(2f);
		effectTime = false;
		Set(thanthu.ID);
	}

	public void Set(int thanthuID, UserInfo.PetInfo thanthuThonPhe = null)
	{
		bg1.color = new Color(1f, 1f, 1f);
		ButtonThuong.color = new Color(1f, 1f, 1f);
		bg2.color = new Color(1f, 1f, 1f);
		ButtonCaoCap.color = new Color(1f, 1f, 1f);
		thanthu = GameManager.instance.m_GameClient.UserInfo.ListThanThu.Find((UserInfo.PetInfo p) => p.ID == thanthuID);
		thanThuName.text = Localization.instance.Get(thanthu.codename);
		UserInfo.PetInfo petInfo = new UserInfo.PetInfo(thanthu);
		UserInfo.PetInfo petInfo2 = new UserInfo.PetInfo(thanthu);
		if (thanthuThonPhe != null)
		{
			selectedThanthu = thanthuThonPhe.ID;
			petInfo.DiemThonPhe += thanthuThonPhe.GetDiemThonPhe();
			petInfo2.DiemThonPhe += (int)(1.5f * (float)thanthuThonPhe.GetDiemThonPhe());
			float num = (float)thanthuThonPhe.GetChiPhiThonPhe() * (float)ConfigManager.instance.OtherConfig.BacThonPhe / 1000000f;
			int num2 = thanthuThonPhe.GetChiPhiThonPhe() * ConfigManager.instance.OtherConfig.KNBThonPhe;
			thanThuThonPheName.text = Localization.instance.Get(thanthuThonPhe.codename);
			TieuHaoThuongLabel.text = ((!(num < 1000f)) ? (num / 1000f + " " + Localization.instance.Get("Ty")) : ((int)num + " " + Localization.instance.Get("Trieu")));
			TieuHaoCaoCapLabel.text = num2.ToString();
			int num3 = thanthuThonPhe.GetChiPhiThonPhe() * ConfigManager.instance.OtherConfig.NKDThonPhe;
			TieuHaoNKDThuong.text = num3.ToString();
			TieuHaoNKDCaoCap.text = num3.ToString();
		}
		else
		{
			selectedThanthu = 0;
			thanThuThonPheName.text = string.Empty;
			TieuHaoThuongLabel.text = "0";
			TieuHaoCaoCapLabel.text = "0";
			TieuHaoNKDThuong.text = "0";
			TieuHaoNKDCaoCap.text = "0";
		}
		thanthuAvatar.Set(thanthu);
		if (thanthu.GetChiSo1() == ChiSoCoBan.Menh)
		{
			ChiSo1IconTruocThuong.spriteName = "icon_mau";
			ChiSo1TruocThuong.text = thanthu.GetHP().ToString();
			ChiSo1TangThuong.text = "[00FF00]~" + (petInfo.GetHP() - thanthu.GetHP());
			ChiSo1IconTruocCaoCap.spriteName = "icon_mau";
			ChiSo1TruocCaoCap.text = thanthu.GetHP().ToString();
			ChiSo1TangCaoCap.text = "[00FF00]~" + (petInfo2.GetHP() - thanthu.GetHP());
		}
		else if (thanthu.GetChiSo1() == ChiSoCoBan.Ngoai)
		{
			ChiSo1IconTruocThuong.spriteName = "icon_cong";
			ChiSo1TruocThuong.text = thanthu.GetCong().ToString();
			ChiSo1TangThuong.text = "[00FF00]~" + (petInfo.GetCong() - thanthu.GetCong());
			ChiSo1IconTruocCaoCap.spriteName = "icon_cong";
			ChiSo1TruocCaoCap.text = thanthu.GetCong().ToString();
			ChiSo1TangCaoCap.text = "[00FF00]~" + (petInfo2.GetCong() - thanthu.GetCong());
		}
		else
		{
			ChiSo1IconTruocThuong.spriteName = "icon_thu";
			ChiSo1TruocThuong.text = thanthu.GetThu().ToString();
			ChiSo1TangThuong.text = "[00FF00]~" + (petInfo.GetThu() - thanthu.GetThu());
			ChiSo1IconTruocCaoCap.spriteName = "icon_thu";
			ChiSo1TruocCaoCap.text = thanthu.GetThu().ToString();
			ChiSo1TangCaoCap.text = "[00FF00]~" + (petInfo2.GetThu() - thanthu.GetThu());
		}
		if (thanthu.GetChiSo2() == ChiSoCoBan.Menh)
		{
			ChiSo2IconTruocThuong.spriteName = "icon_mau";
			ChiSo2TruocThuong.text = thanthu.GetHP().ToString();
			ChiSo2TangThuong.text = "[00FF00]~" + (petInfo.GetHP() - thanthu.GetHP());
			ChiSo2IconTruocCaoCap.spriteName = "icon_mau";
			ChiSo2TruocCaoCap.text = thanthu.GetHP().ToString();
			ChiSo2TangCaoCap.text = "[00FF00]~" + (petInfo2.GetHP() - thanthu.GetHP());
		}
		else if (thanthu.GetChiSo2() == ChiSoCoBan.ThanPhap)
		{
			ChiSo2IconTruocThuong.spriteName = "icon_thu";
			ChiSo2TruocThuong.text = thanthu.GetThu().ToString();
			ChiSo2TangThuong.text = "[00FF00]~" + (petInfo.GetThu() - thanthu.GetThu());
			ChiSo2IconTruocCaoCap.spriteName = "icon_thu";
			ChiSo2TruocCaoCap.text = thanthu.GetThu().ToString();
			ChiSo2TangCaoCap.text = "[00FF00]~" + (petInfo2.GetThu() - thanthu.GetThu());
		}
		else
		{
			ChiSo2IconTruocThuong.spriteName = "icon_noi";
			ChiSo2TruocThuong.text = thanthu.GetMP().ToString();
			ChiSo2TangThuong.text = "[00FF00]~" + (petInfo.GetMP() - thanthu.GetMP());
			ChiSo2IconTruocCaoCap.spriteName = "icon_noi";
			ChiSo2TruocCaoCap.text = thanthu.GetMP().ToString();
			ChiSo2TangCaoCap.text = "[00FF00]~" + (petInfo2.GetMP() - thanthu.GetMP());
		}
		if (thanthuThonPhe != null)
		{
			thanthuHiSinh1.Set(thanthuThonPhe);
		}
		else
		{
			ReleaseAllSlot();
		}
	}

	public void OnBackBtn()
	{
		GUIManager.instance.SetScreen(GUIManager.instance.LastScreen);
	}

	public void OnSelectThanThu(GameObject button)
	{
		if (!effectTime)
		{
			List<int> list = new List<int>();
			list.Add(thanthu.ID);
			PopupSelectThanThu.CreateByNormal(SetThanThuSlot, list);
		}
	}

	public void ReleaseAllSlot()
	{
		thanthuHiSinh1.Release();
	}

	public bool SetThanThuSlot(int id)
	{
		Set(thanthu.ID, GameManager.instance.m_GameClient.UserInfo.ListThanThu.Find((UserInfo.PetInfo p) => p.ID == id));
		selectedThanthu = id;
		return true;
	}

	public void OnThonPheButton()
	{
		if (!effectTime)
		{
			isCaoCap = false;
			GameManager.instance.m_GameClient.RequestThonPheThanThu(thanthu.ID, selectedThanthu, false);
		}
	}

	public void OnThonPheCaoCapButton()
	{
		if (!effectTime)
		{
			isCaoCap = true;
			GameManager.instance.m_GameClient.RequestThonPheThanThu(thanthu.ID, selectedThanthu, true);
		}
	}

	public void btnBack_OnClick()
	{
		GUIManager.instance.SetScreen(GUIManager.instance.LastScreen);
	}
}
