using System;
using NSubstitute;
using NUnit.Framework;
using ScriptableObjectArchitecture.Events.Game_Events;
using ScriptableObjectArchitecture.Events.Listeners;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Tests.PlayMode
{

    [TestFixture]
    public class TypelessGameEventsTests
    {
        private GameEvent _gameEvent;

        [SetUp]
        public void SetUp()
        {
            _gameEvent = ScriptableObject.CreateInstance<GameEvent>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_gameEvent);
        }

        [Test]
        public void AddIGameEventListenerTest()
        {
            var mockListener = Substitute.For<IGameEventListener>();
            _gameEvent.AddListener(mockListener);
            _gameEvent.AddListener(mockListener);
            Assert.AreNotEqual(2, _gameEvent.GetActionsCount(), "Should not allow for the repetition of Listeners");
            Assert.AreEqual(1, _gameEvent.GetListenersCount(), "Event listener was not registred");
        }

        [Test]
        public void AddActionListenerTest()
        {
            var mockAction = Substitute.For<Action>();
            _gameEvent.AddListener(mockAction);
            _gameEvent.AddListener(mockAction);
            Assert.AreNotEqual(2, _gameEvent.GetActionsCount(), "Should not allow for the repetition of Listeners");
            Assert.AreEqual(1, _gameEvent.GetActionsCount(), "Event listener was not registred");
        }

        [Test]
        public void RaiseEnabledGameEventTest()
        {
            var mockAction = Substitute.For<Action>();
            var mockListener = Substitute.For<IGameEventListener>();

            _gameEvent.AddListener(mockAction);
            _gameEvent.AddListener(mockListener);

            _gameEvent.Raise();

            mockAction.Received(1).Invoke();
            mockListener.Received(1).OnEventRaised();
        }

        [Test]
        public void RaiseDisabledGameEventTest()
        {
            var mockAction = Substitute.For<Action>();
            var mockListener = Substitute.For<IGameEventListener>();
            _gameEvent.SetEnabled(false);

            _gameEvent.AddListener(mockAction);
            _gameEvent.AddListener(mockListener);
            _gameEvent.Raise();

            mockAction.DidNotReceive().Invoke();
            mockListener.DidNotReceive().OnEventRaised();
        }

        [Test]
        public void SetEnableGameEventTest()
        {
            _gameEvent.SetEnabled(false);
            Assert.IsFalse(_gameEvent.Enabled);
            _gameEvent.SetEnabled(true);
            Assert.IsTrue(_gameEvent.Enabled);
        }

        [Test]
        public void RemoveIGameEventListenerTest()
        {
            var mockListener = Substitute.For<IGameEventListener>();
            _gameEvent.AddListener(mockListener);
            _gameEvent.RemoveListener(mockListener);
            Assert.Zero(_gameEvent.GetListenersCount());
        }

        [Test]
        public void RemoveActionListenerTest()
        {
            var mockAction = Substitute.For<Action>();
            _gameEvent.AddListener(mockAction);
            _gameEvent.RemoveListener(mockAction);
            Assert.Zero(_gameEvent.GetActionsCount());
        }

        [Test]
        public void RemoveAllListenersTest()
        {
            var mockAction = Substitute.For<Action>();
            var mockListener = Substitute.For<IGameEventListener>();
            _gameEvent.AddListener(mockAction);
            _gameEvent.AddListener(mockListener);
            _gameEvent.RemoveAll();
            Assert.Zero(_gameEvent.GetListenersCount());
            Assert.Zero(_gameEvent.GetActionsCount());
        }
    }
}
