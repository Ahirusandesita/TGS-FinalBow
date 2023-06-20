// --------------------------------------------------------- 
// MouseDebug.cs 
// 
// CreateDay: 2023/06/15
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class MouseDebug : MonoBehaviour
{
    private Camera _mainCamera = default;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (GetClickedObject() != null)
        {
            GetClickedObject().GetComponent<EnemyStats>().TakeDamage(1000);
        }
    }

    private GameObject GetClickedObject()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool isHit = Physics.Raycast(ray, out hit, 1 << 6);
        
        if (isHit && Input.GetMouseButtonDown(0))
        {
            return hit.collider.gameObject;
        }

        return null;
    }
}