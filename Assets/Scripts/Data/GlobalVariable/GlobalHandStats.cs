// 81-C# FactoryScript-FactoryScript.cs
//
//CreateDay:
//Creator  :
//

using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GrobalHandStats", menuName = "Scriptables/GrobalHandStatsData")]
public class GlobalHandStats:ScriptableObject
{

	public InputManagement.EmptyHand empty = default;

    public InputManagement.EmptyHand firstEmptyHand = InputManagement.EmptyHand.Right;

	public InputManagement.EmptyHand SaveHands
    {
        set
        {
            empty = GameObject.FindGameObjectWithTag(InhallLibTags.InputController).GetComponent<InputManagement>().P_EmptyHand;

        }
        get
        {
            return empty;
        }
    }

    private void OnEnable()
    {
        X_Debug.Log("aaa�Ă΂ꂽ");
        empty = firstEmptyHand;
    }

}
