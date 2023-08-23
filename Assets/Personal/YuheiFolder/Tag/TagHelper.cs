// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEditor;
using UnityEngine;
/// <summary>
/// このクラスは使用しない
/// </summary>
public class TagHelper : MonoBehaviour
{
    /*
    private void Awake()
    {
        
        for(int i = 0; i < TagName.tagName.Length; i++)
        {
            AddTag(TagName.tagName[i]);
        }
        Debug.Log("タグをセットした");
        TagName.AddTags = true;
    }
    static void AddTag(string tagName)
    {
        UnityEngine.Object[] asset = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset");
        if ((asset != null) && (asset.Length > 0))
        {
            SerializedObject so = new SerializedObject(asset[0]);
            SerializedProperty tags = so.FindProperty("tags");

            for(int i = 0; i < tags.arraySize; ++i)
            {
                if (tags.GetArrayElementAtIndex(i).stringValue == tagName)
                {
                    return;
                }
            }

            int index = tags.arraySize;
            tags.InsertArrayElementAtIndex(index);
            tags.GetArrayElementAtIndex(index).stringValue = tagName;
            so.ApplyModifiedProperties();
            so.Update();
        }
    }
    */
}
