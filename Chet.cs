// Author: Upwn
// Ver: 1.1.0

using System.Collections.Generic;
using UnityEngine;

/* -- TODO / Notes --
 * Ver 1.1.0
 * Fixed NoHurt 
 * Fixed NoStamina to work while attacking 
 * Added Spawn on player option for Custom Entity Spawner 
 * Added Killaura with customizable settings (not perfect, not bad either)
 * Added hitRange and hitHeight (does not affect killaura, pretty useless, just cool ig)
 * Added ping 
 * Implemented a better log method
 * Better code safety, bug and ud fixes etc.
   -- ----  /  ---- -- */

namespace ValheimChet
{
    internal class Chet : MonoBehaviour
    {
        static void Init()
        {
            System.Console.Clear();
            Chet.Log("Chet injected! Skill corrected! Targets detected! Mayhem perfected ;)");

            // Local Player
            Player localPlayer = Player.m_localPlayer;

            // Default Attack
            Attack defaultAttack = new Attack();

            // Bools
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

            // Floats
            Vars.defaultFov = Camera.main.fieldOfView;
            Vars.currentFov = Vars.defaultFov;

            Vars.killauraFOV = 90f;
            Vars.killauraRadius = 60f;

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

            // Other
            Vars.Prefabs.m_prefabHashDictionary = Reflections.GetNamedPrefabs();
        }

        public static void Log(string _str)
        {
            Debug.Log($"[ValheimChet] {_str}");
        }

        public static void ErrorLog(string _str)
        {
            Debug.LogError($"[ValheimChet] {_str}");
        }


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

        public static void SetBaseHPAndHealth(float value)
        {
            Player localPlayer = Player.m_localPlayer;

            localPlayer.m_baseHP = value;
            localPlayer.SetHealth(value);
        }
        static void Player_Cache()
        {
            Player localPlayer = Player.m_localPlayer;

            Quaternion localQuaternion = Quaternion.identity;

            // dont judge
            if (Vars.fov_changer)
            {
                Camera.main.GetComponent<GameCamera>().m_fov = Vars.currentFov;
            } else { Camera.main.GetComponent<GameCamera>().m_fov = Vars.defaultFov; }

            if (Vars.smoothCamera_toggle)
            {
                Camera.main.GetComponent<GameCamera>().m_smoothness = 0.1f;
            } else { Camera.main.GetComponent<GameCamera>().m_smoothness = 0.01f; }

            if (Vars.speed_changer)
            {
                localPlayer.m_speed = Vars.currentSpeed;
                localPlayer.m_swimSpeed = Vars.currentSpeed;
            } else { 
                localPlayer.m_speed = Vars.defaultSpeed;
                localPlayer.m_swimSpeed = Vars.defaultSwimSpeed;
            }

            if (Vars.runSpeed_changer)
            {
                localPlayer.m_runSpeed = Vars.currentRunSpeed;
            } else { localPlayer.m_runSpeed = Vars.defaultRunSpeed; }

            if (Vars.acceleration_changer)
            {
                localPlayer.m_acceleration = Vars.currentAcceleration;
            } else 
            { 
                localPlayer.m_acceleration= Vars.defaultAcceleration; 
                localPlayer.m_swimAcceleration= Vars.defaultSwimAcceleration; 
            }

            if (Vars.health_changer)
            {
                SetBaseHPAndHealth(Vars.currentBaseHP);
            } else { SetBaseHPAndHealth(Vars.defaultBaseHP); }

            if (Vars.noTurnDelay)
            {

                localPlayer.m_turnSpeed = 10000f;
                localPlayer.m_runTurnSpeed = 10000f;
                localPlayer.m_swimTurnSpeed = 10000f;
                localPlayer.m_flyTurnSpeed = 10000f;
            }
            else
            {
                // If modified by the devs in the future these will be wrong. But meh, they prob won't.
                localPlayer.m_turnSpeed = 300f;
                localPlayer.m_runTurnSpeed = 300f;
                localPlayer.m_swimTurnSpeed = 100f;
                localPlayer.m_flyTurnSpeed = 12f;
            }

        }

