using UnityEngine;

public class DeTuVoLamPhoItem : MonoBehaviour
{
	public enum DeTuVoLamPhoType
	{
		TYPE_AVAI = 0,
		TYPE_INVI = 1,
		TYPE_NONE = 2
	}

	public NhanVatAvatar DeTuAvatar;

	public UILabel lbName;

	public NhanVatCfg m_Data;

	public void setData(NhanVatCfg data, DeTuVoLamPhoType type)
	{
		if (type == DeTuVoLamPhoType.TYPE_NONE)
		{
			NGUITools.SetActive(lbName.gameObject, false);
			DeTuAvatar.Set("empty");
			data = null;
		}
		else if (data != null)
		{
			m_Data = data;
			DeTuAvatar.Set(m_Data.Name);
			lbName.text = m_Data.TenHienThi;
			NGUITools.SetActive(lbName.gameObject, true);
			lbName.text = m_Data.TenHienThi;
			if (type == DeTuVoLamPhoType.TYPE_INVI)
			{
				Color color = default(Color);
				color = new Color(0.4f, 0.4f, 0.4f);
				DeTuAvatar.avatar.color = color;
				DeTuAvatar.avatarBkg.color = color;
				DeTuAvatar.bkg.color = color;
			}
		}
	}
}
