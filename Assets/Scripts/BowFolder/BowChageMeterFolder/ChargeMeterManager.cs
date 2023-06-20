// --------------------------------------------------------- 
// ChageMeterManager.cs 
// 
// CreateDay: 2023/06/17
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

interface IChargeMeterManager
{
    void Charging();
    void ChargeReset();
}

public class ChargeMeterManager : MonoBehaviour, IChargeMeterManager
{
    #region variable 
    private Transform _myTransform = default;

    private IChargeMeter[] _chargeMeters = new IChargeMeter[2];
    #endregion
    #region property
    #endregion
    #region method
    private void Start()
    {
        _myTransform = this.transform;
        try
        {
            for (int i = 0; i < _chargeMeters.Length; i++)
            {
                _chargeMeters[i] = _myTransform.GetChild(i).GetComponent<ChargeMeterLight>();
            }
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("ChargeMeterLightがアタッチされていません");
        }
    }

    public void Charging()
    {
        for (int i = 0; i < _chargeMeters.Length; i++)
        {
            _chargeMeters[i].Charge();
        }
    }
    public void ChargeReset()
    {
        for (int i = 0; i < _chargeMeters.Length; i++)
        {
            _chargeMeters[i].ChargeReset();
        }
    }


    #endregion
}