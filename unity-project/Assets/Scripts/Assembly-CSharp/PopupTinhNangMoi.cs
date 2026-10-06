using UnityEngine;

public class PopupTinhNangMoi : MonoBehaviour
{
	public GameObject group;

	public UILabel titleLabel;

	public UILabel descLabel;

	public GameObject[] LvlGroup;

	public GameObject[] GiangHoGroup;

	public GameObject[] CamDiaGroup;

	public static void CreateByLevel(int level)
	{
		Object obj = Object.Instantiate(Resources.Load("popup/PopupTinhNangMoi"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		PopupTinhNangMoi component = gameObject.GetComponent<PopupTinhNangMoi>();
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		if (component.LvlGroup.Length <= level || component.LvlGroup[level] == null)
		{
			component.DestroyPopup();
			return;
		}
		NGUITools.SetActive(component.LvlGroup[level], true);
		component.titleLabel.text = Localization.instance.Get(string.Format("PopupTinhNangLV{0}", level));
		component.descLabel.text = Localization.instance.Get(string.Format("PopupTinhNangLV{0}_desc", level));
	}

	public static void CreateByGiangHo(int giang_ho)
	{
		Object obj = Object.Instantiate(Resources.Load("popup/PopupTinhNangMoi"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		PopupTinhNangMoi component = gameObject.GetComponent<PopupTinhNangMoi>();
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		if (component.GiangHoGroup.Length <= giang_ho || component.GiangHoGroup[giang_ho] == null)
		{
			component.DestroyPopup();
			return;
		}
		NGUITools.SetActive(component.GiangHoGroup[giang_ho], true);
		component.titleLabel.text = Localization.instance.Get(string.Format("PopupTinhNangGH{0}", giang_ho));
		component.descLabel.text = Localization.instance.Get(string.Format("PopupTinhNangGH{0}_desc", giang_ho));
	}

	public static void CreateByCamDia(int cam_dia)
	{
		Object obj = Object.Instantiate(Resources.Load("popup/PopupTinhNangMoi"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		PopupTinhNangMoi component = gameObject.GetComponent<PopupTinhNangMoi>();
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		if (component.CamDiaGroup.Length <= cam_dia || component.CamDiaGroup[cam_dia] == null)
		{
			component.DestroyPopup();
			return;
		}
		NGUITools.SetActive(component.CamDiaGroup[cam_dia], true);
		component.titleLabel.text = Localization.instance.Get(string.Format("PopupTinhNangCD{0}", cam_dia));
		component.descLabel.text = Localization.instance.Get(string.Format("PopupTinhNangCD{0}_desc", cam_dia));
	}

	public void Update()
	{
		if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBattle)
		{
			NGUITools.SetActive(group, false);
		}
		else
		{
			NGUITools.SetActive(group, true);
		}
	}

	public void OnCloseClick()
	{
		if (TutorialPopup.instance != null)
		{
			TutorialPopup.instance.ShowNextTutorial();
		}
		DestroyPopup();
	}

	public void DestroyPopup()
	{
		Object.Destroy(base.gameObject);
	}
}
