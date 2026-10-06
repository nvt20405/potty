using System.Collections.Generic;

public class NVAnimTimeCfg
{
	public class AnimEffectCfg
	{
		public string AnimName { get; set; }

		public float TimeDamagePercent { get; set; }

		public float AnimLength { get; set; }
	}

	private List<AnimEffectCfg> m_listAnimEffect = new List<AnimEffectCfg>();

	public List<AnimEffectCfg> ListAnimEffect
	{
		get
		{
			return m_listAnimEffect;
		}
		set
		{
			m_listAnimEffect = value;
		}
	}

	public double GetTimeDamage(int Number, double ROF)
	{
		string attack_anim = string.Format("attack_{0}", Number);
		AnimEffectCfg animEffectCfg = ListAnimEffect.Find((AnimEffectCfg e) => e.AnimName == attack_anim);
		if (animEffectCfg != null)
		{
			if ((double)animEffectCfg.AnimLength <= ROF)
			{
				return animEffectCfg.TimeDamagePercent * animEffectCfg.AnimLength;
			}
			return (double)animEffectCfg.TimeDamagePercent * ROF;
		}
		return 0.4000000059604645 * ROF;
	}
}
