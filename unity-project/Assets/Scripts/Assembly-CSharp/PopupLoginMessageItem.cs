using System.Collections;
using UnityEngine;

public class PopupLoginMessageItem : MonoBehaviour
{
	public UILabel TitLabel;

	public UILabel gachImgLabel;

	public UILabel DescLabel;

	public UISprite Bkg;

	public UITexture WebImg;

	private void Start()
	{
	}

	public int Set(UserInfo.ServerData.LoginMessage msg)
	{
		TitLabel.text = msg.Tit;
		DescLabel.text = msg.Desc;
		WebImg.color = new Color(1f, 1f, 1f, 0f);
		int num = 140;
		if (msg.Img == string.Empty)
		{
			gachImgLabel.text = string.Empty;
			DescLabel.transform.localPosition = new Vector3(-271f, -50f, 0f);
			num -= 67;
		}
		else
		{
			StartCoroutine(LoadingWebImage(msg.Img));
			DescLabel.transform.localPosition = new Vector3(-271f, -117f, 0f);
		}
		string processedText = DescLabel.processedText;
		char[] array = processedText.ToCharArray();
		int num2 = 1;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == '\n')
			{
				num2++;
			}
		}
		int num3 = num + 32 * num2;
		Vector3 localScale = Bkg.transform.localScale;
		Bkg.transform.localScale = new Vector3(localScale.x, num3, 1f);
		return num3;
	}

	private IEnumerator LoadingWebImage(string url)
	{
		WWW www = new WWW(url);
		yield return www;
		WebImg.mainTexture = www.texture;
		WebImg.color = new Color(1f, 1f, 1f, 1f);
	}

	private void Update()
	{
	}
}
