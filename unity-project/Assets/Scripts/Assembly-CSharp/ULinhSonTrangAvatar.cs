using UnityEngine;

public class ULinhSonTrangAvatar : MonoBehaviour
{
	public OtherAvatar otherAvatar;

	public NhanVatAvatar nhanVatAvatar;

	public UILabel nameLabel;

	public UISprite spNameBkg;

	private void TurnOnNhanVatAvatar()
	{
		NGUITools.SetActive(nhanVatAvatar.gameObject, true);
		NGUITools.SetActive(otherAvatar.gameObject, false);
	}

	private void TurnOnOtherAvatar()
	{
		NGUITools.SetActive(nhanVatAvatar.gameObject, false);
		NGUITools.SetActive(otherAvatar.gameObject, true);
	}

	public void Set(PhanThuongResponse.PhanThuong pt)
	{
	}
}
