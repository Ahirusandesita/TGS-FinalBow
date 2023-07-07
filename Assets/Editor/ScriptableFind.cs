// 81-C# NomalScript-NewScript.cs
//
//CreateDay:
//Creator  :
//
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
public class ScriptableFind
{

    public static ScriptableObject ScriptableObjectFind(ScriptableEnum.scriptableName scriptableName, ScriptableEnum.name name)
    {
        string[] scriptable = UnityEditor.AssetDatabase.FindAssets("t:ScriptableData");
        string scriptablePath = AssetDatabase.GUIDToAssetPath(scriptable[0]);
        ScriptableData scriptableData = AssetDatabase.LoadAssetAtPath<ScriptableData>(scriptablePath);


        for (int i = 0; i < scriptableData.scriptables.Count; i++)
        {
            if(scriptableName == scriptableData.scriptables[i].name)
            {
                for(int j = 0; j < scriptableData.scriptables[i].scriptables.Count; j++)
                {
                    if(name == scriptableData.scriptables[i].scriptables[j].name)
                    {
                        return scriptableData.scriptables[i].scriptables[j].scriptableObject;
                    }
                }
            }
        }
        return null;
    }





    public static List<ScriptableObject> ScriptableObjectsFind(string scriptableObjectName)
    {
        string[] scriptables = UnityEditor.AssetDatabase.FindAssets("t:" + scriptableObjectName);
        if (scriptables.Length == 0)
        {
            throw new System.IO.FileNotFoundException("MyScriptableObject does not found");
        }

        List<string> paths = new List<string>();
        for (int i = 0; i < scriptables.Length; i++)
        {
            paths.Add(AssetDatabase.GUIDToAssetPath(scriptables[i]));
        }

        List<ScriptableObject> scriptableObjects = new List<ScriptableObject>();
        for (int i = 0; i < paths.Count; i++)
        {
            scriptableObjects.Add(AssetDatabase.LoadAssetAtPath<ScriptableObject>(paths[i]));
        }
        return scriptableObjects;
    }

}