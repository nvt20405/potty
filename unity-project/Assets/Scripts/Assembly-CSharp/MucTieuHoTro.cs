using UnityEngine;

public class MucTieuHoTro : MonoBehaviour
{
	public NhanVatAvatar avatar;

	public UILabel label;

	public int HID { get; private set; }

	public void SetInfo(int hid, string codeName, int level)
	{
		HID = hid;
		avatar.Set(codeName, -1, level);
		NhanVatCfg value = null;
		if (ConfigManager.instance.m_dicNhanVats.TryGetValue(codeName, out value))
		{
			label.gameObject.SetActive(true);
			label.text = value.TenHienThi;
		}
		else
		{
			label.gameObject.SetActive(false);
		}
	}
}
