﻿using System.Collections.Generic;
using System.Reflection.Emit;
using BepInEx;
using BepInEx.Bootstrap;
using HarmonyLib;
using ProjectGenesis.Patches.Logic;
using ProjectGenesis.Utils;
using UnityEngine;

// ReSharper disable CommentTypo
// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBeInternal
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace ProjectGenesis.Compatibility
{
    [BepInPlugin(MODGUID, MODNAME, VERSION)]
    [BepInDependency(GalacticScaleGUID)]
    public class GalacticScaleCompatibilityPlugin : BaseUnityPlugin
    {
        public const string MODGUID = "org.LoShin.GenesisBook.Compatibility.GalacticScale";
        public const string MODNAME = "GenesisBook.Compatibility.GalacticScale";
        public const string VERSION = "1.0.0";

        private const string GalacticScaleGUID = "dsp.galactic-scale.2";

        public void Awake()
        {
            Chainloader.PluginInfos.TryGetValue(GalacticScaleGUID, out var pluginInfo);

            if (pluginInfo == null) return;

            var assembly = pluginInfo.Instance.GetType().Assembly;
            var harmony = new Harmony(MODGUID);

            harmony.Patch(AccessTools.Method(assembly.GetType("GalacticScale.GSTheme"), "ToProto"), null,
                          new HarmonyMethod(typeof(GalacticScaleCompatibilityPlugin), nameof(GSTheme_ToProto_Postfix)));

            harmony.Patch(AccessTools.Method(assembly.GetType("GalacticScale.GS2"), "SetPlanetTheme"), null,
                          new HarmonyMethod(typeof(GalacticScaleCompatibilityPlugin), nameof(SetPlanetTheme_Postfix)));

            harmony.Patch(AccessTools.Method(assembly.GetType("GalacticScale.PatchOnUIPlanetDetail"), "OnPlanetDataSet7Prefix"), null, null,
                          new HarmonyMethod(typeof(GalacticScaleCompatibilityPlugin), nameof(OnPlanetDataSet_Transpiler)));

            harmony.Patch(AccessTools.Method(assembly.GetType("GalacticScale.PatchOnUIStarDetail"), "OnStarDataSet2"), null, null,
                          new HarmonyMethod(typeof(PlanetGasPatches), nameof(PlanetGasPatches.PlanetGen_SetPlanetTheme_Transpiler)));

            harmony.Patch(AccessTools.Method(assembly.GetType("GalacticScale.PatchOnUIStarDetail"), "OnStarDataSet2"), null, null,
                          new HarmonyMethod(typeof(PlanetGasPatches), nameof(PlanetGasPatches.OnStarDataSet_Transpiler)));
        }

        public static void SetPlanetTheme_Postfix(PlanetData planet)
        {
            if (planet.type != EPlanetType.Gas)
            {
                var gsTheme = LDB.themes.Select(planet.theme);

                var length1 = gsTheme.GasItems.Length;
                var length2 = gsTheme.GasSpeeds.Length;

                var numArray1 = new int[length1];
                var numArray2 = new float[length2];

                for (var index = 0; index < length1; ++index) numArray1[index] = gsTheme.GasItems[index];

                var num1 = new DotNet35Random().NextDouble();

                for (var index = 0; index < length2; ++index)
                {
                    var num5 = gsTheme.GasSpeeds[index] * (float)(num1 * 0.190909147262573 + 0.909090876579285);
                    numArray2[index] = num5 * Mathf.Pow(planet.star.resourceCoef, 0.3f);
                }

                planet.gasItems = numArray1;
                planet.gasSpeeds = numArray2;
                planet.gasHeatValues = new float[planet.gasItems.Length];
                planet.gasTotalHeat = 0;
            }
        }

        public static void GSTheme_ToProto_Postfix(ThemeProto __result) => PlanetThemeUtils.AdjustTheme(__result);

        public static IEnumerable<CodeInstruction> OnPlanetDataSet_Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var matcher = new CodeMatcher(instructions).MatchForward(true,
                                                                     new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(PlanetData), "type")),
                                                                     new CodeMatch(OpCodes.Ldc_I4_5), new CodeMatch(OpCodes.Ceq),
                                                                     new CodeMatch(OpCodes.Ldc_I4_0), new CodeMatch(OpCodes.Ceq),
                                                                     new CodeMatch(OpCodes.Stloc_S), new CodeMatch(OpCodes.Ldloc_S),
                                                                     new CodeMatch(OpCodes.Brfalse));

            var label = matcher.Operand;

            matcher.MatchForward(false, new CodeMatch(OpCodes.Ldstr, "实际采集速度"));

            matcher.Advance(-14).SetOperandAndAdvance(label);
            matcher.Advance(39).SetOperandAndAdvance(label);

            matcher.MatchForward(false, new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(StationComponent), "collectionPerTick")));

            var stationComponent = matcher.Advance(-1).Instruction;

            matcher.Advance(-1).InsertAndAdvance(new CodeInstruction(stationComponent), new CodeInstruction(OpCodes.Ldarg_0),
                                                 new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(UIPlanetDetail), "_planet")),
                                                 new CodeInstruction(OpCodes.Call,
                                                                     AccessTools.Method(typeof(PlanetGasPatches),
                                                                                        nameof(PlanetGasPatches.GetGasCollectionPerTick))));

            return matcher.InstructionEnumeration();
        }
    }
}
