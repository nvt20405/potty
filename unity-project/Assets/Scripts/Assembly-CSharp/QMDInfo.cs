using System.Collections.Generic;

public class QMDInfo
{
	public enum QMDXongPhaState
	{
		START = 0,
		SELECT_NV1 = 1,
		SELECT_NV1_SKILL1 = 2,
		SELECT_NV1_SKILL2 = 3,
		SELECT_NV1_SKILL3 = 4,
		SELECT_NV2 = 5,
		SELECT_NV2_SKILL1 = 6,
		SELECT_NV2_SKILL2 = 7,
		SELECT_NV2_SKILL3 = 8,
		SELECT_NV3 = 9,
		SELECT_NV3_SKILL1 = 10,
		SELECT_NV3_SKILL2 = 11,
		SELECT_NV3_SKILL3 = 12,
		SELECT_NV4 = 13,
		SELECT_NV4_SKILL1 = 14,
		SELECT_NV4_SKILL2 = 15,
		SELECT_NV4_SKILL3 = 16,
		FINISH = 17
	}

	public class QMDNhanVat
	{
		public string Name = string.Empty;

		public string Skill1 = string.Empty;

		public string Skill2 = string.Empty;

		public string Skill3 = string.Empty;

		public float DHPosX = 0.5f;

		public float DHPosY = 0.5f;

		public UserInfo.HeroData.AI ChienThuat = new UserInfo.HeroData.AI();
	}

	public class QMDNVChiSo
	{
		public int Menh;

		public int Ngoai;

		public int Than;

		public int Khi;
	}

	public QMDNhanVat NV1 = new QMDNhanVat();

	public QMDNhanVat NV2 = new QMDNhanVat();

	public QMDNhanVat NV3 = new QMDNhanVat();

	public QMDNhanVat NV4 = new QMDNhanVat();

	public string NPCName = string.Empty;

	public List<string> NPCList = new List<string>();

	public int NPCIdx;

	public int NPCAi = 1;

	public int NVLevel = 200;

	public int SkillLevel = 9;

	public string NextChoice1 = string.Empty;

	public string NextChoice2 = string.Empty;

	public string NextChoice3 = string.Empty;

	public QMDNVChiSo Choice1ChiSo = new QMDNVChiSo();

	public QMDNVChiSo Choice2ChiSo = new QMDNVChiSo();

	public QMDNVChiSo Choice3ChiSo = new QMDNVChiSo();

	public QMDXongPhaState State;

	public bool IsLose;

	public bool IsHave1Sao;

	public int ID { get; set; }

	public QMDInfo()
	{
		ID = 0;
	}

	public QMDInfo(int id)
	{
		ID = id;
	}

	public bool IsHaveChoice(string c)
	{
		if (c == NextChoice1)
		{
			return true;
		}
		if (c == NextChoice2)
		{
			return true;
		}
		if (c == NextChoice3)
		{
			return true;
		}
		return false;
	}
}
