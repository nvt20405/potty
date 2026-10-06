using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public const float RunMaxSpeed = 3.5f;

	public const float WalkMaxSpeed = 1.2f;

	public Vector3 currentDes;

	public Avatar3D avatar;

	public UILabel label;

	public UIPanel ui;

	public GameObject grpTonHieu;

	public UISprite spBkbTonHieu;

	public UISprite spBallTonHieu1;

	public UISprite spBallTonHieu2;

	public UILabel lbCurTonHieu;

	public GameObject grpTonHieuTim;

	public GameObject grpTonHieuVang;

	public GameObject parTimLeft;

	public GameObject parTimRight;

	public GameObject parVangLeft;

	public GameObject parVangRight;

	private NavMeshAgent nav;

	private bool isFalling;

	private string codeName;

	private string costume;

	private AnimVuKhi vuKhiAnim;

	private bool _isOnline;

	private static readonly Vector3[] listArround = new Vector3[32]
	{
		new Vector3(3.999961f, 0f, 0.017702792f),
		new Vector3(-3.9996474f, 0f, -0.10698889f),
		new Vector3(3.9990206f, 0f, 0.08850703f),
		new Vector3(-3.9980807f, 0f, -0.12390013f),
		new Vector3(3.9968274f, 0f, 0.15928352f),
		new Vector3(-3.995261f, 0f, -0.19465443f),
		new Vector3(3.9933815f, 0f, 0.2300101f),
		new Vector3(-3.991189f, 0f, -0.26534775f),
		new Vector3(3.9886842f, 0f, 0.3006646f),
		new Vector3(-3.9858665f, 0f, -0.33595788f),
		new Vector3(3.9827368f, 0f, 0.37122488f),
		new Vector3(-3.979295f, 0f, -0.40646276f),
		new Vector3(3.9755414f, 0f, 0.4416688f),
		new Vector3(-3.9714763f, 0f, -0.47684026f),
		new Vector3(3.9671f, 0f, 0.51197433f),
		new Vector3(-3.9624128f, 0f, -0.5470683f),
		new Vector3(0.017702619f, 0f, -1.9999217f),
		new Vector3(1.7110399f, 0f, -1.0355396f),
		new Vector3(1.7799911f, 0f, 0.9119382f),
		new Vector3(0.15909709f, 0f, 1.993662f),
		new Vector3(-1.6128368f, 0f, 1.1826907f),
		new Vector3(-1.8536143f, 0f, -0.7510751f),
		new Vector3(-0.33465198f, 0f, -1.9718033f),
		new Vector3(1.5020143f, 0f, -1.3205881f),
		new Vector3(-3.2f, 0f, 3.2f),
		new Vector3(3.2f, 0f, 3.2f),
		new Vector3(-3.2f, 0f, -3.2f),
		new Vector3(3.2f, 0f, -3.2f),
		new Vector3(-1.6f, 0f, 1.6f),
		new Vector3(1.6f, 0f, 1.6f),
		new Vector3(-1.6f, 0f, -1.6f),
		new Vector3(1.6f, 0f, -1.6f)
	};

	private float moveInterval = 5f;

	private float xacSuatDiChuyen = 0.7f;

	private float timeCounterForMove;

	private int isIdle;

	public string CodeName
	{
		get
		{
			return codeName;
		}
	}

	public string VuKhiName { get; private set; }

	public string Costume
	{
		get
		{
			return costume;
		}
	}

	public string UserName { get; set; }

	public int GID { get; set; }

	public bool IsOnline
	{
		get
		{
			return _isOnline;
		}
		set
		{
			_isOnline = value;
			if (nav != null)
			{
				nav.speed = ((!_isOnline) ? 1.2f : 3.5f);
			}
		}
	}

	private void Awake()
	{
		nav = GetComponent<NavMeshAgent>();
	}

	public void SetCodeName(string codeName, string vuKhi = "", string thuCuoi = "", string costume = "", string thanthu = "", UserInfo.PetInfo.PetQuality thanthuQuality = UserInfo.PetInfo.PetQuality.PHO_THONG)
	{
		SetDirty();
		if (!string.IsNullOrEmpty(Costume))
		{
			if (costume == Costume && avatar != null)
			{
				if (vuKhi != VuKhiName)
				{
					avatar.LoadVK(vuKhi);
					VuKhiName = vuKhi;
				}
				if (thuCuoi != avatar.ThuCuoiName)
				{
					avatar.LoadThuCuoi(thuCuoi);
				}
				if (!string.IsNullOrEmpty(thanthu) && (thanthu != avatar.ThanThuName || thanthuQuality != avatar.ThanThuQuality))
				{
					avatar.LoadThanThu(thanthu, thanthuQuality);
				}
				return;
			}
		}
		else if (Costume == costume && codeName == CodeName && avatar != null)
		{
			if (vuKhi != VuKhiName)
			{
				avatar.LoadVK(vuKhi);
				VuKhiName = vuKhi;
			}
			if (thuCuoi != avatar.ThuCuoiName)
			{
				avatar.LoadThuCuoi(thuCuoi);
			}
			if (!string.IsNullOrEmpty(thanthu) && (thanthu != avatar.ThanThuName || thanthuQuality != avatar.ThanThuQuality))
			{
				avatar.LoadThanThu(thanthu, thanthuQuality);
			}
			return;
		}
		if (avatar != null)
		{
			if (avatar.ThanThuGO != null)
			{
				Object.Destroy(avatar.ThanThuGO);
			}
			Object.Destroy(avatar.gameObject);
		}
		this.codeName = codeName;
		VuKhiName = vuKhi;
		this.costume = costume;
		TrangBiCfg value;
		if (ConfigManager.instance.m_dicTrangBi.TryGetValue(vuKhi, out value))
		{
			vuKhiAnim = value.GetAnimVK();
		}
		avatar = GUIManager.instance.InstantiateAvatar3D(codeName, vuKhi, string.Empty, string.Empty, null, null, thuCuoi, costume, string.Empty);
		avatar.transform.parent = base.transform;
		avatar.transform.localPosition = Vector3.zero;
		avatar.transform.localRotation = Quaternion.identity;
		avatar.transform.localScale = Vector3.one;
	}

	public void SetInfoUser(string userName, string ghiChuTrongNgay, UserInfo.GamerData.TonHieuType typeTonHieu, int level, int countGiangHo)
	{
		label.text = userName;
		UserName = userName;
		if (level < 35)
		{
			displayTonHieuByLevel(5, UserInfo.GamerData.TonHieuType.LEVEL);
		}
		if (level >= 35 && level <= 50)
		{
			displayTonHieuByLevel(4, UserInfo.GamerData.TonHieuType.LEVEL);
		}
		if (level >= 51 && level <= 70)
		{
			displayTonHieuByLevel(3, UserInfo.GamerData.TonHieuType.LEVEL);
		}
		if (level >= 71 && level <= 90)
		{
			displayTonHieuByLevel(2, UserInfo.GamerData.TonHieuType.LEVEL);
		}
		if (level >= 91)
		{
			displayTonHieuByLevel(1, UserInfo.GamerData.TonHieuType.LEVEL);
		}
		switch (typeTonHieu)
		{
		case UserInfo.GamerData.TonHieuType.HANH_TAU:
			if (countGiangHo <= 20)
			{
				displayTonHieuByLevel(5, UserInfo.GamerData.TonHieuType.HANH_TAU);
			}
			if (countGiangHo >= 21 && countGiangHo <= 40)
			{
				displayTonHieuByLevel(4, UserInfo.GamerData.TonHieuType.HANH_TAU);
			}
			if (countGiangHo >= 41 && countGiangHo <= 60)
			{
				displayTonHieuByLevel(3, UserInfo.GamerData.TonHieuType.HANH_TAU);
			}
			if (countGiangHo >= 61 && countGiangHo <= 70)
			{
				displayTonHieuByLevel(2, UserInfo.GamerData.TonHieuType.HANH_TAU);
			}
			if (countGiangHo >= 71)
			{
				displayTonHieuByLevel(1, UserInfo.GamerData.TonHieuType.HANH_TAU);
			}
			break;
		case UserInfo.GamerData.TonHieuType.CHIEN_TRUONG:
			if (ghiChuTrongNgay.Contains("TOP1_ChienTruong"))
			{
				displayTonHieuByLevel(1, UserInfo.GamerData.TonHieuType.CHIEN_TRUONG);
			}
			if (ghiChuTrongNgay.Contains("TOP2_ChienTruong"))
			{
				displayTonHieuByLevel(2, UserInfo.GamerData.TonHieuType.CHIEN_TRUONG);
			}
			if (ghiChuTrongNgay.Contains("TOP3_ChienTruong"))
			{
				displayTonHieuByLevel(3, UserInfo.GamerData.TonHieuType.CHIEN_TRUONG);
			}
			break;
		case UserInfo.GamerData.TonHieuType.TINH_LUYEN:
			if (ghiChuTrongNgay.Contains("TOP1_TinhLuyen"))
			{
				displayTonHieuByLevel(1, UserInfo.GamerData.TonHieuType.TINH_LUYEN);
			}
			if (ghiChuTrongNgay.Contains("TOP2_TinhLuyen"))
			{
				displayTonHieuByLevel(2, UserInfo.GamerData.TonHieuType.TINH_LUYEN);
			}
			if (ghiChuTrongNgay.Contains("TOP3_TinhLuyen"))
			{
				displayTonHieuByLevel(3, UserInfo.GamerData.TonHieuType.TINH_LUYEN);
			}
			break;
		case UserInfo.GamerData.TonHieuType.CONG_LUC:
			if (ghiChuTrongNgay.Contains("TOP1_CongLuc"))
			{
				displayTonHieuByLevel(1, UserInfo.GamerData.TonHieuType.CONG_LUC);
			}
			if (ghiChuTrongNgay.Contains("TOP2_CongLuc"))
			{
				displayTonHieuByLevel(2, UserInfo.GamerData.TonHieuType.CONG_LUC);
			}
			if (ghiChuTrongNgay.Contains("TOP3_CongLuc"))
			{
				displayTonHieuByLevel(3, UserInfo.GamerData.TonHieuType.CONG_LUC);
			}
			break;
		case UserInfo.GamerData.TonHieuType.LUAN_KIEM:
			if (ghiChuTrongNgay.Contains("TOP1_LuanKiem"))
			{
				displayTonHieuByLevel(1, UserInfo.GamerData.TonHieuType.LUAN_KIEM);
			}
			if (ghiChuTrongNgay.Contains("TOP2_LuanKiem"))
			{
				displayTonHieuByLevel(2, UserInfo.GamerData.TonHieuType.LUAN_KIEM);
			}
			if (ghiChuTrongNgay.Contains("TOP3_LuanKiem"))
			{
				displayTonHieuByLevel(3, UserInfo.GamerData.TonHieuType.LUAN_KIEM);
			}
			break;
		case UserInfo.GamerData.TonHieuType.HOANG_KIM:
			if (ghiChuTrongNgay.Contains("TOP1_HoangKim"))
			{
				displayTonHieuByLevel(1, UserInfo.GamerData.TonHieuType.HOANG_KIM);
			}
			if (ghiChuTrongNgay.Contains("TOP2_HoangKim"))
			{
				displayTonHieuByLevel(2, UserInfo.GamerData.TonHieuType.HOANG_KIM);
			}
			if (ghiChuTrongNgay.Contains("TOP3_HoangKim"))
			{
				displayTonHieuByLevel(3, UserInfo.GamerData.TonHieuType.HOANG_KIM);
			}
			break;
		case UserInfo.GamerData.TonHieuType.QUANG_MINH_DINH:
			if (ghiChuTrongNgay.Contains("TOP1_QMD"))
			{
				displayTonHieuByLevel(1, UserInfo.GamerData.TonHieuType.QUANG_MINH_DINH);
			}
			if (ghiChuTrongNgay.Contains("TOP2_QMD"))
			{
				displayTonHieuByLevel(2, UserInfo.GamerData.TonHieuType.QUANG_MINH_DINH);
			}
			if (ghiChuTrongNgay.Contains("TOP3_QMD"))
			{
				displayTonHieuByLevel(3, UserInfo.GamerData.TonHieuType.QUANG_MINH_DINH);
			}
			break;
		case UserInfo.GamerData.TonHieuType.THAN_THU:
			if (ghiChuTrongNgay.Contains("TOP1_ThanThu"))
			{
				displayTonHieuByLevel(1, UserInfo.GamerData.TonHieuType.THAN_THU);
			}
			if (ghiChuTrongNgay.Contains("TOP2_ThanThu"))
			{
				displayTonHieuByLevel(2, UserInfo.GamerData.TonHieuType.THAN_THU);
			}
			if (ghiChuTrongNgay.Contains("TOP3_ThanThu"))
			{
				displayTonHieuByLevel(3, UserInfo.GamerData.TonHieuType.THAN_THU);
			}
			break;
		case UserInfo.GamerData.TonHieuType.DAI_HOI_VO_LAM:
			if (ghiChuTrongNgay.Contains("TOP1_DHVL"))
			{
				displayTonHieuByLevel(1, UserInfo.GamerData.TonHieuType.DAI_HOI_VO_LAM);
			}
			if (ghiChuTrongNgay.Contains("TOP2_DHVL"))
			{
				displayTonHieuByLevel(2, UserInfo.GamerData.TonHieuType.DAI_HOI_VO_LAM);
			}
			if (ghiChuTrongNgay.Contains("TOP3_DHVL"))
			{
				displayTonHieuByLevel(3, UserInfo.GamerData.TonHieuType.DAI_HOI_VO_LAM);
			}
			break;
		case UserInfo.GamerData.TonHieuType.THIEN_MA_THUONG_PHONG:
			if (ghiChuTrongNgay.Contains("TOP1_ThienMa"))
			{
				displayTonHieuByLevel(1, UserInfo.GamerData.TonHieuType.THIEN_MA_THUONG_PHONG);
			}
			if (ghiChuTrongNgay.Contains("TOP2_ThienMa"))
			{
				displayTonHieuByLevel(2, UserInfo.GamerData.TonHieuType.THIEN_MA_THUONG_PHONG);
			}
			if (ghiChuTrongNgay.Contains("TOP3_ThienMa"))
			{
				displayTonHieuByLevel(3, UserInfo.GamerData.TonHieuType.THIEN_MA_THUONG_PHONG);
			}
			break;
		case UserInfo.GamerData.TonHieuType.TRANG_BI_HOANG_KIM:
			if (ghiChuTrongNgay.Contains("TOP1_TBHK"))
			{
				displayTonHieuByLevel(1, UserInfo.GamerData.TonHieuType.TRANG_BI_HOANG_KIM);
			}
			if (ghiChuTrongNgay.Contains("TOP2_TBHK"))
			{
				displayTonHieuByLevel(2, UserInfo.GamerData.TonHieuType.TRANG_BI_HOANG_KIM);
			}
			if (ghiChuTrongNgay.Contains("TOP3_TBHK"))
			{
				displayTonHieuByLevel(3, UserInfo.GamerData.TonHieuType.TRANG_BI_HOANG_KIM);
			}
			break;
		}
	}

	private void displayTonHieuByLevel(int top, UserInfo.GamerData.TonHieuType type)
	{
		grpTonHieu.gameObject.SetActive(true);
		string key = string.Empty;
		string spriteName = string.Empty;
		string spriteName2 = string.Empty;
		for (int i = 1; i < 6; i++)
		{
			if (top == i)
			{
				spriteName2 = "tonhieu_ball_top" + i;
				if (type == UserInfo.GamerData.TonHieuType.LEVEL)
				{
					key = "TonHieu_Level_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.HANH_TAU)
				{
					key = "TonHieu_HanhTau_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.CHIEN_TRUONG)
				{
					key = "TonHieu_ChienTruong_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.TINH_LUYEN)
				{
					key = "TonHieu_TinhLuyen_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.CONG_LUC)
				{
					key = "TonHieu_CongLuc_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.LUAN_KIEM)
				{
					key = "TonHieu_LuanKiem_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.HOANG_KIM)
				{
					key = "TonHieu_HoangKim_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.QUANG_MINH_DINH)
				{
					key = "TonHieu_QMD_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.THAN_THU)
				{
					key = "TonHieu_ThanThu_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.DAI_HOI_VO_LAM)
				{
					key = "TonHieu_DHVL_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.THIEN_MA_THUONG_PHONG)
				{
					key = "TonHieu_ThienMa_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.TRANG_BI_HOANG_KIM)
				{
					key = "TonHieu_TBHoangKim_TOP" + i;
				}
			}
		}
		switch (type)
		{
		case UserInfo.GamerData.TonHieuType.LEVEL:
			spriteName = "tonhieu_level_bg";
			break;
		case UserInfo.GamerData.TonHieuType.HANH_TAU:
			spriteName = "tonhieu_giangho_bg";
			break;
		case UserInfo.GamerData.TonHieuType.CHIEN_TRUONG:
			spriteName = "tonhieu_chientruong_bg";
			break;
		case UserInfo.GamerData.TonHieuType.TINH_LUYEN:
			spriteName = "tonhieu_tinhluyen_bg";
			break;
		case UserInfo.GamerData.TonHieuType.CONG_LUC:
			spriteName = "tonhieu_congluc_bg";
			break;
		case UserInfo.GamerData.TonHieuType.LUAN_KIEM:
			spriteName = "tonhieu_luankiem_bg";
			break;
		case UserInfo.GamerData.TonHieuType.HOANG_KIM:
			spriteName = "tonhieu_hoangkim_bg";
			break;
		case UserInfo.GamerData.TonHieuType.QUANG_MINH_DINH:
			spriteName = "tonhieu_QMD_bg";
			break;
		case UserInfo.GamerData.TonHieuType.THAN_THU:
			spriteName = "tonhieu_thanthu_bg";
			break;
		case UserInfo.GamerData.TonHieuType.DAI_HOI_VO_LAM:
			spriteName = "tonhieu_DHVL_bg";
			break;
		case UserInfo.GamerData.TonHieuType.THIEN_MA_THUONG_PHONG:
			spriteName = "tonhieu_thienma_bg";
			break;
		case UserInfo.GamerData.TonHieuType.TRANG_BI_HOANG_KIM:
			spriteName = "tonhieu_tbhoangkim_bg";
			break;
		}
		grpTonHieuVang.gameObject.SetActive(false);
		grpTonHieuTim.gameObject.SetActive(false);
		if (top == 1)
		{
			grpTonHieuVang.gameObject.SetActive(true);
			parVangRight.GetComponent<ParticleSystem>().Simulate(0f, true, true);
			parVangRight.GetComponent<ParticleSystem>().Play();
			parVangLeft.GetComponent<ParticleSystem>().Simulate(0f, true, true);
			parVangLeft.GetComponent<ParticleSystem>().Play();
		}
		if (top == 2)
		{
			grpTonHieuTim.gameObject.SetActive(true);
			parTimRight.GetComponent<ParticleSystem>().Simulate(0f, true, true);
			parTimRight.GetComponent<ParticleSystem>().Play();
			parTimLeft.GetComponent<ParticleSystem>().Simulate(0f, true, true);
			parTimLeft.GetComponent<ParticleSystem>().Play();
		}
		lbCurTonHieu.text = Localization.instance.Get(key);
		spBkbTonHieu.spriteName = spriteName;
		spBallTonHieu1.spriteName = spriteName2;
		spBallTonHieu2.spriteName = spriteName2;
	}

	public void SetUserName(string userName)
	{
		label.text = userName;
		UserName = userName;
	}

	private Vector3 GetRandomPosArround(Vector3 center)
	{
		Vector3 vector = default(Vector3);
		vector = new Vector3(Random.Range(2f, 5f), 0f, 0f);
		Vector3 vector2 = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f) * vector;
		return center + vector2;
	}

	public void MoveRandomTarget()
	{
		Vector3 point;
		if (GUIManager.instance.homeCity.TryCityWalkPoint(out point))
		{
			NavMeshPath navMeshPath = new NavMeshPath();
			if (!(nav == null) && nav.CalculatePath(point, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
			{
				SetDirty();
				MoveTo(point);
			}
		}
	}

	private void Update()
	{
		NavAnimSetup();
		RotateLabelTowardCam();
		if (!IsOnline && timeCounterForMove > moveInterval)
		{
			if (Random.Range(0f, 1f) < xacSuatDiChuyen)
			{
				MoveRandomTarget();
			}
			timeCounterForMove = 0f;
			moveInterval = Random.Range(15f, 25f);
		}
		timeCounterForMove += Time.deltaTime;
	}

	protected void RotateLabelTowardCam()
	{
		if (ui != null && avatar != null)
		{
			Vector3 vector = Camera.main.transform.rotation * Vector3.forward;
			Vector3 forward = vector;
			ui.transform.rotation = Quaternion.LookRotation(forward);
			if (avatar.ThuCuoiName == null || avatar.ThuCuoiName == string.Empty)
			{
				ui.transform.localPosition = 2f * Vector3.up;
			}
			else
			{
				ui.transform.localPosition = 3f * Vector3.up;
			}
		}
	}

	public void SetDirty()
	{
		isIdle = 0;
	}

	protected void NavAnimSetup()
	{
		if (nav == null || avatar == null)
		{
			return;
		}
		float num = 0.1f;
		if (IsOnline)
		{
			if (!nav.hasPath || Vector3.Distance(base.transform.position, nav.destination) <= num)
			{
				avatar.FadingInIdle(0.2f);
				isIdle = 1;
			}
			else
			{
				avatar.FadingInRun(0.2f);
				isIdle = -1;
			}
		}
		else if (!nav.hasPath || Vector3.Distance(base.transform.position, nav.destination) <= num)
		{
			avatar.FadingInIdle(0.2f);
			isIdle = 1;
		}
		else
		{
			avatar.FadeInWalkAnim(0.4f);
			isIdle = -1;
		}
	}

	public void MoveTo(Vector3 dest)
	{
		if (!(nav == null))
		{
			if (!nav.enabled)
			{
				nav.enabled = true;
			}
			if (nav.SetDestination(dest))
			{
				currentDes = dest;
			}
		}
	}
}
