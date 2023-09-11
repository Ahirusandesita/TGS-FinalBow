// --------------------------------------------------------- 
// TutorialData.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "TutorialData", menuName = "TutorialScriptables/CreateTutorialTable")]
public class TutorialData : ScriptableObject
{
    public string text;
    public Image image;
    public float speakingSpeed;
    public bool canNextText;
}
