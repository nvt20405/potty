using UnityEngine;

public class AnTromTargetItem : MonoBehaviour
{
	public GameObject itemRoot;

	public int landID;

	public int gamerId;

	public int serverId;

	public UISprite avatar;

	public UILabel displayName;

	public UILabel server;

	public UILabel KNBLabel;

	public UISprite KNBSprite;

	public int size;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Create(LinhDuocAnTromData data)
	{
		avatar.spriteName = data.Avatar;
		gamerId = data.Gid;
		serverId = data.ServerID;
		displayName.text = data.DisplayName;
		server.text = "Server " + data.ServerID;
		if (GameManager.instance.m_GameClient.UserInfo.LinhDuocInfo.StealCount < 5)
		{
			KNBLabel.gameObject.SetActive(false);
			KNBSprite.gameObject.SetActive(false);
		}
		else if (GameManager.instance.m_GameClient.UserInfo.LinhDuocInfo.StealCount < 10)
		{
			KNBLabel.gameObject.SetActive(true);
			KNBSprite.gameObject.SetActive(true);
			KNBLabel.text = "2";
		}
		else if (GameManager.instance.m_GameClient.UserInfo.LinhDuocInfo.StealCount < 15)
		{
			KNBLabel.gameObject.SetActive(true);
			KNBSprite.gameObject.SetActive(true);
			KNBLabel.text = "8";
		}
		else
		{
			KNBLabel.gameObject.SetActive(true);
			KNBLabel.text = "MAX";
		}
		GameObject gameObject = (GameObject)Object.Instantiate(base.gameObject);
		gameObject.SetActive(true);
		gameObject.transform.parent = itemRoot.transform;
		gameObject.transform.localScale = Vector3.one;
		gameObject.transform.localPosition = (itemRoot.transform.childCount - 2) * size * Vector3.down;
	}

	public void Steal()
	{
		GetLinhDuocDataRequest getLinhDuocDataRequest = new GetLinhDuocDataRequest();
		getLinhDuocDataRequest.gid = gamerId;
		getLinhDuocDataRequest.serverID = serverId;
		GameManager.instance.m_GameClient.RequestGetLinhDuocInfo(getLinhDuocDataRequest);
	}
}
