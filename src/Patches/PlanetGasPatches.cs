﻿using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;

namespace ProjectGenesis.Patches
{
    public static class PlanetGasPatches
    {
        [HarmonyPatch(typeof(UIPlanetDetail), "OnPlanetDataSet")]
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> UIPlanetDetail_OnPlanetDataSet_Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var matcher = new CodeMatcher(instructions).MatchForward(true,
                                                                     new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(PlanetData), "type")),
                                                                     new CodeMatch(OpCodes.Ldc_I4_5), new CodeMatch(OpCodes.Beq));

            var label = matcher.Operand;

            matcher.MatchForward(false, new CodeMatch(OpCodes.Ldstr, "实际采集速度"));

            matcher.Advance(-10).SetOperandAndAdvance(label);
            matcher.Advance(28).SetOperandAndAdvance(label);

            return matcher.InstructionEnumeration();
        }
        
        [HarmonyPatch(typeof(UIStarDetail), "OnStarDataSet")]
        [HarmonyPatch(typeof(PlanetGen), "SetPlanetTheme")]
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> PlanetGen_SetPlanetTheme_Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var matcher = new CodeMatcher(instructions).MatchForward(false, 
                                                                     new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(PlanetData), "type")),
                                                                     new CodeMatch(OpCodes.Ldc_I4_5), new CodeMatch(OpCodes.Bne_Un));
            matcher.Advance(-1);
            matcher.SetAndAdvance(OpCodes.Nop, null);
            matcher.SetAndAdvance(OpCodes.Nop, null);
            matcher.SetAndAdvance(OpCodes.Nop, null);
            matcher.SetAndAdvance(OpCodes.Nop, null);
            return matcher.InstructionEnumeration();
        }

        [HarmonyPatch(typeof(BuildTool_BlueprintCopy), "DetermineActive")]
        [HarmonyPatch(typeof(BuildTool_BlueprintPaste), "DetermineActive")]
        [HarmonyPatch(typeof(BuildTool_BlueprintPaste), "CheckBuildConditionsPrestage")]
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> BuildTool_Blueprint_DetermineActive_Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var matcher = new CodeMatcher(instructions).MatchForward(true, new CodeMatch(OpCodes.Ldarg_0),
                                                                     new CodeMatch(OpCodes.Ldfld,
                                                                                   AccessTools.Field(typeof(BuildTool), nameof(BuildTool.planet))),
                                                                     new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(PlanetData), "gasItems")));

            matcher.SetOperandAndAdvance(AccessTools.Field(typeof(PlanetData), "type"));
            matcher.InsertAndAdvance(new CodeInstruction(OpCodes.Ldc_I4_5));
            matcher.SetOpcodeAndAdvance(matcher.Opcode == OpCodes.Brfalse_S ? OpCodes.Bne_Un_S : OpCodes.Bne_Un);

            matcher.SetAndAdvance(OpCodes.Nop, null);
            matcher.SetAndAdvance(OpCodes.Nop, null);
            matcher.SetAndAdvance(OpCodes.Nop, null);
            matcher.SetAndAdvance(OpCodes.Nop, null);
            matcher.SetAndAdvance(OpCodes.Nop, null);

            return matcher.InstructionEnumeration();
        }

        [HarmonyPatch(typeof(PlayerController), "OpenBlueprintCopyMode")]
        [HarmonyPatch(typeof(PlayerController), "OpenBlueprintPasteMode")]
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> PlayerController_OpenBlueprintMode_Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var matcher = new CodeMatcher(instructions).MatchForward(true, new CodeMatch(OpCodes.Ldarg_0),
                                                                     new CodeMatch(OpCodes.Ldfld,
                                                                                   AccessTools.Field(typeof(PlayerController), "gameData")),
                                                                     new CodeMatch(OpCodes.Callvirt,
                                                                                   AccessTools.PropertyGetter(typeof(GameData), "localPlanet")),
                                                                     new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(PlanetData), "gasItems")));

            matcher.SetOperandAndAdvance(AccessTools.Field(typeof(PlanetData), "type"));

            var label = matcher.Operand;
            var isS = matcher.Opcode == OpCodes.Brfalse_S;

            matcher.SetAndAdvance(OpCodes.Ldc_I4_5, null);
            matcher.SetAndAdvance(isS ? OpCodes.Bne_Un_S : OpCodes.Bne_Un, label);

            matcher.SetAndAdvance(OpCodes.Nop, null);
            matcher.SetAndAdvance(OpCodes.Nop, null);
            matcher.SetAndAdvance(OpCodes.Nop, null);
            matcher.SetAndAdvance(OpCodes.Nop, null);
            matcher.SetAndAdvance(OpCodes.Nop, null);

            return matcher.InstructionEnumeration();
        }

        [HarmonyPatch(typeof(BuildTool_Click), "CheckBuildConditions")]
        [HarmonyPatch(typeof(BuildTool_BlueprintPaste), "CheckBuildConditions")]
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> BuildTool_Click_CheckBuildConditions_Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var matcher = new CodeMatcher(instructions);

            matcher.MatchForward(false, new CodeMatch(OpCodes.Ldloc_S), new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(BuildPreview), "desc")),
                                 new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(PrefabDesc), "isCollectStation")),
                                 new CodeMatch(OpCodes.Brfalse), new CodeMatch(OpCodes.Ldarg_0),
                                 new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(BuildTool), "planet")), new CodeMatch(OpCodes.Brfalse));

            var preview = matcher.Operand;

            matcher.Advance(9);
            matcher.SetAndAdvance(OpCodes.Ldloc_S, preview);
            matcher.SetAndAdvance(OpCodes.Call, AccessTools.Method(typeof(PlanetGasPatches), nameof(IsSuit)));

            matcher.SetAndAdvance(OpCodes.Nop, null);
            matcher.SetAndAdvance(OpCodes.Nop, null);
            matcher.SetAndAdvance(OpCodes.Nop, null);
            matcher.SetAndAdvance(OpCodes.Nop, null);

            return matcher.InstructionEnumeration();
        }

        public static bool IsSuit(PlanetData planet, BuildPreview preview) => preview.item.ID == (planet.type == EPlanetType.Gas ? 2105 : 6267);
    }
}
