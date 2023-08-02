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
    List<Behaviour> ActiveComponents = new List<Behaviour>();

    public List<Behaviour> GetOnlyActiveConponents
    {
        get
        {
            return ActiveComponents;
        }
    }


    public ActiveClass() { }

    public void SetClass<T>(T t) where T : Behaviour
    {
        ActiveComponents.Add(t);
    }

}