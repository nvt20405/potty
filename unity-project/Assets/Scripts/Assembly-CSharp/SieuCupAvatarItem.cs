using UnityEngine;

public class SieuCupAvatarItem : MonoBehaviour
{
	public int gid;

	public int sid;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void OnViewDoiHinh()
	{
		if (sid != 0)
		{
			PopupYesNo.Create(Localization.instance.Get("ThongTinXemDoiHinhLienServer"), Localization.instance.Get("BattleDongYBtn"), Localization.instance.Get("Cancel"), ConfirmView, null);
		}
	}

	public void ConfirmView()
	{
		GameManager.instance.m_GameClient.RequestGetThongTinLienServer(sid, gid);
	}
}
