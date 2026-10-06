using UnityEngine;

public class PlayerLienMinhInfo : MonoBehaviour
{
	public UILabel ExpLuaTrai;

	public int ID { get; set; }

	private void Awake()
	{
		ExpLuaTrai.gameObject.SetActive(false);
	}

	public void TangExp(int exp)
	{
		ExpLuaTrai.gameObject.SetActive(true);
		ExpLuaTrai.transform.localPosition = Vector3.zero;
		ExpLuaTrai.alpha = 1f;
		ExpLuaTrai.text = string.Format("+ {0} EXP", exp);
		TweenPosition tweenPosition = TweenPosition.Begin(ExpLuaTrai.gameObject, 1f, new Vector3(0f, 70f, 0f));
		tweenPosition.onFinished = OnFinishFlyUpExp;
		TweenAlpha.Begin(ExpLuaTrai.gameObject, 1f, 0.5f);
	}

	private void OnFinishFlyUpExp(UITweener tween)
	{
		ExpLuaTrai.transform.localPosition = Vector3.zero;
		ExpLuaTrai.alpha = 1f;
		ExpLuaTrai.gameObject.SetActive(false);
	}
}
