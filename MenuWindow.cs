using System;
using UnityEngine;

namespace ValheimChet
{
    internal class MenuWindow
    {
        public static Rect menuRect;
        
        public static int tab;
        
        public static float raiseValue = 0f;    
        
        public static bool distantTP = false;

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
            tab = 0;
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
                case 0:
                    {
                        menuRect.height = 500;
                        GUILayout.BeginHorizontal();
                        Vars.fov_changer = GUILayout.Toggle(Vars.fov_changer, "Fov Changer");
                        GUILayout.EndHorizontal();
                        if (Vars.fov_changer)
                        {
                            Vars.currentFov = GUILayout.HorizontalSlider(Vars.currentFov, 1f, 170f);
                            GUILayout.Label($"FOV: {Mathf.RoundToInt(Vars.currentFov)}");
                        }

                        GUILayout.BeginHorizontal();
                        Vars.smoothCamera_toggle = GUILayout.Toggle(Vars.smoothCamera_toggle, "Camera Smoothening");
                        GUILayout.EndHorizontal();

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
                            Vars.currentAcceleration = GUILayout.HorizontalSlider(Vars.currentAcceleration, 1f, 20f);
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
                case 2:
                    {
                        menuRect.height = 570;
                        bool reset = false;

                        // Player Health changer (+swim accel)
                        GUILayout.BeginHorizontal();
                        Vars.health_changer = GUILayout.Toggle(Vars.health_changer, "Health Changer");
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
                                    Debug.Log($"Raised {skill} with {raiseValue}");
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
                                    Debug.Log($"Reseted {skill}");
                                    reset = !reset;
                                }
                            }
                            GUILayout.EndVertical();
                        }
                        break;
                    }
                case 4:
                    {
                        menuRect.height = 500;
                        bool teleportToggle = false;
                        GUILayout.Label("Teleport To [X, Y, Z]:");
                        GUILayout.BeginHorizontal();
                        Vars.str_tpPosX = GUILayout.TextField(Vars.str_tpPosX);
                        Vars.str_tpPosY = GUILayout.TextField(Vars.str_tpPosY);
                        Vars.str_tpPosZ = GUILayout.TextField(Vars.str_tpPosZ);
                        GUILayout.EndHorizontal();

                        GUILayout.BeginHorizontal();
                        Vars.tpAllEntitiesToPlayer = GUILayout.Toggle(Vars.tpAllEntitiesToPlayer, "TP Entities to Player");
                        GUILayout.EndHorizontal();

                        // pls dont judge
                        if (teleportToggle) {
                            if (Int32.TryParse(Vars.str_tpPosX, out Vars.tpPosX))
                            {
                                Vars.tpPosX = Int32.Parse(Vars.str_tpPosX);
                            } else { break; }
                            if (Int32.TryParse(Vars.str_tpPosY, out Vars.tpPosY))
                            {
                                Vars.tpPosY = Int32.Parse(Vars.str_tpPosY);
                            } else { break; }
                            if (Int32.TryParse(Vars.str_tpPosZ, out Vars.tpPosZ))
                            {
                                Vars.tpPosZ = Int32.Parse(Vars.str_tpPosZ);
                            } else { break; }
                        }

                        distantTP = GUILayout.Toggle(distantTP, "Distant TP");
                        Vars.noFall = GUILayout.Toggle(Vars.noFall, "NoFall (Recommended)");
                        teleportToggle = GUILayout.Toggle(teleportToggle, "Start TP");
                        if(teleportToggle && !Player.m_localPlayer.IsTeleporting())
                        {
                            Player.m_localPlayer.TeleportTo(new Vector3((float)Vars.tpPosX, (float)Vars.tpPosY, (float)Vars.tpPosZ), Player.m_localPlayer.transform.rotation, distantTP);
                        }

                        if(Vars.spawnEntity_toggle)
                        {
                            if (Vars.EntitySpawnData.lastSpawnedEntity != null)
                            {
                                EntitySpawner.spawn(Vars.EntitySpawnData.lastSpawnedEntity, new Vector3(0, 50, 0));
                                Vars.spawnEntity_toggle = !Vars.spawnEntity_toggle;
                            } else { Debug.Log("Cannot find valid SpawnData (lastSpawnedEntity is null)"); }
                        }

                        break;
                    }
            }

            GUI.DragWindow();
        }
    }
}
