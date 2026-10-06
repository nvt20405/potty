public class ScreenBangChien : ScreenBase
{
	public ThanhChienBtn[] listThanhChien;

	public BangChienResponse _response;

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(6);
		GameManager.instance.m_GameClient.RequestBangChienGetInfo();
	}

	public override void OnDeactive()
	{
		base.OnDeactive();
	}

	private void OnBackBtnClick()
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenHoatDongLienMinh);
	}

	public void SyncWithNetWorkData(BangChienResponse response)
	{
		_response = response;
		ThanhChienBtn[] array = listThanhChien;
		ThanhChienBtn[] array2 = array;
		foreach (ThanhChienBtn thanhChienBtn in array2)
		{
			bool flag = false;
			for (int j = 0; j < response.ListThanh.Count; j++)
			{
				if (thanhChienBtn.ThanhIdx == response.ListThanh[j].ThanhIdx)
				{
					string tenBang = ((response.ListThanh[j].LienMinhServerID <= 0) ? response.ListThanh[j].LMName : string.Format("s{0}.{1}", response.ListThanh[j].LienMinhServerID, response.ListThanh[j].LMName));
					thanhChienBtn.SetInfo(tenBang, response.ListThanh[j].BangChu);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				thanhChienBtn.SetInfo("Inactive", string.Empty);
			}
		}
	}
}
