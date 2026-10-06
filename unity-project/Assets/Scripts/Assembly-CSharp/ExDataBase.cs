public class ExDataBase
{
	public string ErrorMessage { get; set; }

	public ERROR_CODE ErrorCode { get; set; }

	public ExDataBase()
	{
		ErrorCode = ERROR_CODE.DISPLAY_MESSAGE;
	}
}
