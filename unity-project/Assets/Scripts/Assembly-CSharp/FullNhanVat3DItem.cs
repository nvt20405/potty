using UnityEngine;

public class FullNhanVat3DItem : MonoBehaviour
{
	public GameObject NhanVat3DObj;

	private Avatar3D avatar3DNV;

	private string codeNameNV = string.Empty;

	public void setData(string codeName)
	{
		codeNameNV = codeName;
		if (!string.IsNullOrEmpty(codeNameNV))
		{
			NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[codeNameNV];
			if (avatar3DNV != null)
			{
				Object.Destroy(avatar3DNV.gameObject);
			}
			avatar3DNV = GUIManager.instance.InstantiateAvatar3DWithBattleAnim(nhanVatCfg.Name, nhanVatCfg.VuKhiMacDinh, string.Empty, string.Empty, string.Empty);
			if (avatar3DNV != null)
			{
				avatar3DNV.transform.parent = NhanVat3DObj.transform;
				avatar3DNV.transform.localPosition = Vector3.zero;
				avatar3DNV.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
				avatar3DNV.transform.localScale = Vector3.one;
				avatar3DNV.PlayAnimBattle("market", true);
			}
		}
	}
}
