using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleManager
{
	private class AssetBundleRef
	{
		public AssetBundle assetBundle;

		public int version;

		public string url;

		public bool loading;

		public AssetBundleRef(string strUrlIn, int intVersionIn)
		{
			url = strUrlIn;
			version = intVersionIn;
		}
	}

	private static Dictionary<string, AssetBundleRef> dictAssetBundleRefs;

	static AssetBundleManager()
	{
		dictAssetBundleRefs = new Dictionary<string, AssetBundleRef>();
	}

	public static AssetBundle getAssetBundle(string url, int version)
	{
		string key = url + version;
		AssetBundleRef value;
		if (dictAssetBundleRefs.TryGetValue(key, out value))
		{
			return value.assetBundle;
		}
		return null;
	}

	public static IEnumerator downloadAssetBundle(string url, int version)
	{
		string keyName = url + version;
		while (dictAssetBundleRefs.ContainsKey(keyName))
		{
			yield return null;
		}
		AssetBundleRef abRef = new AssetBundleRef(url, version);
		dictAssetBundleRefs.Add(keyName, abRef);
		abRef.loading = true;
		WWW www = WWW.LoadFromCacheOrDownload(url, version);
		try
		{
			EGDebug.Log("Loading AssetBundle url = " + url + " version: " + version);
			yield return www;
			if (www.error != null)
			{
				EGDebug.LogError("WWW download:" + www.error);
				abRef.loading = false;
				abRef.assetBundle = null;
				dictAssetBundleRefs.Remove(keyName);
			}
			else
			{
				abRef.assetBundle = www.assetBundle;
				abRef.loading = false;
			}
		}
		finally
		{
			if (www != null)
			{
				((IDisposable)www).Dispose();
			}
		}
	}

	public static IEnumerator downloadAssetBundleWP8(string url, int version)
	{
		string keyName = url + version;
		if (dictAssetBundleRefs.ContainsKey(keyName) && dictAssetBundleRefs[keyName].assetBundle != null)
		{
			yield break;
		}
		if (dictAssetBundleRefs.ContainsKey(keyName))
		{
			while (dictAssetBundleRefs.ContainsKey(keyName) && dictAssetBundleRefs[keyName].assetBundle == null)
			{
				yield return null;
			}
		}
		if (dictAssetBundleRefs.ContainsKey(keyName))
		{
			yield break;
		}
		AssetBundleRef abRef = new AssetBundleRef(url, version);
		dictAssetBundleRefs.Add(keyName, abRef);
		abRef.loading = true;
		WWW www = WWW.LoadFromCacheOrDownload(url, version);
		try
		{
			EGDebug.Log("Loading AssetBundle url = " + url + " version: " + version);
			yield return www;
			if (www.error != null)
			{
				EGDebug.LogError("WWW download:" + www.error);
				abRef.loading = false;
				abRef.assetBundle = null;
				dictAssetBundleRefs.Remove(keyName);
			}
			else if (!dictAssetBundleRefs.ContainsKey(keyName) || !(dictAssetBundleRefs[keyName].assetBundle != null))
			{
				abRef.assetBundle = www.assetBundle;
				abRef.loading = false;
			}
		}
		finally
		{
			if (www != null)
			{
				((IDisposable)www).Dispose();
			}
		}
	}

	public static void Unload(string url, int version, bool allObjects)
	{
		string key = url + version;
		AssetBundleRef value;
		if (dictAssetBundleRefs.TryGetValue(key, out value))
		{
			value.assetBundle.Unload(allObjects);
			value.assetBundle = null;
			dictAssetBundleRefs.Remove(key);
		}
	}
}
