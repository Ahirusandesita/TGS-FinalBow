// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjcectSettingData", menuName = "Scriptables/CreateObjectSetting")]
public class ObjectData : ScriptableObject
{
    public List<Setting> settings = new List<Setting>();
}
[System.Serializable]
public class SettingObject<T>
{
    public T t;
    public SettingObject(T t)
    {
        this.t = t;
    }
}
[System.Serializable]
public class SettingGameObject : SettingObject<GameObject>
{
    public SettingGameObject(GameObject t) : base(t)
    {
        this.t = t;
    }
}
[System.Serializable]
public class SettingTransform : SettingObject<Transform>
{
    public SettingTransform(Transform t) : base(t)
    {
        this.t = t;
    }
}
[System.Serializable]
public class Setting
{
    public string name;
    public SettingGameObject settingGameObject;
    public SettingTransform settingTransform;
}
