using System;
using UnityEngine;

[Serializable]
public class AnimationScrollTexture_V : MonoBehaviour
{
	public float Speed;

	public AnimationScrollTexture_V()
	{
		Speed = 0.25f;
	}

	public virtual void FixedUpdate()
	{
		float y = Time.time * (0f - Speed);
		GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0f, y);
	}

	public virtual void Main()
	{
	}
}
