// Author: Upwn
// Ver: 1.1.2

using UnityEngine;

/*  --- TODO/NOTES ---
 *  + Fixed noHurt
    --- TODO/NOTES ---  */

namespace ValheimChet
{
    internal class Chet : MonoBehaviour
    {
        static void Init()
        {
            System.Console.Clear();
            Log("Chet injected! Skill corrected! Targets detected! Mayhem perfected ;)");

            // in-game check
            Player localPlayer = Player.m_localPlayer;
            if (localPlayer == null || !localPlayer.GetZAnim().enabled)
            {
                Vars.inGame = false;
                WarningLog("localPlayer is null or ZAnim not found, exiting... are you in-game?");
                Loader.Unload();
            }

            Vars.inGame = true;
            InitializeValues(localPlayer);
            Vars.Prefabs.m_prefabHashDictionary = Reflections.GetNamedPrefabs();
        }

        public static void Log(string message) => Debug.Log($"[ValheimChet] {message}");
        public static void WarningLog(string message) => Debug.LogWarning($"[ValheimChet] {message}");
        public static void ErrorLog(string message) => Debug.LogError($"[ValheimChet] {message}");


        /*
            Might be used in the future so i will keep them here (ignore it)
         */
        //private Vector2 QuaternionToYawPitch(Quaternion rotation)
        //{
        //    Vector2 yawPitch;

        //    yawPitch.x = rotation.eulerAngles.y;

        //    // pitch (rotation around X axis, considering negative due to camera)
        //    float pitch = rotation.eulerAngles.x;
        //    yawPitch.y = pitch > 180f ? pitch - 360f : pitch;

        //    // avoiding looking too far up or down
        //    yawPitch.y = Mathf.Clamp(yawPitch.y, -89f, 89f);

        //    return yawPitch;
        //}

        //public Quaternion YawPitchToQuaternion(float yaw, float pitch)
        //{
        //    Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f); // Assuming no roll (Z axis)
        //    return rotation;
        //}


        static void InitializeValues(Player localPlayer)
        {
            Attack defaultAttack = new Attack();

            // setup default values and configurations
            Vars.menu_toggle = true;
            Vars.fov_changer = false;
            Vars.speed_changer = false;
            Vars.runSpeed_changer = false;
            Vars.acceleration_changer = false;
            Vars.skill_changer = false;
            Vars.health_changer = false;
            Vars.hitRange_changer = false;
            Vars.hitHeight_changer = false;
            Vars.smoothCamera_toggle = true;
            Vars.tpAllEntitiesToPlayer = false;
            Vars.esp_toggle = false;
            Vars.esp_boxes = false;
            Vars.esp_lines = false;
            Vars.killauraEnabled = false;
            Vars.killauraFOVCircle = false;
            Vars.killauraSnap = false;
            Vars.noTurnDelay = false;
            Vars.noFall = false;
            Vars.noStamina = false;
            Vars.noHurt = false;
            Vars.oneTap = false;

            // setup player stats
            Vars.defaultFov = Camera.main.fieldOfView;
            Vars.currentFov = Vars.defaultFov;

            Vars.killauraFOV = 90f;
            Vars.killauraRadius = 30f;

            Vars.defaultSpeed = localPlayer.m_speed;
            Vars.currentSpeed = Vars.defaultSpeed;
            Vars.defaultSwimSpeed = localPlayer.m_swimSpeed;

            Vars.defaultRunSpeed = localPlayer.m_runSpeed;
            Vars.currentRunSpeed = Vars.defaultRunSpeed;

            Vars.defaultAcceleration = localPlayer.m_acceleration;
            Vars.currentAcceleration = Vars.defaultAcceleration;
            Vars.defaultAirAcceleration = localPlayer.m_airControl;
            Vars.defaultSwimAcceleration = localPlayer.m_swimAcceleration;

            Vars.defaultBaseHP = localPlayer.m_baseHP;
            Vars.currentBaseHP = (int)Vars.defaultBaseHP;

            Vars.defaultHitRange = defaultAttack.m_attackRange;
            Vars.currentHitRange = Vars.defaultHitRange;

            Vars.defaultHitHeight = defaultAttack.m_attackHeight;
            Vars.currentHitHeight = Vars.defaultHitHeight;

            Vars.defaultJumpStaminaUsage = localPlayer.m_jumpStaminaUsage;
            Vars.defaultDodgeStaminaUsage = localPlayer.m_dodgeStaminaUsage;

            // setup camera
            Camera.main.GetComponent<GameCamera>().m_fov = Vars.defaultFov;
            Camera.main.GetComponent<GameCamera>().m_smoothness = Vars.smoothCamera_toggle ? 0.1f : 0.01f;
        }

        public static void SetBaseHPAndHealth(float value)
        {
            Player localPlayer = Player.m_localPlayer;

            localPlayer.m_baseHP = value;
            localPlayer.SetHealth(value);
        }
        static void Player_Cache()
        {
            if (!Vars.inGame) return;
            Player localPlayer = Player.m_localPlayer;

            // Update based on toggles
            UpdatePlayerSpeed(localPlayer);
            UpdatePlayerHealth(localPlayer);
            UpdatePlayerTurnSpeed(localPlayer);
            UpdateCameraFOV();
        }

