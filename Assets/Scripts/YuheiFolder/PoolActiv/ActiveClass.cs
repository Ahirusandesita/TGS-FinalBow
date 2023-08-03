// --------------------------------------------------------- 
// ActiveClass.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using System.Collections.Generic;
using UnityEngine;
interface IFActiveClass
{
    void SetClass<T>(T t) where T : Behaviour;
}

public class ActiveClass : IFActiveClass
{

    public struct ActiveData
    {
        public Behaviour ActiveCompoent;
        public GameObject ActiveGameObject;

        public ActiveData(Behaviour behaviour,GameObject activeObject)
        {
            ActiveCompoent = behaviour;
            ActiveGameObject = activeObject;
        }

    }

    public List<ActiveData> activeDatas = new List<ActiveData>();

    public ActiveClass() { }
    public List<ActiveData> GetOnlyActiveConponents
    {
        get
        {
            return activeDatas;
        }
    }


    public void SetClass<T>(T t) where T :  Behaviour
    {
        activeDatas.Add(new ActiveData(t,t.gameObject));
    }

}
