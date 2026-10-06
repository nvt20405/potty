using UnityEngine;

public class DanhSonItem : MonoBehaviour
{
	public UILabel tenLabel;

	public GameObject daXongPhaObj;

	public UISprite border1;

	public UISprite border2;

	public OtherAvatar phanThuong;

	public UITexture bgTexture;

	public int DanhSonIdx;

	private DanhSonCfg.PhanThuong reward;

	private float scaleXFactor = 0.959375f;

	private void Awake()
	{
		phanThuong.OnEventClick = OnRewardClick;
		scaleXFactor = bgTexture.transform.localScale.x / 640f;
	}

	private void OnRewardClick(OtherAvatar avatar)
	{
		if (reward != null)
		{
			PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
			PhanThuongResponse.PhanThuong phanThuong = new PhanThuongResponse.PhanThuong();
			phanThuong.Name = PhanThuongResponse.GetPhanThuongCodeName(reward.CodeName);
			phanThuong.Level = reward.Level;
			phanThuong.Count = reward.Count;
			phanThuong.Loai = PhanThuongResponse.GetLoaiPhanThuongFromCode(reward.CodeName);
			phanThuongResponse.PhanThuongList.Add(phanThuong);
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTitle"), Localization.instance.Get("PopupPhanThuongDanhSonDesc"), phanThuongResponse);
		}
	}

	public void SetInfo(string name, bool daXongPha, DanhSonCfg.PhanThuong reward)
	{
		tenLabel.text = name;
		daXongPhaObj.SetActive(daXongPha);
		if (daXongPha)
		{
			daXongPhaObj.GetComponentInChildren<UILabel>().text = Localization.instance.Get("DanhSonDaXongPhaLabel");
		}
		border1.color = ((!daXongPha) ? Color.white : Color.black);
		border2.color = ((!daXongPha) ? Color.white : Color.black);
		if (reward != null)
		{
			phanThuong.gameObject.SetActive(true);
			phanThuong.Set(reward.CodeName, 0, (reward.Level <= 1) ? (-1) : reward.Level, (reward.Count <= 1) ? (-1) : reward.Count);
		}
		else
		{
			phanThuong.gameObject.SetActive(false);
		}
		this.reward = reward;
		int giangHoIdxFromDanhSonIdx = ConfigManager.instance.GetGiangHoIdxFromDanhSonIdx(DanhSonIdx);
		GiangHoCfg giangHoCfg = ((giangHoIdxFromDanhSonIdx >= ConfigManager.instance.m_listGiangHo.Count) ? null : ConfigManager.instance.m_listGiangHo[giangHoIdxFromDanhSonIdx]);
		if (giangHoCfg != null)
		{
			if (giangHoIdxFromDanhSonIdx < 30)
			{
				UITexture uITexture = bgTexture;
				Object obj = Resources.Load("texturegui/DVL_worldmap");
				uITexture.mainTexture = (Texture)((obj is Texture) ? obj : null);
			}
			else
			{
				UITexture uITexture2 = bgTexture;
				Object obj2 = Resources.Load("texturegui/DVL_worldmap_2");
				uITexture2.mainTexture = (Texture)((obj2 is Texture) ? obj2 : null);
			}
			float num = scaleXFactor * GUIManager.instance.GameFrame.transform.localScale.x;
			float y = bgTexture.transform.localScale.y;
			bgTexture.transform.localScale = new Vector3(num, y, 1f);
			float num2 = num / (float)bgTexture.mainTexture.width / 2f;
			float num3 = y / (float)bgTexture.mainTexture.height / 2f;
			float left = Mathf.Clamp01(giangHoCfg.UVTextureX - num2 / 2f);
			float top = Mathf.Clamp01(giangHoCfg.UVTextureY - num3 / 2f);
			Rect rect = default(Rect);
			rect = new Rect(left, top, num2, num3);
			if (rect.xMax > 1f)
			{
				rect.xMin -= Mathf.Clamp01(rect.xMax - 1f);
			}
			if (rect.yMax > 1f)
			{
				rect.yMin -= Mathf.Clamp01(rect.yMax - 1f);
			}
			bgTexture.uvRect = rect;
		}
	}

	private void OnXongPhaBtnClick()
	{
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		UserInfo.DanhSonData danhSonByIdx = userInfo.GetDanhSonByIdx(DanhSonIdx);
		if (danhSonByIdx != null)
		{
			PopupDanhSon.Create(danhSonByIdx);
			return;
		}
		int giangHoIdxFromDanhSonIdx = ConfigManager.instance.GetGiangHoIdxFromDanhSonIdx(DanhSonIdx);
		UserInfo.GiangHoData giangHoByIdx = userInfo.GetGiangHoByIdx(giangHoIdxFromDanhSonIdx);
		if (giangHoByIdx != null && giangHoByIdx.HoanThanh > 0)
		{
			danhSonByIdx = new UserInfo.DanhSonData();
			danhSonByIdx.DanhSonIdx = DanhSonIdx;
			PopupDanhSon.Create(danhSonByIdx);
		}
		else
		{
			MessagePopup.Create(string.Format(Localization.instance.Get("PopupDanhSonHoanThanhGH"), ConfigManager.instance.m_listGiangHo[giangHoIdxFromDanhSonIdx].TenHienThi));
		}
	}
}
