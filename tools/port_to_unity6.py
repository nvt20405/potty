#!/usr/bin/env python3
from pathlib import Path
import argparse, hashlib, re, shutil, json

TARGET_VERSION = '6000.3.25f1'
TARGET_REVISION = 'e1dba0a9aba4'

def meta_for(rel: str) -> str:
    guid = hashlib.md5(('mvl-unity6:' + rel.replace('\\','/')).encode()).hexdigest()
    return f'fileFormatVersion: 2\nguid: {guid}\nMonoImporter:\n  externalObjects: {{}}\n  serializedVersion: 2\n  defaultReferences: []\n  executionOrder: 0\n  icon: {{instanceID: 0}}\n  userData: \n  assetBundleName: \n  assetBundleVariant: \n'

def ensure_using(text: str, namespace: str) -> str:
    line=f'using {namespace};'
    if line in text: return text
    m=list(re.finditer(r'(?m)^using [^;]+;\s*$',text))
    if m:
        pos=m[-1].end()
        return text[:pos]+'\n'+line+text[pos:]
    return line+'\n'+text

def replace_component_shortcuts(s: str) -> str:
    types = {
        'animation':'Animation', 'renderer':'Renderer', 'camera':'Camera',
        'audio':'AudioSource', 'light':'Light', 'particleSystem':'ParticleSystem',
        'rigidbody':'Rigidbody'
    }
    for prop, typ in types.items():
        s=s.replace(f'base.{prop}', f'GetComponent<{typ}>()')
    for prop, typ in types.items():
        pat=re.compile(r'(?<![\w>\)])\b([A-Za-z_]\w*(?:\.[A-Za-z_]\w*)*)\.'+re.escape(prop)+r'\b')
        s=pat.sub(lambda m: f'{m.group(1)}.GetComponent<{typ}>()', s)
    s=s.replace('.GetComponent<ParticleSystem>().renderer', '.GetComponent<ParticleSystemRenderer>()')
    return s

def normalize_resource_literal(path: str) -> str:
    parts = path.split('/')
    if len(parts) < 2:
        return path
    stop = len(parts) if path.endswith('/') else len(parts) - 1
    for i in range(stop):
        if parts[i]:
            parts[i] = parts[i].lower()
    normalized = '/'.join(parts)
    # Two recovered prefab names differ only by historical capitalization.
    # Android Resources lookup is case-sensitive, so preserve the asset's exact leaf case.
    aliases = {
        'popup/PopupCheckKNB': 'popup/PopUpCheckKNB',
        'popup/PopUpBanTrangBi': 'popup/PopupBanTrangBi',
    }
    return aliases.get(normalized, normalized)

