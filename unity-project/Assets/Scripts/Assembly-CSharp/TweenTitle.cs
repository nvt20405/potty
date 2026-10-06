using UnityEngine;

[AddComponentMenu("NGUI/Tween/Title")]
public class TweenTitle : UITweener
{
	public float from = 1f;

	public float to = 1f;

	public string[] text;

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
			string text = string.Empty;
			for (int i = 0; (float)i < _number; i++)
			{
				text = text + "\n" + this.text[i];
			}
			((UILabel)mWidget).text = text;
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
		number = Mathf.Floor(Mathf.Lerp(from, to, factor));
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
