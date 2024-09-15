using System;
using System.Collections.Generic;
using UnityEngine;

namespace ValheimChet
{
    internal class MenuWindow
    {

        // stuff idk
        private static Rect menuRect;
        
        private static int tab = 0;
        
        private static float raiseValue = 0f;

        private static bool distantTP = false;
        private static bool spawn = false;
        private static bool isValidPrefabSpawnPos = false;

        // Player TP Pos
        private static string str_tpPosX = string.Empty;
        private static string str_tpPosY = string.Empty;
        private static string str_tpPosZ = string.Empty;

        // Entity Spawn pos
        private static string str_prefabInput = string.Empty;
        private static string str_prefabSpawnPosX = string.Empty;
        private static string str_prefabSpawnPosY = string.Empty;
        private static string str_prefabSpawnPosZ = string.Empty;

        // Variables for search method
        private static string searchQuery = string.Empty;
        private static List<string> searchResults = new List<string>();

        private static Vector2 scrollPosition = Vector2.zero;

        private static void DrawPrefabSearchMenu()
        {
            GUILayout.Label($"Found {searchResults.Count} results");

            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchResults.Clear(); 

                foreach (GameObject prefab in Vars.Prefabs.GetAllPrefabs())
                {
                    string prefabName = prefab.name.ToLower();
                    string userQuery = searchQuery.ToLower();

                    if (prefabName.Contains(userQuery))
                    {
                        searchResults.Add(prefab.name);
                    }
                }

                float maxScrollViewHeight = 335f;

                scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(maxScrollViewHeight));

                GUILayout.Space(4);

                foreach (string result in searchResults)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(result);

                    if (GUILayout.Button("Spawn"))
                    {
                        Vars.EntitySpawnData.currentSpawnData.m_prefab = Vars.Prefabs.PrefabLookUp(result.GetStableHashCode());

                        if (isValidPrefabSpawnPos)
                        {
                            Reflections.spawn(Vars.EntitySpawnData.currentSpawnData, new Vector3((float)Vars.prefabSpawnPosX, (float)Vars.prefabSpawnPosY, (float)Vars.prefabSpawnPosZ));
                            Debug.Log($"Spawned prefab: {result}");
                        }
                        else
                        {
                            GUILayout.Label("Spawn position is not valid...");
                        }
                    }

                    GUILayout.EndHorizontal();
                }

