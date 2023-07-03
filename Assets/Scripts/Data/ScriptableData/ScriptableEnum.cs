// 81-C# NomalScript-NewScript.cs
//
//CreateDay:
//Creator  :

using System;
public class ScriptableEnum
{
    public enum scriptableName
    {
        CreateAnimationCurve,
        EnchantUIData,
        EnemySpwanerTable,
        ObjectData,
        NumberObjectData,
        PoolObjectParamTable,
        SceneObject,
        SoundParamTable,
        TagObject

    };

    public static int GetScriptableNameEnumLength()
    {
        return Enum.GetNames(typeof(scriptableName)).Length;
    }

    public static scriptableName GetScriptableNameEnum(int index)
    {
        return (scriptableName)Enum.ToObject(typeof(scriptableName), index);
    }



    public enum name
    {
        ShotAmplituteCurve,
        ShotFrequencyCurve,
        EnchantUI,
        EnemySpawnerData,
        ObjectSettingData,
        NumberObjectData,
        PoolObjectData,
        Exit,
        GameStart,
        Result,
        Title,
        NewEnchantSound,
        BombHitSound,
        BombHomingHitSound,
        BombKnockBackHitSound,
        BombPenetrateHitSound,
        BombThunderHitSound,
        HomingHitSound,
        HomingPenetrateHitSound,
        KnockBackHitSound,
        KnockBackHomingHitSound,
        KnockBackPenetrateHitSound,
        NormalHitSound,
        PenetrateHitSound,
        ThunderHitSound,
        ThunderKnockBackHitSound,
        ThunderHomingHitSound,
        ThunderPenetrateHitSound,
    }

    public static int GetNameEnumLength()
    {
        return Enum.GetNames(typeof(name)).Length;
    }
    public static name GetNameEnum(int index)
    {
        return (name)Enum.ToObject(typeof(name), index);
    }
}