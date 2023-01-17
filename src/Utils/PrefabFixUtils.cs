﻿using System.Collections.Generic;
using System.Linq;
using ProjectGenesis.Patches;
using UnityEngine;
using ERecipeType_1 = ERecipeType;

namespace ProjectGenesis.Utils
{
    internal static class PrefabFixUtils
    {
        internal static void ItemPostFix(ItemProtoSet itemProtos)
        {
            LDB.items.OnAfterDeserialize();

            itemProtos.Select(2317).prefabDesc.assemblerRecipeType = (ERecipeType_1)ERecipeType.高分子化工;
            itemProtos.Select(2317).prefabDesc.idleEnergyPerTick = 800;
            itemProtos.Select(2317).prefabDesc.workEnergyPerTick = 24000;

            itemProtos.Select(物品.二级制造台).prefabDesc.assemblerRecipeType = (ERecipeType_1)ERecipeType.电路蚀刻;
            itemProtos.Select(物品.三级制造台).prefabDesc.assemblerRecipeType = (ERecipeType_1)ERecipeType.高精度加工;

            itemProtos.Select(物品.一级制造台).prefabDesc.assemblerSpeed = 20000;
            itemProtos.Select(物品.一级制造台).prefabDesc.idleEnergyPerTick *= 2;
            itemProtos.Select(物品.一级制造台).prefabDesc.workEnergyPerTick *= 2;

            itemProtos.Select(物品.二级制造台).prefabDesc.assemblerSpeed = 20000;
            itemProtos.Select(物品.二级制造台).prefabDesc.idleEnergyPerTick *= 2;
            itemProtos.Select(物品.二级制造台).prefabDesc.workEnergyPerTick *= 2;

            itemProtos.Select(物品.三级制造台).prefabDesc.assemblerSpeed = 20000;
            itemProtos.Select(物品.三级制造台).prefabDesc.idleEnergyPerTick *= 2;
            itemProtos.Select(物品.三级制造台).prefabDesc.workEnergyPerTick *= 2;

            itemProtos.Select(物品.位面熔炉).prefabDesc.assemblerSpeed = 40000;
            itemProtos.Select(物品.化工厂).prefabDesc.assemblerSpeed = 20000;
            itemProtos.Select(物品.原油精炼厂).prefabDesc.assemblerSpeed = 20000;
            itemProtos.Select(物品.粒子对撞机).prefabDesc.assemblerSpeed = 40000;

            itemProtos.Select(物品.采矿机).prefabDesc.minerPeriod = 600000;
            itemProtos.Select(物品.大型采矿机).prefabDesc.minerPeriod = 300000;
            itemProtos.Select(物品.原油采集站).prefabDesc.minerPeriod = 300000;
            itemProtos.Select(物品.水泵).prefabDesc.minerPeriod = 360000;

            itemProtos.Select(物品.水泵).prefabDesc.waterTypes = new[] { 1000, 1116, 7018 };

            itemProtos.Select(物品.电磁轨道弹射器).prefabDesc.ejectorChargeFrame = 20;
            itemProtos.Select(物品.电磁轨道弹射器).prefabDesc.ejectorColdFrame = 10;

            itemProtos.Select(物品.垂直发射井).prefabDesc.siloChargeFrame = 24;
            itemProtos.Select(物品.垂直发射井).prefabDesc.siloColdFrame = 6;

            itemProtos.Select(物品.卫星配电站).prefabDesc.powerConnectDistance = 5300.5f;
            itemProtos.Select(物品.卫星配电站).prefabDesc.powerCoverRadius = 2600.5f;
            itemProtos.Select(物品.电力感应塔).prefabDesc.powerConnectDistance = 44.5f;
            itemProtos.Select(物品.电力感应塔).prefabDesc.powerCoverRadius = 20.5f;
            itemProtos.Select(物品.风力涡轮机).prefabDesc.powerConnectDistance = 32.5f;
            itemProtos.Select(物品.风力涡轮机).prefabDesc.powerCoverRadius = 14.9f;

            itemProtos.Select(物品.火力发电机).prefabDesc.genEnergyPerTick = 200000;
            itemProtos.Select(物品.火力发电机).prefabDesc.useFuelPerTick = 250000;

            itemProtos.Select(物品.风力涡轮机).prefabDesc.genEnergyPerTick = 25000;
            itemProtos.Select(物品.太阳能板).prefabDesc.genEnergyPerTick = 30000;
            itemProtos.Select(物品.地热发电机).prefabDesc.genEnergyPerTick = 400000;

            itemProtos.Select(物品.聚变发电机).prefabDesc.genEnergyPerTick = 2500000;
            itemProtos.Select(物品.聚变发电机).prefabDesc.useFuelPerTick = 2500000;

            itemProtos.Select(物品.人造恒星).prefabDesc.genEnergyPerTick = 10000000;
            itemProtos.Select(物品.人造恒星).prefabDesc.useFuelPerTick = 10000000;


            itemProtos.Select(物品.低速传送带).prefabDesc.beltSpeed = 3;
            itemProtos.Select(物品.高速传送带).prefabDesc.beltSpeed = 5;
            itemProtos.Select(物品.极速传送带).prefabDesc.beltSpeed = 10;

            itemProtos.Select(物品.低速分拣器).prefabDesc.inserterSTT = 100000;
            itemProtos.Select(物品.高速分拣器).prefabDesc.inserterSTT = 50000;
            itemProtos.Select(物品.极速分拣器).prefabDesc.inserterSTT = 25000;

            itemProtos.Select(物品.卫星配电站).prefabDesc.idleEnergyPerTick = 12000000;
            itemProtos.Select(物品.卫星配电站).prefabDesc.workEnergyPerTick = 48000000;

            itemProtos.Select(物品.电弧熔炉).prefabDesc.assemblerSpeed = 20000;
            itemProtos.Select(物品.电弧熔炉).prefabDesc.idleEnergyPerTick = itemProtos.Select(物品.电弧熔炉).prefabDesc.idleEnergyPerTick * 2;
            itemProtos.Select(物品.电弧熔炉).prefabDesc.workEnergyPerTick = itemProtos.Select(物品.电弧熔炉).prefabDesc.workEnergyPerTick * 2;
            itemProtos.Select(物品.无线输电塔).prefabDesc.powerConnectDistance = 90.5f;

            itemProtos.Select(6230).prefabDesc.assemblerRecipeType = (ERecipeType_1)ERecipeType.矿物处理;
            itemProtos.Select(6230).prefabDesc.idleEnergyPerTick = 400;
            itemProtos.Select(6230).prefabDesc.workEnergyPerTick = 12000;

            itemProtos.Select(6229).prefabDesc.fluidStorageCount = 1000000;

            itemProtos.Select(6275).prefabDesc.assemblerSpeed = 500;
            itemProtos.Select(6275).prefabDesc.workEnergyPerTick = 2000000;
            itemProtos.Select(6275).prefabDesc.idleEnergyPerTick = 100000;
            itemProtos.Select(6275).prefabDesc.assemblerRecipeType = (ERecipeType_1)ERecipeType.精密组装;

            itemProtos.Select(6276).prefabDesc.assemblerSpeed = 1000;
            itemProtos.Select(6276).prefabDesc.workEnergyPerTick = 8000000;
            itemProtos.Select(6276).prefabDesc.idleEnergyPerTick = 200000;
            itemProtos.Select(6276).prefabDesc.assemblerRecipeType = (ERecipeType_1)ERecipeType.聚变生产;

            //矿场修复
            var oreFactory = LDB.items.Select(6230);
            var oreFactoryModel = LDB.items.Select(302);
            oreFactoryModel.prefabDesc.dragBuild = true;
            oreFactoryModel.prefabDesc.dragBuildDist = new Vector2(2.9f, 2.9f);
            oreFactory.prefabDesc.dragBuild = true;
            oreFactory.prefabDesc.dragBuildDist = new Vector2(2.9f, 2.9f);
        }

