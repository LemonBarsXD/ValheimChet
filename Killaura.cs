using System.Collections.Generic;
using UnityEngine;

namespace ValheimChet
{
    internal class Killaura
    {
        public static void KillauraUpdate()
        {
            if (!Vars.killauraEnabled || !Input.GetMouseButton(4) || !Vars.inGame) return;

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

        public static void TriggerAttack(Character enemy)
        {
            Vars.killauraTargetName = enemy.name;
            Player.m_localPlayer.AttackTowardsPlayerLookDir = true;

            Player.m_localPlayer.StartAttack(enemy, false);

            Player.m_localPlayer.AttackTowardsPlayerLookDir = false;
        }

        private static void LookAtEnemy(Character enemy)
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

        public static void drawKillauraFOVCicle()
        {
            if (Vars.killauraEnabled && Vars.killauraFOVCircle)
            {
                float radius = ((float)Screen.width * Mathf.Tan(Vars.killauraFOV * Mathf.Deg2Rad / 2) / ((Screen.width / Screen.height) * 0.5f));
                Render.DrawCircle(new Vector2(Screen.width / 2, Screen.height / 2), radius, 100, new Color(255, 255, 255), 2f);
            }
        }
    }
}
