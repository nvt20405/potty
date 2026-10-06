using UnityEngine;

public class SuperCupGameInfo : MonoBehaviour
{
	public UILabel name1;

	public UILabel name2;

	public UISprite winner1;

	public UISprite winner2;

	public UISprite avatar1;

	public UISprite avatar2;

	public SuperCupView view;

	public int idx;

	public int TranDauID;

	private void OnShowDetail()
	{
		if (view != null)
		{
			view.OnViewGameDetail(idx);
		}
	}
}
