using UnityEngine;

[AddComponentMenu("NGUI/Tween/Number")]
public class TweenNumber : UITweener
{
	public float from = 1f;

	public float to = 1f;

	public string header = string.Empty;

	private Transform mTrans;

	private UIWidget mWidget;

	private UIPanel mPanel;

	private float _number;

	public float number
	{
		get
		{
			return _number;
		}
		set
		{
			_number = value;
			((UILabel)mWidget).text = header + _number;
		}
	}

	private void Awake()
	{
		mPanel = GetComponent<UIPanel>();
		if (mPanel == null)
		{
			mWidget = GetComponentInChildren<UIWidget>();
		}
	}

	protected override void OnUpdate(float factor, bool isFinished)
	{
		number = (int)Mathf.Lerp(from, to, factor);
	}

	public static TweenNumber Begin(GameObject go, float duration, float number)
	{
		TweenNumber tweenNumber = UITweener.Begin<TweenNumber>(go, duration);
		tweenNumber.from = tweenNumber.number;
		tweenNumber.to = number;
		if (duration <= 0f)
		{
			tweenNumber.Sample(1f, true);
			tweenNumber.enabled = false;
		}
		return tweenNumber;
	}
}
