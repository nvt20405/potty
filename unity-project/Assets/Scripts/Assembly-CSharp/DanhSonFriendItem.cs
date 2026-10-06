using System;
using UnityEngine;

public class DanhSonFriendItem : MonoBehaviour
{
	public UILabel nameLabel;

	public UICheckbox cb;

	public NhanVatAvatar nv1;

	public NhanVatAvatar nv2;

	public Action<bool, DanhSonFriendItem> OnSelectFriend;

	public UILabel daTroGiupLabel;

	public int FriendID { get; set; }

	private void Awake()
	{
		cb = GetComponent<UICheckbox>();
		cb.onStateChange = OnActivateChange;
	}

	private void OnActivateChange(bool isActive)
	{
		if (OnSelectFriend != null)
		{
			OnSelectFriend(isActive, this);
		}
	}

	public void SetInfo(int gid, string nameFriend, string nv1Name, string nv2Name, int level1, int level2, bool daTroGiup)
	{
		nameLabel.text = nameFriend;
		nv1.Set(nv1Name, 0, level1);
		nv2.Set(nv2Name, 0, level2);
		FriendID = gid;
		daTroGiupLabel.gameObject.SetActive(daTroGiup);
	}
}
