public class ScreenMyListBaoKho : ScreenBase
{
	public MyBaoKhoItem item1;

	public MyBaoKhoItem item2;

	public override void OnActive()
	{
		base.OnActive();
		displayMyBaoKhoList();
	}

	public void displayMyBaoKhoList()
	{
		item1.gameObject.SetActive(false);
		item2.gameObject.SetActive(false);
		if (GameManager.instance.m_GameClient.BaoKhoInfoResponse != null && GameManager.instance.m_GameClient.BaoKhoInfoResponse.listMyBaoKho != null)
		{
			if (GameManager.instance.m_GameClient.BaoKhoInfoResponse.listMyBaoKho.Count > 0)
			{
				item1.gameObject.SetActive(true);
				item1.setData(GameManager.instance.m_GameClient.BaoKhoInfoResponse.listMyBaoKho[0]);
			}
			if (GameManager.instance.m_GameClient.BaoKhoInfoResponse.listMyBaoKho.Count > 1)
			{
				item2.gameObject.SetActive(true);
				item2.setData(GameManager.instance.m_GameClient.BaoKhoInfoResponse.listMyBaoKho[1]);
			}
		}
	}

	public void HoangTrieuBaoKhoOnClick()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenBaoKhoMain);
	}
}
