// --------------------------------------------------------- 
// TestText.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class TestText : MonoBehaviour,ITextLikeSpeaking
{
    #region variable 
    public TutorialManagementData tutorialManagementData;
    #endregion
    #region property
    #endregion
    #region method

    private void Awake()
    {

    }

    private void Start()
    {
        GameObject.FindObjectOfType<TextSystem>().TextLikeSpeaking(tutorialManagementData,this);
    }

    private void Update()
    {

    }

    public void IsComplete()
    {
        Debug.Log("設定文字すべて出力");
    }
    #endregion
}