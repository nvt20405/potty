using UnityEngine;

public class DoiDiemThuongItem : MonoBehaviour
{
	public PhanThuongItem phanThuongItem;

	public UILabel lbDiemCan;

	public UILabel lbMaxCount;

	public UILabel lbCount;

	public UIButton btnDoi;

	public int mIndex;

	public UserInfo.ServerData.ULinhSonTrangCfg.DoiThuongItem m_Data;

	public void setData(UserInfo.ServerData.ULinhSonTrangCfg.DoiThuongItem data, int count, int index)
	{
		if (data == null)
		{
			return;
		}
		m_Data = data;
		mIndex = index;
		phanThuongItem.Set(m_Data.PhanThuong, false);
		lbDiemCan.text = "[-]" + Localization.instance.Get("DiemCanLabel") + ": [ff0000]" + m_Data.Diem;
		if (m_Data.MaxCount >= 9999)
		{
			lbMaxCount.text = string.Empty;
		}
		else
		{
			lbMaxCount.text = string.Format(Localization.instance.Get("SoLanDoiThuongToiDaLabel"), count + "/" + m_Data.MaxCount);
		}
		if (m_Data.PhanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.THU_CUOI)
		{
			if (ConfigManager.instance.OtherConfig.ThuCuoiConfig != null && ConfigManager.instance.OtherConfig.ThuCuoiConfig.ContainsKey(m_Data.PhanThuong.Name))
			{
				lbCount.text = string.Format(Localization.instance.Get("TimeSuDungNguaLabel"), m_Data.PhanThuong.Count);
			}
		}
		else
		{
			lbCount.text = "X" + m_Data.PhanThuong.Count;
		}
	}
}
