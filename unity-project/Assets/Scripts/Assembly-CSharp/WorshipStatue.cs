using System.Collections;
using UnityEngine;

public class WorshipStatue : MonoBehaviour
{
	public GameObject Avatar3D;

	public GameObject ThanDieuObj;

	public new UILabel name;

	public UIPanel panel;

	private string avatar = string.Empty;

	public void LoadAvatar3D(string ava)
	{
		if (!(ava == avatar))
		{
			if (Avatar3D != null)
			{
				Object.Destroy(Avatar3D);
				Avatar3D = null;
			}
			avatar = ava;
			Avatar3D avatar3D = GUIManager.instance.InstantiateAvatar3D(ava, string.Empty, string.Empty, string.Empty, null, null, string.Empty, string.Empty, string.Empty);
			Avatar3D = avatar3D.gameObject;
			Avatar3D.transform.parent = base.transform;
			Avatar3D.transform.localPosition = new Vector3(0f, 0.7f, 0f);
			Avatar3D.transform.localScale = Vector3.one;
			Avatar3D.transform.localRotation = Quaternion.identity;
			StartCoroutine(DelayHoaDaForLoadAvatar3D());
		}
	}

	private void Start()
	{
		HoaDaMesh(ThanDieuObj);
	}

	private IEnumerator DelayHoaDaForLoadAvatar3D()
	{
		while (Avatar3D == null || Avatar3D.GetComponent<Avatar3D>().AvatarGO == null)
		{
			yield return null;
		}
		HoaDaMesh(Avatar3D);
	}

	private void Update()
	{
		if (Avatar3D != null && Avatar3D.activeInHierarchy)
		{
			Avatar3D component = Avatar3D.GetComponent<Avatar3D>();
			if (component != null && component.AvatarGO != null && component.AvatarGO.GetComponent<Animation>() != null && !component.AvatarGO.GetComponent<Animation>().IsPlaying("statue"))
			{
				component.PlayAnimBattle("statue", false);
			}
		}
	}

	private void HoaDaMesh(GameObject go)
	{
		SkinnedMeshRenderer[] componentsInChildren = go.GetComponentsInChildren<SkinnedMeshRenderer>(true);
		SkinnedMeshRenderer[] array = componentsInChildren;
		SkinnedMeshRenderer[] array2 = array;
		foreach (SkinnedMeshRenderer skinnedMeshRenderer in array2)
		{
			Material[] array3 = new Material[skinnedMeshRenderer.materials.Length];
			for (int j = 0; j < skinnedMeshRenderer.materials.Length; j++)
			{
				Material material = skinnedMeshRenderer.materials[j];
				Material material2 = new Material(Shader.Find("Unlit/EGStone unlit"));
				material2.color = Color.white;
				material2.SetTexture("_MainTex", material.mainTexture);
				material2.SetTextureScale("_MainTex", material.mainTextureScale);
				material2.SetTextureOffset("_MainTex", material.mainTextureOffset);
				Material material3 = material2;
				Object obj = Resources.Load("egshader/Stone_texture");
				material3.SetTexture("_StoneTex", (Texture)((obj is Texture) ? obj : null));
				material2.SetTextureScale("_StoneTex", Vector2.one);
				material2.SetTextureOffset("_StoneTex", Vector2.zero);
				array3[j] = material2;
			}
			skinnedMeshRenderer.materials = array3;
		}
	}
}
