using UnityEngine;

[AddComponentMenu("EG/Tween/Sprite Filling")]
public class TweenSpriteFilling : UITweener
{
	public float from;

	public float to = 1f;

	private float _fillAmount;

	public UISprite target;

	public float FillAmount
	{
		get
		{
			return _fillAmount;
		}
		set
		{
			_fillAmount = value;
			if (target != null)
			{
				target.fillAmount = value;
			}
		}
	}

	private void Awake()
	{
		if (target == null)
		{
			target = GetComponentInChildren<UISprite>();
		}
	}

	protected override void OnUpdate(float factor, bool isFinished)
	{
		FillAmount = (int)Mathf.Lerp(from, to, factor);
	}

	public static TweenSpriteFilling Begin(GameObject go, float duration, float fillAmount)
	{
		TweenSpriteFilling tweenSpriteFilling = UITweener.Begin<TweenSpriteFilling>(go, duration);
		tweenSpriteFilling.from = tweenSpriteFilling.FillAmount;
		tweenSpriteFilling.to = fillAmount;
		if (duration <= 0f)
		{
			tweenSpriteFilling.Sample(1f, true);
			tweenSpriteFilling.enabled = false;
		}
		return tweenSpriteFilling;
	}
}
