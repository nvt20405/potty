public class PaymentResponse : ExDataBase
{
	public PhanThuongResponse PhanThuong = new PhanThuongResponse();

	public int RealMoneyCount { get; set; }

	public int KnbCount { get; set; }

	public string OrderID { get; set; }
}
