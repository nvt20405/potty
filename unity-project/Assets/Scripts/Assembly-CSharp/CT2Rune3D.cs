using UnityEngine;

public class CT2Rune3D : MonoBehaviour
{
	public UIPanel guiPanel;

	public UISprite typeSprite;

	public int runeType;

	public int runeIndex;

	public GameObject particle;

	private void RotateGUITowardCam()
	{
		if (guiPanel != null && Camera.main != null && Camera.main.transform != null)
		{
			Vector3 vector = Camera.main.transform.rotation * Vector3.forward;
			Vector3 forward = vector - Vector3.up * Vector3.Dot(Vector3.up, vector);
			guiPanel.transform.rotation = Quaternion.LookRotation(forward);
		}
	}

	private void Update()
	{
		RotateGUITowardCam();
	}

	public void SetRuneInfo(int runeType, int runeIndex)
	{
		switch ((ChienTruongChinhTa.RuneType)runeType)
		{
		case ChienTruongChinhTa.RuneType.Ngoai:
		{
			typeSprite.spriteName = "icon_ngoai";
			if (particle != null)
			{
				Object.Destroy(particle);
			}
			Object obj4 = Object.Instantiate(Resources.Load("fx/prefabs/RUNES_NGOAI_SYMBOL"));
			particle = (GameObject)((obj4 is GameObject) ? obj4 : null);
			particle.transform.parent = base.transform;
			particle.transform.localPosition = Vector3.zero;
			break;
		}
		case ChienTruongChinhTa.RuneType.Menh:
		{
			typeSprite.spriteName = "icon_menh";
			if (particle != null)
			{
				Object.Destroy(particle);
			}
			Object obj3 = Object.Instantiate(Resources.Load("fx/prefabs/RUNES_MENH_SYMBOL"));
			particle = (GameObject)((obj3 is GameObject) ? obj3 : null);
			particle.transform.parent = base.transform;
			particle.transform.localPosition = Vector3.zero;
			break;
		}
		case ChienTruongChinhTa.RuneType.Khi:
		{
			typeSprite.spriteName = "icon_noi_goc";
			if (particle != null)
			{
				Object.Destroy(particle);
			}
			Object obj2 = Object.Instantiate(Resources.Load("fx/prefabs/RUNES_KHI_SYMBOL"));
			particle = (GameObject)((obj2 is GameObject) ? obj2 : null);
			particle.transform.parent = base.transform;
			particle.transform.localPosition = Vector3.zero;
			break;
		}
		case ChienTruongChinhTa.RuneType.Than:
		{
			typeSprite.spriteName = "icon_than";
			if (particle != null)
			{
				Object.Destroy(particle);
			}
			Object obj = Object.Instantiate(Resources.Load("fx/prefabs/RUNES_THAN_SYMBOL"));
			particle = (GameObject)((obj is GameObject) ? obj : null);
			particle.transform.parent = base.transform;
			particle.transform.localPosition = Vector3.zero;
			break;
		}
		}
		this.runeType = runeType;
		this.runeIndex = runeIndex;
	}
}
