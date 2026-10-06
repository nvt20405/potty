public class DatTenMonPhaiRequest
{
	public string Name { get; set; }

	public string InviteCode { get; set; }

	public int VatPhamID { get; set; }

	public DatTenMonPhaiRequest()
	{
		Name = string.Empty;
		InviteCode = string.Empty;
		VatPhamID = 0;
	}
}
