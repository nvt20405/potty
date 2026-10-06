using UnityEngine;

public class MainMenuItem : MonoBehaviour
{
	public UILabel lbTitle;

	public string strURL { get; set; }

	public string strName { get; set; }

	public int MainMenuID { get; set; }

	public int SubMenuID { get; set; }

	public void setTitle(string strTitle)
	{
		lbTitle.text = strTitle;
		strName = strTitle;
	}
}
