using UnityEngine;

public class QMDSkillNVGroup : MonoBehaviour
{
	public OtherAvatar Skill1;

	public OtherAvatar Skill2;

	public OtherAvatar Skill3;

	public void Reset()
	{
		Skill1.Set("plus");
		Skill2.Set("plus");
		Skill3.Set("plus");
	}

	public void Set(string skill1, string skill2, string skill3)
	{
		UserInfo.VoCongData voCongData = new UserInfo.VoCongData();
		voCongData.Name = skill1;
		Skill1.Set(voCongData);
		UserInfo.VoCongData voCongData2 = new UserInfo.VoCongData();
		voCongData2.Name = skill2;
		Skill2.Set(voCongData2);
		UserInfo.VoCongData voCongData3 = new UserInfo.VoCongData();
		voCongData3.Name = skill3;
		Skill3.Set(voCongData3);
	}

	public void OnClickSkill1()
	{
	}

	public void OnClickSkill2()
	{
	}

	public void OnClickSkill3()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
