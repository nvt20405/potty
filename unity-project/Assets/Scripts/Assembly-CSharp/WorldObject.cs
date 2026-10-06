using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour
{
	public static Dictionary<int, WorldObject> m_worldObjects = new Dictionary<int, WorldObject>();

	private int m_id_INTERNAL;

	public int m_id
	{
		get
		{
			return m_id_INTERNAL;
		}
		set
		{
			if (m_id_INTERNAL != value)
			{
				if (m_id_INTERNAL != 0)
				{
					m_worldObjects.Remove(m_id);
				}
				if (value != 0)
				{
					m_worldObjects.Add(value, this);
				}
				m_id_INTERNAL = value;
			}
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnDestroy()
	{
		m_id = 0;
	}
}
