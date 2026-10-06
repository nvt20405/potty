using UnityEngine;

public class LienMinhNienThuScreenItem : MonoBehaviour
{
	public UILabel NameLabel;

	public UILabel ScoreLabel;

	private void Start()
	{
	}

	public void Set(string name, int count)
	{
		NameLabel.text = name;
		ScoreLabel.text = count.ToString();
	}
}