                GUILayout.EndScrollView();
            }
        }


        public static void Draw()
        {
            if (Vars.menu_toggle)
            {
                menuRect = GUI.Window(0, menuRect, Window, "ValheimChet");
            }
            GUI.Box(new Rect(Screen.width - 90, 10, 80, 20), "INJECTED");
        }

        public static void Init()
        {
            menuRect = new Rect(900, 100, 400, 500);
        }

        private static void Window(int windowId)
        {

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("CAMERA")) { tab = 0; }
            if (GUILayout.Button("MOVEMENT")) { tab = 1; }
            if (GUILayout.Button("PLAYER")) { tab = 2; }
            if (GUILayout.Button("SKILLS")) { tab = 3; }
            if (GUILayout.Button("MISC")) { tab = 4; }
            GUILayout.EndHorizontal();
            GUILayout.Space(8);


            switch (tab)
            {
                /*
                  sSSs   .S_SSSs     .S_SsS_S.     sSSs   .S_sSSs     .S_SSSs          sdSS_SSSSSSbs   .S_SSSs     .S_SSSs   
                 d%%SP  .SS~SSSSS   .SS~S*S~SS.   d%%SP  .SS~YS%%b   .SS~SSSSS         YSSS~S%SSSSSP  .SS~SSSSS   .SS~SSSSS  
                d%S'    S%S   SSSS  S%S `Y' S%S  d%S'    S%S   `S%b  S%S   SSSS             S%S       S%S   SSSS  S%S   SSSS 
                S%S     S%S    S%S  S%S     S%S  S%S     S%S    S%S  S%S    S%S             S%S       S%S    S%S  S%S    S%S 
                S&S     S%S SSSS%S  S%S     S%S  S&S     S%S    d*S  S%S SSSS%S             S&S       S%S SSSS%S  S%S SSSS%P 
                S&S     S&S  SSS%S  S&S     S&S  S&S_Ss  S&S   .S*S  S&S  SSS%S             S&S       S&S  SSS%S  S&S  SSSY  
                S&S     S&S    S&S  S&S     S&S  S&S~SP  S&S_sdSSS   S&S    S&S             S&S       S&S    S&S  S&S    S&S 
                S&S     S&S    S&S  S&S     S&S  S&S     S&S~YSY%b   S&S    S&S             S&S       S&S    S&S  S&S    S&S 
                S*b     S*S    S&S  S*S     S*S  S*b     S*S   `S%b  S*S    S&S             S*S       S*S    S&S  S*S    S&S 
                S*S.    S*S    S*S  S*S     S*S  S*S.    S*S    S%S  S*S    S*S             S*S       S*S    S*S  S*S    S*S 
                 SSSbs  S*S    S*S  S*S     S*S   SSSbs  S*S    S&S  S*S    S*S             S*S       S*S    S*S  S*S SSSSP  
                  YSSP  SSS    S*S  SSS     S*S    YSSP  S*S    SSS  SSS    S*S             S*S       SSS    S*S  S*S  SSY   
                               SP           SP           SP                 SP              SP               SP   SP         
                               Y            Y            Y                  Y               Y                Y    Y          
                 */
                case 0:
                    {
                        menuRect.height = 500;

                        // FOV Changer
                        GUILayout.BeginHorizontal();
                        Vars.fov_changer = GUILayout.Toggle(Vars.fov_changer, "Fov Changer");
                        GUILayout.EndHorizontal();
                        if (Vars.fov_changer)
                        {
                            Vars.currentFov = GUILayout.HorizontalSlider(Vars.currentFov, 1f, 170f);
                            GUILayout.Label($"FOV: {Mathf.RoundToInt(Vars.currentFov)}");
                        }

                        // Smooth Camera
                        GUILayout.BeginHorizontal();
                        Vars.smoothCamera_toggle = GUILayout.Toggle(Vars.smoothCamera_toggle, "Camera Smoothening");
                        GUILayout.EndHorizontal();

                        // ESP Toggle
                        GUILayout.BeginHorizontal();
                        Vars.esp_toggle = GUILayout.Toggle(Vars.esp_toggle, "ESP");
                        GUILayout.EndHorizontal();
                        if (Vars.esp_toggle)
                        {
                            GUILayout.BeginHorizontal();
                            GUILayout.Space(20);
                            Vars.esp_boxes = GUILayout.Toggle(Vars.esp_boxes, "ESP Box");
                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            GUILayout.Space(20);
                            Vars.esp_lines = GUILayout.Toggle(Vars.esp_lines, "ESP Line");
                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            GUILayout.Space(20);
                            Vars.esp_name = GUILayout.Toggle(Vars.esp_name, "ESP Name");
                            GUILayout.EndHorizontal();
                        }
                        break;
                    }






                /*
                  .S_SsS_S.     sSSs_sSSs     .S    S.     sSSs   .S_SsS_S.     sSSs   .S_sSSs    sdSS_SSSSSSbs        sdSS_SSSSSSbs   .S_SSSs     .S_SSSs   
                .SS~S*S~SS.   d%%SP~YS%%b   .SS    SS.   d%%SP  .SS~S*S~SS.   d%%SP  .SS~YS%%b   YSSS~S%SSSSSP        YSSS~S%SSSSSP  .SS~SSSSS   .SS~SSSSS  
                S%S `Y' S%S  d%S'     `S%b  S%S    S%S  d%S'    S%S `Y' S%S  d%S'    S%S   `S%b       S%S                  S%S       S%S   SSSS  S%S   SSSS 
                S%S     S%S  S%S       S%S  S%S    S%S  S%S     S%S     S%S  S%S     S%S    S%S       S%S                  S%S       S%S    S%S  S%S    S%S 
                S%S     S%S  S&S       S&S  S&S    S%S  S&S     S%S     S%S  S&S     S%S    S&S       S&S                  S&S       S%S SSSS%S  S%S SSSS%P 
                S&S     S&S  S&S       S&S  S&S    S&S  S&S_Ss  S&S     S&S  S&S_Ss  S&S    S&S       S&S                  S&S       S&S  SSS%S  S&S  SSSY  
                S&S     S&S  S&S       S&S  S&S    S&S  S&S~SP  S&S     S&S  S&S~SP  S&S    S&S       S&S                  S&S       S&S    S&S  S&S    S&S 
                S&S     S&S  S&S       S&S  S&S    S&S  S&S     S&S     S&S  S&S     S&S    S&S       S&S                  S&S       S&S    S&S  S&S    S&S 
                S*S     S*S  S*b       d*S  S*b    S*S  S*b     S*S     S*S  S*b     S*S    S*S       S*S                  S*S       S*S    S&S  S*S    S&S 
                S*S     S*S  S*S.     .S*S  S*S.   S*S  S*S.    S*S     S*S  S*S.    S*S    S*S       S*S                  S*S       S*S    S*S  S*S    S*S 
                S*S     S*S   SSSbs_sdSSS    SSSbs_S*S   SSSbs  S*S     S*S   SSSbs  S*S    S*S       S*S                  S*S       S*S    S*S  S*S SSSSP  
                SSS     S*S    YSSP~YSSY      YSSP~SSS    YSSP  SSS     S*S    YSSP  S*S    SSS       S*S                  S*S       SSS    S*S  S*S  SSY   
                        SP                                              SP           SP               SP                   SP               SP   SP         
                        Y                                               Y            Y                Y                    Y                Y    Y          
                 */
                case 1:
                    {
                        menuRect.height = 500;

                        // Speed changer (+swim speed)
                        GUILayout.BeginHorizontal();
                        Vars.speed_changer = GUILayout.Toggle(Vars.speed_changer, "Speed Changer");
                        GUILayout.EndHorizontal();
                        GUILayout.Space(5);
                        if (Vars.speed_changer)
                        {
                            Vars.currentSpeed = GUILayout.HorizontalSlider(Vars.currentSpeed, 1f, 200f);
                            GUILayout.Label($"Speed: {Mathf.RoundToInt(Vars.currentSpeed)}");
                        }

                        // Run Speed changer
                        GUILayout.BeginHorizontal();
                        Vars.runSpeed_changer = GUILayout.Toggle(Vars.runSpeed_changer, "Run Speed Changer");
                        GUILayout.EndHorizontal();
                        GUILayout.Space(5);
                        if (Vars.runSpeed_changer)
                        {
                            Vars.currentRunSpeed = GUILayout.HorizontalSlider(Vars.currentRunSpeed, 1f, 200f);
                            GUILayout.Label($"RunSpeed: {Mathf.RoundToInt(Vars.currentRunSpeed)}");
                        }

                        // Acceleration changer (+swim accel)
                        GUILayout.BeginHorizontal();
                        Vars.acceleration_changer = GUILayout.Toggle(Vars.acceleration_changer, "Acceleration Changer");
                        GUILayout.EndHorizontal();
                        GUILayout.Space(5);
                        if (Vars.acceleration_changer)
                        {
                            Vars.currentAcceleration = GUILayout.HorizontalSlider(Vars.currentAcceleration, 1f, 30f);
                            GUILayout.Label($"Acceleration: {Mathf.RoundToInt(Vars.currentAcceleration)}");
                        }

                        // NoTurnDelay Toggle
                        GUILayout.BeginHorizontal();
                        Vars.noTurnDelay = GUILayout.Toggle(Vars.noTurnDelay, "NoTurnDelay");
                        GUILayout.EndHorizontal();
                        GUILayout.Space(5);

                        // NoFall Toggle
                        GUILayout.BeginHorizontal();
                        Vars.noFall = GUILayout.Toggle(Vars.noFall, "NoFall");
                        GUILayout.EndHorizontal();
                        GUILayout.Space(5);

                        // NoStamina Toggle
                        GUILayout.BeginHorizontal();
                        Vars.noStamina = GUILayout.Toggle(Vars.noStamina, "NoStamina");
                        GUILayout.EndHorizontal();
                        GUILayout.Space(5);

                        break;
                    }






                /*
                  .S_sSSs    S.       .S_SSSs     .S S.     sSSs   .S_sSSs          sdSS_SSSSSSbs   .S_SSSs     .S_SSSs   
                .SS~YS%%b   SS.     .SS~SSSSS   .SS SS.   d%%SP  .SS~YS%%b         YSSS~S%SSSSSP  .SS~SSSSS   .SS~SSSSS  
                S%S   `S%b  S%S     S%S   SSSS  S%S S%S  d%S'    S%S   `S%b             S%S       S%S   SSSS  S%S   SSSS 
                S%S    S%S  S%S     S%S    S%S  S%S S%S  S%S     S%S    S%S             S%S       S%S    S%S  S%S    S%S 
                S%S    d*S  S&S     S%S SSSS%S  S%S S%S  S&S     S%S    d*S             S&S       S%S SSSS%S  S%S SSSS%P 
                S&S   .S*S  S&S     S&S  SSS%S   SS SS   S&S_Ss  S&S   .S*S             S&S       S&S  SSS%S  S&S  SSSY  
                S&S_sdSSS   S&S     S&S    S&S    S S    S&S~SP  S&S_sdSSS              S&S       S&S    S&S  S&S    S&S 
                S&S~YSSY    S&S     S&S    S&S    SSS    S&S     S&S~YSY%b              S&S       S&S    S&S  S&S    S&S 
                S*S         S*b     S*S    S&S    S*S    S*b     S*S   `S%b             S*S       S*S    S&S  S*S    S&S 
                S*S         S*S.    S*S    S*S    S*S    S*S.    S*S    S%S             S*S       S*S    S*S  S*S    S*S 
                S*S          SSSbs  S*S    S*S    S*S     SSSbs  S*S    S&S             S*S       S*S    S*S  S*S SSSSP  
                S*S           YSSP  SSS    S*S    S*S      YSSP  S*S    SSS             S*S       SSS    S*S  S*S  SSY   
                SP                         SP     SP             SP                     SP               SP   SP         
                Y                          Y      Y              Y                      Y                Y    Y          
                 */
                case 2:
                    {
                        menuRect.height = 570;
                        bool reset = false;

                        // Player Health changer
                        GUILayout.BeginHorizontal();
                        Vars.health_changer = GUILayout.Toggle(Vars.health_changer, "Health Changer (constant)");
                        GUILayout.EndHorizontal();
                        if (Vars.health_changer)
                        {
                            bool toggled = false;

                            GUILayout.BeginHorizontal();
                            GUILayout.Space(10);
                            Vars.str_currentBaseHP = GUILayout.TextField(Vars.str_currentBaseHP);
                            toggled = GUILayout.Toggle(toggled, "Set");
                            if (toggled)
                            {
                                if (Int32.TryParse(Vars.str_currentBaseHP, out Vars.currentBaseHP))
                                {
                                    Vars.currentBaseHP = Int32.Parse(Vars.str_currentBaseHP);
                                }
                                else { Debug.Log("Couldn't Parse str_currentBaseHP"); }
                            }
                            GUILayout.Label($"HP: {Mathf.RoundToInt(Vars.currentBaseHP)}");
                            GUILayout.EndHorizontal();
                        }

                        GUILayout.BeginHorizontal();
                        Vars.noHurt = GUILayout.Toggle(Vars.noHurt, "NoHurt");
                        GUILayout.EndHorizontal();

                        GUILayout.BeginHorizontal();
                        Vars.noHurt = GUILayout.Toggle(Vars.noHurt, "OneTap");
                        GUILayout.EndHorizontal();

                        // Effect Changer
                        GUILayout.BeginHorizontal();
                        Vars.effect_changer = GUILayout.Toggle(Vars.effect_changer, "Effect Changer");
                        GUILayout.EndHorizontal();
                        if(Vars.effect_changer)
                        {
                            foreach (string effectName in Vars.effectNames)
                            {
                                bool toggled = false;

                                GUILayout.BeginHorizontal();
                                GUILayout.Label(effectName);
                                toggled = GUILayout.Toggle(toggled, "Add");
                                reset = GUILayout.Toggle(reset, "Reset");
                                GUILayout.Label("        ");
                                GUILayout.EndHorizontal();
                                if(toggled)
                                {
                                    if (effectName == "All")
                                    {
                                        foreach (string _effectName in Vars.effectNames)
                                        {
                                            if (_effectName == "All") continue;

                                            Player.m_localPlayer.GetSEMan().AddStatusEffect(_effectName.GetStableHashCode());
                                        }
                                        continue;
                                    }
                                    Player.m_localPlayer.GetSEMan().AddStatusEffect(effectName.GetStableHashCode());
                                    Debug.Log("Effect Added: " + effectName);
                                    toggled = !toggled;
                                }
                                if(reset)
                                {
                                    if(effectName == "All")
                                    {
                                        Player.m_localPlayer.GetSEMan().RemoveAllStatusEffects();
                                        continue;
                                    }

                                    Player.m_localPlayer.GetSEMan().RemoveStatusEffect(effectName.GetStableHashCode());
                                    reset = !reset;
                                }
                            }
                        }
                        break;
                    }






                /*
                   sSSs   .S    S.    .S  S.      S.        sSSs        sdSS_SSSSSSbs   .S_SSSs     .S_SSSs   
                 d%%SP  .SS    SS.  .SS  SS.     SS.      d%%SP        YSSS~S%SSSSSP  .SS~SSSSS   .SS~SSSSS  
                d%S'    S%S    S&S  S%S  S%S     S%S     d%S'               S%S       S%S   SSSS  S%S   SSSS 
                S%|     S%S    d*S  S%S  S%S     S%S     S%|                S%S       S%S    S%S  S%S    S%S 
                S&S     S&S   .S*S  S&S  S&S     S&S     S&S                S&S       S%S SSSS%S  S%S SSSS%P 
                Y&Ss    S&S_sdSSS   S&S  S&S     S&S     Y&Ss               S&S       S&S  SSS%S  S&S  SSSY  
                `S&&S   S&S~YSSY%b  S&S  S&S     S&S     `S&&S              S&S       S&S    S&S  S&S    S&S 
                  `S*S  S&S    `S%  S&S  S&S     S&S       `S*S             S&S       S&S    S&S  S&S    S&S 
                   l*S  S*S     S%  S*S  S*b     S*b        l*S             S*S       S*S    S&S  S*S    S&S 
                  .S*P  S*S     S&  S*S  S*S.    S*S.      .S*P             S*S       S*S    S*S  S*S    S*S 
                sSS*S   S*S     S&  S*S   SSSbs   SSSbs  sSS*S              S*S       S*S    S*S  S*S SSSSP  
                YSS'    S*S     SS  S*S    YSSP    YSSP  YSS'               S*S       SSS    S*S  S*S  SSY   
                        SP          SP                                      SP               SP   SP         
                        Y           Y                                       Y                Y    Y          
                 */
                case 3:
                    {
                        menuRect.height = 680;
                        bool reset = false;

                        // Skill changer
                        Skills _skills = Player.m_localPlayer.GetSkills();
                        GUILayout.BeginHorizontal();
                        Vars.skill_changer = GUILayout.Toggle(Vars.skill_changer, "Skill Changer");
                        GUILayout.EndHorizontal();
                        if (Vars.skill_changer)
                        {
                            GUILayout.BeginVertical();
                            GUILayout.Label($"Raise Value: {raiseValue}");
                            raiseValue = GUILayout.HorizontalSlider(raiseValue, 0f, 100f);
                            foreach (string skill in Vars.skillNames)
                            {
                                bool toggled = false;
                                
                                GUILayout.BeginHorizontal();
                                GUILayout.Label(skill);
                                toggled = GUILayout.Toggle(toggled, "Raise");
                                reset = GUILayout.Toggle(reset, "Reset");
                                GUILayout.Label("        ");
                                GUILayout.EndHorizontal();
                                if (toggled)
                                {
                                    _skills.CheatRaiseSkill(skill, raiseValue, true);
                                    Debug.Log($"Raised Skill: {skill} with {raiseValue}");
                                    toggled = !toggled;
                                }
                                if (reset)
                                {
                                    if (skill.ToLower() == "all")
                                    {
                                        foreach (string _skill in Vars.skillNames)
                                        {
                                            if(_skill == "all")
                                                continue;
                                            _skills.CheatResetSkill(_skill);
                                        }
                                        continue;
                                    }
                                    _skills.CheatResetSkill(skill);
                                    Debug.Log($"Reseted Skill: {skill}");
                                    reset = !reset;
                                }
                            }
                            GUILayout.EndVertical();
                        }
                        break;
                    }





                /*
                  .S_SsS_S.    .S    sSSs    sSSs        sdSS_SSSSSSbs   .S_SSSs     .S_SSSs   
                .SS~S*S~SS.  .SS   d%%SP   d%%SP        YSSS~S%SSSSSP  .SS~SSSSS   .SS~SSSSS  
                S%S `Y' S%S  S%S  d%S'    d%S'               S%S       S%S   SSSS  S%S   SSSS 
                S%S     S%S  S%S  S%|     S%S                S%S       S%S    S%S  S%S    S%S 
                S%S     S%S  S&S  S&S     S&S                S&S       S%S SSSS%S  S%S SSSS%P 
                S&S     S&S  S&S  Y&Ss    S&S                S&S       S&S  SSS%S  S&S  SSSY  
                S&S     S&S  S&S  `S&&S   S&S                S&S       S&S    S&S  S&S    S&S 
                S&S     S&S  S&S    `S*S  S&S                S&S       S&S    S&S  S&S    S&S 
                S*S     S*S  S*S     l*S  S*b                S*S       S*S    S&S  S*S    S&S 
                S*S     S*S  S*S    .S*P  S*S.               S*S       S*S    S*S  S*S    S*S 
                S*S     S*S  S*S  sSS*S    SSSbs             S*S       S*S    S*S  S*S SSSSP  
                SSS     S*S  S*S  YSS'      YSSP             S*S       SSS    S*S  S*S  SSY   
                        SP   SP                              SP               SP   SP         
                        Y    Y                               Y                Y    Y          
                 */
                case 4:
                    {
                        menuRect.height = 600;
                        bool teleportToggle = false;

                        GUILayout.Label("Teleport To [ X, Y, Z ]:");
                        GUILayout.BeginHorizontal();
                        str_tpPosX = GUILayout.TextField(str_tpPosX);
                        str_tpPosY = GUILayout.TextField(str_tpPosY);
                        str_tpPosZ = GUILayout.TextField(str_tpPosZ);
                        GUILayout.EndHorizontal();

                        GUILayout.BeginHorizontal();
                        Vars.tpAllEntitiesToPlayer = GUILayout.Toggle(Vars.tpAllEntitiesToPlayer, "TP All Entities to Player");
                        GUILayout.EndHorizontal();

                        // check if we can tp (if you have better idea for code structure, show me pls)
                        if (
                            Int32.TryParse(str_tpPosX, out Vars.tpPosX) && 
                            Int32.TryParse(str_tpPosY, out Vars.tpPosY) && 
                            Int32.TryParse(str_tpPosZ, out Vars.tpPosZ)
                            )
                        {
                            Vars.tpPosX = Int32.Parse(str_tpPosX);
                            Vars.tpPosY = Int32.Parse(str_tpPosY);
                            Vars.tpPosZ = Int32.Parse(str_tpPosZ);
                            distantTP = GUILayout.Toggle(distantTP, "Distant TP");
                            Vars.noFall = GUILayout.Toggle(Vars.noFall, "NoFall (Recommended)");
                            teleportToggle = GUILayout.Toggle(teleportToggle, "Start TP");
                            if (teleportToggle && !Player.m_localPlayer.IsTeleporting())
                            {
                                Player.m_localPlayer.TeleportTo(new Vector3((float)Vars.tpPosX, (float)Vars.tpPosY, (float)Vars.tpPosZ), Player.m_localPlayer.transform.rotation, distantTP);
                                Debug.Log($"Teleporting to: {Vars.tpPosX} {Vars.tpPosY} {Vars.tpPosZ}...");
                            }
                        }
                        GUILayout.Space(10);
                        GUILayout.Label("Custom Spawner [ Spawn Pos (X, Y, Z) | Search Query ]: ");
                        GUILayout.BeginHorizontal();
                        str_prefabSpawnPosX = GUILayout.TextField(str_prefabSpawnPosX);
                        str_prefabSpawnPosY = GUILayout.TextField(str_prefabSpawnPosY);
                        str_prefabSpawnPosZ = GUILayout.TextField(str_prefabSpawnPosZ);
                        GUILayout.EndHorizontal();

                        // -------- Search Query Implementation --------
                        GUILayout.Label("Search Prefabs:");
                        searchQuery = GUILayout.TextField(searchQuery);
                        if (searchQuery.Length >= 2) { DrawPrefabSearchMenu(); }
                        // ---------------------------------------------

                        if (Int32.TryParse(str_prefabSpawnPosX, out Vars.prefabSpawnPosX) && Int32.TryParse(str_prefabSpawnPosY, out Vars.prefabSpawnPosY) && Int32.TryParse(str_prefabSpawnPosZ, out Vars.prefabSpawnPosZ))
                        {
                            Vars.prefabSpawnPosX = Int32.Parse(str_prefabSpawnPosX);
                            Vars.prefabSpawnPosY = Int32.Parse(str_prefabSpawnPosY);
                            Vars.prefabSpawnPosZ = Int32.Parse(str_prefabSpawnPosZ);
                            isValidPrefabSpawnPos = true;
                        } else { isValidPrefabSpawnPos = false; }

                        // if hash is a valid prefab
                        if (Int32.TryParse(str_prefabInput, out Vars.currentSpawnHash))
                        {
                            if (Vars.Prefabs.PrefabLookUp(Int32.Parse(str_prefabInput)) != null) {
                                Vars.currentSpawnHash = Int32.Parse(str_prefabInput);
                                Vars.EntitySpawnData.currentSpawnData.m_prefab = Vars.Prefabs.PrefabLookUp(Vars.currentSpawnHash);
                                spawn = GUILayout.Toggle(spawn, $"Spawn {Vars.EntitySpawnData.currentSpawnData.m_prefab.name}");
                                if (spawn) {
                                    if(isValidPrefabSpawnPos)
                                    {
                                        Reflections.spawn(Vars.EntitySpawnData.currentSpawnData, new Vector3((float)Vars.prefabSpawnPosX, (float)Vars.prefabSpawnPosY, (float)Vars.prefabSpawnPosZ));
                                        spawn = !spawn;
                                    }
                                    GUILayout.Label("Spawn position is not valid...");
                                }
                            }
                        }

                        break;
                    }
            }

            GUI.DragWindow();
        }
    }
}
