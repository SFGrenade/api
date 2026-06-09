using Mono.Cecil.Cil;
using MonoMod;
using MonoMod.Cil;
// ReSharper disable All
#pragma warning disable 1591, 0108, 0169, 0649, 0414
#pragma warning disable CS0649, CS0626
namespace Modding.Patches
{
    [MonoModPatch("global::EnemyDeathEffects")]
    public class EnemyDeathEffects : global::EnemyDeathEffects
    {
        [MonoModIgnore]
        [Attributes.RawIlPatch(nameof(IlPatches.EnemyDeathEffects_Die))]
        extern public void Die(float? attackDirection, AttackTypes attackType, bool ignoreEvasion);

        [MonoModIgnore]
        [Attributes.RawIlPatch(nameof(IlPatches.EnemyDeathEffects_RecordKillForJournal))]
        extern public static void RecordKillForJournal(string playerDataName);
    }

    [MonoModIgnore]
    public static partial class IlPatches
    {
        [MonoModIgnore]
        public static void EnemyDeathEffects_Die(ILContext il)
        {
            ILCursor cursor = new ILCursor(il);
            cursor.GotoNext(MoveType.AfterLabel, x => x.MatchLdarg(0));
            cursor.Emit(OpCodes.Ldarg_0);
            cursor.EmitDelegate(global::Modding.ModHooks.OnAfterEnemyDeath);
        }

        [MonoModIgnore]
        public static void EnemyDeathEffects_RecordKillForJournal(ILContext il)
        {
            ILCursor cursor = new ILCursor(il);
            cursor.GotoNext(MoveType.AfterLabel, x => x.MatchLdcI4(0));
            cursor.Emit(OpCodes.Ldarg_0);
            cursor.Emit(OpCodes.Ldarg_0);
            cursor.Emit(OpCodes.Ldfld, ReflectionHelper.GetFieldInfo(typeof(global::EnemyDeathEffects), "playerDataName", true));
            cursor.Emit(OpCodes.Ldloc_1);
            cursor.Emit(OpCodes.Ldloc_2);
            cursor.Emit(OpCodes.Ldloc_3);
            cursor.EmitDelegate(global::Modding.ModHooks.OnRecordKillForJournal);
        }
    }
}