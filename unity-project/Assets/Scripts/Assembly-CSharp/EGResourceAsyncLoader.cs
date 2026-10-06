using System;
using System.Collections;
using UnityEngine;

public class EGResourceAsyncLoader : MonoBehaviour
{
	public delegate void OnFinishLoadingDelegate(UnityEngine.Object go);

	public delegate void OnLoadingDelegate(float progress);

	public UnityEngine.Object Asset;

	public OnFinishLoadingDelegate OnFinishLoading;

	public OnLoadingDelegate OnLoading;

	private bool autoDestroy = true;

	public float Progress { get; private set; }

	public bool IsDone { get; private set; }

	public string Path { get; private set; }

	public static EGResourceAsyncLoader Load(string path, Type type, bool autoDestroy = true, OnFinishLoadingDelegate onfinish = null, OnLoadingDelegate onloading = null)
	{
		GameObject gameObject = new GameObject("loader");
		EGResourceAsyncLoader eGResourceAsyncLoader = gameObject.AddComponent<EGResourceAsyncLoader>();
		eGResourceAsyncLoader.autoDestroy = autoDestroy;
		eGResourceAsyncLoader.OnLoading = onloading;
		eGResourceAsyncLoader.OnFinishLoading = onfinish;
		eGResourceAsyncLoader.LoadAsyncResource(path, type);
		return eGResourceAsyncLoader;
	}

	public static EGResourceAsyncLoader Load(string path, bool autoDestroy = true, OnFinishLoadingDelegate onfinish = null, OnLoadingDelegate onloading = null)
	{
		GameObject gameObject = new GameObject("loader");
		EGResourceAsyncLoader eGResourceAsyncLoader = gameObject.AddComponent<EGResourceAsyncLoader>();
		eGResourceAsyncLoader.autoDestroy = autoDestroy;
		eGResourceAsyncLoader.OnLoading = onloading;
		eGResourceAsyncLoader.OnFinishLoading = onfinish;
		eGResourceAsyncLoader.LoadAsyncResource(path);
		return eGResourceAsyncLoader;
	}

	private void Update()
	{
		if (IsDone && autoDestroy)
		{
			base.gameObject.SetActive(false);
			UnityEngine.Object.Destroy(base.gameObject, 0.5f);
		}
		if (OnLoading != null)
		{
			OnLoading(Progress);
		}
	}

	public void LoadAsyncResource(string path)
	{
		StartCoroutine(LoadResource(path));
	}

	public void LoadAsyncResource(string path, Type type)
	{
		StartCoroutine(LoadResource(path, type));
	}

	private IEnumerator LoadResource(string path, Type type)
	{
		Progress = 0f;
		Asset = Resources.Load(path);
		yield return null;
		Progress = 1f;
		IsDone = true;
		if (OnFinishLoading != null)
		{
			OnFinishLoading(Asset);
		}
	}

	private IEnumerator LoadResource(string path)
	{
		Progress = 0f;
		Asset = Resources.Load(path);
		yield return null;
		Progress = 1f;
		IsDone = true;
		if (OnFinishLoading != null)
		{
			OnFinishLoading(Asset);
		}
	}
}
