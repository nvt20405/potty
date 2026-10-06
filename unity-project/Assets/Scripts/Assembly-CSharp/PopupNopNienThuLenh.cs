using UnityEngine;

public class PopupNopNienThuLenh : MonoBehaviour
{
	public static PopupNopNienThuLenh instance;

	public int ItemID;

	public int Quantity;

	public static void Create()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
		}
		Object obj = Object.Instantiate(Resources.Load("Popup/PopupNopNienThuLenh"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		instance = gameObject.GetComponent<PopupNopNienThuLenh>();
		instance.Quantity = 0;
	}

	public void IncQuantity()
	{
		Quantity++;
	}

	public void DecQuantity()
	{
		if (Quantity > 0)
		{
			Quantity--;
		}
	}

	public void IncQuantity10()
	{
		Quantity += 10;
	}

	public void DecQuantity10()
	{
		if (Quantity > 9)
		{
			Quantity -= 10;
		}
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
		}
		instance = null;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
