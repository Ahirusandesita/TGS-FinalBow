using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUIManager : MonoBehaviour
{
    IFCanvasEvent[] canvasEvents = default;

    private void Start()
    {
        canvasEvents = transform.GetComponentsInChildren<IFCanvasEvent>();
        foreach(IFCanvasEvent action in canvasEvents)
        {
            action.DisableStart();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            int index = Random.Range(0, canvasEvents.Length);
            canvasEvents[index].EnableStart();
        }
    }
}
