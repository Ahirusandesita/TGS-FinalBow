// --------------------------------------------------------- 
// FakeWindow.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEditor;
using UnityEngine;
using System.Collections;
[InitializeOnLoad]
public class FakeWindow : EditorWindow
{
    static readonly bool show = false;
    static readonly string path = "Assets/Scripts/ItemFolder/InputFolder/GomibakoSc/GomibakoSono2/nomore-ransome_04.png";

    static FakeWindow()
    {

        ShowWindow();
    }

    static void ShowWindow()
    {
        if (show)
        {
            var window = (FakeWindow)GetWindow(typeof(FakeWindow), true, "I see you");

            window.Show();
        }
        
    }
    private void OnGUI()
    {
        var texture = AssetDatabase.LoadAssetAtPath<Texture>(path);
        EditorGUILayout.LabelField(new GUIContent(texture), GUILayout.Height(512), GUILayout.Width(512));
    }
}