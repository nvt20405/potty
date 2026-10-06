public class ScreenReg : ScreenBase
{
	public UIInput m_UserName;

	public UIInput m_UserPassword;

	public UIInput m_UserPasswordConfirm;

	public override void OnActive()
	{
		base.OnActive();
		EnsureInputReferences();
	}

	private void EnsureInputReferences()
	{
		if (!(m_UserName == null) && !(m_UserPassword == null) && !(m_UserPasswordConfirm == null))
		{
			return;
		}
		UIInput[] componentsInChildren = GetComponentsInChildren<UIInput>(true);
		UIInput[] array = componentsInChildren;
		foreach (UIInput uIInput in array)
		{
			string text = uIInput.name.ToLower();
			if (text.Contains("confirm") || text.Contains("xacnhan") || text.Contains("repass") || text.Contains("2"))
			{
				m_UserPasswordConfirm = uIInput;
			}
			else if (text.Contains("pass"))
			{
				m_UserPassword = uIInput;
			}
			else if (text.Contains("name") || text.Contains("user") || text.Contains("email") || text.Contains("acc"))
			{
				m_UserName = uIInput;
			}
		}
		if (componentsInChildren.Length >= 3)
		{
			if (m_UserName == null)
			{
				m_UserName = componentsInChildren[0];
			}
			if (m_UserPassword == null)
			{
				m_UserPassword = componentsInChildren[1];
			}
			if (m_UserPasswordConfirm == null)
			{
				m_UserPasswordConfirm = componentsInChildren[2];
			}
		}
		else if (componentsInChildren.Length == 2)
		{
			if (m_UserName == null)
			{
				m_UserName = componentsInChildren[0];
			}
			if (m_UserPassword == null)
			{
				m_UserPassword = componentsInChildren[1];
			}
		}
	}

	private string GetInputText(UIInput input)
	{
		if (input == null)
		{
			return string.Empty;
		}
		string text = input.text;
		if (string.IsNullOrEmpty(text) && input.label != null)
		{
			text = input.label.text;
		}
		return (text ?? string.Empty).Trim();
	}

	private void OnRegister()
	{
		EnsureInputReferences();
		string inputText = GetInputText(m_UserName);
		string inputText2 = GetInputText(m_UserPassword);
		string inputText3 = GetInputText(m_UserPasswordConfirm);
		EGDebug.LogWarning("[ScreenReg] OnRegister: user='" + inputText + "', pass='" + inputText2 + "', confirm='" + inputText3 + "'");
		if (inputText.Length < 3 || inputText.Length > 100)
		{
			MessagePopup.Create(Localization.instance.Get("EmailInvalid"));
			return;
		}
		if (inputText2.Length <= 0 || inputText2.Length > 100)
		{
			MessagePopup.Create(Localization.instance.Get("PasswordInvalid"));
			return;
		}
		if (inputText3.Length > 0 && inputText2 != inputText3)
		{
			MessagePopup.Create(Localization.instance.Get("RetypePasswordInvalid"));
			return;
		}
		GameManager.instance.m_userName = inputText;
		GameManager.instance.m_userPass = inputText2;
		GameManager.instance.m_EntryClient.IssueConnect();
	}

	private void OnCancel()
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenLogin);
	}
}
