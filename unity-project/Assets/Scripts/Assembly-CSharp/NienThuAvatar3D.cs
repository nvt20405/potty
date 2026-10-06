using System;
using UnityEngine;

public class NienThuAvatar3D : MonoBehaviour
{
	public Action<PlayerController> OnPlayerEnterTrigger;

	public Action<PlayerController> OnPlayerStayInTrigger;

	public UISlider HPBar;

	public LookAtTarget Panel;

	public bool isHit;

	public NienThuData NienThu;

	public void Set(NienThuData nienthu)
	{
		Panel.target = GUIManager.instance.homeCity.cam.transform;
		HPBar.sliderValue = (float)nienthu.CurHP / (float)nienthu.MaxHP;
		NienThu = nienthu;
	}

	private void Update()
	{
		if (GUIManager.instance != null && GUIManager.instance.homeCity != null && GUIManager.instance.homeCity.mainAvatar != null && Vector3.Distance(base.transform.position, GUIManager.instance.homeCity.mainAvatar.transform.position) < 1f && OnPlayerEnterTrigger != null && !isHit)
		{
			isHit = true;
			(GUIManager.instance.GetScreen(GAME_SCREEN.ScreenMain) as ScreenMain).PlayEffectNienThu();
			OnPlayerEnterTrigger(GUIManager.instance.homeCity.mainAvatar);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
	}
}
