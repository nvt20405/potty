using UnityEngine;

public class RepVcEffect
{
	private BattleReplay.TrangThai.HieuUng _e = new BattleReplay.TrangThai.HieuUng();

	public BattleReplay.TrangThai.HieuUng EFFECT
	{
		get
		{
			return _e;
		}
		set
		{
			_e = value;
		}
	}

	public GameObject GoParticle { get; set; }
}
