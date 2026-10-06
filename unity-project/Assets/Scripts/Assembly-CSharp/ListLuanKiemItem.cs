using System;
using System.Collections.Generic;
using UnityEngine;

public class ListLuanKiemItem : ListItemBase
{
	public UITexture textureAvatar;

	public UISprite spriteVIP;

	public UILabel labelNumber;

	public UILabel labelUser;

	public UILabel labelLevel;

	public UILabel labelReward;

	public ListBase listUnit;

	public ButtonBase buttonFight;

	protected override void Awake()
	{
		base.Awake();
		renderItem = (RenderItem)Delegate.Combine(renderItem, new RenderItem(renderItemLuanKiem));
	}

	private void Start()
	{
		string[] array = new string[5] { "Crystal_Maiden", "Enchantress", "Windranger", "Puck", "Lina" };
		listUnit.renderList(ListBase.iconsToListData("80px-" + array[UnityEngine.Random.Range(0, array.Length)], "80px-" + array[UnityEngine.Random.Range(0, array.Length)], "80px-" + array[UnityEngine.Random.Range(0, array.Length)]));
	}

	private void Update()
	{
	}

	public void renderItemLuanKiem(Dictionary<string, string> itemData, GameObject go = null, bool isOverride = false)
	{
		labelNumber.text = itemData["hang"];
		labelUser.text = itemData["userName"];
		labelLevel.text = "Cấp " + itemData["level"];
		labelReward.text = string.Format("+ {0}", itemData["reward"]);
		if (itemData.ContainsKey("isMe") && itemData["isMe"] == "true")
		{
			buttonFight.gameObject.SetActive(false);
		}
		else if (itemData["khieuChien"] == "false")
		{
			buttonFight.gameObject.SetActive(false);
		}
	}
}
