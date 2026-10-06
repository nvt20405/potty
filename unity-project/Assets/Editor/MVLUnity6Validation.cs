#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;

public static class MVLUnity6Validation
{
    public static void ValidateOrThrow()
    {
        var errors = new List<string>();
        CheckConfig("Assets/Resources/config/NhanVat.json", "Assets/Resources/nhanvat", errors);
        CheckConfig("Assets/Resources/config/Costume.json", "Assets/Resources/costumes", errors);

        CheckPrefab("Assets/Resources/nhanvat/NV_VUONG_TRUNG_DUONG.prefab", errors);
        CheckPrefab("Assets/Resources/nhanvat/NV_HOANG_DUNG.prefab", errors);

        if (!File.Exists("Assets/GameClient.unity"))
            errors.Add("Missing main scene: Assets/GameClient.unity");

        if (errors.Count != 0)
        {
            foreach (string e in errors)
                Debug.LogError("[MVL] " + e);
            throw new BuildFailedException("MVL model/scene validation failed: " + errors.Count + " error(s)");
        }

        Debug.Log("[MVL] Model/scene validation passed.");
    }

    private static void CheckPrefab(string path, List<string> errors)
    {
        if (AssetDatabase.LoadAssetAtPath<GameObject>(path) == null)
            errors.Add("Missing or unimportable prefab: " + path);
    }

    private static void CheckConfig(string config, string folder, List<string> errors)
    {
        if (!File.Exists(config))
        {
            errors.Add("Missing config: " + config);
            return;
        }

        int count = 0;
        foreach (string key in TopLevelKeys(File.ReadAllText(config)))
        {
            count++;
            CheckPrefab(folder + "/" + key + ".prefab", errors);
        }
        if (count == 0)
            errors.Add("No model keys parsed from " + config);
    }

    private static IEnumerable<string> TopLevelKeys(string json)
    {
        var result = new List<string>();
        int depth = 0;
        bool quoted = false, escaped = false;
        int start = -1;

        for (int i = 0; i < json.Length; i++)
        {
            char c = json[i];
            if (quoted)
            {
                if (escaped) { escaped = false; continue; }
                if (c == '\\') { escaped = true; continue; }
                if (c == '"')
                {
                    quoted = false;
                    if (depth == 1 && start >= 0)
                    {
                        int j = i + 1;
                        while (j < json.Length && char.IsWhiteSpace(json[j])) j++;
                        if (j < json.Length && json[j] == ':')
                            result.Add(json.Substring(start, i - start));
                    }
                    start = -1;
                }
                continue;
            }

            if (c == '{') { depth++; continue; }
            if (c == '}') { depth--; continue; }
            if (c == '"') { quoted = true; if (depth == 1) start = i + 1; }
        }
        return result;
    }
}
#endif
