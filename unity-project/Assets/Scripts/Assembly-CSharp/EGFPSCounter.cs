using UnityEngine;

public class EGFPSCounter : MonoBehaviour
{
	public int FPS = -1;

	private int frameCount;

	private float timeCount;

	public UILabel label;

	private void Update()
	{
		if ((double)Time.timeScale > 0.001)
		{
			frameCount++;
			timeCount += Time.deltaTime;
		}
		else
		{
			FPS = 0;
		}
		if (timeCount >= 1f || FPS < 0)
		{
			FPS = Mathf.RoundToInt((float)frameCount / timeCount);
			frameCount = 0;
			timeCount = 0f;
			if (label != null)
			{
				label.text = string.Format("{0} fps", FPS);
			}
		}
	}
}
