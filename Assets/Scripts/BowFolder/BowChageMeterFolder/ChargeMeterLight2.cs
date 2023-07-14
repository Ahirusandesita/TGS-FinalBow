// --------------------------------------------------------- 
// ChageMeterLight.cs 
// 
// CreateDay: 2023/06/17
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;

public class ChargeMeterLight2 : MonoBehaviour, IChargeMeter
{
    private const int FIRST_MATERIAL = 0;
    private const int MAX_STATE_MATERIAL = 1;
    #region variable 
    [SerializeField]private MeshRenderer[] _chargeMeterMeshes = new MeshRenderer[5];

    private Transform _myTransform = default;

    private int _chargeIndex = 0;

    [SerializeField] Material[] materials = new Material[2];

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
            for (int i = 0; i < _chargeMeterMeshes.Length; i++)
            {
                _chargeMeterMeshes[i].material = materials[FIRST_MATERIAL];
                _chargeMeterMeshes[i].enabled = false;
            }
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("MeshRendererがアタッチされていません");
        }
    }

    public void Charge()
    {
        if (_chargeIndex == _chargeMeterMeshes.Length)
        {
            for (int i = 0; i < _chargeMeterMeshes.Length; i++)
            {
                _chargeMeterMeshes[i].material = materials[MAX_STATE_MATERIAL];
            }
        }

        if (_chargeIndex >= _chargeMeterMeshes.Length)
        {
            return;
        }

        _chargeMeterMeshes[_chargeIndex].enabled = true;
        _chargeIndex++;
    }
    public void ChargeReset()
    {
        _chargeIndex = 0;

        for (int i = 0; i < _chargeMeterMeshes.Length; i++)
        {

            if (!_chargeMeterMeshes[i].enabled)
            {
                return;
            }

            _chargeMeterMeshes[i].material = materials[FIRST_MATERIAL];
            _chargeMeterMeshes[i].enabled = false;
        }
    }

    #endregion
}