        static void UpdatePlayerSpeed(Player localPlayer)
        {
            if (Vars.speed_changer)
            {
                localPlayer.m_speed = Vars.currentSpeed;
                localPlayer.m_swimSpeed = Vars.currentSpeed;
            }
            else
            {
                localPlayer.m_speed = Vars.defaultSpeed;
                localPlayer.m_swimSpeed = Vars.defaultSwimSpeed;
            }

            localPlayer.m_runSpeed = Vars.runSpeed_changer ? Vars.currentRunSpeed : Vars.defaultRunSpeed;
            localPlayer.m_acceleration = Vars.acceleration_changer ? Vars.currentAcceleration : Vars.defaultAcceleration;
        }

        static void UpdatePlayerHealth(Player localPlayer)
        {
            if (Vars.health_changer)
            {
                SetBaseHPAndHealth(Vars.currentBaseHP);
            }
            else
            {
                SetBaseHPAndHealth(Vars.defaultBaseHP);
            }
        }

        static void UpdatePlayerTurnSpeed(Player localPlayer)
        {
            if (Vars.noTurnDelay)
            {
                localPlayer.m_turnSpeed = 10000f;
                localPlayer.m_runTurnSpeed = 10000f;
                localPlayer.m_swimTurnSpeed = 10000f;
                localPlayer.m_flyTurnSpeed = 10000f;
            }
            else
            {
                // hard coded, invalid if changed by devs
                localPlayer.m_turnSpeed = 300f;
                localPlayer.m_runTurnSpeed = 300f;
                localPlayer.m_swimTurnSpeed = 100f;
                localPlayer.m_flyTurnSpeed = 12f;
            }
        }

        static void UpdateCameraFOV()
        {
            var gameCamera = Camera.main.GetComponent<GameCamera>();
            gameCamera.m_fov = Vars.fov_changer ? Vars.currentFov : Vars.defaultFov;
            gameCamera.m_smoothness = Vars.smoothCamera_toggle ? 0.1f : 0.01f;
        }

        public void DrawBoxESP(Vector3 footpos, Vector3 headpos, Color color, string name)
        {
            float widthOffset = 1f;

            float height = Mathf.Abs(headpos.y - footpos.y);
            float width = height / widthOffset;
            float distance = Vector3.Distance(Camera.main.transform.position, footpos);
            width *= Mathf.Clamp(1 / distance, 0.5f, 1f);

            // ESP Box
            if (Vars.esp_boxes)
            {
                Render.DrawBox(footpos.x - (width / 2), (float)Screen.height - footpos.y - height, width, height, color, 2f);
            }

            //ESP Snapline
            if (Vars.esp_lines)
            {
                Render.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(footpos.x, (float)Screen.height - footpos.y), color, 1.5f);
            }

            // ESP Name
            if(Vars.esp_name)
            {
                Render.DrawString(new Vector2(footpos.x, (float)Screen.height - footpos.y + 5f), name.Replace("$", ""));
            }
        }


        public void CharacterLoop()
        {
            if (!Vars.inGame) return;

            foreach (Character character in Character.GetAllCharacters())
            {

                if (character == null || character.IsPlayer()) continue;

                Vector3 pivotPos = character.transform.position; // pivot point NOT at the feet, at the center
                Vector3 playerFootPos; playerFootPos.x = pivotPos.x; playerFootPos.z = pivotPos.z; playerFootPos.y = pivotPos.y - character.GetHeight(); //At the feet
                Vector3 playerHeadPos; playerHeadPos.x = pivotPos.x; playerHeadPos.z = pivotPos.z; playerHeadPos.y = pivotPos.y + character.GetHeight(); //At the head

                // screen position
                Vector3 w2s_footpos = Camera.main.WorldToScreenPoint(playerFootPos);
                Vector3 w2s_headpos = Camera.main.WorldToScreenPoint(playerHeadPos);

                if (Vars.tpAllEntitiesToPlayer)
                {
                    foreach (Character _character in Character.GetAllCharacters())
                    {
                        if (_character.IsPlayer()) continue;


                        _character.transform.position = new Vector3(
                            Player.m_localPlayer.transform.position.x,
                            Player.m_localPlayer.transform.position.y + 6f,
                            Player.m_localPlayer.transform.position.z);
                    }

                    Vars.tpAllEntitiesToPlayer = !Vars.tpAllEntitiesToPlayer;
                }

                if (w2s_footpos.z > 0f && Vars.esp_toggle)
                {
                    DrawBoxESP(w2s_footpos, w2s_headpos, UnityEngine.Color.white, character.m_name);
                }
            }
        }

        public void OnGUI()
        {
            MenuWindow.Draw();
            CharacterLoop();
            // Killaura.drawKillauraFOVCicle();
        }

        public void Start()
        {
            Init();
        }

        public void Update()
        {
           UserInput.PollInput();
           Killaura.KillauraUpdate();
           Player_Cache();
        }
    }
}
