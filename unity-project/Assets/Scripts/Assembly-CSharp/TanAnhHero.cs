using System.Collections.Generic;
using UnityEngine;

public class TanAnhHero : MonoBehaviour
{
	public static float ThoiGianSong = 1.5f;

	private float m_MaxA = 0.3f;

	private float m_fTime = ThoiGianSong;

	private List<Material> m_listMaterials = new List<Material>();

	private void Start()
	{
		Shader shader = Shader.Find("Transparent/Diffuse");
		SkinnedMeshRenderer[] componentsInChildren = base.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
		SkinnedMeshRenderer[] array = componentsInChildren;
		foreach (SkinnedMeshRenderer skinnedMeshRenderer in array)
		{
			if (!(skinnedMeshRenderer != null))
			{
				continue;
			}
			Material[] materials = skinnedMeshRenderer.materials;
			if (materials == null)
			{
				continue;
			}
			for (int j = 0; j < materials.Length; j++)
			{
				if (materials[j] != null)
				{
					if (shader != null)
					{
						materials[j].shader = shader;
					}
					m_listMaterials.Add(materials[j]);
				}
			}
		}
		ParticleSystem[] componentsInChildren2 = base.gameObject.GetComponentsInChildren<ParticleSystem>();
		ParticleSystem[] array2 = componentsInChildren2;
		foreach (ParticleSystem particleSystem in array2)
		{
			if (particleSystem != null)
			{
				NGUITools.SetActive(particleSystem.gameObject, false);
			}
		}
	}

	private void Update()
	{
		m_fTime -= Time.deltaTime;
		m_fTime = Mathf.Max(0f, m_fTime);
		float a = m_MaxA * (m_fTime / ThoiGianSong);
		for (int i = 0; i < m_listMaterials.Count; i++)
		{
			Material material = m_listMaterials[i];
			if (material != null)
			{
				Color color = material.color;
				material.color = new Color(color.r, color.g, color.b, a);
			}
		}
	}
}
