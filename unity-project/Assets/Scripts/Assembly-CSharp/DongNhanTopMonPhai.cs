using UnityEngine;

public class DongNhanTopMonPhai : MonoBehaviour
{
	public UILabel hangLabel;

	public UILabel nameLabel;

	public UILabel satThuongLabel;

	public GameObject lastHit;

	public UISprite tem;

	private Color[] colors = new Color[5]
	{
		new Color(210f / 255f, 12f / 255f, 205f / 255f),
		new Color(1f, 36f / 255f, 36f / 255f),
		new Color(227f / 255f, 14f / 15f, 37f / 255f),
		new Color(18f / 255f, 13f / 15f, 249f / 255f),
		new Color(0f, 214f / 255f, 37f / 255f)
	};

	public void SetInfo(DongNhanResponse.MonPhaiTop mp, int hang)
	{
		hangLabel.text = string.Format(Localization.instance.Get("DongNhanHangLabel"), hang);
		nameLabel.text = mp.Name;
		satThuongLabel.text = mp.TotalThuongTon.ToString();
		lastHit.SetActive(mp.IsLastHit);
		if (hang < colors.Length && hang > 0)
		{
			tem.color = colors[hang - 1];
		}
		else
		{
			tem.color = colors[colors.Length - 1];
		}
	}
}
