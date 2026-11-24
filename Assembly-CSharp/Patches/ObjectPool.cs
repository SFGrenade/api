using System;
using MonoMod;
using UnityEngine;

// ReSharper disable All
#pragma warning disable 1591, 0108, 0169, 0649, 0414,0626

namespace Modding.Patches
{
    [MonoModPatch("global::ObjectPool")]
    public static class ObjectPool
    {
        private static extern GameObject orig_Spawn
        (
            GameObject prefab,
            Transform parent,
            Vector3 position,
            Quaternion rotation
        );

        public static GameObject Spawn(GameObject prefab, Transform parent, Vector3 position, Quaternion rotation)
        {
            try
            {
                GameObject obj = orig_Spawn(prefab, parent, position, rotation);
                return ModHooks.OnObjectPoolSpawn(obj);
            }
            catch (NullReferenceException) when (!ModLoader.LoadState.HasFlag(ModLoader.ModLoadState.Preloaded))
            {
                return null;
            }
        }

        public static extern void orig_CreatePool(GameObject prefab, int initialPoolSize, bool runInitialisation = false);
        public static void CreatePool(GameObject prefab, int initialPoolSize, bool runInitialisation = false)
        {
            try
            {
                orig_CreatePool(prefab, initialPoolSize, runInitialisation);
            }
            catch (NullReferenceException) when (!ModLoader.LoadState.HasFlag(ModLoader.ModLoadState.Preloaded))
            { }
        }
    }
}