using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUIManager : MonoBehaviour
{
    IFCanvasEvent[] canvasEvents = default;
    AudioSource source = default;
    [SerializeField] AudioClip seClip = default;
    private void Start()
    {
        canvasEvents = transform.GetComponentsInChildren<IFCanvasEvent>();
        source = GetComponent<AudioSource>();
        foreach(IFCanvasEvent action in canvasEvents)
        {
            action.DisableStart();
        }
    }

    public void TakeDamageUIEvent()
    {
        if(seClip is not null)
        {
            source.PlayOneShot(seClip);
        }

        int index = Random.Range(0, canvasEvents.Length);
        canvasEvents[index].EnableStart();
    }
}
