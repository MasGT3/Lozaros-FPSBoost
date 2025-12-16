using BepInEx;
using System;
using System.ComponentModel;
using UnityEngine;

namespace Lozaros_FPSBooster.Patches
{
    [Description(Lozaros_FPSBooster.PluginInfo.Description)]
    [BepInPlugin(Lozaros_FPSBooster.PluginInfo.GUID, Lozaros_FPSBooster.PluginInfo.Name, Lozaros_FPSBooster.PluginInfo.Version)]
    public class HarmonyPatches : BaseUnityPlugin
    {
        private static bool fpsBoostApplied = false;
        private static float lastCacheClearedTime = 0f;

        private void Awake()
        {
            FPSboost();
        }

        private void Update()
        {
            if (!fpsBoostApplied)
            {
                FPSboost();
            }

            AutoClearCache();
        }

        public static void FPSboost()
        {
            QualitySettings.globalTextureMipmapLimit = 999999999;
            QualitySettings.maxQueuedFrames = 60;
            Application.targetFrameRate = 144;
            fpsBoostApplied = true;
        }

        public static void AutoClearCache()
        {
            if (Time.time > lastCacheClearedTime)
            {
                lastCacheClearedTime = Time.time + 60f;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                Resources.UnloadUnusedAssets();
            }
        }
    }
}