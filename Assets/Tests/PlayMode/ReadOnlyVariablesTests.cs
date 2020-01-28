using System;
using System.Collections;
using NSubstitute;
using NUnit.Framework;
using ScriptableObjectArchitecture.Observers;
using ScriptableObjectArchitecture.Variables;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Tests.PlayMode
{
    [TestFixture]
    public class ReadOnlyVariablesTests
    {
        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(ScriptableObject.CreateInstance<TimeVariable>(), 100.0f);
                yield return new TestCaseData(ScriptableObject.CreateInstance<RandomFloatVariable>(), 100.0f);
                yield return new TestCaseData(ScriptableObject.CreateInstance<AxisVariable>(), 100.0f);
                yield return new TestCaseData(ScriptableObject.CreateInstance<Axis2DVariable>(), Vector2.down);
            }
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void VariableTypeTest<T, TU>(T variable, TU value) where T : BaseVariable<TU>
        {
            Assert.AreEqual(typeof(TU), variable.Type);

            Object.DestroyImmediate(variable);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void AddIVariableObserverTest<T, TU>(T variable, TU value) where T : BaseVariable<TU>
        {
            var mockObserver = Substitute.For<IVariableObserver>();
            variable.AddObserver(mockObserver);
            variable.AddObserver(mockObserver);
            Assert.AreNotEqual(2, variable.Actions.Count, "Should not allow for the repetition of Observers");
            Assert.AreEqual(1, variable.Observers.Count, "Event listener was not registred");

            Object.DestroyImmediate(variable);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void AddActionObserverTest<T, TU>(T variable, TU value) where T : BaseVariable<TU>
        {
            var mockAction = Substitute.For<Action>();
            variable.AddObserver(mockAction);
            variable.AddObserver(mockAction);
            Assert.AreNotEqual(2, variable.Actions.Count, "Should not allow for the repetition of Observers");
            Assert.AreEqual(1, variable.Actions.Count, "Event listener was not registred");

            Object.DestroyImmediate(variable);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void ChangeReadOnlyVariableValueTest<T, TU>(T variable, TU value) where T : BaseVariable<TU>
        {
            //Assert.AreEqual(default(U), variable.Value);
            Debug.unityLogger.logEnabled = false;
            variable.Value = value;
            Debug.unityLogger.logEnabled = true;
            Assert.AreNotEqual(value, variable.Value);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void RaiseVariableChangedEventTest<T, TU>(T variable, TU value) where T : BaseVariable<TU>
        {
            var mockAction = Substitute.For<Action>();
            var mockObserver = Substitute.For<IVariableObserver>();

            variable.AddObserver(mockAction);
            variable.AddObserver(mockObserver);

            Debug.unityLogger.logEnabled = false;
            variable.Value = value;
            Debug.unityLogger.logEnabled = true;
            mockAction.DidNotReceive().Invoke();
            mockObserver.DidNotReceive().OnVariableChanged();

            Object.DestroyImmediate(variable);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void RemoveIVariableObserverTest<T, TU>(T variable, TU value) where T : BaseVariable<TU>
        {
            var mockObserver = Substitute.For<IVariableObserver>();
            variable.AddObserver(mockObserver);
            variable.RemoveObserver(mockObserver);
            Assert.Zero(variable.Observers.Count);

            Object.DestroyImmediate(variable);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void RemoveActionObserverTest<T, TU>(T variable, TU value) where T : BaseVariable<TU>
        {
            var mockAction = Substitute.For<Action>();
            variable.AddObserver(mockAction);
            variable.RemoveObserver(mockAction);
            Assert.Zero(variable.Actions.Count);

            Object.DestroyImmediate(variable);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void RemoveAllObserversTest<T, TU>(T variable, TU value) where T : BaseVariable<TU>
        {
            var mockAction = Substitute.For<Action>();
            var mockObserver = Substitute.For<IVariableObserver>();
            variable.AddObserver(mockAction);
            variable.AddObserver(mockObserver);
            variable.RemoveAllObservers();
            Assert.Zero(variable.Observers.Count);
            Assert.Zero(variable.Actions.Count);

            Object.DestroyImmediate(variable);
        }
    }
}