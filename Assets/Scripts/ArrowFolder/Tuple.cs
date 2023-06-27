// --------------------------------------------------------- 
// Generic.cs 
// 
// CreateDay: 
// Creator  : 
// --------------------------------------------------------- 
using System.Collections;
using System;
public class Tuple
{
    object[] tuples;

    public Tuple(params object[] tuples)
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

    public static Tuples Tupless(params object[] a)
    {
        Tuples aa = new Tuples(a);
        return aa;
    }

    public class Tuples : Tuple
    {
        object[] a;
        public Tuples(object[] a)
        {
            this.a = a;
        }

    }
}

public class B
{
    Tuple tuple = new Tuple(1, "DE", 3.2f);
    void A()
    {
        Tuple t = new Tuple(2);
        t = tuple;
        Tuple aaa = Tuple.Tupless(1, 2);
       Console.WriteLine(("string", 4, 6));
    }

    

}


public interface IValueTuple
{
    int FieldCount { get; }
}

public struct ValueTuple : IValueTuple
{
    public int FieldCount
    {
        get
        {
            return 0;
        }
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }

        if (obj is ValueTuple)
        {
            ValueTuple v = ((ValueTuple)(obj));
            return this.FieldCount == v.FieldCount;
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return 0;
    }

    public override string ToString()
    {
        return "{ \"FieldCount\":0 }";
    }

    public static bool operator ==(ValueTuple left, IValueTuple right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ValueTuple left, IValueTuple right)
    {
        return !left.Equals(right);
    }
}

public struct ValueTuple<T1> : IValueTuple
{
    public int FieldCount
    {
        get
        {
            return 1;
        }
    }

    public T1 Item1;

    public ValueTuple(T1 item1)
    {
        this.Item1 = item1;
    }

