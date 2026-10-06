# Mộng Võ Lâm — Unity 6 migration

- Target Editor: Unity 6000.3.25f1 LTS
- Render pipeline: Built-in (keeps legacy NGUI/material/shader behavior)
- Decompiled Mono source merged over AssetRipper dummy scripts while retaining original script GUIDs.
- Unity 4 component shortcuts and scene loading APIs updated.
- Legacy WWW calls bridged to UnityWebRequest/UnityWebRequestAssetBundle for cached model/AssetBundle loading.
