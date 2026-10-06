using UnityEngine;

public class EGVietInput : MonoBehaviour
{
	public enum InputMethod
	{
		Off = 0,
		Vni = 1,
		Telex = 2,
		VIQR = 3,
		Combined = 4
	}

	public UIInput Target;

	public EGMUDIM Mudim;

	private InputMethod _method;

	public InputMethod MethodInput
	{
		get
		{
			return _method;
		}
		set
		{
			_method = value;
			if (Mudim != null)
			{
				Mudim.SetMethod((int)value);
			}
		}
	}

	private void Awake()
	{
		Mudim = new EGMUDIM();
		Mudim.SetMethod((int)MethodInput);
		if (Target == null)
		{
			Target = GetComponent<UIInput>();
		}
	}

	private void Start()
	{
		MethodInput = InputMethod.Telex;
	}

	public string UpdateStringTarget(string mText)
	{
		string text = mText.Substring(0, Mudim.startWordOffset);
		return text + Mudim.GetCurStringBuff();
	}

	public bool OnInputKey(char key, string mText, out string targetText)
	{
		targetText = mText;
		switch (key)
		{
		case '\r':
		case ' ':
			Mudim.ClearBuffer();
			return false;
		default:
			if (key > ' ' && key < '\u0080')
			{
				if (Mudim.dirty)
				{
					Mudim.UpdateBuffer(mText);
				}
				if (Mudim.GetBufferLength() == 0)
				{
					Mudim.startWordOffset = mText.Length;
				}
				if (Mudim.AddKey(key))
				{
					targetText = UpdateStringTarget(mText);
					return true;
				}
			}
			else
			{
				Mudim.dirty = true;
			}
			break;
		case '\0':
			break;
		}
		return false;
	}

	private void Update()
	{
	}
}
