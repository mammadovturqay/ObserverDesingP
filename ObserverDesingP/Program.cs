using System;
using System.Collections.Generic;

public interface IObserver
{
    void Update();
}

public interface ISubject
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void NotifyObservers();
}

public class ConcreteSubject : ISubject
{
    private List<IObserver> observers = new List<IObserver>();
    private string state;

    public string State
    {
        get => state;
        set
        {
            state = value;
            NotifyObservers();
        }
    }

    public void Attach(IObserver observer)
    {
        observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.Update();
        }
    }
}

public class ConcreteObserver : IObserver
{
    private readonly ConcreteSubject subject;
    private string observerState;

    public ConcreteObserver(ConcreteSubject subject)
    {
        this.subject = subject;
        this.subject.Attach(this);
    }

    public void Update()
    {
        observerState = subject.State;
        Console.WriteLine($"Observer state updated: {observerState}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        ConcreteSubject subject = new ConcreteSubject();
        ConcreteObserver observer1 = new ConcreteObserver(subject);
        ConcreteObserver observer2 = new ConcreteObserver(subject);

        subject.State = "New State"; 
    }
}
