public class ScreenCheTacHuyenKhi : ScreenBase
{
	public OtherAvatar VCNguyenLieuAvatar;

	public OtherAvatar huyenThanLenhAvatar;

	private int m_voCongSelect;

	public void OnBack_Click()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHuyenKhiMain);
	}

	public override void OnActive()
	{
		base.OnActive();
		m_voCongSelect = 0;
		VCNguyenLieuAvatar.Set("empty");
		if (GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList != null)
		{
			UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_HUYEN_THAN_LENH");
			if (vatPhamTieuThuData != null)
			{
				huyenThanLenhAvatar.Set(vatPhamTieuThuData);
				return;
			}
			vatPhamTieuThuData = new UserInfo.VatPhamTieuThuData();
			vatPhamTieuThuData.Quantity = 0;
			vatPhamTieuThuData.Name = "VP_HUYEN_THAN_LENH";
			huyenThanLenhAvatar.Set(vatPhamTieuThuData);
		}
	}

	public void OnBtnCheTac()
	{
		if (m_voCongSelect > 0)
		{
			GameManager.instance.m_GameClient.RequestCheTaoHuyenKhi(m_voCongSelect);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("ChuaChonVoCongCheTac"));
		}
	}

	public void OnAvatarNeedClick()
	{
		PopupSelectVoCong.CreateBy_S(OnSelectVoCong);
	}

	public bool OnSelectVoCong(int id)
	{
		if (GameManager.instance.m_GameClient.UserInfo.VoCongList != null)
		{
			UserInfo.VoCongData voCongData = GameManager.instance.m_GameClient.UserInfo.VoCongList.Find((UserInfo.VoCongData e) => e.ID == id);
			if (voCongData != null)
			{
				VCNguyenLieuAvatar.Set(voCongData);
			}
		}
		m_voCongSelect = id;
		return true;
	}
}
