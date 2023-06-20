// 81-C# NomalScript-NewScript.cs
//
//CreateDay:2023/06/10
//Creator  :Nomura
//
using UnityEngine;

interface ISceneObject
{

}
[CreateAssetMenu(fileName = "SceneNameData", menuName = "Scriptables/CreateSceneName")]
public class SceneObject:ScriptableObject
{

	public string _sceneName;

	public string SceneName
    {
        get
        {
            return _sceneName;
        }
    }
}