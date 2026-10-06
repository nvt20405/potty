using System;
using UnityEngine;

public class MucTieuBaoVe : MonoBehaviour
{
	public NhanVatAvatar avatar;

	public UILabel nameLabel;

	public UISprite selectedMark;

	private bool _isSelected;

	public Action<bool, GameObject> OnStateChange;

	public bool IsSelected
	{
		get
		{
			return _isSelected;
		}
		private set
		{
			_isSelected = value;
		}
	}

	public string CodeName { get; private set; }

	public int HID { get; private set; }

	private void Awake()
	{
		avatar.OnEventClick = OnNhatVatAvatarClick;
		selectedMark.gameObject.SetActive(_isSelected);
	}

	public void BeSelected(bool isSelected)
	{
		_isSelected = isSelected;
		avatar.IsSelected = isSelected;
	}

	private void OnNhatVatAvatarClick(NhanVatAvatar ava)
	{
		if (_isSelected)
		{
			return;
		}
		BeSelected(true);
		if (OnStateChange != null)
		{
			OnStateChange(true, base.gameObject);
		}
		Transform parent = base.transform.parent;
		MucTieuBaoVe[] componentsInChildren = parent.GetComponentsInChildren<MucTieuBaoVe>();
		MucTieuBaoVe[] array = componentsInChildren;
		MucTieuBaoVe[] array2 = array;
		foreach (MucTieuBaoVe mucTieuBaoVe in array2)
		{
			if (mucTieuBaoVe != this)
			{
				mucTieuBaoVe.BeSelected(false);
				if (mucTieuBaoVe.OnStateChange != null)
				{
					mucTieuBaoVe.OnStateChange(false, base.gameObject);
				}
			}
		}
	}

	public void SetInfo(int hid, string codeName, int level)
	{
		HID = hid;
		avatar.Set(codeName, 0, level);
		CodeName = codeName;
	}
}
