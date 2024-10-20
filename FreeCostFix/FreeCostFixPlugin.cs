using BepInEx;
using RoR2;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace FreeCostFix
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(R2API.R2API.PluginGUID)]
    public class FreeCostFixPlugin : BaseUnityPlugin
    {
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "Gorakh";
        public const string PluginName = "FreeCostFix";
        public const string PluginVersion = "1.0.0";

        internal static FreeCostFixPlugin Instance { get; private set; }

        void Awake()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            Instance = SingletonHelper.Assign(Instance, this);

            Log.Init(Logger);

            AsyncOperationHandle<GameObject> costHologramContentLoad = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Common/VFX/CostHologramContent.prefab");
            costHologramContentLoad.Completed += handle =>
            {
                if (!handle.Result)
                {
                    Log.Error("Failed to load CostHologramContent prefab");
                    return;
                }

                if (handle.Result.TryGetComponent(out CostHologramContent costHologramContent))
                {
                    if (costHologramContent.targetTextMesh)
                    {
                        costHologramContent.targetTextMesh.text = string.Empty;
                    }

                    RefreshDisplayOnAwake refreshDisplayOnAwake = costHologramContent.gameObject.AddComponent<RefreshDisplayOnAwake>();
                    refreshDisplayOnAwake.HologramContent = costHologramContent;
                }
                else
                {
                    Log.Error("CostHologramContent is missing component");
                }
            };

            stopwatch.Stop();
            Log.Message_NoCallerPrefix($"Initialized in {stopwatch.Elapsed.TotalMilliseconds:F0}ms");
        }

        void OnDestroy()
        {
            Instance = SingletonHelper.Unassign(Instance, this);
        }
    }
}
