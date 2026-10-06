public class BanhChungInfoResponse : ExDataBase
{
	public NoiBanh Noi { get; set; }

	public NguoiNauBanh Player { get; set; }

	public UserInfo UpdateUserInfo { get; set; }

	public HomeResponse.Gamer3DInfo Player3DInfo { get; set; }
}
