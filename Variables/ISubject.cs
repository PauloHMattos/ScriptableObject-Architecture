using System.Collections.Generic;

namespace ScriptableObjectArchitecture
{
    public interface ISubject
    {
        List<IVariableObserver> Observers { get; }

        void Raise();
        void AddObserver(IVariableObserver observer);
        void RemoveObserver(IVariableObserver observer);
    }
}