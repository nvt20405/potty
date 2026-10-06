using UnityEngine;

public class VoCongVoLamPhoItem : MonoBehaviour
{
	public enum VoCongVoLamPhoType
	{
		TYPE_AVAI = 0,
		TYPE_INVI = 1,
		TYPE_NONE = 2
	}

	public OtherAvatar voCongAvatar;

	public UILabel lbName;

	public CfgVoCong m_Data;

	public void setData(CfgVoCong data, VoCongVoLamPhoType type)
	{
		if (type == VoCongVoLamPhoType.TYPE_NONE)
		{
			NGUITools.SetActive(lbName.gameObject, false);
			voCongAvatar.Set("empty");
			data = null;
		}
		else if (data != null)
		{
			m_Data = data;
			voCongAvatar.Set(m_Data.Name);
			NGUITools.SetActive(lbName.gameObject, true);
			lbName.text = m_Data.TenHienThi;
			if (type == VoCongVoLamPhoType.TYPE_INVI)
			{
				Color color = default(Color);
				color = new Color(0.4f, 0.4f, 0.4f);
				voCongAvatar.avatar.color = color;
				voCongAvatar.avatarBkg.color = color;
				voCongAvatar.bkg.color = color;
			}
		}
	}
}
