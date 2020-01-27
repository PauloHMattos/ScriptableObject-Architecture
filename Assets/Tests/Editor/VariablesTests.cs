using NSubstitute;
using NUnit.Framework;
using ScriptableObjectArchitecture;
using System;
using System.Collections;
using UnityEngine;

namespace Tests
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
        public void ChangeVariableValueTest<T, U>(T variable, U value) where T : BaseVariable<U>
        {
            Assert.AreEqual(default(U), variable.Value);
            variable.Value = value;
            Assert.AreEqual(value, variable.Value);
            Assert.AreEqual(value, (U)variable);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void ChangeReadOnlyVariableValueTest<T, U>(T variable, U value) where T : BaseVariable<U>
        {
            Assert.AreEqual(default(U), variable.Value);
            variable.ReadOnly = true;

            Debug.unityLogger.logEnabled = false;
            variable.Value = value;
            Debug.unityLogger.logEnabled = true;

            Assert.AreEqual(default(U), variable.Value);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void ChangeClampableVariableValueTest<T, U>(T variable, U value) where T : BaseVariable<U>
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
            Assert.AreEqual(default(U), variable.Value);

            variable.MinClampValue = value;
            variable.MaxClampValue = value;
            Assert.AreEqual(value, variable.Value);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void RaiseVariableChangedEventTest<T, U>(T variable, U value) where T : BaseVariable<U>
        {
            var mockAction = Substitute.For<Action>();
            var mockObserver = Substitute.For<IVariableObserver>();

            variable.AddObserver(mockAction);
            variable.AddObserver(mockObserver);

            variable.Value = value;
            mockAction.Received(1).Invoke();
            mockObserver.Received(1).OnVariableChanged();

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