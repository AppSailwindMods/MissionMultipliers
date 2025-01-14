﻿using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace MissionMultipliers.Patches
{
    public static class IslandMissionOfficePatches
    {
        [HarmonyPatch(typeof(Mission), MethodType.Constructor, typeof(Port), typeof(Port), typeof(GameObject), typeof(int), typeof(int), typeof(float), typeof(int), typeof(int))]
        private static class MissionCtorPatch
        {
            [HarmonyPrefix]
            public static void Prefix(ref int totalPrice)
            {
                totalPrice *= MissionMultipliersMain.instance.missionPayMultiplier.Value;
            }
        }

        // Replaced a transpiler with a patch to the constructor of the mission class
        /*[HarmonyPatch(typeof(IslandMissionOffice), "GenerateMissions")]

        public static class GenerateMissionsPatch
        {
            [HarmonyTranspiler]
#if DEBUG
            [HarmonyDebug]
#endif
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                var codes = new List<CodeInstruction>(instructions);

                //Multiplier insertion
                int insertionIndex = -1;

                for (int i = 0; i < codes.Count; i++)
                {
#if DEBUG
                    FileLog.Log("codes[" + i + "].opcode: " + codes[i].opcode + ", codes[" + i + "].operand: " + codes[i].operand);
#endif
                    if (codes[i].opcode == OpCodes.Stloc_S && codes[i].operand.ToString() == "System.Int32 (19)")
                    {
                        insertionIndex = i;
#if DEBUG
                        FileLog.Log("Found insertion index");
#endif
                        break;
                    }
                }

                var instructionsToInsert = new List<CodeInstruction>();

                instructionsToInsert.Add(new CodeInstruction(OpCodes.Ldc_I4, (int)Main.settings.MissionPayMultiplier));  //Ldc_R4 for float
                instructionsToInsert.Add(new CodeInstruction(OpCodes.Mul));

                if (insertionIndex != -1)
                {
                    codes.InsertRange(insertionIndex, instructionsToInsert);

                }

                return codes;
            }
        }*/
    }
}
