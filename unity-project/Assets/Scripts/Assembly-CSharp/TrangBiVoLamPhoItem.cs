using UnityEngine;

public class TrangBiVoLamPhoItem : MonoBehaviour
{
	public enum TrangBiVoLamPhoType
	{
		TYPE_AVAI = 0,
		TYPE_INVI = 1,
		TYPE_NONE = 2
	}

	public OtherAvatar trangBiAvatar;

	public UILabel lbName;

	public TrangBiCfg m_Data;

	public void setData(TrangBiCfg data, TrangBiVoLamPhoType type)
	{
		if (type == TrangBiVoLamPhoType.TYPE_NONE)
		{
			NGUITools.SetActive(lbName.gameObject, false);
			trangBiAvatar.Set("empty");
			data = null;
		}
		else if (data != null)
		{
			m_Data = data;
			trangBiAvatar.Set(m_Data.Name);
			lbName.text = m_Data.TenHienThi;
			NGUITools.SetActive(lbName.gameObject, true);
			lbName.text = m_Data.TenHienThi;
			if (type == TrangBiVoLamPhoType.TYPE_INVI)
			{
				Color color = default(Color);
				color = new Color(0.4f, 0.4f, 0.4f);
				trangBiAvatar.avatar.color = color;
				trangBiAvatar.avatarBkg.color = color;
				trangBiAvatar.bkg.color = color;
			}
		}
	}
}
