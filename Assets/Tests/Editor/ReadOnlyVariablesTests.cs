using System;
using System.Collections;
using NSubstitute;
using NUnit.Framework;
using ScriptableObjectArchitecture.Observers;
using ScriptableObjectArchitecture.Variables;
using UnityEngine;

namespace Assets.Tests.Editor
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
        public void VariableTypeTest<T, U>(T variable, U value) where T : BaseVariable<U>
        {
            Assert.AreEqual(typeof(U), variable.Type);

            ScriptableObject.DestroyImmediate(variable);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void AddIVariableObserverTest<T, U>(T variable, U value) where T : BaseVariable<U>
        {
            var mockObserver = Substitute.For<IVariableObserver>();
            variable.AddObserver(mockObserver);
            variable.AddObserver(mockObserver);
            Assert.AreNotEqual(2, variable.Actions.Count, "Should not allow for the repetition of Observers");
            Assert.AreEqual(1, variable.Observers.Count, "Event listener was not registred");

            ScriptableObject.DestroyImmediate(variable);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void AddActionObserverTest<T, U>(T variable, U value) where T : BaseVariable<U>
        {
            var mockAction = Substitute.For<Action>();
            variable.AddObserver(mockAction);
            variable.AddObserver(mockAction);
            Assert.AreNotEqual(2, variable.Actions.Count, "Should not allow for the repetition of Observers");
            Assert.AreEqual(1, variable.Actions.Count, "Event listener was not registred");

            ScriptableObject.DestroyImmediate(variable);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void ChangeReadOnlyVariableValueTest<T, U>(T variable, U value) where T : BaseVariable<U>
        {
            //Assert.AreEqual(default(U), variable.Value);
            Debug.unityLogger.logEnabled = false;
            variable.Value = value;
            Debug.unityLogger.logEnabled = true;
            Assert.AreNotEqual(value, variable.Value);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void RaiseVariableChangedEventTest<T, U>(T variable, U value) where T : BaseVariable<U>
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

            ScriptableObject.DestroyImmediate(variable);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void RemoveIVariableObserverTest<T, U>(T variable, U value) where T : BaseVariable<U>
        {
            var mockObserver = Substitute.For<IVariableObserver>();
            variable.AddObserver(mockObserver);
            variable.RemoveObserver(mockObserver);
            Assert.Zero(variable.Observers.Count);

            ScriptableObject.DestroyImmediate(variable);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void RemoveActionObserverTest<T, U>(T variable, U value) where T : BaseVariable<U>
        {
            var mockAction = Substitute.For<Action>();
            variable.AddObserver(mockAction);
            variable.RemoveObserver(mockAction);
            Assert.Zero(variable.Actions.Count);

            ScriptableObject.DestroyImmediate(variable);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void RemoveAllObserversTest<T, U>(T variable, U value) where T : BaseVariable<U>
        {
            var mockAction = Substitute.For<Action>();
            var mockObserver = Substitute.For<IVariableObserver>();
            variable.AddObserver(mockAction);
            variable.AddObserver(mockObserver);
            variable.RemoveAllObservers();
            Assert.Zero(variable.Observers.Count);
            Assert.Zero(variable.Actions.Count);

            ScriptableObject.DestroyImmediate(variable);
        }
    }
}