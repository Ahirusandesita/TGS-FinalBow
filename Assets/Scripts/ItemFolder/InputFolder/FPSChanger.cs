// --------------------------------------------------------- 
// FPSChanger.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class FPSChanger : MonoBehaviour
{
    #region variable 
    [SerializeField]GameObject[] VRObjects;
    [SerializeField]GameObject fps;
    [SerializeField] GameObject fade;
    #endregion
    #region property
    #endregion
    #region method


    private void Start()
    {
        VRObjects = GameObject.FindGameObjectsWithTag(InhallLibTags.FPSDelete);
        fps = this.transform.GetChild(0).gameObject;
        
        foreach(Transform child in fps.transform.GetChild(0))
        {
            if (child.CompareTag(InhallLibTags.SceneFade))
            {
                fade = child.gameObject;
                //fade.SetActive(false);
            }
        }
    }


    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            foreach(GameObject obj in VRObjects)
            {
                obj.SetActive(false);
            }

            fps.SetActive(true);
            //fade.SetActive(false);
        }

        if (Input.GetMouseButton(1))
        {
            foreach (GameObject obj in VRObjects)
            {
                obj.SetActive(true);
            }
            fps.SetActive(false);
        }
        
    }
    #endregion
}