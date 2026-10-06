using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class Utils
{
	public static string GetHierarchyPath(GameObject go)
	{
		if (go == null)
		{
			return string.Empty;
		}
		StringBuilder stringBuilder = new StringBuilder();
		while (go.transform.parent != go.transform.root)
		{
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Insert(0, go.name + "=>");
			}
			else
			{
				stringBuilder.Append(go.name);
			}
			if (go.transform.parent != null)
			{
				go = go.transform.parent.gameObject;
			}
		}
		if (stringBuilder.Length > 0)
		{
			stringBuilder.Insert(0, go.name + "=>");
		}
		else
		{
			stringBuilder.Append(go.name);
		}
		return stringBuilder.ToString();
	}

	public static float CalculateVerticalScrollValueInPixel(UIDraggablePanel drag_panel)
	{
		Bounds bounds = drag_panel.bounds;
		Vector2 vector = bounds.min;
		Vector2 vector2 = bounds.max;
		UIPanel component = drag_panel.gameObject.GetComponent<UIPanel>();
		Vector4 clipRange = component.clipRange;
		float num = clipRange.w * 0.5f;
		float num2 = clipRange.y - num - vector.y;
		float num3 = vector2.y - num - clipRange.y;
		float num4 = vector2.y - vector.y;
		num2 = Mathf.Clamp01(num2 / num4);
		num3 = Mathf.Clamp01(num3 / num4);
		float num5 = num2 + num3;
		float num6 = ((!(num5 > 0.001f)) ? 1f : (1f - num2 / num5));
		return (1f - num6) * num4;
	}

	public static float CalculateVerticalScrollValue(UIDraggablePanel drag_panel)
	{
		Bounds bounds = drag_panel.bounds;
		Vector2 vector = bounds.min;
		Vector2 vector2 = bounds.max;
		UIPanel component = drag_panel.gameObject.GetComponent<UIPanel>();
		Vector4 clipRange = component.clipRange;
		float num = clipRange.w * 0.5f;
		float num2 = clipRange.y - num - vector.y;
		float num3 = vector2.y - num - clipRange.y;
		float num4 = vector2.y - vector.y;
		num2 = Mathf.Clamp01(num2 / num4);
		num3 = Mathf.Clamp01(num3 / num4);
		float num5 = num2 + num3;
		return (!(num5 > 0.001f)) ? 1f : (1f - num2 / num5);
	}

	public static Color MakeColor(int r, int g, int b, int a = 255)
	{
		return new Color((float)r / 255f, (float)g / 255f, (float)b / 255f, (float)a / 255f);
	}

	public static int SetLayer(Transform t, string layerName, bool recursivelyChildren)
	{
		int num = LayerMask.NameToLayer(layerName);
		if (num < 0)
		{
			EGDebug.LogError("SetLayer: Sai ten layer: " + layerName);
			return num;
		}
		t.gameObject.layer = num;
		if (recursivelyChildren)
		{
			foreach (Transform item in t)
			{
				Transform t2 = item;
				SetLayer(t2, layerName, recursivelyChildren);
			}
		}
		return num;
	}

	public static void SetLightMaps(string path, int num = 1)
	{
		LightmapData[] array = LightmapSettings.lightmaps;
		if (array.Length < num)
		{
			Array.Resize(ref array, num);
		}
		for (int i = 0; i < array.Length; i++)
		{
			LightmapData lightmapData = new LightmapData();
			Texture2D far = Resources.Load<Texture2D>(path + "LightmapFar-" + i);
			Texture2D near = Resources.Load<Texture2D>(path + "LightmapNear-" + i);
			lightmapData.lightmapColor = far != null ? far : near;
			array[i] = lightmapData;
		}
		LightmapSettings.lightmaps = array;
	}

	public static void SetGrayLightMaps(string name)
	{
		LightmapData[] lightmaps = LightmapSettings.lightmaps;
		for (int i = 0; i < lightmaps.Length; i++)
		{
			LightmapData lightmapData = new LightmapData();
			lightmapData.lightmapColor = Resources.Load<Texture2D>("Lightmap/Gray/" + name);
			lightmaps[i] = lightmapData;
		}
		LightmapSettings.lightmaps = lightmaps;
	}

	public static List<Dictionary<string, string>> createLabelListData(string prefix, int numItems)
	{
		List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
		for (int i = 0; i < numItems; i++)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("label", prefix + i);
			list.Add(dictionary);
		}
		return list;
	}

	public static GameObject instantiatePrefab(string prefabPath, Transform parent = null, bool resetRotation = false, bool resetPosition = true)
	{
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(prefabPath));
		if (parent != null)
		{
			gameObject.transform.parent = parent;
			gameObject.transform.localScale = Vector3.one;
			if (resetPosition)
			{
				gameObject.transform.localPosition = Vector3.zero;
			}
			if (resetRotation)
			{
				gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
			}
		}
		return gameObject;
	}

	public static string readFile(string url, bool isResource = true)
	{
		string text = null;
		if (isResource)
		{
			text = Resources.Load(url).ToString();
		}
		else
		{
			StreamReader streamReader = new StreamReader(Application.dataPath + "/" + url);
			text = streamReader.ReadToEnd();
			streamReader.Close();
		}
		return text;
	}

	public static T ParseEnum<T>(string value)
	{
		return (T)Enum.Parse(typeof(T), value, true);
	}

	public static Dictionary<Tkey, Tval> cloneDictionary<Tkey, Tval>(Dictionary<Tkey, Tval> dic)
	{
		Dictionary<Tkey, Tval> dictionary = new Dictionary<Tkey, Tval>();
		foreach (KeyValuePair<Tkey, Tval> item in dic)
		{
			dictionary.Add(item.Key, item.Value);
		}
		return dictionary;
	}

	public static void addOrSetDictionary<Tkey, Tval>(Dictionary<Tkey, Tval> dic, Tkey key, Tval val)
	{
		if (!dic.ContainsKey(key))
		{
			dic.Add(key, val);
		}
		else
		{
			dic[key] = val;
		}
	}

	public static T getRandomEnumValue<T>()
	{
		Array values = Enum.GetValues(typeof(T));
		return (T)values.GetValue(UnityEngine.Random.Range(0, values.Length));
	}

	public static void LogBattle(string txt, string fileName)
	{
		string text = Application.persistentDataPath + "/log/";
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		StreamWriter streamWriter = new StreamWriter(text + fileName);
		streamWriter.Write(txt);
		streamWriter.Close();
	}

	public static string getStringNameByKhiThe(int diemKhiThe)
	{
		string result = string.Empty;
		if (diemKhiThe >= 0)
		{
			result = "##khithe1#";
			if (diemKhiThe >= 300)
			{
				result = "##khithe2#";
				if (diemKhiThe >= 600)
				{
					result = "##khithe3#";
					if (diemKhiThe >= 800)
					{
						result = "##khithe4#";
						if (diemKhiThe >= 1000)
						{
							result = "##khithe5#";
							if (diemKhiThe >= 1200)
							{
								result = "##khithe6#";
								if (diemKhiThe >= 1400)
								{
									result = "##khithe7#";
									if (diemKhiThe >= 1600)
									{
										result = "##khithe8#";
										if (diemKhiThe >= 1800)
										{
											result = "##khithe9#";
											if (diemKhiThe >= 2000)
											{
												result = "##khithe10#";
												if (diemKhiThe >= 2200)
												{
													result = "##khithe11#";
													if (diemKhiThe >= 2400)
													{
														result = "##khithe12#";
														if (diemKhiThe >= 2600)
														{
															result = "##khithe13#";
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		return result;
	}
}
