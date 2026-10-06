using UnityEngine;

public class QMDSelectDeTuItem : MonoBehaviour
{
	public UILabel Name;

	public NhanVatAvatar Avatar;

	public UILabel MenhLabel;

	public UILabel NgoaiLabel;

	public UILabel ThanLabel;

	public UILabel KhiLabel;

	private void Start()
	{
	}

	public void Set(string name, int level, int menh, int ngoai, int than, int khi)
	{
		UserInfo.HeroData heroData = new UserInfo.HeroData();
		heroData.Name = name;
		heroData.Level = level;
		Avatar.Set(heroData);
		Name.text = ConfigManager.instance.m_dicNhanVats[name].TenHienThi;
		MenhLabel.text = menh.ToString();
		NgoaiLabel.text = ngoai.ToString();
		ThanLabel.text = than.ToString();
		KhiLabel.text = khi.ToString();
	}

	private void Update()
	{
	}
}
