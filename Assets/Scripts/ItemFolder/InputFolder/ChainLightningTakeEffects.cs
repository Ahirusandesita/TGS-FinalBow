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
    #endregion
    #region property
    #endregion
    #region method

    public GameObject SetEffects
    {
        set { effects = value; }
    }

    public GameObject CreateEffect(Vector3 fromPosition, Vector3 toPosition)
    {
        GameObject effect = Object.Instantiate(effects, (fromPosition + toPosition) / 2, Quaternion.FromToRotation(fromPosition, toPosition));
        Vector3 scale = effect.transform.localScale;
        scale.z = effect.transform.localScale.z * Vector3.Distance(fromPosition, toPosition);
        effect.transform.localScale = scale;
        return effect;
    }
    public void DeleteEffect(GameObject effect)
    {
        Object.Destroy(effect);
    }




    #endregion
}