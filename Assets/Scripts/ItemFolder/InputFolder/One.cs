// --------------------------------------------------------- 
// One.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class One : MonoBehaviour
{

    private void OnEnable()
    {
        //print("out");
        ParticleSystem a = GetComponent<ParticleSystem>();
        a.Clear();
        a.Play(true);
        //print(a.isPlaying);
        //print(a.isPaused);
        //print(a.isStopped);
        //print(a.isEmitting);
        //print("nour");
    }
}