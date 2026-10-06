using System;
using UnityEngine;

[Serializable]
public class AnimationSpriteSheet_H : MonoBehaviour
{
	public int uvX;

	public int uvY;

	public float fps;

	public AnimationSpriteSheet_H()
	{
		uvX = 4;
		uvY = 2;
		fps = 24f;
	}

	public virtual void Update()
	{
		int num = (int)(Time.time * fps);
		num %= uvX * uvY;
		Vector2 scale = new Vector2(1f / (float)uvX, 1f / (float)uvY);
		int num2 = num % uvX;
		int num3 = num / uvX;
		Vector2 offset = new Vector2((float)num2 * scale.x, 1f - scale.y - (float)num3 * scale.y);
		GetComponent<Renderer>().material.SetTextureOffset("_MainTex", offset);
		GetComponent<Renderer>().material.SetTextureScale("_MainTex", scale);
	}

	public virtual void Main()
	{
	}
}
