// --------------------------------------------------------- 
// Generic.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using System.Collections;
using System;
public class MyTuple
{
    object[] tuples;

    public MyTuple(params object[] tuples)
    {
        this.tuples = tuples;
    }

    public object GetTupleElement(int index)
    {
        if(index > tuples.Length - 1)
        {
            return null;
        }

        return tuples[index];
    }
}

