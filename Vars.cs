using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static UnityEngine.XR.ARSubsystems.XRCpuImage;

namespace ValheimChet
{
    internal class Vars
    {
        public static string[] skillNames = {
            "Swords",
            "Knives",
            "Clubs",
            "Polearms",
            "Spears",
            "Blocking",
            "Axes",
            "Bows",
            "ElementalMagic",
            "BloodMagic",
            "Unarmed",
            "Pickaxes",
            "WoodCutting",
            "Crossbows",
            "Jump",
            "Sneak",
            "Run",
            "Swim",
            "Fishing",
            "Ride",
            "All"
        };

        public static string[] effectNames = {
            "CorpseRun",
            "Burning",
            "Frost",
            "Lightning",
            "Poison",
            "Smoked",
            "Spirit",
            "Tared",
            "Wet",
            "Rested",
            "Encumbered",
            "SoftDeath",
            "Shelter",
            "CampFire",
            "Resting",
            "Cold",
            "Freezing",
            "All"
        };

        public class EntitySpawnData
        {
            public static SpawnSystem.SpawnData lastSpawnedEntity = new SpawnSystem.SpawnData();

            
            public static SpawnSystem.SpawnData greyling = new SpawnSystem.SpawnData();
            
        }

        // TODO: dump spawn data and prefab stuff... somehow.
        public class SpawnData
        {
            public string m_name = "";

            public bool m_enabled = true;

            public bool m_devDisabled;

            public GameObject m_prefab;

            [BitMask(typeof(Heightmap.Biome))]
            public Heightmap.Biome m_biome;

            [BitMask(typeof(Heightmap.BiomeArea))]
            public Heightmap.BiomeArea m_biomeArea = Heightmap.BiomeArea.Everything;

            [Header("Total nr of instances (if near player is set, only instances within the max spawn radius is counted)")]
            public int m_maxSpawned = 1;

            [Header("How often do we spawn")]
            public float m_spawnInterval = 4f;

            [Header("Chanse to spawn each spawn interval")]
            [Range(0f, 100f)]
            public float m_spawnChance = 100f;

            [Header("Minimum distance to another instance")]
            public float m_spawnDistance = 10f;

            [Header("Spawn range ( 0 = use global setting )")]
            public float m_spawnRadiusMin;

            public float m_spawnRadiusMax;

            [Header("Only spawn if this key is set")]
            public string m_requiredGlobalKey = "";

            [Header("Only spawn if this environment is active")]
            public List<string> m_requiredEnvironments = new List<string>();

            [Header("Group spawning")]
            public int m_groupSizeMin = 1;

            public int m_groupSizeMax = 1;

            public float m_groupRadius = 3f;

            [Header("Time of day & Environment")]
            public bool m_spawnAtNight = true;

            public bool m_spawnAtDay = true;

            [Header("Altitude")]
            public float m_minAltitude = -1000f;

            public float m_maxAltitude = 1000f;

            [Header("Terrain tilt")]
            public float m_minTilt;

            public float m_maxTilt = 35f;

            [Header("Areas")]
            public bool m_inForest = true;

            public bool m_outsideForest = true;

            public bool m_inLava;

            public bool m_outsideLava = true;

            public bool m_canSpawnCloseToPlayer;

            public bool m_insidePlayerBase;

            [Header("Ocean depth ")]
            public float m_minOceanDepth;

            public float m_maxOceanDepth;

            [Header("States")]
            public bool m_huntPlayer;

            public float m_groundOffset = 0.5f;

            public float m_groundOffsetRandom;

            [Header("Level")]
            public int m_maxLevel = 1;

            public int m_minLevel = 1;

            public float m_levelUpMinCenterDistance;

            public float m_overrideLevelupChance = -1f;

            [HideInInspector]
            public bool m_foldout;

            public SpawnData Clone()
            {
                SpawnData obj = MemberwiseClone() as SpawnData;
                obj.m_requiredEnvironments = new List<string>(m_requiredEnvironments);
                return obj;
            }
        }

        public class EffectHash
        {
            public static readonly int m_lootStatusEffect = "CorpseRun".GetStableHashCode();
            
            public static readonly int s_statusEffectBurning = "Burning".GetStableHashCode();

            public static readonly int s_statusEffectFrost = "Frost".GetStableHashCode();

            public static readonly int s_statusEffectLightning = "Lightning".GetStableHashCode();

            public static readonly int s_statusEffectPoison = "Poison".GetStableHashCode();

            public static readonly int s_statusEffectSmoked = "Smoked".GetStableHashCode();

            public static readonly int s_statusEffectSpirit = "Spirit".GetStableHashCode();

            public static readonly int s_statusEffectTared = "Tared".GetStableHashCode();

            public static readonly int s_statusEffectWet = "Wet".GetStableHashCode();

            public static readonly int s_statusEffectRested = "Rested".GetStableHashCode();

            public static readonly int s_statusEffectEncumbered = "Encumbered".GetStableHashCode();

            public static readonly int s_statusEffectSoftDeath = "SoftDeath".GetStableHashCode();

            public static readonly int s_statusEffectShelter = "Shelter".GetStableHashCode();

            public static readonly int s_statusEffectCampFire = "CampFire".GetStableHashCode();

            public static readonly int s_statusEffectResting = "Resting".GetStableHashCode();

            public static readonly int s_statusEffectCold = "Cold".GetStableHashCode();

            public static readonly int s_statusEffectFreezing = "Freezing".GetStableHashCode();
        }


        /* Booleans */
        public static bool fov_changer;
        public static bool speed_changer;
        public static bool runSpeed_changer;
        public static bool acceleration_changer;
        public static bool skill_changer;
        public static bool health_changer;
        public static bool effect_changer;
        public static bool menu_toggle;
        public static bool smoothCamera_toggle;
        public static bool spawnEntity_toggle;
        public static bool esp_toggle;
        public static bool esp_boxes;
        public static bool esp_lines;
        public static bool esp_name;
        public static bool tpAllEntitiesToPlayer;
        public static bool noFall;
        public static bool noStamina;
        public static bool noTurnDelay;


        /* Floats */
        public static float currentFov;
        public static float defaultFov;

        public static float currentSpeed;
        public static float defaultSpeed;
        public static float defaultSwimSpeed;

        public static float currentRunSpeed;
        public static float defaultRunSpeed;
        
        public static float currentAcceleration;
        public static float defaultAcceleration;
        public static float defaultAirAcceleration;
        public static float defaultSwimAcceleration;

        public static int currentBaseHP; // i know
        public static float defaultBaseHP;
                
        public static float defaultJumpStaminaUsage;
        public static float defaultDodgeStaminaUsage;

        /* Integers */
        public static int tpPosX;
        public static int tpPosY;
        public static int tpPosZ;

        /* Strings */
        public static string str_currentBaseHP;
        public static string str_tpPosX;
        public static string str_tpPosY;
        public static string str_tpPosZ;

        /* Unity structs */
    }
}


