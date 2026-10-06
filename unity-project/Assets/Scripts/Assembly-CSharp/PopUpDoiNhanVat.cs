public class PopUpDoiNhanVat : PopUpMessageBase
{
	public static PopUpDoiNhanVat instanceDoiNhanVat;

	private void Start()
	{
	}

	private void Update()
	{
	}

	protected override void Awake()
	{
		isSingleInstance = false;
		instanceDoiNhanVat = this;
		base.Awake();
	}

	public static PopUpDoiNhanVat getInstance()
	{
		if (instanceDoiNhanVat == null)
		{
			instanceDoiNhanVat = Utils.instantiatePrefab("GUI/Controls/PopUpDoiNhanVat", GUIManager.instance.popUpContainer.transform).GetComponent<PopUpDoiNhanVat>();
		}
		return instanceDoiNhanVat;
	}
}
