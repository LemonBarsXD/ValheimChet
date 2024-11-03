using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using ValheimChet;

// Private methods and fields accessed/called by Reflection
public class Reflections
{

    // Token: 0x06001417 RID: 5143 RVA: 0x00091E60 File Offset: 0x00090060
    // private void Spawn(SpawnSystem.SpawnData critter, Vector3 spawnPoint, bool eventSpawner)
    public static void spawn(SpawnSystem.SpawnData critter, Vector3 spawnPoint, bool eventSpawner = false, int minLevelOverride = -1, float levelUpMultiplier = 1f)
    {
        SpawnSystem classInstance = GameObject.FindObjectOfType<SpawnSystem>();


        Type type = classInstance.GetType();

        MethodInfo spawnMethod = type.GetMethod("Spawn", BindingFlags.NonPublic | BindingFlags.Instance);

        if (spawnMethod != null)
        {
            object[] parameters = { critter, spawnPoint, eventSpawner, minLevelOverride, levelUpMultiplier };
            spawnMethod.Invoke(classInstance, parameters);
        }
        else
        {
            Chet.ErrorLog("Method 'Spawn' not found!");
        }
    }


    // Token: 0x04000DE3 RID: 3555
    // private readonly Dictionary<int, GameObject> m_namedPrefabs = new Dictionary<int, GameObject>();
    public static Dictionary<int, GameObject> GetNamedPrefabs()
    {
        ZNetScene zNetScene = ZNetScene.instance;

        if (zNetScene == null)
        {
            Chet.ErrorLog("ZNetScene instance is null");
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
            Chet.ErrorLog("Field 'm_namedPrefabs' not found.");
            return null;
        }

    }
}
