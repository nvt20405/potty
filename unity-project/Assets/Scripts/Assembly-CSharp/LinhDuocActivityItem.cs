using UnityEngine;

public class LinhDuocActivityItem : MonoBehaviour
{
	public GameObject itemRoot;

	public UILabel content;

	public int size;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Create(string data)
	{
		content.text = data;
		GameObject gameObject = (GameObject)Object.Instantiate(base.gameObject);
		gameObject.SetActive(true);
		gameObject.transform.parent = itemRoot.transform;
		gameObject.transform.localScale = Vector3.one;
		gameObject.transform.localPosition = (itemRoot.transform.childCount - 1) * size * Vector3.down;
	}
}
