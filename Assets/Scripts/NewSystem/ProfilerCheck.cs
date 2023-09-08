// --------------------------------------------------------- 
// ProfilerCheck.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
public class ProfilerCheck
{
    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
    public void HeavyFuncStart()
    {
        sw.Start();
        
    }
    public void HeavyEuncEnd(string name)
    {
        sw.Stop();
        Debug.Log(name + sw.Elapsed);
    }
}