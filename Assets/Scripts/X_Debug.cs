// --------------------------------------------------------- 
// X_Debug.cs 
// 
// CreateDay: 2023/06/09
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;

/// <summary>
/// Unityのデバッグ用クラス
/// </summary>
public static class X_Debug
{
    public static void Log(object log)
    {
        #if UNITY_EDITOR
        System.Diagnostics.StackFrame caller = new System.Diagnostics.StackFrame(1);
        string className = caller.GetMethod().ReflectedType.FullName;
        string methodName = caller.GetMethod().Name;

        Debug.Log(log + "  > [" + className + " : " + methodName + "]");
        #endif
    }

    public static void LogWarning(object log)
    {
        #if UNITY_EDITOR
        System.Diagnostics.StackFrame caller = new System.Diagnostics.StackFrame(1);
        string className = caller.GetMethod().ReflectedType.FullName;
        string methodName = caller.GetMethod().Name;

        Debug.LogWarning(log + "  > [" + className + " : " + methodName + "]");
        #endif
    }

    public static void LogError(object log)
    {
        #if UNITY_EDITOR
        System.Diagnostics.StackFrame caller = new System.Diagnostics.StackFrame(1);
        string className = caller.GetMethod().ReflectedType.FullName;
        string methodName = caller.GetMethod().Name;

        Debug.LogError(log + "  > [" + className + " : " + methodName + "]");
        #endif
    }
}