// --------------------------------------------------------- 
// LockOnUIMove.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class LockOnUIMove : MonoBehaviour
{
    #region variable 
    #endregion
    #region property
    #endregion
    #region method
    Transform _player = default;

    private void Start()
    {
        _player = GameObject.FindWithTag(InhallLibTags.BowController).transform;
    }

    private void Update()
    {
        this.transform.LookAt(_player);
    }
    #endregion
}