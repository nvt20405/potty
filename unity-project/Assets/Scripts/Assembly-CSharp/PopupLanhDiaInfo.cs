using System;
using UnityEngine;

public class PopupLanhDiaInfo : MonoBehaviour
{
	public static PopupLanhDiaInfo instance;

	public UILabel SquareStatus;

	private string CurPosition;

	public UISprite bg;

	public LanhDiaDefenderInfoItem BaseInfoItem;

	public GameObject BasicInfo;

	public float InfoItemSize;

	public UILabel NextMoveTime;

	public UILabel TheLucLabel;

	public GameObject RootItem;

	public static void Create(string position, string display)
	{
		if (instance == null)
		{
			instance = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Popup/PopupLanhDiaInfo"))).GetComponent<PopupLanhDiaInfo>();
		}
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = Vector3.one;
		instance.Set(position, display);
	}

	public void Sync()
	{
		Set(CurPosition, string.Empty);
	}

	private void Set(string position, string display = "")
	{
		CurPosition = position;
		foreach (Transform item in RootItem.transform)
		{
			Transform transform2 = item;
			if (transform2 != BaseInfoItem.transform && transform2 != BasicInfo.transform)
			{
				UnityEngine.Object.Destroy(transform2.gameObject);
			}
		}
		BasicInfo.SetActive(true);
		if (!position.Equals("0,0"))
		{
			if (!string.IsNullOrEmpty(display))
			{
				SquareStatus.text = display;
			}
			UserInfo.LanhDiaSquare lanhDiaSquare = GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map.Find((UserInfo.LanhDiaSquare s) => s.Position.Equals(position));
			if (lanhDiaSquare.Defender == null || lanhDiaSquare.Defender.Count == 0)
			{
				bg.transform.localScale = new Vector3(bg.transform.localScale.x, 176f, 1f);
			}
			else
			{
				bg.transform.localScale = new Vector3(bg.transform.localScale.x, 312f, 1f);
				int num = 0;
				BaseInfoItem.gameObject.SetActive(true);
				foreach (UserInfo.LanhDiaAvatar item2 in lanhDiaSquare.Defender)
				{
					LanhDiaDefenderInfoItem lanhDiaDefenderInfoItem = UnityEngine.Object.Instantiate(BaseInfoItem) as LanhDiaDefenderInfoItem;
					lanhDiaDefenderInfoItem.transform.parent = RootItem.transform;
					lanhDiaDefenderInfoItem.transform.localScale = Vector3.one;
					lanhDiaDefenderInfoItem.transform.localPosition = BaseInfoItem.transform.localPosition + (float)num * InfoItemSize * Vector3.down;
					lanhDiaDefenderInfoItem.Set(item2.DisplayName, item2.Vip, item2.Level, item2.SID, item2.GID);
					num++;
				}
				BaseInfoItem.gameObject.SetActive(false);
			}
		}
		else
		{
			SquareStatus.text = Localization.instance.Get("KhuVucAnToan");
			bg.transform.localScale = new Vector3(bg.transform.localScale.x, 176f, 1f);
		}
		BasicInfo.SetActive(true);
		TheLucLabel.text = string.Format(Localization.instance.Get("LanhDiaTheLuc"), GameManager.instance.m_GameClient.UserInfo.LanhDiaUser.Turn.ToString());
	}

	public void OnMoveConfirm()
	{
		GameManager.instance.m_GameClient.RequestMoveLanhDia(CurPosition);
	}

	private void Update()
	{
		if ((GameManager.instance.m_GameClient.ServerTime - GameManager.instance.m_GameClient.UserInfo.LanhDiaUser.LastMoveTime).TotalMinutes < 5.0)
		{
			TimeSpan timeSpan = GameManager.instance.m_GameClient.UserInfo.LanhDiaUser.LastMoveTime + new TimeSpan(0, 5, 0) - GameManager.instance.m_GameClient.ServerTime;
			NextMoveTime.text = string.Format(Localization.instance.Get("CoTheMoveSau"), (int)timeSpan.TotalMinutes, timeSpan.Seconds);
		}
		else
		{
			NextMoveTime.text = Localization.instance.Get("SanSangXuatQuan");
		}
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			UnityEngine.Object.Destroy(instance.gameObject);
			instance = null;
		}
	}
}
