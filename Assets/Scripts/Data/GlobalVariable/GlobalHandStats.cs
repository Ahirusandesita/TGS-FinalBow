// 81-C# FactoryScript-FactoryScript.cs
//
//CreateDay:
//Creator  :
//

using UnityEngine;


[CreateAssetMenu(fileName = "GrobalHandStats", menuName = "Scriptables/GrobalHandStatsData")]
public class GlobalHandStats : ScriptableObject
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

    public void Initialize()
    {
        empty = firstEmptyHand;
    }

    public void Awake()
    {
        empty = firstEmptyHand;
    }
}
