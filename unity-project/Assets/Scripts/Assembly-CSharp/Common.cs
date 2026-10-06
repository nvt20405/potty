using System.Collections.Generic;

public class Common
{
	public static readonly int FPS = 10;

	public static float TIME_PER_FRAME = 1f / (float)FPS;

	public static float BATTLE_OFFSET_Z = 3f;

	public static float BATTLE_SIZE_Z = 7f;

	public static float BATTLE_SIZE_X = 7.5f;

	public static float TyleModel = 1.25f;

	public static List<string> FullLoginProps = new List<string>
	{
		"gamer", "hero", "doihinh", "trangbi", "vocong", "lienminh", "nguyenkhi", "danhhieu", "thucuoi", "costume",
		"thanthu", "sonmon", "lanhdia", "thienma", "gianghotinhanh", "chienhon", "huyenkhi"
	};

	public static List<string> FullBattleProps = new List<string>
	{
		"gamer", "trangBi", "voCong", "vcthietlap", "hero", "giaTriThoiGian", "doihinh", "giangHo", "danhhieu", "manhvocong",
		"manhtrangbi", "honnhanvat", "vatphamtieuthu", "lienminh", "nguyenkhi", "danhhieu", "thucuoi", "costume", "thanthu", "thienma",
		"gianghotinhanh"
	};
}
