// --------------------------------------------------------- 
// ThunderDriver.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class ThunderDriver : MonoBehaviour
{
    [SerializeField] ChainLightningManager chainLightning = default;

    bool a = true;

    private void Start()
    {
        FindObjectOfType<StageManager>().transform.position = transform.position;
    }
    private void Update()
    {
        if (!Input.GetKey(KeyCode.T))
        {
            a = true;
        }
       
        if (Input.GetKey(KeyCode.T) && a)
        {
            a = false;
            chainLightning.ChainLightning(transform, 6,1);
        }

        if (Input.GetKey(KeyCode.Backspace))
        {
            foreach (TestHpGageUse a in FindObjectsOfType<TestHpGageUse>())
            {
                a.gameObject.SetActive(false);
            }

            foreach(Reaction a in FindObjectsOfType<Reaction>())
            {
                a.gameObject.SetActive(false);
            }
        }
    }
}