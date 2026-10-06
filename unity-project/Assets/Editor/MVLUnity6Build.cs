#if UNITY_EDITOR
using System;
using System.IO;
using System.Linq;
using UnityEditor;
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
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64 | AndroidArchitecture.ARMv7;

        string[] scenes = EditorBuildSettings.scenes.Where(s => s.enabled).Select(s => s.path).ToArray();
        if (scenes.Length == 0)
            throw new BuildFailedException("No enabled scenes in EditorBuildSettings.");

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
