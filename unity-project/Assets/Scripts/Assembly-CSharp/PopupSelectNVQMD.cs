using UnityEngine;

public class PopupSelectNVQMD : MonoBehaviour
{
	public QMDSelectDeTuItem Select1;

	public QMDSelectDeTuItem Select2;

	public QMDSelectDeTuItem Select3;

	public static PopupSelectNVQMD instance;

	public int m_iSelect = 1;

	public static PopupSelectNVQMD Create(QMDInfo info)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupSelectNVQMD"))).GetComponent<PopupSelectNVQMD>();
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
		Select1.Set(info.NextChoice1, info.NVLevel, info.Choice1ChiSo.Menh, info.Choice1ChiSo.Ngoai, info.Choice1ChiSo.Than, info.Choice1ChiSo.Khi);
		Select2.Set(info.NextChoice2, info.NVLevel, info.Choice2ChiSo.Menh, info.Choice2ChiSo.Ngoai, info.Choice2ChiSo.Than, info.Choice2ChiSo.Khi);
		Select3.Set(info.NextChoice3, info.NVLevel, info.Choice3ChiSo.Menh, info.Choice3ChiSo.Ngoai, info.Choice3ChiSo.Than, info.Choice3ChiSo.Khi);
	}
}
