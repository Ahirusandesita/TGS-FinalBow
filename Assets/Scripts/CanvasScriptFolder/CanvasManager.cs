// --------------------------------------------------------- 
// CanvasManager.cs 
// 
// CreateDay: 2023/06/18
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;

interface ICanvasManager
{
    void StagingDamage();
    void StagingRecovery();
}

public class CanvasManager : MonoBehaviour,ICanvasManager
{
    #region variable 
    private Transform _myTransform;
    private ICanvasControll[] _canvasControll = new ICanvasControll[2];
    #endregion
    #region property
    #endregion
    #region method

    private void Start()
    {
        _myTransform = this.transform;
        for (int i = 0; i < _canvasControll.Length; i++)
        {
            _canvasControll[i] = _myTransform.GetChild(i).GetComponent<CanvasControll>();
        }
    }

    public void StagingDamage()
    {
        for(int i = 0; i < _canvasControll.Length; i++)
        {
            _canvasControll[i].LifeDamage();
        }
    }
    public void StagingRecovery()
    {
        for (int i = 0; i < _canvasControll.Length; i++)
        {
            _canvasControll[i].LifeRecovery();
        }
    }


    #endregion
}