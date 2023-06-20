using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class VR_Trace_Value : MonoBehaviour
{
    Canvas left;
    Canvas right;
    Text leftEye;
    Text rightEye;
    

    private void Start()
    {
        left = new GameObject().AddComponent<Canvas>();

        right = new GameObject().AddComponent<Canvas>();

        left.renderMode = RenderMode.ScreenSpaceCamera;

        right.renderMode = RenderMode.ScreenSpaceCamera;

        left.worldCamera = GameObject.Find("LeftEyeAnchor").GetComponent<Camera>();

        right.worldCamera = GameObject.Find("RightEyeAnchor").GetComponent<Camera>();

        leftEye = new GameObject().AddComponent<Text>();

        rightEye = new GameObject().AddComponent<Text>();

        leftEye.transform.parent = left.transform;

        rightEye.transform.parent = right.transform;
    }

    
    public void SetText(string moji)
    {
        leftEye.text = moji;

        rightEye.text = moji;
    }
}
