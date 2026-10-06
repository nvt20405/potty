public class ChoiXocDiaRequest
{
	public enum XocDiaChanLe
	{
		Chan = 0,
		Le = 1
	}

	public int Idx { get; set; }

	public XocDiaChanLe DatCua { get; set; }

	public int TrangBiOrVoCongID { get; set; }
}
