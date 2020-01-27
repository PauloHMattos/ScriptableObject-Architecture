using System;
using System.Collections.Generic;
using ScriptableObjectArchitecture.Observers;

namespace ScriptableObjectArchitecture.Variables
{
    public interface ISubject
    {
        List<IVariableObserver> Observers { get; }
        List<Action> Actions { get; }

        void Raise();
        void AddObserver(IVariableObserver observer);
        void RemoveObserver(IVariableObserver observer);
        void AddObserver(Action action);
        void RemoveObserver(Action action);
        void RemoveAllObservers();
    }
}