using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public static class AssetBundleManager
{
    private sealed class AssetBundleRef
    {
        public AssetBundle assetBundle;
        public int version;
        public string url;
        public bool loading;
        public string error;

        public AssetBundleRef(string sourceUrl, int sourceVersion)
        {
            url = sourceUrl;
            version = sourceVersion;
        }
    }

    private static readonly Dictionary<string, AssetBundleRef> dictAssetBundleRefs = new Dictionary<string, AssetBundleRef>();

    public static AssetBundle getAssetBundle(string url, int version)
    {
        AssetBundleRef value;
        return dictAssetBundleRefs.TryGetValue(MakeKey(url, version), out value) ? value.assetBundle : null;
    }

    public static IEnumerator downloadAssetBundle(string url, int version)
    {
        yield return DownloadInternal(url, version);
    }

    public static IEnumerator downloadAssetBundleWP8(string url, int version)
    {
        yield return DownloadInternal(url, version);
    }

    private static IEnumerator DownloadInternal(string url, int version)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            EGDebug.LogError("AssetBundle URL is empty");
            yield break;
        }

        string key = MakeKey(url, version);
        AssetBundleRef existing;
        if (dictAssetBundleRefs.TryGetValue(key, out existing))
        {
            while (existing.loading) yield return null;
            yield break;
        }

        AssetBundleRef entry = new AssetBundleRef(url, version);
        entry.loading = true;
        dictAssetBundleRefs[key] = entry;

        string cacheDirectory = Path.Combine(Application.persistentDataPath, "LegacyBundles");
        string cacheFile = Path.Combine(cacheDirectory, GetSafeFileName(url, version));

        if (File.Exists(cacheFile))
        {
            AssetBundleCreateRequest cachedLoad = AssetBundle.LoadFromFileAsync(cacheFile);
            yield return cachedLoad;
            if (cachedLoad.assetBundle != null)
            {
                entry.assetBundle = cachedLoad.assetBundle;
                entry.loading = false;
                EGDebug.Log("Loaded AssetBundle from local cache: " + cacheFile);
                yield break;
            }
            try { File.Delete(cacheFile); } catch { }
        }

        EGDebug.Log("Loading AssetBundle url = " + url + " version: " + version);
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.timeout = 30;
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                entry.error = request.error;
                entry.loading = false;
                dictAssetBundleRefs.Remove(key);
                EGDebug.LogError("AssetBundle download failed: " + request.error + " url=" + url);
                yield break;
            }

            byte[] bytes = request.downloadHandler.data;
            if (bytes == null || bytes.Length == 0)
            {
                entry.error = "Empty response";
                entry.loading = false;
                dictAssetBundleRefs.Remove(key);
                EGDebug.LogError("AssetBundle download returned no data: " + url);
                yield break;
            }

            AssetBundleCreateRequest createRequest = AssetBundle.LoadFromMemoryAsync(bytes);
            yield return createRequest;
            if (createRequest.assetBundle == null)
            {
                entry.error = "AssetBundle.LoadFromMemoryAsync failed";
                entry.loading = false;
                dictAssetBundleRefs.Remove(key);
                EGDebug.LogError("Downloaded data is not a Unity 6 compatible AssetBundle: " + url);
                yield break;
            }

            entry.assetBundle = createRequest.assetBundle;
            entry.loading = false;
            try
            {
                Directory.CreateDirectory(cacheDirectory);
                File.WriteAllBytes(cacheFile, bytes);
            }
            catch (Exception ex)
            {
                EGDebug.Log("AssetBundle cache write skipped: " + ex.Message);
            }
        }
    }

    public static void Unload(string url, int version, bool allObjects)
    {
        string key = MakeKey(url, version);
        AssetBundleRef value;
        if (dictAssetBundleRefs.TryGetValue(key, out value))
        {
            if (value.assetBundle != null) value.assetBundle.Unload(allObjects);
            dictAssetBundleRefs.Remove(key);
        }
    }

    private static string MakeKey(string url, int version) { return url + "#" + version; }

    private static string GetSafeFileName(string url, int version)
    {
        string fileName = "bundle";
        try
        {
            Uri uri = new Uri(url);
            string candidate = Path.GetFileName(uri.LocalPath);
            if (!string.IsNullOrEmpty(candidate)) fileName = candidate;
        }
        catch
        {
            string candidate = Path.GetFileName(url);
            if (!string.IsNullOrEmpty(candidate)) fileName = candidate;
        }
        foreach (char invalid in Path.GetInvalidFileNameChars()) fileName = fileName.Replace(invalid, '_');
        return version + "_" + fileName;
    }
}
