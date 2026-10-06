using UnityEngine;

public class NPCAvatar : MonoBehaviour
{
	public Avatar3D avatar;

	public UILabel label;

	public UIPanel ui;

	private string codeName;

	private AnimVuKhi vuKhiAnim;

	public string CodeName
	{
		get
		{
			return codeName;
		}
	}

	public string VuKhiName { get; private set; }

	public string UserName { get; set; }

	public int GID { get; set; }

	public void SetCodeName(string codeName, string vuKhi = "", string thuCuoi = "")
	{
		if (codeName == CodeName && avatar != null)
		{
			if (vuKhi != VuKhiName)
			{
				avatar.LoadVK(vuKhi);
			}
			return;
		}
		if (avatar != null)
		{
			Object.Destroy(avatar.gameObject);
		}
		this.codeName = codeName;
		VuKhiName = vuKhi;
		TrangBiCfg value;
		if (ConfigManager.instance.m_dicTrangBi.TryGetValue(vuKhi, out value))
		{
			vuKhiAnim = value.GetAnimVK();
		}
		avatar = GUIManager.instance.InstantiateAvatar3D(codeName, vuKhi, string.Empty, string.Empty, null, null, thuCuoi, string.Empty, string.Empty);
		avatar.transform.parent = base.transform;
		avatar.transform.localPosition = Vector3.zero;
		avatar.transform.localRotation = Quaternion.identity;
		avatar.transform.localScale = Vector3.one;
	}

	public void SetUserName(string userName)
	{
		label.text = userName;
		UserName = userName;
	}

	private void Start()
	{
	}

	private void Update()
	{
		RotateGUITowardCam();
	}

	protected void RotateGUITowardCam()
	{
		if (ui != null)
		{
			Vector3 vector = Camera.main.transform.rotation * Vector3.forward;
			Vector3 forward = vector - Vector3.up * Vector3.Dot(Vector3.up, vector);
			ui.transform.rotation = Quaternion.LookRotation(forward);
			if (avatar.ThuCuoiName == null || avatar.ThuCuoiName == string.Empty)
			{
				ui.transform.localPosition = 2f * Vector3.up;
			}
			else
			{
				ui.transform.localPosition = 3f * Vector3.up;
			}
		}
	}
}