        internal static void ModelPostFix(ModelProtoSet models)
        {
            // 天穹装配厂
            var testCraftingTableModel = models.Select(453);
            SetMegaAssemberPrefebDesc(ref testCraftingTableModel.prefabDesc, ERecipeType.Assemble);

            // 物质裂解塔
            var testCraftingTableModel2 = models.Select(454);
            SetMegaAssemberPrefebDesc(ref testCraftingTableModel2.prefabDesc, ERecipeType.所有熔炉);

            // 巨型化学反应釜
            var testCraftingTableModel3 = models.Select(455);
            SetMegaAssemberPrefebDesc(ref testCraftingTableModel3.prefabDesc, ERecipeType.所有化工);

            // 精密结构组装厂
            var testCraftingTableModel4 = models.Select(456);
            SetMegaAssemberPrefebDesc(ref testCraftingTableModel4.prefabDesc, ERecipeType.所有高精);

            // 物质分解设施
            var testCraftingTableModel5 = models.Select(460);
            SetMegaAssemberPrefebDesc(ref testCraftingTableModel5.prefabDesc, ERecipeType.垃圾回收, 200000, 1000000, MegaAssemblerPatches.TrashSpeed);

            // 巨型粒子对撞机
            var testCraftingTableModel6 = models.Select(461);
            SetMegaAssemberPrefebDesc(ref testCraftingTableModel6.prefabDesc, ERecipeType.Particle, 200000, 1000000);

            LDB.items.Select(2211).prefabDesc.fuelMask = 5;
            LDB.items.Select(2210).prefabDesc.fuelMask = 6;

            var antiMatterModel = models.Select(457);
            ref var antiMatterModelprefabDesc = ref antiMatterModel.prefabDesc;
            antiMatterModelprefabDesc.fuelMask = 4;
            antiMatterModelprefabDesc.genEnergyPerTick = 1200000000;
            antiMatterModelprefabDesc.useFuelPerTick = 1200000000;

            var megapumper = models.Select(462);
            ref var megapumperprefabDesc = ref megapumper.prefabDesc;
            megapumperprefabDesc.isFractionator = false;
            megapumperprefabDesc.assemblerRecipeType = ERecipeType_1.None;
            megapumperprefabDesc.assemblerSpeed = 0;
            megapumperprefabDesc.minerPeriod = 72000;
            megapumperprefabDesc.minerType = EMinerType.Water;
            megapumperprefabDesc.minimapType = 2;
            megapumperprefabDesc.idleEnergyPerTick = 10000;
            megapumperprefabDesc.workEnergyPerTick = 25000;
            megapumperprefabDesc.waterPoints = new[] { Vector3.zero };
            megapumperprefabDesc.waterTypes = new[] { 1000, 1116, 7018 };

            List<Pose> poses = megapumperprefabDesc.portPoses.ToList();
            poses.Add(new Pose(new Vector3(0, 0, -1.4f), Quaternion.Euler(0, 180, 0)));
            megapumperprefabDesc.portPoses = poses.ToArray();

            var gaspumper = models.Select(463);
            ref var gaspumperprefabDesc = ref gaspumper.prefabDesc;
            gaspumperprefabDesc.workEnergyPerTick = 0;
            gaspumperprefabDesc.isCollectStation = true;
            gaspumperprefabDesc.isPowerConsumer = false;
            gaspumperprefabDesc.stationCollectSpeed = 2;

            var accumulator = models.Select(46);
            ref var accumulatorprefabDesc = ref accumulator.prefabDesc;
            accumulatorprefabDesc.maxAcuEnergy = 2700000000;
            accumulatorprefabDesc.inputEnergyPerTick = 150000;
            accumulatorprefabDesc.outputEnergyPerTick = 150000;

            var energyexchanger = models.Select(45);
            ref var energyexchangerprefabDesc = ref energyexchanger.prefabDesc;
            energyexchangerprefabDesc.maxAcuEnergy = 2700000000;
            energyexchangerprefabDesc.maxExcEnergy = 2700000000;
            energyexchangerprefabDesc.exchangeEnergyPerTick = 10000000;
        }

        private static void SetMegaAssemberPrefebDesc(
            ref PrefabDesc prefabDesc,
            ERecipeType type,
            int idleEnergy = 100000,
            int workEnergy = 500000,
            int speed = MegaAssemblerPatches.MegaAssemblerSpeed)
        {
            prefabDesc.isAssembler = true;
            prefabDesc.assemblerRecipeType = (ERecipeType_1)type;
            prefabDesc.assemblerSpeed = speed;
            prefabDesc.isStation = false;
            prefabDesc.isStellarStation = false;
            prefabDesc.stationMaxDroneCount = 0;
            prefabDesc.stationMaxEnergyAcc = 0;
            prefabDesc.stationMaxItemCount = 0;
            prefabDesc.stationMaxItemKinds = 0;
            prefabDesc.stationMaxShipCount = 0;
            prefabDesc.idleEnergyPerTick = idleEnergy;
            prefabDesc.workEnergyPerTick = workEnergy;
        }
    }
}
