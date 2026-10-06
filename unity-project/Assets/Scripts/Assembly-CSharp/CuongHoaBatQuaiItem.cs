using UnityEngine;

public class CuongHoaBatQuaiItem : MonoBehaviour
{
	public UISprite spIcon;

	public UILabel lbChiSo;

	public GameObject mAnim_MaxLevel;

	public GameObject mAnim_DangCuongHoa;

	public GameObject mAnim_CurLevel;

	public void SetData(string spName, string value)
	{
		spIcon.spriteName = spName;
		lbChiSo.text = value;
		spIcon.color = new Color(255f, 255f, 255f);
		mAnim_CurLevel.gameObject.SetActive(false);
		mAnim_DangCuongHoa.gameObject.SetActive(false);
		mAnim_MaxLevel.gameObject.SetActive(false);
	}

	public void SetMaxLevel()
	{
		mAnim_MaxLevel.gameObject.SetActive(true);
		mAnim_MaxLevel.GetComponent<ParticleSystem>().Play();
		mAnim_CurLevel.gameObject.SetActive(false);
		mAnim_DangCuongHoa.gameObject.SetActive(false);
		spIcon.color = new Color(255f, 255f, 255f);
	}

	public void startCuongHoa()
	{
		mAnim_DangCuongHoa.gameObject.SetActive(true);
		mAnim_DangCuongHoa.GetComponent<ParticleSystem>().Simulate(0f, true, true);
		mAnim_DangCuongHoa.GetComponent<ParticleSystem>().Play();
		mAnim_CurLevel.gameObject.SetActive(false);
		mAnim_MaxLevel.gameObject.SetActive(false);
	}

	public void SetCurrentLevel()
	{
		mAnim_CurLevel.gameObject.SetActive(true);
		mAnim_MaxLevel.GetComponent<ParticleSystem>().Play();
		mAnim_MaxLevel.gameObject.SetActive(false);
		mAnim_DangCuongHoa.gameObject.SetActive(false);
		spIcon.color = new Color(255f, 255f, 255f);
	}

	public void setLockStatus()
	{
		mAnim_CurLevel.gameObject.SetActive(false);
		mAnim_DangCuongHoa.gameObject.SetActive(false);
		mAnim_MaxLevel.gameObject.SetActive(false);
		spIcon.spriteName = "lock";
		spIcon.color = new Color(255f, 255f, 255f);
	}

	public void SetInviStatus()
	{
		mAnim_CurLevel.gameObject.SetActive(false);
		mAnim_DangCuongHoa.gameObject.SetActive(false);
		mAnim_MaxLevel.gameObject.SetActive(false);
		spIcon.color = new Color(114f / 255f, 95f / 255f, 95f / 255f);
	}
}
