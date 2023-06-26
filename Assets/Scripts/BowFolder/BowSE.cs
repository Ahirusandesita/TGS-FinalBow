// --------------------------------------------------------- 
// BowSE.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioClip))]
public class BowSE : MonoBehaviour
{

    #region variable 
    AudioClip _audio = default;
    #endregion
    #region property
    #endregion
    #region method

    private void Awake()
    {

    }

    private void Start()
    {
        _audio = GetComponent<AudioClip>();
    }

    private void Update()
    {

    }
    #endregion
}