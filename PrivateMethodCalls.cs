using System;
using System.Reflection;
using UnityEngine;
using ValheimChet;

public class EntitySpawner
{

    // Token: 0x060013B6 RID: 5046 RVA: 0x0008E3E4 File Offset: 0x0008C5E4
    // private void Spawn(SpawnSystem.SpawnData critter, Vector3 spawnPoint, bool eventSpawner)
    public static void spawn(SpawnSystem.SpawnData critter, Vector3 spawnPoint, bool eventSpawner = false)
    {
        SpawnSystem classInstance = GameObject.FindObjectOfType<SpawnSystem>();


        Type type = classInstance.GetType();

        // Get the MethodInfo for the 'Spawn' method
        MethodInfo spawnMethod = type.GetMethod("Spawn", BindingFlags.NonPublic | BindingFlags.Instance);

        if (spawnMethod != null)
        {
            // Call method 
            object[] parameters = { critter, spawnPoint, eventSpawner };
            spawnMethod.Invoke(classInstance, parameters);
        }
        else
        {
            Debug.LogError("Method 'Spawn' not found!");
        }
    }
}
