using System.Collections.Generic;

public class BatQuaiTran
{
	public static ChiSoCoSo TranDoChiSoTongTangThem(BattleGamerInfo battleGInfo)
	{
		ChiSoCoSo chiSoCoSo = new ChiSoCoSo();
		float num = 0.1f;
		List<int> list = ConfigManager.instance.MapBatQuaiCuongHoa2TranDo(battleGInfo.RawDoiHinhData);
		if (battleGInfo.DoiHinhTranDo != null)
		{
			foreach (BattleGamerInfo.DuLieuHHoTro item in battleGInfo.DoiHinhTranDo)
			{
				ChiSoNhanVat chiSoBatQuaiTran = BattleChiSoHero.GetChiSoBatQuaiTran(item.HID, battleGInfo);
				if (item.Slot > 0)
				{
					num = (float)list[item.Slot - 1] / 100f;
				}
				switch (item.Slot)
				{
				case 1:
				case 5:
					chiSoCoSo.Menh += chiSoBatQuaiTran.Menh * num;
					break;
				case 2:
				case 6:
					chiSoCoSo.Ngoai += chiSoBatQuaiTran.Ngoai * num;
					break;
				case 3:
				case 7:
					chiSoCoSo.ThanPhap += chiSoBatQuaiTran.ThanPhap * num;
					break;
				case 4:
				case 8:
					chiSoCoSo.Noi += chiSoBatQuaiTran.Noi * num;
					break;
				}
			}
		}
		return chiSoCoSo;
	}
}
