using System.Collections;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    //public class EventsTests
    //{
    //    private GameObject testRunnerGameObject;
    //    private GameEventRaiser eventRaiser;

    //    [Test, Performance]
    //    public void GameEventInvokeTest([Values(1, 10, 100, 1000)] int count)
    //    {
    //        //Measure.Method(() => eventRaiser.Raise()).WarmupCount(10).IterationsPerMeasurement(count).GC().Run();
    //    }

    //    [Test, Performance]
    //    public void GameEventInvokeTestNoWarmup([Values(1, 10, 100, 1000)] int count)
    //    {
    //        Measure.Method(() => eventRaiser.Raise()).IterationsPerMeasurement(count).GC().Run();
    //    }

    //    [OneTimeSetUp]
    //    public void Setup()
    //    {
    //        SceneManager.LoadScene("GameEventTestScene", LoadSceneMode.Single);
    //        testRunnerGameObject = GameObject.FindGameObjectWithTag("Player");
    //        eventRaiser = testRunnerGameObject.GetComponent<GameEventRaiser>();
    //    }
    //}
}
