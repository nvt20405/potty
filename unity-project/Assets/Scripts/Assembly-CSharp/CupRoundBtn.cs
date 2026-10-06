using UnityEngine;

public class CupRoundBtn : MonoBehaviour
{
	public UILabel label;

	public UISprite bg;

	public SuperCupView view;

	public int VongDau { get; set; }

	private void OnClick()
	{
		view.OnViewVongDau(VongDau);
	}
}
