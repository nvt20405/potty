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
