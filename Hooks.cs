using HarmonyLib;
using System;
using UnityEngine;
using ValheimChet;


// Does not work, im starting to think fall damage is server sided
// Currently about 7 days later and i can now proudly say that i've found tha solution :) 
/* 
 * Lesson learned:
 * This game has thousands of different functions that modifies your health one way or another.
 * That made it confusing for me to find the right function to hook.
 * Therefore, if you're trying to hook anything in this and it doesn't work, don't make the same mistake as me. 
 * Dig deep down into the structure of the game and the game classes to find the right function and if they don't work try another or another approach. I believe in you :)
*/
public class ModEntryPoint
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
        if(Vars.noFall)
        {
            if (hit.m_hitType == HitData.HitType.Fall)
            {
                hit.m_damage.m_damage = 0;
                Debug.Log("Modified m_damage of HitData.HitType.Fall to: " + hit.m_damage.m_damage);
            }
        }

        if(Vars.noHurt)
        {
            // It's a joke how easy it is XDDD
            if(hit.GetAttacker() != Player.m_localPlayer)
            {
                hit.m_damage.m_damage = 0;
            }
        }

        if(Vars.oneTap)
        {
            // Imagine if I didn't find the RPC_Damage method (hell would've emerged)
            if (hit.GetAttacker() == Player.m_localPlayer)
            {
                hit.m_damage.m_damage = float.MaxValue;
            }
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
        Debug.Log("Entity Spawned: " + critter.m_prefab.name);
        Vars.EntitySpawnData.lastSpawnedEntity = critter;
    }
}

// Token: 0x06000254 RID: 596 RVA: 0x00014C90 File Offset: 0x00012E90
// private ZNetView Spawn()
//[HarmonyPatch(typeof(CreatureSpawner), "Spawn")]
//public class OnCreatureSpawn
//{
//    private void Postfix(CreatureSpawner _instance)
//    {
//        Debug.Log(_instance.m_creaturePrefab.name);

//        //return _instance.GetComponent<ZNetView>();
//    }
//}