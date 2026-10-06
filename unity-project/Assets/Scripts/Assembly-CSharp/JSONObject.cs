using System;
using System.Collections;
using LitJson;

public class JSONObject
{
	private string mJSONString;

	private Hashtable mData = new Hashtable();

	private bool subObject;

	public bool isSubObject
	{
		get
		{
			return subObject;
		}
	}

	public JSONObject()
	{
	}

	public JSONObject(string jsonString)
	{
		Parse(jsonString);
	}

	public JSONObject(Hashtable data)
	{
		mData = data;
		subObject = true;
	}

	public void Parse(string jsonString)
	{
		if (string.IsNullOrEmpty(jsonString))
		{
			mJSONString = null;
			mData.Clear();
			return;
		}
		mJSONString = jsonString;
		mData.Clear();
		try
		{
			JsonReader reader = new JsonReader(mJSONString);
			Parse(reader, mData);
		}
		catch (ArgumentNullException ex)
		{
			EGDebug.LogError(ex.ToString());
		}
		catch (JsonException ex2)
		{
			EGDebug.LogError(ex2.ToString());
		}
		catch (Exception ex3)
		{
			EGDebug.LogError(ex3.ToString());
		}
	}

	private void Parse(JsonReader reader, Hashtable data)
	{
		string text = string.Empty;
		bool flag = false;
		ArrayList arrayList = new ArrayList();
		while (reader.Read())
		{
			switch (reader.Token)
			{
			case JsonToken.ObjectStart:
				if (!string.IsNullOrEmpty(text))
				{
					Hashtable data2 = new Hashtable();
					Parse(reader, data2);
					JSONObject value = new JSONObject(data2);
					if (flag)
					{
						arrayList.Add(value);
						break;
					}
					data.Add(text, value);
					text = string.Empty;
				}
				break;
			case JsonToken.PropertyName:
				text = reader.Value as string;
				break;
			case JsonToken.ObjectEnd:
				return;
			case JsonToken.ArrayStart:
				flag = true;
				break;
			case JsonToken.ArrayEnd:
				flag = false;
				if (arrayList.Count > 0)
				{
					data.Add(text, arrayList.ToArray(arrayList[0].GetType()));
				}
				arrayList.Clear();
				text = string.Empty;
				break;
			case JsonToken.Int:
			case JsonToken.Long:
			case JsonToken.Double:
			case JsonToken.String:
			case JsonToken.Boolean:
				if (flag)
				{
					arrayList.Add(reader.Value);
					break;
				}
				data.Add(text, reader.Value);
				text = string.Empty;
				break;
			case JsonToken.Null:
				if (flag)
				{
					arrayList.Add(null);
					break;
				}
				data.Add(text, null);
				text = string.Empty;
				break;
			default:
				EGDebug.Log("Unknown JsonToken : " + reader.Token);
				EGDebug.Break();
				text = string.Empty;
				break;
			}
		}
	}

	public override string ToString()
	{
		if (string.IsNullOrEmpty(mJSONString))
		{
			return GetType().ToString();
		}
		return mJSONString;
	}

	public object Get(string name)
	{
		if (!mData.Contains(name))
		{
			return null;
		}
		return mData[name];
	}

	private T GetValue<T>(string name)
	{
		object obj = Get(name);
		if (obj == null)
		{
			EGDebug.LogWarning("[WARN] \"" + name + "\" object is null.");
			return default(T);
		}
		if (obj is T)
		{
			return (T)obj;
		}
		EGDebug.LogWarning(string.Concat("[WARN] \"", name, "\" object is not ", typeof(T), " type.\nthis object type is ", obj.GetType()));
		return default(T);
	}

	public bool GetBoolean(string name)
	{
		return GetValue<bool>(name);
	}

	public double GetDouble(string name)
	{
		object obj = Get(name);
		if (obj == null)
		{
			EGDebug.LogWarning("[WARN] \"" + name + "\" object is null.");
			return -1.0;
		}
		if (obj is double || obj is int)
		{
			return Convert.ToDouble(obj);
		}
		Type type = obj.GetType();
		EGDebug.LogWarning("[WARN] \"" + name + "\" object is not double or int type.\nthis object type is " + ((type != null) ? type.ToString() : null));
		return -1.0;
	}

	public int GetInt(string name)
	{
		return GetValue<int>(name);
	}

	public long GetLong(string name)
	{
		object obj = Get(name);
		if (!(obj is int) && !(obj is long))
		{
			EGDebug.LogWarning(string.Concat("[WARN] \"", name, "\" object is not ", typeof(long), " type.\nthis object type is ", obj.GetType()));
			return 0L;
		}
		return Convert.ToInt64(obj);
	}

	public string GetString(string name)
	{
		return GetValue<string>(name);
	}

	public JSONObject GetJSONObject(string name)
	{
		return GetValue<JSONObject>(name);
	}

	public JSONObject[] GetJSONArray(string name)
	{
		return GetValue<JSONObject[]>(name);
	}

	public bool[] GetBooleanArray(string name)
	{
		return GetValue<bool[]>(name);
	}

	public double[] GetDoubleArray(string name)
	{
		return GetValue<double[]>(name);
	}

	public int[] GetIntArray(string name)
	{
		return GetValue<int[]>(name);
	}

	public long[] GetLongArray(string name)
	{
		return GetValue<long[]>(name);
	}

	public string[] GetStringArray(string name)
	{
		return GetValue<string[]>(name);
	}
}