def patch_cs(path: Path):
    s=path.read_text(encoding='utf-8-sig',errors='replace')
    orig=s
    s=s.replace('.FindChild(', '.Find(')
    s=s.replace('Object.DestroyObject(', 'Object.Destroy(')
    s=s.replace('UnityEngine.Object.DestroyObject(', 'UnityEngine.Object.Destroy(')
    s=replace_component_shortcuts(s)
    if 'Application.LoadLevelAsync(' in s or 'Application.LoadLevel(' in s:
        s=s.replace('Application.LoadLevelAsync(', 'SceneManager.LoadSceneAsync(')
        s=s.replace('Application.LoadLevel(', 'SceneManager.LoadScene(')
        s=ensure_using(s,'UnityEngine.SceneManagement')
    s=s.replace('.LoadAsync("CostumesAtlas", typeof(GameObject))', '.LoadAssetAsync<GameObject>("CostumesAtlas")')
    if path.name == 'ClipEntity.cs':
        s=s.replace('gameObject.GetComponent<ParticleSystem>().playbackSpeed = TimeScale;',
                    'var particleMain = gameObject.GetComponent<ParticleSystem>().main;\n\t\t\t\tparticleMain.simulationSpeed = TimeScale;')
    s=s.replace('base.collider', 'GetComponent<Collider>()')
    # Unity 4 APIs removed or renamed in modern Unity.
    s=s.replace('.panLevel', '.spatialBlend')
    s=s.replace('.isOrthoGraphic', '.orthographic')
    s=s.replace('.lightmapFar', '.lightmapColor')
    s=s.replace('.lightmapNear', '.lightmapDir')
    s=re.sub(r'\.AddComponent\("([A-Za-z_]\w*)"\)', r'.AddComponent<\1>()', s)

    # Legacy Component.collider shortcut was removed. Keep RaycastHit.collider intact
    # by only rewriting the concrete expressions seen in the recovered game/NGUI code.
    legacy_collider_exprs = [
        'anGaBtn', 'buttonLabel.transform.parent', 'btnAddFriend',
        'GUIManager.instance.homeCity.mainAvatar',
        'GUIManager.instance.homeCity.dongNhanAvatar3D',
        'btnThuocTinh1', 'btnThuocTinh2', 'btnThuocTinh3',
        'mBG', 'mFG', 'background', 'foreground', 'thumb'
    ]
    for expr in legacy_collider_exprs:
        s=s.replace(expr + '.collider', expr + '.GetComponent<Collider>()')
    s=re.sub(r'ItemList\[i\]\.collider', r'ItemList[i].GetComponent<Collider>()', s)
    if path.name == 'UISlider.cs':
        s=s.replace('((collider is BoxCollider) ? collider : null)',
                    '((GetComponent<Collider>() is BoxCollider) ? GetComponent<Collider>() : null)')

    # Obsolete platform enum values were removed after Unity 5.
    if path.name == 'NGUITools.cs':
        s=s.replace('return Application.platform != RuntimePlatform.WindowsWebPlayer && Application.platform != RuntimePlatform.OSXWebPlayer;',
                    'return Application.platform != RuntimePlatform.WebGLPlayer;')
    if path.name in {'NGUIMath.cs', 'UIAnchor.cs', 'UIPanel.cs'}:
        s=s.replace(' || platform == RuntimePlatform.WindowsWebPlayer', '')
        s=s.replace(' || Application.platform == RuntimePlatform.WindowsWebPlayer', '')
        s=s.replace('Application.platform == RuntimePlatform.WindowsWebPlayer || ', '')
    if path.name == 'UICamera.cs':
        s=s.replace(' || Application.platform == RuntimePlatform.WP8Player || Application.platform == RuntimePlatform.BB10Player', '')

    # Recovered client bug: depositing Nien Thu tokens has its own RMI (4274).
    # The decompiled GameClient incorrectly routed it through RequestDanhNienThu (4270).
    if path.name == 'GameClient.cs':
        s=s.replace(
            'SendRequest(m_C2SProxy.RequestDanhNienThu, JsonMapper.ToJson(nopLenhBaiNienThuRequest));',
            'SendRequest(m_C2SProxy.RequestNopLenhBaiNienThu, JsonMapper.ToJson(nopLenhBaiNienThuRequest));')
        s=s.replace(
            'SendRequest(m_C2SProxy.RequestDoiThuongLienMinh, JsonMapper.ToJson(request));\n\t}\n\n\tpublic bool OnDoiMinhChuResponse',
            'SendRequest(m_C2SProxy.RequestDoiMinhChu, JsonMapper.ToJson(request));\n\t}\n\n\tpublic bool OnDoiMinhChuResponse')

    # Preserve the old NGUI art/layout while keeping anchored controls clear of
    # notches and display cutouts on modern Android devices.
    if path.name == 'UIAnchor.cs':
        s=s.replace(
            '\tpublic bool runOnlyOnce;\n',
            '\tpublic bool runOnlyOnce;\n\n\tpublic bool respectSafeArea = true;\n')
        s=s.replace(
'''				float num = ((!(mRoot != null)) ? 0.5f : ((float)mRoot.activeHeight / (float)Screen.height * 0.5f));
				mRect.xMin = (float)(-Screen.width) * num;
				mRect.yMin = (float)(-Screen.height) * num;
				mRect.xMax = 0f - mRect.xMin;
				mRect.yMax = 0f - mRect.yMin;''',
'''				float num = ((!(mRoot != null)) ? 0.5f : ((float)mRoot.activeHeight / (float)Screen.height * 0.5f));
				Rect safe = (respectSafeArea && Application.isPlaying) ? Screen.safeArea : new Rect(0f, 0f, Screen.width, Screen.height);
				float scale = num * 2f;
				mRect.xMin = (safe.xMin - Screen.width * 0.5f) * scale;
				mRect.yMin = (safe.yMin - Screen.height * 0.5f) * scale;
				mRect.xMax = (safe.xMax - Screen.width * 0.5f) * scale;
				mRect.yMax = (safe.yMax - Screen.height * 0.5f) * scale;''')
        s=s.replace(
'''			flag = true;
			mRect = uiCamera.pixelRect;''',
'''			flag = true;
			mRect = uiCamera.pixelRect;
			if (respectSafeArea && Application.isPlaying)
			{
				Rect safe = Screen.safeArea;
				mRect = Rect.MinMaxRect(
					Mathf.Max(mRect.xMin, safe.xMin),
					Mathf.Max(mRect.yMin, safe.yMin),
					Mathf.Min(mRect.xMax, safe.xMax),
					Mathf.Min(mRect.yMax, safe.yMax));
			}''')
    resource_call = re.compile(r'((?:Resources\.Load(?:Async)?(?:<[^>]+>)?|EGResourceAsyncLoader\.Load)\(\s*")([^"]+)(")')
    s=resource_call.sub(lambda m: m.group(1) + normalize_resource_literal(m.group(2)) + m.group(3), s)
    if re.search(r'\bNavMesh(?:Agent|Hit|Path|Obstacle|LinkData|BuildSettings|Triangulation)?\b', s):
        s=ensure_using(s,'UnityEngine.AI')
    if s != orig:
        path.write_text(s,encoding='utf-8')
        return True
    return False

