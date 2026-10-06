using UnityEngine;

public class ClipAction
{
	public int ID { get; set; }

	public VCEffect.VCEffectType VCHUName { get; set; }

	public string Name { get; set; }

	public float TimeDelay { get; set; }

	public float TimeDetach { get; set; }

	public float Time { get; set; }

	public bool Attached { get; set; }

	public bool Started { get; set; }

	public bool MarkAsRemove { get; set; }

	public string ParamStr1 { get; set; }

	public string ParamStr2 { get; set; }

	public float ParamFloat1 { get; set; }

	public float ParamFloat2 { get; set; }

	public bool ParamBoolean1 { get; set; }

	public bool ParamBoolean2 { get; set; }

	public Vector3 ParamVector { get; set; }

	public bool PauseAsFrozen { get; set; }

	public int ParamInt1 { get; set; }

	public GameObject GoParticle { get; set; }

	public GameObject GoTanAnh { get; set; }

	public Vector3 LookAtPosition { get; set; }

	public ClipAction()
	{
		Attached = false;
		Started = false;
		MarkAsRemove = false;
		PauseAsFrozen = true;
	}
}
