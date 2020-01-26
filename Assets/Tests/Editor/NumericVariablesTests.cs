using NSubstitute;
using NUnit.Framework;
using ScriptableObjectArchitecture;
using System;
using System.Collections;
using UnityEngine;

namespace Tests
{
    [TestFixture]
    public class NumericVariablesTests
    {
        public static IEnumerable TestCases
        {
            get
            {
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
        public void AddToVariableTest<T, U>(T variable, U value) where T : NumericVariable<U, T>
        {
            Assert.AreEqual(default(U), variable.Value);
            variable.Add(value);
            Assert.AreEqual(value, variable.Value);

            var other = ScriptableObject.CreateInstance<T>();
            Assert.AreEqual(default(U), other.Value);
            other.Add(variable);
            Assert.AreEqual(value, other.Value);

            ScriptableObject.DestroyImmediate(variable);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void SubtractToVariableTest<T, U>(T variable, U value) where T : NumericVariable<U, T>
        {
            variable.Value = value;
            Assert.AreEqual(value, variable.Value);
            variable.Subtract(value);
            Assert.AreEqual(default(U), variable.Value);

            var other = ScriptableObject.CreateInstance<T>();
            other.Value = value;
            variable.Value = value;
            Assert.AreEqual(value, other.Value);
            other.Subtract(variable);
            Assert.AreEqual(default(U), other.Value);

            ScriptableObject.DestroyImmediate(variable);
        }
    }
}