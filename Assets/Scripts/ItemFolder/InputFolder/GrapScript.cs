// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Sasaki
// --------------------------------------------------------- 
using UnityEngine;

public class GrapScript : MonoBehaviour
{
    public GameObject hand;
    public InputManagement mng;

    Transform parent;
    Vector3 firstpos;
    Quaternion rote;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
        firstpos = transform.localPosition;
        rote = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(mng.ButtonLeftUpTrigger())
        {
            if (Vector3.Distance(hand.transform.position, transform.position) < 0.5f)
            {
                transform.parent = hand.transform;
            }
        }
        else
        {
            transform.parent = parent;
            transform.localPosition = firstpos;
            transform.localRotation = rote;
        }
    }
}