    public override bool Equals(object obj)
    {
        if (obj is ValueTuple<T1>)
        {
            ValueTuple<T1> v = ((ValueTuple<T1>)(obj));
            if (this.FieldCount == v.FieldCount)
            {
                return this.Item1.Equals(v.Item1);
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return this.Item1.GetHashCode();
    }

    public override string ToString()
    {
        return $"{{ \"FieldCount\":{this.FieldCount}, \"Item1\":\"{this.Item1}\" }}";
    }

    public void Deconstruct(out T1 item1)
    {
        item1 = this.Item1;
    }

    public static bool operator ==(ValueTuple<T1> left, IValueTuple right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ValueTuple<T1> left, IValueTuple right)
    {
        return !left.Equals(right);
    }
}

public struct ValueTuple<T1, T2> : IValueTuple
{
    public int FieldCount
    {
        get
        {
            return 2;
        }
    }

    public T1 Item1;
    public T2 Item2;

    public ValueTuple(T1 item1, T2 item2)
    {
        this.Item1 = item1;
        this.Item2 = item2;
    }

    public override bool Equals(object obj)
    {
        if (obj is ValueTuple<T1, T2>)
        {
            ValueTuple<T1, T2> v = ((ValueTuple<T1, T2>)(obj));
            if (this.FieldCount == v.FieldCount)
            {
                return this.Item1.Equals(v.Item1)
                    && this.Item2.Equals(v.Item2);
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return this.Item1.GetHashCode() ^ this.Item2.GetHashCode();
    }

    public override string ToString()
    {
        return $"{{ \"FieldCount\":{this.FieldCount}, \"Item1\":\"{this.Item1}\", \"Item2\":\"{this.Item2}\" }}";
    }

    public void Deconstruct(out T1 item1, out T2 item2)
    {
        item1 = this.Item1;
        item2 = this.Item2;
    }

    public static bool operator ==(ValueTuple<T1, T2> left, IValueTuple right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ValueTuple<T1, T2> left, IValueTuple right)
    {
        return !left.Equals(right);
    }
}

public struct ValueTuple<T1, T2, T3> : IValueTuple
{
    public int FieldCount
    {
        get
        {
            return 3;
        }
    }

    public T1 Item1;
    public T2 Item2;
    public T3 Item3;

    public ValueTuple(T1 item1, T2 item2, T3 item3)
    {
        this.Item1 = item1;
        this.Item2 = item2;
        this.Item3 = item3;
    }

    public override bool Equals(object obj)
    {
        if (obj is ValueTuple<T1, T2, T3>)
        {
            ValueTuple<T1, T2, T3> v = ((ValueTuple<T1, T2, T3>)(obj));
            if (this.FieldCount == v.FieldCount)
            {
                return this.Item1.Equals(v.Item1)
                    && this.Item2.Equals(v.Item2)
                    && this.Item3.Equals(v.Item3);
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return this.Item1.GetHashCode() ^ this.Item2.GetHashCode() ^ this.Item3.GetHashCode();
    }

    public override string ToString()
    {
        return $"{{ \"FieldCount\":{this.FieldCount}, \"Item1\":\"{this.Item1}\", \"Item2\":\"{this.Item2}\", " +
            $"\"Item3\":\"{this.Item3}\" }}";
    }

    public void Deconstruct(out T1 item1, out T2 item2, out T3 item3)
    {
        item1 = this.Item1;
        item2 = this.Item2;
        item3 = this.Item3;
    }

    public static bool operator ==(ValueTuple<T1, T2, T3> left, IValueTuple right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ValueTuple<T1, T2, T3> left, IValueTuple right)
    {
        return !left.Equals(right);
    }
}

public struct ValueTuple<T1, T2, T3, T4> : IValueTuple
{
    public int FieldCount
    {
        get
        {
            return 4;
        }
    }

    public T1 Item1;
    public T2 Item2;
    public T3 Item3;
    public T4 Item4;

    public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4)
    {
        this.Item1 = item1;
        this.Item2 = item2;
        this.Item3 = item3;
        this.Item4 = item4;
    }

    public override bool Equals(object obj)
    {
        if (obj is ValueTuple<T1, T2, T3, T4>)
        {
            ValueTuple<T1, T2, T3, T4> v = ((ValueTuple<T1, T2, T3, T4>)(obj));
            if (this.FieldCount == v.FieldCount)
            {
                return this.Item1.Equals(v.Item1)
                    && this.Item2.Equals(v.Item2)
                    && this.Item3.Equals(v.Item3)
                    && this.Item4.Equals(v.Item4);
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return this.Item1.GetHashCode() ^ this.Item2.GetHashCode() ^ this.Item3.GetHashCode() ^ this.Item4.GetHashCode();
    }

    public override string ToString()
    {
        return $"{{ \"FieldCount\":{this.FieldCount}, \"Item1\":\"{this.Item1}\", \"Item2\":\"{this.Item2}\", " +
            $"\"Item3\":\"{this.Item3}\", \"Item4\":\"{this.Item4}\" }}";
    }

    public void Deconstruct(out T1 item1, out T2 item2, out T3 item3, out T4 item4)
    {
        item1 = this.Item1;
        item2 = this.Item2;
        item3 = this.Item3;
        item4 = this.Item4;
    }

    public static bool operator ==(ValueTuple<T1, T2, T3, T4> left, IValueTuple right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ValueTuple<T1, T2, T3, T4> left, IValueTuple right)
    {
        return !left.Equals(right);
    }
}

public struct ValueTuple<T1, T2, T3, T4, T5> : IValueTuple
{
    public int FieldCount
    {
        get
        {
            return 5;
        }
    }

    public T1 Item1;
    public T2 Item2;
    public T3 Item3;
    public T4 Item4;
    public T5 Item5;

    public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
    {
        this.Item1 = item1;
        this.Item2 = item2;
        this.Item3 = item3;
        this.Item4 = item4;
        this.Item5 = item5;
    }

    public override bool Equals(object obj)
    {
        if (obj is ValueTuple<T1, T2, T3, T4, T5>)
        {
            ValueTuple<T1, T2, T3, T4, T5> v = ((ValueTuple<T1, T2, T3, T4, T5>)(obj));
            if (this.FieldCount == v.FieldCount)
            {
                return this.Item1.Equals(v.Item1)
                    && this.Item2.Equals(v.Item2)
                    && this.Item3.Equals(v.Item3)
                    && this.Item4.Equals(v.Item4)
                    && this.Item5.Equals(v.Item5);
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return this.Item1.GetHashCode() ^ this.Item2.GetHashCode() ^ this.Item3.GetHashCode() ^ this.Item4.GetHashCode()
            ^ this.Item5.GetHashCode();
    }

    public override string ToString()
    {
        return $"{{ \"FieldCount\":{this.FieldCount}, \"Item1\":\"{this.Item1}\", \"Item2\":\"{this.Item2}\", " +
            $"\"Item3\":\"{this.Item3}\", \"Item4\":\"{this.Item4}\", \"Item5\":\"{this.Item5}\" }}";
    }

    public void Deconstruct(out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5)
    {
        item1 = this.Item1;
        item2 = this.Item2;
        item3 = this.Item3;
        item4 = this.Item4;
        item5 = this.Item5;
    }

    public static bool operator ==(ValueTuple<T1, T2, T3, T4, T5> left, IValueTuple right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ValueTuple<T1, T2, T3, T4, T5> left, IValueTuple right)
    {
        return !left.Equals(right);
    }
}

public struct ValueTuple<T1, T2, T3, T4, T5, T6> : IValueTuple
{
    public int FieldCount
    {
        get
        {
            return 6;
        }
    }

    public T1 Item1;
    public T2 Item2;
    public T3 Item3;
    public T4 Item4;
    public T5 Item5;
    public T6 Item6;

    public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
    {
        this.Item1 = item1;
        this.Item2 = item2;
        this.Item3 = item3;
        this.Item4 = item4;
        this.Item5 = item5;
        this.Item6 = item6;
    }

    public override bool Equals(object obj)
    {
        if (obj is ValueTuple<T1, T2, T3, T4, T5, T6>)
        {
            ValueTuple<T1, T2, T3, T4, T5, T6> v = ((ValueTuple<T1, T2, T3, T4, T5, T6>)(obj));
            if (this.FieldCount == v.FieldCount)
            {
                return this.Item1.Equals(v.Item1)
                    && this.Item2.Equals(v.Item2)
                    && this.Item3.Equals(v.Item3)
                    && this.Item4.Equals(v.Item4)
                    && this.Item5.Equals(v.Item5)
                    && this.Item6.Equals(v.Item6);
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return this.Item1.GetHashCode() ^ this.Item2.GetHashCode() ^ this.Item3.GetHashCode() ^ this.Item4.GetHashCode()
            ^ this.Item5.GetHashCode() ^ this.Item6.GetHashCode();
    }

    public override string ToString()
    {
        return $"{{ \"FieldCount\":{this.FieldCount}, \"Item1\":\"{this.Item1}\", \"Item2\":\"{this.Item2}\", " +
            $"\"Item3\":\"{this.Item3}\", \"Item4\":\"{this.Item4}\", \"Item5\":\"{this.Item5}\", " +
            $"\"Item6\":\"{this.Item6}\" }}";
    }

    public void Deconstruct(out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6)
    {
        item1 = this.Item1;
        item2 = this.Item2;
        item3 = this.Item3;
        item4 = this.Item4;
        item5 = this.Item5;
        item6 = this.Item6;
    }

    public static bool operator ==(ValueTuple<T1, T2, T3, T4, T5, T6> left, IValueTuple right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ValueTuple<T1, T2, T3, T4, T5, T6> left, IValueTuple right)
    {
        return !left.Equals(right);
    }
}

public struct ValueTuple<T1, T2, T3, T4, T5, T6, T7> : IValueTuple
{
    public int FieldCount
    {
        get
        {
            return 7;
        }
    }

    public T1 Item1;
    public T2 Item2;
    public T3 Item3;
    public T4 Item4;
    public T5 Item5;
    public T6 Item6;
    public T7 Item7;

    public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
    {
        this.Item1 = item1;
        this.Item2 = item2;
        this.Item3 = item3;
        this.Item4 = item4;
        this.Item5 = item5;
        this.Item6 = item6;
        this.Item7 = item7;
    }

    public override bool Equals(object obj)
    {
        if (obj is ValueTuple<T1, T2, T3, T4, T5, T6, T7>)
        {
            ValueTuple<T1, T2, T3, T4, T5, T6, T7> v = ((ValueTuple<T1, T2, T3, T4, T5, T6, T7>)(obj));
            if (this.FieldCount == v.FieldCount)
            {
                return this.Item1.Equals(v.Item1)
                    && this.Item2.Equals(v.Item2)
                    && this.Item3.Equals(v.Item3)
                    && this.Item4.Equals(v.Item4)
                    && this.Item5.Equals(v.Item5)
                    && this.Item6.Equals(v.Item6)
                    && this.Item7.Equals(v.Item7);
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return this.Item1.GetHashCode() ^ this.Item2.GetHashCode() ^ this.Item3.GetHashCode() ^ this.Item4.GetHashCode()
            ^ this.Item5.GetHashCode() ^ this.Item6.GetHashCode() ^ this.Item7.GetHashCode();
    }

    public override string ToString()
    {
        return $"{{ \"FieldCount\":{this.FieldCount}, \"Item1\":\"{this.Item1}\", \"Item2\":\"{this.Item2}\", " +
            $"\"Item3\":\"{this.Item3}\", \"Item4\":\"{this.Item4}\", \"Item5\":\"{this.Item5}\", " +
            $"\"Item6\":\"{this.Item6}\", \"Item7\":\"{this.Item7}\" }}";
    }

    public void Deconstruct(out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7)
    {
        item1 = this.Item1;
        item2 = this.Item2;
        item3 = this.Item3;
        item4 = this.Item4;
        item5 = this.Item5;
        item6 = this.Item6;
        item7 = this.Item7;
    }

    public static bool operator ==(ValueTuple<T1, T2, T3, T4, T5, T6, T7> left, IValueTuple right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ValueTuple<T1, T2, T3, T4, T5, T6, T7> left, IValueTuple right)
    {
        return !left.Equals(right);
    }


}



public struct ValueTuple<T1, T2, T3, T4, T5, T6, T7, Tn> : IValueTuple where Tn : IValueTuple, new()
{
    public int FieldCount
    {
        get
        {
            return 7 + ItemN.FieldCount;
        }
    }

    public T1 Item1;
    public T2 Item2;
    public T3 Item3;
    public T4 Item4;
    public T5 Item5;
    public T6 Item6;
    public T7 Item7;
    public Tn ItemN;

    public Tn Rest
    {
        get
        {
            return this.ItemN;
        }

        set
        {
            this.ItemN = value;
        }
    }

    public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, Tn itemN)
    {
        this.Item1 = item1;
        this.Item2 = item2;
        this.Item3 = item3;
        this.Item4 = item4;
        this.Item5 = item5;
        this.Item6 = item6;
        this.Item7 = item7;
        this.ItemN = itemN;
    }

    public override bool Equals(object obj)
    {
        if (obj is ValueTuple<T1, T2, T3, T4, T5, T6, T7, Tn>)
        {
            ValueTuple<T1, T2, T3, T4, T5, T6, T7, Tn> v = ((ValueTuple<T1, T2, T3, T4, T5, T6, T7, Tn>)(obj));
            if (this.FieldCount == v.FieldCount)
            {
                return this.Item1.Equals(v.Item1)
                    && this.Item2.Equals(v.Item2)
                    && this.Item3.Equals(v.Item3)
                    && this.Item4.Equals(v.Item4)
                    && this.Item5.Equals(v.Item5)
                    && this.Item6.Equals(v.Item6)
                    && this.Item7.Equals(v.Item7)
                    && this.ItemN.Equals(v.ItemN);
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return this.Item1.GetHashCode() ^ this.Item2.GetHashCode() ^ this.Item3.GetHashCode() ^ this.Item4.GetHashCode()
            ^ this.Item5.GetHashCode() ^ this.Item6.GetHashCode() ^ this.Item7.GetHashCode() ^ this.ItemN.GetHashCode();
    }

    public override string ToString()
    {
        return $"{{ \"FieldCount\":{this.FieldCount}, \"Item1\":\"{this.Item1}\", \"Item2\":\"{this.Item2}\", " +
            $"\"Item3\":\"{this.Item3}\", \"Item4\":\"{this.Item4}\", \"Item5\":\"{this.Item5}\", " +
            $"\"Item6\":\"{this.Item6}\", \"Item7\":\"{this.Item7}\", \"ItemN\":{this.ItemN} }}";
    }

    public void Deconstruct(
        out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out Tn itemN)
    {
        item1 = this.Item1;
        item2 = this.Item2;
        item3 = this.Item3;
        item4 = this.Item4;
        item5 = this.Item5;
        item6 = this.Item6;
        item7 = this.Item7;
        itemN = this.ItemN;
    }

    public static bool operator ==(ValueTuple<T1, T2, T3, T4, T5, T6, T7, Tn> left, IValueTuple right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ValueTuple<T1, T2, T3, T4, T5, T6, T7, Tn> left, IValueTuple right)
    {
        return !left.Equals(right);
    }
}
