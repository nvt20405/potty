using UnityEngine;

public class HuyetChienTopMP : MonoBehaviour
{
	public UILabel hangLabel;

	public UILabel nameLabel;

	public UILabel aiLabel;

	public UILabel saoLabel;

	public UILabel vaoBangLabel;

	public NhanVatAvatar avatar;

	private int gid;

	private void OnXemDoiHinhClick()
	{
		XemThongTinMonPhaiRequest xemThongTinMonPhaiRequest = new XemThongTinMonPhaiRequest();
		xemThongTinMonPhaiRequest.TargetGID = gid;
		if (gid > 0)
		{
			GameManager.instance.m_GameClient.RequestXemThongTinMonPhai(xemThongTinMonPhaiRequest);
		}
	}

	public void SetInfo(int hang, HuyetChienTopMonPhai mp)
	{
		nameLabel.text = mp.Name;
		hangLabel.text = string.Format("{0}.", hang);
		aiLabel.text = string.Format(Localization.instance.Get("HuyetChienTopVuotAi"), mp.VuotAi);
		saoLabel.text = string.Format(Localization.instance.Get("HuyetChienTopSao"), mp.Sao);
		vaoBangLabel.text = string.Format(Localization.instance.Get("HuyetChienTopNgayVaoBang"), mp.NgayVaoBang);
		gid = mp.GID;
		avatar.Set(mp.CodeName);
	}
}
