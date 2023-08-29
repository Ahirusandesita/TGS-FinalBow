using System;
using System.Collections.Generic;


public interface IReActiveProperty<T>
{
    public Subject<T> Subject { get; }
}
public interface ISubject<T>
{
    void Subscribe(IObserver observer);
    void Subscribe(Action<T> action);
    void Dettach(IObserver observer);
    void Notify(ReActiveProperty<T> reActiveProperty);
}
public interface IObserver
{
    void Updating<T>(T reActivePropertyValue);
}

public class Subject<T> : ISubject<T>
{
    private List<IObserver> observers = new List<IObserver>();
    private List<Action<T>> actionObservers = new List<Action<T>>();
    private List<Action<T>> firstActionObserves = new List<Action<T>>();


    public void Subscribe(IObserver observer) => observers.Add(observer);
    public void Subscribe(Action<T> action) => actionObservers.Add(action);

    public void FirstSubscribe(Action<T> action) => firstActionObserves.Add(action);

    public void Dettach(IObserver observer) => observers.Remove(observer);
    public void Notify(ReActiveProperty<T> reActiveProperty)
    {
        for (int i = 0; i < observers.Count; i++)
        {
            observers[i].Updating<T>(reActiveProperty.Value);
        }
        for(int i = 0; i < actionObservers.Count; i++)
        {
            actionObservers[i](reActiveProperty.Value);
        }
    }

    public void FirstNotify(ReActiveProperty<T> reActiveProperty)
    {
        for(int i = 0; i < firstActionObserves.Count; i++)
        {
            firstActionObserves[i](reActiveProperty.Value);
        }
    }

}



public class ReActiveProperty<T> : IReActiveProperty<T>
{
    private T value;
    private int setCount = 0;
    public T Value
    {
        get => this.value;
        set
        {
            this.value = value;
            setCount++;
            if(setCount == 1)
            {
                subject.FirstNotify(this);
                return;
            }
            subject.Notify(this);
        }
    }
    public Subject<T> Subject => subject;
    private Subject<T> subject = new Subject<T>();
}