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
        GameObject go = new GameObject("loader");
        EGResourceAsyncLoader loader = go.AddComponent<EGResourceAsyncLoader>();
        loader.autoDestroy = autoDestroy;
        loader.OnLoading = onloading;
        loader.OnFinishLoading = onfinish;
        loader.LoadAsyncResource(path, type);
        return loader;
    }

    public static EGResourceAsyncLoader Load(string path, bool autoDestroy = true, OnFinishLoadingDelegate onfinish = null, OnLoadingDelegate onloading = null)
    {
        GameObject go = new GameObject("loader");
        EGResourceAsyncLoader loader = go.AddComponent<EGResourceAsyncLoader>();
        loader.autoDestroy = autoDestroy;
        loader.OnLoading = onloading;
        loader.OnFinishLoading = onfinish;
        loader.LoadAsyncResource(path);
        return loader;
    }

    private void Update()
    {
        if (IsDone && autoDestroy)
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 0.5f);
        }
        if (OnLoading != null) OnLoading(Progress);
    }

    public void LoadAsyncResource(string path) { StartCoroutine(LoadResource(path, null)); }
    public void LoadAsyncResource(string path, Type type) { StartCoroutine(LoadResource(path, type)); }

    private IEnumerator LoadResource(string path, Type type)
    {
        Path = path;
        Progress = 0f;
        Asset = null;
        yield return LoadOne(path, type);

        if (Asset == null)
        {
            string normalized = NormalizeTopDirectory(path);
            if (!string.Equals(normalized, path, StringComparison.Ordinal))
                yield return LoadOne(normalized, type);
        }

        Progress = 1f;
        IsDone = true;
        if (Asset == null) EGDebug.LogError("Resources asset not found: " + path);
        if (OnFinishLoading != null) OnFinishLoading(Asset);
    }

    private IEnumerator LoadOne(string path, Type type)
    {
        ResourceRequest request = type == null ? Resources.LoadAsync(path) : Resources.LoadAsync(path, type);
        while (!request.isDone)
        {
            Progress = Mathf.Max(Progress, request.progress * 0.95f);
            yield return null;
        }
        Asset = request.asset;
    }

    private static string NormalizeTopDirectory(string path)
    {
        if (string.IsNullOrEmpty(path)) return path;
        int slash = path.IndexOf('/');
        if (slash <= 0) return path.ToLowerInvariant();
        return path.Substring(0, slash).ToLowerInvariant() + path.Substring(slash);
    }
}
