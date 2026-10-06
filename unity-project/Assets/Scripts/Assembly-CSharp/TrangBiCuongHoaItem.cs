using UnityEngine;

public class TrangBiCuongHoaItem : MonoBehaviour
{
	public NhanVatAvatar tanHonAvatar;

	public UserInfo.HonNhanVatData m_honNhanVatData;

	public void setData(UserInfo.HonNhanVatData data, int quantity)
	{
		if (data != null)
		{
			m_honNhanVatData = data;
			tanHonAvatar.Set(data.Name, 0, -1, false, quantity, true);
			tanHonAvatar.currentQuantity = quantity;
		}
	}
}
