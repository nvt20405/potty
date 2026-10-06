using UnityEngine;

public class ListItemPartBase : ButtonBase
{
	public ListItemBase parentItem;

	public int partIndex;

	public override void onClick(GameObject go)
	{
		base.onClick(go);
		if (parentItem == null && base.transform.parent != null)
		{
			parentItem = base.transform.parent.GetComponent<ListItemBase>();
		}
		if (parentItem != null)
		{
			parentItem.onPartClick(partIndex);
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
