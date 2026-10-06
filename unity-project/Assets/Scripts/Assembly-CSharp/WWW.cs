using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public sealed class WWW : CustomYieldInstruction, IDisposable
{
    private readonly UnityWebRequest request;
    private readonly bool assetBundleRequest;

    public override bool keepWaiting => request != null && !request.isDone;
    public bool isDone => request == null || request.isDone;
    public float progress => request == null ? 1f : request.downloadProgress;
    public string url => request?.url;
    public string error => request == null || request.result == UnityWebRequest.Result.Success ? null : request.error;
    public byte[] bytes => request?.downloadHandler?.data;
    public string text => request?.downloadHandler?.text;
    public Texture2D texture
    {
        get
        {
            if (request?.downloadHandler?.data == null) return null;
            var tex = new Texture2D(2, 2, TextureFormat.RGBA32, false);
            if (!ImageConversion.LoadImage(tex, request.downloadHandler.data))
            {
                UnityEngine.Object.Destroy(tex);
                return null;
            }
            return tex;
        }
    }
    public AssetBundle assetBundle => assetBundleRequest && request != null && request.isDone && request.result == UnityWebRequest.Result.Success
        ? DownloadHandlerAssetBundle.GetContent(request) : null;

    public WWW(string url)
    {
        request = UnityWebRequest.Get(url);
        assetBundleRequest = false;
        request.SendWebRequest();
    }

    public WWW(string url, WWWForm form)
    {
        request = new UnityWebRequest(url, "POST");
        request.downloadHandler = new DownloadHandlerBuffer();
        request.uploadHandler = new UploadHandlerRaw(form.data);
        foreach (var kv in form.headers)
        {
            if (!string.Equals(kv.Key, "Content-Length", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(kv.Key, "User-Agent", StringComparison.OrdinalIgnoreCase))
                request.SetRequestHeader(kv.Key, kv.Value);
        }
        assetBundleRequest = false;
        request.SendWebRequest();
    }

    private WWW(UnityWebRequest req, bool isBundle)
    {
        request = req;
        assetBundleRequest = isBundle;
        request.SendWebRequest();
    }

    public static WWW LoadFromCacheOrDownload(string url, int version)
    {
        var req = UnityWebRequestAssetBundle.GetAssetBundle(url, unchecked((uint)Math.Max(0, version)), 0u);
        return new WWW(req, true);
    }

    public void Dispose() => request?.Dispose();
}
