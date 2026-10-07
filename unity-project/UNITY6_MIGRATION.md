# Mộng Võ Lâm — Unity 6 migration

- Target Editor: Unity 6000.3.25f1 LTS
- Render pipeline: Built-in (keeps legacy NGUI/material/shader behavior)
- Decompiled Mono source merged over AssetRipper dummy scripts while retaining original script GUIDs.
- Unity 4 component shortcuts and scene loading APIs updated.
- Legacy WWW calls bridged to UnityWebRequest/UnityWebRequestAssetBundle for cached model/AssetBundle loading.
- Android identity preserved: vn.shg.mobi.mongvolam, version 10.0.0; upgrade build code starts at 101.
- Original APK UI orientation preserved as portrait; autorotation to landscape is disabled.
- Android minimum raised from API 21 to API 23 because Unity 6 requires Android 6.0+.
- Android backend: IL2CPP, ARMv7 + ARM64.
- NGUI anchors respect Screen.safeArea on modern cutout/notch devices while keeping the original 720-height layout.
