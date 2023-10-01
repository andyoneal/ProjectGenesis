﻿using System;
using System.Collections.Generic;
using System.Linq;
using ProjectGenesis.Compatibility;

// ReSharper disable LoopCanBePartlyConvertedToQuery

namespace ProjectGenesis.Utils
{
    internal static class PlanetThemeUtils
    {
        private static readonly Dictionary<int, int[]> PlanetGasData = new Dictionary<int, int[]>
                                                                       {
                                                                           { 1, new[] { 6220, 7019 } },
                                                                           { 6, new[] { 6206 } },
                                                                           { 7, new[] { 6220 } },
                                                                           { 8, new[] { 6220, 7019 } },
                                                                           { 9, new[] { 6206, 6220 } },
                                                                           { 10, new[] { 6220 } },
                                                                           { 12, new[] { 6206 } },
                                                                           { 13, new[] { 6206, 6220 } },
                                                                           { 14, new[] { 6220, 7019 } },
                                                                           { 15, new[] { 6220, 7019 } },
                                                                           { 16, new[] { 6220, 7019 } },
                                                                           { 17, new[] { 6220 } },
                                                                           { 18, new[] { 6220, 7019 } },
                                                                           { 19, new[] { 6220 } },
                                                                           { 20, new[] { 6220, 7002 } },
                                                                           { 22, new[] { 6220, 7019 } },
                                                                           { 23, new[] { 6206 } },
                                                                           { 24, new[] { 6220, 6205 } },
                                                                           { 25, new[] { 6220, 7019 } }
                                                                       };

        internal static void AdjustPlanetThemeDataVanilla()
        {
            foreach (ThemeProto theme in LDB.themes.dataArray) AdjustTheme(theme);

            ThemeProto gobi = LDB.themes.Select(12);
            gobi.WaterItemId = 7017;
            gobi.WaterHeight = -0.1f;
            gobi.Distribute = EThemeDistribute.Interstellar;
            gobi.oceanMat = LDB.themes.Select(22).oceanMat;

            ThemeProto oceanicJungle = LDB.themes.Select(8);
            oceanicJungle.WaterItemId = 1000;
            oceanicJungle.Distribute = EThemeDistribute.Interstellar;

            ThemeProto rockySalt = LDB.themes.Select(17);
            rockySalt.WaterItemId = 7014;
            rockySalt.WaterHeight = -0.1f;
            rockySalt.Distribute = EThemeDistribute.Interstellar;
            rockySalt.Algos = new[] { 3 };
            rockySalt.oceanMat = LDB.themes.Select(8).oceanMat;

            ThemeProto ice = LDB.themes.Select(10);
            ice.WaterItemId = 7002;
            ice.Distribute = EThemeDistribute.Interstellar;
        }

        internal static void AdjustTheme(ThemeProto theme)
        {
            void TerrestrialAdjust()
            {
                if (theme.WaterItemId == 1000) theme.WaterItemId = 7018;

                // for GalacticScale mod
                if (GalacticScaleCompatibilityPlugin.GalacticScaleInstalled)
                {
                    if (theme.name == "OceanicJungle") theme.WaterItemId = 1000;

                    if (theme.name.StartsWith("IceGelisol")) theme.WaterItemId = 7002;

                    if (theme.name == "SaltLake")
                    {
                        theme.WaterItemId = 7014;
                        theme.WaterHeight = -0.1f;
                        theme.Algos = new[] { 3 };
                        theme.oceanMat = LDB.themes.Select(8).oceanMat;
                    }

                    if (theme.name == "Gobi")
                    {
                        theme.WaterItemId = 7017;
                        theme.WaterHeight = -0.1f;
                        theme.oceanMat = LDB.themes.Select(22).oceanMat;
                    }
                }

                if (theme.Distribute == EThemeDistribute.Birth)
                {
                    theme.RareVeins = new[] { 8 };
                    theme.RareSettings = new float[] { 1.0f, 0.5f, 0.0f, 0.4f };
                }

                if (theme.Wind == 0)
                {
                    theme.GasItems = Array.Empty<int>();
                    theme.GasSpeeds = Array.Empty<float>();
                }
                else if (PlanetGasData.TryGetValue(theme.ID, out int[] value))
                {
                    theme.GasItems = value;
                    theme.GasSpeeds = theme.GasItems.Length == 1
                                          ? new float[] { theme.Wind * 0.7f }
                                          : new float[] { theme.Wind * 0.7f, theme.Wind * 0.18f };
                }
                else if (theme.GasItems == null || theme.GasItems.Length == 0)
                {
                    switch (theme.PlanetType)
                    {
                        case EPlanetType.Ocean:
                            theme.GasItems = new[] { 6220, 7019 };
                            theme.GasSpeeds = new float[] { theme.Wind * 0.7f, theme.Wind * 0.18f };
                            break;

                        default:
                            theme.GasItems = new[] { 6206 };
                            theme.GasSpeeds = new float[] { theme.Wind * 0.7f };
                            break;
                    }
                }

                AdjustVeins(theme);
            }

            void GasGiantAdjust()
            {
                if (theme.GasItems.Length != 2) return;

                if (theme.GasItems[0] == 1011 && theme.GasItems[1] == 1120)
                {
                    theme.GasItems = new[] { 1011, 1120, 7002 };
                    theme.GasSpeeds = new float[] { theme.GasSpeeds[0], theme.GasSpeeds[1], theme.GasSpeeds[1] * 0.7f };
                }
                else if (theme.GasItems[0] == 1120 && theme.GasItems[1] == 1121)
                {
                    theme.GasItems = new[] { 1120, 1121, 6234 };
                    theme.GasSpeeds = new float[] { theme.GasSpeeds[0], theme.GasSpeeds[1], theme.GasSpeeds[1] * 0.5f };
                }
            }

            if (theme.PlanetType == EPlanetType.Gas)
                GasGiantAdjust();
            else
                TerrestrialAdjust();
        }

        private static void AdjustVeins(ThemeProto theme)
        {
            if (theme.VeinSpot.Length > 2)
            {
                ref int silicon = ref theme.VeinSpot[2];

                if (silicon > 0)
                {
                    silicon = 1 + silicon / 4;
                    theme.VeinCount[2] = 0.5f;
                    theme.VeinOpacity[2] = 0.5f;
                }
            }

            if (theme.VeinSpot.Length > 5)
            {
                ref int coal = ref theme.VeinSpot[5];

                if (!theme.GasItems.Contains(7019))
                {
                    coal = 0;
                    theme.VeinCount[5] = 0f;
                    theme.VeinOpacity[5] = 0f;
                }
                else
                {
                    coal += 1;
                    theme.VeinCount[5] *= 1.2f;
                }
            }
        }
    }
}
