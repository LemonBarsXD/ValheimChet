// Author: Upwn
// Ver: 1.0.0

using UnityEngine;

/* -- TODO / Notes --
 * Custom entity spawn (possible)
 * First person (lol)
 * NoDamage implementation (maybe possible)
 * Global Custom Messages (maybe possible)
   -- ----  /  ---- -- */

namespace ValheimChet
{
    internal class Chet : MonoBehaviour
    {

        static void Init()
        {
            // Local Player
            Player localPlayer = Player.m_localPlayer;
            
            // bools
            Vars.menu_toggle = true;
            Vars.fov_changer = false;
            Vars.speed_changer = false;
            Vars.runSpeed_changer = false;
            Vars.acceleration_changer = false;
            Vars.skill_changer = false;
            Vars.health_changer = false;
            Vars.noTurnDelay = false;
            Vars.noFall = false;
            Vars.noStamina = false;
            Vars.smoothCamera_toggle = true;
            Vars.tpAllEntitiesToPlayer = false;
            Vars.esp_toggle = false;
            Vars.esp_boxes = false;
            Vars.esp_lines = false;

            // floats
            Vars.defaultFov = Camera.main.fieldOfView;
            Vars.currentFov = Vars.defaultFov;

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

            Vars.defaultJumpStaminaUsage = localPlayer.m_jumpStaminaUsage;
            Vars.defaultDodgeStaminaUsage = localPlayer.m_dodgeStaminaUsage;
        }

        public static void SetBaseHPAndHealth(float value)
        {
            Player _localPlayer = Player.m_localPlayer;

            _localPlayer.m_baseHP = value;
            _localPlayer.SetHealth(value);
        }
        static void Player_Cache()
        {
            // i know these look bad (send halp plz)
            if (Vars.fov_changer)
            {
                Camera.main.fieldOfView = Vars.currentFov;
            } else { Camera.main.fieldOfView = Vars.defaultFov; }

            if (Vars.smoothCamera_toggle)
            {
                Camera.main.GetComponent<GameCamera>().m_smoothness = 0.1f;
            } else { Camera.main.GetComponent<GameCamera>().m_smoothness = 0.01f; }

            if (Vars.speed_changer)
            {
                Player.m_localPlayer.m_speed = Vars.currentSpeed;
                Player.m_localPlayer.m_swimSpeed = Vars.currentSpeed;
            } else { 
                Player.m_localPlayer.m_speed = Vars.defaultSpeed;
                Player.m_localPlayer.m_swimSpeed = Vars.defaultSwimSpeed;
            }

            if (Vars.runSpeed_changer)
            {
                Player.m_localPlayer.m_runSpeed = Vars.currentRunSpeed;
            } else { Player.m_localPlayer.m_runSpeed = Vars.defaultRunSpeed; }

            if (Vars.acceleration_changer)
            {
                Player.m_localPlayer.m_acceleration = Vars.currentAcceleration;
            } else 
            { 
                Player.m_localPlayer.m_acceleration= Vars.defaultAcceleration; 
                Player.m_localPlayer.m_swimAcceleration= Vars.defaultSwimAcceleration; 
            }

            if (Vars.health_changer)
            {
                SetBaseHPAndHealth(Vars.currentBaseHP);
            } else { SetBaseHPAndHealth(Vars.defaultBaseHP); }



            // Hard coded but it's fine... untill its not. If these values are modified by the devs in the future these will be wrong.
            if (Vars.noStamina)
            {
                Player.m_localPlayer.m_jumpStaminaUsage = 0f;
                Player.m_localPlayer.m_dodgeStaminaUsage = 0f;
                Player.m_localPlayer.m_blockStaminaDrain = 0f;
                Player.m_localPlayer.m_encumberedStaminaDrain = 0f;
                Player.m_localPlayer.m_swimStaminaDrainMaxSkill = 0f;
                Player.m_localPlayer.m_swimStaminaDrainMinSkill = 0f;
                Player.m_localPlayer.m_equipStaminaDrain = 0f;
                Player.m_localPlayer.m_runStaminaDrain = 0f;
                Player.m_localPlayer.m_sneakStaminaDrain = 0f;
            } else 
            { 
                Player.m_localPlayer.m_jumpStaminaUsage = 10f;
                Player.m_localPlayer.m_dodgeStaminaUsage = 15f;
                Player.m_localPlayer.m_blockStaminaDrain = 10f;
                Player.m_localPlayer.m_encumberedStaminaDrain = 5f;
                Player.m_localPlayer.m_equipStaminaDrain = 6f;
                Player.m_localPlayer.m_runStaminaDrain = 8f;
                Player.m_localPlayer.m_sneakStaminaDrain = 5f;
            }

            // Same here. If modified by the devs in the future these will be wrong.
            if (Vars.noTurnDelay)
            {

                CallMethodPrivate._Spawn();
                Vars.noTurnDelay = !Vars.noTurnDelay;
                FieldsChecker.Start();

                //Player.m_localPlayer.m_turnSpeed = 10000f;
                //Player.m_localPlayer.m_runTurnSpeed = 10000f;
                //Player.m_localPlayer.m_swimTurnSpeed = 10000f;
                //Player.m_localPlayer.m_flyTurnSpeed = 10000f;
            }
            else
            {
                Player.m_localPlayer.m_turnSpeed = 300f;
                Player.m_localPlayer.m_runTurnSpeed = 300f;
                Player.m_localPlayer.m_swimTurnSpeed = 100f;
                Player.m_localPlayer.m_flyTurnSpeed = 12f;
            }

            if(Vars.spawnEntity_toggle)
            {
                CallMethodPrivate._Spawn();
                Vars.noTurnDelay = !Vars.noTurnDelay;
                FieldsChecker.Start();
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

        public void OnGUI()
        {
            MenuWindow.Draw();

            foreach (Character character in Player.GetAllCharacters())
            { 

                if(character == null || character.IsPlayer()) continue;

                Vector3 pivotPos = character.transform.position; //Pivot point NOT at the feet, at the center
                Vector3 playerFootPos; playerFootPos.x = pivotPos.x; playerFootPos.z = pivotPos.z; playerFootPos.y = pivotPos.y - character.GetHeight() ; //At the feet
                Vector3 playerHeadPos; playerHeadPos.x = pivotPos.x; playerHeadPos.z = pivotPos.z; playerHeadPos.y = pivotPos.y + character.GetHeight(); //At the head

                //Screen Position
                Vector3 w2s_footpos = Camera.main.WorldToScreenPoint(playerFootPos);
                Vector3 w2s_headpos = Camera.main.WorldToScreenPoint(playerHeadPos);

                if (Vars.tpAllEntitiesToPlayer)
                {
                    foreach (Character _character in Player.GetAllCharacters())
                    {
                        if(_character.IsPlayer()) continue;


                        _character.transform.position = new Vector3(
                            Player.m_localPlayer.transform.position.x, 
                            Player.m_localPlayer.transform.position.y + 10f, 
                            Player.m_localPlayer.transform.position.z);
                    }

                    Vars.tpAllEntitiesToPlayer = !Vars.tpAllEntitiesToPlayer;
                }

                if (w2s_footpos.z > 0f && Vars.esp_toggle)
                {
                    DrawBoxESP(w2s_footpos, w2s_headpos, Color.white, character.m_name);
                }
            }
        }

        public void Start()
        {
            Init();
            MenuWindow.Init();
        }

        public void Update()
        {
           UserInput.PollInput();
           Player_Cache();
        }
    }
}
