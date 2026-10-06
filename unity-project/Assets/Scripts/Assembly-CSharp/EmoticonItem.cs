using UnityEngine;

public class EmoticonItem : MonoBehaviour
{
	public UILabel lbEmoticon;

	public float wItem = 50f;

	public float hItem = 50f;

	public string strData = string.Empty;

	public void setString(string strEmo)
	{
		strData = strEmo;
		lbEmoticon.text = strEmo;
	}
}
