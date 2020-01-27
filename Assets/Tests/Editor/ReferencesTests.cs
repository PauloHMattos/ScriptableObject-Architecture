using System.Collections;
using NUnit.Framework;
using ScriptableObjectArchitecture.References;
using ScriptableObjectArchitecture.Variables;
using UnityEngine;

namespace Assets.Tests.Editor
{
    [TestFixture]
    public class ReferencesTests
    {
        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(new StringReference("a"), ScriptableObject.CreateInstance<StringVariable>(), "string");
                yield return new TestCaseData(new BoolReference(), ScriptableObject.CreateInstance<BoolVariable>(), true);
                yield return new TestCaseData(new CharReference(), ScriptableObject.CreateInstance<CharVariable>(), 'g');
                yield return new TestCaseData(new ShortReference(), ScriptableObject.CreateInstance<ShortVariable>(), (short)100);
                yield return new TestCaseData(new IntReference(500), ScriptableObject.CreateInstance<IntVariable>(), 100);
                yield return new TestCaseData(new LongReference(), ScriptableObject.CreateInstance<LongVariable>(), (long)100);
                yield return new TestCaseData(new ByteReference(), ScriptableObject.CreateInstance<ByteVariable>(), (byte)255);
                yield return new TestCaseData(new SByteReference(), ScriptableObject.CreateInstance<SByteVariable>(), (sbyte)127);
                yield return new TestCaseData(new UShortReference(), ScriptableObject.CreateInstance<UShortVariable>(), (ushort)100);
                yield return new TestCaseData(new UIntReference(), ScriptableObject.CreateInstance<UIntVariable>(), (uint)100);
                yield return new TestCaseData(new ULongReference(), ScriptableObject.CreateInstance<ULongVariable>(), (ulong)100);
                yield return new TestCaseData(new FloatReference(), ScriptableObject.CreateInstance<FloatVariable>(), 100.0f);
                yield return new TestCaseData(new DoubleReference(), ScriptableObject.CreateInstance<DoubleVariable>(), 100.0);
                yield return new TestCaseData(new Vector2Reference(), ScriptableObject.CreateInstance<Vector2Variable>(), Vector2.down);
                yield return new TestCaseData(new Vector2IntReference(), ScriptableObject.CreateInstance<Vector2IntVariable>(), Vector2Int.down);
                yield return new TestCaseData(new Vector3Reference(), ScriptableObject.CreateInstance<Vector3Variable>(), Vector3.down);
                yield return new TestCaseData(new Vector4Reference(), ScriptableObject.CreateInstance<Vector4Variable>(), Vector4.one);
            }
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void CreateCopyTest<T, TU, TV>(T reference, TU variable, TV value) where TU : BaseVariable<TV> where T : BaseReference<TV, TU>
        {
            var copy = reference.CreateCopy();

            ScriptableObject.DestroyImmediate(variable);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void IsValueDefinedTest<T, TU, TV>(T reference, TU variable, TV value) where TU : BaseVariable<TV> where T : BaseReference<TV, TU>
        {
            reference.UseConstant = false;
            Assert.IsFalse(reference.IsValueDefined);

            reference.UseConstant = true;
            Assert.IsTrue(reference.IsValueDefined);

            reference.Variable = variable;
            Assert.IsTrue(reference.IsValueDefined);

            ScriptableObject.DestroyImmediate(variable);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void ToStringTest<T, TU, TV>(T reference, TU variable, TV value) where TU : BaseVariable<TV> where T : BaseReference<TV, TU>
        {
            var defValue = reference.Value;

            Assert.AreEqual(defValue.ToString(), reference.ToString());

            variable.Value = value;
            reference.Variable = variable;

            Assert.AreEqual(variable.ToString(), reference.ToString());

            ScriptableObject.DestroyImmediate(variable);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void ChangeReferenceValueTest<T, TU, TV>(T reference, TU variable, TV value) where TU : BaseVariable<TV> where T : BaseReference<TV, TU>
        {
            var defValue = reference.Value;

            reference.Value = value;
            Assert.AreEqual(value, reference.Value);

            reference.Value = defValue;
            reference.Variable = variable;
            Assert.AreEqual(variable, reference.Variable);

            variable.Value = value;

            Assert.AreEqual(value, reference.Value);

            reference.Value = defValue;
            Assert.AreEqual(defValue, variable.Value);

            ScriptableObject.DestroyImmediate(variable);
        }
        /*
        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void AddActionObserverTest<T, U>(T reference, U value) where T : BaseReference<U, BaseVariable<U>>
        {
            var mockAction = Substitute.For<Action>();
            reference.AddObserver(mockAction);
            reference.AddObserver(mockAction);
            Assert.AreNotEqual(2, reference.Actions.Count, "Should not allow for the repetition of Observers");
            Assert.AreEqual(1, reference.Actions.Count, "Event listener was not registred");
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void ChangeVariableValueTest<T, U>(T reference, U value) where T : BaseReference<U, BaseVariable<U>>
        {
            Assert.AreEqual(default(U), reference.Value);
            reference.Value = value;
            Assert.AreEqual(value, reference.Value);
            Assert.AreEqual(value, (U)reference);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void ChangeReadOnlyVariableValueTest<T, U>(T reference, U value) where T : BaseReference<U, BaseVariable<U>>
        {
            Assert.AreEqual(default(U), reference.Value);
            reference.ReadOnly = true;

            Debug.unityLogger.logEnabled = false;
            reference.Value = value;
            Debug.unityLogger.logEnabled = true;

            Assert.AreEqual(default(U), reference.Value);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void ChangeClampableVariableValueTest<T, U>(T reference, U value) where T : BaseReference<U, BaseVariable<U>>
        {
            if (!reference.Clampable)
            {
                Assert.Pass();
                return;
            }

            reference.Value = value;
            Assert.AreEqual(value, reference.Value);

            reference.IsClamped = true;
            reference.MinClampValue = default;
            reference.MaxClampValue = default;
            Assert.AreEqual(default(U), reference.Value);

            reference.MinClampValue = value;
            reference.MaxClampValue = value;
            Assert.AreEqual(value, reference.Value);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void RaiseVariableChangedEventTest<T, U>(T reference, U value) where T : BaseReference<U, BaseVariable<U>>
        {
            var mockAction = Substitute.For<Action>();
            var mockObserver = Substitute.For<IVariableObserver>();

            reference.AddObserver(mockAction);
            reference.AddObserver(mockObserver);

            reference.Value = value;
            mockAction.Received(1).Invoke();
            mockObserver.Received(1).OnVariableChanged();

            ScriptableObject.DestroyImmediateImmediate(reference);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void RemoveIVariableObserverTest<T, U>(T reference, U value) where T : BaseReference<U, BaseVariable<U>>
        {
            var mockObserver = Substitute.For<IVariableObserver>();
            reference.AddObserver(mockObserver);
            reference.RemoveObserver(mockObserver);
            Assert.Zero(reference.Observers.Count);

            ScriptableObject.DestroyImmediateImmediate(reference);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void RemoveActionObserverTest<T, U>(T reference, U value) where T : BaseReference<U, BaseVariable<U>>
        {
            var mockAction = Substitute.For<Action>();
            reference.AddObserver(mockAction);
            reference.RemoveObserver(mockAction);
            Assert.Zero(reference.Actions.Count);

            ScriptableObject.DestroyImmediateImmediate(reference);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void RemoveAllObserversTest<T, U>(T reference, U value) where T : BaseReference<U, BaseVariable<U>>
        {
            var mockAction = Substitute.For<Action>();
            var mockObserver = Substitute.For<IVariableObserver>();
            reference.AddObserver(mockAction);
            reference.AddObserver(mockObserver);
            reference.RemoveAllObservers();
            Assert.Zero(reference.Observers.Count);
            Assert.Zero(reference.Actions.Count);

            ScriptableObject.DestroyImmediateImmediate(reference);
        }
        */
    }
}