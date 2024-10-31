using HarmonyLib;
using System;
using UnityEngine;
using ValheimChet;


// 7d2dmods.github.io/HarmonyDocs/index.htm?PrefixandPostfix.html
// Very gud resource, i recommend ;)
public class PatchEntryPoint
{
    public static void Init()
    {
        var harmony = new Harmony("com.valheimchet.hooks");
        harmony.PatchAll();
    }
}

// Token: 0x06000148 RID: 328 RVA: 0x0001046C File Offset: 0x0000E66C
// private void RPC_Damage(long sender, HitData hit)
[HarmonyPatch(typeof(Character), "RPC_Damage")]
public class OnDamage
{
    private static void Prefix(ref long sender, ref HitData hit)
    {
        try
        {
            if (Vars.noFall)
            {
                if (hit.m_hitType == HitData.HitType.Fall)
                {
                    hit.m_damage.m_damage = 0;
                    Chet.Log("Modified m_damage of HitData.HitType.Fall to: " + hit.m_damage.m_damage + "\n");
                }
            }

            if (Vars.noHurt)
            {
                // Does not work for some reason
                if (hit.m_hitType == HitData.HitType.EnemyHit)
                {
                    hit.m_damage.m_blunt = 0;
                    hit.m_damage.m_damage = 0;
                    Chet.Log("Damage set to: " + hit.m_damage.m_damage + " Blunt damage set to: " + hit.m_damage.m_blunt + "\n");
                }
            }

            if (Vars.oneTap)
            {
                // Imagine if I didn't find the RPC_Damage method (hell would've emerged)
                if (hit.GetAttacker() == Player.m_localPlayer)
                {
                    hit.m_damage.m_damage = float.MaxValue;
                    Chet.Log("hit.m_damage.m_damage set to: float.Maxvalue\n");
                }
            }
        }
        catch (System.Exception e)
        {
            Chet.ErrorLog($"Exception in patch of private void Character::RPC_Damage:\n{e}\n");
        }
    }
}

// Token: 0x060013B6 RID: 5046 RVA: 0x0008E3E4 File Offset: 0x0008C5E4
// private void Spawn(SpawnSystem.SpawnData critter, Vector3 spawnPoint, bool eventSpawner)
[HarmonyPatch(typeof(SpawnSystem), "Spawn")]
public class OnSpawn
{
    private static void Postfix(SpawnSystem.SpawnData critter, Vector3 spawnPoint, bool eventSpawner)
    {
        try
        {
            Chet.Log("Entity Spawned: " + critter.m_prefab.name + " at " + spawnPoint + "\n");
            Vars.EntitySpawnData.lastSpawnedEntity = critter;
        }
        catch (System.Exception e)
        {
            Chet.ErrorLog($"Exception in patch of private void SpawnSystem::Spawn:\n{e} \n");
        }
    }
}


// Token: 0x0600037E RID: 894 RVA: 0x00020A35 File Offset: 0x0001EC35
// private void RPC_UseStamina(long sender, float v)
[HarmonyPatch(typeof(Player), "RPC_UseStamina")]
public class OnStaminaUse
{
    private static void Postfix(long sender, float v)
    {   
        try { 
            if (Vars.noStamina)
            {
                Player.m_localPlayer.AddStamina(v);
            }
        } catch (System.Exception e)
        {
            Chet.ErrorLog($"Exception in patch of public private void Player::RPC_UseStamina:\n{e}\n");
        }
    }
}


// Token: 0x06000021 RID: 33 RVA: 0x00003548 File Offset: 0x00001748
// public bool Start(Humanoid character, Rigidbody body, ZSyncAnimation zanim, CharacterAnimEvent animEvent, VisEquipment visEquipment, ItemDrop.ItemData weapon, Attack previousAttack, float timeSinceLastAttack, float attackDrawPercentage)
[HarmonyPatch(typeof(Attack), "Start")]
public class OnAttackStart
{
    public static bool Prefix(Attack __instance, Humanoid character, Rigidbody body, ZSyncAnimation zanim, CharacterAnimEvent animEvent, VisEquipment visEquipment, ItemDrop.ItemData weapon, Attack previousAttack, float timeSinceLastAttack, float attackDrawPercentage)
    {
        try
        {
            if (!character.IsPlayer() || __instance.m_attackType != Attack.AttackType.None || __instance.m_attackType != Attack.AttackType.Projectile)
            {

                if (Vars.killauraEnabled)
                {
                    Vector3 cameraForward = Camera.main.transform.forward;

                    __instance.m_attackRange = Vars.killauraHitRange;
                    __instance.m_attackHeight = Vars.killauraHitHeight+0.5f;
                    Chet.Log($"Modified attack range to: {__instance.m_attackRange}");
                    Chet.Log($"Modified attack height to: {__instance.m_attackHeight}\n");
                }
                else
                {
                    if (Vars.hitRange_changer)
                    {
                        __instance.m_attackRange = Vars.currentHitRange;
                        Chet.Log($"Modified attack range to: {Vars.currentHitRange}\n");
                    }
                    if (Vars.hitHeight_changer)
                    {
                        __instance.m_attackHeight = Vars.currentHitHeight;
                        Chet.Log($"Modified attack height to: {Vars.currentHitHeight}\n");

                    }
                }

            }
        }
        catch (System.Exception e)
        {
            Chet.ErrorLog($"Exception in patch of public bool Attack::Start:\n{e}\n");
        }

        return true;
    }
}

// Token: 0x06000E19 RID: 3609 RVA: 0x0006C700 File Offset: 0x0006A900
// public float GetServerPing()
[HarmonyPatch(typeof(ZNet), "GetServerPing")]
public class OnServerPing
{
    public static bool Prefix(ref float __result)
    {
        try
        {
            Vars.serverPing = (int)__result;
            return true;
        }
        catch (System.Exception e)
        {
            Chet.ErrorLog($"Exception in patch of public float ZNet::GetServerPing:\n{e}\n");
            __result = 0f;
            return false;
        }
    }

    //public static float Prefix(float __result)
    //{
    //    try
    //    {
    //        Vars.serverPing = (int)__result;
    //        return __result;
    //    }
    //    catch (System.Exception e)
    //    {
    //        Chet.ErrorLog($"Exception in patch of public float ZNet::GetServerPing:\n{e}\n");
    //        return 0f;
    //    }
    //}
}