def add_www_compat(script_dir: Path):
    p=script_dir/'WWW.cs'
    p.write_text(r'''using System;
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
''',encoding='utf-8')
    mp=Path(str(p)+'.meta')
    if not mp.exists(): mp.write_text(meta_for(str(p.relative_to(script_dir.parent.parent.parent))),encoding='utf-8')


def add_editor_build_tools(project: Path):
    editor = project/'Assets'/'Editor'
    editor.mkdir(parents=True, exist_ok=True)

    build = editor/'MVLUnity6Build.cs'
    build.write_text(r'''#if UNITY_EDITOR
using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public static class MVLUnity6Build
{
    public static void BuildAndroid()
    {
        MVLUnity6Validation.ValidateOrThrow();

        PlayerSettings.companyName = "HikerGames";
        PlayerSettings.productName = "Mộng Võ Lâm";
        PlayerSettings.SetApplicationIdentifier(NamedBuildTarget.Android, "vn.shg.mobi.mongvolam");
        PlayerSettings.bundleVersion = "6.0.0";
        PlayerSettings.Android.bundleVersionCode = Math.Max(PlayerSettings.Android.bundleVersionCode, 600000);
        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel23;
        PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevelAuto;
        PlayerSettings.SetScriptingBackend(NamedBuildTarget.Android, ScriptingImplementation.IL2CPP);
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64 | AndroidArchitecture.ARMv7;

        string[] scenes = EditorBuildSettings.scenes
            .Where(s => s.enabled && File.Exists(s.path))
            .Select(s => s.path)
            .ToArray();

        if (scenes.Length == 0)
        {
            string[] fallback = { "Assets/AndroidObbLoader.unity", "Assets/GameClient.unity" };
            scenes = fallback.Where(File.Exists).ToArray();
        }
        if (scenes.Length == 0)
            throw new BuildFailedException("No buildable scenes were recovered.");

        Directory.CreateDirectory("Builds");
        var opts = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = Path.GetFullPath("Builds/MongVoLam_Unity6.apk"),
            target = BuildTarget.Android,
            targetGroup = BuildTargetGroup.Android,
            options = BuildOptions.None
        };

        BuildReport report = BuildPipeline.BuildPlayer(opts);
        if (report.summary.result != BuildResult.Succeeded)
            throw new BuildFailedException("Android build failed: " + report.summary.result);

        Debug.Log("[MVL] APK built: " + opts.locationPathName);
    }
}
#endif
''', encoding='utf-8')

    validation = editor/'MVLUnity6Validation.cs'
    validation.write_text(r'''#if UNITY_EDITOR
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
''', encoding='utf-8')

    for p in (build, validation):
        mp = Path(str(p) + '.meta')
        if not mp.exists():
            mp.write_text(meta_for(str(p.relative_to(project))), encoding='utf-8')

