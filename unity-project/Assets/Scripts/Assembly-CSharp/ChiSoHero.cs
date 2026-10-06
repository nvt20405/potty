using UnityEngine;

public class ChiSoHero : MonoBehaviour
{
	public UIAnchor m_Anchor;

	public UILabel m_TenNhanVatLabel;

	public UISprite m_HPBar_1;

	public UISprite m_HPBar_2;

	public UILabel m_HPLabel;

	public UISprite m_MPBar;

	public UILabel m_MPLabel;

	public UISprite m_ASSprite;

	public UILabel m_ASLabel;

	public UISprite m_CongSprite;

	public UILabel m_CongLabel;

	public UISprite m_ThuSprite;

	public UILabel m_ThuLabel;

	public UISprite m_HPRegSprite;

	public UILabel m_HPRegLabel;

	public UISprite m_MPRegSprite;

	public UILabel m_MPRegLabel;

	public UISprite m_MSSprite;

	public UILabel m_MSLabel;

	public UISprite m_VC1Sprite;

	public UISprite m_VC2Sprite;

	public UISprite m_VC3Sprite;

	public UISprite m_VC4Sprite;

	public UISprite m_VCHU1;

	public UISprite m_VCHU2;

	public UISprite m_VCHU3;

	public UISprite m_VCHU4;

	public UISprite m_VCHU5;

	public UISprite m_VCHU6;

	public UISprite m_VCHU7;

	public UISprite m_VCHU8;

	public UISprite m_VCHU9;

	public UISprite m_VCHU10;

	private void Awake()
	{
		if (m_Anchor != null)
		{
			m_Anchor.uiCamera = UICamera.currentCamera;
			m_Anchor.widgetContainer = GUIManager.instance.GameFrame;
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
