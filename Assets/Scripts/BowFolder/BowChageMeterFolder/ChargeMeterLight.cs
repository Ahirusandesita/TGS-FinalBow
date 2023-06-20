// --------------------------------------------------------- 
// ChageMeterLight.cs 
// 
// CreateDay: 2023/06/17
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

interface IChargeMeter
{
    void Charge();
    void ChargeReset();
}
public class ChargeMeterLight : MonoBehaviour,IChargeMeter
{
    #region variable 
    private SpriteRenderer[] _chargeMeterSprites = new SpriteRenderer[10];

    private Transform _myTransform = default;

    private int _chargeIndex = 0;

    private Color _chargeMeterColoer = default;
    #endregion
    #region property
    #endregion
    #region method
    private void Start()
    {
        _chargeIndex = 0;
        _myTransform = this.transform;
        try
        {
            _chargeMeterColoer = _myTransform.GetChild(0).GetComponent<SpriteRenderer>().color;

            for (int i = 0; i < _chargeMeterSprites.Length; i++)
            {
                _chargeMeterSprites[i] = _myTransform.GetChild(i + 1).GetComponent<SpriteRenderer>();
                _chargeMeterSprites[i].enabled = false;
            }
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("SpriteRendererがアタッチされていません");
        }
    }

    public void Charge()
    {
        if (_chargeIndex == _chargeMeterSprites.Length)
        {
            for(int i = 0; i < _chargeMeterSprites.Length; i++)
            {
                _chargeMeterSprites[i].color = Color.red;
            }
        }

        if(_chargeIndex >= _chargeMeterSprites.Length)
        {
            return;
        }

        _chargeMeterSprites[_chargeIndex].enabled = true;
        _chargeIndex++;
    }
    public void ChargeReset()
    {
        _chargeIndex = 0;

        for(int i = 0; i < _chargeMeterSprites.Length; i++)
        {

            if (!_chargeMeterSprites[i].enabled)
            {
                return;
            }

            _chargeMeterSprites[i].color = _chargeMeterColoer;
            _chargeMeterSprites[i].enabled = false;
        }
    }

    #endregion
}