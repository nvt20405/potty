using System;
using UnityEngine;

[Serializable]
public class AnimationScrollTexture_H : MonoBehaviour
{
	public float Speed;

	public AnimationScrollTexture_H()
	{
		Speed = 0.25f;
	}

	public virtual void FixedUpdate()
	{
		float x = Time.time * (0f - Speed);
		GetComponent<Renderer>().material.mainTextureOffset = new Vector2(x, 0f);
	}

	public virtual void Main()
	{
	}
}
