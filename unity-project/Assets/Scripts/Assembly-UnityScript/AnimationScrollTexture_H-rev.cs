using System;
using UnityEngine;

[Serializable]
public class AnimationScrollTexture_H_002Drev : MonoBehaviour
{
	public float Speed;

	public AnimationScrollTexture_H_002Drev()
	{
		Speed = 0.25f;
	}

	public virtual void FixedUpdate()
	{
		float x = Time.time * Speed;
		GetComponent<Renderer>().material.mainTextureOffset = new Vector2(x, 0f);
	}

	public virtual void Main()
	{
	}
}
