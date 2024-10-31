using System.Collections.Generic;
using UnityEngine;

namespace ValheimChet
{
    internal class Vars
    {
        public class EntitySpawnData
        {
            public static SpawnSystem.SpawnData lastSpawnedEntity = new SpawnSystem.SpawnData();
            public static SpawnSystem.SpawnData currentSpawnData = new SpawnSystem.SpawnData();
        }

        public class Prefabs
        {
            public static Dictionary<int, GameObject> m_prefabHashDictionary = new Dictionary<int, GameObject>();
            public static GameObject PrefabLookUp(int hash)
            {
                if (m_prefabHashDictionary == null) return null;
                foreach (KeyValuePair<int, GameObject> pair in m_prefabHashDictionary)
                {
                    if(pair.Key == hash) return pair.Value;
                }
                return null;
            }

            public static List<GameObject> GetAllPrefabs()
            {
                return ZNetScene.instance.m_prefabs;
            }
        }

        /* Booleans */
        public static bool fov_changer;
        public static bool speed_changer;
        public static bool runSpeed_changer;
        public static bool acceleration_changer;
        public static bool skill_changer;
        public static bool health_changer;
        public static bool effect_changer;
        public static bool hitRange_changer;
        public static bool hitHeight_changer;
        public static bool menu_toggle;
        public static bool smoothCamera_toggle;
        public static bool esp_toggle;
        public static bool esp_boxes;
        public static bool esp_lines;
        public static bool esp_name;
        public static bool killauraEnabled;
        public static bool killauraFOVCircle;
        public static bool killauraSnap;
        public static bool tpAllEntitiesToPlayer;
        public static bool noFall;
        public static bool noStamina;
        public static bool noTurnDelay;
        public static bool noHurt;
        public static bool oneTap;


        /* Floats */
        public static float currentFov;
        public static float defaultFov;

        public static float killauraRadius;
        public static float killauraFOV;
        public static float killauraHitRange;
        public static float killauraHitHeight;


        public static float currentHitRange;
        public static float defaultHitRange;

        public static float currentHitHeight;
        public static float defaultHitHeight;

        public static float currentSpeed;
        public static float defaultSpeed;
        public static float defaultSwimSpeed;

        public static float currentRunSpeed;
        public static float defaultRunSpeed;
        
        public static float currentAcceleration;
        public static float defaultAcceleration;
        public static float defaultAirAcceleration;
        public static float defaultSwimAcceleration;

        public static int currentBaseHP; // heck 
        public static float defaultBaseHP;
                
        public static float defaultJumpStaminaUsage;
        public static float defaultDodgeStaminaUsage;


        /* Integers */
        public static int tpPosX;
        public static int tpPosY;
        public static int tpPosZ;
        
        public static int prefabSpawnPosX;
        public static int prefabSpawnPosY;
        public static int prefabSpawnPosZ;

        public static int serverPing;


        /* Strings */
        public static string str_currentBaseHP;
        public static string killauraTargetName;

        public static readonly string[] effectNames = {
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
        public static readonly string[] skillNames = {
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

        /* Unity structs */
    }
}


