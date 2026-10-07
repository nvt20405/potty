# Mộng Võ Lâm — Unity 6.3 migration status

Target branch: `mvl-unity6-recovery`  
Unity Editor: `6000.3.25f1`  
Project: `unity-project/`

## Recovered project

The source APK was recovered with AssetRipper and decompiled Mono sources were merged back while retaining recovered Unity script GUIDs.

Current static validation:

- Serialized GUID references unresolved: **0**
- Character config: **240 expected / 0 missing**
- Costume config: **35 expected / 0 missing**
- Weapon model classification: **4 intentionally hidden / 1 fallback / 0 broken**
- Literal Resources paths: **0 case mismatches / 0 required missing**
- The Nien Thu popup resource warning is from an orphan recovered script with no code or serialized references.
- Legacy `fndid` injection-signature data is intentionally treated as incompatible with the rebuilt assemblies; only the obsolete injection detector disables itself.
- Legacy HTML white-atlas drawing has a runtime-generated texture fallback.

## Unity 6 / Android migration

- Unity 4 component shortcuts and removed APIs migrated.
- Legacy WWW/AssetBundle requests bridged to UnityWebRequest.
- ProudNet client recovered as managed C# networking code.
- Android package: `vn.shg.mobi.mongvolam`
- Portrait orientation retained from the source APK.
- Minimum Android version: **API 25 / Android 7.1**, matching Unity 6.3.
- Scripting backend: **IL2CPP**
- Architectures: **ARMv7 + ARM64**
- Managed stripping: **Minimal**
- `Assets/link.xml` preserves `Assembly-CSharp` for reflection-heavy LitJson/RMI behavior.

## UI migration

The recovered main scene uses a fixed NGUI design canvas of approximately **640 x 1136**.

The migration keeps the original game artwork and layout, while adding:

- `Screen.safeArea` handling for notches/cutouts.
- Adaptive fit-width behavior on tall portrait displays (20:9 / 21:9) so the full 640-wide legacy UI remains visible instead of being horizontally cropped.
- Original 16:9 scale remains unchanged.

## Validation / build workflows

- `.github/workflows/recover-mvl.yml`: recover + migrate + validate + push project.
- `.github/workflows/compile-unity6-scripts.yml`: compile recovered scripts against Unity 6 references without requiring an Editor license.
- `.github/workflows/build-unity6-android.yml`: full Android APK build with Unity/GameCI.

The full Unity Android build requires a valid Unity activation configured as GitHub Actions secrets. The workflow supports:

- `UNITY_LICENSE`, or
- `UNITY_EMAIL` + `UNITY_PASSWORD` (and `UNITY_SERIAL` for applicable paid seats), or
- `UNITY_LICENSING_SERVER`.

Do not commit Unity credentials or license contents to the repository.

Successful builds upload the APK artifact as `MongVoLam-Unity6-Android`.
