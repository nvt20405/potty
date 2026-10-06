using UnityEngine;

public class ScreenTestBattle : ScreenBase
{
	public UIInput m_UserName;

	public UIButton m_BtnTestBattle;

	protected virtual void Awake()
	{
		if (GUIManager.instance == null)
		{
			Object.Destroy(base.gameObject);
		}
	}

	protected virtual void OnEnable()
	{
	}

	protected override void firstTimeInit()
	{
		base.firstTimeInit();
		UIEventListener.Get(m_BtnTestBattle.gameObject).onClick = OnTestBattle;
	}

	public void OnTestBattle(GameObject go)
	{
		GameManager.instance.m_GameClient.SendRequest(GameManager.instance.m_GameClient.m_C2SProxy.RequestGetBattleResult, "\"" + m_UserName.text + "\"");
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public virtual bool onESC()
	{
		OnCancel();
		return true;
	}

	private void OnClick()
	{
		EGDebug.Log("Clicked");
	}

	private void OnHover(bool isOver)
	{
		EGDebug.Log("OnHover");
	}

	private void OnPress(bool isDown)
	{
	}

	private void OnRegister()
	{
	}

	private void OnCancel()
	{
	}
}
