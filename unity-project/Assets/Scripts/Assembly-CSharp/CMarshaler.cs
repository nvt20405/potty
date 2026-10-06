using System.Collections.Generic;
using Nettention.Proud;
using UnityEngine;

public class CMarshaler : Marshaler
{
	public static void Write(Message msg, UnityEngine.Vector3 b)
	{
		msg.Write(b.x);
		msg.Write(b.y);
		msg.Write(b.z);
	}

	public static void Read(Message msg, out UnityEngine.Vector3 b)
	{
		b = default(UnityEngine.Vector3);
		msg.Read(out b.x);
		msg.Read(out b.y);
		msg.Read(out b.z);
	}

	public static void Read(Message msg, out List<int> value)
	{
		value = new List<int>();
		int a = 0;
		msg.ReadScalar(ref a);
		for (int i = 0; i < a; i++)
		{
			int b = 0;
			msg.Read(out b);
			value.Add(b);
		}
	}

	public static void Write(Message msg, List<int> value)
	{
		int count = value.Count;
		msg.WriteScalar(count);
		foreach (int item in value)
		{
			msg.Write(item);
		}
	}

	public static void Read(Message msg, out Dictionary<int, float> value)
	{
		value = new Dictionary<int, float>();
		int a = 0;
		msg.ReadScalar(ref a);
		for (int i = 0; i < a; i++)
		{
			int b = 0;
			float b2 = 0f;
			msg.Read(out b);
			msg.Read(out b2);
			value.Add(b, b2);
		}
	}

	public static void Write(Message msg, Dictionary<int, float> value)
	{
		int count = value.Count;
		msg.WriteScalar(count);
		foreach (KeyValuePair<int, float> item in value)
		{
			msg.Write(item.Key);
			msg.Write(item.Value);
		}
	}

	public static void Read(Message msg, out Position2D value)
	{
		value = new Position2D();
		msg.Read(out value.mX);
		msg.Read(out value.mZ);
	}

	public static void Write(Message msg, Position2D value)
	{
		msg.Write(value.mX);
		msg.Write(value.mZ);
	}

	public static void Read(Message msg, out List<Position2D> value)
	{
		value = new List<Position2D>();
		int a = 0;
		msg.ReadScalar(ref a);
		for (int i = 0; i < a; i++)
		{
			Position2D value2;
			Read(msg, out value2);
			value.Add(value2);
		}
	}

	public static void Write(Message msg, List<Position2D> value)
	{
		int count = value.Count;
		msg.WriteScalar(count);
		foreach (Position2D item in value)
		{
			Write(msg, item);
		}
	}
}
