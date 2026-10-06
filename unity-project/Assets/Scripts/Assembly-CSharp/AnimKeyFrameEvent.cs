using System.Collections.Generic;
using UnityEngine;

public class AnimKeyFrameEvent : MonoBehaviour
{
	private List<VCTanAnh> m_TanAnhList = new List<VCTanAnh>();

	private void Start()
	{
	}

	private void Update()
	{
	}

	private BattleHero GetParentHero(BattleHero parent)
	{
		if (parent != null)
		{
			return parent;
		}
		return GetComponentInParent<BattleHero>();
	}

	private void OnPlayVC_TanAnh(BattleHero parent)
	{
		parent = GetParentHero(parent);
		if (parent != null && parent.CurAnimNamePlay.Contains("VC_"))
		{
			parent.PlayClip_TanAnh(0f, TanAnhHero.ThoiGianSong);
		}
	}

	private void OnFinish_PlayVC(BattleHero parent)
	{
		parent = GetParentHero(parent);
		if (parent != null && !parent.m_bDauNoiLucStart && parent.GoVuKhiAo != null)
		{
			parent.GoVuKhiAo.GetComponent<ParticleSystem>().Stop();
			Object.Destroy(parent.GoVuKhiAo);
			parent.GoVuKhiAo = null;
		}
	}

	private void OnFinish_PlayAttackThuong(BattleHero parent)
	{
		parent = GetParentHero(parent);
		if (parent != null && !parent.m_bDauNoiLucStart && parent.CurAnimNamePlay.Contains("attack") && !parent.Pause)
		{
			string animWait = parent.GetAnimWait();
			parent.PlayClip_Anim(animWait, 0f, true);
		}
	}

	private void OnStartKiemKhi0(BattleHero parent)
	{
		parent = GetParentHero(parent);
		if (!(parent == null) && parent.CurAnimNamePlay.Contains("attack"))
		{
			string text = "FX/Prefabs/" + parent.SpawnInfo.VK + "_PROJECTILE";
			parent.PlayClip_ProjectTile("FX/Prefabs/VK_KEM_THANH_PROJECTILE", 0f, parent.TID);
		}
	}

	private void OnStartAmKhi0(BattleHero parent)
	{
		parent = GetParentHero(parent);
		if (!(parent == null) && parent.CurAnimNamePlay.Contains("attack"))
		{
			string prefab = "FX/Prefabs/" + parent.SpawnInfo.VK + "_PROJECTILE";
			parent.PlayClip_ProjectTile(prefab, 0f, parent.TID);
		}
	}

	private void OnStartAmKhi1(BattleHero parent)
	{
		parent = GetParentHero(parent);
		if (!(parent == null) && parent.CurAnimNamePlay.Contains("attack"))
		{
			parent.PlayClip_ProjectTile("FX/Prefabs/" + parent.SpawnInfo.VK + "_PROJECTILE", 0f, parent.TID);
		}
	}

	private void OnStartAmKhi2(BattleHero parent)
	{
		parent = GetParentHero(parent);
		if (!(parent == null) && parent.CurAnimNamePlay.Contains("attack"))
		{
			parent.PlayClip_ProjectTile("FX/Prefabs/" + parent.SpawnInfo.VK + "_PROJECTILE", 0f, parent.TID);
		}
	}

	private void OnStartAmKhi3(BattleHero parent)
	{
		parent = GetParentHero(parent);
		if (!(parent == null) && parent.CurAnimNamePlay.Contains("attack"))
		{
			parent.PlayClip_ProjectTile("FX/Prefabs/" + parent.SpawnInfo.VK + "_PROJECTILE", 0f, parent.TID);
		}
	}

	private void OnDestroy()
	{
		int count = m_TanAnhList.Count;
		for (int num = count - 1; num >= 0; num--)
		{
			Object.Destroy(m_TanAnhList[num].Go);
		}
		m_TanAnhList.Clear();
	}

	private void OnFinish_DauNoiLucChange(BattleHero hero)
	{
		hero = GetParentHero(hero);
		if (hero != null && hero.CurAnimNamePlay.StartsWith("VC_DAU_NOI"))
		{
			hero.StartAnim(hero.TAnimDauNC + hero.IndexAnimDauNC);
		}
	}
}
