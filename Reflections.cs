using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Private methods and fields accessed/called by Reflection
public class Reflections
{

    // Token: 0x060013B6 RID: 5046 RVA: 0x0008E3E4 File Offset: 0x0008C5E4
    // private void Spawn(SpawnSystem.SpawnData critter, Vector3 spawnPoint, bool eventSpawner)
    public static void spawn(SpawnSystem.SpawnData critter, Vector3 spawnPoint, bool eventSpawner = false)
    {
        SpawnSystem classInstance = GameObject.FindObjectOfType<SpawnSystem>();


        Type type = classInstance.GetType();

        MethodInfo spawnMethod = type.GetMethod("Spawn", BindingFlags.NonPublic | BindingFlags.Instance);

        if (spawnMethod != null)
        {
            // Call
            object[] parameters = { critter, spawnPoint, eventSpawner };
            spawnMethod.Invoke(classInstance, parameters);
        }
        else
        {
            Debug.LogError("Method 'Spawn' not found!");
        }
    }


    // Token: 0x04000DE3 RID: 3555
    // private readonly Dictionary<int, GameObject> m_namedPrefabs = new Dictionary<int, GameObject>();
    public static Dictionary<int, GameObject> GetNamedPrefabs()
    {
        ZNetScene zNetScene = ZNetScene.instance;

        if (zNetScene == null)
        {
            Debug.LogError("ZNetScene instance is null");
            return null;
        }

        FieldInfo namedPrefabsField = typeof(ZNetScene).GetField("m_namedPrefabs", BindingFlags.NonPublic | BindingFlags.Instance);

        if (namedPrefabsField != null)
        {
            // get m_namedPrefabs and return it
            Dictionary<int, GameObject> namedPrefabs = namedPrefabsField.GetValue(zNetScene) as Dictionary<int, GameObject>;

            return namedPrefabs;
        }
        else
        {
            Debug.LogError("Field 'm_namedPrefabs' not found.");
            return null;
        }

    }
}
