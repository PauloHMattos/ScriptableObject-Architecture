using System;
using System.Collections;
using NSubstitute;
using NUnit.Framework;
using ScriptableObjectArchitecture.Events.GameEvents;
using ScriptableObjectArchitecture.Events.Listeners;
using UnityEngine;

namespace Assets.Tests.Editor
{

    [TestFixture]
    public class TypedGameEventsTests
    {
        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(ScriptableObject.CreateInstance<StringGameEvent>(), "string");
                yield return new TestCaseData(ScriptableObject.CreateInstance<IntGameEvent>(), 100);
                yield return new TestCaseData(ScriptableObject.CreateInstance<ShortGameEvent>(), (short)100);
                yield return new TestCaseData(ScriptableObject.CreateInstance<LongGameEvent>(), (long)100);
                yield return new TestCaseData(ScriptableObject.CreateInstance<FloatGameEvent>(), 100.0f);
                yield return new TestCaseData(ScriptableObject.CreateInstance<DoubleGameEvent>(), 100.0);
                yield return new TestCaseData(ScriptableObject.CreateInstance<Vector2GameEvent>(), Vector2.down);
            }
        }


        [Test]
        [TestCaseSource(typeof(TypedGameEventsTests), nameof(TypedGameEventsTests.TestCases))]
        public void AddIGameEventListenerTest<T, TU>(T gameEvent, TU value) where T : GameEventBase<TU>
        {
            var mockListener = Substitute.For<IGameEventListener>();
            var mockTypedListener = Substitute.For<IGameEventListener<TU>>();
            gameEvent.AddListener(mockListener);
            gameEvent.AddListener(mockListener);
            gameEvent.AddListener(mockTypedListener);
            gameEvent.AddListener(mockTypedListener);
            Assert.AreNotEqual(4, gameEvent.GetActionsCount(), "Should not allow for the repetition of Listeners");
            Assert.AreEqual(2, gameEvent.GetListenersCount(), "Event listener was not registred");
            Assert.AreEqual(1, gameEvent.TypedListeners.Count, "Event listener was not registred");

            ScriptableObject.DestroyImmediate(gameEvent);
        }

        [Test]
        [TestCaseSource(typeof(TypedGameEventsTests), nameof(TypedGameEventsTests.TestCases))]
        public void AddActionListenerTest<T, TU>(T gameEvent, TU value) where T : GameEventBase<TU>
        {
            var mockAction = Substitute.For<Action>();
            var mockTyppedAction = Substitute.For<Action<TU>>();
            gameEvent.AddListener(mockAction);
            gameEvent.AddListener(mockAction);
            gameEvent.AddListener(mockTyppedAction);
            Assert.AreNotEqual(4, gameEvent.GetActionsCount(), "Should not allow for the repetition of Listeners");
            Assert.AreEqual(2, gameEvent.GetActionsCount(), "Event listener was not registred");
            Assert.AreEqual(1, gameEvent.TypedActions.Count, "Event listener was not registred");

            ScriptableObject.DestroyImmediate(gameEvent);
        }

        [Test]
        [TestCaseSource(typeof(TypedGameEventsTests), nameof(TypedGameEventsTests.TestCases))]
        public void RaiseEnabledTypedGameEventTest<T, TU>(T gameEvent, TU value) where T : GameEventBase<TU>
        {
            var mockAction = Substitute.For<Action>();
            var mockListener = Substitute.For<IGameEventListener>();

            var mockTypedAction = Substitute.For<Action<TU>>();
            var mockTypedListener = Substitute.For<IGameEventListener<TU>>();


            gameEvent.AddListener(mockAction);
            gameEvent.AddListener(mockListener);
            gameEvent.AddListener(mockTypedAction);
            gameEvent.AddListener(mockTypedListener);

            gameEvent.Raise(value);

            mockAction.Received(1).Invoke();
            mockListener.Received(1).OnEventRaised();

            mockTypedAction.Received(1).Invoke(value);
            mockTypedListener.Received(1).OnEventRaised(value);

            ScriptableObject.DestroyImmediate(gameEvent);
        }

        [Test]
        [TestCaseSource(typeof(TypedGameEventsTests), nameof(TypedGameEventsTests.TestCases))]
        public void RaiseDisabledTypedGameEventTest<T, TU>(T gameEvent, TU value) where T : GameEventBase<TU>
        {
            var mockAction = Substitute.For<Action<TU>>();
            var mockListener = Substitute.For<IGameEventListener<TU>>();
            gameEvent.SetEnabled(false);

            gameEvent.AddListener(mockAction);
            gameEvent.AddListener(mockListener);
            gameEvent.Raise(value);

            mockAction.DidNotReceive().Invoke(value);
            mockListener.DidNotReceive().OnEventRaised(value);

            ScriptableObject.DestroyImmediate(gameEvent);
        }

        [Test]
        [TestCaseSource(typeof(TypedGameEventsTests), nameof(TypedGameEventsTests.TestCases))]
        public void RemoveTypedIGameEventListenerTest<T, TU>(T gameEvent, TU value) where T : GameEventBase<TU>
        {
            var mockListener = Substitute.For<IGameEventListener<TU>>();
            gameEvent.AddListener(mockListener);
            gameEvent.RemoveListener(mockListener);
            Assert.Zero(gameEvent.TypedListeners.Count);

            ScriptableObject.DestroyImmediate(gameEvent);
        }

        [Test]
        [TestCaseSource(typeof(TypedGameEventsTests), nameof(TypedGameEventsTests.TestCases))]
        public void RemoveTypedActionListenerTest<T, TU>(T gameEvent, TU value) where T : GameEventBase<TU>
        {
            var mockAction = Substitute.For<Action<TU>>();
            gameEvent.AddListener(mockAction);
            gameEvent.RemoveListener(mockAction);
            Assert.Zero(gameEvent.TypedActions.Count);

            ScriptableObject.DestroyImmediate(gameEvent);
        }

        [Test]
        [TestCaseSource(typeof(TypedGameEventsTests), nameof(TypedGameEventsTests.TestCases))]
        public void RemoveAllListenersTest<T, TU>(T gameEvent, TU value) where T : GameEventBase<TU>
        {
            var mockAction = Substitute.For<Action<TU>>();
            var mockListener = Substitute.For<IGameEventListener<TU>>();
            gameEvent.AddListener(mockAction);
            gameEvent.AddListener(mockListener);
            gameEvent.RemoveAll();
            Assert.Zero(gameEvent.GetListenersCount());
            Assert.Zero(gameEvent.GetActionsCount());

            ScriptableObject.DestroyImmediate(gameEvent);
        }
    }
}