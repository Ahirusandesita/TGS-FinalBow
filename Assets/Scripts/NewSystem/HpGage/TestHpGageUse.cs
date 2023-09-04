// --------------------------------------------------------- 
// TestHpGageUse.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class TestHpGageUse : MonoBehaviour
{
    #region variable 
    private HpGage hpGage;
    public Camera useCamera;
    #endregion
    #region property
    #endregion
    #region method

    private void Awake()
    {

    }

    private void Start()
    {
        hpGage = GameObject.FindObjectOfType<CreateHpGage>().Create();
        hpGage.SetCamera(useCamera);
    }

    private void Update()
    {
        hpGage.SetPosition(this.transform);
    }
    #endregion
}