// --------------------------------------------------------- 
// ItemShooter.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class ItemShooter : MonoBehaviour,IFCanTakeArrowButton
{
    [SerializeField] ItemShotObjectScriptable data;
    GameObject[] shotObjects;
    float rapidSpeed;

    public void ButtonPush()
    {
        
    }

    private void Initialize()
    {
        
    }
}