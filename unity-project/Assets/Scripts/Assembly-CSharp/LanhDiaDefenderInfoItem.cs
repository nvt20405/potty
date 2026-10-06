using UnityEngine;

public class LanhDiaDefenderInfoItem : MonoBehaviour
{
	public UILabel DisplayName;

	public UISprite Vip;

	public UILabel Level;

	public int SID;

	public int GID;

	public void Set(string displayName, int vip, int level, int sid, int gid)
	{
		SID = sid;
		GID = gid;
		DisplayName.text = displayName;
		Vip.spriteName = "icon_vip" + vip;
		Level.text = "Lv." + level;
	}

	public void GetInfo()
	{
		GameManager.instance.m_GameClient.RequestGetThongTinLienServer(SID, GID);
	}
}
