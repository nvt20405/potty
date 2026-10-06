using System.Text;
using UnityEngine;

public class DongNhanPanel : MonoBehaviour
{
	public UILabel nameLabel;

	public UISlider mauSlider;

	public UILabel topLabel;

	public UILabel topDamLabel;

	public UISprite topBgr;

	public GameObject btnExpand;

	private int topCount = 3;

	private DongNhanResponse response;

	public void OnExpandTopClick()
	{
		if (topCount == 3)
		{
			topCount = 10;
			btnExpand.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
		}
		else
		{
			topCount = 3;
			btnExpand.transform.localRotation = Quaternion.Euler(0f, 0f, 270f);
		}
		SetTop(topCount);
	}

	private void SetTop(int num)
	{
		StringBuilder stringBuilder = new StringBuilder();
		StringBuilder stringBuilder2 = new StringBuilder();
		int num2 = -1;
		bool flag = false;
		if (response != null && response.Top10 != null)
		{
			for (int i = 0; i < response.Top10.Count; i++)
			{
				if (response.Top10[i].Gid == GameManager.instance.GamerID)
				{
					num2 = i;
				}
				if (i < num)
				{
					if (num2 >= 0)
					{
						flag = true;
					}
					string value = string.Format("[ffff00]{0}.[-] {1}", i + 1, response.Top10[i].Name);
					if (i > 0)
					{
						stringBuilder.AppendLine();
						stringBuilder2.AppendLine();
					}
					stringBuilder.Append(value);
					stringBuilder2.Append(response.Top10[i].TotalThuongTon.ToString());
				}
			}
		}
		if (!flag && GameManager.instance != null && GameManager.instance.m_GameClient != null && GameManager.instance.m_GameClient.UserInfo != null && GameManager.instance.m_GameClient.UserInfo.Gamer != null)
		{
			string value2 = string.Format("[ffff00]{0}.[-] {1}", (num2 >= 0) ? (num2 + 1).ToString() : "...", GameManager.instance.m_GameClient.UserInfo.Gamer.DisplayName);
			if (stringBuilder.Length > 0)
			{
				stringBuilder.AppendLine();
			}
			stringBuilder.Append(value2);
			if (stringBuilder2.Length > 0)
			{
				stringBuilder2.AppendLine();
			}
			stringBuilder2.Append(response.TotalThuongTon.ToString());
		}
		topLabel.text = stringBuilder.ToString();
		topDamLabel.text = stringBuilder2.ToString();
		float num3 = topDamLabel.relativeSize.y * topDamLabel.transform.localScale.y;
		topBgr.transform.localScale = new Vector3(topBgr.transform.localScale.x, 28f + num3 + 32f, 1f);
		btnExpand.transform.localPosition = new Vector3(btnExpand.transform.localPosition.x, -28f - num3 - 10f, 0f);
	}

	public void SetInfo(DongNhanResponse response)
	{
		this.response = response;
		nameLabel.text = ConfigManager.instance.m_dicNhanVats["NC_DONG_NHAN_COC"].TenHienThi;
		if (response.MauDongNhanOrig == 0)
		{
			EGDebug.Log("Loi mau dong nhan goc = 0");
			mauSlider.sliderValue = 1f;
		}
		else
		{
			mauSlider.sliderValue = (float)response.MauDongNhan / (float)response.MauDongNhanOrig;
		}
		SetTop(topCount);
	}
}
