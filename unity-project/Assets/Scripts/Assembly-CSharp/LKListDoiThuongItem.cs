using UnityEngine;

public class LKListDoiThuongItem : MonoBehaviour
{
	public OtherAvatar rewardAvatar;

	public UILabel rewardDesc;

	public UILabel costDesc;

	public UILabel buttonLabel;

	private int code;

	private bool isLinhThuong;

	public void SetItem(string avatar_code, string phanThuong, string dieuKien, bool isLinhThuong, int codeDoiThuong, bool isActive)
	{
		rewardAvatar.Set(avatar_code);
		costDesc.text = dieuKien;
		rewardDesc.text = phanThuong;
		if (isLinhThuong)
		{
			buttonLabel.text = Localization.instance.Get("LuanKiemLinhThuongLabel");
		}
		else
		{
			buttonLabel.text = Localization.instance.Get("LuanKiemDoiThuongLabel");
		}
		buttonLabel.transform.parent.GetComponent<Collider>().enabled = isActive;
		code = codeDoiThuong;
		this.isLinhThuong = isLinhThuong;
	}

	private void OnDoiThuongBtnClick()
	{
		if (!isLinhThuong)
		{
			GameManager.instance.m_GameClient.DoiThuongLuanKiem((DoiThuongEnum)code);
		}
		else
		{
			GameManager.instance.m_GameClient.NhanThuongLuanKiem((UserInfo.LuanKiemData.RewardMask)code);
		}
	}
}
