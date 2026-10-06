using UnityEngine;

public class PopupSelectVoCongQMD : MonoBehaviour
{
	public PopupSelectVoCongItem Select1;

	public PopupSelectVoCongItem Select2;

	public PopupSelectVoCongItem Select3;

	public static PopupSelectVoCongQMD instance;

	public int m_iSelect = 1;

	public static PopupSelectVoCongQMD Create(QMDInfo info)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupSelectVoCongQMD"))).GetComponent<PopupSelectVoCongQMD>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.Set(info);
		return instance;
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			instance.gameObject.SetActive(false);
			Object.Destroy(instance.gameObject);
		}
		instance = null;
	}

	public void OnSelect1(bool active)
	{
		if (active)
		{
			m_iSelect = 1;
		}
	}

	public void OnSelect2(bool active)
	{
		if (active)
		{
			m_iSelect = 2;
		}
	}

	public void OnSelect3(bool active)
	{
		if (active)
		{
			m_iSelect = 3;
		}
	}

	public void OnCancelClick()
	{
		DestroyPopup();
	}

	public void OnOkClick()
	{
		ScreenQuangMinhDinh screenQuangMinhDinh = GUIManager.getScreen(GAME_SCREEN.ScreenQuangMinhDinh) as ScreenQuangMinhDinh;
		screenQuangMinhDinh.OnXongPhaSelect(m_iSelect);
	}

	public void Set(QMDInfo info)
	{
		UserInfo.VoCongData voCongData = new UserInfo.VoCongData();
		voCongData.Name = info.NextChoice1;
		voCongData.Level = info.SkillLevel;
		Select1.Set(voCongData);
		UserInfo.VoCongData voCongData2 = new UserInfo.VoCongData();
		voCongData2.Name = info.NextChoice2;
		voCongData2.Level = info.SkillLevel;
		Select2.Set(voCongData2);
		UserInfo.VoCongData voCongData3 = new UserInfo.VoCongData();
		voCongData3.Name = info.NextChoice3;
		voCongData3.Level = info.SkillLevel;
		Select3.Set(voCongData3);
	}
}
