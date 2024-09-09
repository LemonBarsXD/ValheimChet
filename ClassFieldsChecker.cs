// used for debug purposes, can be deleted.

using System.Reflection;
using System.IO;
using UnityEngine;

public class FieldsChecker
{
    public static void Start()
    {
        string path = Path.Combine(Application.dataPath, "DebugLog.txt");

        using (StreamWriter writer = new StreamWriter(path, false))
        {
            SpawnSystem localPlayer = SpawnSystem.FindObjectOfType<SpawnSystem>();

            if (localPlayer != null)
            {
                System.Type currentType = localPlayer.GetType();

                while (currentType != null)
                {
                    writer.WriteLine("Class: " + currentType.Name);

                    // Write all fields in the current class
                    writer.WriteLine("Fields:");
                    FieldInfo[] fields = currentType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                    foreach (FieldInfo field in fields)
                    {
                        writer.WriteLine(field.Name + " (" + field.FieldType + ")");
                    }

                    // Write all properties in the current class
                    writer.WriteLine("\nProperties:");
                    PropertyInfo[] properties = currentType.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                    foreach (PropertyInfo property in properties)
                    {
                        writer.WriteLine(property.Name + " (" + property.PropertyType + ")");
                    }

                    writer.WriteLine("\n---");

                    // Move to the base class
                    currentType = currentType.BaseType;
                }

                writer.WriteLine("\nData saved successfully.");
            }
            else
            {
                writer.WriteLine("Player instance not found.");
            }
        }

        Debug.Log("Fields and properties including base classes have been written to " + path);
    }
}

