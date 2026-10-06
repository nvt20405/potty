using UnityEngine;

public class CongThanhCollider : MonoBehaviour
{
	private void OnTriggerExit(Collider other)
	{
		BangChienMainPlayer component = other.GetComponent<BangChienMainPlayer>();
		if (component != null)
		{
			int num = 0;
			if (base.gameObject.name == "cong1def" && component.playerInfo != null && component.playerInfo.Def > 0)
			{
				num = 1;
				component.RequestRoiCongThanh(num);
			}
			else if (base.gameObject.name == "cong2def" && component.playerInfo != null && component.playerInfo.Def > 0)
			{
				num = 2;
				component.RequestRoiCongThanh(num);
			}
			else if (base.gameObject.name == "cong3def" && component.playerInfo != null && component.playerInfo.Def > 0)
			{
				num = 3;
				component.RequestRoiCongThanh(num);
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		BangChienMainPlayer component = other.GetComponent<BangChienMainPlayer>();
		if (component != null)
		{
			int num = 0;
			if (base.gameObject.name == "cong1" && component.playerInfo != null && component.playerInfo.Def <= 0)
			{
				num = 1;
				component.RequestCongThanh(num);
			}
			else if (base.gameObject.name == "cong2" && component.playerInfo != null && component.playerInfo.Def <= 0)
			{
				num = 2;
				component.RequestCongThanh(num);
			}
			else if (base.gameObject.name == "cong3" && component.playerInfo != null && component.playerInfo.Def <= 0)
			{
				num = 3;
				component.RequestCongThanh(num);
			}
			else if (base.gameObject.name == "cong1def" && component.playerInfo != null && component.playerInfo.Def > 0)
			{
				num = 1;
				component.RequestDenCongThanh(num);
			}
			else if (base.gameObject.name == "cong2def" && component.playerInfo != null && component.playerInfo.Def > 0)
			{
				num = 2;
				component.RequestDenCongThanh(num);
			}
			else if (base.gameObject.name == "cong3def" && component.playerInfo != null && component.playerInfo.Def > 0)
			{
				num = 3;
				component.RequestDenCongThanh(num);
			}
			return;
		}
		BangChienPlayer component2 = other.GetComponent<BangChienPlayer>();
		if (component2 != null)
		{
			if (base.gameObject.name == "cong1" && component2.playerInfo != null && component2.playerInfo.Def <= 0)
			{
				component2.AttackerRotateToWall();
				component2.NhayLenWall(0.5f);
			}
			else if (base.gameObject.name == "cong2" && component2.playerInfo != null && component2.playerInfo.Def <= 0)
			{
				component2.AttackerRotateToWall();
				component2.NhayLenWall(0.5f);
			}
			else if (base.gameObject.name == "cong3" && component2.playerInfo != null && component2.playerInfo.Def <= 0)
			{
				component2.AttackerRotateToWall();
				component2.NhayLenWall(0.5f);
			}
		}
	}

	private void OnTriggerStay(Collider other)
	{
		BangChienMainPlayer component = other.GetComponent<BangChienMainPlayer>();
		if (component != null && component.playerInfo != null && component.playerInfo.Def > 0)
		{
			component.RequestGetPhanThuong();
		}
	}
}
