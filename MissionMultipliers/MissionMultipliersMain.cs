using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;

namespace MissionMultipliers
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public class MissionMultipliersMain : BaseUnityPlugin
    {
        public const string GUID = "com.sircarcass.missionmultipliers";
        public const string NAME = "Mission Multipliers";
        public const string VERSION = "1.0.0";

        internal ConfigEntry<int> missionPayMultiplier;
        public ConfigEntry<int> missionRepMultiplier;

        internal static MissionMultipliersMain instance;

        internal static ManualLogSource logSource;

        private void Awake()
        {
            instance = this;
            logSource = Logger;
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), GUID);

            missionPayMultiplier = Config.Bind("Values", "Mission Pay Multiplier", 1);
            missionRepMultiplier = Config.Bind("Values", "Mission Rep Multiplier", 1);

            Clamp(missionPayMultiplier);
            Clamp(missionRepMultiplier);
        }

        private void Clamp(ConfigEntry<int> entry)
        {
            if(entry.Value < 1)
            {
                entry.Value = 1;
            }

            entry.SettingChanged += (_, __) =>
            {
                if (entry.Value < 1)
                {
                    entry.Value = 1;
                }
            };
        }
    }
}