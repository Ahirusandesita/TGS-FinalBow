// --------------------------------------------------------- 
// ItemMoveTest.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
public class ItemMoveTest : MonoBehaviour
{
    #region variable 
    public bool _isStart = false;

    private Transform _goalTransform = default;
    private float distance = default;

    #endregion

    #region property

    #endregion

    #region method

    private void Update()
    {
        ItemMove();
    }


    private void ItemMove()
    {
        if (_isStart)
        {







        }
    }


    public Transform SetGoalPosition
    {
        get
        {
            return _goalTransform;
        }

        set
        {
            _goalTransform = value;
        }

    }
}

    #endregion
