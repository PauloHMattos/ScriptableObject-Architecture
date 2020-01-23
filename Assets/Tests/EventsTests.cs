using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class EventsTests
    {
        private GameObject testRunnerGameObject;
        private GameObject listenerGameObject;
        private GameEventRaiser eventRaiser;

        [Test, Performance]
        public void GameEventInvokeTest([Values(1, 2, 10, 100, 1000)] int count)
        {
            Measure.Method(() => eventRaiser.Raise()).WarmupCount(0).MeasurementCount(1).IterationsPerMeasurement(count).GC().Run();
        }

        [OneTimeSetUp]
        public void Setup()
        {
            EditorSceneManager.OpenScene(Application.dataPath+ "/Tests/Scenes/GameEventTestScene.unity", OpenSceneMode.Single);
            testRunnerGameObject = GameObject.FindGameObjectWithTag("Player");
            eventRaiser = testRunnerGameObject.GetComponent<GameEventRaiser>();
            listenerGameObject = GameObject.Find("Listener");
        }
    }
}
