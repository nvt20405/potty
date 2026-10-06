using UnityEngine;

public class ChienHonTrans : MonoBehaviour
{
	public static float ThoiGianSong = 1.5f;

	private float m_MaxA = 0.3f;

	private float m_fTime = ThoiGianSong;

	private Shader m_Transparent;

	private SkinnedMeshRenderer[] m_SMeshRenderers;

	private ParticleSystem m_VuKhiAoPar;

	private int m_state = -1;

	private void Start()
	{
		m_Transparent = Shader.Find("Transparent/Diffuse");
		m_SMeshRenderers = base.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
		m_VuKhiAoPar = base.gameObject.GetComponentInChildren<ParticleSystem>();
		if (m_VuKhiAoPar != null)
		{
			NGUITools.SetActive(m_VuKhiAoPar.gameObject, false);
		}
	}

	private void Update()
	{
		if (m_state == -1)
		{
			m_fTime -= Time.deltaTime;
			if (m_fTime < 0f)
			{
				m_state = 1;
			}
		}
		else
		{
			m_fTime += Time.deltaTime;
			if (m_fTime > ThoiGianSong)
			{
				m_state = -1;
			}
		}
		SkinnedMeshRenderer[] sMeshRenderers = m_SMeshRenderers;
		SkinnedMeshRenderer[] array = sMeshRenderers;
		foreach (SkinnedMeshRenderer skinnedMeshRenderer in array)
		{
			skinnedMeshRenderer.material.shader = m_Transparent;
			for (int j = 0; j < skinnedMeshRenderer.materials.Length; j++)
			{
				skinnedMeshRenderer.materials[j].shader = m_Transparent;
				Color color = skinnedMeshRenderer.materials[j].color;
				skinnedMeshRenderer.materials[j].color = new Color(color.r, color.g, color.b, m_MaxA * m_fTime);
			}
		}
	}
}
