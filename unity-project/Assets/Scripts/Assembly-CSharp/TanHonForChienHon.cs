using UnityEngine;

public class TanHonForChienHon : MonoBehaviour
{
	public NhanVatAvatar avatar;

	private UserInfo.HonNhanVatData honnv_;

	public UISprite avatarSpr;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Set(UserInfo.HonNhanVatData honnv)
	{
		honnv_ = new UserInfo.HonNhanVatData();
		honnv_.GID = honnv.GID;
		honnv_.ID = honnv.ID;
		honnv_.Name = honnv.Name;
		honnv_.Quantity = honnv.Quantity;
		avatar.Set(honnv_, false, honnv_.Quantity);
	}

	public void TanHonAvatar_OnClick()
	{
		avatarSpr.alpha = 0.7f;
		if (honnv_.Quantity > 0)
		{
			ScreenThangCapChienHon screenThangCapChienHon = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenThangCapChienHon) as ScreenThangCapChienHon;
			screenThangCapChienHon.TanHonAvatar_OnClick(honnv_.ID, honnv_.Name);
			honnv_.Quantity--;
			avatar.countLabel.text = honnv_.Quantity.ToString();
		}
	}
}
