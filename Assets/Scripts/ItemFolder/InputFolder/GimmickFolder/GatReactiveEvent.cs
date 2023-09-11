// --------------------------------------------------------- 
// GatRactiveEvent.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using UnityEngine;
using System.Collections;
interface IFGetReactiveEvent
{
    void CallEvent();
}
public class GatReactiveEvent : MonoBehaviour
{
    IFGetReactiveEvent[] events = default;
    private void Awake()
    {
        events = GetComponents<IFGetReactiveEvent>();
        
        TestTutorialManager tutorial = FindObjectOfType<TestTutorialManager>();
        if(tutorial is not null)
        {
            tutorial.readOnlyTutorial.Subject.Subscribe(
             tuto =>
             {
                 if (tuto == TutorialType.crystalBreak)
                 {
                     GetCall();
                 }
             }
            );
        }

        
    }

    private void GetCall()
    {
        foreach (IFGetReactiveEvent ev in events)
        {
            ev.CallEvent();
        }
    }
}