        public void KillauraUpdate()
        {
            if (!Vars.killauraEnabled || !Input.GetMouseButton(4)) return;

            Vector3 playerPos = Player.m_localPlayer.transform.position;
            Vector3 cameraForward = Camera.main.transform.forward;

            List<Character> enemies = Character.GetAllCharacters();

            float attackRange = Vars.killauraRadius;
            float cameraFOV = Vars.killauraFOV;

            Character closestEnemy = null;
            float closestDistance = float.MaxValue;

            foreach (Character enemy in enemies)
            {
                if (enemy.IsPlayer() || enemy.IsDead()) continue;

                Vector3 enemyPos = enemy.transform.position;
                float distanceToEnemy = Vector3.Distance(playerPos, enemyPos);

                if (distanceToEnemy <= attackRange)
                {
                    Vector3 directionToEnemy = (enemyPos - Camera.main.transform.position).normalized;
                    float angleToEnemy = Vector3.Angle(cameraForward, directionToEnemy);

                    // if the enemy is within the camera's FOV
                    if (angleToEnemy <= cameraFOV / 2f && distanceToEnemy < closestDistance)
                    {
                        Vars.killauraHitHeight = enemyPos.y - playerPos.y;
                        Vars.killauraHitRange = Vector2.Distance(new Vector2(playerPos.x, playerPos.z), new Vector2(enemyPos.x, enemyPos.z));
                        Player.m_localPlayer.GetTransform().LookAt(enemy.transform);
                        closestEnemy = enemy;
                        closestDistance = distanceToEnemy;
                    }
                }
            }

            // if a valid closest enemy is found within FOV, look at the enemy and attack
            if (closestEnemy != null)
            {
                LookAtEnemy(closestEnemy);
                TriggerAttack(closestEnemy);
            }
        }

        public void TriggerAttack(Character enemy)
        {
            Vars.killauraTargetName = enemy.name;
            Player.m_localPlayer.AttackTowardsPlayerLookDir = true;

            Player.m_localPlayer.StartAttack(enemy, false);

            Player.m_localPlayer.AttackTowardsPlayerLookDir = false;
        }

        private void LookAtEnemy(Character enemy)
        {
            Vector3 enemyPos = enemy.transform.position; enemyPos.y += 0.5f; // offset
            Vector3 cameraPos = Camera.main.transform.position;

            Vector3 directionToEnemy = (enemyPos - cameraPos).normalized;

            Vector3 currentForward = Camera.main.transform.forward;

            float currentYaw = Mathf.Atan2(currentForward.x, currentForward.z) * Mathf.Rad2Deg;
            float targetYaw = Mathf.Atan2(directionToEnemy.x, directionToEnemy.z) * Mathf.Rad2Deg;
            float deltaYaw = targetYaw - currentYaw;

            float currentPitch = Mathf.Asin(currentForward.y) * Mathf.Rad2Deg;
            float targetPitch = Mathf.Asin(directionToEnemy.y) * Mathf.Rad2Deg;
            float deltaPitch = targetPitch - currentPitch;

            Vector2 lookDirDelta = new Vector2(deltaYaw, deltaPitch);

            Player.m_localPlayer.SetMouseLook(lookDirDelta);
        }

        public void drawKillauraFOVCicle()
        {
            if (Vars.killauraEnabled && Vars.killauraFOVCircle)
            {
                float radius = ((float)Screen.width * Mathf.Tan(Vars.killauraFOV * Mathf.Deg2Rad / 2) / ((Screen.width / Screen.height) * 0.5f));
                Render.DrawCircle(new Vector2(Screen.width / 2, Screen.height / 2), radius, 100, new Color(255, 255, 255), 2f);
            }
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
            drawKillauraFOVCicle();
        }

        public void Start()
        {
            Init();
            MenuWindow.Init();
        }

        public void Update()
        {
           UserInput.PollInput();
           KillauraUpdate();
           Player_Cache();
        }
    }
}
