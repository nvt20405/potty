using System.Collections.Generic;
using UnityEngine;

public class SystemMessage : MonoBehaviour
{
	private const float UPDATE_TIME = 900f;

	public float speed = 100f;

	private List<HighLightItem> messages;

	public UILabel[] labelText;

	public GameObject[] labelGroup;

	private float[] labelsSize;

	private int index;

	private float lastTimeRequestUpdate;

	private void Start()
	{
		labelsSize = new float[2];
		lastTimeRequestUpdate = Time.time - 900f + 0.2f;
	}

	public void SetData(List<HighLightItem> list_item)
	{
		messages = list_item;
		SyncWithNetworkData();
	}

	public void SyncWithNetworkData()
	{
		if (messages != null && messages.Count > 0)
		{
			InitMessageLabels();
		}
	}

	private void SetMessageText(int index, string str)
	{
		labelText[index].text = str;
		labelsSize[index] = labelText[index].font.CalculatePrintedSize(str, false, UIFont.SymbolStyle.None).x * (float)labelText[index].font.size;
	}

	private void InitMessageLabels()
	{
		index = 0;
		SetMessageText(0, messages[index].Msg);
		index = (index + 1) % messages.Count;
		SetMessageText(1, messages[index].Msg);
		labelGroup[0].transform.localPosition = Vector3.right * 320f;
		labelGroup[1].transform.localPosition = labelGroup[0].transform.localPosition + Vector3.right * (labelsSize[0] + 100f);
	}

	private void Update()
	{
		if (GUIManager.instance == null || !GUIManager.instance.IsReady || (PopupLoading.instance != null && !base.gameObject.activeInHierarchy))
		{
			return;
		}
		if (Time.time - lastTimeRequestUpdate > 900f)
		{
			GameManager.instance.m_GameClient.RequestHighlight();
			lastTimeRequestUpdate = Time.time;
		}
		if (messages != null && messages.Count != 0)
		{
			for (int i = 0; i < labelGroup.Length; i++)
			{
				labelGroup[i].transform.localPosition += Vector3.left * speed * Time.deltaTime;
			}
			if (labelGroup[0].transform.localPosition.x + labelsSize[0] < -340f)
			{
				SwapLabelsIndex();
				labelsSize[0] = labelsSize[1];
				index = (index + 1) % messages.Count;
				SetMessageText(1, messages[index].Msg);
				labelGroup[1].transform.localPosition = labelGroup[0].transform.localPosition + Vector3.right * (labelsSize[0] + 100f);
			}
		}
	}

	private void SwapLabelsIndex()
	{
		UILabel uILabel = labelText[0];
		labelText[0] = labelText[1];
		labelText[1] = uILabel;
		GameObject gameObject = labelGroup[0];
		labelGroup[0] = labelGroup[1];
		labelGroup[1] = gameObject;
	}
}
