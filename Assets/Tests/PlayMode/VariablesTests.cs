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
    public class VariablesTests
    {
        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(ScriptableObject.CreateInstance<StringVariable>(), "string");
                yield return new TestCaseData(ScriptableObject.CreateInstance<BoolVariable>(), true);
                yield return new TestCaseData(ScriptableObject.CreateInstance<CharVariable>(), 'g');
                yield return new TestCaseData(ScriptableObject.CreateInstance<ShortVariable>(), (short)100);
                yield return new TestCaseData(ScriptableObject.CreateInstance<IntVariable>(), 100);
                yield return new TestCaseData(ScriptableObject.CreateInstance<LongVariable>(), (long)100);
                yield return new TestCaseData(ScriptableObject.CreateInstance<ByteVariable>(), (byte)255);
                yield return new TestCaseData(ScriptableObject.CreateInstance<SByteVariable>(), (sbyte)127);
                yield return new TestCaseData(ScriptableObject.CreateInstance<UShortVariable>(), (ushort)100);
                yield return new TestCaseData(ScriptableObject.CreateInstance<UIntVariable>(), (uint)100);
                yield return new TestCaseData(ScriptableObject.CreateInstance<ULongVariable>(), (ulong)100);
                yield return new TestCaseData(ScriptableObject.CreateInstance<FloatVariable>(), 100.0f);
                yield return new TestCaseData(ScriptableObject.CreateInstance<DoubleVariable>(), 100.0);
                yield return new TestCaseData(ScriptableObject.CreateInstance<Vector2Variable>(), Vector2.down);
                yield return new TestCaseData(ScriptableObject.CreateInstance<Vector2IntVariable>(), Vector2Int.down);
                yield return new TestCaseData(ScriptableObject.CreateInstance<Vector3Variable>(), Vector3.down);
                yield return new TestCaseData(ScriptableObject.CreateInstance<Vector4Variable>(), Vector4.one);
            }
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void VariableNullCheck<TVariable, TValue>(TVariable variable, TValue value) where TVariable : BaseVariable<TValue>
        {
            Assert.IsNotNull(variable);

            Object.DestroyImmediate(variable);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void VariableTypeTest<TVariable, TValue>(TVariable variable, TValue value) where TVariable : BaseVariable<TValue>
        {
            Assert.AreEqual(typeof(TValue), variable.Type);

            Object.DestroyImmediate(variable);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void AddIVariableObserverTest<TVariable, TValue>(TVariable variable, TValue value) where TVariable : BaseVariable<TValue>
        {
            var mockObserver = Substitute.For<IVariableObserver>();
            variable.AddObserver(mockObserver);
            variable.AddObserver(mockObserver);
            Assert.AreNotEqual(2, variable.Actions.Count, "Should not allow for the repetition of Observers");
            Assert.AreEqual(1, variable.Observers.Count, "Event listener was not registered");

            Object.DestroyImmediate(variable);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void AddActionObserverTest<TVariable, TValue>(TVariable variable, TValue value) where TVariable : BaseVariable<TValue>
        {
            var mockAction = Substitute.For<Action>();
            variable.AddObserver(mockAction);
            variable.AddObserver(mockAction);
            Assert.AreNotEqual(2, variable.Actions.Count, "Should not allow for the repetition of Observers");
            Assert.AreEqual(1, variable.Actions.Count, "Event listener was not registered");

            Object.DestroyImmediate(variable);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void ChangeVariableValueTest<TVariable, TValue>(TVariable variable, TValue value) where TVariable : BaseVariable<TValue>
        {
            Assert.AreEqual(default(TValue), variable.Value);
            variable.Value = value;
            Assert.AreEqual(value, variable.Value);
            Assert.AreEqual(value, (TValue)variable);

            Object.DestroyImmediate(variable);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void ChangeReadOnlyVariableValueTest<TVariable, TValue>(TVariable variable, TValue value) where TVariable : BaseVariable<TValue>
        {
            Assert.AreEqual(default(TValue), variable.Value);
            variable.ReadOnly = true;
            variable.name = "ChangeReadOnlyVariableValueTest";
            Debug.unityLogger.logEnabled = false;
            variable.Value = value;
            Debug.unityLogger.logEnabled = true;

            Assert.AreEqual(default(TValue), variable.Value);

            Object.DestroyImmediate(variable);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void ChangeClampableVariableValueTest<TVariable, TValue>(TVariable variable, TValue value) where TVariable : BaseVariable<TValue>
        {
            if (!variable.Clampable)
            {
                Assert.Pass();
                return;
            }

            variable.Value = value;
            Assert.AreEqual(value, variable.Value);

            variable.IsClamped = true;
            variable.MinClampValue = default;
            variable.MaxClampValue = default;
            Assert.AreEqual(default(TValue), variable.Value);

            variable.MinClampValue = value;
            variable.MaxClampValue = value;
            Assert.AreEqual(value, variable.Value);

            Object.DestroyImmediate(variable);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void RaiseVariableChangedEventTest<TVariable, TValue>(TVariable variable, TValue value) where TVariable : BaseVariable<TValue>
        {
            var mockAction = Substitute.For<Action>();
            var mockObserver = Substitute.For<IVariableObserver>();

            variable.AddObserver(mockAction);
            variable.AddObserver(mockObserver);

            variable.Value = value;
            mockAction.Received(1).Invoke();
            mockObserver.Received(1).OnVariableChanged();

            Object.DestroyImmediate(variable);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void RemoveIVariableObserverTest<TVariable, TValue>(TVariable variable, TValue value) where TVariable : BaseVariable<TValue>
        {
            var mockObserver = Substitute.For<IVariableObserver>();
            variable.AddObserver(mockObserver);
            variable.RemoveObserver(mockObserver);
            Assert.Zero(variable.Observers.Count);

            Object.DestroyImmediate(variable);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void RemoveActionObserverTest<TVariable, TValue>(TVariable variable, TValue value) where TVariable : BaseVariable<TValue>
        {
            var mockAction = Substitute.For<Action>();
            variable.AddObserver(mockAction);
            variable.RemoveObserver(mockAction);
            Assert.Zero(variable.Actions.Count);

            Object.DestroyImmediate(variable);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void RemoveAllObserversTest<TVariable, TValue>(TVariable variable, TValue value) where TVariable : BaseVariable<TValue>
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