// --------------------------------------------------------- 
// ChainLightningTakeEffects.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class ChainLightningTakeEffects
{
    #region variable 
    GameObject effects = default;

    LineRendererControlAssets line = new();
    #endregion
    #region property
    #endregion
    #region method

    public GameObject SetEffects
    {
        set
        {            
            effects = value;
            
        }
    }

    public GameObject CreateEffect(Vector3 fromPosition, Vector3 toPosition)
    {
        GameObject effect = Object.Instantiate(effects, fromPosition, Quaternion.FromToRotation(fromPosition, toPosition));
        line.SetControllLine(effect);
        line.LineSetIndex(Vector3.Distance(fromPosition, toPosition));
        effect.transform.LookAt(toPosition);
        return effect;
    }
    public void DeleteEffect(GameObject effect)
    {
        Object.Destroy(effect);
    }




    #endregion
}