def fix_case_collisions(assets: Path):
    groups={}
    for p in assets.rglob('*'):
        if not p.is_file() or p.suffix == '.meta':
            continue
        groups.setdefault(p.relative_to(assets).as_posix().casefold(), []).append(p)
    renamed=[]
    for paths in groups.values():
        if len(paths) < 2:
            continue
        paths=sorted(paths, key=lambda x:x.as_posix())
        for idx,p in enumerate(paths[1:], start=2):
            new=p.with_name(f'{p.stem}__case{idx}{p.suffix}')
            while new.exists():
                idx += 1
                new=p.with_name(f'{p.stem}__case{idx}{p.suffix}')
            old_meta=Path(str(p)+'.meta')
            new_meta=Path(str(new)+'.meta')
            p.rename(new)
            if old_meta.exists():
                old_meta.rename(new_meta)
            renamed.append((p,new))
    return renamed

def migrate(project: Path):
    assets=project/'Assets'
    dummy=assets/'Scripts'/'Assembly-CSharp'
    real=assets/'Scripts'/'Assembly-CSharp-patched'
    if not real.exists(): raise SystemExit(f'Missing decompiled source: {real}')
    dummy.mkdir(parents=True,exist_ok=True)
    merged=extra=0
    for src in real.rglob('*.cs'):
        rel = src.relative_to(real)
        if src.name == 'AssemblyInfo.cs' and 'Properties' in rel.parts:
            continue
        dst = dummy / rel
        dst.parent.mkdir(parents=True, exist_ok=True)
        if dst.exists():
            dst.write_bytes(src.read_bytes()); merged += 1
        else:
            shutil.copy2(src, dst); extra += 1
            mp = Path(str(dst) + '.meta')
            if not mp.exists():
                mp.write_text(meta_for(str(dst.relative_to(project))), encoding='utf-8')
    shutil.rmtree(real)
    real_meta=assets/'Scripts'/'Assembly-CSharp-patched.meta'
    if real_meta.exists(): real_meta.unlink()

    # Unity generates assembly metadata itself. Decompiled AssemblyInfo files from
    # multiple original assemblies collide when imported as C# source in Unity 6.
    for assembly_info in (assets/'Scripts').rglob('AssemblyInfo.cs'):
        if assembly_info.parent.name == 'Properties':
            assembly_info.unlink()
            m = Path(str(assembly_info) + '.meta')
            if m.exists():
                m.unlink()

    for dll in [assets/'Plugins'/'Assembly-CSharp-patched.dll',assets/'Plugins'/'Assembly-CSharp.dll']:
        if dll.exists():
            dll.unlink(); m=Path(str(dll)+'.meta')
            if m.exists(): m.unlink()

    collisions=fix_case_collisions(assets)
    add_www_compat(dummy)
    changed=0
    for p in assets.rglob('*.cs'):
        if patch_cs(p): changed+=1

    add_editor_build_tools(project)

    ps=project/'ProjectSettings'/'ProjectVersion.txt'
    ps.write_text(f'm_EditorVersion: {TARGET_VERSION}\nm_EditorVersionWithRevision: {TARGET_VERSION} ({TARGET_REVISION})\n',encoding='utf-8')
    packages=project/'Packages'; packages.mkdir(exist_ok=True)
    manifest=packages/'manifest.json'
    if not manifest.exists():
        manifest.write_text(json.dumps({'dependencies':{}},indent=2)+'\n',encoding='utf-8')

    (project/'UNITY6_MIGRATION.md').write_text(
        '# Mộng Võ Lâm — Unity 6 migration\n\n'
        f'- Target Editor: Unity {TARGET_VERSION} LTS\n'
        '- Render pipeline: Built-in (keeps legacy NGUI/material/shader behavior)\n'
        '- Decompiled Mono source merged over AssetRipper dummy scripts while retaining original script GUIDs.\n'
        '- Unity 4 component shortcuts and scene loading APIs updated.\n'
        '- Legacy WWW calls bridged to UnityWebRequest/UnityWebRequestAssetBundle for cached model/AssetBundle loading.\n',encoding='utf-8')
    print(f'merged={merged} extra={extra} patched_files={changed} case_collisions_renamed={len(collisions)}')

if __name__=='__main__':
    ap=argparse.ArgumentParser(); ap.add_argument('project',type=Path)
    args=ap.parse_args(); migrate(args.project.resolve())
