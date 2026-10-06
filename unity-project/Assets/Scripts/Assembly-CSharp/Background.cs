using UnityEngine;

public class Background : MonoBehaviour
{
	public const float ConventionalMinRatio = 1.5f;

	public const float ConventionalMaxRatio = 1.775f;

	private const int targetHeight = 1136;

	private int _lastWidth;

	private int _lastHeight;

	private UIRoot mRoot;

	public int Width { get; private set; }

	public int Height { get; private set; }

	private void Awake()
	{
		if (GUIManager.instance != null)
		{
			mRoot = GUIManager.instance.GUI2DRoot;
		}
		else
		{
			mRoot = Object.FindObjectOfType<UIRoot>();
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (_lastWidth != Screen.width || _lastHeight != Screen.height)
		{
			_lastWidth = Screen.width;
			_lastHeight = Screen.height;
			float num = (float)Screen.height / (float)Screen.width;
			if (num < 1.5f)
			{
				mRoot.manualHeight = 1136;
				Height = 1136;
				Width = Mathf.RoundToInt(757.3333f);
			}
			else if (num > 1.775f)
			{
				mRoot.manualHeight = Mathf.RoundToInt(1136f * num / 1.775f);
				Width = Mathf.RoundToInt((float)mRoot.manualHeight / num);
				Height = mRoot.manualHeight;
			}
			else
			{
				mRoot.manualHeight = 1136;
				Height = mRoot.manualHeight;
				Width = Mathf.RoundToInt((float)mRoot.manualHeight / num);
			}
			base.transform.localScale = new Vector3(Width, Height, 1f);
		}
	}
}
