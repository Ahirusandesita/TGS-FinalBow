// --------------------------------------------------------- 
// ArrowPassiveEffect.cs 
// 
// CreateDay: 2023/06/08
// Creator  : Nomura
// --------------------------------------------------------- 
interface IStats<T>
{
    T GetStatus();
}
public struct Stats<T> :IStats<T>
{
    private T _t;
    public Stats(T t)
    {
        this._t = t;
    }
    public T GetStatus()
    {
        return _t;
    }
    public void AA()
    {

    }
}
